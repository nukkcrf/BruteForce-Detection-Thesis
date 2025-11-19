using System;



/* argumentparser hanterar kommandoradsargument för att konfigurera attacken.*
 
 
 */
namespace BruteForceSimulator
{
    public class ArgumentParser
    {
        public static AttackConfig Parse(string[] args)
        {
            var config = new AttackConfig();
            config.AddDefaultTargets(); // localhost as default

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--target":
                    case "-t":
                        if (i + 1 < args.Length)
                        {
                            config.Targets.Clear();
                            ParseSingleTarget(args[++i], config);
                        }
                        break;

                    case "--targets":
                        if (i + 1 < args.Length)
                        {
                            config.Targets.Clear();
                            ParseMultipleTargets(args[++i], config);
                        }
                        break;

                    case "-a":
                    case "--attempts":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int attempts))
                        {
                            config.AttemptsPerUser = attempts;
                        }
                        break;

                    case "-d":
                    case "--delay":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int delay))
                        {
                            config.DelayMs = delay;
                        }
                        break;

                    case "-m":
                    case "--mode":
                        if (i + 1 < args.Length && Enum.TryParse(args[++i], true, out AttackMode mode))
                        {
                            config.Mode = mode;
                        }
                        break;
                }
            }

            return config;
        }

        private static void ParseSingleTarget(string targetStr, AttackConfig config)
        {
            var parts = targetStr.Split(':');
            string host = parts[0];
            AttackProtocol protocol = parts.Length > 1 && parts[1].ToLower() == "ssh"
                ? AttackProtocol.SSH
                : AttackProtocol.RDP;

            config.Targets.Add(new NetworkTarget(host, protocol));
        }

        private static void ParseMultipleTargets(string targetsStr, AttackConfig config)
        {
            string[] targets = targetsStr.Split(',');

            foreach (var target in targets)
            {
                ParseSingleTarget(target.Trim(), config);
            }
        }
    }
}