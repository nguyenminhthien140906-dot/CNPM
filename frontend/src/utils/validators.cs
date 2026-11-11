
using System.Text.RegularExpressions;

namespace CollabSphere.Web.Utils
{
    public static class Validators
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(
                email.Trim(),
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return password.Length >=
                Constants.Validation.MinPasswordLength;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return name.Trim().Length <=
                Constants.Validation.MaxNameLength;
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(
                phone.Trim(),
                @"^(0|\+84)[0-9]{9,10}$"
            );
        }

        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidDescription(string description)
        {
            if (description == null)
                return true;

            return description.Length <=
                Constants.Validation.MaxDescriptionLength;
        }

        public static bool IsValidScore(decimal score)
        {
            return score >= 0 && score <= 10;
        }

        public static bool IsValidDeadline(DateTime deadline)
        {
            return deadline.Date >= DateTime.Today;
        }
    }
}

