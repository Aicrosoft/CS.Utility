using System;
using System.Linq;
using CS.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS.UtilityTests.Utils
{
    [TestClass]
    public class ArrayCopyPerformanceTests
    {
        [TestMethod]
        public void Run()
        {
            int length = 1000 * 1000;
            var source = Enumerable.Repeat(100, length).ToArray();
            var dest = new int[length];

            int iteration = 1000;
            CodeTimer.Initialize();

            CodeTimer.Time("One by one", iteration, () =>
            {
                for (int i = 0; i < length; i++) dest[i] = source[i];
            });

            CodeTimer.Time("Int32[].CopyTo", iteration, () =>
            {
                source.CopyTo(dest, 0);
            });

            CodeTimer.Time("Array.Copy", iteration, () =>
            {
                Array.Copy(source, dest, length);
            });

            CodeTimer.Time("Buffer.BlockCopy", iteration, () =>
            {
                Buffer.BlockCopy(source, 0, dest, 0, length * 4);
            });

            /*
            
            One by one <1,000> 
            -------------------------------------------
	            Time Elapsed:	3,100 ms
	            CPU Cycles:	11,127,172,321
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------

            Int32[].CopyTo <1,000> 
            -------------------------------------------
	            Time Elapsed:	259 ms
	            CPU Cycles:	930,051,832
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------

            Array.Copy <1,000> 
            -------------------------------------------
	            Time Elapsed:	260 ms
	            CPU Cycles:	934,784,097
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------

            Buffer.BlockCopy <1,000> 
            -------------------------------------------
	            Time Elapsed:	260 ms
	            CPU Cycles:	934,540,580
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------


            
            
            */

        }

        /// <summary>
        /// 经RFX大牛提示，在引用复制时会为了避免GC造成影响而使用Write Barrier，因此使用引用类型进行测试会有明显的效果。于是我们来补充一个测试：
        /// </summary>
        [TestMethod]
        public void RunSupplement()
        {
            int length = 1000 * 1000;
            var source = Enumerable.Repeat(new object(), length).ToArray();
            var dest = new object[length];

            int iteration = 1000;
            CodeTimer.Initialize();

            CodeTimer.Time("One by one", iteration, () =>
            {
                for (int i = length - 1; i >= 0; i--) dest[i] = source[i];
            });

            CodeTimer.Time("Int32[].CopyTo", iteration, () =>
            {
                source.CopyTo(dest, 0);
            });

            CodeTimer.Time("Array.Copy", iteration, () =>
            {
                Array.Copy(source, dest, length);
            });

            /*
            
            One by one <1,000> 
            -------------------------------------------
	            Time Elapsed:	4,395 ms
	            CPU Cycles:	15,724,261,165
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------

            Int32[].CopyTo <1,000> 
            -------------------------------------------
	            Time Elapsed:	741 ms
	            CPU Cycles:	2,649,949,768
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------

            Array.Copy <1,000> 
            -------------------------------------------
	            Time Elapsed:	746 ms
	            CPU Cycles:	2,675,445,092
	            Gen 0: 		0
	            Gen 1: 		0
	            Gen 2: 		0
            -------------------------------------------


            */
        }

    }
}