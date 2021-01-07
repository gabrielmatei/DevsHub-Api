namespace DevsHub.Helpers
{
    public static class ValidationErrors
    {
        public static string NotNull = "notNull";
        public static string NotEmpty = "notEmpty";
        public static string MinimumLength = "minimumLength";
        public static string MaximumLength = "maximumLength";
        public static string EmailAddress = "emailAddress";
        public static string PasswordMinimumLength = "passwordMinimumLength";
        public static string PasswordMaximumLength = "passwordMaximumLength";
        public static string PasswordUppercaseLetter = "passwordUppercaseLetter";
        public static string PasswordLowercaseLetter = "passwordLowercaseLetter";
        public static string PasswordDigit = "passwordDigit";
        public static string PasswordSpecialCharacter = "passwordSpecialCharacter";
        public static string ConfirmPassword = "confirmPassword";
        public static string IsRole = "isRole";
        public static string IsAnnouncementType = "isAnnouncementType";
        public static string GreaterThanNow = "greaterThanNow";
        public static string GreaterThanStart = "greaterThanStart";
    }
}
