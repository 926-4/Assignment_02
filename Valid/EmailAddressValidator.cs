using System.Text.RegularExpressions;
namespace Valid
{
    class EmailAddressValidator : IValidator
    {
        private Regex pattern;
        private string EscapeSpecificChars(string value)
        {
            return value.Replace(".", "\\.");
        }
        internal EmailAddressValidator(string config = "default") 
        {
            if(config == "default")
            {
                config = "[A-Za-z0-9!\\-\\._]+@[A-Za-z0-9-]+\\.[a-z]{2,}"; 
            }
            else
            {
                config = config.Replace("?", "[a-z_0-9.]");
                config = EscapeSpecificChars(config);
            }
            pattern = new Regex(config, RegexOptions.None);
        }
        public string Apply(string input)
        {
            return (pattern.Matches(input).Count>0) ? input : "";
        }
    }


}