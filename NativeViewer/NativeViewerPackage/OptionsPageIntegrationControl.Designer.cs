namespace NativeViewerPackage
{
  partial class OptionsPageIntegrationControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsPageIntegrationControl));
      this.groupBoxContainer = new System.Windows.Forms.GroupBox();
      this.LabelStatus = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.ButtonRemoveEntry = new System.Windows.Forms.Button();
      this.ButtonAddEntry = new System.Windows.Forms.Button();
      this.textBoxHelp = new System.Windows.Forms.TextBox();
      this.ButtonCheckStatus = new System.Windows.Forms.Button();
      this.groupBoxContainer.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBoxContainer
      // 
      this.groupBoxContainer.Controls.Add(this.LabelStatus);
      this.groupBoxContainer.Controls.Add(this.label1);
      this.groupBoxContainer.Controls.Add(this.ButtonRemoveEntry);
      this.groupBoxContainer.Controls.Add(this.ButtonAddEntry);
      this.groupBoxContainer.Controls.Add(this.textBoxHelp);
      this.groupBoxContainer.Controls.Add(this.ButtonCheckStatus);
      this.groupBoxContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBoxContainer.Location = new System.Drawing.Point(0, 0);
      this.groupBoxContainer.Name = "groupBoxContainer";
      this.groupBoxContainer.Size = new System.Drawing.Size(460, 334);
      this.groupBoxContainer.TabIndex = 0;
      this.groupBoxContainer.TabStop = false;
      // 
      // LabelStatus
      // 
      this.LabelStatus.AutoSize = true;
      this.LabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.LabelStatus.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LabelStatus.Location = new System.Drawing.Point(237, 19);
      this.LabelStatus.Name = "LabelStatus";
      this.LabelStatus.Size = new System.Drawing.Size(111, 13);
      this.LabelStatus.TabIndex = 6;
      this.LabelStatus.Text = "Status Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(150, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(91, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Integration status:";
      // 
      // ButtonRemoveEntry
      // 
      this.ButtonRemoveEntry.Location = new System.Drawing.Point(149, 43);
      this.ButtonRemoveEntry.Name = "ButtonRemoveEntry";
      this.ButtonRemoveEntry.Size = new System.Drawing.Size(136, 23);
      this.ButtonRemoveEntry.TabIndex = 4;
      this.ButtonRemoveEntry.Text = "Remove entry";
      this.ButtonRemoveEntry.UseVisualStyleBackColor = true;
      this.ButtonRemoveEntry.Click += new System.EventHandler(this.ButtonRemoveEntry_Click);
      // 
      // ButtonAddEntry
      // 
      this.ButtonAddEntry.Location = new System.Drawing.Point(7, 43);
      this.ButtonAddEntry.Name = "ButtonAddEntry";
      this.ButtonAddEntry.Size = new System.Drawing.Size(136, 23);
      this.ButtonAddEntry.TabIndex = 3;
      this.ButtonAddEntry.Text = "Add entry";
      this.ButtonAddEntry.UseVisualStyleBackColor = true;
      this.ButtonAddEntry.Click += new System.EventHandler(this.ButtonAddEntry_Click);
      // 
      // textBoxHelp
      // 
      this.textBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBoxHelp.Location = new System.Drawing.Point(7, 82);
      this.textBoxHelp.Multiline = true;
      this.textBoxHelp.Name = "textBoxHelp";
      this.textBoxHelp.ReadOnly = true;
      this.textBoxHelp.Size = new System.Drawing.Size(445, 243);
      this.textBoxHelp.TabIndex = 2;
      this.textBoxHelp.Text = resources.GetString("textBoxHelp.Text");
      // 
      // ButtonCheckStatus
      // 
      this.ButtonCheckStatus.Location = new System.Drawing.Point(7, 14);
      this.ButtonCheckStatus.Name = "ButtonCheckStatus";
      this.ButtonCheckStatus.Size = new System.Drawing.Size(137, 23);
      this.ButtonCheckStatus.TabIndex = 0;
      this.ButtonCheckStatus.Text = "Check integration";
      this.ButtonCheckStatus.UseVisualStyleBackColor = true;
      this.ButtonCheckStatus.Click += new System.EventHandler(this.ButtonCheckStatus_Click);
      // 
      // OptionsPageIntegrationControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBoxContainer);
      this.Name = "OptionsPageIntegrationControl";
      this.Size = new System.Drawing.Size(460, 334);
      this.groupBoxContainer.ResumeLayout(false);
      this.groupBoxContainer.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxContainer;
    private System.Windows.Forms.Button ButtonCheckStatus;
    private System.Windows.Forms.Button ButtonRemoveEntry;
    private System.Windows.Forms.Button ButtonAddEntry;
    private System.Windows.Forms.TextBox textBoxHelp;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label LabelStatus;


  }
}
