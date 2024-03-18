using Logging;
using DataValidation;

internal class Program
{
    private static void Main(string[] args)
    {
        LoggingModule a = new LoggingModule();
        Validator b = new Validator();
        a.LoggingMethod();
        b.Method();
        Console.WriteLine("Press any key to finish...");
        Console.ReadLine();
    }
}