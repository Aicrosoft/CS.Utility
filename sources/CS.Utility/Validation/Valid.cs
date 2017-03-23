#region copyright info
//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 巧思工作室.  All rights reserved.
//     Contact:		Email:atwind@cszi.com , QQ:3329091
//		Link:		 http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------
#endregion

using System;
using System.Text.RegularExpressions;

namespace CS.Validation
{
    /// <summary>
    /// 字符串的相关验证
    /// </summary>
    /// 
    /// <description class = "CS.Utility.Valid">
    /// 
    /// </description>
    /// 
    /// <history>
    ///     Create     :	    Atwind, 2008-6-13 21:39:05;
    ///     Update    :       Atwind , 2010-4-12 增加校验可委托给外部处理
    ///  </history>
    public static class Valid
    {
         /// <summary>
        /// 初始化。
        /// <para>初始化 <see cref="Verify"/> 委托 </para>
        /// </summary>
        static Valid()
        {
            Restore();
        }

        /// <summary>
        /// 验证委托，必要时可以委托外部处理
        /// </summary>
        internal static Func<string,string,bool> Verify;

        /// <summary>
        /// 恢复默认的委托方法
        /// </summary>
        public static void Restore()
        {
            //Verify = (input,pattern) => CheckValue(input,pattern);
            Verify = CheckValue;
        }

        /// <summary>
        /// 检查输入字符串是否为空, 是否符合正则
        /// 注意:空时一定检测失败
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>不为空且符合正则返回 true</returns>
        public static bool CheckValue(string input, string pattern)
        {
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 通用名称验证，不可为空且英文3~50，中文2~25字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool NameValidator(string input)
        {
            return Verify(input, RegexLib.NAME);
        }
        /// <summary>
        /// 描述验证，可以为空，不为空时 英文5~200，中文2~100字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool DescriptionValidator(string input)
        {
            return string.IsNullOrEmpty(input) || Verify(input, RegexLib.DESCRIPTION);
        }

        /// <summary>
        /// 帐户验证[英文数字]:英文开头,4~32位
        /// </summary>
        /// <returns></returns>
        public static bool AccountValidator()
        {
            return false;
        }

        /// <summary>
        /// 帐户验证[英文数字]:英文开头,4~32位
        /// </summary>
        /// <param name="input"></param>
        /// <returns>通过验证为true</returns>
        public static bool AccountValidator(string input)
        {
            return Verify(input, RegexLib.ACCOUNT);
        }
        
        /// <summary>
        /// 名字验证
        /// 英文开头3-16 字符||中文开头 2-12 字符
        /// </summary>
        /// <param name="input">需要验证的 名字</param>
        /// <returns>验证结果</returns>
        public static bool NicknameValidator(string input)
        {
            return Verify(input, RegexLib.NICKNAME);
        }

        /// <summary>
        /// 中文姓名验证[2~8个汉字]
        /// </summary>
        /// <param name="input">需要验证的 名字</param>
        /// <returns>验证结果</returns>
        public static bool ChineseNameValidator(string input)
        {
            return Verify(input, RegexLib.CHINESE_NAME);
        }
        
        /// <summary>
        /// 密码验证:8~32位字符串
        /// </summary>
        /// <param name="input">需要验证的 密码</param>
        /// <returns>验证结果</returns>
        public static bool PasswordValidator(string input)
        {
            return Verify(input, RegexLib.PASSWORD);
        }

        /// <summary>
        /// 严格密码验证：密码中必须包含字母、数字、特称字符，至少8个字符，最多32个字符
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public static bool StrickPasswordValidtor(string intput)
        {
            return Verify(intput, RegexLib.STRICTPASSWORD);
        }

        /// <summary>
        /// QQ 验证
        /// </summary>
        /// <param name="input">需要验证的 QQ</param>
        /// <returns>验证结果</returns>
        public static bool QqValidator(string input)
        {
            return Verify(input, RegexLib.QQ);
        }

        /// <summary>
        /// Email 验证
        /// </summary>
        /// <param name="input">需要验证的 Email</param>
        /// <returns>验证结果</returns>
        public static bool EmailValidator(string input)
        {
            return Verify(input, RegexLib.EMAIL);
        }

        /// <summary>
        /// Url 验证[必须带协议]
        /// </summary>
        /// <param name="p">需要验证的Url</param>
        /// <returns>是否通过验证</returns>
        public static bool UrlValidator(string p)
        {
            return Verify(p, RegexLib.URL);
        }

        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="input">需要验证的 手机号码</param>
        /// <returns>验证结果</returns>
        public static bool MobilephoneValidator(string input)
        {
            return Verify(input, RegexLib.MOBILEPHONE);
        }

        /// <summary>
        /// 电话号码验证[包括国际电话]
        /// </summary>
        /// <param name="input">需要验证的 电话号码</param>
        /// <returns>验证结果</returns>
        public static bool TelephoneValidator(string input)
        {
            return Verify(input, RegexLib.TELEPHONE);
        }

        /// <summary>
        /// 综合性号码验证
        /// </summary>
        /// <param name="input">匹配格式：11位手机号码 3-4位区号，7-8位直播号码，1－4位分机号 如：12345678901、1234-12345678-1234</param>
        /// <returns>结果</returns>
        public static bool PhoneValidator(string input)
        {
            return Verify(input, RegexLib.PHONE);
        }

        /// <summary>
        /// 判断字符串是否是由 _, (_代表空格)分割的 id 字串.
        /// </summary>
        /// <param name="input">需要检查的文本</param>
        /// <returns>检查结果</returns>
        public static bool IdStringValidator(string input)
        {
            return Verify(input.Replace(" ", ""), RegexLib.ID_STRINGS);
        }

      


        #region 身份证验证

        /// <summary>
        /// 身分证校验
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIDCard(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (id.Length == 18) return CheckIDCard18(id);
            return id.Length == 15 && CheckIDCard15(id);
        }

        /// <summary>
        /// 18位身分证验证
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIDCard18(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 18)
                return false;
            long n = 0;
            if (!long.TryParse(id.Remove(17), out n) || n < Math.Pow(10, 16) || !long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n)) return false;//数字验证

            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1) return false;//省份验证

            var birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false) return false;//生日验证

            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var ai = id.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());

            var y = -1;
            Math.DivRem(sum, 11, out y);
            return arrVarifyCode[y] == id.Substring(17, 1).ToLower();
        }

        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIDCard15(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 15)
                return false;
            long n;
            if (!long.TryParse(id, out n) || n < Math.Pow(10, 14)) return false;//数字验证

            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1) return false;//省份验证

            var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            return DateTime.TryParse(birth, out time);
        }

        #endregion


    }
}