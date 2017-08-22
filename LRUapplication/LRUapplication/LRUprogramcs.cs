using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication8
{
    public class LRUprogram
    {
        public int maxLengthOfDictionary;

        public Dictionary<string, object> Dic = new Dictionary<string, object>();

        public Dictionary<string, object> Log = new Dictionary<string, object>();

        public LRUprogram(int length)
        {
            this.maxLengthOfDictionary = length;
        }

        public int CheckDicLength()
        {
            return Dic.Count;
        }

        public void AddRecord(string key, object value)
        {
            if (!Dic.Keys.Contains(key))
            {
                if (CheckDicLength() <= maxLengthOfDictionary)
                {
                    Dic.Add(key, value);
                    Log.Add(key, DateTime.Now);
                }

                if (CheckDicLength() > maxLengthOfDictionary)
                {
                    var oldestValue = Log.Values.Min();

                    var oldestKey = Log.FirstOrDefault(x => x.Value == oldestValue).Key;

                    Dic.Remove(oldestKey);
                    Log.Remove(oldestKey);
                }
            }

            else
            {
                Console.WriteLine("The Key with the name ' " + key + " ' already exist ! ! !");
                Console.WriteLine();
            }
        }

        public string GetValue(string key)
        {
            if (Dic.ContainsKey(key))
            {
                Log.Remove(key);
                Log.Add(key, DateTime.Now);

                return Dic[key].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
