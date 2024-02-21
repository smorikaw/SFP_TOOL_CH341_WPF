
#include	<string>
#include	<format>
#include	"CMIS52.h"
#include	"SFF8024.h"
#include	"sff_common.h"
#define LF	"\r\n"
//

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
std::string  CMIS52::decode(char data[]) {
	std::string s;
	SFF_common sfp;
	SFF8024 sff8024;

	s = "---------- CMIS -------\r\n";
	s += "Identifer : " + sff8024.ident(data[0x80]) + LF;
	s += "Power Class : " + CMIS52::pwrc(data[200]) + LF;
	s += "Vendor Name : " + sfp.nGet(data, 129, 16) + LF;
	s += "Vendor PN   : " + sfp.nGet(data, 148, 16) + LF;
	s += "Vendor OUI  : " + format("{:02X}:{:02X}:{:02X}", data[145], data[146], data[147]) + LF;
	s += "Vendor REV  : " + sfp.nGet(data, 164, 2) + LF;
	s += "Vendor SN   : " + sfp.nGet(data, 166, 16) + LF;
	s += "Vendor DATE : " + sfp.nGet(data, 182, 8) + LF;
	s += "connector   : " + sff8024.connector_type(data[203]) + LF;

	s += "Link Length : " + to_string(data[202]) + " km\r\n";
	s += "Media Lane  : " + format("{:08B}", data[210]) + LF;
	s += "Cable assy  : " + format("{:02X}",data[211]) + LF;
	s += "Media tech  : " + CMIS52::MTech(data[212]) + LF;

	int i;
	std::memcpy(UPPER, data, 0x7f);
	std::memcpy(&PAGE00[128], &data[128], 0x7f);
//	s += "PN          : " + CMIS52::PN() + LF;
//	s += "Power class : " + CMIS52::PWRC() + LF;
	for (i = 1; i <= CMIS52::APPC(); i++) {
		s += "APP HOST  : " + CMIS52::APPHOST(i) + LF;
		s += "APP MEDIA : " + CMIS52::APPMEDIA(i) + LF;
		s += "APP LANE  : " + CMIS52::APPLANE(i) + LF;
		s += "APP OPTION: " + CMIS52::APPOPT(i) + LF;
	}
	return s;
}
string CMIS52::pwrc(int code) {
	string s;
	switch (0xe0 & code) {
	case 0x00: s = "class 1";	break;
	case 0x20: s = "class 2";	break;
	case 0x40: s = "class 3";	break;
	case 0x60: s = "class 4";	break;
	case 0x80: s = "class 5";	break;
	case 0xa0: s = "class 6";	break;
	case 0xc0: s = "class 7";	break;
	case 0xe0: s = "class 8";	break;
	}
	return s;
}
float CMIS52::clen(int code) {
	float mx = 0.0;
	switch (0xc0 & code) {
	case 0x00: mx = (float)0.1;	break;
	case 0x40: mx = 1.0;	break;
	case 0x80: mx = 10.0;	break;
	case 0xc0: mx = 100.0;	break;
	}
	return (mx * (float)(0x1f & (code)));
}
// CMIS Table 8-36 Media Interface Technology encodings
std::string CMIS52::MTech(int code) {
	std::string s = "";
	switch (code) {
	case 0x00: s = "850 nm VCSEL";	break;
	case 0x01: s = "1310 nm VCSEL";	break;
	case 0x02: s = "1550 nm VCSEL";	break;
	case 0x03: s = "1310 nm FP";	break;
	case 0x04: s = "1310 nm DFB";	break;
	case 0x05: s = "1550 nm DFB";	break;
	case 0x06: s = "1310 nm EML";	break;
	case 0x07: s = "1550 nm EML";	break;
	case 0x08: s = "Others";	break;
	case 0x09: s = "1490 nm DFB";	break;
	case 0x0a: s = "Copper cable unequalized";	break;
	case 0x0b: s = "Copper cable passive equalized";	break;
	case 0x0c: s = "Copper cable, near and far end limiting active equalizers";	break;
	case 0x0d: s = "Copper cable, far end limiting active equalizers";	break;
	case 0x0e: s = "Copper cable, near end limiting active equalizers";	break;
	case 0x0f: s = "Copper cable, linear active equalizers";	break;
	case 0x10: s = "C-band tunable laser";	break;
	case 0x11: s = "C-band tunable laser";	break;
	}
	return s;
}
char CMIS52::ID() {
	return CMIS52::PAGE00[0];
}
string CMIS52::VN() {
	SFF_common sfp;
	return sfp.nGet(CMIS52::PAGE00, 129, 16);
}
string CMIS52::OUI() {
	return  format("{:02X}:{:02X}:{:02X}", CMIS52::PAGE00[145], CMIS52::PAGE00[146], CMIS52::PAGE00[147]);
}
string CMIS52::PN() {
	SFF_common sfp;
	return sfp.nGet(CMIS52::PAGE00, 148, 16);
}
string CMIS52::REV() {
	SFF_common sfp;
	return sfp.nGet(CMIS52::PAGE00, 164, 2);
}
string CMIS52::SN() {
	SFF_common sfp;
	return sfp.nGet(CMIS52::PAGE00, 166, 16);
}
string CMIS52::DATE() {
	SFF_common sfp;
	return sfp.nGet(CMIS52::PAGE00, 182, 8);
}
string CMIS52::CLEI() {
	SFF_common sfp;
	return sfp.nGet(CMIS52::PAGE00, 190, 10);
}
string CMIS52::PWRC() {
	return CMIS52::pwrc(PAGE00[200]);
}
float CMIS52::maxPWR() {
	return ((float)CMIS52::PAGE00[201] * (float)0.25);
}
string CMIS52::CONNECTOR() {
	SFF8024 sff8024;
	return sff8024.connector_type(CMIS52::PAGE00[203]);
}
float CMIS52::LINKLEN() {
	return CMIS52::clen(CMIS52::PAGE00[202 - 128]);
}

//
/// //////////////// application list ////////////
//
// list number( 1-15) not zero start
//
	// extend APP table page 01h (9-15)
int CMIS52::APPC() {
	int c = 1;
	if ((0xff & UPPER[90]) == 0xff) return 1;
	if ((0xff & UPPER[94]) == 0xff) return 2;
	if ((0xff & UPPER[98]) == 0xff) return 3;
	if ((0xff & UPPER[102]) == 0xff) return 4;
	if ((0xff & UPPER[106]) == 0xff) return 5;
	if ((0xff & UPPER[110]) == 0xff) return 6;
	if ((0xff & UPPER[114]) == 0xff) return 7;
	return c;
}
string CMIS52::APPHOST(int i) {
	SFF8024 sff8024;
	if (i > 9) {
		return sff8024.ehint(PAGE01[(223 - 9 + i)]);
	}
	else {
		return sff8024.ehint(UPPER[82 + i * 4]);
	}
}
string CMIS52::mint(int code, int type) {
	string s;
	SFF8024 sff8024;
	switch (type) {		// check SMF or MMF
	case 0x01: s = sff8024.mmfint(code); break;
	case 0x02: s = sff8024.smfint(code); break;
	default: s = "";
	}
	return s;
}
//
// SMF or MMF
//
string CMIS52::APPMEDIA(int i) {
	return CMIS52::mint(UPPER[83 + (i * 4)], UPPER[85]);
}
string CMIS52::APPLANE(int i) {
	int v = UPPER[84 + (i * 4)];
	return format("{:02X} : {:02X}", (0xf0 & v) >> 4 , (0x0f & v));
}
string CMIS52::APPOPT(int i) {
	// PAGE[1]175+i]
	return format("{:02B}", PAGE01[175 + i]);
}

