using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TaskManager.Test.Utils
{
    public static class ProjectUtil
    {
        public static string GetProjectPath(string slnName, string solutionRelativePath, Assembly startupAssembly)
        {
            string projectName = startupAssembly.GetName().Name;
            string applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, slnName));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}
