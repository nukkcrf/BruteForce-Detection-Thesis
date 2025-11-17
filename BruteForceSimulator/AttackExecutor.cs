using System;
using System.Threading;

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