using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NativeViewerGUI
{
  public partial class FormMain : Form
  {
    public FormMain(Image image)
    {
      InitializeComponent();

      // Set thumbnail image
      pictureBoxThumbnail.Image = image;

      // Adjust window position relative to cursor position
      Point p = System.Windows.Forms.Cursor.Position;
      p.Offset(Location - new Size(PointToScreen(new Point(2, 2))));
      Location = p;

      // Adjust initial window size to fit image size
      ClientSize = image.Size + (ClientSize - pictureBoxThumbnail.Size);

      // Initialize status bar
      toolStripStatusLabelSize.Text = String.Format("{0}x{1}", image.Width, image.Height);
      toolStripStatusLabelDepth.Text = image.Tag as String;
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
 
    private void timerCheckBounds_Tick(object sender, EventArgs e)
    {
      // Close window when mouse cursor leaves it or when it loses focus
      if (!Bounds.Contains(Cursor.Position) || GetForegroundWindow() != Handle)
      {
        Close();
      }
    }
  }
}
