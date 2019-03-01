// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.CoverArt
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.IO;

namespace AudibleConvertor
{
  internal class CoverArt
  {
    public MemoryStream ms = new MemoryStream();
    private string tmpFileName = "";
    private string filePath = "";

    public CoverArt(string homeDir)
    {
      this.filePath = homeDir;
      this.RemoveOrphans();
    }

    private void RemoveOrphans()
    {
      try
      {
        string[] files = Directory.GetFiles(this.filePath, "inaudible-scraped*.jpg");
        Audible.diskLogger("Cleaning up " + (object) files.Length + " orphaned thumbnails.");
        foreach (string path in files)
        {
          try
          {
            File.Delete(path);
          }
          catch
          {
          }
        }
      }
      catch
      {
      }
    }

    public void StoreImage(string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        byte[] buffer = new byte[fileStream.Length];
        fileStream.Read(buffer, 0, (int) fileStream.Length);
        this.ms.Write(buffer, 0, (int) fileStream.Length);
      }
      File.Delete(fileName);
    }

    public string GenerateFileName()
    {
      if (this.tmpFileName != "")
        return this.tmpFileName;
      this.tmpFileName = "inAudible-scraped-" + Guid.NewGuid().ToString() + ".jpg";
      return this.tmpFileName;
    }

    public string GetImage(string tmpPath)
    {
      this.GenerateFileName();
      if (File.Exists(tmpPath + "\\" + this.tmpFileName))
        return this.tmpFileName;
      using (FileStream fileStream = new FileStream(tmpPath + "\\" + this.tmpFileName, FileMode.Create, FileAccess.Write))
      {
        byte[] buffer = new byte[this.ms.Length];
        this.ms.Read(buffer, 0, (int) this.ms.Length);
        fileStream.Write(buffer, 0, buffer.Length);
        this.ms.Position = 0L;
      }
      return this.tmpFileName;
    }

    public void CopyStream(Stream input)
    {
      input.Position = 0L;
      input.CopyTo((Stream) this.ms);
      this.ms.Position = 0L;
    }

    public void Init()
    {
      this.tmpFileName = "";
    }

    public void Cleanup()
    {
      File.Delete(this.filePath + "\\" + this.tmpFileName);
    }
  }
}
