namespace Valid
{
    class PhoneNumberValidator : IValidator
    {
        Regex pattern;
        internal PhoneNumberValidator(string config = "default") 
        {
            if (config == "default")
            {
                config = "(\\+[0-9]{1,3})?[0-9]{9}";
            }
            else
            {
                foreach (char c in config)
                {
                    if ((c < '0' || c > '9') && (c != 'n') && (c != '+') && (c != '-') && (c != ' '))
                    {
                        throw new Exception("Unaccepted character in phone number pattern -- The pattern should only contain +,- and whitespaces, along with n representing any digit");
                    }
                }
                while (config.Contains('n'))
                {
                    config = config.Replace('n', "[0-9]");
                }
                Console.WriteLine(config);
            }
            pattern = new(Regex.Escape(config), RegexOptions.None);
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