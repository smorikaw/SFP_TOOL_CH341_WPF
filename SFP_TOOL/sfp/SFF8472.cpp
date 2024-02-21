#include	"SFF8472.h"
#include	<string>	//ä˘Ç… +.hÇ≈ì«Ç›çûÇ‹ÇÍÇƒÇ¢ÇÈÇÕÇ∏
#include	<format>
#include	"sff_common.h"
#include	"SFF8024.h"
#include	"sff8636.h"

std::string SFF8472::type(char  data[]) {
	SFF8024 sff8024;
	std::string s = "";
	s = sff8024.exttype(data[36]);
	if (0x80 & data[3]) s += "10G-ER/";
	if (0x40 & data[3]) s += "10G-LRM/";
	if (0x20 & data[3]) s += "10G-LR/";
	if (0x10 & data[3]) s += "10G-SR/";
	if (0x08 & data[3]) s += "IB 1X SX/";
	if (0x04 & data[3]) s += "IB 1X LX/";
	if (0x02 & data[3]) s += "1B 1X ACC/";
	if (0x01 & data[3]) s += "1B 1X DAC/";
	// byte 4, 5 ESCON SONET
	if (0x80 & data[6]) s += "BASE-PX/";
	if (0x40 & data[6]) s += "BASE-BX10/";
	if (0x20 & data[6]) s += "100BASE-FX/";
	if (0x10 & data[6]) s += "100BASE^LX/";
	if (0x08 & data[6]) s += "1000BASE-T/";
	if (0x04 & data[6]) s += "1000BASE-CX/";
	if (0x02 & data[6]) s += "1000BASE-LX/";
	if (0x01 & data[6]) s += "1000BASE-SX/";
	// byte 7,8,9,10 FC
	if (0x80 & data[7]) s += "FC-V/";
	if (0x40 & data[7]) s += "FC-S/";
	if (0x20 & data[7]) s += "FC-I/";
	if (0x10 & data[7]) s += "FC-L/";
	if (0x08 & data[7]) s += "FC-M/";
	if (0x04 & data[7]) s += "FC-SA/";
	if (0x02 & data[7]) s += "FC-LC/";
	if (0x01 & data[7]) s += "FC-EL/";

	if (0x80 & data[8]) s += "FC-EL/";
	if (0x40 & data[8]) s += "FC-SN/";
	if (0x20 & data[8]) s += "FC-SL/";
	if (0x10 & data[8]) s += "FC-LL/";
	if (0x08 & data[8]) s += "SFP+ Active/";
	if (0x04 & data[8]) s += "SFP+ Passive/";

	if (0x80 & data[9]) s += "FC-TW/";
	if (0x40 & data[9]) s += "FC-TP/";
	if (0x20 & data[9]) s += "FC-MI/";
	if (0x10 & data[9]) s += "FC-TV/";
	if (0x08 & data[9]) s += "FC-M6/";
	if (0x04 & data[9]) s += "FC-M5/";

	if (0x80 & data[10]) s += "FC-1200MB/";
	if (0x40 & data[10]) s += "FC-800MB/";
	if (0x20 & data[10]) s += "FC-1600MB/";
	if (0x10 & data[10]) s += "FC-400MB/";
	if (0x08 & data[10]) s += "FC-3200MB/";
	if (0x08 & data[10]) s += "FC-200MB/";
	if (0x02 & data[10]) s += "FC-SPEED2/";
	if (0x01 & data[10]) s += "FC-100MB/";

	if (0x01 & data[62]) s += "FC-64G/";

	return s;
}


std::string SFF8472::decode(char data[]) {

	std::string s;
	SFF_common sfp;
	SFF8024 sff8024;
	SFF8636 sff8636;

	s = "---------- SFF-8636 -------\r\n";
	s += "Identifer : " + sff8024.ident(data[0x00]) + "\r\n";
	s += "Type      : " + SFF8472::type(data) + "\r\n";
	s += "Power Class : " + sff8636.pwrc(data[64]) + "\r\n";
	s += "Vendor Name : " + sfp.nGet(data, 20, 16) + "\r\n";
	s += "Vendor PN   : " + sfp.nGet(data, 40, 16) + "\r\n";
	s += "Vendor OUI  : " + std::format("{:02X}:{:02X}:{:02X}", data[145], data[146], data[147]) + "\r\n";
	s += "Vendor REV  : " + sfp.nGet(data, 56, 4) + "\r\n";
	s += "Vendor SN   : " + sfp.nGet(data, 68, 16) + "\r\n";
	s += "Vendor DATE : " + sfp.nGet(data, 84, 8) + "\r\n";
	s += "connector   : " + sff8024.connector_type(data[2]) + "\r\n";

	s += "Length(SMF) : " + std::to_string(data[14]) + " km\r\n";
	s += "Length(OM3) : " + std::to_string(data[19] * 0.1) + " m\r\n";
	s += "Length(OM2) : " + std::to_string(data[16] * 10) + " m\r\n";
	s += "Length(OM1) : " + std::to_string(data[17] * 10) + " m\r\n";
	s += "Length(OM4) : " + std::to_string(data[18] * 10) + " m\r\n";

	return s;
}