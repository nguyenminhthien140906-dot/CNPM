import React, { useEffect, useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { request } from '../services/api';

const PATHS = {
  dashboard: 'M3 3h7v7H3z M14 3h7v7h-7z M3 14h7v7H3z M14 14h7v7h-7z',
  book: 'M4 19.5A2.5 2.5 0 0 1 6.5 17H20 M6.5 2H20v20H6.5A2.5 2.5 0 0 1 4 19.5v-15A2.5 2.5 0 0 1 6.5 2z',
  file: 'M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z M14 2v6h6 M8 13h8 M8 17h8',
  classes: 'M3 21h18 M5 21V5l7-3 7 3v16 M9 9h.01 M15 9h.01 M9 13h.01 M15 13h.01 M9 17h.01 M15 17h.01',
  folder: 'M3 7a2 2 0 0 1 2-2h5l2 2h7a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z',
  users: 'M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2 M9 11a4 4 0 1 0 0-8 4 4 0 0 0 0 8z M22 21v-2a4 4 0 0 0-3-3.87 M16 3.13a4 4 0 0 1 0 7.75',
  tasks: 'M8 6h13 M8 12h13 M8 18h13 M3 6h.01 M3 12h.01 M3 18h.01',
  target: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20z M12 18a6 6 0 1 0 0-12 6 6 0 0 0 0 12z M12 14a2 2 0 1 0 0-4 2 2 0 0 0 0 4z',
  message: 'M21 11.5a8.38 8.38 0 0 1-.9 3.8A8.5 8.5 0 0 1 12.5 20a8.38 8.38 0 0 1-3.8-.9L3 21l1.9-5.7a8.38 8.38 0 0 1-.9-3.8A8.5 8.5 0 0 1 12.5 3h.5a8.5 8.5 0 0 1 8 8z',
  video: 'M15 10l5-3v10l-5-3 M3 7a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z',
  resources: 'M6 3h12l3 4v14H3V7z M6 3v5h12V3 M8 21v-8h8v8',
  pen: 'M12 19l7-7 3 3-7 7-4 1z M18 13l-7-7-8 8v7h7 M3 3l4 4',
  star: 'M12 2l3.1 6.3 7 1-5.1 5 .9 7-5.9-3.1-5.9 3.1.9-7-5.1-5 7-1z',
  chart: 'M3 3v18h18 M7 15l4-4 4 3 5-7',
  bell: 'M18 8a6 6 0 0 0-12 0c0 7-3 7-3 9h18c0-2-3-2-3-9 M10 21h4',
  settings: 'M12 15.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z M19.4 15a1.7 1.7 0 0 0 .34 1.9l.06.06-2 2-.06-.06a1.7 1.7 0 0 0-1.9-.34l-.1.04V21h-3v-2.4a1.7 1.7 0 0 0-1.9-.34l-.1.04-1.7 1.7-2-2 1.7-1.7a1.7 1.7 0 0 0-.34-1.9l-.04-.1H5v-3h2.4a1.7 1.7 0 0 0 .34-1.9l-.04-.1L6 7.6l2-2 1.7 1.7a1.7 1.7 0 0 0 1.9-.34l.1-.04V4h3v2.4a1.7 1.7 0 0 0 1.9.34l.1-.04 1.7-1.7 2 2-1.7 1.7a1.7 1.7 0 0 0 .34 1.9l.04.1H21v3h-2.4z',
  search: 'M11 19a8 8 0 1 0 0-16 8 8 0 0 0 0 16z M21 21l-4.35-4.35',
  plus: 'M12 5v14 M5 12h14',
  arrow: 'M5 12h14 M13 6l6 6-6 6',
  menu: 'M4 7h16 M4 12h16 M4 17h16',
  logout: 'M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4 M16 17l5-5-5-5 M21 12H9',
  chevron: 'M6 9l6 6 6-6',
  clock: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20z M12 6v6l4 2',
  shield: 'M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z M9 12l2 2 4-4',
  download: 'M12 3v12 M7 10l5 5 5-5 M4 17v4h16v-4',
  edit: 'M12 20h9 M16.5 3.5a2.1 2.1 0 0 1 3 3L9 17l-4 1 1-4z',
  trash: 'M3 6h18 M8 6V4h8v2 M5 6l1 15h12l1-15 M10 10v7 M14 10v7',
  filter: 'M4 7h16 M7 12h10 M10 17h4',
  send: 'M22 2L11 13 M22 2l-7 20-4-9-9-4z',
  refresh: 'M20 11a8 8 0 0 0-14-5L3 9 M3 3v6h6 M4 13a8 8 0 0 0 14 5l3-3 M21 21v-6h-6',
  external: 'M14 3h7v7 M21 3l-10 10 M21 13v8H3V3h8',
  close: 'M18 6L6 18 M6 6l12 12',
  layers: 'M12 2l9 5-9 5-9-5z M3 12l9 5 9-5 M3 17l9 5 9-5',
  upload: 'M12 16V3 M7 8l5-5 5 5 M4 16v5h16v-5',
  link: 'M10 13a5 5 0 0 0 7.1 0l3-3a5 5 0 0 0-7.1-7.1l-1.7 1.7 M14 11a5 5 0 0 0-7.1 0l-3 3a5 5 0 0 0 7.1 7.1l1.7-1.7',
  check: 'M20 6L9 17l-5-5',
  alert: 'M12 2l10 18H2z M12 9v4 M12 17h.01',
  user: 'M20 21v-2a7 7 0 0 0-14 0v2 M13 11a4 4 0 1 0 0-8 4 4 0 0 0 0 8z',
  eye: 'M2 12s4-7 10-7 10 7 10 7-4 7-10 7-10-7-10-7z M12 15a3 3 0 1 0 0-6 3 3 0 0 0 0 6z',
  rocket: 'M4.5 16.5c-1.5 1.3-2.5 4.5-2.5 4.5s3.2-1 4.5-2.5 M12 16l-4 4-4-4 4-4 M13 11c4-5 8-7 9-9-1 8-4 13-9 17l-5-5z',
  home: 'M3 10l9-7 9 7v11H3z M9 21v-8h6v8',
  sparkles: 'M12 3l1.7 6.3L20 11l-6.3 1.7L12 19l-1.7-6.3L4 11l6.3-1.7z M20 18v4 M18 20h4',
  checkcircle: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20z M7 12l3 3 7-7',
  info: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20z M12 11v6 M12 7h.01',
  lock: 'M5 11h14v10H5z M8 11V7a4 4 0 0 1 8 0v4',
  calendar: 'M3 5h18v16H3z M7 3v4 M17 3v4 M3 10h18',
  more: 'M5 12h.01 M12 12h.01 M19 12h.01',
  clipboard: 'M9 3h6l1 3h3v16H5V6h3z M9 12h6 M9 16h6',
  undo: 'M3 7v6h6 M3 13a8 8 0 1 1 2 5',
  eraser: 'M20 20H9l-7-7 9-9a2 2 0 0 1 3 0l8 8a2 2 0 0 1 0 3l-5 5',
  circle: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20z',
};
export function Icon({ name = 'circle', size = 19, strokeWidth = 1.8, className = '' }) {
  return <svg className={`icon ${className}`} width={size} height={size} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth={strokeWidth} strokeLinecap="round" strokeLinejoin="round" aria-hidden="true"><path d={PATHS[name] || PATHS.circle}/></svg>;
}
export function Badge({ children, tone = 'neutral' }) { return <span className={`badge badge-${tone}`}>{children}</span>; }
const statusNames = { DONE: 'Hoàn thành', IN_PROGRESS: 'Đang thực hiện', TODO: 'Cần làm', REVIEW: 'Chờ duyệt', SUBMITTED: 'Đã nộp', PENDING: 'Chờ phê duyệt', APPROVED: 'Đã duyệt', REJECTED: 'Từ chối', DRAFT: 'Bản nháp', OPEN: 'Đang mở', PLANNED: 'Dự kiến', CLOSED: 'Đã đóng', ARCHIVED: 'Lưu trữ', HIGH: 'Cao', MEDIUM: 'Trung bình', LOW: 'Thấp', ACTIVE: 'Hoạt động' };
export function Status({ value }) {
  const tone = ['DONE','APPROVED','SUBMITTED','OPEN','ACTIVE'].includes(value) ? 'success' : ['IN_PROGRESS','REVIEW'].includes(value) ? 'info' : ['PENDING','PLANNED','MEDIUM'].includes(value) ? 'warning' : ['REJECTED','HIGH','CLOSED'].includes(value) ? 'danger' : 'neutral';
  return <Badge tone={tone}>{statusNames[value] || value || '—'}</Badge>;
}
export function Button({ children, icon, variant = 'primary', size = '', className = '', type = 'button', ...props }) {
  return <button type={type} className={`btn btn-${variant} ${size ? `btn-${size}` : ''} ${className}`} {...props}>{icon && <Icon name={icon} size={17}/>}<span>{children}</span></button>;
}
export function PageHeader({ eyebrow, title, description, action, children }) {
  return <div className="page-heading"><div><div className="eyebrow">{eyebrow || 'COLLABSPHERE / PBL WORKSPACE'}</div><h1>{title}</h1>{description && <p>{description}</p>}</div>{(action || children) && <div className="heading-actions">{action}{children}</div>}</div>;
}
export function Panel({ title, subtitle, action, children, className = '', noPad = false }) {
  return <section className={`panel ${className}`}><div className="panel-heading"><div><h3>{title}</h3>{subtitle && <p>{subtitle}</p>}</div>{action}</div><div className={noPad ? '' : 'panel-body'}>{children}</div></section>;
}
export function Empty({ icon = 'folder', title = 'Chưa có dữ liệu', text = 'Nội dung sẽ xuất hiện tại đây khi được tạo.', action }) {
  return <div className="empty"><div className="empty-icon"><Icon name={icon} size={27}/></div><strong>{title}</strong><p>{text}</p>{action}</div>;
}
export function ErrorBox({ error, onRetry }) { return error ? <div className="error-box"><Icon name="alert" size={18}/><span>{error}</span>{onRetry && <button type="button" onClick={onRetry}>Thử lại</button>}</div> : null; }
export function Loading() { return <div className="loading"><div className="spinner"/> Đang tải dữ liệu...</div>; }
export function Progress({ value = 0, color = 'purple' }) { const n = Math.min(100, Math.max(0, Number(value) || 0)); return <div className="progress"><div className={`progress-fill ${color}`} style={{ width: `${n}%` }}/></div>; }
export function Metric({ icon, label, value, hint, color = 'purple' }) { return <div className="metric"><div className={`metric-icon ${color}`}><Icon name={icon} size={22}/></div><div className="metric-label">{label}</div><div className="metric-value">{value}</div><div className="metric-hint">{hint}</div></div>; }
export function Avatar({ name = '', size = 'md' }) { const colors = ['#7b68ee','#18a999','#f3a247','#da7297','#5d91db']; const code = Array.from(name).reduce((a, c) => a + c.charCodeAt(0), 0); const initials = name.trim().split(/\s+/).slice(-2).map((s) => s[0]).join('').toUpperCase(); return <span className={`avatar avatar-${size}`} style={{ backgroundColor: colors[code % colors.length] }}>{initials || '?'}</span>; }
export function Modal({ title, subtitle, children, onClose, footer }) { useEffect(() => { const fn = (e) => e.key === 'Escape' && onClose(); document.addEventListener('keydown', fn); return () => document.removeEventListener('keydown', fn); }, [onClose]); return <div className="modal-backdrop" onMouseDown={(e) => e.target === e.currentTarget && onClose()}><div className="modal" role="dialog" aria-modal="true" aria-label={title}><div className="modal-head"><div><h2>{title}</h2>{subtitle && <p>{subtitle}</p>}</div><button className="icon-btn" onClick={onClose} aria-label="Đóng"><Icon name="close"/></button></div><div className="modal-body">{children}</div>{footer && <div className="modal-footer">{footer}</div>}</div></div>; }
export function FormModal({ title, subtitle, fields, initial = {}, onClose, onSave, submitText = 'Lưu thay đổi' }) {
  const [values, setValues] = useState(() => Object.fromEntries(fields.map((f) => [f.name, initial[f.name] ?? f.defaultValue ?? ''])));
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');
  const submit = async (e) => { e.preventDefault(); setSaving(true); setError(''); try { await onSave(values); onClose(); } catch (err) { setError(err.message); } finally { setSaving(false); } };
  return <Modal title={title} subtitle={subtitle} onClose={onClose}><form onSubmit={submit} className="form-stack"><ErrorBox error={error}/>{fields.map((f) => <label key={f.name} className="field"><span>{f.label}{f.required && <b> *</b>}</span>{f.type === 'select' ? <select value={values[f.name]} required={f.required} onChange={(e) => setValues({ ...values, [f.name]: e.target.value })}><option value="">Chọn {f.label.toLowerCase()}</option>{(f.options || []).map((o) => <option key={typeof o === 'object' ? o.value : o} value={typeof o === 'object' ? o.value : o}>{typeof o === 'object' ? o.label : o}</option>)}</select> : f.type === 'textarea' ? <textarea rows={4} value={values[f.name]} placeholder={f.placeholder || ''} required={f.required} onChange={(e) => setValues({ ...values, [f.name]: e.target.value })}/> : <input type={f.type || 'text'} value={values[f.name]} placeholder={f.placeholder || ''} required={f.required} min={f.min} max={f.max} step={f.step} onChange={(e) => setValues({ ...values, [f.name]: e.target.value })}/>}</label>)}<div className="form-actions"><Button variant="subtle" onClick={onClose}>Hủy</Button><Button type="submit" icon="check" disabled={saving}>{saving ? 'Đang lưu...' : submitText}</Button></div></form></Modal>;
}
export function DemoNotice({ feature = 'Chức năng' }) { const { isDemo } = useAuth(); return <div className="notice"><Icon name="info" size={18}/><div><strong>{isDemo ? 'Chế độ trải nghiệm' : 'Chưa có API trong mã nguồn'}</strong><span>{isDemo ? `${feature} đang dùng dữ liệu mẫu lưu trên trình duyệt, không đồng bộ MySQL.` : `${feature} chưa có endpoint backend tương ứng trong ZIP. Không ghi dữ liệu giả lên máy chủ.`}</span></div></div>; }
export function useCollection(path, demoItems, deps = []) {
  const { isDemo, user } = useAuth();
  const [items, setItems] = useState([]); const [loading, setLoading] = useState(false); const [error, setError] = useState(''); const [tick, setTick] = useState(0);
  useEffect(() => {
    if (isDemo) { setItems(demoItems || []); setError(''); setLoading(false); return; }
    if (!user || !path) { setItems([]); return; }
    const abort = new AbortController(); setLoading(true); setError('');
    request(path, { signal: abort.signal }).then((result) => {
      const list = Array.isArray(result) ? result : result?.items || result?.users || result?.data || [];
      setItems(Array.isArray(list) ? list : []);
    }).catch((err) => { if (!abort.signal.aborted) setError(err.message); }).finally(() => { if (!abort.signal.aborted) setLoading(false); });
    return () => abort.abort();
  }, [isDemo, user?.userId, path, tick, ...deps]);
  return { items: isDemo ? (demoItems || []) : items, loading, error, refresh: () => setTick((v) => v + 1) };
}
export const dateLabel = (v) => { if (!v) return '—'; const d = new Date(v); return Number.isNaN(d.getTime()) ? String(v) : d.toLocaleDateString('vi-VN'); };
export const timeLabel = (v) => { if (!v) return '—'; const d = new Date(v); return Number.isNaN(d.getTime()) ? String(v) : d.toLocaleString('vi-VN', { day: '2-digit', month: '2-digit', hour: '2-digit', minute: '2-digit' }); };
export const keyOf = (obj, ...keys) => { for (const key of keys) if (obj?.[key] !== undefined) return obj[key]; return undefined; };
export const confirmDelete = (name) => window.confirm(`Bạn có chắc muốn xóa "${name}"?`);
