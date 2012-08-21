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
    private InterpolationMode _interp_mode_shrink;
    private InterpolationMode _interp_mode_stretch;
    private Size _auto_size_max;
    private Size _auto_size_min;

    [DefaultValue(InterpolationMode.NearestNeighbor)]
    [Category("Behavior")]
    [DisplayName("Image stretch interpolation")]
    [Description("Interpolation mode used for stretching thumbnail image")]
    public InterpolationMode InterpModeStretch 
    { 
      get 
      {
        return _interp_mode_stretch;
      }
      set 
      {
        if (value == InterpolationMode.Invalid)
        {
          value = InterpolationMode.Default;
        }
        _interp_mode_stretch = value;
      } 
    }

    [DefaultValue(InterpolationMode.HighQualityBilinear)]
    [Category("Behavior")]
    [DisplayName("Image shrink interpolation")]
    [Description("Interpolation mode used for shrinking thumbnail image")]
    public InterpolationMode InterpModeShrink
    {
      get
      {
        return _interp_mode_shrink;
      }
      set
      {
        if (value == InterpolationMode.Invalid)
        {
          value = InterpolationMode.Default;
        }
        _interp_mode_shrink = value;
      }
    }

    [DefaultValue(typeof(Size), "640, 480")]
    [Category("Layout")]
    [DisplayName("Image maximum initial size")]
    [Description("Maximum size of the thumbnail image at the moment it is shown. Set to [0; 0] to disable constraint.")]
    public Size AutoSizeMax
    {
      get
      {
        return _auto_size_max;
      }
      set
      {
        if (value.Width <= 0 && value.Height <= 0)
        {
          value.Width = 0;
          value.Height = 0;
        }
        else
        {
          value.Width = Math.Max(1, value.Width);
          value.Height = Math.Max(1, value.Height);

          if (AutoSizeMin.Width > 0 && AutoSizeMin.Height > 0)
          {
            value.Width = Math.Max(value.Width, AutoSizeMin.Width);
            value.Height = Math.Max(value.Height, AutoSizeMin.Height);
          }
        }
        _auto_size_max = value;
      }
    }

    [DefaultValue(typeof(Size), "160, 120")]
    [Category("Layout")]
    [DisplayName("Image minimum initial size")]
    [Description("Minimum size of the thumbnail image at the moment it is shown. Set to [0; 0] to disable constraint.")]
    public Size AutoSizeMin
    {
      get
      {
        return _auto_size_min;
      }
      set
      {
        if (value.Width <= 0 && value.Height <= 0)
        {
          value.Width = 0;
          value.Height = 0;
        }
        else
        {
          value.Width = Math.Max(1, value.Width);
          value.Height = Math.Max(1, value.Height);

          if (AutoSizeMax.Width > 0 && AutoSizeMax.Height > 0)
          {
            value.Width = Math.Min(value.Width, AutoSizeMax.Width);
            value.Height = Math.Min(value.Height, AutoSizeMax.Height);
          }
        }
        _auto_size_min = value;
      }
    }

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
