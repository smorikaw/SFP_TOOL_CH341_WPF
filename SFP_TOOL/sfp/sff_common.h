#pragma once

#include	<string>

#define SFF8472_FORMAT 0
#define SFF8636_FORMAT 1
#define CMIS_FORMAT 2

typedef int BOOL;

public ref class SFF_common {
public:
	std::string strnGet(char data[], int addr, int len);
	std::string nGet(char data[], int addr, int len);
	int sff_ID(char data[]);					// SFP or QSFP28 ?
	int format_type(int ident);					// SFF8472,SFF8636 or CMIS?
	std::string sff_IDstr(int f, char data[]);	// "QSFP28"
	std::string sff_PN(int f, char data[]);
	std::string sff_VN(int f, char data[]);
	std::string sff_SN(int f, char data[]);
	std::string sff_REV(int f, char data[]);
	std::string sff_DATE(int f, char data[]);
	std::string sff_OUI(int f, char data[]);
	std::string sff_TYPE(int f, char data[]);

	BOOL sff_CC_check(char data[]);
};	// end of class
