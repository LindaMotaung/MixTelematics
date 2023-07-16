using System;
using System.IO;

namespace MixTelematics.Helpers
{
    public static class ProjectDirectoryLocator
    {
        public static string GetProjectDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectDirectory = currentDirectory;

            for (var i = 0; i < 3; i++)
            {
                projectDirectory = Directory.GetParent(projectDirectory)?.FullName;
                if (projectDirectory == null)
                {
                    throw new InvalidOperationException("Unable to find the project directory.");
                }
            }

            return projectDirectory;
        }
    }
}
