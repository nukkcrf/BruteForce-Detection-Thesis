using System;
using System.Threading;



/*
 *AttackExecutor orkestrerar hela attacken.*

*Execute-metoden loopar genom:*

1. *Alla targets (maskiner)*
2. *Alla usernames*
3. *Alla försök per användare*

*För varje försök anropar den ExecuteAttack som väljer rätt attackmetod baserat på protokollet.*

*Jag använder en switch-sats för att delegera till rätt attacker-klass:*

- *WindowsLocal → WindowsAttacker*
- *SSH → SSHAttacker*
- *RDP → RDPAttacker*

*Varje lyckat genererat försök markeras med 'X', misslyckade med '.'. Detta ger visuell feedback i realtid.*

*Thread.Sleep mellan försök är viktigt - det simulerar en realistisk attack. 
*En riktig attackerare kan inte göra tusentals försök per sekund."*

 */
namespace BruteForceSimulator
{
    public class AttackExecutor
    {
        private AttackConfig _config;

        public AttackExecutor(AttackConfig config)
        {
            _config = config;
        }

        public int Execute()
        {
            int totalAttempts = 0;

            foreach (var target in _config.Targets)
            {
                Console.WriteLine($"\n[*] Attacking target: {target}");

                foreach (string username in _config.Usernames)
                {
                    Console.WriteLine($"  [*] User: {username}");

                    for (int i = 0; i < _config.AttemptsPerUser; i++)
                    {
                        bool success = ExecuteAttack(target, username);

                        if (success)
                        {
                            totalAttempts++;
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write(".");
                        }

                        Thread.Sleep(_config.DelayMs);
                    }
                    Console.WriteLine();
                }
            }

            return totalAttempts;
        }

        private bool ExecuteAttack(NetworkTarget target, string username)
        {
            switch (target.Protocol)
            {
                case AttackProtocol.WindowsLocal:
                    return WindowsAttacker.SimulateLocalAttack(target.Host, username);

                case AttackProtocol.SSH:
                    return SSHAttacker.SimulateAttack(target.Host, username, target.Port);

                case AttackProtocol.RDP:
                    return RDPAttacker.SimulateAttack(target.Host, username);

                default:
                    return false;
            }
        }
    }
}