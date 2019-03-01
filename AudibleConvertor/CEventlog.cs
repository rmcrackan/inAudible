// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.CEventlog
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.Diagnostics;

namespace AudibleConvertor
{
  public class CEventlog
  {
    public static bool Write(string strSource, string strEvent)
    {
      return CEventlog.Write(EventLogEntryType.Information, strSource, strEvent, 0);
    }

    public static bool Write(EventLogEntryType evtType, string strSource, string strEvent)
    {
      return CEventlog.Write(evtType, strSource, strEvent, 0);
    }

    public static bool Write(EventLogEntryType evtType, string strSource, string strEvent, int iEventID)
    {
      string logName = "Application";
      try
      {
        if (!EventLog.SourceExists(strSource))
          EventLog.CreateEventSource(strSource, logName);
        EventLog.WriteEntry(strSource, strEvent, evtType, iEventID);
      }
      catch
      {
        return false;
      }
      return true;
    }
  }
}
