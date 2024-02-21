//
// OIF CMIS EEPROM contents class
//
#pragma once
#include	<string>

//CMIS format infomation
#define CMIS_VN		129
#define CMIS_OUI	145
#define CMIS_PN		147
#define CMIS_REV	164
#define CMIS_CC		222
#define CMIS_SN		166
#define CMIS_DATE	182

using namespace std;

public class CMIS52 {
private:
	string pwrc(int code);
	float clen(int code);
public:
	char UPPER[128];
	char PAGE00[256];	// 前半 128バイトは使わない
	char PAGE01[256];	//
	char PAGE02[256];	//
	char PAGE03[256];	//

	char ID();
	std::string VN();
	std::string OUI();
	std::string PN();
	std::string REV();
	std::string SN();
	std::string DATE();
	std::string CLEI();
	std::string PWRC();
	float maxPWR();
	std::string CONNECTOR();
	float LINKLEN();

	int APPC();
	std::string APPHOST(int i);
	std::string mint(int code, int type);
	std::string APPMEDIA(int i);
	std::string APPLANE(int i);
	std::string APPOPT(int i);

	std::string MTech(int code);
	std::string decode(char data[]);
};

