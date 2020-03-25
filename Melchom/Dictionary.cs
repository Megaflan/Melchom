using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchanter
{
    class Dictionary
    {
        static Dictionary<char, char> dicFW2HW, dicHW2FW;        

        public static void LoadDictionary(params string[] source)
        {
            var pairs = source.Where(s => s.Length == 2).ToList();
            dicFW2HW = pairs.ToDictionary(p => p[0], p => p[1]);
            dicHW2FW = pairs.ToDictionary(p => p[1], p => p[0]);
        }

        public string Transform(string str, string dicType)
        {
            Dictionary<char, char> dic;
            switch (dicType)
            {
                case "dicFW2HW":
                    dic = dicFW2HW;
                    return string.Concat(str.Select(c => dic.TryGetValue(c, out var d) ? d : c));
                case "dicHW2FW":
                    dic = dicHW2FW;
                    return string.Concat(str.Select(c => dic.TryGetValue(c, out var d) ? d : c));
                default:
                    dic = dicFW2HW;
                    return string.Concat(str.Select(c => dic.TryGetValue(c, out var d) ? d : c));
            }           
            
        }

    }
}
