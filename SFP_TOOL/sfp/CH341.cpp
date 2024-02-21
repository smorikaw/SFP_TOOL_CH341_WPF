#include "CH341.h"
#include <stdio.h>
#include <Windows.h>
#include <iostream>
#include <iomanip>
#include <tchar.h>
#include "CH341DLL_EN.H"
// CH341のドライバーとchipのversionを調べる
int CH341::getver(ULONG* dllVersion, ULONG* driverVersion, PVOID p, ULONG* icVersion) {
	ULONG iIndex = 0;		// 一つしかデバイスが無い決め打ち
	// Device Index Number
	HANDLE h = CH341OpenDevice(iIndex);

	// DLL verison
	*dllVersion = CH341GetVersion();
	std::cerr << "DLL verison " << dllVersion << "\n";

	// Driver version
	*driverVersion = CH341GetDrvVersion();
	std::cerr << "Driver verison " << driverVersion << std::endl;

	// Device Name
	p = CH341GetDeviceName(iIndex);
	std::cerr << "Device Name " << (PCHAR)p << std::endl;

	// IC verison 0x10=CH341,0x20=CH341A,0x30=CH341A3
	*icVersion = CH341GetVerIC(iIndex);

	return (int)h;
}
// 指定されたlowr pageをI2経由で読む
int CH341::read_low( char out[], int page)
{
	int i;
	UCHAR data;
	ULONG iIndex = 0;
	std::cerr << "SFP/QSFP/OSFP EEPROM A0h page dump via CH341\n";

	HANDLE h = CH341OpenDevice(iIndex);

	// Reset Device
	BOOL b = CH341ResetDevice(iIndex);
	std::cerr << "Reset Device " << b << std::endl;

	CH341WriteI2C(iIndex, 0x50, 0x7f, page);	// page select

	for (i = 0x80; i < 0xff; i++) {				// 128 byteあけて後半だけに埋める
		BOOL b = CH341ReadI2C(iIndex, 0x50, i, &data);
		//		printf(" %02X :", (0xff & (char)data));
		out[i] = (0xff & (char)data);

	}
	return 0xff;
}
// CH341が使えるか確認する
BOOL CH341::check() {
	std::cerr << "check CH341 driver\n";
	// Device Index Number
	ULONG iIndex = 0;

	// Open Device
	int h = (int)CH341OpenDevice(iIndex);

	//	std::cerr << h;
	if (h < 0) { return false; }
	else; { return true; }
}
//
//初期のpageをI2C経由で読む
int CH341::read_page00(char eprom[])
{
	int i;
	UCHAR data;

	std::cerr << "SFP/QSFP/OSFP EEPROM A0h dump via CH341\n";
	// Device Index Number
	ULONG iIndex = 0;

	// Open Device
	HANDLE h = CH341OpenDevice(iIndex);

	// Reset Device
	BOOL b = CH341ResetDevice(iIndex);
	std::cerr << "Reset Device " << b << std::endl;
	// I2C speed mode (0=20k / 1 = 100K / 2 =400k)
	b = CH341SetStream(iIndex, 1);

	CH341WriteI2C(iIndex, 0x50, 0x7f, 0x00);		// page select 00

	for (i = 0; i < 0xff; i++) {
		BOOL b = CH341ReadI2C(iIndex, 0x50, i, &data);
		printf(" %02X :", (0xff & (char)data));
		eprom[i] = (0xff & (char)data);
		//	Sleep(1);		// delat 1 msec
	}
	return 0xff;
}