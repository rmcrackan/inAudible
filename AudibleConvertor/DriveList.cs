// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.DriveList
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using CUETools.AccurateRip;
using CUETools.CDImage;
using CUETools.Ripper.SCSI;
using System;
using System.ComponentModel;
using System.Management;
using System.Net;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class DriveList : INotifyPropertyChanged
  {
    public bool empty = true;
    private string _driveLetter;
    private string _info;
    private string _tracks;
    private string _model;
    private string _discTime;
    private string _discNum;
    private string _progress;
    private string _errors;
    private int _mode;
    private int _progressBar;
    private bool _inAccurateRipDB;
    private string _accurateRip;
    public DateTime realStart;

    public event PropertyChangedEventHandler PropertyChanged;

    public DriveList(string driveLetter, int discNum)
    {
      this._driveLetter = driveLetter;
      this._discNum = discNum != 0 ? discNum.ToString() : "";
      this._model = this.GetDriveModel(this._driveLetter);
      this.GetDiscStats(driveLetter);
    }

    private string GetDriveModel(string driveLetter)
    {
      CDDriveReader cdDriveReader = new CDDriveReader();
      string arName;
      try
      {
        cdDriveReader.Open(driveLetter.ToCharArray()[0]);
        arName = cdDriveReader.ARName;
        cdDriveReader.Close();
      }
      catch
      {
        arName = cdDriveReader.ARName;
        cdDriveReader.Close();
      }
      return arName;
    }

    private string GetHardwareInfo(string driveLetter)
    {
      string str = "";
      try
      {
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive").Get())
        {
          if (managementObject["Drive"].ToString() == driveLetter + ":")
          {
            str = managementObject["Caption"].ToString();
            break;
          }
        }
      }
      catch (ManagementException ex)
      {
        int num = (int) MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
      }
      return str;
    }

    private void GetDiscStats(string driveLetter)
    {
      CDDriveReader cdDriveReader = new CDDriveReader();
      try
      {
        cdDriveReader.Open(driveLetter.ToCharArray()[0]);
        if (cdDriveReader.TOC.AudioTracks < 1U)
        {
          this._info = "nada";
          this._tracks = "0";
        }
        else
        {
          this.empty = false;
          this._tracks = cdDriveReader.TOC.AudioTracks.ToString();
          AccurateRipVerify accurateRipVerify = new AccurateRipVerify(cdDriveReader.TOC, WebRequest.GetSystemWebProxy());
          string accurateRipId = AccurateRipVerify.CalculateAccurateRipId(cdDriveReader.TOC);
          accurateRipVerify.ContactAccurateRip(accurateRipId);
          this._discTime = CDImageLayout.TimeToString(cdDriveReader.TOC.AudioLength);
          if (accurateRipVerify.ARStatus == null)
          {
            this._inAccurateRipDB = true;
            this._accurateRip = "Found";
          }
          else
          {
            this._inAccurateRipDB = false;
            this._accurateRip = "Not found";
          }
        }
      }
      catch
      {
        this.empty = true;
        this._tracks = "";
        this._discNum = "";
        this._discTime = "";
        this._inAccurateRipDB = false;
        this._info = "";
        this._errors = "";
        this._mode = -1;
        this._progressBar = 0;
        cdDriveReader.Close();
        return;
      }
      cdDriveReader.Close();
    }

    public string GetModeName()
    {
      string str;
      switch (this._mode)
      {
        case 0:
          str = "Burst";
          break;
        case 1:
          str = "Secure";
          break;
        case 2:
          str = "Paranoid";
          break;
        default:
          str = "";
          break;
      }
      return str;
    }

    public string accurateRip
    {
      get
      {
        return this._accurateRip;
      }
      set
      {
        this._accurateRip = value;
        this.NotifyPropertyChanged(nameof (accurateRip));
      }
    }

    public string drive
    {
      get
      {
        return this._driveLetter;
      }
      set
      {
        this._driveLetter = value;
        this.NotifyPropertyChanged(nameof (drive));
      }
    }

    public string progress
    {
      get
      {
        return this._progress;
      }
      set
      {
        this._progress = value;
        this.NotifyPropertyChanged(nameof (progress));
      }
    }

    public string discNum
    {
      get
      {
        return this._discNum;
      }
      set
      {
        this._discNum = value;
        this.NotifyPropertyChanged(nameof (discNum));
      }
    }

    public int iDiscNum
    {
      get
      {
        return int.Parse(this._discNum);
      }
      set
      {
        this._discNum = value.ToString();
        this.NotifyPropertyChanged("discNum");
      }
    }

    public int progressBar
    {
      get
      {
        return this._progressBar;
      }
      set
      {
        this._progressBar = value;
        this.NotifyPropertyChanged(nameof (progressBar));
      }
    }

    public string model
    {
      get
      {
        return this._model;
      }
      set
      {
        this._model = value;
        this.NotifyPropertyChanged(nameof (model));
      }
    }

    public string discTime
    {
      get
      {
        return this._discTime;
      }
      set
      {
        this._discTime = value;
        this.NotifyPropertyChanged(nameof (discTime));
      }
    }

    public string tracks
    {
      get
      {
        return this._tracks;
      }
      set
      {
        this._tracks = value;
        this.NotifyPropertyChanged(nameof (tracks));
      }
    }

    public string info
    {
      get
      {
        return this._info;
      }
      set
      {
        this._info = value;
        this.NotifyPropertyChanged(nameof (info));
      }
    }

    public string errors
    {
      get
      {
        return this._errors;
      }
      set
      {
        this._errors = value;
        this.NotifyPropertyChanged(nameof (errors));
      }
    }

    public string modeDescription
    {
      get
      {
        return this.GetModeName();
      }
    }

    public int mode
    {
      get
      {
        return this._mode;
      }
      set
      {
        this._mode = value;
        this.NotifyPropertyChanged(nameof (mode));
      }
    }

    private void NotifyPropertyChanged(string name)
    {
      try
      {
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(name));
      }
      catch
      {
      }
    }

    internal void Update(string driveLetter, int trackNum)
    {
      this._discNum = trackNum.ToString();
      this.GetDiscStats(driveLetter);
    }
  }
}
