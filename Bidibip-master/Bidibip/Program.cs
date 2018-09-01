using Bidibip.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidibip
{
    /// <summary>
    /// Entry class
    /// Created by Zino2201
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                new BidibipBot("config.ini").Start().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}\n{1}", e.Message, e.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
