using System;
using Microsoft.Owin.Hosting;

namespace webserver
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Press [q] to quit...");
                while (Console.ReadKey().Key != ConsoleKey.Q)
                {
                    
                };
                Console.WriteLine("Quitting");
            }
        }
    }
}
