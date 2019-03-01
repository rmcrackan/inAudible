// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.SupportLibraries
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.IO;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class SupportLibraries
  {
    public static string appPath = Path.GetDirectoryName(AudibleConvertor.GLOBALS.ExecutablePath);
    public string audibleChaptersPath = SupportLibraries.appPath + "\\AudibleChapters.exe";
    public string neroAACpath = SupportLibraries.appPath + "\\neroAacEnc.exe";
    public string mp4chapsPath = SupportLibraries.appPath + "\\mp4chaps.exe";
    public string neroAACtagPath = SupportLibraries.appPath + "\\neroAacTag.exe";
    public string soxPath = SupportLibraries.appPath + "\\sox.exe";
    public string iTunesProxyPath = SupportLibraries.appPath + "\\iTunesProxy.exe";
    public string lamePath = SupportLibraries.appPath + "\\lame.exe";
    public string helixPath = SupportLibraries.appPath + "\\hmp3.exe";
    public string mp3valPath = SupportLibraries.appPath + "\\mp3val.exe";
    public string mp3SplitPath = SupportLibraries.appPath + "\\mp3split\\mp3splt.exe";
    public string mp4boxPath = SupportLibraries.appPath + "\\mp4box\\mp4box.exe";
    public string ngPath = SupportLibraries.appPath + "\\ng\\";
    public string ffmpegPath = SupportLibraries.appPath + "\\ffmpeg\\ffmpeg.exe";
    public string mp3packerPath = SupportLibraries.appPath + "\\mp3packer.exe";
    public string ffprobePath = SupportLibraries.appPath + "\\ffmpeg\\ffprobe.exe";
    public string opusPath = SupportLibraries.appPath + "\\opusEnc.exe";
    public string oggPath = SupportLibraries.appPath + "\\oggenc2.exe";
    public string cddaPath = SupportLibraries.appPath + "\\cdda2wav.exe";
    public string mp4ArtPath = SupportLibraries.appPath + "\\mp4art.exe";
    public string aax2wavPath = SupportLibraries.appPath + "\\aax2wav.exe";
    public string instaripPath = SupportLibraries.appPath + "\\instarip.exe";
    public string BardEmu = SupportLibraries.appPath + "\\bard\\bardemu.exe";
    public string BardDecoder = SupportLibraries.appPath + "\\bard\\decoder.exe";
    public string atomicParsleyPath = SupportLibraries.appPath + "\\AtomicParsley.exe";
    public string trackDumpPath = SupportLibraries.appPath + "\\decrypter\\mp4trackdump.exe";
    public string cueToolsRipper = SupportLibraries.appPath + "\\cuetools\\CUETools.ConsoleRipper.exe";
    public string tempPath = Path.GetTempPath();
  }
}
