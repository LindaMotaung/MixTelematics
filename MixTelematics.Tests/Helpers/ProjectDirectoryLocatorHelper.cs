using System;
using System.Diagnostics;
using System.Reflection;
using MixTelematics.Helpers;

namespace MixTelematics.Tests.Helpers
{
    public class ProjectDirectoryLocatorHelper
    {
        public readonly string RootLevel = ProjectDirectoryLocator.GetProjectDirectory();

        public string Path()
        {
            var callingAssembly = GetCallingAssembly();
            var assemblyName = callingAssembly.GetName().Name;
            var filePath = string.Empty;

            if (assemblyName != null && assemblyName.EndsWith(".Tests"))
            {
                filePath = RootLevel[..RootLevel.IndexOf(".Tests", StringComparison.Ordinal)];
            }

            return filePath;
        }

        private static Assembly GetCallingAssembly()
        {
            // Get the calling assembly from the call stack
            var stackTrace = new StackTrace();
            var callingFrame = stackTrace.GetFrame(2);
            Assembly callingAssemblyTest = null;
            var callingMethod = callingFrame?.GetMethod();
            if (callingMethod is null) return null;
            if (callingMethod.DeclaringType != null)
            {
                //var callingAssembly = callingMethod.DeclaringType.Assembly;
                callingAssemblyTest = callingMethod.DeclaringType.Assembly;
            }

            return callingAssemblyTest;
        }
    }
}
