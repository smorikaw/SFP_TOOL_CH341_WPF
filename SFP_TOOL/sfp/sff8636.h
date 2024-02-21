#pragma once
#include	<string>


// SFF-8636 format infomation
#define SFF8636_VN	148
#define SFF8636_OUI	165
#define SFF8636_PN	168
#define SFF8636_REV	184
#define SFF8636_CC_BASE	191
#define SFF8636_SN	196
#define SFF8636_DATE	212

public ref class SFF8636 {
	public:
		std::string pwrc(int code);
		std::string decode(char data[]);
};


