using System.Text.RegularExpressions;
namespace Valid
{ 
    class DateValidator : IValidator
    {
        private List<string> format;
        private Regex? pattern; 
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
                config.Replace(".", "-").Replace("/", "-");
            }
            format = config.Split('-').ToList();
        }
        public string Apply(string input)
        {
            int inputIndex = 0;
            return input;
        }
    }
}