using Logging;
using Valid;
using Encryption;
using Authentification;
using ConfigurationManagement;
using System.Configuration;
internal static class Program
{
    static private string validatorExitMessage="";
    static private bool testValidator()
    {
        ValidatorFactory vf = new ValidatorFactory();
        IValidator sql = vf.getValidator(Valid.ValidationMode.SQL_COMMAND_SANITIZER);
        IValidator censor = vf.getValidator(Valid.ValidationMode.CENSOR_FORBIDDEN_WORDS);
        string input, output;
        input = "SELECT * FROM STUDENTS WHERE NAME = \"Mark\"; DROP TABLE STUDENTS \"\"";
        output = sql.Apply(input);
        if (output != "SELECT * FROM STUDENTS WHERE NAME = \"Mark\"")
        {
            validatorExitMessage = "SQL Validation failed at noticing SQL bathced statement injection";
        }
        input = "Eu sunt Ciprian";
        output = censor.Apply(input);
        if (output != "Eu sunt *******")
        {
            Console.WriteLine(output);
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "Eu sunt ciprian";
        output = censor.Apply(input);
        if (output != "Eu sunt *******")
        {
            Console.WriteLine(output);
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "Eu sunt CIPRIAN";
        output = censor.Apply(input);
        if (output != "Eu sunt *******")
        {
            Console.WriteLine(output);
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "Eu sunt CIPRIAN ciprian";
        output = censor.Apply(input);
        if (output != "Eu sunt ******* *******")
        {
            Console.WriteLine(output);
            validatorExitMessage = "Censoring failed";
            return false;
        }
        validatorExitMessage = "Validator test successfully passed";
        return true;
    }


    static private string loggerExitMessage="";
    static private bool testLogger()
    {
        LoggingModule a = new LoggingModule();
        a.LoggingMethod();
        loggerExitMessage = "Logger test successfully passed";
        return true;
    }
    static private string encrypterExitMessage="";
    static private bool testEncrypter()
    {
        Cypher cypher = new Cypher("bcdefghijklmnopqrstuvwxyza");
        string input,output;
        input = "An apple farm";
        output=cypher.encrypt(input);
        if(output!="Bo bqqmf gbsn")
        {
            encrypterExitMessage = "Encrypting failed";
            return false;
        }
        input = cypher.decrypt(output);
        if(input!="An apple farm")
        {
            encrypterExitMessage = "Decrypting failed";
            return false;
        }
        try
        {
            cypher = new Cypher("bcdefghijklmnopqrstuvwxyz1");
            encrypterExitMessage = "Initialization failed";
            return false;
        }
        catch (Exception)
        {

        }
        try
        {
            cypher = new Cypher("bcdefghijklmnopqrstuvwxyz");
            encrypterExitMessage = "Initialization failed";
            return false;
        }
        catch (Exception)
        {

        }
        encrypterExitMessage = "Encrypter test successfully passed";
        return true;
    }
    static private string authenticaterExitMessage="";
    static private bool testAuthenticator()
    {
        AuthentificationModule b = new AuthentificationModule();
        b.AuthMethod();
        authenticaterExitMessage = "Authenticater test successfully passed";
        return true;
    }
    static private string configuratorExitMessage="";
    static private bool testConfigurator()
    {
        ConfigMngModule c = new ConfigMngModule();
        c.ConfigMethod();
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
        testEncrypter();
        Console.WriteLine(encrypterExitMessage);
    }
}