using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace SecureLoaderWF
{
    static class Program
    {
        [DllImport(@"C:\\Users\\Master\\source\\repos\\SecureLoader\\Release\\SecureLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        static public extern IntPtr CreateSession(IntPtr server_host, int server_port);

        [DllImport(@"C:\\Users\\Master\\source\\repos\\SecureLoader\\Release\\SecureLoader.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static public extern void DisposeSession(IntPtr pClassNameObject);

        [DllImport(@"C:\\Users\\Master\\source\\repos\\SecureLoader\\Release\\SecureLoader.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static public extern int CallSessionLogin(IntPtr pClassNameObject, IntPtr username, IntPtr hash);

        [DllImport(@"C:\\Users\\Master\\source\\repos\\SecureLoader\\Release\\SecureLoader.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static public extern void __GetValue__(StringBuilder str, int strlen);

        static public IntPtr pSession;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
