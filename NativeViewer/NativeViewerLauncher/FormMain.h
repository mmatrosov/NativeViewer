#pragma once

#include "EEAddInWrapper.h"

namespace NativeViewerLauncher {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
  using namespace System::IO;

	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
    static String^ data_dir = "../../data/";

		FormMain(void)
		{
			InitializeComponent();

      array<String^>^ files = Directory::GetFiles(data_dir);

      for (int i = 0; i < files->Length; ++i)
      {
        listBoxImages->Items->Add(Path::GetFileName(files[i]));
      }

      listBoxImages_SelectedIndexChanged(this, nullptr);
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
  private: System::Windows::Forms::ListBox^  listBoxImages;

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
      this->listBoxImages = (gcnew System::Windows::Forms::ListBox());
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
      // listBoxImages
      // 
      this->listBoxImages->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom) 
        | System::Windows::Forms::AnchorStyles::Left) 
        | System::Windows::Forms::AnchorStyles::Right));
      this->listBoxImages->FormattingEnabled = true;
      this->listBoxImages->Location = System::Drawing::Point(12, 67);
      this->listBoxImages->Name = L"listBoxImages";
      this->listBoxImages->Size = System::Drawing::Size(339, 186);
      this->listBoxImages->TabIndex = 2;
      this->listBoxImages->SelectedIndexChanged += gcnew System::EventHandler(this, &FormMain::listBoxImages_SelectedIndexChanged);
      // 
      // FormMain
      // 
      this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
      this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
      this->ClientSize = System::Drawing::Size(363, 263);
      this->Controls->Add(this->listBoxImages);
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
             String^ file_name = dynamic_cast<String^>(listBoxImages->Items[listBoxImages->SelectedIndex]);

             msclr::interop::marshal_context context;

             cv::Mat img = cv::imread(context.marshal_as<std::string>(data_dir + file_name), -1);

             String^ result = CallEEAddIn(img);

             textBoxResult->Text = result;
           }

  private: System::Void listBoxImages_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e) 
           {
             buttonCall->Enabled = listBoxImages->SelectedIndex >= 0;
           }
  };
}

