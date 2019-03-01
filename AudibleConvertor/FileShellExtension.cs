// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FileShellExtension
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Microsoft.Win32;

namespace AudibleConvertor
{
  internal static class FileShellExtension
  {
    public static void Register(string fileType, string shellKeyName, string menuText, string menuCommand)
    {
      string subkey = string.Format("{0}\\shell\\{1}", (object) fileType, (object) shellKeyName);
      using (RegistryKey subKey = Registry.ClassesRoot.CreateSubKey(subkey))
        subKey.SetValue((string) null, (object) menuText);
      using (RegistryKey subKey = Registry.ClassesRoot.CreateSubKey(string.Format("{0}\\command", (object) subkey)))
        subKey.SetValue((string) null, (object) menuCommand);
    }

    public static void Unregister(string fileType, string shellKeyName)
    {
      string subkey = string.Format("{0}\\shell\\{1}", (object) fileType, (object) shellKeyName);
      Registry.ClassesRoot.DeleteSubKeyTree(subkey);
    }
  }
}
