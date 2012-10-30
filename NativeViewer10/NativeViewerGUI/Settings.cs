using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace NativeViewerGUI
{
  // This class is accessed from both NativeViewerGUI and NativeViewerPackage projects
  public enum RootEnum
  {
    RGB, BGR
  }

  public class Settings
  {
    public enum TImageFormat
    {
      RGB, BGR
    }

    public InterpolationMode InterpModeStretch { get; set; }

    public InterpolationMode InterpModeShrink { get; set; }

    public Size AutoSizeMax { get; set; }

    public Size AutoSizeMin { get; set; }

    public TImageFormat ImageFormat { get; set; }

    private static string FilePath
    {
      get
      {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
          Path.DirectorySeparatorChar + "NativeViewer.xml";
      }
    }

    public static Settings Load()
    {
      XmlSerializer deserializer = new XmlSerializer(typeof(Settings));
      
      using (TextReader textReader = new StreamReader(FilePath))
      {
        return deserializer.Deserialize(textReader) as Settings;
      }
    }

    public static void Save(Settings settings)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Settings));
      
      using (TextWriter textWriter = new StreamWriter(FilePath))
      {
        serializer.Serialize(textWriter, settings);
      }
    }
  }
}
