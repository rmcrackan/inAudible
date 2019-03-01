// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.DownloadCache
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AudibleConvertor
{
  public class DownloadCache
  {
    public void Add(Uri uri, string path, WebHeaderCollection headers)
    {
      string destinationFolder;
      string destinationFileName;
      DownloadCache.GetCacheFileName(uri, headers, out destinationFolder, out destinationFileName);
      Directory.CreateDirectory(destinationFolder);
      if (!System.IO.File.Exists(destinationFileName))
        return;
      if (new FileInfo(destinationFileName).Length == 0L)
        return;
      try
      {
        System.IO.File.Copy(path, destinationFileName);
      }
      catch
      {
      }
    }

    public string Get(Uri uri, WebHeaderCollection headers)
    {
      string destinationFolder;
      string destinationFileName;
      DownloadCache.GetCacheFileName(uri, headers, out destinationFolder, out destinationFileName);
      if (System.IO.File.Exists(destinationFileName))
        return destinationFileName;
      return (string) null;
    }

    public void Invalidate(Uri uri)
    {
      string path = Path.Combine(Path.GetTempPath(), DownloadCache.MD5(uri.ToString()));
      if (!Directory.Exists(path))
        return;
      Directory.Delete(path, true);
    }

    private static void GetCacheFileName(Uri uri, WebHeaderCollection headers, out string destinationFolder, out string destinationFileName)
    {
      string path2_1 = DownloadCache.MD5(headers.Get("ETag"));
      string path2_2 = DownloadCache.MD5(uri.ToString());
      destinationFolder = Path.Combine(Path.GetTempPath(), path2_2);
      destinationFileName = Path.Combine(destinationFolder, path2_1);
    }

    private static string MD5(string input)
    {
      using (MD5 md5 = System.Security.Cryptography.MD5.Create())
      {
        byte[] bytes = Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("X2"));
        return stringBuilder.ToString();
      }
    }
  }
}
