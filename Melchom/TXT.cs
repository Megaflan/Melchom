using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchanter
{
    class TXT
    {
        System.Text.Encoding SJIS = System.Text.Encoding.GetEncoding(932);
        List<string> textList = new List<string>();
        public void Initialize(string f)
        {
            Interpeter(f);
            WriteToPo(f);
        }

        private void Interpeter(string f)
        {
            String[] lines = File.ReadAllLines(f, SJIS);
            foreach (var l in lines)
            {
                if (!l.StartsWith("//"))                                   
                    textList.Add(l);                
            }            
        }

        private void WriteToPo(string f)
        {
            PO po = new PO();
            foreach (var t in textList)
            {
                po.POExport(t);
            }
            po.POWrite(f);
        }
    }
}
