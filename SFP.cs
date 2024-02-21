﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP_TOOL_CH341
{
    public class SFP
    {
        public SFP() { }
        // 
        public String nGet(ref byte[] data, int addr, int len)
        {
            int i, j;
            char[] str = new char[256+1];

            j = 0;
            for (i = addr; j < len; i++)
            {
                str[j++] = (char)data[i];

            }
            str[j] = (char)0x00;        // terminater
  
            // delete no ASCII char
            for (i = 0; i < len; i++)
            {
                if (str[i] < 0x20 || str[i] > 0x7f) str[i] = (char)'.';
            }
            // strip space
            for (i = len - 1; i > 0; i--)
            {
                if (str[i] == 0x20) str[i] = (char)0x00;
            }
            return new string(str);
            //return String.Format("{0}",str);
        }
        //
        public string sff_eth(MainWindow w)
        {
            string s = "";

            SFF8472 sff8472 = new();
            SFF8636 sff8636 = new();
            SFP sfp = new();
            CMIS52 cmis = new();

            
            switch (sfp.format_type(w))
            {
                case 0:
                    w.statusLabel.Content = "decode SFF-8472";
                    s = sff8472.decode(w); break;
                case 1:
                    w.statusLabel.Content = "decode SFF-8636"; 
                    s = sff8636.decode(w); break;
                case 2:
                    w.statusLabel.Content = "decode CMIS";
                    s = cmis.decode(w); break;
            }
            
            return s;
        }
        // 0:SFF8472, 1:SFF8636, 2:CMIS
        public int format_type(MainWindow w)
        {
            int f=0;
            if (0x03 == w.EEPROM[0x00]) return 0; // SFP/SFP+/SFP28/
            switch (w.PAGE00[0x80])
            {
                case 0x0d:      // QSFP+
                case 0x11:      // QSFP28
                          f = 1;  break;
                case 0x18:      // QSFP-DD
                case 0x19:      // OSFP
                           f = 2; break;
            }

            return f; 
        }

    }

}
