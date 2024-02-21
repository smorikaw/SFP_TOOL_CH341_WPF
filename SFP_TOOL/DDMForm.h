#pragma once

namespace FirstCPPApp {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// MyForm1 の概要
	/// </summary>
	public ref class DDMForm : public System::Windows::Forms::Form
	{
	public:
		DDMForm(void)
		{
			InitializeComponent();
			//
			//TODO: ここにコンストラクター コードを追加します
			//
		}

	protected:
		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		~DDMForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::CheckBox^ checkBox1;




































	private: System::Windows::Forms::CheckBox^ checkBox2;




	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::DataGridView^ DDMGrid;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ LANE;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ RXPWR;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ TXBIAS;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ TXPWR;













	protected:

	private:
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		void InitializeComponent(void)
		{
			this->checkBox1 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox2 = (gcnew System::Windows::Forms::CheckBox());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->DDMGrid = (gcnew System::Windows::Forms::DataGridView());
			this->LANE = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->RXPWR = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->TXBIAS = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->TXPWR = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->DDMGrid))->BeginInit();
			this->SuspendLayout();
			// 
			// checkBox1
			// 
			this->checkBox1->AutoSize = true;
			this->checkBox1->Location = System::Drawing::Point(51, 7);
			this->checkBox1->Name = L"checkBox1";
			this->checkBox1->Size = System::Drawing::Size(64, 16);
			this->checkBox1->TabIndex = 0;
			this->checkBox1->Text = L"LPmode";
			this->checkBox1->UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this->checkBox2->AutoSize = true;
			this->checkBox2->Location = System::Drawing::Point(133, 7);
			this->checkBox2->Name = L"checkBox2";
			this->checkBox2->Size = System::Drawing::Size(84, 16);
			this->checkBox2->TabIndex = 0;
			this->checkBox2->Text = L"auto update";
			this->checkBox2->UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this->button1->BackColor = System::Drawing::SystemColors::ActiveCaptionText;
			this->button1->ForeColor = System::Drawing::SystemColors::ButtonHighlight;
			this->button1->Location = System::Drawing::Point(223, 3);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(75, 23);
			this->button1->TabIndex = 4;
			this->button1->Text = L"update";
			this->button1->UseVisualStyleBackColor = false;
			// 
			// DDMGrid
			// 
			this->DDMGrid->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->DDMGrid->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(4) {
				this->LANE, this->RXPWR,
					this->TXBIAS, this->TXPWR
			});
			this->DDMGrid->Location = System::Drawing::Point(0, 95);
			this->DDMGrid->Name = L"DDMGrid";
			this->DDMGrid->RowTemplate->Height = 21;
			this->DDMGrid->Size = System::Drawing::Size(305, 187);
			this->DDMGrid->TabIndex = 5;
			this->DDMGrid->CellContentClick += gcnew System::Windows::Forms::DataGridViewCellEventHandler(this, &DDMForm::dataGridView1_CellContentClick);
			// 
			// LANE
			// 
			this->LANE->HeaderText = L"LANE";
			this->LANE->Name = L"LANE";
			this->LANE->Width = 30;
			// 
			// RXPWR
			// 
			this->RXPWR->HeaderText = L"RX power";
			this->RXPWR->MinimumWidth = 40;
			this->RXPWR->Name = L"RXPWR";
			this->RXPWR->ReadOnly = true;
			this->RXPWR->Width = 60;
			// 
			// TXBIAS
			// 
			this->TXBIAS->HeaderText = L"TX bias";
			this->TXBIAS->MinimumWidth = 40;
			this->TXBIAS->Name = L"TXBIAS";
			this->TXBIAS->ReadOnly = true;
			this->TXBIAS->Width = 60;
			// 
			// TXPWR
			// 
			this->TXPWR->HeaderText = L"TX power";
			this->TXPWR->MinimumWidth = 40;
			this->TXPWR->Name = L"TXPWR";
			this->TXPWR->ReadOnly = true;
			this->TXPWR->Width = 60;
			// 
			// MyForm1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(304, 281);
			this->Controls->Add(this->DDMGrid);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->checkBox2);
			this->Controls->Add(this->checkBox1);
			this->MaximumSize = System::Drawing::Size(320, 320);
			this->MinimumSize = System::Drawing::Size(320, 320);
			this->Name = L"MyForm1";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"RealTime DDM";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->DDMGrid))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void dataGridView1_CellContentClick(System::Object^ sender, System::Windows::Forms::DataGridViewCellEventArgs^ e) {
	}
};
}
