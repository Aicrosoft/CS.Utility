using System;
using CS.UtilityTests.Utils;

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
