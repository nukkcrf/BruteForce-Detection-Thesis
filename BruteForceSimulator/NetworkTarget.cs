using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceSimulator
{
    public class NetworkTarget
    {
        public string Host { get; set; }
        public AttackProtocol Protocol { get; set; }
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;

        public NetworkTarget(string host, AttackProtocol protocol)
        {
            Host = host;
            Protocol = protocol;
            Port = protocol == AttackProtocol.SSH ? 22 : 3389;
        }
        public override string ToString()
        {
            return $"{Host}:{Port} ({Protocol})";

        }
    }
}