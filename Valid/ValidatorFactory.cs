using System.Runtime.CompilerServices;

namespace Valid;

public interface IValidator
{
    string Apply(string input);
}
protected class SQLValidator: IValidator
{
    private static readonly Regex tautologyRegex = new("\\b(.*)\\s*=\\s*\\1\\b", RegexOptions.None);
    private string[] configOptions;
    protected SQLValidator(string config= "batched,tautology") 
    {
        configOptions = config.Split(',');
        foreach (string option in configOptions)
        {
            if(option not in ["batched", "tautology"])
                throw new Exception("SQL Validator config usage -- choice of 'batched', 'tautology', join multiple by ','");
        }
    }
    private string removeBatched(string input) 
    {
        if(input.Contains(";")){
            Console.WriteLine("Detected possible batched command in string -- {0}\nTrimming...", input);
            input = input.Split(";")[0];
        }
        return input;
    }
    private string removeTautology(string input) 
    {
        Match checkForTautology = tautologyRegex.Match(input);
        if (checkForTautology.Success)
        {
            Console.WriteLine("Spotted \"{0}\"", checkForTautology.Value);
            input = input.Replace(checkForTautology.Value, "");
        }
        return input;
    }
    public string Apply(string input)
    {
        foreach(string option in configOptions) 
        { 
            option switch 
            {
                "batched" => input = removeBatched(input).Trim(),
                "tautology" => input = removeTautology(input).Trim()
            }
        }
        return input;
    }
}
protected class CensoringValidator: IValidator
{
    protected CensoringValidator(string config = "full") 
    {
        
    }
}
protected class PhoneNumberValidator : IValidator
{
    protected PhoneNumberValidator(string config = "full") { }
}
protected class EmailAddressValidator : IValidator
{
    protected EmailAddressValidator(string config = "full") { }
}
protected class DateTimeValidator: IValidator
{
    protected DateTimeValidator(string config = "full") { }
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
    public IValidator getValidator = (ValidationMode validationMode, string config) =>
        validationMode switch
        {
            SQL_COMMAND_SANITIZER => new SQLValidator(config),
            CENSOR_FORBIDDEN_WORDS => new CensoringValidator(config),
            VALIDATE_PHONE_NUMBER => new PhoneNumberValidator(config),
            VALIDATE_EMAIL_ADDRESS => new EmailAddressValidator(config),
            VALIDATE_DATE_TIME => new DateTimeValidator(config),
            _ => throw new Exception("Unaccepted validation mode!")
        };
}
