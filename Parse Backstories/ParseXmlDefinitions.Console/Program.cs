using ParseXmlDefinitions.Model;
using System.Xml;
using System.Xml.Serialization;

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

                string rimWorldPath = "C:\\src\\github\\rimworld-defs";

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
                ///C:\src\github\rimworld-defs\BackstoryDefs\Solid\Solid_Adult.xml
                //CC:\src\github\rimworld-defs\Data\Core\Defs\BackstoryDefs\Solid
                string solidAdultRelativeFilePath = "BackstoryDefs\\Solid\\Solid_Adult.xml";
                string solidAdultPath = Path.Combine(defsPath, solidAdultRelativeFilePath);

                XmlDefFileReader reader = new(solidAdultPath);
                List<DefElement> defs = reader.Parse();

                DefTypeLibrary library = new();
                library.ParseDefs(defs);

                var lib = library.library;

                string s = "";

            }



        }
    }
}
