using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace SFP_TOOL_CH341
{
    internal class CMIS52
    {
    //==================================== class

         //128        1 SFF8024IdentifierCopy Copy of Byte 00h:0
         //129 - 144 16 VendorName Vendor name(ASCII)
         //145 - 147  3 VendorOUI Vendor IEEE company ID
         //148 - 163 16 VendorPN Part number provided by vendor(ASCII)
         //164 - 165  2 VendorRev Revision level for part number provided by vendor(ASCII)
         //166 - 181 16 VendorSN Vendor Serial Number(ASCII)
         //182 - 189  8 DateCode Manufacturing Date Code(ASCII)
         //190 - 199 10 CLEICode Common Language Equipment Identification Code(ASCII)
         //200 - 201  2 ModulePowerCharacteristics Module power characteristics
         //202        1 CableAssemblyLinkLength Cable length(for cable assembly modules only)
         //203        1 ConnectorType Connector type of the media interface
         //204 - 209  6 Copper Cable Attenuation Attenuation characteristics(passive copper cables only)
         //210        1 MediaLaneInformation Supported near end media lanes(all modules)
         //211        1 Cable Assembly Information Far end modules information(cable assemblies only)
         //212        1 MediaInterfaceTechnology Information on media side device or cable technology
         //213 - 220  8 - Reserved[8]
         //221        1 - Custom[1]
         //222        1 PageChecksum Page Checksum over bytes 128 - 221
         //223 - 255 33 - Custom[33] Information(non - volatile)
         //
         //85          1 Module Type encoding
         //86         1 HotInterfaceID
         //87         1 MediaInterfaceID
         //88         1 HostLaneCount / MediaLaneCount
         //89         1 HostLaneAssignmnetOptions
         //p01:176    1 MediaLaneAssigmentOptions
        public static String decode(MainWindow w) {
            String s = "";

            s = "---------- CMIS -------\r\n";
            s += "Identifer   : " + SFF8024.ident(w.PAGE00[0x80]) + "\r\n";
            s += "CMIS Rev    : " + string.Format("{0:X2}", w.EEPROM[1]) + "\r\n";
            //  2 : support config
            //  3 : module state
            //  4-6 : page flags
            //  8 : Cdb
            //  9-11 : Warning flags
            // 14-15 : TempMonValue
            // 16-17 : VccMonVoltage
            s += "Temp Mon    : " + string.Format("{0:F2}", w.EEPROM[14] + (w.EEPROM[15]/256.0)) + " C\r\n";
            s += "Vcc Mon     : " + string.Format("{0:F2}", (w.EEPROM[16]*0x100 + w.EEPROM[17]) * 0.0001) + " V\r\n";
            // 18-19 : Aux1MonValtage(TEC/Later etc.)
            // 20-21 : Aux2MonValtage
            // 22-23 : Aux3MonValtage
            s += "FW Revision : " + string.Format("{0}.", w.EEPROM[39]) + string.Format("{0}", w.EEPROM[40]) + "\r\n";
            // page 00    
            s += "Power Class : " + pwrc(w.PAGE00[200]) + string.Format(" {0:F2}W", w.PAGE00[201] * 0.25)+"\r\n";
            s += "Vendor VN   : " + SFP.nGet(ref w.PAGE00, 129, 16) + "\r\n";
            s += "Vendor PN   : " + SFP.nGet(ref w.PAGE00, 148, 16) + "\r\n";
            w.PN = SFP.nGet(ref w.PAGE00, 148, 16);
            s += "Vendor OUI  : " + string.Format("{0:X2}:", w.PAGE00[145])+
                                    string.Format("{0:X2}:", w.PAGE00[146]) +
                                    string.Format("{0:X2}",  w.PAGE00[147]) + "\r\n";
            s += "Vendor REV  : " + SFP.nGet(ref w.PAGE00, 164, 2) + "\r\n";
            s += "Vendor SN   : " + SFP.nGet(ref w.PAGE00, 166, 16) + "\r\n";
            w.SN = SFP.nGet(ref w.PAGE00, 166, 16);
            s += "Vendor DATE : " + SFP.nGet(ref w.PAGE00, 182, 8) + "\r\n";
            s += "connector   : " + SFF8024.connector_type(w.PAGE00[203]) + "\r\n";

            s += "Link Length : " + string.Format("{0:#,0}", LEN(w.PAGE00[202])) + " m\r\n";
            s += "Media Lane  : " + string.Format("{0:B8}", w.PAGE00[210]) + "\r\n";
            s += "Cable assy  : " + string.Format("{0:X2}", w.PAGE00[211]) + "\r\n";
            s += "Media tech  : " + MTech(w.PAGE00[212]) + "\r\n";
                
                int i,cc;

            for (i = 1; i <= APPC(w); i++)
            {
                s += string.Format("{0:D1})Host ID   : ", i) + APPHOST(i,w) + "\r\n";
                s += string.Format("{0:D1})Media ID  : ", i) + APPMEDIA(i,w) + "\r\n";
                s += string.Format("{0:D1})Lane count: ", i) + APPLANE(i,w) + "\r\n";
                s += string.Format("{0:D1})Hlane Assign: ", i) + APPOPTH(i, w) + "\r\n";
                s += string.Format("{0:D1})Mlane Assign: ", i) + APPOPTM(i, w) + "\r\n";
           //     s += string.Format("{0:D1})Hlane Assign: ", i) + APPOPT(w.EEPROM[85 + (i * 4)]) + "\r\n";
           //     s += string.Format("{0:D1})Mlane Assign: ", i) + APPOPT(w.PAGE01[0xb0-1+i]) + "\r\n";
            }
            //8.3.10 Page 00h Page Checksum (required) 2
            //The page checksum is a one - byte code that can be used to verify that the read-only static data
            //on Page 00h is 3 valid.The page checksum value shall be the low order 8 bits of the arithmetic
            //sum of all byte values from byte 4 128 to byte 221, inclusive.
            cc = 0;
            for (i = 128; i < 221; i++)
            {
                cc += w.PAGE00[i];
            }
            s += "Page00 checksum : " + string.Format("{0:X4}", cc) 
                                      + string.Format(" vs {0:X2}", w.PAGE00[222])  + "\r\n";
            // 8.4.15 Page Checksum (Page 01h, Byte 255, RO RQD) 1
            // The Page Checksum is a one - byte code that can be used to verify that the read-only static data
            // on Page 01h is 2 valid.The checksum code shall be the low order 8 bits of the arithmetic sum of
            // all byte values from byte 130 to byte 254, inclusive.
            cc = 0;
            for (i=130; i < 254; i++)
            {
                cc += w.PAGE01[i];
            }
            s += "Page01 checksum : " + string.Format("{0:X4}", cc)
                                       +string.Format(" vs {0:X2}", w.PAGE01[255]) + "\r\n";
            // 8.5.3 Page Checksum (Page 02h, Byte 255, RO RQD) 7
            // The Page Checksum code is a one - byte code that can be used to verify that the device property
            // information in 8 the module is valid.The Page Checksum code shall be the low order 8 bits of
            // the arithmetic sum of all byte 9 values from byte 128 to byte 254, inclusive.
            cc = 0;
            for (i = 128; i < 254; i++)
            {
                cc += w.PAGE02[i];
            }
            s += "Page02 checksum : " + string.Format("{0:X4}", cc)
                                      + string.Format(" vs {0:X2}", w.PAGE02[255]) + "\r\n"; 

            return s;
        }
        // byte 202 Table 8-29 Cable Assembly Link Length (Page 00h)
        public static float LEN(byte b)
        {
            float multi = 0.0F;
            switch(0xc0 & b)
            {
                case 0x00: multi = 0.1F; break;
                case 0x40: multi = 1.0F; break;
                case 0x80: multi = 10.0F; break;
                case 0xc0: multi = 100.0F; break;
            }
            return (multi * (0x3f & b));
        }
        // -----------------------------------------------------------------------
        public static String pwrc(int code) {
            string s = "";
            switch (0xe0 & code)
                {
                    case 0x00: s = "class 1"; break;
                    case 0x20: s = "class 2"; break;
                    case 0x40: s = "class 3"; break;
                    case 0x60: s = "class 4"; break;
                    case 0x80: s = "class 5"; break;
                    case 0xa0: s = "class 6"; break;
                    case 0xc0: s = "class 7"; break;
                    case 0xe0: s = "class 8"; break;
                }
                return s;
            }
        public static float clen(int code) {
            float mx = 0.0F;
            switch (0xc0 & code)
                {
                    case 0x00: mx = (float)0.1F; break;
                    case 0x40: mx = 1.0F; break;
                    case 0x80: mx = 10.0F; break;
                    case 0xc0: mx = 100.0F; break;
                }
                return (mx * (float)(0x1f & (code)));
            }
            // CMIS Table 8-36 Media Interface Technology encodings
        public static String MTech(int code) {
             string s = "";
             switch (code)
                 {
                    case 0x00: s = "850 nm VCSEL"; break;
                    case 0x01: s = "1310 nm VCSEL"; break;
                    case 0x02: s = "1550 nm VCSEL"; break;
                    case 0x03: s = "1310 nm FP"; break;
                    case 0x04: s = "1310 nm DFB"; break;
                    case 0x05: s = "1550 nm DFB"; break;
                    case 0x06: s = "1310 nm EML"; break;
                    case 0x07: s = "1550 nm EML"; break;
                    case 0x08: s = "Others"; break;
                    case 0x09: s = "1490 nm DFB"; break;
                    case 0x0a: s = "Copper cable unequalized"; break;
                    case 0x0b: s = "Copper cable passive equalized"; break;
                    case 0x0c: s = "Copper cable, near and far end limiting active equalizers"; break;
                    case 0x0d: s = "Copper cable, far end limiting active equalizers"; break;
                    case 0x0e: s = "Copper cable, near end limiting active equalizers"; break;
                    case 0x0f: s = "Copper cable, linear active equalizers"; break;
                    case 0x10: s = "C-band tunable laser"; break;
                    case 0x11: s = "C-band tunable laser"; break;
                }
                return s;
            }
        public static byte ID(MainWindow w) {
                return w.PAGE00[0];
            }
        public static String VN(MainWindow w) {
                return SFP.nGet(ref w.PAGE00, 129, 16);
            }
        public static String OUI(MainWindow w) {
                return String.Format("{0:X2}:{0:X2}:{0:X2}", w.PAGE00[145], w.PAGE00[146], w.PAGE00[147]);
            }
        public static String PN(MainWindow w) {
                return SFP.nGet(ref w.PAGE00, 148, 16);
            }
        public static String REV(MainWindow w) {
                return SFP.nGet(ref w.PAGE00, 164, 2);
            }
        public static String SN(MainWindow w) {
                return SFP.nGet(ref w.PAGE00, 166, 16);
            }
        public static String DATE(MainWindow w) {
                return SFP.nGet(ref w.PAGE00, 182, 8);
            }
        public static String CLEI(MainWindow w) {
                return SFP.nGet(ref w.PAGE00, 190, 10);
            }
        public static String PWRC(MainWindow w) {
                return pwrc(w.PAGE00[200]);
            }
        public static float maxPWR(MainWindow w) {
                return ((float)w.PAGE00[201] * (float)0.25F);
            }
        public static String CONNECTOR(MainWindow w) {
                return SFF8024.connector_type(w.PAGE00[203]);
            }
        public static float LINKLEN(MainWindow w) {
                return clen(w.PAGE00[202 - 128]);
            }

        //
        /// //////////////// application list ////////////
        //
        // list number( 1-15) not zero start
        // APP table lower byte 86-
        // extend APP table (9-15) page 01h byte 223
        public static int APPC(MainWindow w) {
            int c = 15;
            if ((0xff & w.EEPROM[90]) == 0xff) c = 1;
            if ((0xff & w.EEPROM[94]) == 0xff) c = 2;
            if ((0xff & w.EEPROM[98]) == 0xff) c = 3;
            if ((0xff & w.EEPROM[102]) == 0xff) c = 4;
            if ((0xff & w.EEPROM[106]) == 0xff) c = 5;
            if ((0xff & w.EEPROM[110]) == 0xff) c = 6;
            if ((0xff & w.EEPROM[114]) == 0xff) c = 7;

            if ((0xff & w.PAGE01[223]) == 0xff) c = 8;
            if ((0xff & w.PAGE01[227]) == 0xff) c = 9;
            if ((0xff & w.PAGE01[231]) == 0xff) c = 10;
            if ((0xff & w.PAGE01[235]) == 0xff) c = 11;
            if ((0xff & w.PAGE01[239]) == 0xff) c = 12;
            if ((0xff & w.PAGE01[243]) == 0xff) c = 13;
            if ((0xff & w.PAGE01[247]) == 0xff) c = 14;
            return c;
            }
        public static string APPHOST(int i, MainWindow w) {
            int v =0;

            if (i > 8)
                {
                    v = w.PAGE01[223 + ((i-9) * 4)];
                }
                else
                {
                    v = w.EEPROM[82 + (i * 4)];     // from 86
                }
            return SFF8024.ehint(v);
        }
        public static string mint(int code, int type) {
                String s = "";

                switch (type)
                {       // check SMF or MMF
                    case 0x01: s = SFF8024.mmfint(code); break; // Multimode separate transceiver
                    case 0x02: s = SFF8024.smfint(code); break; // Singlemode separate transceiver
                    case 0x03: s = SFF8024.dacint(code); break; // Passive copper cable
                    case 0x04: s = SFF8024.aocint(code); break; // Active cable
                    case 0x05: s = SFF8024.utpint(code); break; // BASE-T media
                    default: s = ""; break;
                }
                return s;
            }
            //
            // SMF or MMF
            //
        public static string APPMEDIA(int i, MainWindow w) {
                int v = 0;
                if (i > 8)
                {
                    v = w.PAGE01[223+1 + ((i - 9) * 4)];
                }
                else
                {
                    v = w.EEPROM[83 + (i * 4)];
                }

                return mint(v, w.EEPROM[85]);
            }
        public static string APPLANE(int i, MainWindow w) {
            int v=0;
            if (i > 8) 
                {
                    v = w.PAGE01[223+2 + ((i - 9) * 4)];
                }
            else
                {
                    v = w.EEPROM[84 + (i * 4)];
                }
            return string.Format("({0:X2}) = ", v) +
                   string.Format("{0:X2} : ",  (0xf0 & v)>>4) +
                   string.Format("{0:X2}",     (0x0f & v));
            }
        public static string APPOPTH(int i, MainWindow w)
        {
            int v;
            if (i > 8)
            {
                v = w.PAGE01[223 + 3 + ((i - 9) * 4)];
            }
            else
            {
                v = w.EEPROM[85 + (i * 4)];
            }
            return string.Format("({0:X2}) = ", v) + string.Format("{0:B8}", v);
        }
        public static string APPOPTM(int i, MainWindow w)
        {
            int v;
            v = w.PAGE01[0xb0 - 1 + i];
            return string.Format("({0:X2}) = ", v) + string.Format("{0:B8}", v);
        }
        public static string APPOPT(int i) {
            // PAGE[1]175+i]
            return string.Format("({0:X2}) = ", i) + string.Format("{0:B8}", i);
            }
        
    }
}
