// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.AudibleGraph
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using DirectShowLib;
using System;
using System.Runtime.InteropServices;

namespace AudibleConvertor
{
  internal class AudibleGraph
  {
    private static void checkHR(int hr, string msg)
    {
      if (hr >= 0)
        return;
      Console.WriteLine(msg);
      DsError.ThrowExceptionForHR(hr);
    }

    private static void BuildGraph(IGraphBuilder pGraph, string srcFile1, string dstFile)
    {
      AudibleGraph.checkHR(((ICaptureGraphBuilder2) new CaptureGraphBuilder2()).SetFiltergraph(pGraph), "Can't set SetFiltergraph");
      Guid clsid1 = new Guid("D05F33E0-3F75-11D3-A176-006008944486");
      Guid clsid2 = new Guid("3C78B8E2-6C4D-11D1-ADE2-0000F8754B99");
      IBaseFilter instance1 = (IBaseFilter) Activator.CreateInstance(Type.GetTypeFromCLSID(clsid1));
      AudibleGraph.checkHR(pGraph.AddFilter(instance1, "Audible Words Codec"), "Can't add audible filter");
      IBaseFilter instance2 = (IBaseFilter) Activator.CreateInstance(Type.GetTypeFromCLSID(clsid2));
      AudibleGraph.checkHR(pGraph.AddFilter(instance2, "Audible Words Codec"), "Can't add WAV Dest filter");
      IBaseFilter baseFilter1 = (IBaseFilter) new AsyncReader();
      AudibleGraph.checkHR(pGraph.AddFilter(baseFilter1, "File Source (Async.)"), "Can't find file source filter");
      IFileSourceFilter fileSourceFilter = baseFilter1 as IFileSourceFilter;
      if (fileSourceFilter == null)
        AudibleGraph.checkHR(-2147467262, "Can't get source filter");
      AudibleGraph.checkHR(fileSourceFilter.Load(srcFile1, (AMMediaType) null), "Can't load file");
      IBaseFilter baseFilter2 = (IBaseFilter) new FileWriter();
      AudibleGraph.checkHR(pGraph.AddFilter(baseFilter2, "File Writer"), "Can't add File Writer to graph");
      AudibleGraph.checkHR((baseFilter2 as IFileSinkFilter).SetFileName(dstFile, (AMMediaType) null), "Can't set filename");
      AudibleGraph.checkHR(pGraph.ConnectDirect(AudibleGraph.GetPin(baseFilter1, "Output"), AudibleGraph.GetPin(instance1, "Input"), (AMMediaType) null), "Can't connect source file to audible codec");
      AudibleGraph.checkHR(pGraph.ConnectDirect(AudibleGraph.GetPin(instance1, "Output"), AudibleGraph.GetPin(instance2, "In"), (AMMediaType) null), "Can't connect audible codect to wav");
      AudibleGraph.checkHR(pGraph.ConnectDirect(AudibleGraph.GetPin(instance2, "Out"), AudibleGraph.GetPin(baseFilter2, "in"), (AMMediaType) null), "Can't wav to writer");
    }

    private static IPin GetPin(IBaseFilter filter, string pinname)
    {
      IEnumPins ppEnum;
      DsError.ThrowExceptionForHR(filter.EnumPins(out ppEnum));
      IntPtr pcFetched = Marshal.AllocCoTaskMem(4);
      IPin[] ppPins = new IPin[1];
      while (ppEnum.Next(1, ppPins, pcFetched) == 0)
      {
        PinInfo pInfo;
        ppPins[0].QueryPinInfo(out pInfo);
        bool flag = pInfo.name == pinname;
        DsUtils.FreePinInfo(pInfo);
        if (flag)
          return ppPins[0];
      }
      DsError.ThrowExceptionForHR(-1);
      return (IPin) null;
    }
  }
}
