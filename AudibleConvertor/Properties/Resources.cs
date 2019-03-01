// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.Properties.Resources
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AudibleConvertor.Properties
{
  [CompilerGenerated]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) AudibleConvertor.Properties.Resources.resourceMan, (object) null))
          AudibleConvertor.Properties.Resources.resourceMan = new ResourceManager("AudibleConvertor.Properties.Resources", typeof (AudibleConvertor.Properties.Resources).Assembly);
        return AudibleConvertor.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return AudibleConvertor.Properties.Resources.resourceCulture;
      }
      set
      {
        AudibleConvertor.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Icon AudioIcon
    {
      get
      {
        return (Icon) AudibleConvertor.Properties.Resources.ResourceManager.GetObject(nameof (AudioIcon), AudibleConvertor.Properties.Resources.resourceCulture);
      }
    }
  }
}
