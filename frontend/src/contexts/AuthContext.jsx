import React, { createContext, useCallback, useContext, useEffect, useState } from 'react';
import { api } from '../services/api';

const AuthContext = createContext(null);
const DEMO_ROLES = {
  ADMIN: { username: 'admin-demo', fullName: 'Quản trị viên', roleCode: 'ADMIN', userId: 1 },
  HEAD: { username: 'head-demo', fullName: 'Trưởng bộ môn', roleCode: 'HEAD', userId: 2 },
  STAFF: { username: 'staff-demo', fullName: 'Nhân viên học vụ', roleCode: 'STAFF', userId: 3 },
  LECTURER: { username: 'lecturer-demo', fullName: 'Giảng viên Nguyễn Minh Anh', roleCode: 'LECTURER', userId: 4 },
  STUDENT: { username: 'student-demo', fullName: 'Sinh viên Trần Gia Bảo', roleCode: 'STUDENT', userId: 5 },
};
export const ROLE_LABELS = { ADMIN: 'Quản trị viên', HEAD: 'Trưởng bộ môn', STAFF: 'Nhân viên học vụ', LECTURER: 'Giảng viên', STUDENT: 'Sinh viên' };
export const canManageAcademic = (user) => ['ADMIN', 'HEAD', 'STAFF'].includes(user?.roleCode);
export const canManageProject = (user) => ['LECTURER', 'HEAD'].includes(user?.roleCode);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(() => {
    try { return JSON.parse(sessionStorage.getItem('collabsphere_user') || 'null'); } catch { return null; }
  });
  const [mode, setMode] = useState(() => sessionStorage.getItem('collabsphere_mode') || '');
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');

  const save = useCallback((nextUser, nextMode, token = '') => {
    setUser(nextUser); setMode(nextMode); setError('');
    if (nextUser) {
      sessionStorage.setItem('collabsphere_user', JSON.stringify(nextUser));
      sessionStorage.setItem('collabsphere_mode', nextMode);
    } else {
      sessionStorage.removeItem('collabsphere_user');
      sessionStorage.removeItem('collabsphere_mode');
    }
    if (token) sessionStorage.setItem('collabsphere_token', token);
    else sessionStorage.removeItem('collabsphere_token');
  }, []);

  const login = async (username, password) => {
    setBusy(true); setError('');
    try {
      const data = await api.login(username.trim(), password);
      const token = data?.accessToken || data?.AccessToken;
      const account = data?.user || data?.User;
      if (!token || !account) throw new Error('API đăng nhập không trả về token hoặc thông tin tài khoản.');
      save({ ...account, roleCode: account.roleCode || account.RoleCode, fullName: account.fullName || account.FullName }, 'live', token);
      return true;
    } catch (err) { setError(err.message); return false; }
    finally { setBusy(false); }
  };
  const demoLogin = (role = 'STUDENT') => save(DEMO_ROLES[role] || DEMO_ROLES.STUDENT, 'demo');
  const logout = () => save(null, '');
  const switchDemoRole = (role) => { if (mode === 'demo') demoLogin(role); };

  useEffect(() => {
    if (mode !== 'live' || !user) return;
    let active = true;
    api.me().then((me) => {
      if (active && me?.role) setUser((prev) => ({ ...prev, roleCode: me.role, username: me.username || prev.username }));
    }).catch(() => { if (active) logout(); });
    return () => { active = false; };
  }, []);

  return <AuthContext.Provider value={{ user, mode, busy, error, login, demoLogin, logout, switchDemoRole, isDemo: mode === 'demo' }}>{children}</AuthContext.Provider>;
}
export const useAuth = () => useContext(AuthContext);
