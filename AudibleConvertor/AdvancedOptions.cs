// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.AdvancedOptions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Data;
using System.Drawing;
using System.IO;

namespace AudibleConvertor
{
  public class AdvancedOptions
  {
    public bool decrypt = true;
    public bool ng = true;
    public string[] ngKeys = new string[0];
    public string AudibleMangerDLLPath = "c:\\Program Files (x86)\\Audible\\Bin\\AAXSDKWin.dll";
    public string ffmpegPath = "";
    public bool fdk = true;
    public bool nfo = true;
    public string outputPath = "";
    private string tempPath = Path.GetTempPath();
    public string completion = "none";
    public int iOSSplitThreshold = 8;
    public int iOSSplitSize = 6;
    public int iOSMinSplitSize = 2;
    public int verifySplitsSearchSize = 5;
    public bool doNotVerifyIfSilence = true;
    public bool lowQualityPreview = true;
    public bool chapterEditorTempFile = true;
    public string audibleCustomerId = "";
    public string renameOptions = "";
    public bool newRipper = true;
    public string threadPriority = "Normal";
    private bool allowFormResize = true;
    private string _downloadPath = "C:\\Users\\Public\\Documents\\Audible\\Downloads";
    private bool _removeTinyChapters = true;
    public bool overlapOverride;
    public bool iTunesMode;
    public bool aaDirectShow;
    public bool beep;
    public bool cylon;
    public bool SHA256Checksum;
    public string[] genres;
    public DataTable codecOptions;
    public bool legacyChapterMode;
    public bool okayToSaveSettings;
    public bool useChapterAsTitle;
    public bool includeChapterNumberInFilename;
    public bool includeChapterAndTitleInTitleTag;
    public bool noTitleInFilename;
    private Size _savedFormSize;
    private AdvancedOptions.SplitTypes _splitMode;

    public bool OptionsLoaded { get; set; }

    public bool AppleTags { get; set; }

    public bool AudibleScraping { get; set; }

    public bool EncodeAfterDownload { get; set; }

    public bool RevertedDownloadPath { get; set; }

    public string BardUser { get; set; }

    public string BardPassword { get; set; }

    public bool RemoveTinyChapters
    {
      get
      {
        return this._removeTinyChapters;
      }
      set
      {
        this._removeTinyChapters = value;
      }
    }

    public string DownloadPath
    {
      get
      {
        if (Directory.Exists(this._downloadPath))
          return this._downloadPath;
        Audible.diskLogger("Download path does not exsist.  Reverting to desktop folder.");
        this._downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        this.RevertedDownloadPath = true;
        return this._downloadPath;
      }
      set
      {
        this._downloadPath = value;
      }
    }

    public string GetTempPath()
    {
      if (this.tempPath == "" || this.tempPath == null)
        return Path.GetTempPath();
      return this.tempPath;
    }

    public Size SavedFormSize
    {
      get
      {
        return this._savedFormSize;
      }
      set
      {
        this._savedFormSize = value;
      }
    }

    public bool AllowFormResize
    {
      get
      {
        return this.allowFormResize;
      }
      set
      {
        this.allowFormResize = value;
      }
    }

    public AdvancedOptions.SplitTypes SplitMode
    {
      get
      {
        return this._splitMode;
      }
      set
      {
        this._splitMode = value;
      }
    }

    public void SetTempPath(string s)
    {
      this.tempPath = s;
    }

    public AdvancedOptions()
    {
      this.codecOptions = new DataTable();
      this.codecOptions.Columns.Add("Codec", typeof (string));
      this.codecOptions.Columns.Add("Swtiches", typeof (string));
      this.codecOptions.Rows.Add((object) "LAME", (object) "--noreplaygain");
      this.codecOptions.Rows.Add((object) "Helix", (object) "");
      this.codecOptions.Rows.Add((object) "NeroAAC", (object) "");
      this.codecOptions.Rows.Add((object) "FDK", (object) "");
      this.codecOptions.Rows.Add((object) "FLAC", (object) "");
      this.codecOptions.Rows.Add((object) "Ogg", (object) "");
      this.codecOptions.Rows.Add((object) "Opus", (object) "");
    }

    public string GetCodecOptions(string codec)
    {
      return this.codecOptions.Select("Codec = '" + codec + "'")[0][1].ToString();
    }

    public enum SplitTypes
    {
      ffmpeg,
      MP3SPLT,
    }
  }
}
