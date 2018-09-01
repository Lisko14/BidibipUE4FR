using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidibip.Utils
{
    /// <summary>
    /// Simple logging class
    /// Created by Zino2201
    /// </summary>
    public class Logger
    {
        public static Task Log(string message)
        {
            Console.WriteLine(message);

            return Task.CompletedTask;
        }

        public static Task Log(LogMessage message)
        {
            Console.WriteLine("[{0}] {1}", message.Severity, message.Message);

            return Task.CompletedTask;
        }

        public static Task Log(string format, params string[] p)
        {
            Console.WriteLine(format, p);

            return Task.CompletedTask;
        }
    }
}
