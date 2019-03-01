// Decompiled with JetBrains decompiler
// Type: CustomControls.Controls.OpenFileDialogEx
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using CustomControls.OS;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CustomControls.Controls
{
    public class OpenFileDialogEx : UserControl
    {
        private AddonWindowLocation mStartLocation = AddonWindowLocation.Right;
        private FolderViewMode mDefaultViewMode = FolderViewMode.Default;
        private const SetWindowPosFlags UFLAGSHIDE = SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_HIDEWINDOW | SetWindowPosFlags.SWP_NOOWNERZORDER;
        private IContainer components;
        protected OpenFileDialog dlgOpen;

        public event OpenFileDialogEx.PathChangedHandler FileNameChanged;

        public event OpenFileDialogEx.PathChangedHandler FolderNameChanged;

        public event EventHandler ClosingDialog;

        public OpenFileDialogEx()
        {
            this.InitializeComponent();
        }

        public OpenFileDialog OpenDialog
        {
            get
            {
                return this.dlgOpen;
            }
        }

        [DefaultValue(AddonWindowLocation.Right)]
        public AddonWindowLocation StartLocation
        {
            get
            {
                return this.mStartLocation;
            }
            set
            {
                this.mStartLocation = value;
            }
        }

        [DefaultValue(FolderViewMode.Default)]
        public FolderViewMode DefaultViewMode
        {
            get
            {
                return this.mDefaultViewMode;
            }
            set
            {
                this.mDefaultViewMode = value;
            }
        }

        public virtual void OnFileNameChanged(string fileName)
        {
            if (this.FileNameChanged == null)
                return;
            this.FileNameChanged(this, fileName);
        }

        public virtual void OnFolderNameChanged(string folderName)
        {
            if (this.FolderNameChanged == null)
                return;
            this.FolderNameChanged(this, folderName);
        }

        public virtual void OnClosingDialog()
        {
            if (this.ClosingDialog == null)
                return;
            this.ClosingDialog((object)this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            return this.ShowDialog((IWin32Window)null);
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            DialogResult dialogResult = DialogResult.Cancel;
            OpenFileDialogEx.DummyForm dummyForm = new OpenFileDialogEx.DummyForm(this);
            dummyForm.Show(owner);
            Win32.SetWindowPos(dummyForm.Handle, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_HIDEWINDOW | SetWindowPosFlags.SWP_NOOWNERZORDER);
            dummyForm.WatchForActivate = true;
            try
            {
                dialogResult = this.dlgOpen.ShowDialog((IWin32Window)dummyForm);
            }
            catch (Exception ex)
            {
            }
            dummyForm.Close();
            return dialogResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dlgOpen = new OpenFileDialog();
            this.dlgOpen.AutoUpgradeEnabled = false;
            this.SuspendLayout();
            this.Name = nameof(OpenFileDialogEx);
            this.Size = new Size((int)byte.MaxValue, 246);
            this.ResumeLayout(false);
        }

        public delegate void PathChangedHandler(OpenFileDialogEx sender, string filePath);

        private class OpenDialogNative : NativeWindow, IDisposable
        {
            private RECT mOpenDialogWindowRect = new RECT();
            private RECT mOpenDialogClientRect = new RECT();
            private const SetWindowPosFlags UFLAGSSIZE = SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER;
            private const SetWindowPosFlags UFLAGSSIZEEX = SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_DEFERERASE | SetWindowPosFlags.SWP_ASYNCWINDOWPOS;
            private const SetWindowPosFlags UFLAGSMOVE = SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER;
            private const SetWindowPosFlags UFLAGSHIDE = SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_HIDEWINDOW | SetWindowPosFlags.SWP_NOOWNERZORDER;
            private const SetWindowPosFlags UFLAGSZORDER = SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE;
            private Size mOriginalSize;
            private IntPtr mOpenDialogHandle;
            private IntPtr mListViewPtr;
            private WINDOWINFO mListViewInfo;
            private OpenFileDialogEx.BaseDialogNative mBaseDialogNative;
            private IntPtr mComboFolders;
            private WINDOWINFO mComboFoldersInfo;
            private IntPtr mGroupButtons;
            private WINDOWINFO mGroupButtonsInfo;
            private IntPtr mComboFileName;
            private WINDOWINFO mComboFileNameInfo;
            private IntPtr mComboExtensions;
            private WINDOWINFO mComboExtensionsInfo;
            private IntPtr mOpenButton;
            private WINDOWINFO mOpenButtonInfo;
            private IntPtr mCancelButton;
            private WINDOWINFO mCancelButtonInfo;
            private IntPtr mHelpButton;
            private WINDOWINFO mHelpButtonInfo;
            private OpenFileDialogEx mSourceControl;
            private IntPtr mToolBarFolders;
            private WINDOWINFO mToolBarFoldersInfo;
            private IntPtr mLabelFileName;
            private WINDOWINFO mLabelFileNameInfo;
            private IntPtr mLabelFileType;
            private WINDOWINFO mLabelFileTypeInfo;
            private IntPtr mChkReadOnly;
            private WINDOWINFO mChkReadOnlyInfo;
            private bool mIsClosing;
            private bool mInitializated;

            public OpenDialogNative(IntPtr handle, OpenFileDialogEx sourceControl)
            {
                this.mOpenDialogHandle = handle;
                this.mSourceControl = sourceControl;
                this.AssignHandle(this.mOpenDialogHandle);
            }

            private void BaseDialogNative_FileNameChanged(OpenFileDialogEx.BaseDialogNative sender, string filePath)
            {
                if (this.mSourceControl == null)
                    return;
                this.mSourceControl.OnFileNameChanged(filePath);
            }

            private void BaseDialogNative_FolderNameChanged(OpenFileDialogEx.BaseDialogNative sender, string folderName)
            {
                if (this.mSourceControl == null)
                    return;
                this.mSourceControl.OnFolderNameChanged(folderName);
            }

            public void Dispose()
            {
                this.ReleaseHandle();
                if (this.mBaseDialogNative == null)
                    return;
                this.mBaseDialogNative.FileNameChanged -= new OpenFileDialogEx.BaseDialogNative.PathChangedHandler(this.BaseDialogNative_FileNameChanged);
                this.mBaseDialogNative.FolderNameChanged -= new OpenFileDialogEx.BaseDialogNative.PathChangedHandler(this.BaseDialogNative_FolderNameChanged);
                this.mBaseDialogNative.Dispose();
            }

            private void PopulateWindowsHandlers()
            {
                Win32.EnumChildWindows(this.mOpenDialogHandle, new Win32.EnumWindowsCallBack(this.OpenFileDialogEnumWindowCallBack), 0);
            }

            private bool OpenFileDialogEnumWindowCallBack(IntPtr hwnd, int lParam)
            {
                StringBuilder stringBuilder = new StringBuilder(256);
                Win32.GetClassName(hwnd, stringBuilder, stringBuilder.Capacity);
                int dlgCtrlId = Win32.GetDlgCtrlID(hwnd);
                WINDOWINFO pwi;
                Win32.GetWindowInfo(hwnd, out pwi);
                if (stringBuilder.ToString().StartsWith("#32770"))
                {
                    this.mBaseDialogNative = new OpenFileDialogEx.BaseDialogNative(hwnd);
                    this.mBaseDialogNative.FileNameChanged += new OpenFileDialogEx.BaseDialogNative.PathChangedHandler(this.BaseDialogNative_FileNameChanged);
                    this.mBaseDialogNative.FolderNameChanged += new OpenFileDialogEx.BaseDialogNative.PathChangedHandler(this.BaseDialogNative_FolderNameChanged);
                    return true;
                }
                switch ((ControlsID)dlgCtrlId)
                {
                    case ControlsID.ComboFileName:
                        if (stringBuilder.ToString().ToLower() == "comboboxex32")
                        {
                            this.mComboFileName = hwnd;
                            this.mComboFileNameInfo = pwi;
                            break;
                        }
                        break;
                    case ControlsID.LeftToolBar:
                        this.mToolBarFolders = hwnd;
                        this.mToolBarFoldersInfo = pwi;
                        break;
                    case ControlsID.DefaultView:
                        this.mListViewPtr = hwnd;
                        Win32.GetWindowInfo(hwnd, out this.mListViewInfo);
                        if (this.mSourceControl.DefaultViewMode != FolderViewMode.Default)
                        {
                            Win32.SendMessage(this.mListViewPtr, 273, (int)this.mSourceControl.DefaultViewMode, 0);
                            break;
                        }
                        break;
                    case ControlsID.ComboFileType:
                        this.mComboExtensions = hwnd;
                        this.mComboExtensionsInfo = pwi;
                        break;
                    case ControlsID.ComboFolder:
                        this.mComboFolders = hwnd;
                        this.mComboFoldersInfo = pwi;
                        break;
                    case ControlsID.ButtonOpen:
                        this.mOpenButton = hwnd;
                        this.mOpenButtonInfo = pwi;
                        break;
                    case ControlsID.ButtonCancel:
                        this.mCancelButton = hwnd;
                        this.mCancelButtonInfo = pwi;
                        break;
                    case ControlsID.ButtonHelp:
                        this.mHelpButton = hwnd;
                        this.mHelpButtonInfo = pwi;
                        break;
                    case ControlsID.CheckBoxReadOnly:
                        this.mChkReadOnly = hwnd;
                        this.mChkReadOnlyInfo = pwi;
                        break;
                    case ControlsID.GroupFolder:
                        this.mGroupButtons = hwnd;
                        this.mGroupButtonsInfo = pwi;
                        break;
                    case ControlsID.LabelFileType:
                        this.mLabelFileType = hwnd;
                        this.mLabelFileTypeInfo = pwi;
                        break;
                    case ControlsID.LabelFileName:
                        this.mLabelFileName = hwnd;
                        this.mLabelFileNameInfo = pwi;
                        break;
                }
                return true;
            }

            private void InitControls()
            {
                this.mInitializated = true;
                Win32.GetClientRect(this.mOpenDialogHandle, ref this.mOpenDialogClientRect);
                Win32.GetWindowRect(this.mOpenDialogHandle, ref this.mOpenDialogWindowRect);
                this.PopulateWindowsHandlers();
                switch (this.mSourceControl.StartLocation)
                {
                    case AddonWindowLocation.None:
                        this.mSourceControl.Location = new Point(0, 0);
                        break;
                    case AddonWindowLocation.Right:
                        this.mSourceControl.Location = new Point((int)((long)this.mOpenDialogClientRect.Width - (long)this.mSourceControl.Width), 0);
                        break;
                    case AddonWindowLocation.Bottom:
                        this.mSourceControl.Location = new Point(0, (int)((long)this.mOpenDialogClientRect.Height - (long)this.mSourceControl.Height));
                        break;
                }
                Win32.SetParent(this.mSourceControl.Handle, this.mOpenDialogHandle);
                Win32.SetWindowPos(this.mSourceControl.Handle, (IntPtr)1L, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE);
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case 532:
                        switch (this.mSourceControl.StartLocation)
                        {
                            case AddonWindowLocation.None:
                                RECT rect1 = new RECT();
                                Win32.GetClientRect(this.mOpenDialogHandle, ref rect1);
                                if ((long)rect1.Width != (long)this.mSourceControl.Width || (long)rect1.Height != (long)this.mSourceControl.Height)
                                {
                                    Win32.SetWindowPos(this.mSourceControl.Handle, (IntPtr)1L, 0, 0, (int)rect1.Width, (int)rect1.Height, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_DEFERERASE | SetWindowPosFlags.SWP_ASYNCWINDOWPOS);
                                    break;
                                }
                                break;
                            case AddonWindowLocation.Right:
                                RECT rect2 = new RECT();
                                Win32.GetClientRect(this.mOpenDialogHandle, ref rect2);
                                if ((long)rect2.Height != (long)this.mSourceControl.Height)
                                {
                                    Win32.SetWindowPos(this.mSourceControl.Handle, (IntPtr)1L, 0, 0, this.mSourceControl.Width, (int)rect2.Height, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_DEFERERASE | SetWindowPosFlags.SWP_ASYNCWINDOWPOS);
                                    break;
                                }
                                break;
                            case AddonWindowLocation.Bottom:
                                RECT rect3 = new RECT();
                                Win32.GetClientRect(this.mOpenDialogHandle, ref rect3);
                                if ((long)rect3.Height != (long)this.mSourceControl.Height)
                                {
                                    Win32.SetWindowPos(this.mSourceControl.Handle, (IntPtr)1L, 0, 0, (int)rect3.Width, this.mSourceControl.Height, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_DEFERERASE | SetWindowPosFlags.SWP_ASYNCWINDOWPOS);
                                    break;
                                }
                                break;
                        }
                        break;
                    case 642:
                        if (m.WParam == (IntPtr)1L)
                        {
                            this.mIsClosing = true;
                            this.mSourceControl.OnClosingDialog();
                            Win32.SetWindowPos(this.mOpenDialogHandle, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_HIDEWINDOW | SetWindowPosFlags.SWP_NOOWNERZORDER);
                            Win32.GetWindowRect(this.mOpenDialogHandle, ref this.mOpenDialogWindowRect);
                            Win32.SetWindowPos(this.mOpenDialogHandle, IntPtr.Zero, (int)this.mOpenDialogWindowRect.left, (int)this.mOpenDialogWindowRect.top, this.mOriginalSize.Width, this.mOriginalSize.Height, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER);
                            break;
                        }
                        break;
                    case 24:
                        this.mInitializated = true;
                        this.InitControls();
                        break;
                    case 70:
                        if (!this.mIsClosing && !this.mInitializated)
                        {
                            WINDOWPOS structure = (WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
                            RECT rect4;
                            if (this.mSourceControl.StartLocation == AddonWindowLocation.Right && (int)structure.flags != 0 && ((int)structure.flags & 1) != 1)
                            {
                                this.mOriginalSize = new Size(structure.cx, structure.cy);
                                structure.cx += this.mSourceControl.Width;
                                Marshal.StructureToPtr((object)structure, m.LParam, true);
                                rect4 = new RECT();
                                Win32.GetClientRect(this.mOpenDialogHandle, ref rect4);
                                this.mSourceControl.Height = (int)rect4.Height;
                            }
                            if (this.mSourceControl.StartLocation == AddonWindowLocation.Bottom && (int)structure.flags != 0 && ((int)structure.flags & 1) != 1)
                            {
                                this.mOriginalSize = new Size(structure.cx, structure.cy);
                                structure.cy += this.mSourceControl.Height;
                                Marshal.StructureToPtr((object)structure, m.LParam, true);
                                rect4 = new RECT();
                                Win32.GetClientRect(this.mOpenDialogHandle, ref rect4);
                                this.mSourceControl.Width = (int)rect4.Width;
                                break;
                            }
                            break;
                        }
                        break;
                }
                base.WndProc(ref m);
            }
        }

        private class BaseDialogNative : NativeWindow, IDisposable
        {
            private IntPtr mHandle;

            public event OpenFileDialogEx.BaseDialogNative.PathChangedHandler FileNameChanged;

            public event OpenFileDialogEx.BaseDialogNative.PathChangedHandler FolderNameChanged;

            public BaseDialogNative(IntPtr handle)
            {
                this.mHandle = handle;
                this.AssignHandle(handle);
            }

            public void Dispose()
            {
                this.ReleaseHandle();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 78)
                {
                    OFNOTIFY structure = (OFNOTIFY)Marshal.PtrToStructure(m.LParam, typeof(OFNOTIFY));
                    if ((int)structure.hdr.code == -602)
                    {
                        StringBuilder stringBuilder = new StringBuilder(256);
                        Win32.SendMessage(Win32.GetParent(this.mHandle), 1125, 256, stringBuilder);
                        if (this.FileNameChanged != null)
                            this.FileNameChanged(this, stringBuilder.ToString());
                    }
                    else if ((int)structure.hdr.code == -603)
                    {
                        StringBuilder stringBuilder = new StringBuilder(256);
                        Win32.SendMessage(Win32.GetParent(this.mHandle), 1126, 256, stringBuilder);
                        if (this.FolderNameChanged != null)
                            this.FolderNameChanged(this, stringBuilder.ToString());
                    }
                }
                base.WndProc(ref m);
            }

            public delegate void PathChangedHandler(OpenFileDialogEx.BaseDialogNative sender, string filePath);
        }

        private class DummyForm : Form
        {
            private IntPtr mOpenDialogHandle = IntPtr.Zero;
            private OpenFileDialogEx.OpenDialogNative mNativeDialog;
            private OpenFileDialogEx mFileDialogEx;
            private bool mWatchForActivate;

            public DummyForm(OpenFileDialogEx fileDialogEx)
            {
                this.mFileDialogEx = fileDialogEx;
                this.Text = "";
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(-32000, -32000);
                this.ShowInTaskbar = false;
            }

            public bool WatchForActivate
            {
                get
                {
                    return this.mWatchForActivate;
                }
                set
                {
                    this.mWatchForActivate = value;
                }
            }

            protected override void OnClosing(CancelEventArgs e)
            {
                if (this.mNativeDialog != null)
                    this.mNativeDialog.Dispose();
                base.OnClosing(e);
            }

            protected override void WndProc(ref Message m)
            {
                if (this.mWatchForActivate && m.Msg == 6)
                {
                    this.mWatchForActivate = false;
                    this.mOpenDialogHandle = m.LParam;
                    this.mNativeDialog = new OpenFileDialogEx.OpenDialogNative(m.LParam, this.mFileDialogEx);
                }
                base.WndProc(ref m);
            }
        }
    }
}
