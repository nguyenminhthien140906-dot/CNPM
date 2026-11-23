import React, { createContext, useCallback, useContext, useEffect, useMemo, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { useAuth } from './AuthContext';
import { useDemo } from './DemoContext';
import { api, apiUrl, getToken } from '../services/api';
const NotificationContext = createContext(null);
export function NotificationProvider({ children }) {
  const { user, isDemo } = useAuth();
  const { data, update } = useDemo();
  const [liveNotifications, setLiveNotifications] = useState([]);
  const [connection, setConnection] = useState('offline');
  const [error, setError] = useState('');
  const [toast, setToast] = useState(null);
  const refresh = useCallback(async () => {
    if (!user || isDemo) return;
    try { const list = await api.notifications(); setLiveNotifications(Array.isArray(list) ? list : []); setError(''); }
    catch (err) { setError(err.message); }
  }, [user?.userId, isDemo]);

  useEffect(() => {
    if (!user || isDemo) { setConnection(isDemo ? 'demo' : 'offline'); return; }
    refresh();
    const hub = new signalR.HubConnectionBuilder()
      .withUrl(apiUrl('/hubs/notifications'), { accessTokenFactory: getToken })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Error)
      .build();
    let mounted = true;
    hub.on('ReceiveNotification', (notification) => {
      if (!mounted) return;
      setLiveNotifications((prev) => [notification, ...prev.filter((n) => (n.notificationId ?? n.NotificationId) !== notification.notificationId)]);
      setToast(notification.title || 'Bạn có thông báo mới');
    });
    hub.onreconnecting(() => mounted && setConnection('connecting'));
    hub.onreconnected(() => mounted && setConnection('online'));
    hub.onclose(() => mounted && setConnection('offline'));
    setConnection('connecting');
    hub.start().then(() => mounted && setConnection('online')).catch(() => mounted && setConnection('offline'));
    return () => { mounted = false; hub.stop().catch(() => {}); };
  }, [user?.userId, isDemo, refresh]);
  const notifications = isDemo ? data.notifications : liveNotifications;
  const unread = notifications.filter((n) => !(n.isRead ?? n.IsRead)).length;
  const markRead = async (id) => {
    if (isDemo) update('notifications', id, { isRead: true }, 'notificationId');
    else { await api.readNotification(id); setLiveNotifications((prev) => prev.map((n) => (n.notificationId ?? n.NotificationId) === id ? { ...n, isRead: true } : n)); }
  };
  const value = useMemo(() => ({ notifications, unread, connection, error, refresh, markRead, toast, setToast }), [notifications, unread, connection, error, refresh, toast]);
  return <NotificationContext.Provider value={value}>{children}</NotificationContext.Provider>;
}
export const useNotifications = () => useContext(NotificationContext);
