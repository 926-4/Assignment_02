namespace Valid
{ 
    class DateValidator : IValidator
    {
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
                if (!checkAllowedFormats(config))
                {
                    throw new Exception($"{config} is not among the allowed formats. " +
                        $"\tdd-mm-yyyy\n" +
                        $"\tmm-dd-yyyy\n" +
                        $"\tyyyy-mm-dd\n" +
                        $"\tdd-mm-yy\n" +
                        $"\tmm-dd-yy");
                }
            }
        }
        public string Apply(string input)
        {
            return input;
        }
    }
}