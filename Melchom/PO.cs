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
            var text = toPO.Split(':');
            poYarhl.Add(new PoEntry(text[3].Replace("<BR>", "\n"))
            {
                Context = text[0] + ":" + text[1] + ":" + text[2] + ":"
            });
        }

        public void POWrite(string file)
        {
            poYarhl.ConvertTo<BinaryFormat>().Stream.WriteTo(file + ".po");
        }


        public void POImport(string poFile, string outFile)
        {
            var poInstance = new BinaryFormat(new DataStream(poFile, FileOpenMode.Read)).ConvertTo<Po>();

            var o = File.ReadAllLines(outFile, SJIS);
            foreach (var p in poInstance.Entries)
            {
                var con = p.Context.Split(',');
                o[Convert.ToInt32(con[0])] = con[1] + dic.Transform(p.Text.Replace("\n", "<BR>"), "dicHW2FW");
            }
            File.WriteAllLines(outFile, o, SJIS);            
        }
    }
}
