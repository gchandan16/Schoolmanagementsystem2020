using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    internal class Log
    {
        static string Library = "BALCommon";
        static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        static Log()
        {
        }

        public static void Warning(string message)
        {
            Logger.Write(string.Format("{0, -10} | {1}", Version, message), Library, 0, 0, System.Diagnostics.TraceEventType.Warning);
        }

        public static void Info(string message)
        {
            Logger.Write(string.Format("{0, -10} | {1}", Version, message), Library, 0, 0, System.Diagnostics.TraceEventType.Information);
        }

        public static void Error(string message)
        {
            Logger.Write(string.Format("{0, -10} | {1}", Version, message), Library, 0, 0, System.Diagnostics.TraceEventType.Error);
        }

        public static void Critical(string message)
        {
            Logger.Write(string.Format("{0, -10} | {1}", Version, message), Library, 0, 0, System.Diagnostics.TraceEventType.Critical);
        }

        public static void Debug(string message)
        {
            Logger.Write(string.Format("{0, -10} | {1}", Version, message), Library, 0, 0, System.Diagnostics.TraceEventType.Verbose);

        }
    }
}
