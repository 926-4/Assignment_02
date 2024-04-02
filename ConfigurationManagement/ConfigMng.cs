using ConfigurationManagement;
using System.ComponentModel.Design;

namespace ConfigurationManagement
{
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
        private string[] blocked_users;

        public ConfigMngModule(string ConfigurationFileName = "config.json", string JsonString = "")
        {
            this.ConfigurationFileName = ConfigurationFileName;
            this.JsonString = JsonString;
        }
        
        public void ConfigMethod()
        {
            DeserializeJSON();
            SerializeJSON();
        }

        public void DeserializeJSON()
        {
            this.JsonString = File.ReadAllText(this.ConfigurationFileName);

            this.JsonString = this.JsonString.Substring(1, this.JsonString.Length - 2).Trim();

            TraverseJsonString();
        }

        public void TraverseJsonString()
        {
            while (this.JsonString.Length > 0)
            {
                string key = GetNextKeyString();

                string value = GetNextValueString();

                AssignValueToKey(key, value);
            }
        }

        public string GetNextKeyString()
        {
            this.JsonString = this.JsonString.TrimStart();
            int index = this.JsonString.IndexOf(':');
            string keyString = this.JsonString.Substring(0, index).TrimStart().Trim('\"');

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart();

            return keyString;
        }

        // TODO \" in string
        public string GetStringValue()
        {
            this.JsonString = this.JsonString.TrimStart('\"');

            int index = this.JsonString.IndexOf('"');

            string valueString = this.JsonString.Substring(0, index);

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart().TrimStart(',', ' ');

            return valueString;
        }

        public string GetIntValue()
        {
            int index = this.JsonString.IndexOf(',');

            string intString;

            if (index == -1)
            {
                intString = this.JsonString;

                this.JsonString = "";

                return intString;
            }

            intString = this.JsonString.Substring(0, index);

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart();

            return intString;
        }

        public string GetBoolValue()
        {
            int index = this.JsonString.IndexOf(',');

            string boolString;

            if (index == -1)
            {
                boolString = this.JsonString;

                this.JsonString = "";

                return boolString;
            }

            boolString = this.JsonString.Substring(0, index);

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart();

            return boolString;
        }

        public string GetArrayValue()
        {
            this.JsonString = this.JsonString.Substring(1);

            int index = this.JsonString.IndexOf(']');

            string arrayValue = this.JsonString.Substring(0, index).Trim();

            this.JsonString = this.JsonString.Substring(index + 1).TrimStart().TrimStart(',', ' ');

            if(this.JsonString.IndexOf(',') == -1)
            {
                this.JsonString = "";
            }

            return arrayValue;
        }

        public string GetNextValueString()
        {
            char startingCharacter = this.JsonString[0];

            if (startingCharacter == '\"')
            {
                return GetStringValue();
            }

            if ((startingCharacter >= '0') && (startingCharacter <= '9'))
            {
                return GetIntValue();
            }

            if ((startingCharacter == 't') || (startingCharacter == 'T') || (startingCharacter == 'f') || (startingCharacter == 'F'))
            {
                return GetBoolValue();
            }

            if(startingCharacter == '[')
            {
                return GetArrayValue();
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
                    this.blocked_users = value.Split(',');
                    break;

                default:
                    break;
            }
        }

        public void SerializeJSON()
        {
            this.JsonString = "";
            this.JsonString += "{";
            this.JsonString += "\n\t";
            
            this.JsonString += "\"handle\":" + $"\"{this.handle}\",";
            this.JsonString += "\n\t";

            this.JsonString += "\"username\":" + $"\"{this.username}\",";
            this.JsonString += "\n\t";

            this.JsonString += "\"bio\":" + $"\"{this.bio}\",";       
            this.JsonString += "\n\t";

            this.JsonString += "\"age\":" + $"{this.age},";
            this.JsonString += "\n\t";

            this.JsonString += "\"mail\":" + $"\"{this.mail}\",";       
            this.JsonString += "\n\t";

            this.JsonString += "\"phone.number\":" + $"\"{this.phone_number}\",";
            this.JsonString += "\n\t";

            this.JsonString += "\"two_factor_authentification\":" + $"{this.two_factor_authentification},";
            this.JsonString += "\n\t";

            this.JsonString += "\"profile_type\":" + $"\"{this.profile_type}\",";
            this.JsonString += "\n\t";

            this.JsonString += "\"allow_tags:\":" + $"{this.allow_tags},";
            this.JsonString += "\n\t";

            this.JsonString += "\"allow_messages\":" + $"{this.allow_messages},";
            this.JsonString += "\n\t";

            this.JsonString += "\"push_notifications\":" + $"{this.push_notifications},";
            this.JsonString += "\n\t";

            this.JsonString += "\"mail_notifications\":" + $"{this.mail_notifications},";
            this.JsonString += "\n\t";

            this.JsonString += "\"app_mode\":" + $"\"{this.app_mode}\",";
            this.JsonString += "\n\t";

            this.JsonString += "\"app_theme\":" + $"\"{app_theme}\",";
            this.JsonString += "\n\t";

            this.JsonString += "\"language\":" + $"\"{this.language}\",";
            this.JsonString += "\n\t";

            // TODO
            /*
            this.JsonString += "\"blocked_users\":" + $"[]";
            this.JsonString += "\n";
            */

            this.JsonString += "}";

            File.WriteAllText("JSON.json", this.JsonString);
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
