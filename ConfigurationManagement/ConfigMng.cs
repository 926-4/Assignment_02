using System.Net.Mail;
using System.Net.NetworkInformation;
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
    
        public Profile(string handle="@user", string username="username", string bio="none", int age=0, string mail="user@mail", string phone="0000000000") { }
    }

    public class Privacy
    {
        public Privacy(string profile_type = "public", bool allow_tags = true, bool allow_message = true) { }
        
        public string profile_type { get; set; }
        public bool allow_tags { get; set; }
        public bool allow_message { get; set; }

    }

    public class Notifications
    {
        public bool push { get; set; }
        public bool mail { get; set; }

        public Notifications(bool push=true, bool mail=true) { }
    }

    public class UserSettings
    {
        public Profile profile { get; set; }
        public Privacy privacy { get; set; }
        public Notifications notifications { get; set; }

        public UserSettings(Profile profile, Privacy privacy, Notifications notifications) { }
    }

    public class Appearance
    {
        public string mode { get; set; }
        public string theme { get; set; }

        public Appearance(string mode="system", string theme="default") { }
    }

    public class AdvancedSettings
    {
        public bool two_factor_authentification { get; set; }
        public List<User> blocked_users { get; set; }

        public AdvancedSettings(bool two_factor_authentification = false, List<User> blocked_users = []) { }
    }

    public class Configuration
    {
        public UserSettings user_settings { get; set; }
        public Appearance appearance { get; set; }
        public string language { get; set; }

        public Configuration(UserSettings user_settings, Appearance appearance, string languag = "en-US") { }
    }

    public class ConfigMngModule
    {
        private string ConfigurationFileName;
        private string JsonString;

        public ConfigMngModule(string ConfigurationFileName = "config.json", string JsonString = "") { }

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
