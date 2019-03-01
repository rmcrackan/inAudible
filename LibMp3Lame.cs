// Decompiled with JetBrains decompiler
// Type: LibMp3Lame
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Runtime.InteropServices;

public class LibMp3Lame : IDisposable
{
  private IntPtr dllHandle = LibMp3Lame.LoadLibrary("libmp3lame.dll");
  private IntPtr lame_global_flags;

  [DllImport("kernel32.dll")]
  private static extern IntPtr LoadLibrary(string lpFileName);

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern bool FreeLibrary(IntPtr hModule);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr GetModuleHandle(string libname);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern IntPtr get_lame_version();

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern IntPtr lame_init();

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern void lame_close(IntPtr lame_global_flags);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_errorf(IntPtr lame_global_flags, LibMp3Lame.LameInfoCallback func);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_debugf(IntPtr lame_global_flags, LibMp3Lame.LameInfoCallback func);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_msgf(IntPtr lame_global_flags, LibMp3Lame.LameInfoCallback func);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_brate(IntPtr lame_global_flags, int brate);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_VBR_q(IntPtr lame_global_flags, int quality);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_VBR(IntPtr lame_global_flags, int mode);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_bWriteVbrTag(IntPtr lame_global_flags, int mode);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_get_VBR_q(IntPtr lame_global_flags, int quality);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_get_VBR(IntPtr lame_global_flags, int mode);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_get_bWriteVbrTag(IntPtr lame_global_flags, int mode);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_mode(IntPtr lame_global_flags, LibMp3Lame.MPEG_mode mode);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_in_samplerate(IntPtr lame_global_flags, int rateInHz);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_out_samplerate(IntPtr lame_global_flags, int rateInHz);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_num_channels(IntPtr lame_global_flags, int channels);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_quality(IntPtr lame_global_flags, int quality);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_init_params(IntPtr lame_global_flags);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_set_scale(IntPtr lame_global_flags, float setting);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_encode_buffer(IntPtr lame_global_flags, short[] buffer_l, short[] buffer_r, int nsamples, IntPtr mp3buf, int mp3buf_size);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_encode_flush(IntPtr lame_global_flags, IntPtr mp3buf, int mp3buf_size);

  [DllImport("libmp3lame.dll", CallingConvention = CallingConvention.Cdecl)]
  private static extern int lame_get_lametag_frame(IntPtr lame_global_flags, IntPtr buffer, int size);

  public static string GetLameVersion()
  {
    return Marshal.PtrToStringAnsi(LibMp3Lame.get_lame_version());
  }

  public void LameInit()
  {
    IntPtr num = LibMp3Lame.lame_init();
    if (num == new IntPtr())
      throw new LibMp3LameException("Unable to create the lame struct possibly due to no memory being left");
    this.lame_global_flags = num;
  }

  public void LameSetErrorFunction(LibMp3Lame.LameInfoCallback callback)
  {
    if (LibMp3Lame.lame_set_errorf(this.lame_global_flags, callback) < 0)
      throw new LibMp3LameException("lame_set_errorf returned an error");
  }

  public void LameSetDebugFunction(LibMp3Lame.LameInfoCallback callback)
  {
    if (LibMp3Lame.lame_set_debugf(this.lame_global_flags, callback) < 0)
      throw new LibMp3LameException("lame_set_errorf returned an error");
  }

  public void LameSetMessageFunction(LibMp3Lame.LameInfoCallback callback)
  {
    if (LibMp3Lame.lame_set_msgf(this.lame_global_flags, callback) < 0)
      throw new LibMp3LameException("lame_set_errorf returned an error");
  }

  public void LameSetBRate(int brate)
  {
    if (LibMp3Lame.lame_set_brate(this.lame_global_flags, brate) != 0)
      throw new LibMp3LameException("lame_set_brate returned an error");
  }

  public void LameSetVBRQuality(int brate)
  {
    if (LibMp3Lame.lame_set_VBR_q(this.lame_global_flags, brate) != 0)
      throw new LibMp3LameException("lame_set_VBR_quality returned an error");
  }

  public void LameSetVBR(int mode)
  {
    if (LibMp3Lame.lame_set_VBR(this.lame_global_flags, mode) != 0)
      throw new LibMp3LameException("lame_set_VBR returned an error");
  }

  public void LameSetVBRtag(int mode)
  {
    if (LibMp3Lame.lame_set_bWriteVbrTag(this.lame_global_flags, mode) != 0)
      throw new LibMp3LameException("lame_set_VBR returned an error");
  }

  public void LameSetMode(LibMp3Lame.MPEG_mode mode)
  {
    if (LibMp3Lame.lame_set_mode(this.lame_global_flags, mode) != 0)
      throw new LibMp3LameException("lame_set_mode returned an error");
  }

  public void LameSetInSampleRate(int rateInHz)
  {
    if (LibMp3Lame.lame_set_in_samplerate(this.lame_global_flags, rateInHz) != 0)
      throw new LibMp3LameException("lame_set_in_samplerate returned an error");
  }

  public void LameSetOutSampleRate(int rateInHz)
  {
    if (LibMp3Lame.lame_set_out_samplerate(this.lame_global_flags, rateInHz) != 0)
      throw new LibMp3LameException("lame_set_in_samplerate returned an error");
  }

  public void LameSetNumChannels(int numChannels)
  {
    if (LibMp3Lame.lame_set_num_channels(this.lame_global_flags, numChannels) != 0)
      throw new LibMp3LameException("lame_set_num_channels returned an error");
  }

  public void LameSetQuality(int quality)
  {
    if (LibMp3Lame.lame_set_quality(this.lame_global_flags, quality) != 0)
      throw new LibMp3LameException("lame_set_quality returned an error");
  }

  public void LameSetScale(float quality)
  {
    if (LibMp3Lame.lame_set_scale(this.lame_global_flags, quality) != 0)
      throw new LibMp3LameException("lame_set_scale returned an error");
  }

  public void LameInitParams()
  {
    if (LibMp3Lame.lame_init_params(this.lame_global_flags) < 0)
      throw new LibMp3LameException("lame_init_params returned an error");
  }

  public int LameEncodeBuffer(short[] bufferL, short[] bufferR, int nsamples, byte[] mp3Buffer)
  {
    GCHandle gcHandle = GCHandle.Alloc((object) mp3Buffer, GCHandleType.Pinned);
    IntPtr mp3buf = gcHandle.AddrOfPinnedObject();
    int num = LibMp3Lame.lame_encode_buffer(this.lame_global_flags, bufferL, bufferR, nsamples, mp3buf, mp3Buffer.Length);
    gcHandle.Free();
    if (num < 0)
      throw new LibMp3LameException("lame_encode_buffer returned an error (" + (object) num + ")");
    return num;
  }

  public int LameEncodeFlush(byte[] mp3Buffer)
  {
    GCHandle gcHandle = GCHandle.Alloc((object) mp3Buffer, GCHandleType.Pinned);
    int num = LibMp3Lame.lame_encode_flush(this.lame_global_flags, gcHandle.AddrOfPinnedObject(), mp3Buffer.Length);
    gcHandle.Free();
    if (num < 0)
      throw new LibMp3LameException("lame_encode_flush returned an error (" + (object) num + ")");
    return num;
  }

  public int LameGetLameTagFrame(byte[] buffer)
  {
    GCHandle gcHandle = GCHandle.Alloc((object) buffer, GCHandleType.Pinned);
    int lametagFrame = LibMp3Lame.lame_get_lametag_frame(this.lame_global_flags, gcHandle.AddrOfPinnedObject(), buffer.Length);
    gcHandle.Free();
    if (lametagFrame < 0)
      throw new LibMp3LameException("lame_encode_flush returned an error (" + (object) lametagFrame + ")");
    if (lametagFrame > buffer.Length)
      throw new LibMp3LameException("lame_encode_flush failed due to buffer being to small as it should have been at least " + (object) lametagFrame + " bytes rather than " + (object) buffer.Length);
    return lametagFrame;
  }

  public void LameClose()
  {
    LibMp3Lame.lame_close(this.lame_global_flags);
    this.lame_global_flags = new IntPtr();
  }

  public void Dispose()
  {
    if (!(this.lame_global_flags != new IntPtr()))
      return;
    this.LameClose();
  }

  public enum MPEG_mode
  {
    STEREO,
    JOINT_STEREO,
    DUAL_CHANNEL,
    MONO,
    NOT_SET,
    MAX_INDICATOR,
  }

  public delegate void LameInfoCallback(string format, params object[] args);
}
