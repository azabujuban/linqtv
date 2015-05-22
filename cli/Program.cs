using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAP;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            new Parser<Commands>().Run(args, new Commands());
        }
    }
}
