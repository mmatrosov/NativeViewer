namespace NativeViewerGUI
{
  partial class FormMain
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.statusStripMain = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabelSize = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelDepth = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabelZoom = new System.Windows.Forms.ToolStripStatusLabel();
      this.timerCheckBounds = new System.Windows.Forms.Timer(this.components);
      this.pictureBoxThumbnail = new NativeViewerGUI.MyPictureBox();
      this.statusStripMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail)).BeginInit();
      this.SuspendLayout();
      // 
      // statusStripMain
      // 
      this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelSize,
            this.toolStripStatusLabelDepth,
            this.toolStripStatusLabelZoom});
      this.statusStripMain.Location = new System.Drawing.Point(0, 184);
      this.statusStripMain.Name = "statusStripMain";
      this.statusStripMain.Size = new System.Drawing.Size(304, 22);
      this.statusStripMain.TabIndex = 0;
      this.statusStripMain.Text = "statusStrip1";
      // 
      // toolStripStatusLabelSize
      // 
      this.toolStripStatusLabelSize.Name = "toolStripStatusLabelSize";
      this.toolStripStatusLabelSize.Size = new System.Drawing.Size(60, 17);
      this.toolStripStatusLabelSize.Text = "1360x1024";
      // 
      // toolStripStatusLabelDepth
      // 
      this.toolStripStatusLabelDepth.Name = "toolStripStatusLabelDepth";
      this.toolStripStatusLabelDepth.Size = new System.Drawing.Size(37, 17);
      this.toolStripStatusLabelDepth.Text = "32fC4";
      // 
      // toolStripStatusLabelZoom
      // 
      this.toolStripStatusLabelZoom.Name = "toolStripStatusLabelZoom";
      this.toolStripStatusLabelZoom.Size = new System.Drawing.Size(35, 17);
      this.toolStripStatusLabelZoom.Text = "800%";
      // 
      // timerCheckBounds
      // 
      this.timerCheckBounds.Enabled = true;
      this.timerCheckBounds.Tick += new System.EventHandler(this.timerCheckBounds_Tick);
      // 
      // pictureBoxThumbnail
      // 
      this.pictureBoxThumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBoxThumbnail.Location = new System.Drawing.Point(0, 0);
      this.pictureBoxThumbnail.Name = "pictureBoxThumbnail";
      this.pictureBoxThumbnail.Size = new System.Drawing.Size(304, 184);
      this.pictureBoxThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBoxThumbnail.TabIndex = 1;
      this.pictureBoxThumbnail.TabStop = false;
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 206);
      this.Controls.Add(this.pictureBoxThumbnail);
      this.Controls.Add(this.statusStripMain);
      this.DataBindings.Add(new System.Windows.Forms.Binding("MinimumSize", global::NativeViewerGUI.Properties.Settings.Default, "MinSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "NativeViewer";
      this.statusStripMain.ResumeLayout(false);
      this.statusStripMain.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip statusStripMain;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSize;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDepth;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoom;
    private NativeViewerGUI.MyPictureBox pictureBoxThumbnail;
    private System.Windows.Forms.Timer timerCheckBounds;
  }
}