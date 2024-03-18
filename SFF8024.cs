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
                default: s = ""; break;
            }
            return s;
        }
        public static String connector_type(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x01: s = "SC"; break;
                case 0x07: s = "LC"; break;
                case 0x08: s = "MTRJ"; break;
                case 0x0c: s = "MPO 1x12"; break;
                case 0x0b: s = "Optical Pigtail"; break;
                case 0x0d: s = "MPO 2x16"; break;
                case 0x22: s = "RJ45"; break;
                case 0x23: s = "No separable"; break;
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
                case 0x09: s = "(40GBASE-LR)4"; break;
                case 0x0a: s = "(40GBASE-FR)"; break;
                case 0x0b: s = "(50GBASE-FR)"; break;
                case 0x0c: s = "(50GBASE-LR)"; break;
                case 0x0d: s = "(100GBASE-LR4)"; break;
                case 0x0e: s = "(100GBASE-ER4)"; break;
                case 0x0f: s = "(100G PSM4)"; break;
                case 0x34: s = "(100G CWDM4-OCP)"; break;
                case 0x10: s = "(100G CWDM4)"; break;
                case 0x11: s = "(100G 4WDM-10)"; break;
                case 0x12: s = "(100G 4WDM-20)"; break;
                case 0x13: s = "(100G 4WDM-40)"; break;
                case 0x14: s = "(100GBASE-DR)"; break;
                case 0x15: s = "(100GGBASE-FR1)"; break;
                case 0x16: s = "(100GGBASE-LR1)"; break;
                case 0x17: s = "(200GGBASE-DR4)"; break;
                case 0x41: s = "(200GBASE-ER4)"; break;
                case 0x42: s = "(400GBASE-ER8)"; break;
                case 0x1c: s = "(400GBASE-DR4)"; break;
                case 0x1d: s = "(400GBASE FR4)"; break;
                case 0x43: s = "(400GBASE-LR4-6)"; break;
                case 0x1e: s = "(400GBASE-LR4-10)"; break;
                case 0x4d: s = "(400GBASE-ZR)"; break;
                case 0x56: s = "(800GBASE-DR8)"; break;
                case 0x57: s = "(800GBASE-DR8-2)"; break;
                default: s = ""; break;
             }
            return s;
        }
        // sff8024_table Table 4 - 5 Host Electrical Interface IDs
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
                case 0x1b: s = "(200GBASE-SR2)"; break;
                case 0x1e: s = "(200GBASE-VR2)"; break;
                case 0x0f: s = "(400GBASE-SR16)"; break;
                case 0x10: s = "(400GBASE-SR8)"; break;
                case 0x11: s = "(400GBASE-SR4)"; break;
                case 0x1f: s = "(400GBASE-VR4)"; break;
                case 0x12: s = "(800GBASE-SR8)"; break;
                case 0x20: s = "(800GBASE-VR8)"; break;
                case 0x1a: s = "(400GBASE-SR4.2)"; break;
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
