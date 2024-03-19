using System.Text.RegularExpressions;
namespace Valid
{
	public class CensoringValidator : IValidator
	{
		private List<string> keywords;
		internal CensoringValidator(string config = "full")
		{
			keywords = new List<string>(config.Split(','));
		}
		public string Apply(string input)
		{
			foreach(string word in keywords)
			{
				input = input.Replace(word, "***");
				///Console.WriteLine(input);
			}
			return input;
		}
	}
}