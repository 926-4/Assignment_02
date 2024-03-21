namespace Valid
{
    class EmailAddressValidator : IValidator
    {
        internal EmailAddressValidator(string config = "default") 
        {
            if(config == "default")
            {
                config = "[a-z_][a-z_0-9.]*@[a-z_0-9]+\\.[a-z]{2,}"; 
            }
        }
        public string Apply(string input)
        {
            return input;
        }
    }
}