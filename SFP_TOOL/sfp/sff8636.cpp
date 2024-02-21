#include	<stdio.h>
#include	<iostream>
#include	<string>
#include	<format>

#include	"sff8636.h"
#include	"SFF8024.h"
#include	"sff_common.h"

using namespace std;
typedef int BOOL;

extern char EEPROM[];
extern char EEPROM_p[4][256];
///////////////////////////// SFF-8636 /////////////////////////////////
// 128
// 130 connector type
// 131 specification conpliance
// 139 encoding
// 140 signaling rate
// 141 extended rate
// 142 Length(SMF)
// 143 Length(OM3)
// 144 Length(OM2)
// 145 Length(OM1)
// 146 Length(OM4)
// 147 Device Technology
// 148 Vendor name
// 164 Extended module
// 165 Vendor OUI
// 168 Vendor PN
// 184 Vendor REV
// 196 SN
// 212 DATE
// 220 Diag type
// 221 Echanced Optios
// 222 BR rate
// 223 CC_EXT
// 224 Vendor specific
std::string SFF8636::pwrc(int code) {
	std::string s;
	switch (0xe3 & code) {
	case 0x00:	s = "Power Class 1 (1.5 W max.)"; break;
	case 0x40:	s = "Power Class 2 (2.0 W max.)"; break;
	case 0x80:	s = "Power Class 3 (2.5 W max.)"; break;
	case 0xc0:	s = "Power Class 4 (3.5 W max.)"; break;
	case 0xc1:	s = "Power Class 5 (4.0 W max.)"; break;
	case 0xc2:	s = "Power Class 6 (4.5 W max.)"; break;
	case 0xc3:	s = "Power Class 7 (5.0 W max.)"; break;
	}
	if(0x20 & code)	s = "Power Class 8 (over 5.0 W)";
	return s;
}
std::string  SFF8636::decode(char data[]) {
	std::string s;
	SFF_common sfp;
	SFF8024 sff8024;

	s = "---------- SFF-8636 -------\r\n";
	s += "Identifer : " + sff8024.ident(data[0x80]) + "\r\n";
	s += "Power Class : " + SFF8636::pwrc(data[129]) + "\r\n";
	s += "Vendor Name : " + sfp.nGet(data, 148, 16) + "\r\n";
	s += "Vendor PN   : " + sfp.nGet(data, 168, 16) + "\r\n";
	s += "Vendor OUI  : " + format("{:#02X}:{:#02X}:{:#02X}", data[145] , data[146] ,data[147]) + "\r\n";
	s += "Vendor REV  : " + sfp.nGet(data, 184, 2) + "\r\n";
	s += "Vendor SN   : " + sfp.nGet(data, 196, 16) + "\r\n";
	s += "Vendor DATE : " + sfp.nGet(data, 212, 8) + "\r\n";
	s += "connector   : " + sff8024.connector_type(data[130]) + "\r\n";

	s += "Length(SMF) : " + to_string(data[142]) + " km\r\n";
	s += "Length(OM3) : " + to_string(data[143]*2) + " m\r\n";
	s += "Length(OM2) : " + to_string(data[144]) + " m\r\n";
	s += "Length(OM1) : " + to_string(data[145]) + " m\r\n";
	s += "Length(OM4) : " + to_string(data[146]*2) + " m\r\n";

	return s;
}