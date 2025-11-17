using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceSimulator
{
    public enum AttackProtocol
    {
        WindowsLocal,
        RDP,
        SSH
    }

    public enum AttackMode
    {
        Sequential,
        Spray,
        Random,
        Distributed
    }
}