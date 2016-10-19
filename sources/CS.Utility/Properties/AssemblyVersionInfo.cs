using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

////************************版本信息是CS.Utility和CS.Utility.Extension的共同版本，CS.Utility.Extension是CS.Utility的先行官，新功能先在其中实践后酌情收录至CS.Utility（且加入了第三方扩展）

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
[assembly: AssemblyVersion("1.0.6.0")]  //已取消CI脚本的版本号替换功能，该版本号将发布至NuGet上。Note:版本号不变化时将不会发布至Nuget上
//[assembly: AssemblyFileVersion("1.0.0.0")]


/*



v 1.0.6.0
-----------------
. Json序列化时的null判断
. 升级相关引用的版本号



v 1.0.5.0
-----------------
. 增加基本类型的相关扩展
. 结构调整

v 0.1.4.0
-----------------
. 所有.NetFramework版本改为4.5

v 0.1.3.0
------------------
. 加入老赵的CodeTime效能计算类
. 加入DebugConsole彩色控制台输出类

v 0.1.0.2
------------------
1. 新项目启动
2. 日志适配模式与相关日志适配器的实现
3. 默认的SectionGroup配置项可以由AppSettings中指定

*/
