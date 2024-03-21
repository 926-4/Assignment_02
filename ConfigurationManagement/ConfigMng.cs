using System.Text.Json;

namespace ConfigurationManagement
{
    // TODO User is outside of this module
    public class User
    {

    }

    public class Profile
    {
        public string handle { get; set; }
        public string username { get; set; }
        public string bio { get; set; }
        public int age { get; set; }
        public string mail { get; set; }
        public string phone_number { get; set; }
    }

    public class Privacy
    {
        public string profile_type { get; set; }
        public bool allow_tags { get; set; }
        public bool allow_message { get; set; }
    }

    public class Notifications
    {
        public bool push { get; set; }
        public bool mail { get; set; }
    }

    public class UserSettings
    {
        public Profile profile { get; set; }
        public Privacy privacy { get; set; }
        public Notifications notifications { get; set; }

    }

    public class Appearance
    {
        public string mode { get; set; }
        public string theme { get; set; }
    }

    public class AdvancedSettings
    {
        public bool two_factor_authentification { get; set; }
        public List<User> blocked_users { get; set; }
    }

    public class Configuration
    {
        public UserSettings user_settings { get; set; }
        public Appearance appearance { get; set; }
        public string language { get; set; }
    }

    public class ConfigMngModule
    {
        private string ConfigurationFileName;
        private string JsonString;

        public void ConfigMethod()
        {
            ReadConfigurationFileName();
            DeserializeJSON();
            Console.WriteLine("CONFIG WORKING");
        }

        public void ReadConfigurationFileName()
        {
            // constant or?
            this.ConfigurationFileName = "./config.json";
            // TODO validate
        }

        // deserialize JSON
        public void DeserializeJSON()
        {
            this.JsonString = File.ReadAllText(this.ConfigurationFileName);

            Configuration configuration = JsonSerializer.Deserialize<Configuration>(this.JsonString);
        }

        // serialize JSON
        public void SerializeJSON()
        {

        }
    }
}
