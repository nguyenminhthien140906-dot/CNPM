
export const toDate = (date) => {
    if (!date) {
        return "Chưa có ngày";
    }

    return new Date(date).toLocaleDateString("vi-VN", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric"
    });
};

export const toDateTime = (date) => {
    if (!date) {
        return "Chưa có thời gian";
    }

    return new Date(date).toLocaleString("vi-VN", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        hour12: false
    });
};

export const toShortDate = (date) => {
    if (!date) {
        return "";
    }

    return new Date(date).toLocaleDateString("vi-VN", {
        day: "2-digit",
        month: "2-digit"
    });
};

export const isOverdue = (deadline) => {
    if (!deadline) {
        return false;
    }

    return new Date(deadline) < new Date();
};

export const daysRemaining = (deadline) => {
    if (!deadline) {
        return 0;
    }

    const today = new Date();
    const target = new Date(deadline);

    today.setHours(0, 0, 0, 0);
    target.setHours(0, 0, 0, 0);

    return Math.ceil(
        (target - today) / (1000 * 60 * 60 * 24)
    );
};

export const getDeadlineStatus = (deadline) => {
    if (!deadline) {
        return "Chưa đặt thời hạn";
    }

    const days = daysRemaining(deadline);

    if (days < 0) {
        return "Đã quá hạn";
    }

    if (days === 0) {
        return "Hạn chót hôm nay";
    }

    if (days <= 3) {
        return "Sắp đến hạn";
    }

    return "Còn thời gian";
};

const FormatDate = {
    toDate,
    toDateTime,
    toShortDate,
    isOverdue,
    daysRemaining,
    getDeadlineStatus
};

export default FormatDate;
