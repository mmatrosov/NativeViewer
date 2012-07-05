#pragma once

namespace NativeViewer {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for FormMain
	/// </summary>
	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
		FormMain(void)
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
		~FormMain()
		{
			if (components)
			{
				delete components;
			}
		}
  private: System::Windows::Forms::StatusStrip^  statusStrip1;
  protected: 
  private: System::Windows::Forms::ToolStripStatusLabel^  toolStripStatusLabel1;
  private: System::Windows::Forms::ToolStripStatusLabel^  toolStripStatusLabel2;

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
      this->statusStrip1 = (gcnew System::Windows::Forms::StatusStrip());
      this->toolStripStatusLabel1 = (gcnew System::Windows::Forms::ToolStripStatusLabel());
      this->toolStripStatusLabel2 = (gcnew System::Windows::Forms::ToolStripStatusLabel());
      this->statusStrip1->SuspendLayout();
      this->SuspendLayout();
      // 
      // statusStrip1
      // 
      this->statusStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {this->toolStripStatusLabel1, 
        this->toolStripStatusLabel2});
      this->statusStrip1->Location = System::Drawing::Point(0, 240);
      this->statusStrip1->Name = L"statusStrip1";
      this->statusStrip1->Size = System::Drawing::Size(284, 22);
      this->statusStrip1->TabIndex = 0;
      this->statusStrip1->Text = L"statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this->toolStripStatusLabel1->Name = L"toolStripStatusLabel1";
      this->toolStripStatusLabel1->Size = System::Drawing::Size(34, 17);
      this->toolStripStatusLabel1->Text = L"8uC3";
      // 
      // toolStripStatusLabel2
      // 
      this->toolStripStatusLabel2->Name = L"toolStripStatusLabel2";
      this->toolStripStatusLabel2->Size = System::Drawing::Size(54, 17);
      this->toolStripStatusLabel2->Text = L"1024x768";
      // 
      // FormMain
      // 
      this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
      this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
      this->ClientSize = System::Drawing::Size(284, 262);
      this->Controls->Add(this->statusStrip1);
      this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::None;
      this->Name = L"FormMain";
      this->Text = L"FormMain";
      this->statusStrip1->ResumeLayout(false);
      this->statusStrip1->PerformLayout();
      this->ResumeLayout(false);
      this->PerformLayout();

    }
#pragma endregion
	};
}
