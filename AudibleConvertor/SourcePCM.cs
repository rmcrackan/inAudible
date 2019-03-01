// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.SourcePCM
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.IO;

namespace AudibleConvertor
{
  [Serializable]
  public class SourcePCM : ICloneable
  {
    public string fileName;
    public long start;
    public long end;
    public long relativeStart;
    public long relativeEnd;

    internal void AddWAV(string inputWav)
    {
      this.fileName = inputWav;
      this.start = this.GetPCMDataOffset();
      try
      {
        this.end = new FileInfo(inputWav).Length;
      }
      catch
      {
      }
    }

    internal void AddRaw(string inputWav)
    {
      this.fileName = inputWav;
      this.start = 0L;
      this.end = new FileInfo(inputWav).Length;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    private long GetPCMDataOffset()
    {
      long num = 44;
      long length = 1000;
      try
      {
        FileStream fileStream = File.OpenRead(this.fileName);
        if (fileStream.Length < length)
          length = fileStream.Length;
        byte[] numArray = new byte[length];
        fileStream.Read(numArray, 0, Convert.ToInt32(length));
        fileStream.Close();
        byte[] needle = new byte[4]
        {
          (byte) 100,
          (byte) 97,
          (byte) 116,
          (byte) 97
        };
        num = (long) SourcePCM.SearchBytes(numArray, needle);
        num += 8L;
        Audible.diskLogger("PCM offset found @ " + (object) num + " in " + this.fileName);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Failed to find PCM offset: " + ex.Message);
      }
      return num;
    }

    private static int SearchBytes(byte[] haystack, byte[] needle)
    {
      int length = needle.Length;
      int num = haystack.Length - length;
      for (int index1 = 0; index1 <= num; ++index1)
      {
        int index2 = 0;
        while (index2 < length && (int) needle[index2] == (int) haystack[index1 + index2])
          ++index2;
        if (index2 == length)
          return index1;
      }
      return -1;
    }
  }
}
