// Decompiled with JetBrains decompiler
// Type: LibMp3LameException
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;

public class LibMp3LameException : Exception
{
  public LibMp3LameException(string message)
    : base(message)
  {
  }

  public LibMp3LameException(string message, LibMp3LameException innerException)
    : base(message, (Exception) innerException)
  {
  }
}
