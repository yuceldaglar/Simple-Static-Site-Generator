using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleStaticSiteGenerator
{
    class Commands
    {
        private const string OUTPUT = "_output";
        private const string HEADER = "header.txt";
        private const string FOOTER = "footer.txt";

        public static void generate(string rootDir, string outputDir)
        {
            outputDir ??= Path.Combine(rootDir, OUTPUT);

            GenerateDirectory(rootDir, null, null, outputDir);
        }

        private static void GenerateDirectory(string dir, string header, string footer, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

            header = GetHeader(dir) ?? header;
            footer = GetFooter(dir) ?? header;

            var files = Directory.GetFiles(dir, "*.html") ?? new string[0];
            foreach (var file in files)
            {

                GenerateFile(file, header, footer, outputPath);
            }

            var directories = Directory.GetDirectories(dir) ?? new string[0];
            foreach (var directory in directories)
            {
                if (Path.GetFileName(directory) == OUTPUT) continue;

                var subDirectory = Path.Combine(outputPath, directory);

                GenerateDirectory(directory, header, footer, subDirectory);
            }
        }

        private static void GenerateFile(string file, string header, string footer, string outputPath)
        {
            var content = header + File.ReadAllText(file) + footer;
            var filename = Path.GetFileName(file);
            var outfile = Path.Combine(outputPath, filename);

            File.WriteAllText(outfile, content);
        }

        private static string GetHeader(string dir)
        {
            var file = Path.Combine(dir, HEADER);
            if (!File.Exists(file)) return null;

            return File.ReadAllText(file);
        }

        private static string GetFooter(string dir)
        {
            var file = Path.Combine(dir, FOOTER);
            if (!File.Exists(file)) return null;

            return File.ReadAllText(file);
        }
    }
}