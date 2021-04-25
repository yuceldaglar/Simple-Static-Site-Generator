using System;

namespace SimpleStaticSiteGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleEngine = new ConsoleEngine();
            
            consoleEngine.AddStaticClassMethods<Commands>();

            consoleEngine.Initialize(args);
        }
    }
}
