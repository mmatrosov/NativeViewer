using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace NativeViewerPackage
{
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [CLSCompliant(false), ComVisible(true)]
  public class OptionsPageGeneral : DialogPage
  {
    [DefaultValue(InterpolationMode.NearestNeighbor)]
    [Category("Behavior")]
    [DisplayName("Image stretch interpolation")]
    [Description("Interpolation mode used for stretching thumbnail image")]
    public virtual InterpolationMode InterpModeStretch { get; set; }

    [DefaultValue(InterpolationMode.HighQualityBilinear)]
    [Category("Behavior")]
    [DisplayName("Image shrink interpolation")]
    [Description("Interpolation mode used for shrinking thumbnail image")]
    public virtual InterpolationMode InterpModeShrink { get; set; }

    [DefaultValue(typeof(Size), "640, 480")]
    [Category("Layout")]
    [DisplayName("Image maximum auto size")]
    [Description("Maximum size of the thumbnail image at the moment it is shown")]
    public virtual Size AutoSizeMax { get; set; }

    [DefaultValue(typeof(Size), "160, 120")]
    [Category("Layout")]
    [DisplayName("Image minimum auto size")]
    [Description("Minimum size of the thumbnail image at the moment it is shown")]
    public virtual Size AutoSizeMin { get; set; }

    public override void SaveSettingsToStorage()
    {
      NativeViewerGUI.Settings settings = new NativeViewerGUI.Settings();

      Type settings_t = settings.GetType();
      Type options_t = this.GetType();

      foreach (PropertyInfo settings_prop in settings_t.GetProperties())
      {
        string name = settings_prop.Name;
        PropertyInfo options_prop = options_t.GetProperty(name);
        object value = options_prop.GetValue(this, null);
        settings_prop.SetValue(settings, value, null);
      }

      NativeViewerGUI.Settings.Save(settings);
    }

    public override void LoadSettingsFromStorage()
    {
      NativeViewerGUI.Settings settings = NativeViewerGUI.Settings.Load();

      Type settings_t = settings.GetType();
      Type options_t = this.GetType();

      foreach (PropertyInfo settings_prop in settings_t.GetProperties())
      {
        string name = settings_prop.Name;
        PropertyInfo options_prop = options_t.GetProperty(name);
        object value = settings_prop.GetValue(settings, null);
        options_prop.SetValue(this, value, null);
      }
    }
  }
}
