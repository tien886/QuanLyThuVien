using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper
{
    public static class KnownFolders
    {
        private static readonly Guid DownloadsFolderGuid =
            new("374DE290-123F-4565-9164-39C4925E467B");

        [DllImport("shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken,
            out IntPtr ppszPath);

        public static string GetDownloadPath()
        {
            IntPtr outPath;
            SHGetKnownFolderPath(DownloadsFolderGuid, 0, IntPtr.Zero, out outPath);
            string path = Marshal.PtrToStringUni(outPath);
            Marshal.FreeCoTaskMem(outPath);
            return path;
        }
    }
}
