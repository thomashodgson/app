﻿using System;
using Microsoft.Owin.Hosting;

namespace webserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var isInDocker = Environment.GetEnvironmentVariable("APP_RABBIT_NAME") != null;
            string baseAddress = isInDocker ? "http://*:9000/" : "http://localhost:9000/";

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
