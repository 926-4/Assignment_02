using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public class Cypher
    {
        public Cypher(string _key) {
            if (_key != null && _key.Length != 26)
            {
                throw new ArgumentException($"Invalid key length: {_key.Length} - Expected value: 26");
            }
            if (_key == null)
            {
                throw new ArgumentException($"Key is null");
            }
            bool[] visited = new bool[26];
            Array.Fill(visited, false);
            for (int i = 0; i < _key.Length; i++) 
            { 
                if(!(_key[i]>='a' && _key[i] <= 'z'))
                {
                    throw new ArgumentException($"Invalid key - key should onsist of only lowercase letters");
                }
                else
                {
                    visited[_key[i] - 97] = true;
                }
            }
            for(int i=0;i < 26; i++)
            {
                if (!visited[i])
                {
                    throw new ArgumentException($"Invalid key - every letter of the alphbet should be mapped to a different letter");
                }
            }
            key = _key;
        }
        private string key;
        public string Key
        {
            get { return key; }
            set
            {
                if(value != null && value.Length!=26 ) {
                    throw new ArgumentException($"Invalid key length: {value.Length} - Expected value: 26");
                }
                if( value == null )
                {
                    throw new ArgumentException($"Key is null");
                }
                key = value;
            }
        }
        public string encrypt(string plain)
        {
            char[] chars = new char[plain.Length];
            for( int i = 0; i < plain.Length; i++ )
            {
                if (plain[i]>='a' &&plain[i] <= 'z')
                {
                    int j = plain[i] - 97;
                    chars[i] = key[j];
                }else if (plain[i] >= 'A' && plain[i] <= 'Z')
                {
                    int j = plain[i] - 65;
                    chars[i] = Char.ToUpper(key[j]);
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
            for( int i = 0;i < coded.Length;i++)
            {
                if (coded[i] >= 'a' && coded[i] <= 'z')
                {
                    int j = key.IndexOf(coded[i]);
                    chars[i] = (char)(j+97);
                }
                else if (coded[i] >= 'A' && coded[i] <= 'Z')
                {
                    int j = key.IndexOf(Char.ToLower(coded[i]));
                    chars[i] = Char.ToUpper((char)(j+97));
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
