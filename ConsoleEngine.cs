using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleStaticSiteGenerator
{
    class ConsoleEngine
    {
        private List<Type> CommandClasses = new List<Type>();

        public void AddStaticClassMethods<T>()
        {
            CommandClasses.Add(typeof(T));
        }

        public void Initialize(string[] args)
        {
            bool hasArguments = args != null && args.Length > 0;
            if (!hasArguments)
            {
                PrintHelp();
                return;
            }

            var argsIsValid = Execute(args);

            if (!argsIsValid)
                PrintHelp();
        }

        private bool Execute(string[] args)
        {
            var argDic = new Dictionary<string, string>();
            for (int i = 1; i < args.Length; i += 2)
            {
                argDic[args[i].TrimStart('-')] = args[i + 1];
            }

            foreach (var t in CommandClasses)
            {
                var method = t.GetMethod(args[0]);
                if (method != null)
                {
                    var paramList = new List<string>();
                    foreach (var p in method.GetParameters())
                    {
                        if (argDic.ContainsKey(p.Name))
                            paramList.Add(argDic[p.Name]);
                        else
                            paramList.Add(null);
                    }

                    method.Invoke(null, paramList.ToArray());

                    return true;
                }
            }

            return false;
        }

        private void PrintHelp()
        {
            Console.WriteLine("usage: command [--optionA value --optionB value ...]");
            Console.WriteLine("");
            Console.WriteLine("You can use following commands:");

            foreach (var t in CommandClasses)
            {
                foreach (var methodInfo in t.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                {
                    var options = string.Join(" --", methodInfo.GetParameters()?.Select(x => x.Name) ?? new string[0]);
                    Console.WriteLine($"\t{methodInfo.Name} --{options}");
                }
            }
        }
    }
}
