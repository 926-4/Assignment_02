namespace Valid
{ 
    class DateValidator : IValidator
    {
        private List<string> format;
        private Regex pattern; 
        private bool checkAllowedFormats(string toCheck)
        {
            return (toCheck == "dd-mm-yyyy") || (toCheck == "mm-dd-yyyy") || (toCheck == "yyyy-mm-dd") || (toCheck == "dd-mm-yy") || (toCheck == "mm-dd-yy");
        }
        internal DateValidator(string config = "default") 
        { 
            if(config == "default") 
            {
                config = "dd-mm-yyyy";
            }
            else
            {
                /// implement some checks
                config.Replace(".", "-").replace("/", "-");
            }
            format = new List<string>() { config.Split("-") };
        }
        public string Apply(string input)
        {
            int inputIndex = 0;

        }
    }
}