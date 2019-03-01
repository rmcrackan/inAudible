// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.Overdrive
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using TagLib;
using TagLib.Id3v2;

namespace AudibleConvertor
{
  public class Overdrive
  {
    public static string mergeText = "(previous chapter)";
    public string mp3Filename = "";
    public string ffprobePath = "";
    private AdvancedSplitting.Chapters myChapters = new AdvancedSplitting.Chapters();
    public string errorText = "";
    public double totalTime;

    public Overdrive(string file)
    {
      this.mp3Filename = file;
    }

    public void SetChapters(AdvancedSplitting.Chapters newChapters)
    {
      this.myChapters = newChapters;
    }

    public bool HasOverdriveMetadata()
    {
      bool flag = false;
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffprobePath;
      process.StartInfo.Arguments = "-loglevel panic -show_format -print_format flat \"" + this.mp3Filename + "\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string str1 = end;
      char[] chArray1 = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        if (str2.Split(chArray2)[0] == "format.tags.OverDrive_MediaMarkers")
          flag = true;
      }
      return flag;
    }

    public AdvancedSplitting.Chapters ReturnChapters()
    {
      return this.myChapters;
    }

    public void SetChapterDescription(int index, string value)
    {
      this.myChapters.SetDescription(index, value);
    }

    public bool ParseChapters()
    {
      File file = File.Create(this.mp3Filename);
      IEnumerable<Frame> frames = ((TagLib.Id3v2.Tag) file.GetTag(TagTypes.Id3v2)).GetFrames((ByteVector) "TXXX");
      string overdriveMetadata = "";
      foreach (object obj in frames)
        overdriveMetadata = obj.ToString().Replace("[OverDrive MediaMarkers]", "").Trim();
      if (overdriveMetadata == null || overdriveMetadata == "")
        return false;
      this.totalTime = file.Properties.Duration.TotalSeconds;
      this.OverdriveXML2Chapters(overdriveMetadata);
      return true;
    }

    public void RemoveSubchapters(bool firstFile)
    {
      List<int> intList = new List<int>();
      int num = 0;
      if (firstFile)
        num = 1;
      for (int pos = num; pos < this.myChapters.Count(); ++pos)
      {
        string str = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(this.myChapters.GetChapter(pos).description));
        if (str.StartsWith(" ") || str.StartsWith("?"))
        {
          if (pos == 0)
            this.myChapters.SetDescription(0, Overdrive.mergeText);
          else
            intList.Add(pos);
        }
        else if (pos > 0 && str == this.myChapters.GetChapter(pos - 1).description)
          intList.Add(pos);
      }
      for (int index = intList.Count - 1; index >= 0; --index)
        this.myChapters.Remove(intList[index]);
    }

    private void OverdriveXML2Chapters(string overdriveMetadata)
    {
      this.myChapters.Clear();
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(overdriveMetadata);
      XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/Markers");
      List<string> desc = new List<string>();
      List<double> chapters = new List<double>();
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
          if (childNode.FirstChild.Name == "Name")
          {
            string[] strArray = childNode.ChildNodes[1].InnerText.Split(':');
            double num1 = double.Parse(strArray[strArray.Length - 1]);
            for (int index = 1; index < strArray.Length; ++index)
            {
              double num2 = Math.Pow(60.0, (double) index);
              double num3 = double.Parse(strArray[strArray.Length - 1 - index]);
              num1 += num2 * num3;
            }
            if (num1 > this.totalTime)
            {
              Audible.diskLogger("Chapter point is longer than total file size: " + (object) num1 + "/" + (object) this.totalTime);
              this.errorText = this.errorText + "Chapter point is longer than total file size: " + num1.ToString("0.00") + "/" + this.totalTime.ToString("0.00") + " - this download may be corrupt. Tossing \"" + childNode.FirstChild.InnerText + "\". ";
            }
            else
            {
              desc.Add(childNode.FirstChild.InnerText);
              chapters.Add(num1);
            }
          }
        }
      }
      this.myChapters.SetChaptersAndDescriptions(chapters, desc);
    }
  }
}
