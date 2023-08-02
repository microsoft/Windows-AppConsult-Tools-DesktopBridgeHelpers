using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DesktopBridge
{
    public class Helpers
    {
        const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);

        public bool IsRunningAsUwp()
        {
            if (!IsWindows8OrHigher)
            {
                return false;
            }
            else
            {
                int length = 0;
                StringBuilder sb = new StringBuilder(0);
                int result = GetCurrentPackageFullName(ref length, sb);

                sb = new StringBuilder(length);
                result = GetCurrentPackageFullName(ref length, sb);

                return result != APPMODEL_ERROR_NO_PACKAGE;
            }
        }

        private bool IsWindows8OrHigher
        {
            get
            {
                OperatingSystem osVersion = Environment.OSVersion;
                if (osVersion.Platform != PlatformID.Win32NT)
                {
                    return false;
                }
                else
                {
                    int versionMajor = osVersion.Version.Major;
                    int versionMinor = osVersion.Version.Minor;
                    // Windows 8 is version 6.2.
                    return versionMajor > 6 || versionMajor == 6 && versionMinor >= 2;
                }
            }
        }
    }
}
