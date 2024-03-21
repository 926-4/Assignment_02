using System.Runtime;
namespace Valid
{
    
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
        public IValidator getValidator(ValidationMode validationMode, string config="default")
        {
            switch (validationMode)
            {
                case ValidationMode.SQL_COMMAND_SANITIZER: return new SQLValidator(config);
                case ValidationMode.CENSOR_FORBIDDEN_WORDS: return new CensoringValidator(config);
                case ValidationMode.VALIDATE_PHONE_NUMBER: return new PhoneNumberValidator(config);
                case ValidationMode.VALIDATE_EMAIL_ADDRESS: return new EmailAddressValidator(config);
                case ValidationMode.VALIDATE_DATE_TIME: return new DateValidator(config);
                default: throw new Exception("");
            }
        }
    }
}