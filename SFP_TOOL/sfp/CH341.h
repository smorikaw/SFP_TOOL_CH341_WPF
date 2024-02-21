#pragma once
#include <stdio.h>
#include <Windows.h>
#include <iostream>
#include <iomanip>
#include <tchar.h>

// ref classにするとメモリー管理を自動的にやってくれる、gcnewしてdelete忘れても大丈夫
//
// CH341関連の操作を分離
//
public class CH341
{
public:
	// CH341のデバイスがで接続されているか調べる
	int getver(ULONG* dllVersion, ULONG* driverVersion, PVOID p, ULONG* icVersion);
	int read_low(char eeprom[], int p);
	BOOL check();
	int read_page00(char eprom[]);
};
