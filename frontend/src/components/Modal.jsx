
import React, { useEffect } from "react";
import "./Modal.css";

const Modal = ({
    isOpen = false,
    title = "Thông báo",
    onClose,
    children,
    footer,
    closeOnOutsideClick = true
}) => {

    useEffect(() => {
        if (!isOpen) return;

        const handleKeyDown = (event) => {
            if (event.key === "Escape") {
                onClose?.();
            }
        };

        document.addEventListener("keydown", handleKeyDown);

        return () => {
            document.removeEventListener("keydown", handleKeyDown);
        };
    }, [isOpen, onClose]);

    if (!isOpen) {
        return null;
    }

    const handleOverlayClick = () => {
        if (closeOnOutsideClick) {
            onClose?.();
        }
    };

    return (
        <div
            className="modal-overlay"
            onClick={handleOverlayClick}
        >
            <div
                className="modal-container"
                onClick={(event) => event.stopPropagation()}
                role="dialog"
                aria-modal="true"
                aria-label={title}
            >
                <div className="modal-header">
                    <h3>{title}</h3>

                    <button
                        type="button"
                        className="modal-close"
                        onClick={onClose}
                        aria-label="Đóng"
                    >
                        &times;
                    </button>
                </div>

                <div className="modal-body">
                    {children}
                </div>

                {footer != null ? (
                    <div className="modal-footer">
                        {footer}
                    </div>
                ) : (
                    <div className="modal-footer">
                        <button
                            type="button"
                            className="btn-cancel"
                            onClick={onClose}
                        >
                            Đóng
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Modal;
