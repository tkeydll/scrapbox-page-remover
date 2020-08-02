using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CommandLine;

namespace scrapbox_page_remover
{
    class Program
    {
        public class Options
        {
            [Option('s', "source-file", Required = true, HelpText = "Set source file name (json).")]
            public string Source {get;set;}

            [Option('o', "output", Required = false, Default = "result.json", HelpText = "Set output file name.")]
            public string Output {get;set;}

            [Option('p', "pattern", Required = true, HelpText = "Set the pattern to match the title to be deleted.")]
            public string Pattern { get; set; }

            [Option('d',"debug", Required = false, Default = false, HelpText = "Show page title to debugging.")]
            public bool Debug {get;set;}
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(opt => {
                    Console.WriteLine("Option parse failed.");
                    return;
                });
        }

        static void RunOptions(Options opt)
        {
            string jsonString = string.Empty;
            using(var reader = new StreamReader(opt.Source))
            {
                jsonString = reader.ReadToEnd();
            }

            var o = JObject.Parse(jsonString);
            var pages = o["pages"];

            // show all pages.
            Console.WriteLine("Total Pages = " + pages.Count());
            if (opt.Debug)
            {
                pages.ToList().ForEach(x => Console.WriteLine(x["title"]));
                Console.WriteLine();
            }

            // select target
            var regex = new System.Text.RegularExpressions.Regex(opt.Pattern);
            var matched_pages = pages.Where(x => regex.IsMatch((string)x["title"]));
            
            // show target pages.
            Console.WriteLine("Target Pages = " + matched_pages.Count());
            if (opt.Debug)
            {
                matched_pages.ToList()
                            .ForEach(x => Console.WriteLine(x["title"]));
                Console.WriteLine();
            }

            // remove matched page
            matched_pages.ToList()
                         .ForEach(x => x.Remove());
                         

            // show saved pages.
            Console.WriteLine("Saved Pages = " + pages.Count());
            if (opt.Debug)
            {
                pages.ToList().ForEach(x => Console.WriteLine(x["title"]));
                Console.WriteLine();
            }

            // replace pages.
            o["pages"] = pages;

            // write result.
            using (var writer = new StreamWriter(opt.Output))
            {
                writer.WriteLine(JsonConvert.SerializeObject(o));
                Console.WriteLine();
            }
        }
    }
}
