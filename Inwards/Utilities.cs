// Decompiled with JetBrains decompiler
// Type: Inwards.Utilities
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudibleConvertor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Inwards
{
  internal class Utilities
  {
    private Stopwatch sw = new Stopwatch();

    public static string WordWrap(string input, int myLimit = 80)
    {
      if (input.Contains("\r") || input.Contains("\n"))
        return "";
      string[] strArray = input.Split(' ');
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = "";
      foreach (string str2 in strArray)
      {
        if ((str1 + str2).Length > myLimit)
        {
          stringBuilder.AppendLine(str1);
          str1 = "";
        }
        str1 += string.Format("{0} ", (object) str2);
      }
      if (str1.Length > 0)
        stringBuilder.AppendLine(str1);
      return stringBuilder.ToString();
    }

    public static string GetLeadingZeroesFileNumber(int num, int total)
    {
      int num1 = 1;
      if (total > 9)
        num1 = 2;
      if (total > 99)
        num1 = 3;
      if (total > 999)
        num1 = 4;
      return num.ToString("D" + (object) num1);
    }

    public static int PercentageCompleteByDuration(double currentTime, double totalTime)
    {
      if (totalTime == 0.0)
        return 0;
      return (int) (currentTime / totalTime * 100.0);
    }

    public static double GetFFmpegTime(string line)
    {
      if (line == null || !line.StartsWith("size="))
        return -1.0;
      string[] strArray = line.Split('=')[2].Split(' ')[0].Split(':');
      return double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2]);
    }

    public static double ConvertTimeStringToDouble(string sTime)
    {
      string[] strArray = sTime.Split(':');
      return double.Parse(strArray[0]) * 60.0 * 60.0 + double.Parse(strArray[1]) * 60.0 + double.Parse(strArray[2]);
    }

    public static string ConvertDoubleToTimeString(double time)
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds(time);
      return timeSpan.Hours.ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2") + "." + timeSpan.Milliseconds.ToString("D3");
    }

    public static AdvancedSplitting.Chapters ParseCueFile(string cueFile)
    {
      AdvancedSplitting.Chapters chapters1 = new AdvancedSplitting.Chapters();
      List<double> chapters2 = new List<double>();
      List<string> desc = new List<string>();
      string[] strArray1 = (!System.IO.File.Exists(cueFile) ? cueFile : System.IO.File.ReadAllText(cueFile)).Split('\n');
      try
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          if (strArray1[index].Trim().StartsWith("INDEX"))
          {
            string[] strArray2 = strArray1[index].Trim().Split(' ')[2].Split(':');
            double num = double.Parse(strArray2[2]) * 0.01 + double.Parse(strArray2[1]) + double.Parse(strArray2[0]) * 60.0;
            chapters2.Add(num);
          }
          if (strArray1[index].Trim().StartsWith("TITLE"))
          {
            string[] strArray2 = strArray1[index].Trim().Split('"');
            desc.Add(strArray2[1]);
          }
        }
        chapters1.SetChaptersAndDescriptions(chapters2, desc);
        chapters1.generatedDescriptions = false;
      }
      catch
      {
      }
      return chapters1;
    }

    public static string CommonPrefix(string[] ss)
    {
      if (ss.Length == 0)
        return "";
      if (ss.Length == 1)
        return ss[0];
      int length = 0;
      foreach (char ch in ss[0])
      {
        foreach (string str in ss)
        {
          if (str.Length <= length || (int) str[length] != (int) ch)
            return ss[0].Substring(0, length);
        }
        ++length;
      }
      return ss[0];
    }

    public static string URLize(string original)
    {
      return "file://" + Uri.EscapeDataString(original);
    }

    public void StopwatchStart()
    {
      this.sw.Reset();
      this.sw.Start();
    }

    public void StopwatchStop()
    {
      this.sw.Stop();
    }

    public string StopwatchGetElapsed(string optionalText)
    {
      string str = this.sw.Elapsed.Hours.ToString("D2") + ":" + this.sw.Elapsed.Minutes.ToString("D2") + ":" + this.sw.Elapsed.Seconds.ToString("D2") + "." + this.sw.Elapsed.Milliseconds.ToString("D3");
      if (optionalText != "")
        str = optionalText + ": " + str;
      return str;
    }

    public static string CleanFileName(string fileName)
    {
      return ((IEnumerable<char>) Path.GetInvalidFileNameChars()).Aggregate<char, string>(fileName, (System.Func<string, char, string>) ((current, c) => current.Replace(c.ToString(), string.Empty)));
    }

    public static int CountDuplicates(string[] array)
    {
      HashSet<string> stringSet = new HashSet<string>();
      int num = 0;
      foreach (string str in array)
      {
        if (stringSet.Contains(str))
          ++num;
        else
          stringSet.Add(str);
      }
      return num;
    }

    public static IEnumerable<KeyValuePair<string, int>> FindDuplicates(string[] array)
    {
      Dictionary<string, int> source = new Dictionary<string, int>();
      foreach (string key in array)
      {
        int num;
        source[key] = !source.TryGetValue(key, out num) ? 1 : num + 1;
      }
      return source.Where<KeyValuePair<string, int>>((System.Func<KeyValuePair<string, int>, bool>) (p => p.Value > 1));
    }

    internal static void WriteTags(Audible myAudible, string file, int trackNum = 0, int trackTotal = 0)
    {
      TagLib.File file1 = TagLib.File.Create(file);
      file1.Tag.Album = myAudible.album;
      file1.Tag.Title = myAudible.title;
      file1.Tag.Track = (uint) trackNum;
      file1.Tag.TrackCount = (uint) trackTotal;
      file1.Tag.Performers = (string[]) null;
      file1.Tag.Performers = new string[1]
      {
        myAudible.narrator
      };
      file1.Tag.Composers = (string[]) null;
      file1.Tag.Composers = new string[1]
      {
        myAudible.author
      };
      file1.Tag.Year = uint.Parse(myAudible.year);
      file1.Tag.Comment = myAudible.GetComments();
      file1.Save();
    }

    internal static string[] SplitGeneric(string inputFile, string outputMask, AdvancedSplitting.Chapters myChapters, bool deleteOriginal)
    {
      SupportLibraries supportLibraries = new SupportLibraries();
      List<double> doubleList1 = myChapters.GetDoubleList();
      string extension = Path.GetExtension(inputFile);
      string directoryName = Path.GetDirectoryName(inputFile);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = supportLibraries.ffmpegPath;
      process.StartInfo.WorkingDirectory = directoryName;
      string str1 = "";
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      List<double> doubleList2 = new List<double>();
      for (int index = 0; index < doubleList1.Count - 1; ++index)
        doubleList2.Add(doubleList1[index]);
      for (int pos = 0; pos < doubleList2.Count; ++pos)
      {
        string str2 = outputMask + " - " + (pos + 1).ToString("D3");
        string finalFileName = myChapters.GetFinalFileName(pos, outputMask);
        if (doubleList2.Count != pos + 1)
          str1 = str1 + " -c copy -map 0 -vn -t " + (doubleList2[pos + 1] - doubleList2[pos]).ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " -ss " + doubleList2[pos].ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + finalFileName + extension + "\"";
        else
          str1 = str1 + " -c copy -map 0 -vn -ss " + doubleList2[pos].ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture) + " \"" + finalFileName + extension + "\"";
        stringList1.Add(directoryName + "\\" + finalFileName + extension);
        if (str1.Length > 7500)
        {
          stringList2.Add(str1);
          str1 = "";
        }
      }
      if (stringList2.Count == 0)
      {
        process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object) inputFile, (object) str1);
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        process.WaitForExit();
      }
      else
      {
        if (str1 != "")
          stringList2.Add(str1);
        foreach (string str2 in stringList2)
        {
          process.StartInfo.Arguments = string.Format("-y -i \"{0}\" {1}", (object) inputFile, (object) str2);
          process.StartInfo.UseShellExecute = false;
          process.StartInfo.CreateNoWindow = true;
          process.Start();
          process.WaitForExit();
        }
      }
      if (process.ExitCode == 0)
      {
        try
        {
          System.IO.File.Delete(inputFile);
        }
        catch (Exception ex)
        {
        }
      }
      return stringList1.ToArray();
    }
  }
}
