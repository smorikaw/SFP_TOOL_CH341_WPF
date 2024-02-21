#pragma once
#include	<string>

// SFF-8472 format infomation
#define SFF8472_CON	2
#define SFF8472_VN	20
#define SFF8472_PN	40
#define SFF8472_OUI	37
#define SFF8472_REV	56
#define SFF8472_CC_BASE	63
#define SFF8472_SN	68
#define SFF8472_DATE	84

public ref class SFF8472
{
public:
	std::string  type(char data[]);
	std::string  decode(char data[]);
};

