using System;
using CS.Utils.Tests;

namespace CS.UtilityTests
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
           var x = new CodeTimerTests();
            x.Run();
        }
    }
}
