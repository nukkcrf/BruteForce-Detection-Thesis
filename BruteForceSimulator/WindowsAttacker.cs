using System;
using System.Management.Automation;

namespace BruteForceSimulator
{
    public class WindowsAttacker
    {
        public static bool SimulateLocalAttack(string host, string username)
        {
            try
            {
                string password = "WrongPassword" + new Random().Next(1000);
                string psCommand = $"net use \\\\{host}\\IPC$ /user:{username} {password} 2>&1";

                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddScript(psCommand);
                    ps.Invoke();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}