using System.Text.RegularExpressions;
namespace Valid
{
	class CensoringValidator : IValidator
	{
		private List<string> forbiddenWords;
		private Dictionary<string, Regex> patterns;
		internal CensoringValidator(string config = "default")
		{
			if (config.Contains('*'))
				throw new Exception("You cannot ban the censoring character, sorry");
			if (config == "default")
			{
				forbiddenWords = new List<string> { "idiot", "stupid", "shit", "fuck", "ass", "ciprian" };
			}
			else 
				forbiddenWords = new List<string>(config.Split(','));
			patterns = new Dictionary<string, Regex>();
			foreach(string word in forbiddenWords) 
			{
				Regex pattern = new("\\b" + Regex.Escape(word) + "\\b", RegexOptions.IgnoreCase);
				patterns.Add(word, pattern);
			}

		}
        public string Apply(string input)
        {
            foreach (var pattern in patterns)
            {
                string replacement = new string('*', pattern.Key.Length);
                input = pattern.Value.Replace(input, replacement);
            }
            return input;
        }
    }
}