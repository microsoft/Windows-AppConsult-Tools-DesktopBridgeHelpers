using System;
using System.Runtime.InteropServices;

namespace DesktopBridge
{
    public unsafe class Helpers
    {
        const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern int GetCurrentPackageFullName(uint* packageFullNameLength, char* packageFullName);

        public bool IsRunningAsUwp()
        {
            if (IsWindows7OrLower)
            {
                return false;
            }
            else
            {
                uint length = 0;
                int result = GetCurrentPackageFullName(&length, null);
                return result != APPMODEL_ERROR_NO_PACKAGE;
            }
        }

        private bool IsWindows7OrLower
        {
            get
            {
                int versionMajor = Environment.OSVersion.Version.Major;
                int versionMinor = Environment.OSVersion.Version.Minor;
                double version = versionMajor + (double)versionMinor / 10;
                return version <= 6.1;
            }
        }
    }
}
