// Decompiled with JetBrains decompiler
// Type: Ionic.Utils.FolderBrowserDialogEx
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace Ionic.Utils
{
  public class FolderBrowserDialogEx : CommonDialog
  {
    private static readonly int MAX_PATH = 260;
    private bool _newStyle = true;
    private bool _showFullPathInEditBox = true;
    private PInvoke.BrowseFolderCallbackProc _callback;
    private string _descriptionText;
    private Environment.SpecialFolder _rootFolder;
    private string _selectedPath;
    private bool _selectedPathNeedsCheck;
    private bool _showNewFolderButton;
    private bool _showEditBox;
    private bool _showBothFilesAndFolders;
    private bool _dontIncludeNetworkFoldersBelowDomainLevel;
    private int _uiFlags;
    private IntPtr _hwndEdit;
    private IntPtr _rootFolderLocation;

    public new event EventHandler HelpRequest
    {
      add
      {
        base.HelpRequest += value;
      }
      remove
      {
        base.HelpRequest -= value;
      }
    }

    public FolderBrowserDialogEx()
    {
      this.Reset();
    }

    public static FolderBrowserDialogEx PrinterBrowser()
    {
      FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx();
      folderBrowserDialogEx.BecomePrinterBrowser();
      return folderBrowserDialogEx;
    }

    public static FolderBrowserDialogEx ComputerBrowser()
    {
      FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx();
      folderBrowserDialogEx.BecomeComputerBrowser();
      return folderBrowserDialogEx;
    }

    private void BecomePrinterBrowser()
    {
      this._uiFlags += 8192;
      this.Description = "Select a printer:";
      PInvoke.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, 4, ref this._rootFolderLocation);
      this.ShowNewFolderButton = false;
      this.ShowEditBox = false;
    }

    private void BecomeComputerBrowser()
    {
      this._uiFlags += 4096;
      this.Description = "Select a computer:";
      PInvoke.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, 18, ref this._rootFolderLocation);
      this.ShowNewFolderButton = false;
      this.ShowEditBox = false;
    }

    private int FolderBrowserCallback(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
    {
      switch (msg)
      {
        case 1:
          if (this._selectedPath.Length != 0)
          {
            PInvoke.User32.SendMessage(new HandleRef((object) null, hwnd), 1127, 1, this._selectedPath);
            if (this._showEditBox && this._showFullPathInEditBox)
            {
              this._hwndEdit = PInvoke.User32.FindWindowEx(new HandleRef((object) null, hwnd), IntPtr.Zero, "Edit", (string) null);
              PInvoke.User32.SetWindowText(this._hwndEdit, this._selectedPath);
              break;
            }
            break;
          }
          break;
        case 2:
          IntPtr pidl = lParam;
          if (pidl != IntPtr.Zero)
          {
            if ((this._uiFlags & 8192) == 8192 || (this._uiFlags & 4096) == 4096)
            {
              PInvoke.User32.SendMessage(new HandleRef((object) null, hwnd), 1125, 0, 1);
              break;
            }
            IntPtr num = Marshal.AllocHGlobal(FolderBrowserDialogEx.MAX_PATH * Marshal.SystemDefaultCharSize);
            bool pathFromIdList = PInvoke.Shell32.SHGetPathFromIDList(pidl, num);
            string stringAuto = Marshal.PtrToStringAuto(num);
            Marshal.FreeHGlobal(num);
            PInvoke.User32.SendMessage(new HandleRef((object) null, hwnd), 1125, 0, pathFromIdList ? 1 : 0);
            if (pathFromIdList && !string.IsNullOrEmpty(stringAuto))
            {
              if (this._showEditBox && this._showFullPathInEditBox && this._hwndEdit != IntPtr.Zero)
                PInvoke.User32.SetWindowText(this._hwndEdit, stringAuto);
              if ((this._uiFlags & 4) == 4)
              {
                PInvoke.User32.SendMessage(new HandleRef((object) null, hwnd), 1124, 0, stringAuto);
                break;
              }
              break;
            }
            break;
          }
          break;
      }
      return 0;
    }

    private static PInvoke.IMalloc GetSHMalloc()
    {
      PInvoke.IMalloc[] ppMalloc = new PInvoke.IMalloc[1];
      PInvoke.Shell32.SHGetMalloc(ppMalloc);
      return ppMalloc[0];
    }

    public override void Reset()
    {
      this._rootFolder = Environment.SpecialFolder.Desktop;
      this._descriptionText = string.Empty;
      this._selectedPath = string.Empty;
      this._selectedPathNeedsCheck = false;
      this._showNewFolderButton = true;
      this._showEditBox = true;
      this._newStyle = true;
      this._dontIncludeNetworkFoldersBelowDomainLevel = false;
      this._hwndEdit = IntPtr.Zero;
      this._rootFolderLocation = IntPtr.Zero;
    }

    protected override bool RunDialog(IntPtr hWndOwner)
    {
      bool flag = false;
      if (this._rootFolderLocation == IntPtr.Zero)
      {
        PInvoke.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int) this._rootFolder, ref this._rootFolderLocation);
        if (this._rootFolderLocation == IntPtr.Zero)
        {
          PInvoke.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref this._rootFolderLocation);
          if (this._rootFolderLocation == IntPtr.Zero)
            throw new InvalidOperationException("FolderBrowserDialogNoRootFolder");
        }
      }
      this._hwndEdit = IntPtr.Zero;
      if (this._dontIncludeNetworkFoldersBelowDomainLevel)
        this._uiFlags += 2;
      if (this._newStyle)
        this._uiFlags += 64;
      if (!this._showNewFolderButton)
        this._uiFlags += 512;
      if (this._showEditBox)
        this._uiFlags += 16;
      if (this._showBothFilesAndFolders)
        this._uiFlags += 16384;
      if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != ApartmentState.STA)
        throw new ThreadStateException("DebuggingException: ThreadMustBeSTA");
      IntPtr num1 = IntPtr.Zero;
      IntPtr hglobal = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      try
      {
        PInvoke.BROWSEINFO lpbi = new PInvoke.BROWSEINFO();
        hglobal = Marshal.AllocHGlobal(FolderBrowserDialogEx.MAX_PATH * Marshal.SystemDefaultCharSize);
        num2 = Marshal.AllocHGlobal(FolderBrowserDialogEx.MAX_PATH * Marshal.SystemDefaultCharSize);
        this._callback = new PInvoke.BrowseFolderCallbackProc(this.FolderBrowserCallback);
        lpbi.pidlRoot = this._rootFolderLocation;
        lpbi.Owner = hWndOwner;
        lpbi.pszDisplayName = hglobal;
        lpbi.Title = this._descriptionText;
        lpbi.Flags = this._uiFlags;
        lpbi.callback = this._callback;
        lpbi.lParam = IntPtr.Zero;
        lpbi.iImage = 0;
        num1 = PInvoke.Shell32.SHBrowseForFolder(lpbi);
        if ((this._uiFlags & 8192) == 8192 || (this._uiFlags & 4096) == 4096)
        {
          this._selectedPath = Marshal.PtrToStringAuto(lpbi.pszDisplayName);
          flag = true;
        }
        else if (num1 != IntPtr.Zero)
        {
          PInvoke.Shell32.SHGetPathFromIDList(num1, num2);
          this._selectedPathNeedsCheck = true;
          this._selectedPath = Marshal.PtrToStringAuto(num2);
          flag = true;
        }
      }
      finally
      {
        PInvoke.IMalloc shMalloc = FolderBrowserDialogEx.GetSHMalloc();
        shMalloc.Free(this._rootFolderLocation);
        this._rootFolderLocation = IntPtr.Zero;
        if (num1 != IntPtr.Zero)
          shMalloc.Free(num1);
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
        if (hglobal != IntPtr.Zero)
          Marshal.FreeHGlobal(hglobal);
        this._callback = (PInvoke.BrowseFolderCallbackProc) null;
      }
      return flag;
    }

    public string Description
    {
      get
      {
        return this._descriptionText;
      }
      set
      {
        this._descriptionText = value == null ? string.Empty : value;
      }
    }

    public Environment.SpecialFolder RootFolder
    {
      get
      {
        return this._rootFolder;
      }
      set
      {
        if (!Enum.IsDefined(typeof (Environment.SpecialFolder), (object) value))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (Environment.SpecialFolder));
        this._rootFolder = value;
      }
    }

    public string SelectedPath
    {
      get
      {
        if (this._selectedPath != null && this._selectedPath.Length != 0 && this._selectedPathNeedsCheck)
        {
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._selectedPath).Demand();
          this._selectedPathNeedsCheck = false;
        }
        return this._selectedPath;
      }
      set
      {
        this._selectedPath = value == null ? string.Empty : value;
        this._selectedPathNeedsCheck = true;
      }
    }

    public bool ShowNewFolderButton
    {
      get
      {
        return this._showNewFolderButton;
      }
      set
      {
        this._showNewFolderButton = value;
      }
    }

    public bool ShowEditBox
    {
      get
      {
        return this._showEditBox;
      }
      set
      {
        this._showEditBox = value;
      }
    }

    public bool NewStyle
    {
      get
      {
        return this._newStyle;
      }
      set
      {
        this._newStyle = value;
      }
    }

    public bool DontIncludeNetworkFoldersBelowDomainLevel
    {
      get
      {
        return this._dontIncludeNetworkFoldersBelowDomainLevel;
      }
      set
      {
        this._dontIncludeNetworkFoldersBelowDomainLevel = value;
      }
    }

    public bool ShowFullPathInEditBox
    {
      get
      {
        return this._showFullPathInEditBox;
      }
      set
      {
        this._showFullPathInEditBox = value;
      }
    }

    public bool ShowBothFilesAndFolders
    {
      get
      {
        return this._showBothFilesAndFolders;
      }
      set
      {
        this._showBothFilesAndFolders = value;
      }
    }

    private class CSIDL
    {
      public const int PRINTERS = 4;
      public const int NETWORK = 18;
    }

    private class BrowseFlags
    {
      public const int BIF_DEFAULT = 0;
      public const int BIF_BROWSEFORCOMPUTER = 4096;
      public const int BIF_BROWSEFORPRINTER = 8192;
      public const int BIF_BROWSEINCLUDEFILES = 16384;
      public const int BIF_BROWSEINCLUDEURLS = 128;
      public const int BIF_DONTGOBELOWDOMAIN = 2;
      public const int BIF_EDITBOX = 16;
      public const int BIF_NEWDIALOGSTYLE = 64;
      public const int BIF_NONEWFOLDERBUTTON = 512;
      public const int BIF_RETURNFSANCESTORS = 8;
      public const int BIF_RETURNONLYFSDIRS = 1;
      public const int BIF_SHAREABLE = 32768;
      public const int BIF_STATUSTEXT = 4;
      public const int BIF_UAHINT = 256;
      public const int BIF_VALIDATE = 32;
      public const int BIF_NOTRANSLATETARGETS = 1024;
    }

    private static class BrowseForFolderMessages
    {
      public const int BFFM_INITIALIZED = 1;
      public const int BFFM_SELCHANGED = 2;
      public const int BFFM_VALIDATEFAILEDA = 3;
      public const int BFFM_VALIDATEFAILEDW = 4;
      public const int BFFM_IUNKNOWN = 5;
      public const int BFFM_SETSTATUSTEXT = 1124;
      public const int BFFM_ENABLEOK = 1125;
      public const int BFFM_SETSELECTIONA = 1126;
      public const int BFFM_SETSELECTIONW = 1127;
    }
  }
}
