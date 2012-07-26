using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NativeViewerGUI
{
  public partial class FormMain : Form
  {
    public FormMain(Image image)
    {
      InitializeComponent();

      // Set thumbnail image
      pictureBoxThumbnail.Image = image;

      if (image.PixelFormat == PixelFormat.Format8bppIndexed)
      {
        // The only grayscale format available for GDI+ is indexed, and 
        // the default palette needs to be overridden for proper display
        ColorPalette palette = image.Palette;
        
        Color[] entries = palette.Entries;
        for (int i = 0; i < 256; ++i) entries[i] = Color.FromArgb(i, i, i);

        image.Palette = palette;
      }

      // Adjust window position relative to cursor position
      Point p = System.Windows.Forms.Cursor.Position;
      p.Offset(Location - new Size(PointToScreen(new Point(2, 2))));
      Location = p;

      // Adjust the initial window size to fit the image size. However, window size is 
      // restricted at this point, for it should not be accidentally made too big or too small.
      MinimumSize = Properties.Settings.Default.AutoSizeMin;
      MaximumSize = Properties.Settings.Default.AutoSizeMax;
      ClientSize = image.Size + (ClientSize - pictureBoxThumbnail.Size);
      MinimumSize = new Size();
      MaximumSize = new Size();

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

    private void pictureBoxThumbnail_SizeChanged(object sender, EventArgs e)
    {
      SizeF PboxSize = pictureBoxThumbnail.Size;
      SizeF ImgSize = pictureBoxThumbnail.Image.Size;

      double zoom = Math.Min(
        PboxSize.Width / ImgSize.Width, PboxSize.Height / ImgSize.Height);

      toolStripStatusLabelZoom.Text = (Math.Round(zoom * 100)).ToString() + "%";

      if (zoom > 1)
      {
        pictureBoxThumbnail.Interpolation = Properties.Settings.Default.InterpModeStretch;
      }
      else
      {
        pictureBoxThumbnail.Interpolation = Properties.Settings.Default.InterpModeShrink;
      }
    }

    private void FormMain_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        Close();
      }
    }
  }
}
