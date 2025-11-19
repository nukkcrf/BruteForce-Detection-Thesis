using System.Management.Automation.Runspaces;
using Renci.SshNet;
using Renci.SshNet.Common;

using System;


/*
 SSHAttacker använder SSH.NET-biblioteket för att attackera Linux-servrar.
Processen är:
Skapa en ConnectionInfo med hostname, port, username och lösenord
Sätt timeout till 5 sekunder (viktigt för att inte hänga)
Försök ansluta med SshClient
Jag använder try-catch för att hantera olika fel:
SshAuthenticationException - perfekt! Det betyder misslyckad inloggning, vilket är vad vi vill
SshConnectionException - nätverksproblem
Andra exceptions - oväntade fel
När SSH-autentisering misslyckas skriver Linux '/var/log/auth.log' med 'Failed password' - det är vad Splunk samlar in.
Timeout är kritisk - utan den kan programmet hänga i flera minuter om nätverket är långsamt.
Metoden returnerar true när en failed login genererades framgångsrikt."
 
 */

namespace BruteForceSimulator
{
    public class SSHAttacker
    {
        public static bool SimulateAttack(string host, string username, int port)
        {
            try
            {
                string password = "WrongPassword" + new Random().Next(1000);

                var connectionInfo = new ConnectionInfo(host, port, username,
                    new PasswordAuthenticationMethod(username, password))
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };

                using (var client = new SshClient(connectionInfo))
                {
                    client.Connect();
                    client.Disconnect();
                    return true; // Shouldn't happen (successful login)
                }
            }
            catch (SshAuthenticationException)
            {
                // Failed login - this is exactly what we want for testing
                return true; // Successfully generated failed attempt
            }
            catch (SshConnectionException ex)
            {
                Console.WriteLine($"    [!] Connection error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    [!] Error: {ex.Message}");
                return false;
            }
        }
    }
}