// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.MimeType
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AudibleConvertor
{
  public class MimeType
  {
    private static readonly byte[] BMP = new byte[2]
    {
      (byte) 66,
      (byte) 77
    };
    private static readonly byte[] DOC = new byte[8]
    {
      (byte) 208,
      (byte) 207,
      (byte) 17,
      (byte) 224,
      (byte) 161,
      (byte) 177,
      (byte) 26,
      (byte) 225
    };
    private static readonly byte[] EXE_DLL = new byte[2]
    {
      (byte) 77,
      (byte) 90
    };
    private static readonly byte[] GIF = new byte[4]
    {
      (byte) 71,
      (byte) 73,
      (byte) 70,
      (byte) 56
    };
    private static readonly byte[] ICO = new byte[4]
    {
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 0
    };
    private static readonly byte[] JPG = new byte[3]
    {
      byte.MaxValue,
      (byte) 216,
      byte.MaxValue
    };
    private static readonly byte[] MP3 = new byte[3]
    {
      byte.MaxValue,
      (byte) 251,
      (byte) 48
    };
    private static readonly byte[] OGG = new byte[14]
    {
      (byte) 79,
      (byte) 103,
      (byte) 103,
      (byte) 83,
      (byte) 0,
      (byte) 2,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
    private static readonly byte[] PDF = new byte[7]
    {
      (byte) 37,
      (byte) 80,
      (byte) 68,
      (byte) 70,
      (byte) 45,
      (byte) 49,
      (byte) 46
    };
    private static readonly byte[] PNG = new byte[16]
    {
      (byte) 137,
      (byte) 80,
      (byte) 78,
      (byte) 71,
      (byte) 13,
      (byte) 10,
      (byte) 26,
      (byte) 10,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 13,
      (byte) 73,
      (byte) 72,
      (byte) 68,
      (byte) 82
    };
    private static readonly byte[] RAR = new byte[7]
    {
      (byte) 82,
      (byte) 97,
      (byte) 114,
      (byte) 33,
      (byte) 26,
      (byte) 7,
      (byte) 0
    };
    private static readonly byte[] SWF = new byte[3]
    {
      (byte) 70,
      (byte) 87,
      (byte) 83
    };
    private static readonly byte[] TIFF = new byte[4]
    {
      (byte) 73,
      (byte) 73,
      (byte) 42,
      (byte) 0
    };
    private static readonly byte[] TORRENT = new byte[11]
    {
      (byte) 100,
      (byte) 56,
      (byte) 58,
      (byte) 97,
      (byte) 110,
      (byte) 110,
      (byte) 111,
      (byte) 117,
      (byte) 110,
      (byte) 99,
      (byte) 101
    };
    private static readonly byte[] TTF = new byte[5]
    {
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
    private static readonly byte[] WAV_AVI = new byte[4]
    {
      (byte) 82,
      (byte) 73,
      (byte) 70,
      (byte) 70
    };
    private static readonly byte[] WMV_WMA = new byte[16]
    {
      (byte) 48,
      (byte) 38,
      (byte) 178,
      (byte) 117,
      (byte) 142,
      (byte) 102,
      (byte) 207,
      (byte) 17,
      (byte) 166,
      (byte) 217,
      (byte) 0,
      (byte) 170,
      (byte) 0,
      (byte) 98,
      (byte) 206,
      (byte) 108
    };
    private static readonly byte[] ZIP_DOCX = new byte[4]
    {
      (byte) 80,
      (byte) 75,
      (byte) 3,
      (byte) 4
    };

    public static string GetMimeType(byte[] file, string fileName)
    {
      string str1 = "application/octet-stream";
      if (string.IsNullOrWhiteSpace(fileName))
        return str1;
      string str2 = Path.GetExtension(fileName) == null ? string.Empty : Path.GetExtension(fileName).ToUpper();
      if (((IEnumerable<byte>) file).Take<byte>(2).SequenceEqual<byte>((IEnumerable<byte>) MimeType.BMP))
        str1 = "image/bmp";
      else if (((IEnumerable<byte>) file).Take<byte>(8).SequenceEqual<byte>((IEnumerable<byte>) MimeType.DOC))
        str1 = "application/msword";
      else if (((IEnumerable<byte>) file).Take<byte>(2).SequenceEqual<byte>((IEnumerable<byte>) MimeType.EXE_DLL))
        str1 = "application/x-msdownload";
      else if (((IEnumerable<byte>) file).Take<byte>(4).SequenceEqual<byte>((IEnumerable<byte>) MimeType.GIF))
        str1 = "image/gif";
      else if (((IEnumerable<byte>) file).Take<byte>(4).SequenceEqual<byte>((IEnumerable<byte>) MimeType.ICO))
        str1 = "image/x-icon";
      else if (((IEnumerable<byte>) file).Take<byte>(3).SequenceEqual<byte>((IEnumerable<byte>) MimeType.JPG))
        str1 = "image/jpeg";
      else if (((IEnumerable<byte>) file).Take<byte>(3).SequenceEqual<byte>((IEnumerable<byte>) MimeType.MP3))
        str1 = "audio/mpeg";
      else if (((IEnumerable<byte>) file).Take<byte>(14).SequenceEqual<byte>((IEnumerable<byte>) MimeType.OGG))
        str1 = !(str2 == ".OGX") ? (!(str2 == ".OGA") ? "video/ogg" : "audio/ogg") : "application/ogg";
      else if (((IEnumerable<byte>) file).Take<byte>(7).SequenceEqual<byte>((IEnumerable<byte>) MimeType.PDF))
        str1 = "application/pdf";
      else if (((IEnumerable<byte>) file).Take<byte>(16).SequenceEqual<byte>((IEnumerable<byte>) MimeType.PNG))
        str1 = "image/png";
      else if (((IEnumerable<byte>) file).Take<byte>(7).SequenceEqual<byte>((IEnumerable<byte>) MimeType.RAR))
        str1 = "application/x-rar-compressed";
      else if (((IEnumerable<byte>) file).Take<byte>(3).SequenceEqual<byte>((IEnumerable<byte>) MimeType.SWF))
        str1 = "application/x-shockwave-flash";
      else if (((IEnumerable<byte>) file).Take<byte>(4).SequenceEqual<byte>((IEnumerable<byte>) MimeType.TIFF))
        str1 = "image/tiff";
      else if (((IEnumerable<byte>) file).Take<byte>(11).SequenceEqual<byte>((IEnumerable<byte>) MimeType.TORRENT))
        str1 = "application/x-bittorrent";
      else if (((IEnumerable<byte>) file).Take<byte>(5).SequenceEqual<byte>((IEnumerable<byte>) MimeType.TTF))
        str1 = "application/x-font-ttf";
      else if (((IEnumerable<byte>) file).Take<byte>(4).SequenceEqual<byte>((IEnumerable<byte>) MimeType.WAV_AVI))
        str1 = str2 == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
      else if (((IEnumerable<byte>) file).Take<byte>(16).SequenceEqual<byte>((IEnumerable<byte>) MimeType.WMV_WMA))
        str1 = str2 == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
      else if (((IEnumerable<byte>) file).Take<byte>(4).SequenceEqual<byte>((IEnumerable<byte>) MimeType.ZIP_DOCX))
        str1 = str2 == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/x-zip-compressed";
      return str1;
    }
  }
}
