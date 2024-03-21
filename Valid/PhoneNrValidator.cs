using System.Text.RegularExpressions;
namespace Valid
{
    class PhoneNumberValidator : IValidator
    {
        Regex pattern;
        private string escapeSpecificChars(string input)
        {
            return input.Replace("+", "\\+").Replace("(", "\\(").Replace(")", "\\)");
        }
        private bool allowedChar(char toCheck)
        {
            return (toCheck == 'n') || (toCheck >= '0' && toCheck <= '9') || (toCheck == '+') || (toCheck == '-') || (toCheck == '(') || (toCheck == ')');
        }
        internal PhoneNumberValidator(string config = "default") 
        {
            if (config == "default")
            {
                config = "(\\+\\d{1,3})?\\d{9}";
                pattern = new(config, RegexOptions.None);
                return;
            }
            foreach (char c in config)
            {
                if (!allowedChar(c))
                {
                    throw new Exception($"Unaccepted character in phone number pattern --{c}-- The pattern should only contain +,-,(,) and whitespaces, along with n representing any digit");
                }
            }
            config = config.Replace("n", "\\d");
            config = escapeSpecificChars(config);
            pattern = new(config, RegexOptions.None);
        }
        public string Apply(string input)
        {
            if (!pattern.IsMatch(input))
            {
                return "";
            }
            return input;
        }
    }
}