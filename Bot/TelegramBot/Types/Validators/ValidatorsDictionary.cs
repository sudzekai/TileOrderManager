using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace TelegramBot.Types
{
    public static partial class ValidatorsDictionary
    {
        private static readonly Regex FullName = FullNameRegex();

        private static readonly Regex Email = EmailRegex();

        private static readonly Regex Phone = PhoneRegex();


        public static bool IsValidFullName(this string fullName)
            => FullName.IsMatch(fullName);

        public static bool IsValidPhoneNumber(this string phoneNumber)
            => Phone.IsMatch(phoneNumber);

        public static bool IsValidEmail(this string email)
            => Email.IsMatch(email);

        public static string NormalizePhone(this string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            phone = phone.Trim();

            var digits = new string(phone.Where(char.IsDigit).ToArray());

            if (digits.StartsWith("8"))
                digits = "7" + digits[1..];

            return $"+{digits}";
        }

        [GeneratedRegex(@"^[А-ЯЁ][а-яё]+(?:[-'’][А-ЯЁа-яё]+)*\s+[А-ЯЁ][а-яё]+(?:[-'’][А-ЯЁа-яё]+)*\s+[А-ЯЁ][а-яё]+(?:[-'’][А-ЯЁа-яё]+)*$")]
        private static partial Regex FullNameRegex();

        [GeneratedRegex(@"^(?:\+7|8)[\s-]?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{2}[\s-]?\d{2}$")]
        private static partial Regex PhoneRegex();

        [GeneratedRegex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$")]
        private static partial Regex EmailRegex();
    }
}
