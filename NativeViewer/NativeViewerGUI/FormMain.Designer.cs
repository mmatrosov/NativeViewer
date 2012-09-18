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
      this.toolStripStatusLabelFormat = new System.Windows.Forms.ToolStripStatusLabel();
      this.contextMenuStripThumbnail = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItemZoom = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
      this.pictureBoxThumbnail = new NativeViewerGUI.MyPictureBox();
      this.statusStripMain.SuspendLayout();
      this.contextMenuStripThumbnail.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail)).BeginInit();
      this.SuspendLayout();
      // 
      // statusStripMain
      // 
      this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelSize,
            this.toolStripStatusLabelDepth,
            this.toolStripStatusLabelZoom,
            this.toolStripStatusLabelFormat});
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
      // toolStripStatusLabelFormat
      // 
      this.toolStripStatusLabelFormat.Name = "toolStripStatusLabelFormat";
      this.toolStripStatusLabelFormat.Size = new System.Drawing.Size(29, 17);
      this.toolStripStatusLabelFormat.Text = "BGR";
      // 
      // contextMenuStripThumbnail
      // 
      this.contextMenuStripThumbnail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemZoom});
      this.contextMenuStripThumbnail.Name = "contextMenuStripThumbnail";
      this.contextMenuStripThumbnail.Size = new System.Drawing.Size(153, 48);
      this.contextMenuStripThumbnail.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripThumbnail_Closed);
      this.contextMenuStripThumbnail.Opened += new System.EventHandler(this.contextMenuStripThumbnail_Opened);
      // 
      // toolStripMenuItemZoom
      // 
      this.toolStripMenuItemZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
      this.toolStripMenuItemZoom.Name = "toolStripMenuItemZoom";
      this.toolStripMenuItemZoom.Size = new System.Drawing.Size(152, 22);
      this.toolStripMenuItemZoom.Text = "Zoom";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
      this.toolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
      this.toolStripMenuItem1.Tag = "";
      this.toolStripMenuItem1.Text = "25%";
      this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItemZoom_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
      this.toolStripMenuItem2.Size = new System.Drawing.Size(142, 22);
      this.toolStripMenuItem2.Tag = "";
      this.toolStripMenuItem2.Text = "50%";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItemZoom_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
      this.toolStripMenuItem3.Size = new System.Drawing.Size(142, 22);
      this.toolStripMenuItem3.Tag = "";
      this.toolStripMenuItem3.Text = "100%";
      this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItemZoom_Click);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
      this.toolStripMenuItem4.Size = new System.Drawing.Size(142, 22);
      this.toolStripMenuItem4.Tag = "";
      this.toolStripMenuItem4.Text = "200%";
      this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItemZoom_Click);
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
      this.toolStripMenuItem5.Size = new System.Drawing.Size(142, 22);
      this.toolStripMenuItem5.Tag = "";
      this.toolStripMenuItem5.Text = "400%";
      this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItemZoom_Click);
      // 
      // pictureBoxThumbnail
      // 
      this.pictureBoxThumbnail.ContextMenuStrip = this.contextMenuStripThumbnail;
      this.pictureBoxThumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBoxThumbnail.Location = new System.Drawing.Point(0, 0);
      this.pictureBoxThumbnail.Name = "pictureBoxThumbnail";
      this.pictureBoxThumbnail.Size = new System.Drawing.Size(304, 184);
      this.pictureBoxThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBoxThumbnail.TabIndex = 1;
      this.pictureBoxThumbnail.TabStop = false;
      this.pictureBoxThumbnail.SizeChanged += new System.EventHandler(this.pictureBoxThumbnail_SizeChanged);
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 206);
      this.Controls.Add(this.pictureBoxThumbnail);
      this.Controls.Add(this.statusStripMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.KeyPreview = true;
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "NativeViewer";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
      this.statusStripMain.ResumeLayout(false);
      this.statusStripMain.PerformLayout();
      this.contextMenuStripThumbnail.ResumeLayout(false);
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
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFormat;
    private System.Windows.Forms.ContextMenuStrip contextMenuStripThumbnail;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoom;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
  }
}