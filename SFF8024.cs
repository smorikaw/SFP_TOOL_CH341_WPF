using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP_TOOL_CH341
{
    internal class SFF8024
    {
        ///===================================== SFF-8024 class
        //
        // SFF8024 Table 4-1 Identifier Values
        // 
        public static String ident(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "GBIC"; break;
                case 0x03: s = "SFP/SFP+/SFP28"; break;
                case 0x05: s = "XENPAK"; break;
                case 0x06: s = "XFP"; break;
                case 0x0a: s = "X2"; break;
                case 0x0b: s = "DWDM-SFP"; break;
                case 0x0d: s = "QSFP+"; break;
                case 0x11: s = "QSFP28"; break;
                case 0x12: s = "CXP2"; break;
                case 0x17: s = "microQSFP"; break;
                case 0x18: s = "QSFP-DD"; break;
                case 0x19: s = "OSFP"; break;
                case 0x1a: s = "SFP-DD"; break;
                case 0x1b: s = "DSFP"; break;
                case 0x1e: s = "QSFP(CMIS)"; break;
                case 0x1f: s = "SFP-DD(CMIS)"; break;
                case 0x20: s = "SFP+(CMIS)"; break;
                case 0x21: s = "OSFP-XD(CMIS)"; break;
                case 0x22: s = "OIF-ELSFP(CMIS)"; break;
                case 0x23: s = "CDFP (x4 PCIe)"; break;
                case 0x24: s = "CDFP (x8 PCIe)"; break;
                case 0x25: s = "CDFP (x16 PCIe)"; break;
                default: s = ""; break;
            }
            return s;
        }
        // Table 4-3 Connector Types
        public static String connector_type(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "SC"; break;
                case 0x02: s = "FC style1"; break;
                case 0x03: s = "FC style2"; break;
                case 0x04: s = "BNC"; break;
                case 0x05: s = "FC coax"; break;
                case 0x07: s = "LC"; break;
                case 0x08: s = "MTRJ"; break;
                case 0x0c: s = "MPO 1x12"; break;
                case 0x0b: s = "Optical Pigtail"; break;
                case 0x0d: s = "MPO 2x16"; break;
                case 0x21: s = "Copper pigtail"; break;
                case 0x22: s = "RJ45"; break;
                case 0x23: s = "No separable"; break;
                case 0x24: s = "MXC 2x16"; break;
                case 0x25: s = "CS"; break;
                case 0x26: s = "SN"; break;
                case 0x27: s = "MPO 2x12"; break;
                case 0x28: s = "MPO 1x16"; break;
                default: s = "";    break;
            }
            return s;
        }
        // SFF8024 tabel 4-4
        public static String exttype(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x00: s = ""; break;
                case 0x01: s = "(AOC 5E-5)"; break;
                case 0x02: s = "(100G SR4/25G SR)"; break;
                case 0x03: s = "(100G LR4/25G LR)"; break;
                case 0x04: s = "(100G ER4/25G ER)"; break;
                case 0x06: s = "(100G CWDM4)"; break;
                case 0x07: s = "(100G PSM4)"; break;
                case 0x0b: s = "(100G DAC/25G DAC CA-L)"; break;
                case 0x0c: s = "(25G DAC CA-S)"; break;
                case 0x0d: s = "(25G DAC CA-N)"; break;
                case 0x10: s = "(40G ER4)"; break;
                case 0x12: s = "(40G PSM4)"; break;
                case 0x16: s = "(10G T)"; break;
                case 0x18: s = "(100G AOC/25G AOC 10E-12)"; break;
                case 0x1c: s = "(10G T 30m)"; break;
                case 0x1d: s = "(5G T)"; break;
                case 0x1e: s = "(2.5G T)"; break;
                case 0x22: s = "(100G 4WDM-10)"; break;
                case 0x25: s = "(100G DR)"; break;
                case 0x26: s = "(100G FR)"; break;
                case 0x27: s = "(100G LR)"; break;
                // no FEC on host interface(QSFP28)
                case 0x28: s = "(100GBASE-SR1)"; break;
                case 0x3a: s = "(100GBASE-VR1)"; break;
                    // FEC host
                case 0x29: s = "(100GBASE-SR1)"; break;
                case 0x36: s = "(100GBASE-VR1)"; break;
                case 0x2a: s = "(100GBASE-FR1)"; break;
                case 0x2b: s = "(100GBASE-LR1)"; break;
                case 0x2c: s = "(100GBASE-LR1-20)"; break;
                case 0x2d: s = "(100GBASE-ER1-30)"; break;
                case 0x2e: s = "(100GBASE-ER1-40)"; break;
                case 0x2f: s = "(100GBASE-LR1-20)"; break;
                case 0x34: s = "(100GBASE-ER1-30)"; break;
                case 0x35: s = "(100GBASE-ER1-40)"; break;
                case 0x30: s = "(ACC 1E-6)"; break;
                case 0x31: s = "(AOC 1E-6)"; break;
                case 0x32: s = "(ACC 2.6E-5)"; break;
                case 0x33: s = "(AOC 2.6E-5)"; break;
                    // 50G
                case 0x40: s = "(50GBASE-CR)"; break;
                case 0x41: s = "(50GBASE-SR)"; break;
                case 0x42: s = "(50GBASE-FR)"; break;
                case 0x45: s = "(50GBASE-LR)"; break;
                case 0x4a: s = "(50GBASE-ER)"; break;
                    // 200G
                case 0x43: s = "(200GBASE-FR4)"; break;
                case 0x46: s = "(200GBASE-LR4)"; break;
                    // 400G
                case 0x47: s = "(400GBASE-DR4)"; break;
                case 0x48: s = "(400GBASE-FR4)"; break;
                case 0x49: s = "(400GBASE-LR4-6)"; break;
                case 0x4b: s = "(400G-LR4-10)"; break;
                case 0x4c: s = "(400GBASE-ZR)"; break;

                default: s = ""; break;
            }
            return s;
        }
        // sff8024_table Table 4 - 5 Host Electrical Interface IDs
        public static String tech(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x00: s = "(850 nm VCSEL)"; break;
                case 0x10: s = "(1310 nm VCSEL)"; break;
                case 0x20: s = "(1550 nm VCSEL)"; break;
                case 0x40: s = "(1310 nm DFB)"; break;
                case 0x44: s = "(1310 nm DFB/Cooled)"; break;
                case 0x45: s = "(1310 nm DFB/Cooled/Tunable)"; break;

                case 0x48: s = "(1310 nm DFB/Active CTL)"; break;
                case 0x4a: s = "(1310 nm DFB/Active CTL/APD)"; break;
                case 0x64: s = "(1310 nm EML/Cooled)"; break;
                case 0x68: s = "(1310 nm EML/Active CTL)"; break;
                case 0x6a: s = "(1310 nm EML/Active CTL/APD)"; break;
                default: s = ""; break;
            }
            return s;
        }
        // sff8024_table Table 4 - 5 Host Electrical Interface IDs
        public static String ehint(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "(1000BASE-CX)"; break;
                case 0x02: s = "(XAUI)"; break;
                case 0x03: s = "(XFI)"; break;
                case 0x0a: s = "(50GAUI-1 C2M)"; break;
                case 0x0b: s = "(CAUI-4 C2M)"; break;
                case 0x41: s = "(CAUI-4 C2M wo FEC)"; break;
                case 0x42: s = "(CAUI-4 C2M w RS FEC)"; break;
                case 0x0c: s = "(100GAUI-4 C2M)"; break;
                case 0x0d: s = "(100GAUI-2 C2M)"; break;
                case 0x4b: s = "(100GAUI-1-S C2M)"; break;
                case 0x4c: s = "(100GAUI-1-L C2M)"; break;
                case 0x0e: s = "(200GAUI-8 C2M)"; break;
                case 0x0f: s = "(200GAUI-4 C2M)"; break;
                case 0x4d: s = "(200GAUI-2-S C2M)"; break;
                case 0x4e: s = "(200GAUI-2-L C2MM)"; break;
                case 0x10: s = "(400GAUI-16 C2M)"; break;
                case 0x11: s = "(400GAUI-8 C2M)"; break;
                case 0x4f: s = "(400GAUI-4-S C2M)"; break;
                case 0x50: s = "(400GAUI-4-L C2M)"; break;
                case 0x2c: s = "(IB SDR)"; break;
                case 0x2d: s = "(IB DDR)"; break;
                case 0x2e: s = ")IB QDR)"; break;
                case 0x2f: s = "(IB FDR)"; break;
                case 0x30: s = "(IB EDR)"; break;
                case 0x31: s = "(IB HDR)"; break;
                case 0x32: s = "(IB NDR)"; break;
                case 0x51: s = "(800G S C2M)"; break;
                case 0x52: s = "(800G L C2M)"; break;
                case 0x53: s = "(OTL4.2)"; break;
                case 0x55: s = "(1.6TAUI-16-S C2M)"; break;
                case 0x56: s = "(1.6TAUI-16-L C2M)"; break;
                case 0x70: s = "(PCIe 4.0)"; break;
                case 0x71: s = "(PCIe 5.0)"; break;
                case 0x72: s = "(PCIe 6.0)"; break;
                case 0x73: s = "(PCIe 7.0)"; break;
                case 0x74: s = "(CEI-112G-LINEAR-PAM4)"; break;
                case 0x82: s = "(800GAUI-4)"; break;
                case 0x83: s = "(1.6TAUI-8)"; break;
                default: s = ""; break;

            }
            return s;
        }
        // sff8024_table Table 4 - 5 Host Electrical Interface IDs
        public static String smfint(int code)
        {
            string s = "";
            switch (code)
            {
                case 0x01: s = "(10GBASE-LW)"; break;
                case 0x02: s = "(10GBASE-EW)"; break;
                case 0x03: s = "(10G-ZW)"; break;
                case 0x04: s = "(110GBASE-L)R"; break;
                case 0x05: s = "(10GBASE-ER)"; break;
                case 0x4e: s = "(10GBASE-BR)"; break;
                case 0x06: s = "(10GBASE-ZR)"; break;
                case 0x07: s = "(25GBASE-LR)"; break;
                case 0x08: s = "(25GBASE-ER)"; break;
                case 0x4f: s = "(25GBASE-BR)"; break;
                case 0x09: s = "(40GBASE-LR4)"; break;
                case 0x0a: s = "(40GBASE-FR)"; break;
                case 0x0b: s = "(50GBASE-FR)"; break;
                case 0x0c: s = "(50GBASE-LR)"; break;
                case 0x40: s = "(50GBASE-ER)"; break;
                case 0x50: s = "(50GBASE-BR)"; break;
                // 100G
                case 0x0d: s = "(100GBASE-LR4)"; break;
                case 0x0e: s = "(100GBASE-ER4)"; break;
                case 0x0f: s = "(100G PSM4)"; break;
                case 0x10: s = "(100G CWDM4)"; break;
                case 0x11: s = "(100G 4WDM-10)"; break;
                case 0x12: s = "(100G 4WDM-20)"; break;
                case 0x13: s = "(100G 4WDM-40)"; break;
                case 0x14: s = "(100GBASE-DR)"; break;
                case 0x15: s = "(100GBASE-FR1)"; break;
                case 0x16: s = "(100GBASE-LR1)"; break;
                case 0x4a: s = "(100G-LR1-20)"; break;
                case 0x4b: s = "(100G-ER1-30)"; break;
                case 0x4c: s = "(100G-ER1-40)"; break;
                case 0x44: s = "(100GBASE-ZR)"; break;
                    // 200G
                case 0x17: s = "(200GBASE-DR4)"; break;
                case 0x18: s = "(200GBASE-FR4)"; break;
                case 0x19: s = "(200GBASE-LR4)"; break;
                case 0x41: s = "(200GBASE-ER4)"; break;
                case 0x73: s = "(200GBASE-DR1)"; break;
                case 0x74: s = "(200GBASE-DR1-2)"; break;
                // 400G
                case 0x42: s = "(400GBASE-ER8)"; break;
                case 0x1c: s = "(400GBASE-DR4)"; break;
                case 0x1a: s = "(400GBASE-FR8)"; break;
                case 0x1b: s = "(400GBASE-LR8)"; break;
                case 0x1d: s = "(400GBASE-FR4)"; break;
                case 0x34: s = "(100G CWDM4-OCP)"; break;
                // OIF
                case 0x3e: s = "(400ZR)"; break;
                case 0x3f: s = "(400ZR Unamplified)"; break;
                case 0x6c: s = "(800ZR-A)"; break;
                case 0x6d: s = "(800ZR-B)"; break;
                case 0x6e: s = "(800ZR-C)"; break;
                case 0x43: s = "(400GBASE-LR4-6)"; break;
                case 0x1e: s = "(400GBASE-LR4-10)"; break;
                case 0x4d: s = "(400GBASE-ZR)"; break;
                case 0x55: s = "(400GBASE-DR4-2"; break;
                case 0x56: s = "(800GBASE-DR8)"; break;
                case 0x57: s = "(800GBASE-DR8-2)"; break;
                case 0x6f: s = "(400G-ER4-30)"; break;
                case 0x75: s = "(400GBASE-DR2)"; break;
                case 0x76: s = "(400GBASE-DR2-2)"; break;
                    // 800G
                case 0x77: s = "(800GBASE-DR4)"; break;
                case 0x78: s = "(800GBASE-DR4-2)"; break;
                case 0x79: s = "(800GBASE-FR4-500)"; break;
                case 0x7a: s = "(800GBASE-FR4)"; break;
                case 0x7b: s = "(800GBASE-LR4)"; break;
                case 0x7c: s = "(800GBASE-LR1)"; break;
                case 0x7d: s = "(800GBASE-ER1-20)"; break;
                case 0x7e: s = "(800GBASE-ER1)"; break;
                    // 1.6T
                case 0x7f: s = "(1.6TBASE-DR8)"; break;
                case 0x80: s = "(1.6TBASE-DR8-2)"; break;
                // OpenZR+
                case 0x46: s = "(ZR400-OFEC-16QAM)"; break;
                case 0x35: s = "(ZR400-OFEC-16QAM-HA)"; break;
                case 0x36: s = "(ZR400-OFEC-16QAM-HB)"; break;
                case 0x37: s = "(ZR400-OFEC-8QAM-HA)"; break;
                case 0x58: s = "(ZR400-OFEC-8QAM-HB)"; break;
                default: s = ""; break;
             }
            return s;
        }
        // sff8024_table Table 4 - 5 Host Electrical Interface IDs
        //  Rev 4.12 9-Jul-24 Added code 74h and codes 81h-83h (200G/lane) in Table 4-5, modified code 80h
        public static String mmfint(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "(10GBASE-SW)"; break;
                case 0x02: s = "(10GBASE-SR)"; break;
                case 0x03: s = "(25GBASE-SR)"; break;
                case 0x04: s = "(40GBASE-SR4)"; break;
                case 0x05: s = "(40GBASE SWDM4)"; break;
                case 0x06: s = "(40G BiDi)"; break;
                case 0x07: s = "(50GBASE-SR)"; break;
                case 0x08: s = "(100GBASE-SR10"; break;
                case 0x09: s = "(100GBASE-SR4)"; break;
                case 0x0a: s = "(100G SWDM4)"; break;
                case 0x0b: s = "(100G BiDi)"; break;
                case 0x0c: s = "(100GBASE-SR2)"; break;
                case 0x0d: s = "(100GBASE-SR1)"; break;
                case 0x1d: s = "(100GBASE-VR1)"; break;
                case 0x0e: s = "(200GBASE-SR4)"; break;
                case 0x1a: s = "(400GBASE-SR4.2)"; break;
                case 0x1b: s = "(200GBASE-SR2)"; break;
                case 0x1e: s = "(200GBASE-VR2)"; break;
                case 0x0f: s = "(400GBASE-SR16)"; break;
                case 0x10: s = "(400GBASE-SR8)"; break;
                case 0x11: s = "(400GBASE-SR4)"; break;
                case 0x1f: s = "(400GBASE-VR4)"; break;
                case 0x12: s = "(800GBASE-SR8)"; break;
                case 0x20: s = "(800GBASE-VR8)"; break;
                case 0x21: s = "(800G-VR4.2)"; break;
                case 0x22: s = "(800G-SR4.2)"; break;
                case 0x23: s = "(1.6T-VR8.2)"; break;
                case 0x24: s = "(1.6T-SR8.2"; break;

                default: s = ""; break;
              }
            return s;
        }

        // sff8024_table Table 4-8 Passive and Linear Active Copper Cable and Passive Loopback media interface codes
        public static String dacint(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "(Copper cable)"; break;
                case 0xbf: s = "(Passive Loopback module)"; break;
                default: s = ""; break;
            }
            return s;
        }
        // sff8024_table Table 4-9 Limiting and Retimed Active Cable assembly and Active Loopback media interface codes
        public static String aocint(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "(Active Cable assembly with BER < 10-12)"; break;
                case 0x02: s = "(Active Cable assembly with BER < 5x10-5)"; break;
                case 0x03: s = "(Active Cable assembly with BER < 2.6x10-4)"; break;
                case 0x04: s = "(Active Cable assembly with BER < 10-6)"; break;
                case 0xbf: s = "(Active Loopback module)"; break;
                default: s = ""; break;
            }
            return s;
        }
        // sff8024_table Table 4-10 BASE-T media interface codes
        public static String utpint(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "(1000BASE-T)"; break;
                case 0x02: s = "(2.5GBASE-T)"; break;
                case 0x03: s = "(5GBASE-T)"; break;
                case 0x04: s = "(10GBASE-T)"; break;
                case 0x05: s = "(25GBASE-T)"; break;
                case 0x06: s = "(40GBASE-T)"; break;
                case 0x07: s = "(50GBASE-T)"; break;
                default: s = ""; break;
            }
            return s;
        }
        public static String optype(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "MMF"; break;
                case 0x02: s = "SMF"; break;
                default: s = ""; break;
            }
            return s;
        }

    }
}
