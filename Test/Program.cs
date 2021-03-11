﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SyslogLogging;

namespace Test
{
    class Program
    {
        public static LoggingModule log;

        static void Main(string[] args)
        {
            /*
            List<SyslogServer> servers = new List<SyslogServer>
            {
                new SyslogServer("myhost.com", 514),
                new SyslogServer("127.0.0.1", 514)
            };
            */

            string json = "[{'Hostname':'127.0.0.1','Port':514},{'Hostname':'logs5.papertrailapp.com','Port':21657}]";
            List<SyslogServer> servers = JsonConvert.DeserializeObject<List<SyslogServer>>(json);

            log = new LoggingModule(servers, true);
            log.Settings.MinimumSeverity = Severity.Debug;
            log.Settings.FileLogging = FileLoggingMode.SingleLogFile;
            log.Settings.LogFilename = "test.log";
             
            log.Debug("This is a Debug message.");
            log.Info("This is an Info message.");
            log.Warn("This is a Warn message.");
            log.Error("This is an Error message.");
            log.Alert("This is an Alert message.");
            log.Critical("This is a Critical message.");
            log.Emergency("This is an Emergency message.");
            log.Info("Let's test logging an exception.");
            log.Log(Severity.Info, "Just another way to create log messages!");

            int numerator = 15;
            int denominator = 0;

            try
            {
                log.Critical("Shall we divide by zero?");
                int testVal = numerator / denominator;
                log.Warn("If you see this, there's a problem.");
            }
            catch (Exception e)
            { 
                e.Data.Add("foo", "bar");
                log.Exception(e, "Program", "Main");
            }

            log.Alert("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
