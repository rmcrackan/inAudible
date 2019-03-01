// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormVolume
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudioSoundEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormVolume : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    private const int VOLUME_FLAT = 0;
    private const int VOLUME_SLIDING = 1;
    private TextBox textBoxInitialVolume;
    private TextBox textBoxFinalVolume;
    private RadioButton radioButtonBoth;
    private RadioButton radioButtonLeft;
    private RadioButton radioButtonRight;
    private Button buttonOK;
    private Button buttonCancel;
    private Label labelInitialVolume;
    private Label labelFinalVolume;
    private GroupBox groupBoxChannels;
    private Container components;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public short m_nVolumeMode;
    public short m_nInitialVolume;
    public short m_nFinalVolume;
    public enumChannels m_nAffectedChannels;
    public bool m_bCancel;

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    public FormVolume()
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
      this.labelInitialVolume = new Label();
      this.labelFinalVolume = new Label();
      this.textBoxInitialVolume = new TextBox();
      this.textBoxFinalVolume = new TextBox();
      this.groupBoxChannels = new GroupBox();
      this.radioButtonRight = new RadioButton();
      this.radioButtonLeft = new RadioButton();
      this.radioButtonBoth = new RadioButton();
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.groupBoxChannels.SuspendLayout();
      this.SuspendLayout();
      this.labelInitialVolume.Location = new Point(16, 16);
      this.labelInitialVolume.Name = "labelInitialVolume";
      this.labelInitialVolume.Size = new Size(168, 16);
      this.labelInitialVolume.TabIndex = 0;
      this.labelInitialVolume.Text = "Flat volume (expressed in %)";
      this.labelFinalVolume.Location = new Point(16, 72);
      this.labelFinalVolume.Name = "labelFinalVolume";
      this.labelFinalVolume.Size = new Size(160, 16);
      this.labelFinalVolume.TabIndex = 1;
      this.labelFinalVolume.Text = "Final volume (expressed in %)";
      this.textBoxInitialVolume.Location = new Point(16, 40);
      this.textBoxInitialVolume.Name = "textBoxInitialVolume";
      this.textBoxInitialVolume.Size = new Size(168, 20);
      this.textBoxInitialVolume.TabIndex = 2;
      this.textBoxInitialVolume.Text = "200";
      this.textBoxFinalVolume.Location = new Point(16, 96);
      this.textBoxFinalVolume.Name = "textBoxFinalVolume";
      this.textBoxFinalVolume.Size = new Size(168, 20);
      this.textBoxFinalVolume.TabIndex = 3;
      this.textBoxFinalVolume.Text = "100";
      this.groupBoxChannels.Controls.Add((Control) this.radioButtonRight);
      this.groupBoxChannels.Controls.Add((Control) this.radioButtonLeft);
      this.groupBoxChannels.Controls.Add((Control) this.radioButtonBoth);
      this.groupBoxChannels.Location = new Point(200, 8);
      this.groupBoxChannels.Name = "groupBoxChannels";
      this.groupBoxChannels.Size = new Size(128, 112);
      this.groupBoxChannels.TabIndex = 4;
      this.groupBoxChannels.TabStop = false;
      this.groupBoxChannels.Text = "Affected channels";
      this.radioButtonRight.Location = new Point(16, 80);
      this.radioButtonRight.Name = "radioButtonRight";
      this.radioButtonRight.Size = new Size(96, 16);
      this.radioButtonRight.TabIndex = 2;
      this.radioButtonRight.Text = "Right channel";
      this.radioButtonLeft.Location = new Point(16, 56);
      this.radioButtonLeft.Name = "radioButtonLeft";
      this.radioButtonLeft.Size = new Size(96, 16);
      this.radioButtonLeft.TabIndex = 1;
      this.radioButtonLeft.Text = "Left channel";
      this.radioButtonBoth.Location = new Point(16, 32);
      this.radioButtonBoth.Name = "radioButtonBoth";
      this.radioButtonBoth.Size = new Size(96, 16);
      this.radioButtonBoth.TabIndex = 0;
      this.radioButtonBoth.Text = "Both channels";
      this.buttonOK.Location = new Point(64, 144);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 5;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(192, 144);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 6;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.CancelButton = (IButtonControl) this.buttonCancel;
      this.ClientSize = new Size(360, 182);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.groupBoxChannels);
      this.Controls.Add((Control) this.textBoxFinalVolume);
      this.Controls.Add((Control) this.textBoxInitialVolume);
      this.Controls.Add((Control) this.labelFinalVolume);
      this.Controls.Add((Control) this.labelInitialVolume);
      this.Name = nameof (FormVolume);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Volume settings";
      this.Load += new System.EventHandler(this.FormVolume_Load);
      this.groupBoxChannels.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void FormVolume_Load(object sender, EventArgs e)
    {
      FormVolume.SetWindowLong(this.textBoxInitialVolume.Handle, -16, FormVolume.GetWindowLong(this.textBoxInitialVolume.Handle, -16) | 8192);
      FormVolume.SetWindowLong(this.textBoxFinalVolume.Handle, -16, FormVolume.GetWindowLong(this.textBoxFinalVolume.Handle, -16) | 8192);
      if ((int) this.m_nVolumeMode == 0)
      {
        this.Text = "Flat volume settings";
        this.labelInitialVolume.Text = "Flat volume (expressed in %)";
        this.labelFinalVolume.Visible = false;
        this.textBoxFinalVolume.Visible = false;
        this.textBoxInitialVolume.Text = "200";
      }
      else
      {
        this.Text = "Sliding volume settings";
        this.labelInitialVolume.Text = "Initial volume (expressed in %)";
        this.labelFinalVolume.Visible = true;
        this.textBoxFinalVolume.Visible = true;
        this.textBoxInitialVolume.Text = "0";
        this.textBoxFinalVolume.Text = "100";
      }
      this.m_nInitialVolume = (short) -1;
      this.m_nFinalVolume = (short) -1;
      if (this.audioSoundEditor1.GetChannels() == 2)
      {
        this.groupBoxChannels.Enabled = true;
        this.radioButtonBoth.Checked = true;
      }
      else
      {
        this.groupBoxChannels.Enabled = false;
        this.radioButtonBoth.Enabled = false;
        this.radioButtonLeft.Enabled = false;
        this.radioButtonRight.Enabled = false;
      }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nInitialVolume = Convert.ToInt16(this.textBoxInitialVolume.Text);
      this.m_nFinalVolume = Convert.ToInt16(this.textBoxFinalVolume.Text);
      if (this.radioButtonBoth.Checked)
        this.m_nAffectedChannels = enumChannels.CHANNELS_BOTH;
      else if (this.radioButtonLeft.Checked)
        this.m_nAffectedChannels = enumChannels.CHANNELS_LEFT;
      else if (this.radioButtonRight.Checked)
        this.m_nAffectedChannels = enumChannels.CHANNELS_RIGHT;
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }
  }
}
