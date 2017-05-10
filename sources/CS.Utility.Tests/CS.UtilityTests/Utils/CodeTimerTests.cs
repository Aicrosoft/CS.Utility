using System.Text;
using System.Threading;
using CS.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS.UtilityTests.Utils
{
    [TestClass]
    public class CodeTimerTests
    {

        [TestMethod]
        public void Run()
        {
            CodeTimer.Initialize();

            CodeTimer.Time("Thread Sleep", 1, () => { Thread.Sleep(3000); });
            CodeTimer.Time("Empty Method", 10000000, () => { });
            /*
            与传统计数方法相比，这段代码还输出了更多信息：
            CPU时钟周期及各代垃圾收集回收次数。
            CPU时钟周期是性能计数中的辅助参考，说明CPU分配了多少时间片给这段方法来执行，它和消耗时间并没有必然联系。
            例如Thread.Sleep方法会让CPU暂时停止对当前线程的“供给”，这样虽然消耗了时间，但是节省了CPU时钟周期

            Thread Sleep
	            Time Elapsed:	3,000.000ms
	            CPU Cycles:	280,941.000
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0

            Empty Method
	            Time Elapsed:	54.000ms
	            CPU Cycles:	195,135,672.000
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0

            */

            
            int iteration = 100 * 1000;
            string s = "";
            CodeTimer.Time("String Concat", iteration, () => { s += "a"; });
            StringBuilder sb = new StringBuilder();
            CodeTimer.Time("StringBuilder", iteration, () => { sb.Append("a"); });
            /*
            而垃圾收集次数的统计，即直观地反应了方法资源分配（消耗）的规模: 
            String Concat
	            Time Elapsed:	1,787.000ms
	            CPU Cycles:	6,416,259,101.000
	            Gen 0: 		2972
	            Gen 1: 		2545
	            Gen 2: 		2545

            StringBuilder
	            Time Elapsed:	0.000ms
	            CPU Cycles:	3,517,830.000
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            
            */

        }



    }
}