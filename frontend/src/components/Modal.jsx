import React from 'react';

const Modal = ({ isOpen, title, children, onClose, onSubmit, submitText = "Lưu" }) => {
  if (!isOpen) return null;

  return (
    <div style={styles.overlay}>
      <div style={styles.modal}>
        <h3 style={{ margin: '0 0 15px 0' }}>{title}</h3>
        <div style={{ marginBottom: '20px' }}>{children}</div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '10px' }}>
          <button className="btn" onClick={onClose} style={{ border: '1px solid #cbd5e1' }}>Hủy</button>
          <button className="btn btn-primary" onClick={onSubmit}>{submitText}</button>
        </div>
      </div>
    </div>
  );
};

const styles = {
  overlay: { position: 'fixed', top: 0, left: 0, right: 0, bottom: 0, backgroundColor: 'rgba(0,0,0,0.5)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000 },
  modal: { backgroundColor: '#fff', padding: '24px', borderRadius: '8px', width: '400px', maxWidth: '90%', boxShadow: '0 10px 15px rgba(0,0,0,0.1)' }
};

export default Modal;
