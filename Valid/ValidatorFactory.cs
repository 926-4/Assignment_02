using System.Runtime.CompilerServices;
namespace Valid
{
    class EmailAddressValidator : IValidator
    {
        internal EmailAddressValidator(string config = "full") { }
        public string Apply(string input)
        {
            return input;
        }
    }
    class DateTimeValidator : IValidator
    {
        internal DateTimeValidator(string config = "full") { }
        public string Apply(string input)
        {
            return input;
        }
    }
    public enum ValidationMode
    {
        SQL_COMMAND_SANITIZER,
        CENSOR_FORBIDDEN_WORDS,
        VALIDATE_PHONE_NUMBER,
        VALIDATE_EMAIL_ADDRESS,
        VALIDATE_DATE_TIME
    }
    public class ValidatorFactory
    {
        public IValidator getValidator(ValidationMode validationMode, string config="full")
        {
            switch (validationMode)
            {
                case ValidationMode.SQL_COMMAND_SANITIZER: return new SQLValidator(config);
                case ValidationMode.CENSOR_FORBIDDEN_WORDS: return new CensoringValidator(config);
                case ValidationMode.VALIDATE_PHONE_NUMBER: return new PhoneNumberValidator(config);
                case ValidationMode.VALIDATE_EMAIL_ADDRESS: return new EmailAddressValidator(config);
                case ValidationMode.VALIDATE_DATE_TIME: return new DateTimeValidator(config);
            }
        }
    }
}