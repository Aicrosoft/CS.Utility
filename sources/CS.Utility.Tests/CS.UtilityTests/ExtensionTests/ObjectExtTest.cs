using System;
using System.Collections.Generic;
using CS.Extension;
using CS.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS.UtilityTests.ExtensionTests
{
    [TestClass]
    public class ObjectExtTest
    {
        //[TestMethod]
        //public void FillPropertiesTest()
        //{
        //    var s = new Sample {Name = "temp", Age = 15};
        //    var dic = new SortedDictionary<string, object>();
        //    dic["name"] = null;
        //    dic["age"] = null;

        //    s.FillProperties(dic);
        //    Console.WriteLine(dic.ToJsonByJc());
        //}

        [TestMethod]
        public void GetSortdDictionaryTest()
        {
            var s = new Sample { Name = "temp", Age = 15 };
            var dic = s.GetPropertiesSortedDictionary(new string[] { "name", "age" ,"temp"});
            Console.WriteLine(dic.ToJsonByJc());


            CodeTimer.Time("GetSortedDictionary",1000, () =>
            {
                var dic1 = s.GetPropertiesSortedDictionary(new string[] { "name", "age","temp" });
                var x = dic1.Count;

            });
        }

    }

    internal class Sample
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public void Put()
        {
            Console.WriteLine("test");
        }
    }
}