using System.Text.RegularExpressions;
namespace Valid
{ 
    class DateValidator : IValidator
    {
        private static readonly int[] daysPerMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private bool leap(int year)
        {
            return (year % 4 == 0) && (year % 100 != 0 || year % 400 == 0);
        }
        private bool CheckValidDate(int day, int month, int year)
        {
            if (month < 1 || month > 12)
                return false;
            if(month != 2)
            {
                return (0 < day) && (day < daysPerMonth[month - 1]);
            }
            return (0 < day) && (day < daysPerMonth[month - 1] + (leap(year) ? 1 : 0));
        }
        private Regex pattern; 
        internal DateValidator(string config = "default") 
        {
            switch (config)
            {
                case "default":
                case "dmy":
                    {
                        pattern = new("(?<day>\\d{1,2})(-|/|.)+(?<month>\\d{1,2})\\1+(?<year>\\d{4})", RegexOptions.None);
                        break;
                    }
                case "mdy":
                    {
                        pattern = new("(?<month>\\d{1,2})(-|/|.)+(?<day>\\d{1,2})\\1+(?<year>\\d{4})", RegexOptions.None);
                        break;
                    }
                default:
                    throw new Exception("Unaccepted date format. Try 'mdy' or 'dmy'. 'dmy' is default");
            }
        }
        public string Apply(string input)
        {
            Match match = pattern.Match(input);
            if (!match.Success)
            {
                return "";
            }
            int day = int.Parse(match.Groups["day"].Value);
            int  month = int.Parse(match.Groups["month"].Value);
            int year = int.Parse(match.Groups["year"].Value);
            return CheckValidDate(day, month, year) ? input : "";
        }
    }
}