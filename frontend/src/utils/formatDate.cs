
using System.Globalization;

namespace CollabSphere.Web.Utils
{
    public static class FormatDate
    {
        private static readonly CultureInfo VietnameseCulture =
            new CultureInfo("vi-VN");

        public static string ToDate(DateTime? date)
        {
            if (date == null)
                return "Chưa có ngày";

            return date.Value.ToString(
                "dd/MM/yyyy",
                VietnameseCulture
            );
        }

        public static string ToDateTime(DateTime? date)
        {
            if (date == null)
                return "Chưa có thời gian";

            return date.Value.ToString(
                "dd/MM/yyyy HH:mm",
                VietnameseCulture
            );
        }

        public static string ToShortDate(DateTime? date)
        {
            if (date == null)
                return "";

            return date.Value.ToString("dd/MM");
        }

        public static bool IsOverdue(DateTime? deadline)
        {
            if (deadline == null)
                return false;

            return deadline.Value < DateTime.Now;
        }

        public static int DaysRemaining(DateTime? deadline)
        {
            if (deadline == null)
                return 0;

            return (deadline.Value.Date - DateTime.Today).Days;
        }

        public static string GetDeadlineStatus(DateTime? deadline)
        {
            if (deadline == null)
                return "Chưa đặt thời hạn";

            int days = DaysRemaining(deadline);

            if (days < 0)
                return "Đã quá hạn";

            if (days == 0)
                return "Hạn chót hôm nay";

            if (days <= 3)
                return "Sắp đến hạn";

            return "Còn thời gian";
        }
    }
}

