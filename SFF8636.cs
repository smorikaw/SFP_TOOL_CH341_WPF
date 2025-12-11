using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SFP_TOOL_CH341
{
    internal class SFF8636
    {
        // power class
        public static String pwrc(int code)
        {
            String s = "";
            switch (0xe3 & code)
            {
                case 0x00: s = "Power Class 1 (1.5 W max.)"; break;
                case 0x40: s = "Power Class 2 (2.0 W max.)"; break;
                case 0x80: s = "Power Class 3 (2.5 W max.)"; break;
                case 0xc0: s = "Power Class 4 (3.5 W max.)"; break;
                case 0xc1: s = "Power Class 5 (4.0 W max.)"; break;
                case 0xc2: s = "Power Class 6 (4.5 W max.)"; break;
                case 0xc3: s = "Power Class 7 (5.0 W max.)"; break;
            }
            //  if (0x20 & code) s = "Power Class 8 (over 5.0 W)";
            return s;
        }
        public static string diagtype(int code)
        {
            String s = "";

            if ((0b_0010_0000 & (int)code) != 0) s += "/Temp";
            if ((0b_0001_0000 & (int)code) != 0) s += "/VCC";
            if ((0b_0000_1000 & (int)code) != 0) s += "/AVP";
            if ((0b_0000_0100 & (int)code) != 0) s += "/TXP"; ;

            return s;
        }
        public static string rev(int code)
        {
            String s = "";
            switch (code)
            {
                case 0x00: s = "Revision not specified. Do not use for SFF-8636 rev 2.5 or higher."; break;
                case 0x01: s = "SFF-8436 Rev 4.8 or earlier"; break;
                case 0x02: s = "revision 4.8 or earlier of SFF-8436"; break;
                case 0x03: s = "SFF-8636 Rev 1.3 or earlier"; break;
                case 0x04: s = "SFF-8636 Rev 1.4"; break;
                case 0x05: s = "SFF-8636 Rev 1.5"; break;
                case 0x06: s = "SFF-8636 Rev 2.0"; break;
                case 0x07: s = "SFF-8636 Rev 2.5, 2.6 and 2.7"; break;
                case 0x08: s = "SFF-8636 Rev 2.8, 2.9 and 2.10"; break;
            }

            return s;
        }
        public static string devtech(int code){
            string s = "";
                    switch (0xf0 & code)
            {
                case 0b_0000_0000: s = "850 nm VCSEL"; break;
                case 0b_0001_0000: s = "1310 nm VCSEL"; break;
                case 0b_0010_0000: s = "1550 nm VCSEL"; break;
                case 0b_0011_0000: s = "1310 nm FP"; break;
                case 0b_0100_0000: s = "1310 nm DFB"; break;
                case 0b_0101_0000: s = "1550 nm DFB"; break;
                case 0b_0111_0000: s = "1310 nm EML"; break;
                case 0b_1000_0000: s = "Other / Undefined"; break;
                case 0b_1001_0000: s = "1490 nm DFB"; break;

                case 0b_1010_0000: s = "Copper cable"; break;
                case 0b_1011_0000: s = "1490 nm DFB"; break;
                case 0b_1100_0000: s = "1490 nm DFB"; break;
                case 0b_1101_0000: s = "1490 nm DFB"; break;
                case 0b_1110_0000: s = "1490 nm DFB"; break;
                case 0b_1111_0000: s = "1490 nm DFB"; break;
            }
            if ((0b_0000_1000 & (int)code) != 0) s += "/Active wavelength control";
            if ((0b_0000_0100 & (int)code) != 0) s += "/Cooled transmitter";
            if ((0b_0000_0010 & (int)code) != 0) s += "/APD detector";
            if ((0b_0000_0001 & (int)code) != 0) s += "/tunable";
            return s;
        }
        public static string type(int code)
        {
            string s = "";

            if ((0b_1000_0000 & (int)code) != 0) s += "extend type";
            if ((0b_0100_0000 & (int)code) != 0) s += "/10GBASE-LRM";
            if ((0b_0010_0000 & (int)code) != 0) s += "/10GBASE-LR";
            if ((0b_0001_0000 & (int)code) != 0) s += "/10GBASE-SR";
            if ((0b_0000_1000 & (int)code) != 0) s += "/40GBASE-CR4";
            if ((0b_0000_0100 & (int)code) != 0) s += "/40GBASE-SR4";
            if ((0b_0000_0010 & (int)code) != 0) s += "/40GBASE-LR4";
            if ((0b_0000_0001 & (int)code) != 0) s += "/40G Active";

            return s;
        }
        public static string optpage(int code)
        {
            string s = "";

            if ((0b_0000_0100 & (int)code) != 0) s += "page00 only"; else s += "page 03";
            if ((0b_0000_0010 & (int)code) != 0) s += "/IntL not asserted";

            return s;
        }
        public static string opt193(int code)
        {
            string s = "";

            if ((0b_0100_0000 & (int)code) != 0) s += "LPMode/TXDis configurable";
            if ((0b_0010_0000 & (int)code) != 0) s += "/IntL/RXLOSL configurable";

            return s;
        }
        public static string opt195(int code)
        {
            string s = "";

            if ((0b_1000_0000 & (int)code) != 0) s += "page02";
            if ((0b_0100_0000 & (int)code) != 0) s += "/page01";
            if ((0b_0010_0000 & (int)code) != 0) s += "/rate select";
            if ((0b_0001_0000 & (int)code) != 0) s += "/TXDis";
            if ((0b_0000_1000 & (int)code) != 0) s += "/TX falt";
            if ((0b_0000_0010 & (int)code) != 0) s += "/TX LOS";
            if ((0b_0000_0001 & (int)code) != 0) s += "/page20-21";
            return s;
        }
        // CC_BASE (00h 191)
        // bytes from 128 to 190
        public static bool SFF8636_cc(MainWindow w)
        {
            int i, c = 0;
            for (i=128; i <= 190; i++)
            {
                c += w.PAGE00[i];
            }
            if((byte)(0xff & c) == w.PAGE00[191]) return true;
            return false;
        }
        //
        // Table 6-29 Equalizer, Emphasis, Amplitude and Timing (Page 03h Bytes 224-229)
        // bit 7 :Controllable Host-Side FEC support
        // bit 6 :Controllable Media-Side FEC support
        public static string media_FEC(MainWindow w)
        {
            string s = "";
            //    w.PAGE03[227];
            return s;
        }
        //
        // main decode table
        //
        public static String decode(MainWindow w)
        {
            byte[] data = w.PAGE00;
            String s = "";

            s = "---------- SFF-8636 -------\r\n";
            if (SFF8636_cc(w) == false)
            {
                s += "checksum fail!!";
            }
            else { 

            s += "Identifer   : " + SFF8024.ident(w.PAGE00[0x80]) + "\r\n";
            s += "Revision    : " + rev(w.PAGE00[0x82]) + "\r\n";
            s += "Power Class : " + pwrc(w.PAGE00[129]) + "\r\n";
            s += "Type        : " + type(w.PAGE00[131]) + "\r\n";
            s += "EXT Type    : " + SFF8024.exttype(w.PAGE00[192]) + "\r\n";
            s += "Vendor Name : " + SFP.nGet(ref w.PAGE00, 148, 16) + "\r\n";
            s += "Vendor PN   : " + SFP.nGet(ref w.PAGE00, 168, 16) + "\r\n";
            w.PN = SFP.nGet(ref w.PAGE00, 168, 16);
            s += "Vendor OUI  : " + string.Format("{0:X2}:", w.PAGE00[165]) +
                                    string.Format("{0:X2}:", w.PAGE00[166]) +
                                    string.Format("{0:X2}", w.PAGE00[167]) + "\r\n";
            s += "Vendor REV  : " + SFP.nGet(ref w.PAGE00, 184, 2) + "\r\n";
            s += "Vendor SN   : " + SFP.nGet(ref w.PAGE00, 196, 16) + "\r\n";
            w.SN = SFP.nGet(ref w.PAGE00, 196, 16);
            s += "Vendor DATE : " + SFP.nGet(ref w.PAGE00, 212, 8) + "\r\n";
            s += "connector   : " + SFF8024.connector_type(w.PAGE00[130]) + "\r\n";

            s += "Length(SMF) : " + string.Format("{0:d}", w.PAGE00[142]) + " km\r\n";
            s += "Length(OM3) : " + string.Format("{0:d}", w.PAGE00[144]) + " m\r\n";
            s += "Length(OM1) : " + string.Format("{0:d}", w.PAGE00[145]) + " m\r\n";
            s += "Length(OM4) : " + string.Format("{0:d}", w.PAGE00[146] * 2) + " m\r\n";
            s += "Device Tech : " + devtech(w.PAGE00[147]) + "\r\n";
            // 6.3.24 Options (00h 193-195)
            s += "Options     : " + string.Format("{0:B8} : ", w.PAGE00[193]) +
                                    string.Format("{0:B8} : ", w.PAGE00[194]) +
                                    string.Format("{0:B8}", w.PAGE00[195]) + "\r\n";
            s += "Options(193): " + opt193(w.PAGE00[193]) + "\r\n";
            s += "Options(195): " + opt195(w.PAGE00[195]) + "\r\n";
                // Table 6-24 Diagnostic Monitoring Type (Page 00h Byte 220
                s += "Diag type   : " + diagtype(w.PAGE00[220]) + "\r\n";
            // Table 6-25 Enhanced Options (Page 00h Byte 221)
            s += "Echance opt : " + string.Format("{0:B8}", w.PAGE00[221]) + "\r\n";
            // Table 6-26 Extended Baud Rate: Nominal (Page 00h Byte 222)
            // F2 is 小数点以下二桁
            s += "BR rate     : " + string.Format("{0:F2}", w.PAGE00[222] * 0.25F) + "Gbps\r\n";
                // Table 6-24 Diagnostic Monitoring Type (Page 00h Byte 220
            s += "L-Rev Comp  : " + rev(w.EEPROM[1]) + "\r\n";
            s += "L-Options   : " + optpage(w.EEPROM[2]) + "\r\n";
            }
            return s;
            
        }
    }

}
