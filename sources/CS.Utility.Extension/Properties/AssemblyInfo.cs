﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("CS.Utility.Extension")]
[assembly: AssemblyDescription("CS.Utility,需要引用第三个库才能使用的相关扩展内容")]
[assembly: AssemblyConfiguration("")]

//将 ComVisible 设置为 false 将使此程序集中的类型
//对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("26edf82e-9da2-41b5-a28d-00e038c01ecc")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本 ： 具有相同名称但不同主版本号的程序集不可互换。
//      次版本 ： 如果两个程序集的名称和主版本号相同，而次版本号不同，这指示显著增强，但照顾到了向后兼容性。
//      修订号 ： 一般是Bug 的修复或是一些小的变动或是一些功能的扩充，要经常发布    修订版，修复一个严重 Bug 即可发布一个修订版
//      生成号 ： 每次生成时不同
//      附加说明：base、alpha、beta 、RC 、 release
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.1.0.0")]  //已取消CI脚本的版本号替换功能，该版本号将发布至NuGet上。Note:版本号不变化时将不会发布至Nuget上
//[assembly: AssemblyFileVersion("1.0.0.0")]


/*


v0.0.1.0.1
-------------
. 引入Newtonsoft.Json，扩展Json序列化



*/
