// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.CLog
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace AudibleConvertor
{
  public class CLog
  {
    private static bool m_bPathSetup = false;
    private static int m_lLogLevel = 1;
    private static long m_lMaxKBLogSize = 1024;
    private static string m_szRootLogFolder = "Logs\\";
    private static ArrayList m_arrLogQueue = new ArrayList();
    private static int m_iPauseLogging = 0;
    private static ArrayList m_arrFileHandles = new ArrayList();
    private static object m_Locker = new object();
    private static bool m_bLogsInUse = false;
    private static bool m_bConsoleWrite = false;
    private static bool m_bTimezone = false;
    private static bool m_bDaylightSavings = false;
    private static bool m_LogFailure = false;
    private static string m_strFilenameFormat = "";
    private static string m_strApplicationName = "";
    private static bool m_bDebugSpeed = false;
    private const int EVENT_MESSAGE = 0;
    private const int EVENT_SHUTDOWN = 1;
    private const string LOG_EXTENSION = ".log";
    private const string LOG_DEBUG_NAME = "Debug_";
    private const string LOG_WARN_NAME = "Warning_";
    private const string LOG_ERR_NAME = "Error_";
    private const string LOG_DEFAULT_DST_FORMAT = "z";
    private const string LOG_DEFAULT_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss:ffff";
    private static AutoResetEvent[] m_autoLogEvents;

    public static void StopLogger()
    {
      if (CLog.m_autoLogEvents == null || CLog.m_autoLogEvents[1] == null)
        return;
      CLog.m_autoLogEvents[1].Set();
    }

    public static string ApplicationName
    {
      get
      {
        return CLog.m_strApplicationName;
      }
      set
      {
        CLog.m_strApplicationName = value;
      }
    }

    public static bool ConsoleWrite
    {
      get
      {
        return CLog.m_bConsoleWrite;
      }
      set
      {
        CLog.m_bConsoleWrite = value;
      }
    }

    public static bool ShowTimeZone
    {
      get
      {
        return CLog.m_bTimezone;
      }
      set
      {
        CLog.m_bTimezone = value;
      }
    }

    public static bool ShowDaylightSavings
    {
      get
      {
        return CLog.m_bDaylightSavings;
      }
      set
      {
        CLog.m_bDaylightSavings = value;
      }
    }

    public static long MaxLogKBSize
    {
      get
      {
        return CLog.m_lMaxKBLogSize;
      }
      set
      {
        CLog.m_lMaxKBLogSize = value;
        if (CLog.m_lMaxKBLogSize >= 1024L || CLog.m_lMaxKBLogSize == 0L)
          return;
        CLog.m_lMaxKBLogSize = 1024L;
      }
    }

    public static bool PauseLogger
    {
      get
      {
        return CLog.m_iPauseLogging > 0;
      }
      set
      {
        if (value)
          ++CLog.m_iPauseLogging;
        else
          --CLog.m_iPauseLogging;
      }
    }

    public static LOG_TYPE LogLevel
    {
      get
      {
        return (LOG_TYPE) CLog.m_lLogLevel;
      }
      set
      {
        CLog.m_lLogLevel = (int) value;
      }
    }

    public static string LogFolder
    {
      get
      {
        return CLog.m_szRootLogFolder;
      }
      set
      {
        CLog.m_szRootLogFolder = value;
        if (CLog.m_szRootLogFolder == null)
          CLog.m_szRootLogFolder = "";
        if (CLog.m_szRootLogFolder.EndsWith("\\") || string.IsNullOrEmpty(CLog.m_szRootLogFolder))
          return;
        CLog.m_szRootLogFolder += "\\";
      }
    }

    public static string TRACE(string sVar, params object[] args)
    {
      sVar = string.Format(sVar, args);
      CLog.TRACE(sVar);
      return sVar;
    }

    public static string TRACE(string sVar)
    {
      if (CLog.m_bConsoleWrite)
        Console.WriteLine(sVar);
      CLog.WriteLine(LOG_TYPE.LOG_DEBUG, sVar);
      return sVar;
    }

    public static bool WriteLine(Exception ex, string szBuffer, params object[] args)
    {
      szBuffer = string.Format(szBuffer, args);
      szBuffer = szBuffer + " - TraceStack: " + ex.StackTrace + ", Source: " + ex.Source + ", Message: " + ex.Message;
      return CLog.WriteData(LOG_TYPE.LOG_ERR, szBuffer, true, true, "");
    }

    public static bool WriteLine(Exception ex, string szBuffer)
    {
      szBuffer = szBuffer + " - TraceStack: " + ex.StackTrace + ", Source: " + ex.Source + ", Message: " + ex.Message;
      return CLog.WriteData(LOG_TYPE.LOG_ERR, szBuffer, true, true, "");
    }

    public static bool WriteLine(Exception ex, string szBuffer, bool bAddDate)
    {
      szBuffer = szBuffer + " - TraceStack: " + ex.StackTrace + ", Source: " + ex.Source + ", Message: " + ex.Message;
      return CLog.WriteData(LOG_TYPE.LOG_ERR, szBuffer, bAddDate, true, "");
    }

    public static bool WriteLine(LOG_TYPE enumFileType, string szBuffer, params object[] args)
    {
      if ((LOG_TYPE) CLog.m_lLogLevel < enumFileType)
        return true;
      szBuffer = string.Format(szBuffer, args);
      return CLog.WriteData(enumFileType, szBuffer, true, true, "");
    }

    public static bool WriteLine(LOG_TYPE enumFileType, string szBuffer)
    {
      if ((LOG_TYPE) CLog.m_lLogLevel < enumFileType)
        return true;
      return CLog.WriteData(enumFileType, szBuffer, true, true, "");
    }

    public static bool WriteLine(LOG_TYPE enumFileType, string szBuffer, bool bAddDate)
    {
      if ((LOG_TYPE) CLog.m_lLogLevel < enumFileType)
        return true;
      return CLog.WriteData(enumFileType, szBuffer, bAddDate, true, "");
    }

    public static void LogReset(bool bDeleteHistorical)
    {
      string str1 = "CLog::LogReset";
      if (!CLog.SetupPath())
      {
        CLog.StopLogger();
      }
      else
      {
        CLog.SetFileFormat();
        CLog.PauseLogger = true;
        while (CLog.m_bLogsInUse)
          Thread.Sleep(100);
        CLog.FileHandles fileHandles = new CLog.FileHandles();
        string str2 = DateTime.Now.ToString(CLog.m_strFilenameFormat);
        for (int index = 0; index < CLog.m_arrFileHandles.Count; ++index)
        {
          fileHandles = (CLog.FileHandles) CLog.m_arrFileHandles[index];
          fileHandles.twHandle.Close();
          Thread.Sleep(0);
          try
          {
            File.Delete(fileHandles.szFileName);
          }
          catch (Exception ex)
          {
            CLog.WriteLine(ex, "{0}: Error while deleting file: {1}", (object) str1, (object) fileHandles.szFileName);
          }
        }
        Thread.Sleep(0);
        if (bDeleteHistorical)
        {
          foreach (string file in Directory.GetFiles(CLog.m_szRootLogFolder, CLog.m_strApplicationName + "_Error_*.log"))
          {
            try
            {
              File.Delete(file);
            }
            catch (Exception ex)
            {
              CLog.WriteLine(ex, "{0}: Error while deleting file: {1}", (object) str1, (object) fileHandles.szFileName);
            }
          }
          Thread.Sleep(0);
          foreach (string file in Directory.GetFiles(CLog.m_szRootLogFolder, CLog.m_strApplicationName + "_Warning_*.log"))
          {
            try
            {
              File.Delete(file);
            }
            catch (Exception ex)
            {
              CLog.WriteLine(ex, "{0}: Error while deleting file: {1}", (object) str1, (object) fileHandles.szFileName);
            }
          }
          Thread.Sleep(0);
          foreach (string file in Directory.GetFiles(CLog.m_szRootLogFolder, CLog.m_strApplicationName + "_Debug_*.log"))
          {
            try
            {
              File.Delete(file);
            }
            catch (Exception ex)
            {
              CLog.WriteLine(ex, "{0}: Error while deleting file: {1}", (object) str1, (object) fileHandles.szFileName);
            }
          }
        }
        for (int index = 0; index < CLog.m_arrFileHandles.Count; ++index)
        {
          CLog.FileHandles arrFileHandle = (CLog.FileHandles) CLog.m_arrFileHandles[index];
          for (arrFileHandle.szFileName = CLog.m_szRootLogFolder + arrFileHandle.szRootFileName + str2 + ".log"; File.Exists(arrFileHandle.szFileName); arrFileHandle.szFileName = CLog.m_szRootLogFolder + arrFileHandle.szRootFileName + str2 + ".log")
          {
            Thread.Sleep(100);
            str2 = DateTime.Now.ToString(CLog.m_strFilenameFormat);
          }
          arrFileHandle.twHandle = new FileStream(arrFileHandle.szFileName, FileMode.OpenOrCreate);
          arrFileHandle.dwFileSize = (Decimal) arrFileHandle.twHandle.Length;
          CLog.m_arrFileHandles[index] = (object) arrFileHandle;
        }
        CLog.PauseLogger = false;
      }
    }

    public static void StartLogger()
    {
      CLog.FileHandles fh = new CLog.FileHandles();
      if (!CLog.SetupPath())
        return;
      CLog.SetFileFormat();
      string str1 = DateTime.Now.ToString(CLog.m_strFilenameFormat);
      fh.szRootFileName = CLog.m_strApplicationName + "_Error_";
      for (fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str1 + ".log"; File.Exists(fh.szFileName); fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str1 + ".log")
      {
        Thread.Sleep(100);
        str1 = DateTime.Now.ToString(CLog.m_strFilenameFormat);
      }
      fh.twHandle = new FileStream(fh.szFileName, FileMode.OpenOrCreate);
      fh.dwFileSize = (Decimal) fh.twHandle.Length;
      CLog.m_arrFileHandles.Add((object) fh);
      fh.szRootFileName = CLog.m_strApplicationName + "_Warning_";
      for (fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str1 + ".log"; File.Exists(fh.szFileName); fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str1 + ".log")
      {
        Thread.Sleep(100);
        str1 = DateTime.Now.ToString(CLog.m_strFilenameFormat);
      }
      fh.twHandle = new FileStream(fh.szFileName, FileMode.OpenOrCreate);
      fh.dwFileSize = (Decimal) fh.twHandle.Length;
      CLog.m_arrFileHandles.Add((object) fh);
      fh.szRootFileName = CLog.m_strApplicationName + "_Debug_";
      string str2;
      for (fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str1 + ".log"; File.Exists(fh.szFileName); fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str2 + ".log")
      {
        Thread.Sleep(100);
        str2 = DateTime.Now.ToString(CLog.m_strFilenameFormat);
      }
      fh.twHandle = new FileStream(fh.szFileName, FileMode.OpenOrCreate);
      fh.dwFileSize = (Decimal) fh.twHandle.Length;
      CLog.m_arrFileHandles.Add((object) fh);
      CLog.m_autoLogEvents = new AutoResetEvent[2]
      {
        new AutoResetEvent(true),
        new AutoResetEvent(false)
      };
      CLog.WriteFull("Logging thread has started", LOG_TYPE.LOG_DEBUG, DateTime.Now.ToString(CLog.GetDateFormat()), true, "");
      int num1 = 0;
      while (num1 == 0)
      {
        num1 = WaitHandle.WaitAny((WaitHandle[]) CLog.m_autoLogEvents);
        CLog.m_bLogsInUse = true;
        while (CLog.m_arrLogQueue.Count > 0 && CLog.m_iPauseLogging == 0 && !CLog.m_LogFailure)
        {
          ArrayList arrayList = new ArrayList();
          lock (CLog.m_Locker)
          {
            arrayList = CLog.m_arrLogQueue;
            CLog.m_arrLogQueue = new ArrayList();
          }
          string str3 = !CLog.m_bDebugSpeed ? "" : "(In Queue:" + arrayList.Count.ToString() + ") ";
          int num2 = 0;
          foreach (CLog.TransLog transLog in arrayList)
          {
            ++num2;
            if (transLog.AddedDateTime != null)
              CLog.WriteFull(str3 + transLog.szBuffer, transLog.enumFileType, transLog.AddedDateTime, transLog.bAddReturn, transLog.szFileName);
            if (num2 > 100)
            {
              num2 = 0;
              Thread.Sleep(0);
            }
            if (CLog.m_LogFailure)
            {
              CLog.StopLogger();
              break;
            }
          }
        }
        for (int index = 0; index < CLog.m_arrFileHandles.Count; ++index)
        {
          fh = (CLog.FileHandles) CLog.m_arrFileHandles[index];
          try
          {
            fh.twHandle.Flush();
          }
          catch
          {
          }
        }
        CLog.m_bLogsInUse = false;
      }
      CLog.WriteFull("Logging thead has closed", LOG_TYPE.LOG_DEBUG, DateTime.Now.ToString(CLog.GetDateFormat()), true, "");
      for (int index = 0; index < CLog.m_arrFileHandles.Count; ++index)
      {
        fh = (CLog.FileHandles) CLog.m_arrFileHandles[index];
        CLog.CloseFile(fh, fh.twHandle.Length);
      }
    }

    public static string GetLogFileName()
    {
      string str = "";
      foreach (CLog.FileHandles arrFileHandle in CLog.m_arrFileHandles)
      {
        if (arrFileHandle.szFileName.ToLower().Contains("debug"))
          str = arrFileHandle.szFileName;
      }
      return str;
    }

    public static bool WriteData(LOG_TYPE enumFileType, string szBuffer, bool bAddDate, bool bAddReturn, string szFileName)
    {
      if ((LOG_TYPE) CLog.m_lLogLevel < enumFileType)
        return true;
      if (CLog.m_LogFailure)
        return false;
      if (CLog.m_bConsoleWrite)
        Console.WriteLine(szBuffer);
      CLog.TransLog transLog = new CLog.TransLog();
      if (bAddDate)
      {
        string dateFormat = CLog.GetDateFormat();
        transLog.AddedDateTime = DateTime.Now.ToString(dateFormat);
      }
      transLog.bAddReturn = bAddReturn;
      transLog.enumFileType = enumFileType;
      transLog.szBuffer = szBuffer;
      transLog.szFileName = szFileName;
      lock (CLog.m_Locker)
        CLog.m_arrLogQueue.Add((object) transLog);
      if (CLog.m_autoLogEvents != null)
        CLog.m_autoLogEvents[0].Set();
      return true;
    }

    private static void SetFileFormat()
    {
      if (!string.IsNullOrEmpty(CLog.m_strFilenameFormat))
        return;
      CLog.m_strFilenameFormat = "yyyy-MM-dd HH:mm:ss:ffff".Replace(" ", "_");
      CLog.m_strFilenameFormat = CLog.m_strFilenameFormat.Replace(":", "");
      CLog.m_strFilenameFormat = CLog.m_strFilenameFormat.Replace("-", "");
    }

    private static string GetDateFormat()
    {
      string str = "yyyy-MM-dd HH:mm:ss:ffff";
      if (CLog.m_bTimezone)
        str = !CLog.m_bDaylightSavings ? "[z] yyyy-MM-dd HH:mm:ss:ffff" : (!DateTime.Now.IsDaylightSavingTime() ? "[z,0] yyyy-MM-dd HH:mm:ss:ffff" : "[z,1] yyyy-MM-dd HH:mm:ss:ffff");
      return str;
    }

    private static void CloseFile(CLog.FileHandles fh, long lFileSize)
    {
      string str = "CLog::CloseFile";
      fh.twHandle.Close();
      if (lFileSize != 0L)
        return;
      Thread.Sleep(0);
      try
      {
        File.Delete(fh.szFileName);
      }
      catch (Exception ex)
      {
        CLog.WriteLine(ex, "{0}: Error while deleting file: {1}", (object) str, (object) fh.szFileName);
      }
    }

    private static bool WriteFull(string szBuffer, LOG_TYPE enumFileType, string szAddDate, bool bAddReturn, string szFileName)
    {
      if ((LOG_TYPE) CLog.m_lLogLevel < enumFileType)
        return true;
      if (CLog.m_LogFailure)
        return false;
      try
      {
        if (szAddDate != null && !string.IsNullOrEmpty(szAddDate))
          szBuffer = szAddDate + ": " + szBuffer;
        if (bAddReturn)
          szBuffer += "\r\n";
        if (!CLog.SetupPath())
          CLog.StopLogger();
        CLog.FileHandles arrFileHandle = (CLog.FileHandles) CLog.m_arrFileHandles[(int) enumFileType];
        CLog.CheckMaxSize(ref arrFileHandle);
        FileStream twHandle = arrFileHandle.twHandle;
        ASCIIEncoding asciiEncoding = new ASCIIEncoding();
        if (CLog.m_bDebugSpeed)
          szBuffer = "[WriteTime:" + DateTime.Now.ToString("HH:mm:ss:ffff") + "] " + szBuffer;
        arrFileHandle.dwFileSize = arrFileHandle.dwFileSize + (Decimal) szBuffer.Length;
        CLog.m_arrFileHandles[(int) enumFileType] = (object) arrFileHandle;
        byte[] bytes = asciiEncoding.GetBytes(szBuffer);
        twHandle.Write(bytes, 0, bytes.Length);
        return true;
      }
      catch
      {
        CLog.m_LogFailure = true;
        return false;
      }
    }

    private static void CheckMaxSize(ref CLog.FileHandles fh)
    {
      if (CLog.m_LogFailure || CLog.m_lMaxKBLogSize <= 0L || !(fh.dwFileSize > (Decimal) (CLog.m_lMaxKBLogSize * 1024L)))
        return;
      fh.twHandle.Close();
      string str = DateTime.Now.ToString(CLog.m_strFilenameFormat);
      fh.szFileName = CLog.m_szRootLogFolder + fh.szRootFileName + str + ".log";
      try
      {
        fh.twHandle = new FileStream(fh.szFileName, FileMode.OpenOrCreate);
        fh.dwFileSize = (Decimal) fh.twHandle.Length;
      }
      catch (Exception ex)
      {
        CEventlog.Write(EventLogEntryType.Error, "Chizl.AsyncLogger", "Error '" + ex.Message + "' in application '" + Assembly.GetExecutingAssembly().Location.ToString() + "' while opening '" + fh.szFileName + "'", 101);
        CLog.m_LogFailure = true;
      }
    }

    private static bool SetupPath()
    {
      if (CLog.m_bPathSetup)
        return true;
      CLog.m_bPathSetup = true;
      if (!CLog.m_szRootLogFolder.EndsWith("\\"))
        CLog.m_szRootLogFolder += "\\";
      try
      {
        if (!Directory.Exists(CLog.m_szRootLogFolder))
          Directory.CreateDirectory(CLog.m_szRootLogFolder);
      }
      catch
      {
        return false;
      }
      return true;
    }

    private enum EVENT_ID
    {
      ID_CHECKMAXSIZE = 100,
      ID_CREATEFILEHANDLE = 101,
    }

    private struct FileHandles
    {
      public FileStream twHandle;
      public string szFileName;
      public Decimal dwFileSize;
      public string szRootFileName;
    }

    private struct TransLog
    {
      public string szBuffer;
      public LOG_TYPE enumFileType;
      public string AddedDateTime;
      public bool bAddReturn;
      public string szFileName;
    }
  }
}
