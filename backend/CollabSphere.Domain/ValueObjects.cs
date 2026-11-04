using System;
using System.Text.RegularExpressions;

namespace CollabSphere.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Địa chỉ email không hợp lệ!");
            Value = value;
        }
    }

    public record Score
    {
        public float Value { get; }

        public Score(float value)
        {
            if (value < 0 || value > 10)
                throw new ArgumentException("Điểm số phải nằm trong khoảng từ 0 đến 10.");
            Value = value;
        }
    }
}
