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
		FormMain(Image^ image)
		{
			InitializeComponent();

      pictureBoxThumbnail->Image = image;
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
  private: System::Windows::Forms::StatusStrip^  statusStripMain;
  private: System::Windows::Forms::ToolStripStatusLabel^  toolStripStatusLabelDepth;
  private: System::Windows::Forms::ToolStripStatusLabel^  toolStripStatusLabelSize;
  private: System::Windows::Forms::ToolStripStatusLabel^  toolStripStatusLabelScale;
  protected: 

  protected: 



  private: System::Windows::Forms::PictureBox^  pictureBoxThumbnail;

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
      this->statusStripMain = (gcnew System::Windows::Forms::StatusStrip());
      this->toolStripStatusLabelDepth = (gcnew System::Windows::Forms::ToolStripStatusLabel());
      this->toolStripStatusLabelSize = (gcnew System::Windows::Forms::ToolStripStatusLabel());
      this->toolStripStatusLabelScale = (gcnew System::Windows::Forms::ToolStripStatusLabel());
      this->pictureBoxThumbnail = (gcnew System::Windows::Forms::PictureBox());
      this->statusStripMain->SuspendLayout();
      (cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBoxThumbnail))->BeginInit();
      this->SuspendLayout();
      // 
      // statusStripMain
      // 
      this->statusStripMain->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(3) {this->toolStripStatusLabelDepth, 
        this->toolStripStatusLabelSize, this->toolStripStatusLabelScale});
      this->statusStripMain->Location = System::Drawing::Point(0, 240);
      this->statusStripMain->Name = L"statusStripMain";
      this->statusStripMain->Size = System::Drawing::Size(284, 22);
      this->statusStripMain->TabIndex = 0;
      this->statusStripMain->Text = L"statusStrip1";
      // 
      // toolStripStatusLabelDepth
      // 
      this->toolStripStatusLabelDepth->Name = L"toolStripStatusLabelDepth";
      this->toolStripStatusLabelDepth->Size = System::Drawing::Size(34, 17);
      this->toolStripStatusLabelDepth->Text = L"8uC3";
      // 
      // toolStripStatusLabelSize
      // 
      this->toolStripStatusLabelSize->Name = L"toolStripStatusLabelSize";
      this->toolStripStatusLabelSize->Size = System::Drawing::Size(54, 17);
      this->toolStripStatusLabelSize->Text = L"1024x768";
      // 
      // toolStripStatusLabelScale
      // 
      this->toolStripStatusLabelScale->Name = L"toolStripStatusLabelScale";
      this->toolStripStatusLabelScale->Size = System::Drawing::Size(35, 17);
      this->toolStripStatusLabelScale->Text = L"100%";
      // 
      // pictureBoxThumbnail
      // 
      this->pictureBoxThumbnail->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom) 
        | System::Windows::Forms::AnchorStyles::Left) 
        | System::Windows::Forms::AnchorStyles::Right));
      this->pictureBoxThumbnail->Location = System::Drawing::Point(12, 12);
      this->pictureBoxThumbnail->Name = L"pictureBoxThumbnail";
      this->pictureBoxThumbnail->Size = System::Drawing::Size(260, 225);
      this->pictureBoxThumbnail->SizeMode = System::Windows::Forms::PictureBoxSizeMode::Zoom;
      this->pictureBoxThumbnail->TabIndex = 1;
      this->pictureBoxThumbnail->TabStop = false;
      // 
      // FormMain
      // 
      this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
      this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
      this->ClientSize = System::Drawing::Size(284, 262);
      this->Controls->Add(this->pictureBoxThumbnail);
      this->Controls->Add(this->statusStripMain);
      this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::SizableToolWindow;
      this->Name = L"FormMain";
      this->Text = L"FormMain";
      this->statusStripMain->ResumeLayout(false);
      this->statusStripMain->PerformLayout();
      (cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBoxThumbnail))->EndInit();
      this->ResumeLayout(false);
      this->PerformLayout();

    }
#pragma endregion
	};
}
