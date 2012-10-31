namespace NativeViewerPackage10
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.LabelStatus = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.ButtonRemoveEntry = new System.Windows.Forms.Button();
      this.ButtonAddEntry = new System.Windows.Forms.Button();
      this.textBoxHelp = new System.Windows.Forms.TextBox();
      this.ButtonCheckStatus = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.LabelStatus);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.ButtonRemoveEntry);
      this.panel1.Controls.Add(this.ButtonAddEntry);
      this.panel1.Controls.Add(this.textBoxHelp);
      this.panel1.Controls.Add(this.ButtonCheckStatus);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(460, 334);
      this.panel1.TabIndex = 0;
      // 
      // LabelStatus
      // 
      this.LabelStatus.AutoSize = true;
      this.LabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.LabelStatus.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LabelStatus.Location = new System.Drawing.Point(233, 9);
      this.LabelStatus.Name = "LabelStatus";
      this.LabelStatus.Size = new System.Drawing.Size(111, 13);
      this.LabelStatus.TabIndex = 12;
      this.LabelStatus.Text = "Status Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(146, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(91, 13);
      this.label1.TabIndex = 11;
      this.label1.Text = "Integration status:";
      // 
      // ButtonRemoveEntry
      // 
      this.ButtonRemoveEntry.Location = new System.Drawing.Point(145, 32);
      this.ButtonRemoveEntry.Name = "ButtonRemoveEntry";
      this.ButtonRemoveEntry.Size = new System.Drawing.Size(136, 23);
      this.ButtonRemoveEntry.TabIndex = 10;
      this.ButtonRemoveEntry.Text = "Remove entry";
      this.ButtonRemoveEntry.UseVisualStyleBackColor = true;
      this.ButtonRemoveEntry.Click += new System.EventHandler(this.ButtonClickProxy);
      // 
      // ButtonAddEntry
      // 
      this.ButtonAddEntry.Location = new System.Drawing.Point(3, 32);
      this.ButtonAddEntry.Name = "ButtonAddEntry";
      this.ButtonAddEntry.Size = new System.Drawing.Size(136, 23);
      this.ButtonAddEntry.TabIndex = 9;
      this.ButtonAddEntry.Text = "Add entry";
      this.ButtonAddEntry.UseVisualStyleBackColor = true;
      this.ButtonAddEntry.Click += new System.EventHandler(this.ButtonClickProxy);
      // 
      // textBoxHelp
      // 
      this.textBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBoxHelp.Location = new System.Drawing.Point(3, 63);
      this.textBoxHelp.Multiline = true;
      this.textBoxHelp.Name = "textBoxHelp";
      this.textBoxHelp.ReadOnly = true;
      this.textBoxHelp.Size = new System.Drawing.Size(452, 266);
      this.textBoxHelp.TabIndex = 8;
      this.textBoxHelp.Text = resources.GetString("textBoxHelp.Text");
      // 
      // ButtonCheckStatus
      // 
      this.ButtonCheckStatus.Location = new System.Drawing.Point(3, 3);
      this.ButtonCheckStatus.Name = "ButtonCheckStatus";
      this.ButtonCheckStatus.Size = new System.Drawing.Size(137, 23);
      this.ButtonCheckStatus.TabIndex = 7;
      this.ButtonCheckStatus.Text = "Check integration";
      this.ButtonCheckStatus.UseVisualStyleBackColor = true;
      this.ButtonCheckStatus.Click += new System.EventHandler(this.ButtonClickProxy);
      // 
      // OptionsPageIntegrationControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panel1);
      this.Name = "OptionsPageIntegrationControl";
      this.Size = new System.Drawing.Size(460, 334);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label LabelStatus;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button ButtonRemoveEntry;
    private System.Windows.Forms.Button ButtonAddEntry;
    private System.Windows.Forms.TextBox textBoxHelp;
    private System.Windows.Forms.Button ButtonCheckStatus;



  }
}
