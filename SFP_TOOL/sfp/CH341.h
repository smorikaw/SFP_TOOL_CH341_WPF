#pragma once
#include <stdio.h>
#include <Windows.h>
#include <iostream>
#include <iomanip>
#include <tchar.h>

// ref class�ɂ���ƃ������[�Ǘ��������I�ɂ���Ă����Agcnew����delete�Y��Ă����v
//
// CH341�֘A�̑���𕪗�
//
public class CH341
{
public:
	// CH341�̃f�o�C�X���Őڑ�����Ă��邩���ׂ�
	int getver(ULONG* dllVersion, ULONG* driverVersion, PVOID p, ULONG* icVersion);
	int read_low(char eeprom[], int p);
	BOOL check();
	int read_page00(char eprom[]);
};
