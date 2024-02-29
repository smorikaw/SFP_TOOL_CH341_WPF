using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            public String decode(MainWindow w) {
            String s = "";
                SFP sfp = new();
                SFF8024 sff8024 = new(); 
                CMIS52 cmis = new();

            s = "---------- CMIS -------\r\n";
                s += "Identifer   : " + sff8024.ident(w.PAGE00[0x80]) + "\r\n";
                s += "Power Class : " + cmis.pwrc(w.PAGE00[200]) + "\r\n";
                s += "Vendor VN   : " + sfp.nGet(ref w.PAGE00, 129, 16) + "\r\n";
                s += "Vendor PN   : " + sfp.nGet(ref w.PAGE00, 148, 16) + "\r\n";
                s += "Vendor OUI  : " + string.Format("{0:X2}:", w.PAGE00[145])+
                                        string.Format("{0:X2}:", w.PAGE00[146]) +
                                        string.Format("{0:X2}",  w.PAGE00[147]) + "\r\n";
                s += "Vendor REV  : " + sfp.nGet(ref w.PAGE00, 164, 2) + "\r\n";
                s += "Vendor SN   : " + sfp.nGet(ref w.PAGE00, 166, 16) + "\r\n";
                s += "Vendor DATE : " + sfp.nGet(ref w.PAGE00, 182, 8) + "\r\n";
                s += "connector   : " + sff8024.connector_type(w.PAGE00[203]) + "\r\n";

                s += "Link Length : " + string.Format("{0}",  w.PAGE00[202]) + " km\r\n";
                s += "Media Lane  : " + string.Format("{0:B8}", w.PAGE00[210]) + "\r\n";
                s += "Cable assy  : " + string.Format("{0:X2}", w.PAGE00[211]) + "\r\n";
                s += "Media tech  : " + cmis.MTech(w.PAGE00[212]) + "\r\n";

                int i;

                for (i = 1; i <= cmis.APPC(w); i++)
                {
                    s += string.Format("APP HOST{0:D1}  : " ,i)+ cmis.APPHOST(i,w) + "\r\n";
                    s += string.Format("APP MEDIA{0:D1} : " , i)+ cmis.APPMEDIA(i,w) + "\r\n";
                    s += string.Format("APP LANE{0:D1}  : ", i) + cmis.APPLANE(i,w) + "\r\n";
                    s += string.Format("APP OPTION{0:D1}: ", i) + cmis.APPOPT(i,w) + "\r\n";
                }
                return s;
            }
        // -----------------------------------------------------------------------
            public String pwrc(int code) {
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
            public float clen(int code) {
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
            public String MTech(int code) {
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
            public byte ID(MainWindow w) {
                return w.PAGE00[0];
            }
            public String VN(MainWindow w) {
                SFP sfp = new();
                return sfp.nGet(ref w.PAGE00, 129, 16);
            }
            public String OUI(MainWindow w) {
                return String.Format("{0:X2}:{0:X2}:{0:X2}", w.PAGE00[145], w.PAGE00[146], w.PAGE00[147]);
            }
            public String PN(MainWindow w) {
                SFP sfp = new();
            return sfp.nGet(ref w.PAGE00, 148, 16);
            }
            public String REV(MainWindow w) {
                SFP sfp = new();
            return sfp.nGet(ref w.PAGE00, 164, 2);
            }
            public String SN(MainWindow w) {
                SFP sfp = new();
            return sfp.nGet(ref w.PAGE00, 166, 16);
            }
            public String DATE(MainWindow w) {
                SFP sfp = new();
            return sfp.nGet(ref w.PAGE00, 182, 8);
            }
            public String CLEI(MainWindow w) {
                SFP sfp = new();
            return sfp.nGet(ref w.PAGE00, 190, 10);
            }
            public String PWRC(MainWindow w) {
                CMIS52 cmis = new();
                return cmis.pwrc(w.PAGE00[200]);
            }
            public float  maxPWR(MainWindow w) {
                return ((float)w.PAGE00[201] * (float)0.25F);
            }
            public String CONNECTOR(MainWindow w) {
                SFF8024 sff8024 = new();
                return sff8024.connector_type(w.PAGE00[203]);
            }
            public float LINKLEN(MainWindow w) {
                return clen(w.PAGE00[202 - 128]);
            }

            //
            /// //////////////// application list ////////////
            //
            // list number( 1-15) not zero start
            //
            // extend APP table page 01h (9-15)
            public int APPC(MainWindow w) {
                int c = 1;
                if ((0xff & w.EEPROM[90]) == 0xff) return 1;
                if ((0xff & w.EEPROM[94]) == 0xff) return 2;
                if ((0xff & w.EEPROM[98]) == 0xff) return 3;
                if ((0xff & w.EEPROM[102]) == 0xff) return 4;
                if ((0xff & w.EEPROM[106]) == 0xff) return 5;
                if ((0xff & w.EEPROM[110]) == 0xff) return 6;
                if ((0xff & w.EEPROM[114]) == 0xff) return 7;
                return c;
            }
            public string APPHOST(int i, MainWindow w) {
                SFF8024 sff8024 = new();
                if (i > 9)
                {
                    return sff8024.ehint(w.PAGE01[(223 - 9 + i)]);
                }
                else
                {
                    return sff8024.ehint(w.EEPROM[82 + i * 4]);
                }
            }
            public string mint(int code, int type) {
                String s = "";
                SFF8024 sff8024 = new();
                switch (type)
                {       // check SMF or MMF
                    case 0x01: s = sff8024.mmfint(code); break;
                    case 0x02: s = sff8024.smfint(code); break;
                    default: s = ""; break;
                }
                return s;
            }
            //
            // SMF or MMF
            //
            public string APPMEDIA(int i, MainWindow w) {
                CMIS52 cmis = new();
                return cmis.mint(w.EEPROM[83 + (i * 4)], w.EEPROM[85]);
            }
            public string APPLANE(int i, MainWindow w) {
                int v = w.EEPROM[84 + (i * 4)];
                return string.Format("({0:X2}) = ", v) +
                        string.Format("{0:X2} : ",  (0xf0 & v)>>4) +
                        string.Format("{0:X2}",     (0x0f & v));
            }
            public string APPOPT(int i, MainWindow w) {
                // PAGE[1]175+i]
                return string.Format("({0:X2}) = ", w.PAGE01[175+i]) + string.Format("{0:B8}", w.PAGE01[175 + i]);
            }


        
    }
}
