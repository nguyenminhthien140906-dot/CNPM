import React, { useState } from 'react';
import { Badge, Button, ErrorBox, Icon, Modal } from './ui';

// Small RFC-4180-style CSV parser, supporting quoted cells, escaped quotes and newlines.
export function parseCsv(text) {
  const rows = []; let row = [], cell = '', quoted = false;
  const source = String(text).replace(/^\uFEFF/, '');
  for (let i = 0; i < source.length; i++) {
    const ch = source[i];
    if (ch === '"') {
      if (quoted && source[i+1] === '"') { cell += '"'; i++; }
      else quoted = !quoted;
    } else if (ch === ',' && !quoted) { row.push(cell.trim()); cell = ''; }
    else if ((ch === '\n' || ch === '\r') && !quoted) {
      if (ch === '\r' && source[i+1] === '\n') i++;
      row.push(cell.trim()); if (row.some(Boolean)) rows.push(row);
      row = []; cell = '';
    } else cell += ch;
  }
  if (quoted) throw new Error('File CSV có dấu ngoặc kép chưa được đóng.');
  row.push(cell.trim()); if (row.some(Boolean)) rows.push(row);
  if (rows.length < 2) throw new Error('CSV cần có dòng tiêu đề và ít nhất một dòng dữ liệu.');
  const headers = rows[0].map((h) => h.trim());
  if (new Set(headers).size !== headers.length) throw new Error('Tên cột CSV không được trùng nhau.');
  return rows.slice(1).map((cells, index) => {
    if (cells.length !== headers.length) throw new Error(`Dòng ${index+2} có ${cells.length} cột, cần ${headers.length} cột.`);
    return Object.fromEntries(headers.map((h, i) => [h, cells[i]]));
  });
}
export default function CsvImportModal({ title, columns, onImport, onClose }) {
  const [rows, setRows] = useState([]); const [error, setError] = useState(''); const [busy, setBusy] = useState(false);
  const read = async (e) => { const file = e.target.files?.[0]; if (!file) return; try { if (file.size > 1024 * 1024) throw new Error('CSV tối đa 1 MB trong chế độ demo.'); const parsed = parseCsv(await file.text()); const missing = columns.filter((c) => !Object.prototype.hasOwnProperty.call(parsed[0],c)); if (missing.length) throw new Error(`Thiếu cột: ${missing.join(', ')}`); if (parsed.length > 500) throw new Error('Chỉ hỗ trợ tối đa 500 dòng mỗi lần nhập.'); setRows(parsed); setError(''); } catch (err) { setError(err.message); setRows([]); } };
  const save = async () => { setBusy(true); try { await onImport(rows); onClose(); } catch (err) { setError(err.message); } finally { setBusy(false); } };
  return <Modal title={title} subtitle="Nhập dữ liệu CSV vào bản demo; không gửi tệp lên máy chủ" onClose={onClose}><div className="form-stack"><div className="notice"><Icon name="info" size={17}/><div><strong>Định dạng CSV</strong><span>Dòng đầu chứa tên cột: {columns.join(', ')}</span></div></div><label className="field"><span>Chọn tệp CSV (UTF-8)</span><input type="file" accept=".csv,text/csv" onChange={read}/></label><ErrorBox error={error}/>{rows.length > 0 && <><div className="section-row"><strong>Xem trước dữ liệu</strong><Badge tone="info">{rows.length} dòng</Badge></div><div className="table-wrap"><table className="data-table"><thead><tr>{columns.map((c) => <th key={c}>{c}</th>)}</tr></thead><tbody>{rows.slice(0,5).map((r,i) => <tr key={i}>{columns.map((c) => <td key={c}>{r[c] || '—'}</td>)}</tr>)}</tbody></table></div>{rows.length > 5 && <small>Đang xem trước 5/{rows.length} dòng.</small>}</>}<div className="form-actions"><Button variant="subtle" onClick={onClose}>Hủy</Button><Button icon="upload" disabled={!rows.length || busy} onClick={save}>{busy ? 'Đang nhập...' : `Nhập ${rows.length} dòng`}</Button></div></div></Modal>;
}
