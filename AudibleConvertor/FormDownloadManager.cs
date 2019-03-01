// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormDownloadManager
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormDownloadManager : Form
  {
    public string downloadPath = "";
    public bool applied;
    private IContainer components;
    private Label label1;
    private Button btnBrowse;
    private Button btnSetAssociation;
    private Button btnApply;
    public TextBox txtDownoadDir;

    public FormDownloadManager()
    {
      this.InitializeComponent();
    }

    private void btnSetAssociation_Click(object sender, EventArgs e)
    {
      this.SetAssociation();
    }

    private void SetAssociation()
    {
      try
      {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts", RegistryKeyPermissionCheck.ReadWriteSubTree);
        if (registryKey != null)
        {
          registryKey.DeleteSubKeyTree(".adh", false);
          registryKey.Close();
        }
        RegistryKey subKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.adh");
        subKey.SetValue("Application", (object) Path.GetFileName(AudibleConvertor.GLOBALS.ExecutablePath));
        subKey.SetValue("Progid", (object) "Audible_Download_Manager");
        subKey.CreateSubKey("edit").CreateSubKey("command").SetValue("", (object) ("\"" + Path.GetFileName(AudibleConvertor.GLOBALS.ExecutablePath) + "\" \"%1\""));
        subKey.CreateSubKey("open").CreateSubKey("command").SetValue("", (object) ("\"" + Path.GetFileName(AudibleConvertor.GLOBALS.ExecutablePath) + "\" \"%1\""));
        subKey.CreateSubKey("DefaultIcon").SetValue("", (object) (Path.GetDirectoryName(AudibleConvertor.GLOBALS.ExecutablePath) + "\\ADH.ICO"));
        subKey.CreateSubKey("UserChoice").SetValue("Progid", (object) "Audible_Download_Manager");
        subKey.CreateSubKey("OpenWithList").SetValue("a", (object) Path.GetFileName(AudibleConvertor.GLOBALS.ExecutablePath));
        subKey.Close();
        FormDownloadManager.SHChangeNotify(134217728U, 0U, IntPtr.Zero, IntPtr.Zero);
      }
      catch (Exception ex)
      {
        Audible.diskLogger("Could not update registry: " + ex.ToString());
      }
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = "cmd.exe";
      process.StartInfo.Arguments = "/c ftype adhfile=\"" + AudibleConvertor.GLOBALS.ExecutablePath + "\" \"%1\"";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.Verb = "runas";
      process.Start();
      string str1 = "";
      process.WaitForExit();
      Audible.diskLogger("adhfile(" + (object) process.ExitCode + "): " + str1);
      process.StartInfo = new ProcessStartInfo();
      process.StartInfo.FileName = "cmd.exe";
      process.StartInfo.Arguments = "/c assoc .adh=adhfile";
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.Verb = "runas";
      process.Start();
      string str2 = "";
      process.WaitForExit();
      Audible.diskLogger("assoc(" + (object) process.ExitCode + "): " + str2);
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
      commonOpenFileDialog.IsFolderPicker = true;
      commonOpenFileDialog.Title = "Select download folder";
      if (this.txtDownoadDir.Text.Trim() != "")
        commonOpenFileDialog.InitialDirectory = this.txtDownoadDir.Text;
      if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
        return;
      this.txtDownoadDir.Text = commonOpenFileDialog.FileName;
      this.downloadPath = commonOpenFileDialog.FileName;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      this.applied = true;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDownloadManager));
      this.label1 = new Label();
      this.txtDownoadDir = new TextBox();
      this.btnBrowse = new Button();
      this.btnSetAssociation = new Button();
      this.btnApply = new Button();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(13, 14);
      this.label1.Name = "label1";
      this.label1.Size = new Size(101, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Download directory:";
      this.txtDownoadDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtDownoadDir.Location = new Point(120, 11);
      this.txtDownoadDir.Name = "txtDownoadDir";
      this.txtDownoadDir.Size = new Size(365, 20);
      this.txtDownoadDir.TabIndex = 1;
      this.btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnBrowse.Location = new Point(491, 9);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new Size(67, 23);
      this.btnBrowse.TabIndex = 2;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
      this.btnSetAssociation.Anchor = AnchorStyles.Top;
      this.btnSetAssociation.Location = new Point(143, 48);
      this.btnSetAssociation.Name = "btnSetAssociation";
      this.btnSetAssociation.Size = new Size(283, 23);
      this.btnSetAssociation.TabIndex = 3;
      this.btnSetAssociation.Text = "Make inAudible the default application for ADH files";
      this.btnSetAssociation.UseVisualStyleBackColor = true;
      this.btnSetAssociation.Click += new EventHandler(this.btnSetAssociation_Click);
      this.btnApply.Anchor = AnchorStyles.Bottom;
      this.btnApply.Location = new Point(247, 77);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 4;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(569, 108);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.btnSetAssociation);
      this.Controls.Add((Control) this.btnBrowse);
      this.Controls.Add((Control) this.txtDownoadDir);
      this.Controls.Add((Control) this.label1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormDownloadManager);
      this.Text = "inAudible Download Manager";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
