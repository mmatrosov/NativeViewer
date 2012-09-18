using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NativeViewerGUI
{
  class DialogDeactivator : IDisposable
  {
    private FormMain _form;

    private Timer _timer;

    private Rectangle _allowedRect;

    private int AllowedRectBorder { get; set; }

    public DialogDeactivator(FormMain form)
    {
      AllowedRectBorder = 5;

      _form = form;
      _form.FormClosed += form_FormClosed;
      _form.SizeChanged += form_LayoutChanged;
      _form.LocationChanged += form_LayoutChanged;
      _form.KeyDown += form_KeyDown;

      _timer = new Timer();
      _timer.Enabled = true;
      _timer.Tick += timer_Tick;

      UpdateAllowedRectBasedOnCursorPosition();
    }

    public void Enable()
    {
      _timer.Enabled = true;
    }

    public void Disable()
    {
      _timer.Enabled = false;
    }

    public void UpdateAllowedRectBasedOnCursorPosition()
    {
      _allowedRect = GetAllowedRectBasedOnCursorPosition();
    }

    private Rectangle GetAllowedRectBasedOnCursorPosition()
    {
      Rectangle result = Rectangle.Union(
        _form.Bounds, new Rectangle(Cursor.Position, new Size(1, 1)));

      result.Inflate(AllowedRectBorder, AllowedRectBorder);

      return result;
    }

    public void Dispose()
    {
      _timer.Dispose();
    }

    private void form_FormClosed(object sender, FormClosedEventArgs e)
    {
      // This instance must be disposed on form close so that timer will not try to 
      // access form after it was closed
      Dispose();
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    private void timer_Tick(object sender, EventArgs e)
    {
      // Close window when mouse cursor leaves it or when it loses focus
      if (!_allowedRect.Contains(Cursor.Position) || GetForegroundWindow() != _form.Handle)
      {
        _form.Close();
      }
      else
      {
        // If the window is made smaller than allowed rectangle, e.g. after setting a 
        // smaller zoom level, this branch will ensure the allowed rectangle will shrink 
        // on the cursor's way to the window.
        _allowedRect.Intersect(GetAllowedRectBasedOnCursorPosition());
      }
    }

    private void form_LayoutChanged(object sender, EventArgs e)
    {
      // If the window's layout (size or location) is changed manually, cursor is located 
      // inside the window and this will update the allowed rectangle based on the window 
      // bounds. If the window's layout is changed programmatically, cursor may fall out 
      // of the window bounds, but it will be correctly included in the allowed rectangle.
      UpdateAllowedRectBasedOnCursorPosition();
    }

    private void form_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        _form.Close();
      }
    }
  }
}
