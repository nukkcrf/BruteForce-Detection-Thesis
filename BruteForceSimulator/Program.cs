using System;
using System.Management.Automation;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace BruteForceSimulator
{
    class AttackConfig
    {
        public string Target { get; set; } = "localhost";
        public List<string> Usernames { get; set; }
        public int AttemptsPerUser { get; set; } = 5;
        public int DelayMs { get; set; } = 1000;
        public AttackMode Mode { get; set; } = AttackMode.Sequential;
    }

    enum AttackMode
    {
        Sequential,  // One user at a time
        Spray,       // All users, then repeat
        Random       // Random order
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Brute-Force Attack Simulator ===\n");

            AttackConfig config = ParseArguments(args);

            Console.WriteLine($"Target: {config.Target}");
            Console.WriteLine($"Mode: {config.Mode}");
            Console.WriteLine($"Users: {string.Join(", ", config.Usernames)}");
            Console.WriteLine($"Attempts per user: {config.AttemptsPerUser}");
            Console.WriteLine($"Delay: {config.DelayMs}ms\n");

            if (!IsRunAsAdmin())
            {
                Console.WriteLine("[!] WARNING: Not running as Administrator!");
                Console.WriteLine("[!] Some features may not work.\n");
            }

            Console.WriteLine("Press any key to start attack simulation...");
            Console.ReadKey();
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            int totalAttempts = RunAttackSimulation(config);
            DateTime endTime = DateTime.Now;

            Console.WriteLine($"\n{'=',-50}");
            Console.WriteLine($"[+] Attack simulation complete!");
            Console.WriteLine($"[+] Total attempts: {totalAttempts}");
            Console.WriteLine($"[+] Duration: {(endTime - startTime).TotalSeconds:F2} seconds");
            Console.WriteLine($"[+] Rate: {totalAttempts / (endTime - startTime).TotalSeconds:F2} attempts/second");
            Console.WriteLine($"\n[*] Check Splunk with:");
            Console.WriteLine($"    index=* EventCode=4625 earliest=-5m");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static AttackConfig ParseArguments(string[] args)
        {
            var config = new AttackConfig
            {
                Usernames = new List<string>
                {
                    "admin", "administrator", "user", "test", "root",
                    "guest", "support", "backup", "service", "operator"
                }
            };

            // Simple argument parsing
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-t":
                    case "--target":
                        if (i + 1 < args.Length)
                            config.Target = args[++i];
                        break;

                    case "-a":
                    case "--attempts":
                        if (i + 1 < args.Length)
                        {
                            if (int.TryParse(args[++i], out int attempts))
                            {
                                config.AttemptsPerUser = attempts;
                            }
                        }
                        break;

                    case "-d":
                    case "--delay":
                        if (i + 1 < args.Length)
                        {
                            if (int.TryParse(args[++i], out int delay))
                            {
                                config.DelayMs = delay;
                            }
                        }
                        break;

                    case "-m":
                    case "--mode":
                        if (i + 1 < args.Length)
                        {
                            if (Enum.TryParse(args[++i], true, out AttackMode mode))
                            {
                                config.Mode = mode;
                            }
                        }
                        break;
                }
            }
      
            return config;
        }

        static int RunAttackSimulation(AttackConfig config)
        {
            int totalAttempts = 0;

            switch (config.Mode)
            {
                case AttackMode.Sequential:
                    totalAttempts = RunSequentialAttack(config);
                    break;
                case AttackMode.Spray:
                    totalAttempts = RunSprayAttack(config);
                    break;
                case AttackMode.Random:
                    totalAttempts = RunRandomAttack(config);
                    break;
            }

            return totalAttempts;
        }

        static int RunSequentialAttack(AttackConfig config)
        {
            // Traditional brute-force: exhaust one user, then move to next
            int total = 0;

            foreach (string username in config.Usernames)
            {
                Console.WriteLine($"\n[*] Attacking user: {username}");

                for (int i = 0; i < config.AttemptsPerUser; i++)
                {
                    if (SimulateFailedLogin(config.Target, username))
                    {
                        total++;
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(".");
                    }

                    Thread.Sleep(config.DelayMs);
                }
            }

            return total;
        }

        static int RunSprayAttack(AttackConfig config)
        {
            // Password spray: try all users with same password, then repeat
            int total = 0;

            for (int attempt = 0; attempt < config.AttemptsPerUser; attempt++)
            {
                Console.WriteLine($"\n[*] Spray round {attempt + 1}/{config.AttemptsPerUser}");

                foreach (string username in config.Usernames)
                {
                    if (SimulateFailedLogin(config.Target, username))
                    {
                        total++;
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(".");
                    }

                    Thread.Sleep(config.DelayMs);
                }
            }

            return total;
        }

        static int RunRandomAttack(AttackConfig config)
        {
            // Random: unpredictable pattern
            int total = 0;
            int totalAttempts = config.Usernames.Count * config.AttemptsPerUser;
            var random = new Random();

            Console.WriteLine($"\n[*] Running random attack pattern");

            for (int i = 0; i < totalAttempts; i++)
            {
                string username = config.Usernames[random.Next(config.Usernames.Count)];

                if (SimulateFailedLogin(config.Target, username))
                {
                    total++;
                    Console.Write("X");
                }
                else
                {
                    Console.Write(".");
                }

                if ((i + 1) % 50 == 0)
                    Console.WriteLine($" [{i + 1}/{totalAttempts}]");

                Thread.Sleep(config.DelayMs);
            }

            return total;
        }

        static bool SimulateFailedLogin(string host, string username)
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

        static bool IsRunAsAdmin()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
    }
}