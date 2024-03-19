using System.Runtime.CompilerServices;
namespace Valid
{
    
    class CensoringValidator : IValidator
    {
        internal CensoringValidator(string config = "full")
        {

        }
        public string Apply(string input)
        {
            return input;
        }
    }
    class PhoneNumberValidator : IValidator
    {
        internal PhoneNumberValidator(string config = "full") { }
        public string Apply(string input)
        {
            return input;
        }
    }
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
            return new SQLValidator();
        }
    }
}