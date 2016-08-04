# CS.Utility



## CS.Utility ##

###常用扩展及辅助类库###

####**CS.Utility** 是一个常用库，如类型的快速转换，辅助工具类，性能调试与监测，缓存管理 等####

#### CS.Utility 原来的[CSHive](https://github.com/CSStudio/CSHive)项目重构。C#版的常用库，类型的转换，辅助工具类，适配其它的开源库采用Adapter模式，避免对不同开源类库的引用膨胀。
----------
>*Tags*:CSStudio,CSWare,Utility,Lib,Log,Trace,Cache,Extension

----------

### Build & Test Status ###
----------
dev: [![Build status](https://ci.appveyor.com/api/projects/status/gpf06okuuyjw6fhg?svg=true)](https://ci.appveyor.com/project/cswares/cs-utility)  
master: [![Build status](https://ci.appveyor.com/api/projects/status/gpf06okuuyjw6fhg/branch/master?svg=true)](https://ci.appveyor.com/project/cswares/cs-utility/branch/master)  
nuget: [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg?maxAge=2592000)](https://www.nuget.org/packages/CS.Utility/)   

---------------
### 安装本类库 (To install CS.Utility, run the following command in the Package Manager Console)

> PM> Install-Package CS.Utility

--------------

### 开发约定
- .Net最低版本4.6
- IDE环境：VS2015
- **CS.Utility**项目中不引用任何非.Net的基础库，但会通过Adapter模式引入其它的开源库
- 除非特殊情况，都采用UTF-8进行编码（如遇特殊情况，也必须提供UTF8的接口）
- Extension文件夹采用命字空间命名，方便扩展方法的使用。

----------

### 常用开发类库的引用与示例 ###

## log4net的引入 ##
**LogHelper** 是通过LogManager创建Log实例的辅助方法，在该类所有的程序集的**AssemblyInfo.cs**中加入如下两行内容（配置示例在[doc]/log4net.config）

```C#
//外置log4net配置
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
```

----------

## 常用的Nuget ##

### .Net ###
- PM> Install-Package log4net
- PM> Install-Package Newtonsoft.Json

### jQuery ###
- PM> Install-Package jQuery
- PM> Install-Package jQuery.UI.Combined
- PM> Install-Package jQuery.Validation


----------


### 关于作者 ###

[艺风在线](http://max.cszi.com)

##### 我要站在巨人们的肩膀上 #####

------------
>*Author*: [atwind](mailto:atwind@cszi.com)   
>*Owners*: CSStudio    
>*Copyright*: [cszi.com](http://www.cszi.com)     
   
----------
