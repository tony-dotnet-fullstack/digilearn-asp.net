namespace Common.Application.Validation
{
    public static class ValidationMessages
    {
        public const string Required = "";
        public const string InvalidPhoneNumber = "";
        public const string NotFound = "";
        public const string MaxLength = "";
        public const string MinLength = "";

        public static string required(string field) => $"{field}   ";
        public static string maxLength(string field, int maxLength) => $"{field}    {maxLength}  ";
        public static string minLength(string field, int minLength) => $"{field}    {minLength}  ";
    }
}