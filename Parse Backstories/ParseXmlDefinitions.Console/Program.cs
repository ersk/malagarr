using System.Xml.Serialization;
using System.Xml;

namespace ParseXmlDefinitions.ConsoleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            try
            {
                Console.WriteLine("Test");

                string rimWorldPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\RimWorld";

                Parser parser = new(rimWorldPath);
                parser.Run();

            }
            catch (Exception ex)
            {
                string s = "";
            }

            //XmlDefFileReader reader = new();

        }

        public class Parser
        {
            const string defsRelativePath = "Data\\Core\\Defs";

            string defsPath;

            public Parser(string rimWorldPath)
            {
                defsPath = Path.Combine(rimWorldPath, defsRelativePath);
            }

            public void Run()
            {
                string solidAdultRelativeFilePath = "BackstoryDefs\\Solid\\Solid_Adult.xml";
                string solidAdultPath = Path.Combine(defsPath, solidAdultRelativeFilePath);

                XmlDefFileReader reader = new(solidAdultPath);
                reader.Parse();
            }



        }
    }
}
