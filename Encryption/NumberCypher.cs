namespace Encryption
{
    public class NumberCypher : ICypher
    {
        private string key;
        public NumberCypher(string _key)
        {
            if (_key != null && _key.Length != 10)
            {
                throw new ArgumentException($"Invalid key length: {_key.Length} - Expected value: 10");
            }
            if (_key == null)
            {
                throw new ArgumentException($"Key is null");
            }
            bool[] visited = new bool[10];
            Array.Fill(visited, false);
            for (int i = 0; i < _key.Length; i++)
            {
                if (!(_key[i] >= '0' && _key[i] <= '9'))
                {
                    throw new ArgumentException($"Invalid key - key should onsist of only digits");
                }
                else
                {
                    visited[_key[i] - '0'] = true;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (!visited[i])
                {
                    throw new ArgumentException($"Invalid key - every digit of the alphbet should be mapped to a different digit");
                }
            }
            key = _key;
        }
        public string Key
        {
            get { return key; }
            set
            {
                if (value != null && value.Length != 10)
                {
                    throw new ArgumentException($"Invalid key length: {value.Length} - Expected value: 10");
                }
                if (value == null)
                {
                    throw new ArgumentException($"Key is null");
                }
                bool[] visited = new bool[10];
                Array.Fill(visited, false);
                for (int i = 0; i < key.Length; i++)
                {
                    if (!(key[i] >= '0' && key[i] <= '9'))
                    {
                        throw new ArgumentException($"Invalid key - key should onsist of only digits");
                    }
                    else
                    {
                        visited[key[i] - '0'] = true;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!visited[i])
                    {
                        throw new ArgumentException($"Invalid key - every digit of the alphbet should be mapped to a different digit");
                    }
                }
                key = value;
            }
        }
        public string encrypt(string plain)
        {
            char[] chars = new char[plain.Length];
            for (int i = 0; i < plain.Length; i++)
            {
                if (plain[i] >= '0' && plain[i] <= '9')
                {
                    int j = plain[i] - '0';
                    chars[i] = key[j];
                }
                else
                {
                    chars[i] = plain[i];
                }
            }
            return new string(chars);
        }
        public string decrypt(string coded)
        {
            char[] chars = new char[coded.Length];
            for (int i = 0; i < coded.Length; i++)
            {
                if (coded[i] >= '0' && coded[i] <= '9')
                {
                    int j = key.IndexOf(coded[i]);
                    chars[i] = (char)(j + '0');
                }
                else
                {
                    chars[i] = coded[i];
                }
            }
            return new string(chars);
        }
    }
}

