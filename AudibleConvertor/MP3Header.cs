// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.MP3Header
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.IO;

namespace AudibleConvertor
{
  internal class MP3Header
  {
    public int intBitRate;
    public string strFileName;
    public long lngFileSize;
    public int intFrequency;
    public string strMode;
    public int intLength;
    public string strLengthFormatted;
    private ulong bithdr;
    private bool boolVBitRate;
    private int intVFrames;

    public bool ReadMP3Information(string FileName)
    {
      FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
      this.strFileName = fileStream.Name;
      string[] strArray = this.strFileName.Split(new char[2]
      {
        '\\',
        '/'
      });
      int upperBound = strArray.GetUpperBound(0);
      this.strFileName = strArray[upperBound];
      this.strFileName = this.strFileName.Replace("'", "''");
      this.lngFileSize = fileStream.Length;
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[12];
      int num1 = 0;
      do
      {
        fileStream.Position = (long) num1;
        fileStream.Read(numArray1, 0, 4);
        ++num1;
        this.LoadMP3Header(numArray1);
      }
      while (!this.IsValidHeader() && fileStream.Position != fileStream.Length);
      if (fileStream.Position == fileStream.Length)
        return false;
      int num2 = num1 + 3;
      int num3 = this.getVersionIndex() != 3 ? (this.getModeIndex() != 3 ? num2 + 17 : num2 + 9) : (this.getModeIndex() != 3 ? num2 + 32 : num2 + 17);
      fileStream.Position = (long) num3;
      fileStream.Read(numArray2, 0, 12);
      this.boolVBitRate = this.LoadVBRHeader(numArray2);
      this.intBitRate = this.getBitrate();
      this.intFrequency = this.getFrequency();
      this.strMode = this.getMode();
      this.intLength = this.getLengthInSeconds();
      this.strLengthFormatted = this.getFormattedLength();
      fileStream.Close();
      return true;
    }

    private void LoadMP3Header(byte[] c)
    {
      this.bithdr = (ulong) (((int) c[0] & (int) byte.MaxValue) << 24 | ((int) c[1] & (int) byte.MaxValue) << 16 | ((int) c[2] & (int) byte.MaxValue) << 8 | (int) c[3] & (int) byte.MaxValue);
    }

    private bool LoadVBRHeader(byte[] inputheader)
    {
      if ((int) inputheader[0] != 88 || (int) inputheader[1] != 105 || ((int) inputheader[2] != 110 || (int) inputheader[3] != 103))
        return false;
      if (((((int) inputheader[4] & (int) byte.MaxValue) << 24 | ((int) inputheader[5] & (int) byte.MaxValue) << 16 | ((int) inputheader[6] & (int) byte.MaxValue) << 8 | (int) inputheader[7] & (int) byte.MaxValue) & 1) == 1)
      {
        this.intVFrames = ((int) inputheader[8] & (int) byte.MaxValue) << 24 | ((int) inputheader[9] & (int) byte.MaxValue) << 16 | ((int) inputheader[10] & (int) byte.MaxValue) << 8 | (int) inputheader[11] & (int) byte.MaxValue;
        return true;
      }
      this.intVFrames = -1;
      return true;
    }

    private bool IsValidHeader()
    {
      if ((this.getFrameSync() & 2047) == 2047 && (this.getVersionIndex() & 3) != 1 && ((this.getLayerIndex() & 3) != 0 && (this.getBitrateIndex() & 15) != 0) && ((this.getBitrateIndex() & 15) != 15 && (this.getFrequencyIndex() & 3) != 3))
        return (this.getEmphasisIndex() & 3) != 2;
      return false;
    }

    private int getFrameSync()
    {
      return (int) ((long) (this.bithdr >> 21) & 2047L);
    }

    private int getVersionIndex()
    {
      return (int) ((long) (this.bithdr >> 19) & 3L);
    }

    private int getLayerIndex()
    {
      return (int) ((long) (this.bithdr >> 17) & 3L);
    }

    private int getProtectionBit()
    {
      return (int) ((long) (this.bithdr >> 16) & 1L);
    }

    private int getBitrateIndex()
    {
      return (int) ((long) (this.bithdr >> 12) & 15L);
    }

    private int getFrequencyIndex()
    {
      return (int) ((long) (this.bithdr >> 10) & 3L);
    }

    private int getPaddingBit()
    {
      return (int) ((long) (this.bithdr >> 9) & 1L);
    }

    private int getPrivateBit()
    {
      return (int) ((long) (this.bithdr >> 8) & 1L);
    }

    private int getModeIndex()
    {
      return (int) ((long) (this.bithdr >> 6) & 3L);
    }

    private int getModeExtIndex()
    {
      return (int) ((long) (this.bithdr >> 4) & 3L);
    }

    private int getCoprightBit()
    {
      return (int) ((long) (this.bithdr >> 3) & 1L);
    }

    private int getOrginalBit()
    {
      return (int) ((long) (this.bithdr >> 2) & 1L);
    }

    private int getEmphasisIndex()
    {
      return (int) ((long) this.bithdr & 3L);
    }

    private double getVersion()
    {
      return new double[4]{ 2.5, 0.0, 2.0, 1.0 }[this.getVersionIndex()];
    }

    private int getLayer()
    {
      return 4 - this.getLayerIndex();
    }

    private int getBitrate()
    {
      if (this.boolVBitRate)
        return (int) ((double) this.lngFileSize / (double) this.getNumberOfFrames() * (double) this.getFrequency() / (1000.0 * (this.getLayerIndex() == 3 ? 12.0 : 144.0)));
      return new int[2, 3, 16]
      {
        {
          {
            0,
            8,
            16,
            24,
            32,
            40,
            48,
            56,
            64,
            80,
            96,
            112,
            128,
            144,
            160,
            0
          },
          {
            0,
            8,
            16,
            24,
            32,
            40,
            48,
            56,
            64,
            80,
            96,
            112,
            128,
            144,
            160,
            0
          },
          {
            0,
            32,
            48,
            56,
            64,
            80,
            96,
            112,
            128,
            144,
            160,
            176,
            192,
            224,
            256,
            0
          }
        },
        {
          {
            0,
            32,
            40,
            48,
            56,
            64,
            80,
            96,
            112,
            128,
            160,
            192,
            224,
            256,
            320,
            0
          },
          {
            0,
            32,
            48,
            56,
            64,
            80,
            96,
            112,
            128,
            160,
            192,
            224,
            256,
            320,
            384,
            0
          },
          {
            0,
            32,
            64,
            96,
            128,
            160,
            192,
            224,
            256,
            288,
            320,
            352,
            384,
            416,
            448,
            0
          }
        }
      }[this.getVersionIndex() & 1, this.getLayerIndex() - 1, this.getBitrateIndex()];
    }

    private int getFrequency()
    {
      return new int[4, 3]
      {
        {
          32000,
          16000,
          8000
        },
        {
          0,
          0,
          0
        },
        {
          22050,
          24000,
          16000
        },
        {
          44100,
          48000,
          32000
        }
      }[this.getVersionIndex(), this.getFrequencyIndex()];
    }

    private string getMode()
    {
      switch (this.getModeIndex())
      {
        case 1:
          return "Joint Stereo";
        case 2:
          return "Dual Channel";
        case 3:
          return "Single Channel";
        default:
          return "Stereo";
      }
    }

    private int getLengthInSeconds()
    {
      return (int) (8L * this.lngFileSize / 1000L) / this.getBitrate();
    }

    private string getFormattedLength()
    {
      int lengthInSeconds = this.getLengthInSeconds();
      int num1 = lengthInSeconds % 60;
      int num2 = (lengthInSeconds - num1) / 60;
      int num3 = num2 % 60;
      return ((num2 - num3) / 60).ToString("D2") + ":" + num3.ToString("D2") + ":" + num1.ToString("D2");
    }

    private int getNumberOfFrames()
    {
      if (!this.boolVBitRate)
        return (int) ((double) this.lngFileSize / ((this.getLayerIndex() == 3 ? 12.0 : 144.0) * (1000.0 * (double) this.getBitrate() / (double) this.getFrequency())));
      return this.intVFrames;
    }
  }
}
