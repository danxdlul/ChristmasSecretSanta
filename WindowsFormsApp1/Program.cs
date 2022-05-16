using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            bool isRunning;
            var mutex = new Mutex(true, "{7855C0FE-8FBD-46FE-84D0-44FF8B37F466}");
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form1 = new Form1();

                try
                {
                    Application.Run(form1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during runtime!" + Environment.NewLine + $"{ex.Data}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (form1.FileIsLocked())
                        form1.UnlockListFile();

                }

            }
            else
            {
                MessageBox.Show("Program is already running!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var proc = Process.GetCurrentProcess();
            string fullPath = proc.MainModule.FileName;
            isRunning = Helper.ProgramIsRunning(fullPath);
            if (!isRunning)
            {
                mutex.ReleaseMutex();
            }

        }
    }
}