using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using CS.Extension;

namespace CS
{
    /// <summary>
    /// 当前主机的相关情况，类似于Environment
    /// </summary>
    public class Host
    {

        #region DllImport 声明

        //[DllImport("IpHlpApi.dll")]
        //private static extern uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);

        //[DllImport("User32")]
        //private static extern int GetWindow(int hWnd, int wCmd);

        //[DllImport("User32")]
        //private static extern int GetWindowLongA(int hWnd, int wIndx);

        //[DllImport("user32.dll")]
        //private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

        //[DllImport("user32", CharSet = CharSet.Auto)]
        //private static extern int GetWindowTextLength(IntPtr hWnd);

        #endregion


        #region 结束指定进程

        /// <summary>
        /// 结束指定进程
        /// </summary>
        /// <param name="pid">进程的 Process ID</param>
        public static void EndProcess(int pid)
        {
            try
            {
                var process = Process.GetProcessById(pid);
                process.Kill();
            }
            catch (Exception ex)
            {
                //Tracer.Error($"EndProcess:Id={pid}时异常", ex);
            }
        }

        #endregion


        #region 查找所有应用程序标题

        //private const int GW_HWNDFIRST = 0;
        //private const int GW_HWNDNEXT = 2;
        //private const int GWL_STYLE = (-16);
        //private const int WS_VISIBLE = 268435456;
        //private const int WS_BORDER = 8388608;

        ///// <summary>
        ///// 查找所有应用程序标题
        ///// </summary>
        ///// <returns>应用程序标题范型</returns>
        //public static List<string> FindAllApps(int handle)
        //{
        //    var apps = new List<string>();
        //    var hwCurr = GetWindow(handle, GW_HWNDFIRST);
        //    while (hwCurr > 0)
        //    {
        //        const int isTask = (WS_VISIBLE | WS_BORDER);
        //        var lngStyle = GetWindowLongA(hwCurr, GWL_STYLE);
        //        var taskWindow = ((lngStyle & isTask) == isTask);
        //        if (taskWindow)
        //        {
        //            var length = GetWindowTextLength(new IntPtr(hwCurr));
        //            var sb = new StringBuilder(2*length + 1);
        //            GetWindowText(hwCurr, sb, sb.Capacity);
        //            var strTitle = sb.ToString();
        //            if (!string.IsNullOrEmpty(strTitle))
        //            {
        //                apps.Add(strTitle);
        //            }
        //        }
        //        hwCurr = GetWindow(hwCurr, GW_HWNDNEXT);
        //    }

        //    return apps;
        //}

        #endregion


        ///// <summary>
        ///// CPU的核心数量
        ///// </summary>
        //public static int CpuCores
        //{
        //    get
        //    {
        //        return
        //            new ManagementObjectSearcher("Select * from Win32_Processor").Get()
        //                .Cast<ManagementBaseObject>()
        //                .Sum(item => int.Parse(item["NumberOfCores"].ToString()));
        //    }
        //}

        ///// <summary>
        ///// 物理核心数
        ///// </summary>
        //public static int CpuPhysicsCores
        //{
        //    get
        //    {
        //        return
        //            new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get()
        //                .Cast<ManagementBaseObject>()
        //                .Sum(item => item["NumberOfProcessors"].ToInt(0));
        //    }
        //}


        /// <summary>
        /// 当前运行程序的目录
        /// </summary>
        public static string CurrentDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// 当前CPU的个数(逻辑)
        /// </summary>
        public static int ProcessorCount => Environment.ProcessorCount;

        /// <summary>
        /// 系统换行符
        /// </summary>
        public static string NewLine => Environment.NewLine;

        /// <summary>
        /// 主机名称
        /// </summary>
        public static string UserDomainName => Environment.UserDomainName;

    }

    /// <summary>
    /// 系统负载信息
    /// </summary>
    public class HostLoad
    {
        private readonly PerformanceCounter _pcCpuLoad; //CPU计数器

        /// <summary>
        /// 构造函数，初始化计数器等
        /// </summary>
        public HostLoad()
        {
            //初始化CPU计数器
            _pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total") { MachineName = "." };
            _pcCpuLoad.NextValue();

            //获得物理内存
            var mc = new ManagementClass("Win32_ComputerSystem");
            var moc = mc.GetInstances();
            foreach (var mo in moc.Cast<ManagementObject>().Where(mo => mo["TotalPhysicalMemory"] != null))
            {
                PhysicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString());
            }
        }

        #region 属性

        /// <summary>
        /// 获取可用内存
        /// </summary>
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory");
                //foreach (ManagementObject mo in mos.Get())
                //{
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString());
                //}
                var mos = new ManagementClass("Win32_OperatingSystem");
                foreach (var mo in mos.GetInstances().Cast<ManagementObject>().Where(mo => mo["FreePhysicalMemory"] != null))
                {

                    availablebytes = 1024 * mo["FreePhysicalMemory"].ToLong();
                    //long.Parse(mo["FreePhysicalMemory"].ToString());
                    //Tracer.Debug(availablebytes);
                }
                return availablebytes;
            }
        }

        /// <summary>
        /// 获取物理内存
        /// </summary>
        public long PhysicalMemory { get; } = 0;

        /// <summary>
        /// CPU负载(百分比)
        /// </summary>
        public float MemoryLoad => 100 * (float)(PhysicalMemory - MemoryAvailable) / PhysicalMemory;

        /// <summary>
        /// 获取当前的CPU负载(百分比)
        /// </summary>
        public float CpuLoad => _pcCpuLoad.NextValue();

        #endregion
    }



    /// <summary>
    /// CLR运行时类型
    /// </summary>
    public enum ClrRuntimeType
    {


    }
}