#pragma once
#include <stdio.h>
#include	"sfp/sff_common.h"
#include	"sfp/CH341.h"
#include	"DDMForm.h"
#include	"I2CForm.h"
void sff_eth(char data[], char out[]);

char EEPROM[256];	// A0h upper and page 00 store
char PAGE00[256];	//
char PAGE01[256];	//
char PAGE02[256];	//
char PAGE03[256];	//

namespace FirstCPPApp {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MainForm
	/// </summary>
	public ref class MainForm : public System::Windows::Forms::Form
	{
	public:
		MainForm(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~MainForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^ textBox;
	private: System::Windows::Forms::MenuStrip^ menuStrip1;
	private: System::Windows::Forms::ToolStripMenuItem^ fiileToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ openToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ savebinToolStripMenuItem;
	private: System::Windows::Forms::ToolStripSeparator^ toolStripSeparator2;
	private: System::Windows::Forms::ToolStripMenuItem^ exitToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ i2CToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ readToolStripMenuItem;
	private: System::Windows::Forms::ToolStripSeparator^ toolStripSeparator1;
	private: System::Windows::Forms::ToolStripMenuItem^ writeUpperToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ writePage00ToolStripMenuItem;
	private: System::Windows::Forms::ToolStripSeparator^ toolStripSeparator3;
	private: System::Windows::Forms::ToolStripMenuItem^ setupToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ viewToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ otherToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ infoToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ fontToolStripMenuItem;
	private: System::Windows::Forms::ToolStripSeparator^ toolStripSeparator4;
	private: System::Windows::Forms::ToolStripMenuItem^ dDMToolStripMenuItem;

	protected:

	protected:








	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->textBox = (gcnew System::Windows::Forms::TextBox());
			this->menuStrip1 = (gcnew System::Windows::Forms::MenuStrip());
			this->fiileToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->openToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->savebinToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->toolStripSeparator2 = (gcnew System::Windows::Forms::ToolStripSeparator());
			this->exitToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->i2CToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->readToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->toolStripSeparator1 = (gcnew System::Windows::Forms::ToolStripSeparator());
			this->writeUpperToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->writePage00ToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->toolStripSeparator3 = (gcnew System::Windows::Forms::ToolStripSeparator());
			this->setupToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->viewToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->fontToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->otherToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->infoToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->dDMToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->toolStripSeparator4 = (gcnew System::Windows::Forms::ToolStripSeparator());
			this->menuStrip1->SuspendLayout();
			this->SuspendLayout();
			// 
			// textBox
			// 
			this->textBox->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
				| System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->textBox->Font = (gcnew System::Drawing::Font(L"ÇlÇr ÉSÉVÉbÉN", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(128)));
			this->textBox->Location = System::Drawing::Point(-1, 24);
			this->textBox->Multiline = true;
			this->textBox->Name = L"textBox";
			this->textBox->ScrollBars = System::Windows::Forms::ScrollBars::Both;
			this->textBox->Size = System::Drawing::Size(383, 341);
			this->textBox->TabIndex = 0;
			this->textBox->TextChanged += gcnew System::EventHandler(this, &MainForm::textBox_TextChanged);
			// 
			// menuStrip1
			// 
			this->menuStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(4) {
				this->fiileToolStripMenuItem,
					this->i2CToolStripMenuItem, this->viewToolStripMenuItem, this->otherToolStripMenuItem
			});
			this->menuStrip1->Location = System::Drawing::Point(0, 0);
			this->menuStrip1->Name = L"menuStrip1";
			this->menuStrip1->Size = System::Drawing::Size(384, 24);
			this->menuStrip1->TabIndex = 1;
			this->menuStrip1->Text = L"menuStrip1";
			// 
			// fiileToolStripMenuItem
			// 
			this->fiileToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(4) {
				this->openToolStripMenuItem,
					this->savebinToolStripMenuItem, this->toolStripSeparator2, this->exitToolStripMenuItem
			});
			this->fiileToolStripMenuItem->Name = L"fiileToolStripMenuItem";
			this->fiileToolStripMenuItem->Size = System::Drawing::Size(40, 20);
			this->fiileToolStripMenuItem->Text = L"Fiile";
			// 
			// openToolStripMenuItem
			// 
			this->openToolStripMenuItem->Name = L"openToolStripMenuItem";
			this->openToolStripMenuItem->Size = System::Drawing::Size(131, 22);
			this->openToolStripMenuItem->Text = L"Open";
			// 
			// savebinToolStripMenuItem
			// 
			this->savebinToolStripMenuItem->Name = L"savebinToolStripMenuItem";
			this->savebinToolStripMenuItem->Size = System::Drawing::Size(131, 22);
			this->savebinToolStripMenuItem->Text = L"Save(*.bin)";
			// 
			// toolStripSeparator2
			// 
			this->toolStripSeparator2->Name = L"toolStripSeparator2";
			this->toolStripSeparator2->Size = System::Drawing::Size(128, 6);
			// 
			// exitToolStripMenuItem
			// 
			this->exitToolStripMenuItem->Name = L"exitToolStripMenuItem";
			this->exitToolStripMenuItem->Size = System::Drawing::Size(131, 22);
			this->exitToolStripMenuItem->Text = L"Exit";
			// 
			// i2CToolStripMenuItem
			// 
			this->i2CToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(8) {
				this->readToolStripMenuItem,
					this->toolStripSeparator1, this->writeUpperToolStripMenuItem, this->writePage00ToolStripMenuItem, this->toolStripSeparator4,
					this->dDMToolStripMenuItem, this->toolStripSeparator3, this->setupToolStripMenuItem
			});
			this->i2CToolStripMenuItem->Name = L"i2CToolStripMenuItem";
			this->i2CToolStripMenuItem->Size = System::Drawing::Size(35, 20);
			this->i2CToolStripMenuItem->Text = L"I2C";
			// 
			// readToolStripMenuItem
			// 
			this->readToolStripMenuItem->Name = L"readToolStripMenuItem";
			this->readToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->readToolStripMenuItem->Text = L"read";
			this->readToolStripMenuItem->Click += gcnew System::EventHandler(this, &MainForm::readToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this->toolStripSeparator1->Name = L"toolStripSeparator1";
			this->toolStripSeparator1->Size = System::Drawing::Size(177, 6);
			// 
			// writeUpperToolStripMenuItem
			// 
			this->writeUpperToolStripMenuItem->Name = L"writeUpperToolStripMenuItem";
			this->writeUpperToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->writeUpperToolStripMenuItem->Text = L"write upper";
			// 
			// writePage00ToolStripMenuItem
			// 
			this->writePage00ToolStripMenuItem->Name = L"writePage00ToolStripMenuItem";
			this->writePage00ToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->writePage00ToolStripMenuItem->Text = L"write page00";
			// 
			// toolStripSeparator3
			// 
			this->toolStripSeparator3->Name = L"toolStripSeparator3";
			this->toolStripSeparator3->Size = System::Drawing::Size(177, 6);
			// 
			// setupToolStripMenuItem
			// 
			this->setupToolStripMenuItem->Name = L"setupToolStripMenuItem";
			this->setupToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->setupToolStripMenuItem->Text = L"setup";
			// 
			// viewToolStripMenuItem
			// 
			this->viewToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->fontToolStripMenuItem });
			this->viewToolStripMenuItem->Name = L"viewToolStripMenuItem";
			this->viewToolStripMenuItem->Size = System::Drawing::Size(44, 20);
			this->viewToolStripMenuItem->Text = L"View";
			// 
			// fontToolStripMenuItem
			// 
			this->fontToolStripMenuItem->Name = L"fontToolStripMenuItem";
			this->fontToolStripMenuItem->Size = System::Drawing::Size(98, 22);
			this->fontToolStripMenuItem->Text = L"Font";
			// 
			// otherToolStripMenuItem
			// 
			this->otherToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->infoToolStripMenuItem });
			this->otherToolStripMenuItem->Name = L"otherToolStripMenuItem";
			this->otherToolStripMenuItem->Size = System::Drawing::Size(49, 20);
			this->otherToolStripMenuItem->Text = L"Other";
			// 
			// infoToolStripMenuItem
			// 
			this->infoToolStripMenuItem->Name = L"infoToolStripMenuItem";
			this->infoToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->infoToolStripMenuItem->Text = L"info";
			// 
			// dDMToolStripMenuItem
			// 
			this->dDMToolStripMenuItem->Name = L"dDMToolStripMenuItem";
			this->dDMToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->dDMToolStripMenuItem->Text = L"DDM";
			this->dDMToolStripMenuItem->Click += gcnew System::EventHandler(this, &MainForm::dDMToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this->toolStripSeparator4->Name = L"toolStripSeparator4";
			this->toolStripSeparator4->Size = System::Drawing::Size(177, 6);
			// 
			// MainForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(192)), static_cast<System::Int32>(static_cast<System::Byte>(192)),
				static_cast<System::Int32>(static_cast<System::Byte>(255)));
			this->ClientSize = System::Drawing::Size(384, 361);
			this->Controls->Add(this->textBox);
			this->Controls->Add(this->menuStrip1);
			this->MainMenuStrip = this->menuStrip1;
			this->Margin = System::Windows::Forms::Padding(2);
			this->MaximizeBox = false;
			this->MaximumSize = System::Drawing::Size(800, 1400);
			this->MinimizeBox = false;
			this->MinimumSize = System::Drawing::Size(400, 400);
			this->Name = L"MainForm";
			this->Text = L"SFP_TOOL";
			this->menuStrip1->ResumeLayout(false);
			this->menuStrip1->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

private: System::Void readToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e) {

	CH341 usb;
	SFF_common sfp;

	if (usb.check()) {
		usb.read_page00(EEPROM);		// read I2C A0h bytes 0-255( upper and page 00)
		usb.read_low(PAGE00, 0);		// 
		usb.read_low(PAGE01, 1);		// 
		usb.read_low(PAGE02, 2);		// 
		usb.read_low(PAGE03, 3);		// 
		disp_HEX(EEPROM);
		disp_HEX_low(PAGE01);
		disp_HEX_low(PAGE02);
		disp_HEX_low(PAGE03);
		if (sfp.sff_CC_check(EEPROM)) {

			char buf[2048];
			sff_eth(EEPROM, buf);		// Mainform.cpp	
			this->textBox->Text += gcnew String(buf);
		}
	}
	else {
		MessageBox::Show("CH341 USB open error", "Validation", MessageBoxButtons::OK, MessageBoxIcon::Error);
	}

}
	   void disp_HEX(char eprom[]) {			//			textHEX->Text = "";

		   int i, j;
		   char buf[1024];		// more 3 bytes "00 " x 256

		   this->textBox->Text = "";

		   for (i = 0; i < 16; i++) {
			   sprintf(buf, "%02X: ", i<<4);
			   this->textBox->Text += gcnew String(buf);
			   //this->textHEX->Text += format("{:%02X} : ", i);
			   for (j = 0; j < 0x10; j++) {
				   sprintf(buf, "%02X ", (0xff & EEPROM[(i * 0x10) + j]));
				   this->textBox->Text += gcnew String(buf);
				   //this->textHEX->Text += format("{:%02X} ", EEPROM[(i * 0x10) + j]);
				   //		this->textHEX->Text += "BB ";
			   }
			   this->textBox->Text += "\r\n";
		   }//	end of i loop
	   }
	   void disp_HEX_low(char eprom[]) {			//			textHEX->Text = "";

		   int i, j;
		   char buf[1024];		// more 3 bytes "00 " x 256

		   for (i = 8; i < 16; i++) {
			   sprintf(buf, "%02X: ", i << 4);
			   this->textBox->Text += gcnew String(buf);
			   //this->textHEX->Text += format("{:%02X} : ", i);
			   for (j = 0; j < 0x10; j++) {
				   sprintf(buf, "%02X ", (0xff & EEPROM[(i * 0x10) + j]));
				   this->textBox->Text += gcnew String(buf);
				   //this->textHEX->Text += format("{:%02X} ", EEPROM[(i * 0x10) + j]);
				   //		this->textHEX->Text += "BB ";
			   }
			   this->textBox->Text += "\r\n";
		   }//	end of i loop
	   }

private: System::Void textBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
}
private: System::Void dDMToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e) {
//	DDMForm ddmf = new DDMForm();
//	FirstCPPApp::DDMForm ddmf;
	DDMForm ddmf = gcnew DDMForm();
	ddmf.Show();
}
};
}
