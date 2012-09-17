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
    private Settings _settings;

    // Assign to this property to adjust window size so that size of the picture box
    // is equal to the given
    private Size pictureBoxThumbnailSize
    {
      get
      {
        return pictureBoxThumbnail.Size;
      }
      set
      {
        ClientSize = value + (ClientSize - pictureBoxThumbnail.Size);
      }
    }

    public FormMain(Image image)
    {
      InitializeComponent();

      _settings = Settings.Load();

      InitializeContent(image);
      InitializeLayout();
    }

    private void InitializeContent(Image image)
    {
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

      // Initialize status bar
      toolStripStatusLabelSize.Text = String.Format("{0}x{1}", image.Width, image.Height);
      toolStripStatusLabelDepth.Text = image.Tag as String;
      toolStripStatusLabelFormat.Text = image.PixelFormat == 
        PixelFormat.Format8bppIndexed ? "" : _settings.ImageFormat.ToString();

      // Maximum window size is restricted by the screen resolution. Here we try to set
      // all possible zoom levels and see whether we succeeded. If not, a corresponding 
      // menu item is disabled. No image redraw is done by the framework during this 
      // process, only size manipulation.
      foreach (ToolStripMenuItem item in toolStripMenuItemZoom.DropDownItems)
      {
        item.PerformClick();
        Size actual = pictureBoxThumbnailSize;
        Size target = GetZoomMenuItemAssociatedSize(item);
        item.Enabled = actual == target;
      }
    }

    private void InitializeLayout()
    {
      // Adjust window position relative to cursor position
      Point p = System.Windows.Forms.Cursor.Position;
      p.Offset(Location - new Size(PointToScreen(new Point(2, 2))));
      Location = p;

      // Adjust the initial window size to fit the image size. However, window size is 
      // restricted at this point, for it should not be accidentally made too big or too small.
      Size min_size = _settings.AutoSizeMin;
      Size max_size = _settings.AutoSizeMax;

      if (max_size.Width == 0 || max_size.Height == 0)
      {
        max_size = new Size(int.MaxValue, int.MaxValue);
      }

      Func<Size, Size> ConstrainSize = (Size s) => new Size(
        Math.Max(Math.Min(s.Width, max_size.Width), min_size.Width),
        Math.Max(Math.Min(s.Height, max_size.Height), min_size.Height));

      Size size = pictureBoxThumbnail.Image.Size;
      Size constrained_size = ConstrainSize(size);
      double ratio = Math.Min(
        1.0 * constrained_size.Width / size.Width, 
        1.0 * constrained_size.Height / size.Height);
      Size scaled_size = new Size(
        Convert.ToInt32(size.Width * ratio), Convert.ToInt32(size.Height * ratio));
      Size new_size = ConstrainSize(scaled_size);

      pictureBoxThumbnailSize = new_size;
    }

    private Size GetZoomMenuItemAssociatedSize(ToolStripMenuItem item)
    {
      // Text in zoom menu item is assumed to look like "25%" or "200%"
      int percent_zoom = Convert.ToInt32(item.Text.Replace("%", ""));
      double zoom = Convert.ToDouble(percent_zoom) / 100;

      Size img_size = pictureBoxThumbnail.Image.Size;

      return new Size(
        Convert.ToInt32(img_size.Width * zoom), Convert.ToInt32(img_size.Height * zoom));
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
      if (pictureBoxThumbnail.Image == null)
      {
        return;
      }

      SizeF pbox_size = pictureBoxThumbnail.Size;
      SizeF img_size = pictureBoxThumbnail.Image.Size;

      double zoom = Math.Min(
        pbox_size.Width / img_size.Width, pbox_size.Height / img_size.Height);

      toolStripStatusLabelZoom.Text = (Math.Round(zoom * 100)).ToString() + "%";

      if (zoom > 1)
      {
        pictureBoxThumbnail.Interpolation = _settings.InterpModeStretch;
      }
      else
      {
        pictureBoxThumbnail.Interpolation = _settings.InterpModeShrink;
      }
    }

    private void FormMain_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        Close();
      }

      // Context menu shortcuts are not automatically triggered when a menu is inactive,
      // so here we manually check every item in a menu and click it, if shortcut is 
      // equal to the key pressed.
      Action<ToolStripMenuItem> check_shortcut = null;

      check_shortcut = (node) =>
      {
        if (node.ShortcutKeys == e.KeyData)
        {
          node.PerformClick();
        }
        foreach (ToolStripMenuItem child in node.DropDownItems)
        {
          check_shortcut(child);
        }
      };

      foreach (ToolStripMenuItem item in contextMenuStripThumbnail.Items)
      {
        check_shortcut(item);
      }
    }

    private void toolStripMenuItemZoom_Click(object sender, EventArgs e)
    {
      pictureBoxThumbnailSize = GetZoomMenuItemAssociatedSize(sender as ToolStripMenuItem);
    }
  }
}
