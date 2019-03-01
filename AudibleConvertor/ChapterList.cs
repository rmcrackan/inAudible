// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.ChapterList
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.ComponentModel;

namespace AudibleConvertor
{
  public class ChapterList : INotifyPropertyChanged
  {
    public bool chapterNumberInName = true;
    public bool titleInChapterName = true;
    public string title = "";
    private int _chapterNum;
    private string _chapterName;
    private string _fullChapterName;

    public event PropertyChangedEventHandler PropertyChanged;

    public ChapterList(int chapterNum, string chapterName)
    {
      this._chapterNum = chapterNum;
      this._chapterName = chapterName;
      if (this.chapterNumberInName)
        this._fullChapterName = (chapterNum + 1).ToString("D3") + " - " + chapterName;
      else
        this._fullChapterName = chapterName;
    }

    public ChapterList(int chapterNum, string chapterName, string titleName)
    {
      this._chapterNum = chapterNum;
      this._chapterName = chapterName;
      this.title = titleName;
      if (this.chapterNumberInName)
        this._fullChapterName = titleName + " - " + (chapterNum + 1).ToString("D3") + " - " + chapterName;
      else
        this._fullChapterName = titleName + " - " + chapterName;
    }

    public void SetFullName(bool num)
    {
      this.chapterNumberInName = num;
      this._fullChapterName = !this.chapterNumberInName ? this._chapterName : (this._chapterNum + 1).ToString("D3") + " - " + this._chapterName;
      this.NotifyPropertyChanged("FullName");
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
        this.NotifyPropertyChanged(nameof (ChapterNum));
      }
    }

    public string ChapterName
    {
      get
      {
        return this._chapterName;
      }
      set
      {
        this._chapterName = value;
        this.NotifyPropertyChanged(nameof (ChapterName));
        this.SetFullName(this.chapterNumberInName);
      }
    }

    public string FullName
    {
      get
      {
        return this._fullChapterName;
      }
      set
      {
        this._fullChapterName = value;
        this.NotifyPropertyChanged(nameof (FullName));
      }
    }

    private void NotifyPropertyChanged(string name)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(name));
    }
  }
}
