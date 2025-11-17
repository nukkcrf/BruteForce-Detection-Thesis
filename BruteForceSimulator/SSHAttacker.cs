using System.Management.Automation.Runspaces;
using Renci.SshNet;
using Renci.SshNet.Common;

using System;

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