using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace NativeViewerPackage
{
  public partial class OptionsPageIntegrationControl : UserControl
  {
    public enum TStatus
    {
      Unknown, Integrated, NotIntegrated, Outdated, Conflicted
    }

    readonly string AutoExpFilePath;

    // Context for the text entry in the autoexp.dat file consists of the prefix, the 
    // mask, and the suffix. The mask is given by the regular expression. Status is set
    // to "Integrated" only if the mask is found in file within all the context.
    // In case the mask is found, but the context match fails, status is set to 
    // "Conflicted". If the mask is not found at all, status is set to "NotIntegrated".
    readonly string[] AutoExpEntryPrefix = new string[] { "; NativeViewer" };
    readonly Regex AutoExpEntryMask = new Regex(@"^\s*cv::Mat\s*=");
    readonly string[] AutoExpEntrySuffix = new string[] { "" };

    // Entry is added in the following section
    readonly string AutoExpSectionHeader = "[AutoExpand]";
    // The calling template itself
    readonly string AutoExpEntry;

    readonly Color[] StatusColors = new Color[] 
      { Color.Black, Color.Green, Color.Olive, Color.Olive, Color.Red };

    internal OptionsPageIntegration _page;

    private TStatus _status;

    public TStatus Status
    {
      get
      {
        return _status;
      }
      set
      {
        _status = value;
        
        ButtonAddEntry.Enabled = value == TStatus.NotIntegrated;
        ButtonRemoveEntry.Enabled = value == TStatus.Integrated || value == TStatus.Outdated;

        LabelStatus.Text = value.ToString();
        LabelStatus.ForeColor = StatusColors[(int)value];
      }
    }

    public OptionsPageIntegrationControl(OptionsPageIntegration page)
    {
      InitializeComponent();

      _page = page;

      Status = TStatus.Unknown;

      // dte.FullName evaluates to "%VS_INSTALL_DIR%\Common7\IDE\devenv.exe" 
      DTE dte = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(DTE)) as DTE;
      AutoExpFilePath = 
        Path.GetDirectoryName(dte.FullName) + @"\..\Packages\Debugger\autoexp.dat";

      // Path to main dll inside package directory
      string dll_path = Path.GetDirectoryName(
        Assembly.GetExecutingAssembly().Location) + @"\NativeViewer.dll";
      AutoExpEntry = "cv::Mat=$ADDIN(" + dll_path + ",CvMatViewer)";
    }

    private void ButtonCheckStatus_Click(object sender, EventArgs e)
    {
      int prefix_len = AutoExpEntryPrefix.Length;
      int suffix_len = AutoExpEntrySuffix.Length;
      int buf_len = prefix_len + suffix_len + 1;

      using (StreamReader reader = new StreamReader(AutoExpFilePath))
      {
        string line;
        int padded_count = 0;
        List<string> buf = new List<string>();

        for ( ; ; )
        {
          line = reader.ReadLine();

          if (line == null)
          {
            if (++padded_count > suffix_len) break;
          }

          buf.Add(line);

          if (buf.Count < buf_len) continue;
          if (buf.Count > buf_len) buf.RemoveAt(0);

          string entry = buf[prefix_len];

          // Check whether the entry is matched against the mask
          if (!AutoExpEntryMask.IsMatch(entry)) continue;

          bool buf_matches_context =
            Enumerable.SequenceEqual(buf.GetRange(0, prefix_len), AutoExpEntryPrefix) &&
            Enumerable.SequenceEqual(buf.GetRange(prefix_len + 1, suffix_len), AutoExpEntrySuffix);

          if (buf_matches_context)
          {
            if (entry == AutoExpEntry)
            {
              Status = TStatus.Integrated;
            }
            else
            {
              Status = TStatus.Outdated;
            }
          }
          else
          {
            MessageBox.Show(
              "An entry was found in the autoexp.dat file, but it seems like it was not placed by NativeViewer. " +
              "It is not possible to use your own template together with NativeViewer's functionality. " +
              "Check the content of the file manually at \"" + AutoExpFilePath + "\". " +
              "Remove an entry for the \"cv::Mat\" class and try again.",
              "NativeViewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Status = TStatus.Conflicted;
          }

          // At this point decision is made
          return;
        }

        // No line found which matches against the mask
        Status = TStatus.NotIntegrated;
      }
    }

    private void ButtonAddEntry_Click(object sender, EventArgs e)
    {
      string[] lines = File.ReadAllLines(AutoExpFilePath);

      if (!RecheckStatus(lines)) return;

      if (Array.IndexOf(lines, AutoExpSectionHeader) < 0)
      {
        MessageBox.Show(
          "Unknown format of the autoexp.dat file! " +
          "Cannot find the \"" + AutoExpSectionHeader + "\" section header!",
          "NativeViewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      using (StreamWriter writer = new StreamWriter(AutoExpFilePath))
      {
        for (int i = 0; i < lines.Length; ++i)
        {
          writer.WriteLine(lines[i]);
          
          if (lines[i] == AutoExpSectionHeader)
          {
            Array.ForEach(AutoExpEntryPrefix, s => writer.WriteLine(s));
            writer.WriteLine(AutoExpEntry);
            Array.ForEach(AutoExpEntrySuffix, s => writer.WriteLine(s));
          }
        }
      }

      MessageBox.Show(
        "Integration was successfully completed. " + 
        "You can now enjoy NativeViewer functionality :)",
        "NativeViewer", MessageBoxButtons.OK, MessageBoxIcon.Information);

      Status = TStatus.Integrated;
    }

    private void ButtonRemoveEntry_Click(object sender, EventArgs e)
    {
      string[] lines = File.ReadAllLines(AutoExpFilePath);

      int position;

      if (!RecheckStatus(lines, out position)) return;

      using (StreamWriter writer = new StreamWriter(AutoExpFilePath))
      {
        for (int i = 0; i < lines.Length; ++i)
        {
          if (i < position - AutoExpEntryPrefix.Length || 
              i > position + AutoExpEntrySuffix.Length)
          {
            writer.WriteLine(lines[i]);
          }
        }
      }

      Status = TStatus.NotIntegrated;      
    }

    private bool RecheckStatus(string[] lines)
    {
      int dummy;
      return RecheckStatus(lines, out dummy);
    }

    private bool RecheckStatus(string[] lines, out int position)
    {
      TStatus rechecked_status = TStatus.NotIntegrated;

      int prefix_len = AutoExpEntryPrefix.Length;
      int suffix_len = AutoExpEntrySuffix.Length;

      string[] prefix_buf = new string[prefix_len];
      string[] suffix_buf = new string[suffix_len];

      position = -1;

      for (int i = 0; i < lines.Length; ++i)
      {
        if (!AutoExpEntryMask.IsMatch(lines[i])) continue;

        position = i;

        bool buf_matches_context = false;

        if (i >= prefix_len && i < lines.Length - suffix_len)
        {
          Array.Copy(lines, i - prefix_len, prefix_buf, 0, prefix_len);
          Array.Copy(lines, i + 1, suffix_buf, 0, suffix_len);
          buf_matches_context =
            Enumerable.SequenceEqual(prefix_buf, AutoExpEntryPrefix) &&
            Enumerable.SequenceEqual(suffix_buf, AutoExpEntrySuffix);
        }

        if (buf_matches_context)
        {
          rechecked_status = 
            (lines[i] == AutoExpEntry) ? TStatus.Integrated : TStatus.Outdated;
        }
        else
        {
          rechecked_status = TStatus.Conflicted;
        }
      }

      if (rechecked_status != Status)
      {
        MessageBox.Show(
          "Integration status is out of date. Please, recheck integration status.",
          "NativeViewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Status = TStatus.Unknown;
        return false;
      }

      return true;
    }
  }
}
