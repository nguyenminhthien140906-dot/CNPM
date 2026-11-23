// API contract checked against backend/CollabSphere.API/Controllers.
// During Vite development /api and /hubs are proxied to ASP.NET Core.
// VITE_API_URL configures the Vite proxy target, not the browser's cross-origin base.
// For production on a separate domain, use VITE_PUBLIC_API_URL and enable CORS on the API.
const configuredBase = (import.meta.env.VITE_PUBLIC_API_URL || '').replace(/\/$/, '');
export const API_ORIGIN = configuredBase;
export const apiUrl = (path) => `${API_ORIGIN}${path.startsWith('/') ? path : `/${path}`}`;
export const getToken = () => sessionStorage.getItem('collabsphere_token') || '';

export async function request(path, { method = 'GET', body, token = getToken(), signal } = {}) {
  let response;
  try {
    response = await fetch(apiUrl(path), {
      method, signal,
      headers: {
        ...(body !== undefined ? { 'Content-Type': 'application/json' } : {}),
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
      },
      ...(body !== undefined ? { body: JSON.stringify(body) } : {}),
    });
  } catch {
    throw new Error('Không kết nối được API. Hãy kiểm tra ASP.NET Core tại localhost:5000.');
  }
  const raw = await response.text();
  let json = null;
  try { json = raw ? JSON.parse(raw) : null; } catch { json = { message: raw }; }
  if (!response.ok || json?.success === false) {
    const errors = json?.error?.details || json?.errors;
    const details = errors && typeof errors === 'object' ? Object.values(errors).flat().join(' · ') : '';
    const message = json?.error?.message || json?.message || `API trả về HTTP ${response.status}`;
    throw new Error(details ? `${message} ${details}` : message);
  }
  // Some endpoints return ApiResponse<T>, some raw arrays or custom AI objects.
  return json?.data !== undefined ? json.data : json;
}

export const api = {
  login: (usernameOrEmail, password) => request('/api/auth/login', { method: 'POST', body: { usernameOrEmail, password }, token: '' }),
  me: () => request('/api/auth/me'),
  subjects: () => request('/api/subjects'),
  syllabi: () => request('/api/syllabi'),
  classes: () => request('/api/classes'),
  lecturers: () => request('/api/lecturers'),
  students: () => request('/api/students'),
  classStudents: (id) => request(`/api/classes/${id}/students`),
  classLecturers: (id) => request(`/api/classes/${id}/lecturers`),
  adminUsers: (search = '') => request(`/api/admin/users?page=1&pageSize=100${search ? `&search=${encodeURIComponent(search)}` : ''}`),
  notifications: () => request('/api/notifications'),
  readNotification: (id) => request(`/api/notifications/${id}/read`, { method: 'PATCH' }),
  meetings: (teamId) => request(`/api/meetings/team/${teamId}`),
  chat: (message, conversationId) => request('/api/ai/chat', { method: 'POST', body: { message, ...(conversationId ? { conversationId } : {}) } }),
  aiHistory: (id) => request(`/api/ai/conversations/${id}/messages`),
  progress: (teamId) => request(`/api/ai/teams/${teamId}/progress`),
  contributions: (teamId) => request(`/api/ai/teams/${teamId}/contributions`),
};
