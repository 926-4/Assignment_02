using System.Text.RegularExpressions;
namespace Valid
{
    public class SQLValidator : IValidator
    {
        private static readonly Regex tautologyRegex = new("\\b(.*)\\s*=\\s*\\1\\b", RegexOptions.None);
        private static readonly List<string> allOptions = new List<string>() { "batched", "tautology" };
        private List<string> configOptions;
        internal SQLValidator(string config = "default")
        {
            if (config == "default")
            {
                configOptions = new List<string>() { "batched", "tautology" };
                return; 
            }
            else configOptions = new List<string>(config.Split(','));
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
            }
            return input;
        }
    }
}