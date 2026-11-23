import React, { createContext, useCallback, useContext, useState } from 'react';
import { demoSeed } from '../data/demoData';
const KEY = 'collabsphere_demo_v2';
const DemoContext = createContext(null);
function initialData() {
  try {
    const raw = JSON.parse(localStorage.getItem(KEY) || 'null');
    if (raw && typeof raw === 'object') return { ...structuredClone(demoSeed), ...raw };
  } catch { /* ignore invalid saved state */ }
  return structuredClone(demoSeed);
}
export function DemoProvider({ children }) {
  const [data, setData] = useState(initialData);
  const commit = useCallback((update) => setData((previous) => {
    const next = typeof update === 'function' ? update(previous) : update;
    try { localStorage.setItem(KEY, JSON.stringify(next)); } catch { /* storage may be disabled */ }
    return next;
  }), []);
  const add = (collection, item) => commit((previous) => ({ ...previous, [collection]: [...(previous[collection] || []), { id: Date.now(), ...item }] }));
  const update = (collection, id, patch, idKey = 'id') => commit((previous) => ({ ...previous, [collection]: (previous[collection] || []).map((item) => String(item[idKey]) === String(id) ? { ...item, ...patch } : item) }));
  const remove = (collection, id, idKey = 'id') => commit((previous) => ({ ...previous, [collection]: (previous[collection] || []).filter((item) => String(item[idKey]) !== String(id)) }));
  const reset = () => { try { localStorage.removeItem(KEY); } catch {} setData(structuredClone(demoSeed)); };
  return <DemoContext.Provider value={{ data, add, update, remove, commit, reset }}>{children}</DemoContext.Provider>;
}
export const useDemo = () => useContext(DemoContext);
