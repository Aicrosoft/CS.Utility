#region copyright info
//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 巧思工作室.  All rights reserved.
//     Contact:		email:atwind@cszi.com , QQ:3329091
//		Link:		http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------
#endregion
using System;

namespace CS.Theory
{
    /// <summary>
    ///  正态分布随机数 ver:0.10
    ///   Gaussian Random Number Generator class
    ///   ref. ``Numerical Recipes in C++ 2/e'', p.293 ~ p.294
    /// </summary>
    /// 
    /// <description class = "CS.Theory.GaussianRandom">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-4-2 13:24:42 , atwind ,  创建	     
    ///  </history>
    public class GaussianRandom
    {
        int _iset;
        double _gset;
        readonly Random r1, r2;

        ///<summary>
        /// 默认构造 
        ///</summary>
        public GaussianRandom()
        {
            r1 = new Random(unchecked((int)DateTime.Now.Ticks));
            r2 = new Random(~unchecked((int)DateTime.Now.Ticks));
            _iset = 0;
        }
        
        /// <summary>
        /// 返回一个double形的随机数
        /// </summary>
        /// <returns>可正可负的随机数</returns>
        public double Next()
        {
            if (_iset == 0)
            {
                double rsq, v1, v2;
                do
                {
                    v1 = 2.0 * r1.NextDouble() - 1.0;
                    v2 = 2.0 * r2.NextDouble() - 1.0;
                    rsq = v1 * v1 + v2 * v2;
                } 
                while (rsq >= 1.0 || rsq == 0.0);

                var fac = Math.Sqrt(-2.0 * Math.Log(rsq) / rsq);
                _gset = v1 * fac;
                _iset = 1;
                return v2 * fac;
            }
            _iset = 0;
            return _gset;
        }

        



    }
}