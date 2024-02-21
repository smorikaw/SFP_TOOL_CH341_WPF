#include "sff_common.h"
#include	<string>
#include	<format>
#include	"SFF8024.h"
#include	"SFF8472.h"
#include	"CMIS52.h"

std::string SFF_common::strnGet(char data[], int addr, int len) {
	int i, j;

	std::string s = "";
	j = 0;
	for (i = addr; j < len; i++) {
		j++;
		s += std::to_string(data[i]);
	}

	return s;
}
std::string SFF_common::nGet(char data[], int addr, int len) {
	int i, j;
	char str[255 + 1];

	j = 0;
	for (i = addr; j < len; i++) {
		str[j++] = data[i];

	}
	str[j] = 0x00;
	// delete no ASCII

	for (i = 0; i < len; i++) {
		if (str[i] < 0x20 || str[i] > 0x7f) str[i] = '.';
	}
	// strip space
	for (i = len - 1; i > 0; i--) {
		if (str[i] == 0x20) str[i] = 0x00;
	}

	std::string s = str;

	return s;
	//return System::String::String(str);
	//return std::format("[%s}", str);
}
int SFF_common::format_type(int ident) {
	int ret = 0;
	switch (ident) {
	case 0x0d:
	case 0x11:
		ret = SFF8636_FORMAT;	break;
	case 0x18:
	case 0x19:
		ret = CMIS_FORMAT;	break;
	default:
		ret = SFF8472_FORMAT;
	}
	return ret;
}
std::string SFF_common::sff_IDstr(int f, char data[]) {
	int offset = 0;
	SFF8024 sff8024;
	switch (f) {
	case SFF8472_FORMAT:	offset = 0; break;
	case SFF8636_FORMAT:	offset = 0x80; break;
	case CMIS_FORMAT:		offset = 0x80; break;
	}
	return sff8024.ident(data[offset]);
}
std::string SFF_common::sff_PN(int f, char data[]) {
	int offset = 0;
	switch (f) {
	case SFF8472_FORMAT:	offset = 40; break;
	case SFF8636_FORMAT:	offset = 168; break;
	case CMIS_FORMAT:		offset = 146; break;
	}
	return nGet(data, offset, 16);
}
std::string SFF_common::sff_VN(int f, char data[]) {
	int offset = 0;
	switch (f) {
	case SFF8472_FORMAT:	offset = 20; break;
	case SFF8636_FORMAT:	offset = 148; break;
	case CMIS_FORMAT:		offset = 129; break;
	}
	return nGet(data, offset, 16);
}
std::string SFF_common::sff_DATE(int f, char data[]) {
	int offset = 0;
	switch (f) {
	case SFF8472_FORMAT:	offset = 84; break;
	case SFF8636_FORMAT:	offset = 212; break;
	case CMIS_FORMAT:		offset = 182; break;
	}
	return nGet(data, offset, 8);
}
std::string SFF_common::sff_SN(int f, char data[]) {
	int offset = 0;
	switch (f) {
	case SFF8472_FORMAT:	offset = 40; break;
	case SFF8636_FORMAT:	offset = 196; break;
	case CMIS_FORMAT:		offset = 166; break;
	}
	return nGet(data, offset, 16);
}
std::string SFF_common::sff_REV(int f, char data[]) {
	int offset = 0, len = 0;
	switch (f) {
	case SFF8472_FORMAT:	offset = 40; len = 4; break;
	case SFF8636_FORMAT:	offset = 184; len = 2; break;
	case CMIS_FORMAT:		offset = 166; len = 2; break;
	}
	return nGet(data, offset, len);
}
std::string SFF_common::sff_TYPE(int f, char data[]) {
	SFF8472 sff8472;
	SFF8024 sff8024;
	CMIS52 cmis;
	std::string s;
	switch (f) {
	case SFF8472_FORMAT:	s = sff8472.type(data); break;
	case SFF8636_FORMAT:	s = sff8024.exttype(data[0x81]); break;
	case CMIS_FORMAT:		std::memcpy(cmis.UPPER, data, 0x7f);
							s = cmis.APPMEDIA(1);	break;
	}
	return s;
}
std::string SFF_common::sff_OUI(int f, char data[]) {
	int offset = 0;
	switch (f) {
	case SFF8472_FORMAT:	offset = 40;	break;
	case SFF8636_FORMAT:	offset = 165;	break;
	case CMIS_FORMAT:		offset = 145;	break;
	}
	return std::format("{:02X}:{:02X}:{:02X}",data[offset], data[offset+1], data[offset+2]);
}
//  あとで精査、　upper or lowerを考慮する必要あり
int SFF_common::sff_ID(char data[]) {
	//	MessageBox::Show("sff type");
	int ret = 0xff;
	//	まずQSFPであるかを調べる
	switch (data[0x80]) {
	case 0x0d:
	case 0x11:
	case 0x18:
	case 0x19:
		ret = data[0x80];	break;
	default:
		ret = data[0x00];
	}
	return ret;
}
// ccheck sum check
// SFF-8472 : CC_BASE 0 to 62 on 63, 
// SFF-8636 : CC_BASE 128 to 190 on 191, CC_EXT 192 to 222 on 223
// CMIS 5.2 : 128 to 221 on 222
//
BOOL SFF_common::sff_CC_check(char data[]) {
	int i, cc1 = 0, cc2 = 0;
	BOOL ret = false;
	switch (format_type(sff_ID(data))) {
	case SFF8472_FORMAT:			// SFP/SFP+/SFP28
		for (i = 0; i < 63; i++) {
			cc1 += data[i];
		}
		if ((0xff & cc1) == (0xff & data[63])) ret = true;
		break;
	case SFF8636_FORMAT:			// QSFP+
		for (i = 128; i < 191; i++) {
			cc1 += data[i];
		}
		if ((0xff & cc1) == (0xff & data[191])) ret = true;
		break;
	case CMIS_FORMAT:			// OSFP
		for (i = 128; i < 222; i++) {
			cc1 += data[i];
		}
		if ((0xff & cc1) == (0xff & data[222])) ret = true;
		break;
	}
	return ret;
}
