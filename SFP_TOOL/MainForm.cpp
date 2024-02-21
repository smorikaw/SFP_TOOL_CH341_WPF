//
#include	<string>
#include	"MainForm.h"
#include	"DDMForm.h"
#include	"I2CForm.h"
#include	"./sfp/sff_common.h"
#include	"./sfp/SFF8024.h"
#include	"./sfp/SFF8472.h"
#include	"./sfp/sff8636.h"
#include	"./sfp/CMIS52.h"
#pragma comment(lib,"./CH341/CH341DLLA64.LIB")

using namespace System;
using namespace System::Windows::Forms;

//void main(array<String^>^ args)
int main()
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	FirstCPPApp::MainForm form;
//	FirstCPPApp::DDMForm ddmform;
//	FirstCPPApp::I2CForm i2cform;
	Application::Run(% form);
}

//
//  EEPROM finromation decode with char [] style
//
void sff_eth(char data[], char out[]) {

	std::string s;
	SFF_common sfp;
	SFF8472 sff8472;
	SFF8636 sff8636;
	CMIS52 cmis;
	switch (sfp.format_type(sfp.sff_ID(data))) {
	case SFF8472_FORMAT:s = sff8472.decode(data); break;
	case SFF8636_FORMAT:s = sff8636.decode(data); break;
	case CMIS_FORMAT:		s = cmis.decode(data);	break;
	}
	std::strcpy(out, s.c_str());
}