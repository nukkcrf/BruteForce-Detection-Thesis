using System;

namespace BruteForceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Distributed Brute-Force Attack Simulator ===\n");

            // Parse arguments and create configuration
            AttackConfig config = ArgumentParser.Parse(args);

            // Display configuration
            config.DisplayConfiguration();

            // Wait for user confirmation
            Console.WriteLine("\nPress any key to start attack simulation...");
            Console.ReadKey();
            Console.WriteLine();

            // Execute attack
            DateTime startTime = DateTime.Now;
            var executor = new AttackExecutor(config);
            int totalAttempts = executor.Execute();
            DateTime endTime = DateTime.Now;

            // Display results
            DisplayResults(totalAttempts, startTime, endTime);
        }

        static void DisplayResults(int totalAttempts, DateTime start, DateTime end)
        {
            Console.WriteLine($"\n{new string('=', 50)}");
            Console.WriteLine($"[+] Attack simulation complete!");
            Console.WriteLine($"[+] Total attempts: {totalAttempts}");
            Console.WriteLine($"[+] Duration: {(end - start).TotalSeconds:F2} seconds");

            double duration = (end - start).TotalSeconds;
            if (duration > 0)
            {
                Console.WriteLine($"[+] Rate: {totalAttempts / duration:F2} attempts/second");
            }

            Console.WriteLine($"\n[*] Check Splunk with:");
            Console.WriteLine($"    index=windows_security OR index=linux_auth");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}