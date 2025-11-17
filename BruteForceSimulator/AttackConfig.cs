using BruteForceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceSimulator
{ 
    public class AttackConfig
    {
        public List<NetworkTarget> Targets { get; set; }
        public List<string> Usernames { get; set; }
        public int AttemptsPerUser { get; set; } = 5;
        public int DelayMs { get; set; } = 1000;
        public AttackMode Mode { get; set; } = AttackMode.Sequential;

        public AttackConfig()
        {
            Targets = new List<NetworkTarget>();
            Usernames = new List<string>
            {
                "admin", "administrator", "user", "test", "root", "guest", "support", "backup", "service", "operator"
            };
            AttemptsPerUser = 5;    
            DelayMs = 1000;
            Mode = AttackMode.Sequential;
        }

        public void AddDefaultTargets()
        {
            Targets.Add(new NetworkTarget("localhost", AttackProtocol.WindowsLocal));
        }

        public void DisplayConfiguration()
        {
            System.Console.WriteLine($"Mode: {Mode}");
            System.Console.WriteLine($"Targets ({Targets.Count}):");
            foreach (var target in Targets)
            {
                System.Console.WriteLine($"  - {target}");
            }
            System.Console.WriteLine($"Users: {string.Join(", ", Usernames)}");
            System.Console.WriteLine($"Attempts per user: {AttemptsPerUser}");
            System.Console.WriteLine($"Delay: {DelayMs}ms");
        }
    }
}