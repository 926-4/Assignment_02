using Logging;
using Valid;
using Encryption;
using Authentification;
using ConfigurationManagement;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
internal static class Program
{
    static private string validatorExitMessage="";
    static private bool testValidator()
    {
        ValidatorFactory vf = new ValidatorFactory();
        IValidator sql = vf.getValidator(Valid.ValidationMode.SQL_COMMAND_SANITIZER);
        IValidator censor = vf.getValidator(Valid.ValidationMode.CENSOR_FORBIDDEN_WORDS);
        IValidator romanianTelephoneNrChecker = vf.getValidator(Valid.ValidationMode.VALIDATE_PHONE_NUMBER, "+40nnnnnnnnn");
        IValidator phone = vf.getValidator(Valid.ValidationMode.VALIDATE_PHONE_NUMBER);
        IValidator email = vf.getValidator(ValidationMode.VALIDATE_EMAIL_ADDRESS, "???.???@gmail.??");
        IValidator defaultEmail = vf.getValidator(ValidationMode.VALIDATE_EMAIL_ADDRESS);
        IValidator dateValidator = vf.getValidator(ValidationMode.VALIDATE_DATE);
        string input, output;
        input = "SELECT * FROM STUDENTS WHERE NAME = \"Mark\"; DROP TABLE STUDENTS \"\"";
        output = sql.Apply(input);
        if (output != "SELECT * FROM STUDENTS WHERE NAME = \"Mark\"")
        {
            validatorExitMessage = "SQL Validation failed at noticing SQL bathced statement injection";
            return false;
        }
        input = "SELECT * FROM Students WHERE NAME = \"THis is a benign string\"";
        output = censor.Apply(input);
        if(output != input)
        {
            validatorExitMessage = "SQL Validation overcorrects";
            return false;
        }
        input = "Eu sunt Ciprian";
        output = censor.Apply(input);
        if (output != "Eu sunt *******")
        {
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "Eu sunt ciprian";
        output = censor.Apply(input);
        if (output != "Eu sunt *******")
        {
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "Eu sunt CIPRIAN";
        output = censor.Apply(input);
        if (output != "Eu sunt *******")
        {
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "Eu sunt CIPRIAN ciprian";
        output = censor.Apply(input);
        if (output != "Eu sunt ******* *******")
        {
            validatorExitMessage = "Censoring failed";
            return false;
        }
        input = "+40723456789";
        output = romanianTelephoneNrChecker.Apply(input);
        if(output != input)
        {
            validatorExitMessage = "Checking phone nr failed";
            return false;
        }
        input = "+12345678901";
        output = phone.Apply(input);
        if(output != input)
        {
            validatorExitMessage = "Checking phone nr failed";
            return false;
        }
        input = "abc.def@gmail.ro";
        output = email.Apply(input);
        if (output != input)
        {
            validatorExitMessage = "Checking email failed";
            return false;
        }
        input = "fed.cba@gmail.hu";
        output = email.Apply(input);
        if (output != input)
        {
            validatorExitMessage = "Checking email failed";
            return false;
        }
        input = "_._._._.@gogoga.ga";
        output = defaultEmail.Apply(input);
        if (output != input)
        {
            validatorExitMessage = "Checking email failed";
            return false;
        }
        input = "fed.cba@gmail.hu";
        output = defaultEmail.Apply(input);
        if (output != input)
        {
            validatorExitMessage = "Checking email failed";
            return false;
        }
        input = "this is not a valid email address";
        output = defaultEmail.Apply(input);
        if(output != "")
        {
            validatorExitMessage = "Checking email failed";
            return false;
        }
        input = "21.03.2024";
        output = dateValidator.Apply(input);
        if(output != input)
        {
            validatorExitMessage = "Failed date validation";
            return false;
        }
        input = "2.3.2024";
        output = dateValidator.Apply(input);
        if (output != input)
        {
            validatorExitMessage = "Failed date validation";
            return false;
        }
        input = "2.13.2024";
        output = dateValidator.Apply(input);
        if (output != "")
        {
            validatorExitMessage = "Failed date validation";
            return false;
        }
        validatorExitMessage = "Validator test successfully passed";
        return true;
    }
    static private string loggerExitMessage="";
    static private bool testLogger()
    {
        LoggingModule a = new LoggingModule();
        try
        {
            LoggingModule.LogInfo("Test info message");

            string[] lines = System.IO.File.ReadAllLines("log.txt");
            if (lines.Length != 0)
            {
                loggerExitMessage = "Logging of info message failed: Message was immediately written to the file";
                return false;
            }

            LoggingModule.LogWarning("Test warning message");

            lines = System.IO.File.ReadAllLines("log.txt");
            if (!lines[1].Contains("[Warning]") || !lines[1].Contains("Test warning message"))
            {
                loggerExitMessage = "Logging of warning message failed";
                return false;
            }

            LoggingModule.LogError("Test error message");

            lines = System.IO.File.ReadAllLines("log.txt");
            if (!lines[2].Contains("[Error]") || !lines[2].Contains("Test error message"))
            {
                loggerExitMessage = "Logging of error message failed";
                return false;
            }

            LoggingModule.LogInfo("Test info message 1");
            LoggingModule.LogInfo("Test info message 2");

            System.Threading.Thread.Sleep(500);

            lines = System.IO.File.ReadAllLines("log.txt");
            if (lines.Length != 5 || !lines[3].Contains("[Info]") || !lines[3].Contains("Test info message 1") || !lines[4].Contains("[Info]") || !lines[4].Contains("Test info message 2"))
            {
                loggerExitMessage = "Flushing buffered info messages failed";
                return false;
            }

            loggerExitMessage = "Logger test successfully passed";
            return true;
        }
        catch (Exception ex)
        {
            loggerExitMessage = "An error occurred during logger test: " + ex.Message;
            return false;
        }
    }
    static private string encrypterExitMessage = "";
    static private bool testEncrypter()
    {
        Cypher cypher = new Cypher("bcdefghijklmnopqrstuvwxyza");
        string input, output;
        input = "An apple farm";
        output = cypher.encrypt(input);
        if (output != "Bo bqqmf gbsn")
        {
            encrypterExitMessage = "Encrypting failed";
            return false;
        }
        input = cypher.decrypt(output);
        if (input != "An apple farm")
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
        NumberCypher numberCypher = new NumberCypher("1234567890");
        input = "0aa";
        output = numberCypher.encrypt(input);
        if (output != "1aa")
        {
            encrypterExitMessage = "Encryption failed";
            return false;
        }
        input = numberCypher.decrypt(output);
        if (input != "0aa")
        {
            encrypterExitMessage = "Decryption failed";
            return false;
        }
        try
        {
            numberCypher = new("123456789");
            encrypterExitMessage = "Initialization failed";
            return false;
        }
        catch (Exception)
        {

        }
        try
        {
            cypher = new Cypher("123456789a");
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
        Dictionary<string, string> credentials = new Dictionary<string, string>();
        TimeSpan timeout = TimeSpan.FromSeconds(10);
        credentials.Add("user1", "password1");
        AuthentificationModule b = new AuthentificationModule(credentials, timeout);
        string u1 = "user1", u2 = "user2", p1 = "password1", p2 = "password2";
        try
        {
            b.AuthMethod(u1, p2);
            authenticaterExitMessage = "Authenticater test failed";
            return false;
        }
        catch (Exception)
        {

        }
        try
        {
            b.AuthMethod("a", "5");
            authenticaterExitMessage = "Authenticater test failed";
            return false;
        }
        catch (Exception)
        {

        }
        if (b.isUserLoggedIn(u2))
        {
            authenticaterExitMessage = "Authenticater test failed";
            return false;
        }
        b.AuthMethod(u1, p1);
        try
        {
            b.AuthMethod(u1, p1);
            authenticaterExitMessage = "Authenticater test failed";
            return false;
        }
        catch (Exception)
        {

        }
        Thread.Sleep(11000);
        if (!b.isUserLoggedIn(u1))
        {
            authenticaterExitMessage = "Authenticater test failed";
            return false;
        }
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
        //testValidator();
        //Console.WriteLine(validatorExitMessage);
        testLogger();
        Console.WriteLine(loggerExitMessage);
        Console.ReadLine();
        //testEncrypter();
        //Console.WriteLine(encrypterExitMessage);
        //testAuthenticator();
        //Console.WriteLine(authenticaterExitMessage);
        //testConfigurator();
        //Console.WriteLine(configuratorExitMessage);
        //testEncrypter();
        //Console.WriteLine(encrypterExitMessage);
    }
}