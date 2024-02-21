using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFP_TOOL_CH341
{
    internal class SFF8472
    {
        public String type(ref byte[] data)
        {
            int v = 0;
            String s = "";
            SFF8024 sff8024 = new();

            s = sff8024.exttype(data[36]);
           
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
        // CC_BASE [Address A0h, Byte 63]
        // byte 0 to byte 62
        bool SFF8472_cc(MainWindow w)
        {
            int i, c = 0;
            for (i = 0; i <= 62; i++)
            {
                c += w.EEPROM[i];
            }
            if ((byte)(0xff & c) == w.EEPROM[63]) return true;
            return false;
        }
        public String decode(MainWindow w)
        {

            string s = "";
            SFP sfp = new();
            SFF8024 sff8024 = new();
            SFF8472 sff8472 = new();
            SFF8636 sff8636 = new();

            s = "---------- SFF-8472-------\r\n";

            if (SFF8472_cc(w) == false)
            {
                s += "checksum fail!!";
            }
            else
            {

                s += "Identifer   : " + sff8024.ident(w.EEPROM[0x00]) + "\r\n";
                s += "Type        : " + sff8472.type(ref w.EEPROM) + "\r\n";
                s += "Power Class : " + sff8636.pwrc(w.EEPROM[64]) + "\r\n";
                s += "Vendor Name : " + sfp.nGet(ref w.EEPROM, 20, 16) + "\r\n";
                s += "Vendor PN   : " + sfp.nGet(ref w.EEPROM, 40, 16) + "\r\n";

                s += "Vendor OUI  : " + string.Format("{0:X2}:", w.PAGE00[37]) +
                                        string.Format("{0:X2}:", w.PAGE00[38]) +
                                        string.Format("{0:X2}", w.PAGE00[39]) + "\r\n";
                s += "Vendor REV  : " + sfp.nGet(ref w.EEPROM, 56, 4) + "\r\n";
                s += "Vendor SN   : " + sfp.nGet(ref w.EEPROM, 68, 16) + "\r\n";
                s += "Vendor DATE : " + sfp.nGet(ref w.EEPROM, 84, 8) + "\r\n";
                s += "connector   : " + sff8024.connector_type(w.EEPROM[2]) + "\r\n";

                s += "Length(SMF) : " + string.Format("{0:d4}", w.EEPROM[14]) + " km\r\n";
                s += "Length(OM3) : " + string.Format("{0:d4}", w.EEPROM[19] * 10) + " m\r\n";
                s += "Length(OM2) : " + string.Format("{0:d4}", w.EEPROM[16] * 10) + " m\r\n";
                s += "Length(OM1) : " + string.Format("{0:d4}", w.EEPROM[17] * 10) + " m\r\n";
                s += "Length(OM4) : " + string.Format("{0:d4}", w.EEPROM[18] * 10) + " m\r\n";

            }
            return s;
        }
    }
}
