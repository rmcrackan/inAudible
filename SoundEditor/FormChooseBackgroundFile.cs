// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormChooseBackgroundFile
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormChooseBackgroundFile : Form
  {
    public ToolTip ToolTip1;
    public Label Label1;
    private OpenFileDialog openFileDialog1;
    public Button buttonOK;
    public CheckBox checkboxLoop;
    public Button buttonBrowse;
    public TextBox textboxPathname;
    private IContainer components;
    private Button buttonCancel;
    public string m_strPathname;
    public bool m_bLoop;

    public FormChooseBackgroundFile()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.buttonOK = new Button();
      this.checkboxLoop = new CheckBox();
      this.buttonBrowse = new Button();
      this.ToolTip1 = new ToolTip(this.components);
      this.textboxPathname = new TextBox();
      this.Label1 = new Label();
      this.openFileDialog1 = new OpenFileDialog();
      this.buttonCancel = new Button();
      this.SuspendLayout();
      this.buttonOK.BackColor = SystemColors.Control;
      this.buttonOK.Cursor = Cursors.Default;
      this.buttonOK.DialogResult = DialogResult.OK;
      this.buttonOK.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonOK.ForeColor = SystemColors.ControlText;
      this.buttonOK.Location = new Point(144, 88);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.RightToLeft = RightToLeft.No;
      this.buttonOK.Size = new Size(96, 25);
      this.buttonOK.TabIndex = 9;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.checkboxLoop.BackColor = SystemColors.Control;
      this.checkboxLoop.Checked = true;
      this.checkboxLoop.CheckState = CheckState.Checked;
      this.checkboxLoop.Cursor = Cursors.Default;
      this.checkboxLoop.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.checkboxLoop.ForeColor = SystemColors.ControlText;
      this.checkboxLoop.Location = new Point(8, 52);
      this.checkboxLoop.Name = "checkboxLoop";
      this.checkboxLoop.RightToLeft = RightToLeft.No;
      this.checkboxLoop.Size = new Size(125, 21);
      this.checkboxLoop.TabIndex = 8;
      this.checkboxLoop.Text = "Apply in loop";
      this.buttonBrowse.BackColor = SystemColors.Control;
      this.buttonBrowse.Cursor = Cursors.Default;
      this.buttonBrowse.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonBrowse.ForeColor = SystemColors.ControlText;
      this.buttonBrowse.Location = new Point(420, 24);
      this.buttonBrowse.Name = "buttonBrowse";
      this.buttonBrowse.RightToLeft = RightToLeft.No;
      this.buttonBrowse.Size = new Size(76, 25);
      this.buttonBrowse.TabIndex = 7;
      this.buttonBrowse.Text = "Browse...";
      this.buttonBrowse.Click += new EventHandler(this.buttonBrowse_Click);
      this.textboxPathname.AcceptsReturn = true;
      this.textboxPathname.AutoSize = false;
      this.textboxPathname.BackColor = SystemColors.Window;
      this.textboxPathname.Cursor = Cursors.IBeam;
      this.textboxPathname.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textboxPathname.ForeColor = SystemColors.WindowText;
      this.textboxPathname.Location = new Point(8, 24);
      this.textboxPathname.MaxLength = 0;
      this.textboxPathname.Name = "textboxPathname";
      this.textboxPathname.RightToLeft = RightToLeft.No;
      this.textboxPathname.Size = new Size(409, 21);
      this.textboxPathname.TabIndex = 5;
      this.textboxPathname.Text = "";
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(8, 8);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(377, 17);
      this.Label1.TabIndex = 6;
      this.Label1.Text = "Enter sound file's full pathname or press the Browse button";
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(264, 88);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(96, 25);
      this.buttonCancel.TabIndex = 10;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(504, 121);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.checkboxLoop);
      this.Controls.Add((Control) this.buttonBrowse);
      this.Controls.Add((Control) this.textboxPathname);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormChooseBackgroundFile);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Choose background file";
      this.ResumeLayout(false);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_strPathname = this.textboxPathname.Text;
      this.m_bLoop = this.checkboxLoop.Checked;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.Filter = "Supported Sounds (*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv;*.it;*.xm;*.s3m;*.mod;*.mtm;*.mo3;*.cda)|*.mp3;*.mp2;*.wav;*.ogg;*.aiff;*.wma;*.wmv;*.asx;*.asf;*.m4a;*.mp4;*.flac;*.aac;*.ac3;*.wv|MP3 and MP2 sounds (*.mp3;*.mp2)|*.mp3;*.mp2|AAC and MP4 sounds (*.aac;*.mp4)|*.aac;*.mp4|WAV sounds (*.wav)|*.wav|OGG Vorbis sounds (*.ogg)|*.ogg|AIFF sounds (*.aiff)|*.aiff|Windows Media sounds (*.wma;*.wmv;*.asx;*.asf)|*.wma;*.wmv;*.asx;*.asf|AC3 sounds (*.ac3)|*.ac3;|ALAC sounds (*.m4a)|*.ac3;|FLAC sounds (*.flac)|*.flac;|WavPack sounds (*.wv)|*.wv;|All files (*.*)|*.*";
      this.openFileDialog1.Title = "Choose a sound file";
      if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
        return;
      this.textboxPathname.Text = this.openFileDialog1.FileName;
    }
  }
}
