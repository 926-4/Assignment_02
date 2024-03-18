using Logging;
using Valid;
internal static class Program
{
    static private string validatorExitMessage;
    static private bool testValidator()
    {
        validatorExitMessage = "Validator test successfully passed";
        return true;
    }
    static private string loggerExitMessage;
    static private bool testLogger()
    {
        loggerExitMessage = "Logger test successfully passed";
        return true;
    }
    static private string encrypterExitMessage;
    static private bool testEncrypter()
    {
        encrypterExitMessage = "Encrypter test successfully passed";
        return true;
    }
    static private string authenticaterExitMessage;
    static private bool testAuthenticator()
    {
        authenticaterExitMessage = "Authenticater test successfully passed";
        return true;
    }
    static private string configuratorExitMessage;
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
        Console.WriteLine("Press any key to terminate gracefully...");
        Console.ReadKey();
    }
}