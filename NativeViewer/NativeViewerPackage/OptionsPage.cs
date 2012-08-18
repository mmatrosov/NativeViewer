using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NativeViewerPackage
{
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [CLSCompliant(false), ComVisible(true)]
  public class OptionsPage : DialogPage
  {
    [Category("My Category CS")]
    [DisplayName("My Option Integer")]
    [Description("My integer option")]
    public virtual int OptionInteger { get; set; }

    [Category("My Category CS")]
    [DisplayName("My Option Size")]
    [Description("My size option")]
    public virtual Size OptionSize { get; set; }

    [Category("My Category CS")]
    [DisplayName("My Option Interpolation")]
    [Description("My interpolation option")]
    public virtual InterpolationMode OptionInterpolation { get; set; }
  }
}
