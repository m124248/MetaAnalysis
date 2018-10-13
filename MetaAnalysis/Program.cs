using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RDotNet;

namespace MetaAnalysis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();

            var scriptFilePath = "RinC/RScript.txt";
            var csvFilePath = "Data/dat_mes.csv";
            //var valueAtRisk = "25750000000";
            ExecuteScriptFile(scriptFilePath, csvFilePath);
        }

        public static void ExecuteScriptFile(string scriptFilePath, string csvFilePath) //there was a parameter for var valueAtRisk
        {
            using (var en = REngine.GetInstance())
            {
                var args_r = new string[1] { csvFilePath };
                var execution = "source('" + scriptFilePath + "')";
                en.SetCommandLineArguments(args_r);
                en.Evaluate(execution);
            }
        }
    }
}
