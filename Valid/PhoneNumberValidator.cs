namespace Valid
{

    public class PhoneNumberValidator : IValidator
    {
        internal PhoneNumberValidator(string config = "full") { }
        public string Apply(string input)
        {
            return input;
        }
    }
}