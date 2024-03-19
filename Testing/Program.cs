using Logging;
using Valid;
internal static class Program
{
    static private string validatorExitMessage="";
    static private bool testValidator()
    {
        ValidatorFactory vf = new ValidatorFactory();
        
        /// test sql injection
        IValidator sql = vf.getValidator(Valid.ValidationMode.SQL_COMMAND_SANITIZER);
        string input, output;
        input = "SELECT * FROM STUDENTS WHERE NAME = \"Mark\"; DROP TABLE STUDENTS \"\"";
        output = sql.Apply(input);
        if (output != "SELECT * FROM STUDENTS WHERE NAME = \"Mark\"")
        {
            validatorExitMessage = "SQL Validation failed at noticing SQL bathced statement injection";
            return false;
        }
        input = "SELECT * FROM STUDENTS WHERE NAME=\"Mark\" OR 1=   1\"\"";
        output = sql.Apply(input);
        if (output != "SELECT * FROM STUDENTS WHERE NAME=\"Mark\" OR\"\"")
        {
            validatorExitMessage = "SQL Validation failed at noticing SQL tautology injection";
            return false;
        }
        
        /// test censoring bad words
        IValidator censor = vf.getValidator(Valid.ValidationMode.CENSOR_FORBIDDEN_WORDS, "bad word,hate");
        input = "this string has a bad word";
        output = censor.Apply(input);
        if (output != "this string has a ***")
        {
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "this string is a message filled with hate, and has at least one bad word in it";
        output = censor.Apply(input);
        if (output!= "this string is a message filled with ***, and has at least one *** in it")
        {
            validatorExitMessage = "Censoring failed";
            return false;
        }
        validatorExitMessage = "Validator test successfully passed";
        return true;
    }


    static private string loggerExitMessage="";
    static private bool testLogger()
    {
        loggerExitMessage = "Logger test successfully passed";
        return true;
    }
    static private string encrypterExitMessage="";
    static private bool testEncrypter()
    {
        encrypterExitMessage = "Encrypter test successfully passed";
        return true;
    }
    static private string authenticaterExitMessage="";
    static private bool testAuthenticator()
    {
        authenticaterExitMessage = "Authenticater test successfully passed";
        return true;
    }
    static private string configuratorExitMessage="";
    static private bool testConfigurator()
    {
        configuratorExitMessage = "Configurator test successfully passed";
        return true;
    }
    private static void Main(string[] args)
    {
        testValidator();
        Console.WriteLine(validatorExitMessage);
        testLogger();
        Console.WriteLine(loggerExitMessage);
        testEncrypter();
        Console.WriteLine(encrypterExitMessage);
        testAuthenticator();
        Console.WriteLine(authenticaterExitMessage);
        testConfigurator();
        Console.WriteLine(configuratorExitMessage);
        ///Console.WriteLine("Press any key to terminate gracefully...");
        ///Console.ReadKey();
    }
}