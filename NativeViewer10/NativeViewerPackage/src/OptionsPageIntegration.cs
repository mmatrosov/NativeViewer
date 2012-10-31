using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;

namespace NativeViewerPackage10
{
  [ClassInterface(ClassInterfaceType.AutoDual)]
  public class OptionsPageIntegration : DialogPage
  {
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected override IWin32Window Window
    {
      get
      {
        return new OptionsPageIntegrationControl(this);
      }
    }
  }
}
