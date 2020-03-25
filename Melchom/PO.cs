using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl;
using Yarhl.FileFormat;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace Merchanter
{
    class PO
    {
        System.Text.Encoding SJIS = System.Text.Encoding.GetEncoding(932);
        Dictionary dic = new Dictionary();

        Po poYarhl = new Po
        {
            Header = new PoHeader("Recettear: An Item Shop's Tale", "tradusquare@gmail.com", "es")
            {
                LanguageTeam = "TraduSquare",
            }
        };

        public void POExport(string toPO)
        {
            if (toPO != "")
            {
                var lcontext = "";
                if (toPO.Contains(":"))
                {
                    var lpack = toPO.Split(':');                    
                    for (int i = 0; i < lpack.Length - 1; i++)
                    {
                        lcontext += lpack[i] + ":";
                    }
                    toPO = lpack[lpack.Length - 1];
                }
                poYarhl.Add(new PoEntry(toPO.Replace("<BR>", "\n")) { Context = lcontext });
            }
        }

        public void POWrite(string file)
        {
            poYarhl.ConvertTo<BinaryFormat>().Stream.WriteTo(file + ".po");
        }


        public void POImport(string poFile)
        {
            var poInstance = new BinaryFormat(new DataStream(poFile, FileOpenMode.Read)).ConvertTo<Po>();
            List<string> textList = new List<string>();
                
            foreach (var p in poInstance.Entries)
            {
                textList.Add(p.Context + dic.Transform(p.Text, "dicHW2FW"));
            }
            File.WriteAllLines(poFile + Path.GetFileNameWithoutExtension(poFile) + ".txt", textList.ToArray());            
        }
    }
}
