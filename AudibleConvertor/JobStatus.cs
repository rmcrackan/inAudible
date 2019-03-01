// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.JobStatus
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

namespace AudibleConvertor
{
  public class JobStatus
  {
    private bool _shown = true;
    private string _message = "";
    private int _fileNum;
    private int _chapterNum;

    public void SetStatus(int file, int chap, bool shown, string message)
    {
      this.FileNum = file;
      this.ChapterNum = chap;
      this.Shown = shown;
      this.Message = message;
    }

    public string GetStatus()
    {
      this.Shown = true;
      return this.Message;
    }

    public int FileNum
    {
      get
      {
        return this._fileNum;
      }
      set
      {
        this._fileNum = value;
      }
    }

    public int ChapterNum
    {
      get
      {
        return this._chapterNum;
      }
      set
      {
        this._chapterNum = value;
      }
    }

    public bool Shown
    {
      get
      {
        return this._shown;
      }
      set
      {
        this._shown = value;
      }
    }

    public string Message
    {
      get
      {
        return this._message;
      }
      set
      {
        this._message = value;
      }
    }
  }
}
