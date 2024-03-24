using ConfigurationManagement;
using System.ComponentModel.Design;

namespace ConfigurationManagement
{
    public class User
    {
        // TODO external import
    }

    public class ConfigMngModule
    {
        private string ConfigurationFileName;
        private string JsonString;

        private string handle;
        private string username;
        private string bio;
        private int age;
        private string mail;
        private string phone_number;
        private bool two_factor_authentification;
        private string profile_type;
        private bool allow_tags;
        private bool allow_messages;
        private bool push_notifications;
        private bool mail_notifications;
        private string app_mode;
        private string app_theme;
        private string language;
        private List<User> blocked_users;

        public ConfigMngModule(string ConfigurationFileName = "config.json", string JsonString = "")
        {
            this.ConfigurationFileName = ConfigurationFileName;
            this.JsonString = JsonString;
        }
        
        public void ConfigMethod()
        {
            DeserializeJSON();
            SerializeJSON();

            Console.WriteLine("handle: ", this.handle);
            Console.WriteLine("age: ", this.age);
            Console.WriteLine("push_notifications: ", this.push_notifications);
        }

        // deserialize JSON
        public void DeserializeJSON()
        {
            this.JsonString = File.ReadAllText(this.ConfigurationFileName);

            this.JsonString = this.JsonString.Substring(1, this.JsonString.Length - 2).Trim();

            Console.WriteLine(this.JsonString);

            TraverseJsonString();
        }

        public void TraverseJsonString()
        {
            // "handle": "neon1024_",
            // "age": 20,
            // "push_notifications": true

            // steps
            // get the key string
            // get the value string
            // key = value
            // repeat
            while (this.JsonString.Length > 0)
            {
                string key = GetNextKeyString();

                Console.WriteLine(this.JsonString);

                string value = GetNextValueString();

                Console.WriteLine(this.JsonString);

                Console.WriteLine(key);
                Console.WriteLine(value);

                AssignValueToKey(key, value);
            }
        }

        public string GetNextKeyString()
        {
            // "key": "value"
            int index = this.JsonString.IndexOf(':');
            string keyString = this.JsonString.Substring(0, index).Trim('\"');

            this.JsonString = this.JsonString.Substring(index + 1);
            this.JsonString.TrimStart();

            return keyString;
        }

        // TODO \" in string
        public string GetStringValue()
        {
            // "value",
            this.JsonString.TrimStart('\"');

            // value",
            int index = this.JsonString.IndexOf('"');

            string valueString = this.JsonString.Substring(0, index);

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart(',', ' ');

            return valueString;
        }

        public string GetIntValue()
        {
            int index = this.JsonString.IndexOf(',');

            string intString = this.JsonString.Substring(0, index);

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart();

            return intString;
        }

        public string GetBoolValue()
        {
            int index = this.JsonString.IndexOf(',');

            string boolString = this.JsonString.Substring(0, index);

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart();

            return boolString;
        }

        public string GetNextValueString()
        {
            char startingCharacter = this.JsonString[0];

            if (startingCharacter == '\"')
            {
                return GetStringValue();
            }

            if ((startingCharacter >= 0) && (startingCharacter <= 9))
            {
                return GetIntValue();
            }

            if ((startingCharacter == 't') || (startingCharacter == 'T') || (startingCharacter == 'f') || (startingCharacter == 'F'))
            {
                return GetBoolValue();
            }

            return "";
        }

        public void AssignValueToKey(string key, string value)
        {
            switch(key)
            {
                case "handle":
                    this.handle = value;
                    break;

                case "username":
                    this.username = value;
                    break;

                case "bio":
                    this.bio = value;
                    break;

                case "age":
                    this.age = Int32.Parse(value);
                    break;

                case "mail":
                    this.mail = value;
                    break;

                case "phone_number":
                    this.phone_number = value;
                    break;

                case "two_factor_authentification":
                    this.two_factor_authentification = Boolean.Parse(value);
                    break;

                case "profile_type":
                    this.profile_type = value;
                    break;

                case "allow_tags":
                    this.allow_tags = Boolean.Parse(value);
                    break;

                case "allow_messages":
                    this.allow_messages = Boolean.Parse(value);
                    break;

                case "push_notifications":
                    this.push_notifications = Boolean.Parse(value);
                    break;

                case "mail_notifications":
                    this.mail_notifications = Boolean.Parse(value);
                    break;

                case "app_theme":
                    this.app_theme = value;
                    break;

                case "app_mode":
                    this.app_mode = value;
                    break;

                case "language":
                    this.language = value;
                    break;

                case "blocked_users":
                    break;

                default:
                    break;
            }
        }

        // serialize JSON
        public void SerializeJSON()
        {

        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        ConfigMngModule c = new ConfigMngModule();
        c.ConfigMethod();
    }
}
