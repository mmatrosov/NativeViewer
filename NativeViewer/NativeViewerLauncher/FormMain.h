#pragma once

#include "EEAddInWrapper.h"

namespace NativeViewerLauncher {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
		FormMain(void)
		{
			InitializeComponent();
		}

	protected:
		~FormMain()
		{
			if (components)
			{
				delete components;
			}
		}

  private: System::Windows::Forms::Button^ buttonCall;
  private: System::Windows::Forms::TextBox^ textBoxResult;

	private: System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
      this->buttonCall = (gcnew System::Windows::Forms::Button());
      this->textBoxResult = (gcnew System::Windows::Forms::TextBox());
      this->SuspendLayout();
      // 
      // buttonCall
      // 
      this->buttonCall->Location = System::Drawing::Point(12, 12);
      this->buttonCall->Name = L"buttonCall";
      this->buttonCall->Size = System::Drawing::Size(172, 23);
      this->buttonCall->TabIndex = 0;
      this->buttonCall->Text = L"Call NativeViewer EEAddIn";
      this->buttonCall->UseVisualStyleBackColor = true;
      this->buttonCall->Click += gcnew System::EventHandler(this, &FormMain::buttonCall_Click);
      // 
      // textBoxResult
      // 
      this->textBoxResult->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Left) 
        | System::Windows::Forms::AnchorStyles::Right));
      this->textBoxResult->Location = System::Drawing::Point(12, 41);
      this->textBoxResult->Name = L"textBoxResult";
      this->textBoxResult->Size = System::Drawing::Size(339, 20);
      this->textBoxResult->TabIndex = 1;
      // 
      // FormMain
      // 
      this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
      this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
      this->ClientSize = System::Drawing::Size(363, 77);
      this->Controls->Add(this->textBoxResult);
      this->Controls->Add(this->buttonCall);
      this->Name = L"FormMain";
      this->Text = L"NativeViewerLauncher";
      this->ResumeLayout(false);
      this->PerformLayout();

    }
#pragma endregion

  private: System::Void buttonCall_Click(System::Object^ sender, System::EventArgs^ e)
           {
             cv::Mat img = cv::imread("../../data/mountains.jpg", -1);

             String^ result = CallEEAddIn(img);

             textBoxResult->Text = result;
           }
  };
}

