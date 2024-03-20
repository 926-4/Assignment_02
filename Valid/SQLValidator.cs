using System.Text.RegularExpressions;
namespace Valid
{
    public class SQLValidator : IValidator
    {
        private static readonly Regex tautologyRegex = new("\\b(.*)\\s*=\\s*\\1\\b", RegexOptions.None);
        private static readonly List<string> allOptions = new List<string>() { "batched", "tautology" };
        private string[] configOptions;
        internal SQLValidator(string config = "full")
        {
            if (config == "full") config = "batched,tautology";
            configOptions = config.Split(',');
            foreach (string option in configOptions)
            {
                if (!allOptions.Any(str => str == option))
                {
                    throw new Exception("SQL Validator config usage -- choice of 'batched', 'tautology', join multiple by ','");
                }
            }
        }
        private string removeBatched(string input)
        {
            if (input.Contains(";"))
            {
                input = input.Split(";")[0];
            }
            return input;
        }
        private string removeTautology(string input)
        {
            Match checkForTautology = tautologyRegex.Match(input);
            if (checkForTautology.Success)
            {
                input = input.Replace(checkForTautology.Value, "");
            }
            return input;
        }
        public string Apply(string input)
        {
            foreach (string option in configOptions)
            {
                if (option == "batched")
                {
                    input = removeBatched(input);
                    continue;
                }
                if (option == "tautology")
                {
                    input = removeTautology(input);
                    continue;
                }
                throw new Exception("Invalid option for sql ");
            }
            return input;
        }
    }
}