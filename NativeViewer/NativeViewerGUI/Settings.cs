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

  public class Settings
  {
    public virtual InterpolationMode InterpModeStretch { get; set; }

    public virtual InterpolationMode InterpModeShrink { get; set; }

    public virtual Size AutoSizeMax { get; set; }

    public virtual Size AutoSizeMin { get; set; }

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
      TextReader textReader = new StreamReader(FilePath);
      Settings settings = deserializer.Deserialize(textReader) as Settings;
      textReader.Close();
      return settings;
    }

    public static void Save(Settings settings)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Settings));
      TextWriter textWriter = new StreamWriter(FilePath);
      serializer.Serialize(textWriter, settings);
      textWriter.Close();
    }
  }
}
