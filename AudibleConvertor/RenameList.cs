// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.RenameList
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.ComponentModel;
using System.IO;

namespace AudibleConvertor
{
  public class RenameList : INotifyPropertyChanged
  {
    private string _sourceFile;
    private string _targetFile;
    private string _sourceFileName;
    private string _targetFileName;
    private Audible _audible;

    public event PropertyChangedEventHandler PropertyChanged;

    public RenameList(string sourceFile, string targetFile)
    {
      this._sourceFile = sourceFile;
      this._targetFile = targetFile;
      this._sourceFileName = Path.GetFileName(sourceFile);
      this._targetFileName = Path.GetFileName(targetFile);
    }

    public Audible Audible
    {
      get
      {
        return this._audible;
      }
      set
      {
        this._audible = value;
        this.NotifyPropertyChanged(nameof (Audible));
      }
    }

    public string SourceFile
    {
      get
      {
        return this._sourceFile;
      }
      set
      {
        this._sourceFile = value;
        this.NotifyPropertyChanged(nameof (SourceFile));
      }
    }

    public string SourceFileName
    {
      get
      {
        return this._sourceFileName;
      }
      set
      {
        this._sourceFileName = value;
        this.NotifyPropertyChanged(nameof (SourceFileName));
      }
    }

    public string TargetFile
    {
      get
      {
        return this._targetFile;
      }
      set
      {
        this._targetFile = value;
        this.NotifyPropertyChanged(nameof (TargetFile));
      }
    }

    public string TargetFileName
    {
      get
      {
        return this._targetFileName;
      }
      set
      {
        this._targetFileName = value;
        this.NotifyPropertyChanged(nameof (TargetFileName));
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
