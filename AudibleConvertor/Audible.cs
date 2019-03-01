// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.Audible
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Mp4Chapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TagLib;
using TagLib.Mpeg4;

namespace AudibleConvertor
{
  public class Audible
  {
    public static string appPath = Path.GetDirectoryName(AudibleConvertor.GLOBALS.ExecutablePath);
    public SupportLibraries supportLibs = new SupportLibraries();
    public string audibleChaptersPath = Audible.appPath + "\\AudibleChapters.exe";
    public string mp4chapsPath = Audible.appPath + "\\mp4chaps.exe";
    public string mp4infoPath = Audible.appPath + "\\mp4info.exe";
    public string ffmpegPath = "";
    public string ffprobePath = Audible.appPath + "\\ffmpeg\\ffprobe.exe";
    public AdvancedSplitting.Chapters newChaps = new AdvancedSplitting.Chapters();
    public string title = "";
    public string author = "";
    private string comments = "";
    public string cmt = "";
    public string narrator = "";
    public string bookId = "";
    public string codec = "";
    public string coverPath = "";
    public string year = "";
    public string publisher = "";
    public string id = "";
    public string guid = "";
    public string totalTime = "";
    public TimeSpan duration = new TimeSpan();
    public string decryptedFile = "";
    public string album = "";
    public string trackNum = "";
    public string trackTotal = "";
    public string genre = "";
    public AdvancedOptions advancedOptions = new AdvancedOptions();
    public NFO nfo = new NFO();
    public AppleTag originalTags;
    private List<double> chapters;
    private byte[] rawHeader;
    public bool addTrackToTitle;
    public bool useTrackInsteadOfTitle;
    public IPicture coverArt;
    public bool hasCoverArt;
    public double totalSeconds;
    public long startOfPatternInTargetFile;
    public long startOfPatternInSourceFile;

    public string GetComments()
    {
      if (this.comments == null)
        this.comments = "";
      return this.comments;
    }

    public void SetComments(string s)
    {
      this.comments = s;
    }

    public void GetCoverArt(string file)
    {
      TagLib.File file1 = TagLib.File.Create(file);
      if (file1.Tag.Pictures.Length < 1)
        return;
      this.coverArt = file1.Tag.Pictures[0];
      this.hasCoverArt = true;
    }

    public string GetASCIITag(string property)
    {
      if (property == null)
        return "";
      foreach (char ch in new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()))
        property = property.Replace(ch.ToString(), "");
      return property;
    }

    public List<double> getAudibleChapters(string file)
    {
      string str1 = "";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.audibleChaptersPath;
      process.StartInfo.Arguments = string.Format("\"{0}\"", (object) file);
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string[] strArray1 = end.Split('\n');
      str1 = "";
      this.chapters = new List<double>();
      try
      {
        foreach (string str2 in strArray1)
        {
          if (str2.StartsWith("Chapter"))
          {
            string[] strArray2 = str2.Split(':');
            this.chapters.Add(TimeSpan.Parse(strArray2[1].Trim() + ":" + strArray2[2].Trim() + ":" + strArray2[3].Trim()).TotalSeconds);
          }
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Could not parse chapters properly.");
      }
      return this.chapters;
    }

    public void writeCUEfile(string cue, string outputFile)
    {
      System.IO.File.WriteAllText(outputFile, cue);
    }

    public string getCUEfromChapters(AdvancedSplitting.Chapters chapters, string fileName, string cueType, double totalSeconds)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (fileName != "")
        stringBuilder.Append("FILE \"" + fileName + "\" " + cueType + "\n");
      int num1 = 1;
      double chapterDouble = chapters.GetChapterDouble(0);
      if (chapterDouble != 0.0)
        chapters.Remove(chapters.Count() - 1);
      foreach (double num2 in chapters.GetDoubleList())
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(num2 - chapterDouble);
        int seconds = timeSpan.Seconds;
        int milliseconds = timeSpan.Milliseconds;
        string str = Math.Floor(timeSpan.TotalMinutes).ToString() + ":" + (object) seconds + ":" + (milliseconds / 10).ToString("D2");
        stringBuilder.Append("TRACK " + (object) num1 + " AUDIO\n");
        if (chapters.generatedDescriptions)
          stringBuilder.Append("  TITLE \"Chapter " + num1.ToString("D2") + "\"\n");
        else
          stringBuilder.Append("  TITLE \"" + chapters.GetChapter(num1 - 1).description + "\"\n");
        stringBuilder.Append("  INDEX 01 " + str + "\n");
        ++num1;
      }
      return stringBuilder.ToString();
    }

    public AdvancedSplitting.Chapters GetM4BChapters(string file)
    {
      string xml = "";
      AdvancedSplitting.Chapters chapters = new AdvancedSplitting.Chapters();
      try
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.ffprobePath;
        process.StartInfo.Arguments = "-loglevel panic -show_chapters -print_format xml \"" + file + "\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        xml = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xml);
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/ffprobe/chapters/chapter");
        if (xmlNodeList.Count == 0)
        {
          using (FileStream fileStream = System.IO.File.OpenRead(file))
          {
            Audible.diskLogger("No chapters found; trying mp4Chapters library...");
            ChapterExtractor chapterExtractor = new ChapterExtractor((IAbstractStream) new StreamWrapper((Stream) fileStream));
            Audible.diskLogger("Appears to be an M4a = " + (object) chapterExtractor.IsMp4a());
            chapterExtractor.Run();
            foreach (ChapterInfo chapterInfo in chapterExtractor.Chapters ?? new ChapterInfo[0])
              chapters.Add(((TimeSpan) chapterInfo.Time).TotalMilliseconds / 1000.0);
          }
          if (chapters.Count() == 0)
            chapters.Add(0.0);
          return chapters;
        }
        foreach (XmlNode xmlNode in xmlNodeList)
        {
          string desc = "";
          foreach (XmlNode childNode in xmlNode.ChildNodes)
          {
            if (childNode.Attributes["key"].Value != null && childNode.Attributes["key"].Value == "title")
              desc = childNode.Attributes["value"].Value;
          }
          chapters.Add(double.Parse(xmlNode.Attributes["start_time"].Value.Replace(",", "."), (IFormatProvider) CultureInfo.InvariantCulture), desc);
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("STDOUT: " + xml);
        Audible.diskLogger("FFMPEG Chapter extraction failed. Falling back to MP4Chaps.  Error was: " + ex.ToString());
        chapters.SetDoubleChapters(this.getAAXChaptersMP4Chaps(file));
      }
      return chapters;
    }

    public List<double> getAAXChapters(string file)
    {
      string xml = "";
      try
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo();
        process.StartInfo.FileName = this.ffprobePath;
        process.StartInfo.Arguments = "-loglevel panic -show_chapters -print_format xml \"" + file + "\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        xml = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xml);
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/ffprobe/chapters/chapter");
        this.chapters = new List<double>();
        if (xmlNodeList.Count == 0)
        {
          using (FileStream fileStream = System.IO.File.OpenRead(file))
          {
            Audible.diskLogger("No chapters found; trying mp4Chapters library...");
            ChapterExtractor chapterExtractor = new ChapterExtractor((IAbstractStream) new StreamWrapper((Stream) fileStream));
            Audible.diskLogger("Appears to be an M4a = " + (object) chapterExtractor.IsMp4a());
            chapterExtractor.Run();
            foreach (ChapterInfo chapterInfo in chapterExtractor.Chapters ?? new ChapterInfo[0])
              this.chapters.Add(((TimeSpan) chapterInfo.Time).TotalMilliseconds / 1000.0);
          }
          if (this.chapters.Count == 0)
            this.chapters.Add(0.0);
          return this.chapters;
        }
        foreach (XmlNode xmlNode in xmlNodeList)
          this.chapters.Add(double.Parse(xmlNode.Attributes["start_time"].Value.Replace(",", "."), (IFormatProvider) CultureInfo.InvariantCulture));
      }
      catch (Exception ex)
      {
        Audible.diskLogger("STDOUT: " + xml);
        Audible.diskLogger("FFMPEG Chapter extraction failed. Falling back to MP4Chaps.  Error was: " + ex.ToString());
        this.chapters = this.getAAXChaptersMP4Chaps(file);
      }
      return this.chapters;
    }

    public List<double> getAAXChaptersMP4Chaps(string file)
    {
      string str1 = "";
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mp4chapsPath;
      process.StartInfo.Arguments = string.Format("-l \"{0}\"", (object) file);
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string[] strArray1 = end.Split('\n');
      str1 = "";
      this.chapters = new List<double>();
      if (strArray1.Length == 2)
      {
        this.chapters.Add(0.0);
        return this.chapters;
      }
      try
      {
        int num = 0;
        foreach (string str2 in strArray1)
        {
          if (str2.Trim().StartsWith("Chapter"))
          {
            string[] strArray2 = str2.Split('-')[1].Trim().Split(':');
            string[] strArray3 = (strArray2[0].Trim() + ":" + strArray2[1].Trim() + ":" + strArray2[2].Trim()).Split('.');
            string str3 = strArray3[0];
            TimeSpan timeSpan = new TimeSpan(0, int.Parse(str3.Split(':')[0]), int.Parse(str3.Split(':')[1]), int.Parse(str3.Split(':')[2]), int.Parse(strArray3[1]));
            if (timeSpan.TotalSeconds != 0.0 || num != 1)
              this.chapters.Add(timeSpan.TotalSeconds);
            ++num;
          }
        }
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Could not parse chapters properly.");
      }
      if (this.chapters.Count == 0)
      {
        this.chapters = this.getAudibleChapters(file);
        for (int index = 0; index < this.chapters.Count; ++index)
          this.chapters[index] = this.chapters[index] * 2.0;
        if (this.chapters.Count > 0)
          Audible.diskLogger("Had to revert to AudibleChapters for chapter list.");
      }
      return this.chapters;
    }

    public string GetCustomAAXTags(string file, string tag, byte lead)
    {
      byte[] bytes1 = Audible.GetBytes(tag);
      byte[] pattern = new byte[bytes1.Length + 1];
      pattern[0] = lead;
      for (int index = 1; index < pattern.Length; ++index)
        pattern[index] = bytes1[index - 1];
      byte[] bytes2 = Audible.GetBytes("mdirappl");
      int count = 8388608;
      byte[] data1 = (byte[]) null;
      FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
      BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
      long length = new FileInfo(file).Length;
      int num1 = 0;
      long num2 = 0;
      while (num2 < length)
      {
        data1 = binaryReader.ReadBytes(count);
        num1 = this.GetPositionAfterLastMatch(data1, bytes2);
        if (num1 <= 0)
        {
          count += count;
          num2 += (long) count;
        }
        else
          break;
      }
      byte[] data2 = new byte[data1.Length - num1];
      for (long index = 0; index < (long) (data1.Length - num1); ++index)
        data2[index] = data1[(long) num1 + index];
      long num3 = (long) num1;
      while (num3 < length)
      {
        num1 = this.GetPositionAfterMatch(data2, pattern);
        if (num1 <= 0)
          num3 += (long) count;
        else
          break;
      }
      int num4 = num1 + 16;
      List<byte> byteList = new List<byte>();
      for (int index = num4; index < data2.Length && (int) data2[index] != 0; ++index)
        byteList.Add(data2[index]);
      byte[] bytes3 = new byte[byteList.Count];
      for (int index = 0; index < byteList.Count; ++index)
        bytes3[index] = byteList[index];
      string str = Encoding.UTF8.GetString(bytes3);
      fileStream.Close();
      return str;
    }

    public string GetAudibleId(string file)
    {
      string str = "";
      byte[] bytes = Audible.GetBytes("CDEK");
      int count = 8388608;
      byte[] data = (byte[]) null;
      FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
      BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
      long length = new FileInfo(file).Length;
      int num1 = 0;
      long num2 = 0;
      while (num2 < length)
      {
        data = binaryReader.ReadBytes(count);
        num1 = this.GetPositionAfterMatch(data, bytes);
        if (num1 <= 0)
          num2 += (long) count;
        else
          break;
      }
      for (int index = num1 + 16; index < data.Length && (int) data[index] != 0; ++index)
        str += Convert.ToChar(data[index]).ToString();
      fileStream.Close();
      return str;
    }

    private int GetPositionAfterMatch(byte[] data, byte[] pattern)
    {
      for (int index1 = 0; index1 < data.Length - pattern.Length; ++index1)
      {
        bool flag = true;
        for (int index2 = 0; index2 < pattern.Length; ++index2)
        {
          if ((int) data[index1 + index2] != (int) pattern[index2])
          {
            flag = false;
            break;
          }
        }
        if (flag)
          return index1 + pattern.Length;
      }
      return 0;
    }

    private int GetPositionAfterLastMatch(byte[] data, byte[] pattern)
    {
      int num = 0;
      for (int index1 = 0; index1 < data.Length - pattern.Length; ++index1)
      {
        bool flag = true;
        for (int index2 = 0; index2 < pattern.Length; ++index2)
        {
          if ((int) data[index1 + index2] != (int) pattern[index2])
          {
            flag = false;
            break;
          }
        }
        if (flag)
          ++num;
        if (flag && num > 1)
          return index1 + pattern.Length;
      }
      return 0;
    }

    public void getMetaData(string file)
    {
      int length = 4096;
      this.rawHeader = new byte[length];
      using (BinaryReader binaryReader = new BinaryReader((Stream) System.IO.File.Open(file, FileMode.Open, FileAccess.Read)))
      {
        for (int index = 0; index < length; ++index)
          this.rawHeader[index] = binaryReader.ReadByte();
      }
      this.title = this.getTag("parent_title");
      this.author = this.getTag("author");
      this.comments = this.getTag("long_description");
      this.narrator = this.getTag("narrator");
      this.codec = this.getTag("codec");
      this.year = this.getTag("pubdate");
      try
      {
        this.year = this.year.Split('-')[2];
      }
      catch
      {
        this.year = "1999";
      }
    }

    private string getTag(string tag)
    {
      return this.cleanupTag(this.getRawTag(Audible.ByteSearch(this.rawHeader, Audible.GetBytes(tag), 0) + tag.Length));
    }

    private string cleanupTag(string tagValue)
    {
      return Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.ASCII, Encoding.UTF8.GetBytes(tagValue)));
    }

    private string getRawTag(int valuePosition)
    {
      string str = "";
      for (int index = valuePosition; index < this.rawHeader.Length && (int) this.rawHeader[index] != 0; ++index)
      {
        if ((int) this.rawHeader[index] == 34)
          this.rawHeader[index] = (byte) 39;
        str += Convert.ToChar(this.rawHeader[index]).ToString();
      }
      return str;
    }

    private static byte[] GetBytes(string str)
    {
      return Encoding.UTF8.GetBytes(str);
    }

    private static string GetString(byte[] bytes)
    {
      return Encoding.UTF8.GetString(bytes);
    }

    public static long ByteSearchBHM(byte[] value, byte[] pattern, int tries)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (pattern == null)
        throw new ArgumentNullException(nameof (pattern));
      long longLength1 = value.LongLength;
      long longLength2 = pattern.LongLength;
      if (longLength1 == 0L || longLength2 == 0L || longLength2 > longLength1)
        return -1;
      long[] numArray = new long[256];
      for (long index = 0; index < 256L; ++index)
        numArray[index] = longLength2;
      long num1 = longLength2 - 1L;
      for (long index = 0; index < num1; ++index)
        numArray[(int) pattern[index]] = num1 - index;
      long num2 = 0;
      while (num2 <= longLength1 - longLength2)
      {
        for (long index = num1; (int) value[num2 + index] == (int) pattern[index]; --index)
        {
          if (tries > 0 && stopwatch.ElapsedMilliseconds > (long) (tries * 1000))
          {
            Audible.diskLogger("giving up on this pattern. i=" + (object) index + ", index=" + (object) num2);
            return -1;
          }
          if (index == 0L)
            return num2;
        }
        num2 += numArray[(int) value[num2 + num1]];
      }
      return -1;
    }

    private static int LinqByteSearch(byte[] haystack, byte[] needle)
    {
      int? nullable = Enumerable.Range(0, haystack.Length - 1).Cast<int?>().FirstOrDefault<int?>((System.Func<int?, bool>) (n => ((IEnumerable<byte>) haystack).Skip<byte>(n.Value).Take<byte>(needle.Length).SequenceEqual<byte>((IEnumerable<byte>) needle)));
      if (nullable.HasValue)
        return nullable.Value;
      return -1;
    }

    private static int SimpleBoyerMooreSearch(byte[] haystack, byte[] needle)
    {
      int[] numArray = new int[256];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = needle.Length;
      for (int index = 0; index < needle.Length; ++index)
        numArray[(int) needle[index]] = needle.Length - index - 1;
      int index1 = needle.Length - 1;
      byte num1 = ((IEnumerable<byte>) needle).Last<byte>();
      while (index1 < haystack.Length)
      {
        byte num2 = haystack[index1];
        if ((int) haystack[index1] == (int) num1)
        {
          bool flag = true;
          for (int index2 = needle.Length - 2; index2 >= 0; --index2)
          {
            if ((int) haystack[index1 - needle.Length + index2 + 1] != (int) needle[index2])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return index1 - needle.Length + 1;
          ++index1;
        }
        else
          index1 += numArray[(int) num2];
      }
      return -1;
    }

    private static int ByteSearch(byte[] searchIn, byte[] searchBytes, int start = 0)
    {
      int num = -1;
      if (searchIn.Length > 0 && searchBytes.Length > 0 && (start <= searchIn.Length - searchBytes.Length && searchIn.Length >= searchBytes.Length))
      {
        for (int index1 = start; index1 <= searchIn.Length - searchBytes.Length; ++index1)
        {
          if ((int) searchIn[index1] == (int) searchBytes[0])
          {
            if (searchIn.Length > 1)
            {
              bool flag = true;
              for (int index2 = 1; index2 <= searchBytes.Length - 1; ++index2)
              {
                if ((int) searchIn[index1 + index2] != (int) searchBytes[index2])
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
              {
                num = index1;
                break;
              }
            }
            else
            {
              num = index1;
              break;
            }
          }
        }
      }
      return num;
    }

    internal void GetM4BMetaData(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.mp4infoPath;
      process.StartInfo.Arguments = string.Format("\"{0}\"", (object) file);
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      this.title = this.GetTagFromMp4info(end, "Name");
      this.author = this.GetTagFromMp4info(end, "Artist");
      this.year = this.GetTagFromMp4info(end, "Release Date");
      this.narrator = this.GetTagFromMp4info(end, "Composer");
      this.codec = "M4B";
      this.comments = this.GetTagFromMp4info(end, "Comments");
      this.totalTime = this.GetM4BTotalTime(end);
    }

    internal void GetM4BMetaDataTagLib(string file)
    {
      try
      {
        TagLib.File file1 = TagLib.File.Create(file, "audio/mp4", ReadStyle.Average);
        this.title = file1.Tag.Title;
        this.author = file1.Tag.FirstPerformer;
        this.year = file1.Tag.Year.ToString();
        this.narrator = file1.Tag.FirstComposer;
        this.codec = "M4B";
        this.comments = file1.Tag.Comment;
        this.album = file1.Tag.Album;
        this.duration = file1.Properties.Duration;
        this.totalTime = this.GetTotalTimeFormatted();
        this.genre = file1.Tag.FirstGenre;
        AppleTag tag = (AppleTag) file1.GetTag(TagTypes.Apple, true);
        this.publisher = tag.Publisher;
        ReadOnlyByteVector readOnlyByteVector = (ReadOnlyByteVector) "ldes";
        string str = "";
        foreach (AppleDataBox dataBox in tag.DataBoxes((ByteVector[]) new ReadOnlyByteVector[1]
        {
          readOnlyByteVector
        }))
          str = dataBox.Text;
        if (tag.LongDescription != null && tag.LongDescription != "")
          this.comments = tag.LongDescription;
        if (this.comments == null || this.comments == "")
        {
          if (tag.Description != null && tag.Description != "")
            this.comments = tag.Description;
          if (str != null && str != "")
            this.comments = str;
        }
        if (this.narrator == null || this.narrator == "")
          this.narrator = tag.Narrator;
        this.bookId = tag.AudibleBookId;
        this.guid = tag.AudibleGUID;
        this.id = tag.AudibleCDEK;
        file1.Dispose();
      }
      catch
      {
      }
    }

    internal void GetGenericMetaDataTagLib(string file)
    {
      try
      {
        TagLib.File file1 = TagLib.File.Create(file);
        this.title = file1.Tag.Title;
        this.author = file1.Tag.FirstPerformer;
        this.year = file1.Tag.Year.ToString();
        this.narrator = file1.Tag.FirstComposer;
        this.codec = "M4B";
        this.comments = file1.Tag.Comment;
        this.album = file1.Tag.Album;
        this.duration = file1.Properties.Duration;
        this.totalTime = this.GetTotalTimeFormatted();
        file1.Dispose();
      }
      catch
      {
      }
      if (this.title != null)
        return;
      this.GetM4BMetaDataFfmpeg(file);
    }

    public string GetTotalTimeFormatted()
    {
      try
      {
        return ((int) this.duration.TotalHours).ToString("D2") + ":" + this.duration.Minutes.ToString("D2") + ":" + this.duration.Seconds.ToString("D2") + "." + (object) this.duration.Milliseconds;
      }
      catch
      {
        return "00:00:00.000";
      }
    }

    internal void GetM4BMetaDataFfmpeg(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffmpegPath;
      process.StartInfo.Arguments = string.Format("-i \"{0}\"", (object) file);
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardError.ReadToEnd();
      process.WaitForExit();
      this.title = this.GetTagFromFfmpeg(end, "title");
      this.author = this.GetTagFromFfmpeg(end, "artist");
      this.year = this.GetTagFromFfmpeg(end, "date");
      this.narrator = this.GetTagFromFfmpeg(end, "composer");
      this.codec = "M4B";
      this.comments = this.GetTagFromFfmpeg(end, "comment");
      this.album = this.GetTagFromFfmpeg(end, "album");
      this.totalTime = Audible.GetM4BTotalTimeffmpeg(end);
    }

    private string GetM4BTotalTime(string output)
    {
      string[] strArray1 = output.Split('\n');
      string str1 = "";
      try
      {
        foreach (string str2 in strArray1)
        {
          if (str2.Contains(" secs,"))
          {
            string[] strArray2 = str2.Split(',');
            for (int index = 0; index < strArray2.Length; ++index)
            {
              if (strArray2[index].Contains(" secs"))
                str1 = VirtualWAV.GetFormattedTime(double.Parse(strArray2[index].Trim().Split(' ')[0]));
            }
          }
        }
      }
      catch
      {
        str1 = "00:00:00";
      }
      return str1;
    }

    public static string GetChecksumBuffered(string fileName)
    {
      using (BufferedStream bufferedStream = new BufferedStream((Stream) new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read), 32768))
        return BitConverter.ToString(new SHA256Managed().ComputeHash((Stream) bufferedStream)).Replace("-", string.Empty);
    }

    public static string GetM4BTotalTimeffmpeg(string output)
    {
      string[] strArray1 = output.Split('\n');
      string str1 = "";
      try
      {
        foreach (string str2 in strArray1)
        {
          if (str2.Trim().StartsWith("Duration:"))
          {
            string[] strArray2 = str2.Split(',')[0].Split(' ');
            str1 = strArray2[strArray2.Length - 1].Trim();
          }
        }
      }
      catch
      {
        str1 = "00:00:00";
      }
      return str1;
    }

    private string GetTagFromMp4info(string output, string tag)
    {
      string[] strArray1 = output.Split('\n');
      string str1 = "";
      try
      {
        foreach (string str2 in strArray1)
        {
          if (str2.Trim().StartsWith(tag))
          {
            string[] strArray2 = str2.Split(':');
            for (int index = 1; index < strArray2.Length; ++index)
            {
              if (index > 1)
                str1 += ": ";
              str1 += strArray2[index].Trim();
            }
            break;
          }
        }
      }
      catch
      {
      }
      return str1;
    }

    private string GetTagFromFfmpeg(string output, string tag)
    {
      string[] strArray1 = output.Split('\n');
      string str1 = "";
      try
      {
        foreach (string str2 in strArray1)
        {
          if (str2.Trim().StartsWith(tag))
          {
            string[] strArray2 = str2.Split(':');
            for (int index = 1; index < strArray2.Length; ++index)
            {
              if (index > 1)
                str1 += ": ";
              str1 += strArray2[index].Trim();
            }
            break;
          }
        }
      }
      catch
      {
      }
      return str1;
    }

    public AAXMetaData GetCompleteAAXMetaData(string file)
    {
      AAXMetaData aaxMetaData = new AAXMetaData();
      try
      {
        aaxMetaData.nam = this.GetCustomAAXTags(file, "nam", (byte) 169);
        aaxMetaData.pti = this.GetCustomAAXTags(file, "pti", (byte) 64);
        aaxMetaData.PST = this.GetCustomAAXTags(file, "PST", (byte) 64);
        aaxMetaData.ART = this.GetCustomAAXTags(file, "ART", (byte) 169);
        aaxMetaData.alb = this.GetCustomAAXTags(file, "alb", (byte) 169);
        aaxMetaData.gen = this.GetCustomAAXTags(file, "gen", (byte) 169);
        aaxMetaData.cmt = this.GetCustomAAXTags(file, "cmt", (byte) 169);
        aaxMetaData.pub = this.GetCustomAAXTags(file, "pub", (byte) 169);
        aaxMetaData.day = this.GetCustomAAXTags(file, "day", (byte) 169);
        aaxMetaData.nrt = this.GetCustomAAXTags(file, "nrt", (byte) 169);
        aaxMetaData.sti = this.GetCustomAAXTags(file, "sti", (byte) 64);
        aaxMetaData.des = this.GetCustomAAXTags(file, "des", (byte) 169);
        aaxMetaData.rldt = this.GetCustomAAXTags(file, "rldt", (byte) 35);
        aaxMetaData.tagCollection.Add("nam|" + aaxMetaData.nam);
        aaxMetaData.tagCollection.Add("pti|" + aaxMetaData.pti);
        aaxMetaData.tagCollection.Add("PST|" + aaxMetaData.PST);
        aaxMetaData.tagCollection.Add("ART|" + aaxMetaData.ART);
        aaxMetaData.tagCollection.Add("alb|" + aaxMetaData.alb);
        aaxMetaData.tagCollection.Add("gen|" + aaxMetaData.gen);
        aaxMetaData.tagCollection.Add("cmt|" + aaxMetaData.cmt);
        aaxMetaData.tagCollection.Add("pub|" + aaxMetaData.pub);
        aaxMetaData.tagCollection.Add("day|" + aaxMetaData.day);
        aaxMetaData.tagCollection.Add("nrt|" + aaxMetaData.nrt);
        aaxMetaData.tagCollection.Add("sti|" + aaxMetaData.sti);
        aaxMetaData.tagCollection.Add("des|" + aaxMetaData.des);
        aaxMetaData.tagCollection.Add("rldt|" + aaxMetaData.rldt);
      }
      catch
      {
      }
      return aaxMetaData;
    }

    internal void GetCustomM4PMetaData(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.ffprobePath;
      process.StartInfo.Arguments = "-loglevel panic -show_format \"" + file + "\"";
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string str1 = end;
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray))
      {
        string str3 = str2.Replace("\r", "");
        if (str3.StartsWith("TAG:title"))
          this.title = str3.Split('=')[1];
        if (str3.StartsWith("TAG:album"))
          this.album = str3.Split('=')[1];
        if (str3.StartsWith("TAG:artist"))
          this.author = str3.Split('=')[1];
        if (str3.StartsWith("TAG:album_artist"))
          this.narrator = str3.Split('=')[1];
        if (str3.StartsWith("TAG:date"))
        {
          this.year = str3.Split('=')[1];
          this.year = this.year.Split('-')[0];
        }
        this.codec = "M4B";
      }
    }

    public int GetMetaDataWithAtomicParsley(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.supportLibs.atomicParsleyPath;
      process.StartInfo.Arguments = "\"" + file + "\" -t 1";
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      string str1 = end;
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray))
      {
        string str3 = str2.Replace("\r", "");
        if (str3.Contains("©nam"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.title = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("©alb"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.album = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("©ART"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.author = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("©nrt"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.narrator = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("©pub"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.publisher = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("GUID"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.guid = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("©day"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.year = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("des"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.comments = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("cmt"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.cmt = str3.Substring(startIndex, str3.Length - startIndex);
        }
        if (str3.Contains("prID"))
        {
          int startIndex = str3.IndexOf(':') + 2;
          this.bookId = str3.Substring(startIndex, str3.Length - startIndex);
        }
      }
      return process.ExitCode;
    }

    internal void EscapeTags()
    {
      this.title = this.title.Replace('"', '\'');
      this.comments = this.comments.Replace('"', '\'');
      this.cmt = this.cmt.Replace('"', '\'');
    }

    internal void GetCustomAAXMetaData(string file)
    {
      this.StoreOriginalMetaData(file);
      if (this.GetMetaDataWithAtomicParsley(file) == 0)
      {
        this.codec = "M4B";
        if (this.comments.Length < 30)
          this.comments = this.GetCustomAAXTags(file, "cmt", (byte) 169);
        this.EscapeTags();
        this.id = this.GetAudibleId(file);
      }
      else
      {
        try
        {
          this.title = this.GetCustomAAXTags(file, "nam", (byte) 169);
          this.album = this.GetCustomAAXTags(file, "alb", (byte) 169);
          this.author = this.GetCustomAAXTags(file, "ART", (byte) 169);
          this.narrator = this.GetCustomAAXTags(file, "nrt", (byte) 169);
          this.publisher = this.GetCustomAAXTags(file, "pub", (byte) 169);
          this.guid = this.GetCustomAAXTags(file, "GUID", (byte) 38);
          this.codec = "M4B";
          this.year = this.GetCustomAAXTags(file, "day", (byte) 169);
          this.comments = this.GetCustomAAXTags(file, "des", (byte) 169);
          this.cmt = this.GetCustomAAXTags(file, "cmt", (byte) 169);
          if (this.comments.Length < 30)
            this.comments = this.GetCustomAAXTags(file, "cmt", (byte) 169);
          this.EscapeTags();
          this.id = this.GetAudibleId(file);
        }
        catch
        {
          Audible.diskLogger("Failed to parse AAX header with custom code");
          this.title = "Title";
          this.author = "Author";
          this.narrator = "Narrator";
          this.year = "1999";
          this.id = "none";
        }
      }
    }

    public void StoreOriginalMetaData(string file)
    {
      try
      {
        TagLib.File file1 = TagLib.File.Create(file, "audio/mp4", ReadStyle.Average);
        this.originalTags = (AppleTag) file1.GetTag(TagTypes.Apple, true);
        file1.Dispose();
      }
      catch (Exception ex)
      {
        Audible.diskLogger("blew up trying to read AAX metadata: " + ex.ToString());
      }
    }

    public void GetMetaDataTaglib(string file)
    {
      try
      {
        TagLib.File file1 = TagLib.File.Create(file, "audio/mp4", ReadStyle.Average);
        this.title = file1.Tag.Title;
        this.author = file1.Tag.Performers[0];
        this.codec = "M4B";
        this.year = file1.Tag.Year.ToString();
        file1.Dispose();
        this.comments = file1.Tag.Comment;
        this.duration = file1.Properties.Duration;
        this.EscapeTags();
      }
      catch
      {
        this.GetCustomAAXMetaData(file);
      }
    }

    internal void getAAXMetaData(string file)
    {
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = this.audibleChaptersPath;
      process.StartInfo.Arguments = string.Format("\"{0}\"", (object) file);
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = false;
      process.Start();
      string end = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      try
      {
        string[] strArray1 = end.Split('\n');
        this.title = strArray1[4].Trim();
        string[] strArray2 = this.title.Split(' ');
        this.title = strArray2[0];
        for (int index = 1; index < strArray2.Length - 1; ++index)
        {
          Audible audible = this;
          string str = audible.title + " " + strArray2[index];
          audible.title = str;
        }
        string[] strArray3 = strArray1[5].Split('(');
        string[] strArray4 = strArray3[1].Split(')');
        this.author = strArray3[0].Trim();
        this.narrator = strArray4[0].Trim();
        this.codec = "M4B";
        string[] strArray5 = strArray1[4].Split('(');
        string str1 = strArray5[strArray5.Length - 1].Trim();
        this.year = str1.Substring(0, str1.Length - 1);
        this.id = this.GetAudibleId(file);
      }
      catch
      {
        Audible.diskLogger("Failed to get meta info with AudibleChapters.  Using custom code...");
        this.GetCustomAAXMetaData(file);
      }
    }

    internal bool findOverlap(string file1, string file2)
    {
      bool flag1 = true;
      long length1 = new FileInfo(file2).Length;
      long length2 = new FileInfo(file1).Length;
      long length3 = 100000000;
      long offset = 0;
      byte[] buffer;
      if (length2 > length3)
      {
        buffer = new byte[length3];
        offset = length2 - length3;
        using (BinaryReader binaryReader = new BinaryReader((Stream) new FileStream(file1, FileMode.Open)))
        {
          binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
          binaryReader.Read(buffer, 0, (int) length3);
        }
      }
      else
        buffer = System.IO.File.ReadAllBytes(file1);
      long length4 = 100000000;
      if (length1 < length4)
        length4 = length1;
      byte[] numArray = new byte[length4];
      using (FileStream fileStream = new FileStream(file2, FileMode.Open, FileAccess.Read))
      {
        fileStream.Read(numArray, 0, numArray.Length);
        fileStream.Close();
      }
      Audible.diskLogger(file1 + ": sourceData = " + (object) numArray.Length + ", " + file2 + ": targetData = " + (object) buffer.Length);
      long sourceOffset = 44;
      long sourcePatternSize = 1048576;
      long num1 = 102400;
      byte[] pattern1 = this.copySubset(sourceOffset, sourcePatternSize, numArray);
      int num2 = (int) Audible.ByteSearchBHM(buffer, pattern1, 1);
      bool flag2 = false;
      int num3 = 100;
      int tries = 1;
      if (this.advancedOptions.overlapOverride)
        flag2 = true;
      while (num2 < 0)
      {
        Console.WriteLine(sourceOffset);
        sourceOffset += num1;
        try
        {
          byte[] pattern2 = this.copySubset(sourceOffset, sourcePatternSize, numArray);
          num2 = flag2 ? (int) Audible.ByteSearchBHM(buffer, pattern2, -1) : (int) Audible.ByteSearchBHM(buffer, pattern2, tries);
          Audible.diskLogger("pass = " + (object) tries);
        }
        catch
        {
          Audible.diskLogger("Could not find overlapping audio within threshold limits.  Restarting in brute force mode.");
          sourceOffset = 44L;
          flag2 = true;
        }
        ++tries;
        if (tries > num3)
        {
          Audible.diskLogger("Failed to find overlap.");
          return false;
        }
      }
      Audible.diskLogger("Found at " + (object) num2 + " in " + (object) tries);
      this.startOfPatternInSourceFile = sourceOffset;
      this.startOfPatternInTargetFile = (long) num2 + offset;
      return flag1;
    }

    public byte[] copySubset(long sourceOffset, long sourcePatternSize, byte[] sourceData)
    {
      byte[] numArray = new byte[sourcePatternSize];
      int index1 = 0;
      for (long index2 = sourceOffset; index2 < sourcePatternSize + sourceOffset; ++index2)
      {
        numArray[index1] = sourceData[index2];
        ++index1;
      }
      return numArray;
    }

    internal void mergeWAV(string appendTo, string appendFrom, string output)
    {
      long length1 = new FileInfo(appendTo).Length;
      long length2 = new FileInfo(appendFrom).Length;
      Audible.diskLogger("From = " + (object) length2 + " (" + (object) this.startOfPatternInTargetFile + ") , To = " + (object) length1 + " (" + (object) (length2 - this.startOfPatternInSourceFile) + ")");
      byte[] numArray1 = System.IO.File.ReadAllBytes(appendTo);
      byte[] numArray2 = System.IO.File.ReadAllBytes(appendFrom);
      long length3 = this.startOfPatternInTargetFile + (length2 - this.startOfPatternInSourceFile);
      Console.WriteLine("Target Size = " + (object) length3);
      byte[] bytes = new byte[length3];
      Console.WriteLine("Part 1...");
      int index1 = 0;
      for (long index2 = 0; index2 < this.startOfPatternInTargetFile; ++index2)
      {
        bytes[index2] = numArray1[index2];
        ++index1;
      }
      Console.WriteLine("Part 2...");
      for (long patternInSourceFile = this.startOfPatternInSourceFile; patternInSourceFile < length2; ++patternInSourceFile)
      {
        bytes[index1] = numArray2[patternInSourceFile];
        ++index1;
      }
      int num1 = (int) length3 - 8;
      bytes[4] = (byte) (num1 & (int) byte.MaxValue);
      bytes[5] = (byte) ((num1 & 65280) >> 8);
      bytes[6] = (byte) ((num1 & 16711680) >> 16);
      bytes[7] = (byte) (((long) num1 & 4278190080L) >> 24);
      int num2 = (int) length3 - 44;
      bytes[40] = (byte) (num2 & (int) byte.MaxValue);
      bytes[41] = (byte) ((num2 & 65280) >> 8);
      bytes[42] = (byte) ((num2 & 16711680) >> 16);
      bytes[43] = (byte) (((long) num2 & 4278190080L) >> 24);
      Console.WriteLine("Writing...");
      System.IO.File.WriteAllBytes(output, bytes);
      Console.WriteLine("Done...");
    }

    internal byte[] mergeWAVPipe(string appendTo, string appendFrom)
    {
      long length1 = new FileInfo(appendTo).Length;
      long length2 = new FileInfo(appendFrom).Length;
      Audible.diskLogger("From = " + (object) length2 + " (" + (object) this.startOfPatternInTargetFile + ") , To = " + (object) length1 + " (" + (object) (length2 - this.startOfPatternInSourceFile) + ")");
      byte[] numArray1 = System.IO.File.ReadAllBytes(appendTo);
      byte[] numArray2 = System.IO.File.ReadAllBytes(appendFrom);
      long length3 = this.startOfPatternInTargetFile + (length2 - this.startOfPatternInSourceFile);
      Console.WriteLine("Target Size = " + (object) length3);
      byte[] numArray3 = new byte[length3];
      Console.WriteLine("Part 1...");
      int index1 = 0;
      for (long index2 = 0; index2 < this.startOfPatternInTargetFile; ++index2)
      {
        numArray3[index2] = numArray1[index2];
        ++index1;
      }
      Console.WriteLine("Part 2...");
      for (long patternInSourceFile = this.startOfPatternInSourceFile; patternInSourceFile < length2; ++patternInSourceFile)
      {
        numArray3[index1] = numArray2[patternInSourceFile];
        ++index1;
      }
      int num1 = (int) length3 - 8;
      numArray3[4] = (byte) (num1 & (int) byte.MaxValue);
      numArray3[5] = (byte) ((num1 & 65280) >> 8);
      numArray3[6] = (byte) ((num1 & 16711680) >> 16);
      numArray3[7] = (byte) (((long) num1 & 4278190080L) >> 24);
      int num2 = (int) length3 - 44;
      numArray3[40] = (byte) (num2 & (int) byte.MaxValue);
      numArray3[41] = (byte) ((num2 & 65280) >> 8);
      numArray3[42] = (byte) ((num2 & 16711680) >> 16);
      numArray3[43] = (byte) (((long) num2 & 4278190080L) >> 24);
      byte[] numArray4 = new byte[numArray3.Length - 44];
      long index3 = 0;
      for (long index2 = 44; index2 < (long) (numArray3.Length - 44); ++index2)
      {
        numArray4[index3] = numArray3[index2];
        ++index3;
      }
      Audible.diskLogger("merged stream prepared");
      return numArray4;
    }

    internal void createM4BchapterFile(List<double> chapters, List<string> chapNames, string filename)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num = 1;
      foreach (double chapter in chapters)
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds(chapter);
        stringBuilder.AppendLine("CHAPTER" + (object) num + "=" + (object) (int) timeSpan.TotalHours + ":" + (object) timeSpan.Minutes + ":" + (object) timeSpan.Seconds + "." + (object) timeSpan.Milliseconds);
        string chapterTitle = this.GetChapterTitle(chapters, chapNames, num - 1);
        stringBuilder.AppendLine("CHAPTER" + (object) num + "NAME=" + chapterTitle);
        ++num;
      }
      System.IO.File.WriteAllText(filename, stringBuilder.ToString());
    }

    public string GetChapterTitle(List<double> chapters, List<string> chapNames, int chapterPos)
    {
      if (chapterPos == chapNames.Count)
        return "(End)";
      if (chapterPos >= chapNames.Count)
        return "Chapter " + (object) chapterPos;
      return chapNames[chapterPos];
    }

    public bool IsChapterTitleSet(List<double> chapters, List<string> chapNames, int chapterPos)
    {
      return chapterPos <= chapNames.Count && chapNames.Count > 0;
    }

    public static void diskLogger(string text)
    {
      Path.GetTempPath();
      string name = new StackTrace().GetFrame(1).GetMethod().Name;
//"[" + DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] - " + name + " - " + text + "\r\n";
      try
      {
        CLog.WriteLine(LOG_TYPE.LOG_DEBUG, name + " - " + text);
      }
      catch
      {
      }
    }
  }
}
