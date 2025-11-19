using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 AttackProtocol *beskriver vilken typ av attack vi utför:*

- *WindowsLocal - attacker mot lokala maskinen med net use*
- *RDP - Remote Desktop Protocol attacker*
- *SSH - Secure Shell attacker mot Linux-servrar*

**AttackMode** *beskriver hur attacken utförs:*

- *Sequential - traditionell brute-force, en användare i taget*
- *Spray - password spraying, alla användare med samma lösenord*
- *Random - slumpmässigt mönster*
- *Distributed - attackera flera mål samtidigt*

*Genom att använda enums istället för strängar får jag type-safety och undviker stavfel."*
 
 */

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