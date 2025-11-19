using System;
using System.Management.Automation;



/*
 WindowsAttacker hanterar attacker mot Windows-maskiner via nätverksautentisering.

*Jag genererar ett slumpmässigt felaktigt lösenord för varje försök - 'WrongPassword' plus ett random nummer.*

*När net use misslyckas genereras Event ID 4625 i Windows Security Log - det är exakt det Splunk letar efter.*

*Metoden returnerar true om kommandot kördes (även om det misslyckades), false bara om det blev ett tekniskt fel.*

*Det smarta är att vi använder Windows egna autentiseringsmekanism, så loggarna blir helt realistiska."*
 
 */
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