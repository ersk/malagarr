using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Parse_Backstories
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {


                string rimWorldPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\RimWorld";

                Parser parser = new(rimWorldPath);
                Defs defs = parser.Run();

                Writer writer = new(defs);
                writer.Run();
            }
            catch(Exception ex)
            {
                string s= "";
            }
        }
    }

    public class Writer
    {
        Defs defs;
        public Writer(Defs defs)
        {
            this.defs = defs;
        }
        public void Run()
        {
            FileStream fs = File.Create("export.csv");

            StreamWriter sw = new StreamWriter(fs);

            defs.BackstoryDef.ForEach(bs =>
            {
                sw.WriteLine(bs.ToCsvString());
            });

            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }
    }

    public class Parser
    {
        const string defsRelativePath = "Data\\Core\\Defs";

        string defsPath;

        public Parser(string rimWorldPath)
        {
            defsPath = Path.Combine(rimWorldPath, defsRelativePath);
        }

        public Defs Run() 
        {
            string solidAdultRelativeFilePath = "BackstoryDefs\\Solid\\Solid_Adult.xml";
            string solidAdultPath = Path.Combine(defsPath, solidAdultRelativeFilePath);

            Defs topDef = ReadDefFile(solidAdultPath);

            return topDef;
        }

        //public class TopDefs
        //{
        //    public List<Back>? Defs { get; set; }
        //}

        public Defs ReadDefFile(string filePath)
        {
            //string allText = File.ReadAllText(filePath);

            //JsonSerializerOptions options = new()
            //{
            //    PropertyNameCaseInsensitive = true,
            //};
            //TopDefs? def = System.Text.Json.JsonSerializer.Deserialize<TopDefs>(allText, options);

            Defs? def;
            using (StreamReader sr = new StreamReader(filePath))
            {
                XmlReader reader = XmlReader.Create(sr);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Defs));
                def = (Defs?)xmlSerializer.Deserialize(reader, null);
            }

            if(def == null)
            {
                throw new Exception("File deserialized to null.");
            }

            return def;

        }


    }
}
