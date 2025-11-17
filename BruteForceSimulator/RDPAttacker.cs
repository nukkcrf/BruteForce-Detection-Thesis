using System;
using System.Management.Automation;

namespace BruteForceSimulator
{
    public class RDPAttacker
    {
        public static bool SimulateAttack(string host, string username)
        {
            try
            {
                string password = "WrongPassword" + new Random().Next(1000);

                // Attempt RDP connection via PowerShell
                string psCommand = $"cmdkey /generic:{host} /user:{username} /pass:{password}; " +
                                 $"cmdkey /delete:{host}";

                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddScript(psCommand);
                    ps.Invoke();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    [!] RDP error: {ex.Message}");
                return false;
            }
        }
    }
}