using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace SFP_TOOL_CH341
{
    internal class SFF8472
    {
        public static String type(ref byte[] data)
        {
            int v = 0;
            String s = "";

            s = SFF8024.exttype(data[36]);
           
            v = data[3];
            if ((0b_1000_0000 & v) != 0) { s += "10G-ER/"; }
            if ((0b01000000 & v) != 0) s += "10G-LRM/";
            if ((0b00100000 & v) != 0) s += "10G-LR/";
            if ((0b00010000 & v) != 0) s += "10G-SR/";
            if ((0b00001000 & v) != 0) s += "IB 1X SX/";
            if ((0b10000000 & v) != 0) s += "IB 1X LX/";
            if ((0b10000000 & v) != 0) s += "1B 1X ACC/";
            if ((0b10000000 & v) != 0) s += "1B 1X DAC/";
            // byte 4, 5 ESCON SONET
            v = data[6];
            if ((0b_1000_0000 & v) != 0) s += "BASE-PX/";
            if ((0b_0100_0000 & v) != 0) s += "BASE-BX10/";
            if ((0b_0010_0000 & v) != 0) s += "100BASE-FX/";
            if ((0b_0001_0000 & v) != 0) s += "100BASE-LX/";
            if ((0b_0000_1000 & v) != 0) s += "1000BASE-T/";
            if ((0b_0000_0100 & v) != 0) s += "1000BASE-CX/";
            if ((0b_0000_0010 & v) != 0) s += "1000BASE-LX/";
            if ((0b_0000_0001 & v) != 0) s += "1000BASE-SX/";
            // byte 7,8,9,10 FC
            v = data[7];
            if ((0b_1000_0000 & v) != 0) s += "FC-V/";
            if ((0b_0100_0000 & v) != 0) s += "FC-S/";
            if ((0b_0010_0000 & v) != 0) s += "FC-I/";
            if ((0b_0001_0000 & v) != 0) s += "FC-L/";
            if ((0b_0000_1000 & v) != 0) s += "FC-M/";
            if ((0b_0000_0100 & v) != 0) s += "FC-SA/";
            if ((0b_0000_0010 & v) != 0) s += "FC-LC/";
            if ((0b_0000_0001 & v) != 0) s += "FC-EL/";
            v = data[8];
            if ((0b_1000_0000 & v) != 0) s += "FC-EL/";
            if ((0b_0100_0000 & v) != 0) s += "FC-SN/";
            if ((0b_0010_0000 & v) != 0) s += "FC-SL/";
            if ((0b_0001_0000 & v) != 0) s += "FC-LL/";
            if ((0b_0000_1000 & v) != 0) s += "SFP+ Active/";
            if ((0b_0000_0100 & v) != 0) s += "SFP+ Passive/";
            v = data[9];
            if ((0b_1000_0000 & v) != 0) s += "FC-TW/";
            if ((0b_0100_0000 & v) != 0) s += "FC-TP/";
            if ((0b_0010_0000 & v) != 0) s += "FC-MI/";
            if ((0b_0001_0000 & v) != 0) s += "FC-TV/";
            if ((0b_0000_1000 & v) != 0) s += "FC-M6/";
            if ((0b_0000_0100 & v) != 0) s += "FC-M5/";
            v = data[10];
            if ((0b_1000_0000 & v) != 0) s += "FC-1200MB/";
            if ((0b_0100_0000 & v) != 0) s += "FC-800MB/";
            if ((0b_0010_0000 & v) != 0) s += "FC-1600MB/";
            if ((0b_0001_0000 & v) != 0) s += "FC-400MB/";
            if ((0b_0000_1000 & v) != 0) s += "FC-3200MB/";
            if ((0b_0000_0100 & v) != 0) s += "FC-200MB/";
            if ((0b_0000_0010 & v) != 0) s += "FC-SPEED2/";
            if ((0b_0000_0001 & v) != 0) s += "FC-100MB/";

            if ((0b_0000_0001 & data[62]) !=0 ) s += "FC-64G/";

            return s;
        }
        public static String diagtype(int v)
        {
            string s="";
            if ((0b_0100_0000 & v) != 0) s += "Diag imp/";
            if ((0b_0010_0000 & v) != 0) s += "Int calib/";
            if ((0b_0001_0000 & v) != 0) s += "Ext calib/";

            if ((0b_0000_1000 & v) != 0) s += "AVP"; else s = "OMA";

            return s;
        }
        // High Power Level Declaration (see SFF-8431 Addendum)
        //Value of zero identifies standard Power Levels 1 ,2 and 3 as indicated by bits 1 and 5.
        //Value of one identifies Power Level 4 requirement.Maximum power is declared in A2h, byte 66.
        public static String pwrc(int v)
        {
            string s = "";
            if ((0b_0100_0000 & v) != 0) s += "High power/";

            if ((0b_0001_0000 & v) != 0) s += "A2h Paging/";

            if ((0b_0000_1000 & v) != 0) s += "CDR/";
            if ((0b_0000_0100 & v) != 0) s += "Cooled/";
            switch (0b_0010_0010 & v)
            {
                case 0b_0010_0010: s += "Class 4"; break;
                case 0b_0010_0000: s += "Class 3"; break;
                case 0b_0000_0010: s += "Class 2"; break;
                case 0b_0000_0000: s += "Class 1"; break;
            }
            return s;
        }
        public static String ehoptions(int v)
        {
            string s = "";
            if ((0b_1000_0000 & v) != 0) s += "Alarm/";
            if ((0b_0100_0000 & v) != 0) s += "TX_DISABLE/";
            if ((0b_0010_0000 & v) != 0) s += "TX_FAULT/";
            if ((0b_0001_0000 & v) != 0) s += "RX_LOS/";

            if ((0b_0000_1000 & v) != 0) s += "RATE_SELECT/";
            if ((0b_0000_0100 & v) != 0) s += "SFF-8079/";
            if ((0b_0000_0010 & v) != 0) s += "SFF-SFF-8431";

            return s;
        }
        public static String compliance(int code)
        {
            string s = "";
            switch (code)
            {
                case 0x01: s = "Rev 9.3"; break;
                case 0x02: s = "Rev 9.5"; break;
                case 0x03: s = "Rev 10.2"; break;
                case 0x04: s = "Rev 10.4"; break;
                case 0x05: s = "Rev 11.0"; break;
                case 0x06: s = "Rev 11.3"; break;
                case 0x07: s = "Rev 11.4"; break;
                case 0x08: s = "Rev 12.3"; break;
                case 0x09: s = "Rev 12.4"; break;
            }
            return s;
        }
        // CC_BASE [Address A0h, Byte 63]
        // byte 0 to byte 62
        public static bool SFF8472_cc(MainWindow w)
        {
            int i, c = 0;
            for (i = 0; i <= 62; i++)
            {
                c += w.EEPROM[i];
            }
            if ((byte)(0xff & c) == w.EEPROM[63]) return true;
            return false;
        }
        public static String decode(MainWindow w)
        {

            string s = "";


            s = "---------- SFF-8472-------\r\n";

            if (SFF8472_cc(w) == false)
            {
                s += "checksum fail!!";
            }
            else
            {

                s += "Identifer   : " + SFF8024.ident(w.EEPROM[0x00]) + "\r\n";
                s += "Type        : " + type(ref w.EEPROM) + "\r\n";
                s += "Power Class : " + pwrc(w.EEPROM[64]) + "\r\n";
                s += "Vendor Name : " + SFP.nGet(ref w.EEPROM, 20, 16) + "\r\n";
                s += "Vendor PN   : " + SFP.nGet(ref w.EEPROM, 40, 16) + "\r\n";

                s += "Vendor OUI  : " + string.Format("{0:X2}:", w.EEPROM[37]) +
                                        string.Format("{0:X2}:", w.EEPROM[38]) +
                                        string.Format("{0:X2}", w.EEPROM[39]) + "\r\n";
                s += "Vendor REV  : " + SFP.nGet(ref w.EEPROM, 56, 4) + "\r\n";
                s += "Vendor SN   : " + SFP.nGet(ref w.EEPROM, 68, 16) + "\r\n";
                s += "Vendor DATE : " + SFP.nGet(ref w.EEPROM, 84, 8) + "\r\n";
                s += "connector   : " + SFF8024.connector_type(w.EEPROM[2]) + "\r\n";
                s += "wavelength  : " + string.Format("{0:d} nm",(w.EEPROM[60] * 0x100 + w.EEPROM[61])) + "\r\n";

                s += "BR nomial   : " + string.Format("{0:F2}", w.EEPROM[12] * 0.1F) + " Gbps\r\n";
                s += "Length(SMF) : " + string.Format("{0:d}", w.EEPROM[14]) + " km\r\n";
                s += "Length(OM3) : " + string.Format("{0:d}", w.EEPROM[19] * 10) + " m\r\n";
                s += "Length(OM2) : " + string.Format("{0:d}", w.EEPROM[16] * 10) + " m\r\n";
                s += "Length(OM1) : " + string.Format("{0:d}", w.EEPROM[17] * 10) + " m\r\n";
                s += "Length(OM4) : " + string.Format("{0:d}", w.EEPROM[18] * 10) + " m\r\n";
                s += "BR max      : " + string.Format("{0:F2}", w.EEPROM[12] * 0.1F) + " Gbps\r\n";
                s += "BR min      : " + string.Format("{0:F2}", w.EEPROM[12] * 0.1F) + " Gbps\r\n";
                s += "Diag type   : " + diagtype(w.EEPROM[92]) + "\r\n";
                s += "enhance opt : " + ehoptions(w.EEPROM[93]) + "\r\n";
                s += "Revision    : " + compliance(w.EEPROM[94]) + "\r\n";

            }
            return s;
        }
    }
}
