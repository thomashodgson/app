using System;
using System.Collections.Generic;
using Autofac;
using messaging;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
         {
 
             using (StartService())
             {
                Console.WriteLine("Press [q] to quit...");
                 while (Console.ReadKey().Key != ConsoleKey.Q)
                 {
 
                 };
                 Console.WriteLine("Quitting");
             }
         }
 
         private static IDisposable StartService()
         {
             var builder = new ContainerBuilder();
             builder.RegisterModule<WorkerModule>();
             var container = builder.Build();
 
             foreach (var messageConsumer in container.Resolve<IEnumerable<IMessageConsumer>>())
             {
                 messageConsumer.Start();
             }
 
             return container;
         }
    }
}
