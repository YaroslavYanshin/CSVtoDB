using BL;
using System;
using System.Configuration;

namespace CSVtoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFolder = ConfigurationManager.AppSettings["CSVSourceFolder"];
            CSVController controller = new CSVController(sourceFolder);
            controller.Run();

            Console.WriteLine("\nTo stop watching and exit press '0'");
            while (Console.ReadKey(true).KeyChar != '0') ;
            controller.Stop();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

        }
    }
}
