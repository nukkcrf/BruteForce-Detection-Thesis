# BruteForce-Detection-Thesis

Brute-force attack detection using Splunk SIEM with C# attack simulator - KYH Examensarbete



\# Brute-Force Detection with Splunk SIEM



\*\*KYH Thesis Project - Cybersecurity\*\*  

&nbsp;

\*\*Status:\*\* Network Testing Complete ✅



---



\## Project Status (November 19, 2025)



\### ✅ Completed:

\- ESXi lab environment (4 VMs)

\- Splunk Enterprise deployment (VM1)

\- Splunk Universal Forwarders (VM2, VM3)

\- C# attack simulator (multi-protocol, network-capable)

\- \*\*409 SSH brute-force attempts detected successfully\*\*

\- Cross-platform log collection (Windows + Linux)

\- Detection queries operational



\###  In Progress:

\- Report writing

\- Documentation

\- Final testing scenarios



---



\## Architecture



\*\*Lab Environment:\*\*

```

VM1: Splunk SIEM Server (192.168.100.51) - Windows Server 2019

VM2: Windows Target (192.168.100.50) - Windows 11 Pro

VM3: Ubuntu Target (192.168.100.52) - Ubuntu Server

VM4: Ubuntu Target (192.168.100.54) - Ubuntu Server

```



\*\*Attack Protocols:\*\*

\- SSH (Secure Shell) - Working 

\- Windows Local (net use) - Working 

\- RDP (Remote Desktop) - Working 



---



\## Test Results



\*\*Latest Network Attack Test:\*\*

\- Target: VM3 (Ubuntu SSH)

\- Attempts: 200 (10 users × 10 attempts × 2 runs)

\- Events detected in Splunk: \*\*409\*\*

\- Detection rate: 100%

\- Average attack rate: 0.49 attempts/second



---



\## Next Steps



1\. Complete report (10-15 pages)

2\. Create architecture diagrams

3\. LinkedIn portfolio post

4\. Video demonstration (for presentation)



---



\*\*Last Updated:\*\* November 19, 2025

