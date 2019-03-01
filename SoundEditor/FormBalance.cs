// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormBalance
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormBalance : Form
  {
    private Button buttonAboutBox;
    private TrackBar trackBarBalanceExternal;
    private Label label1;
    private Button buttonCancel;
    private Button buttonOK;
    private Label label2;
    private Label label3;
    private Container components;
    public bool m_bUseInternal;
    public short m_nBalancePercentage;
    public bool m_bCancel;
    public int m_idDspBalanceExternal;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;

    public FormBalance()
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
      this.buttonAboutBox = new Button();
      this.trackBarBalanceExternal = new TrackBar();
      this.label1 = new Label();
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.label2 = new Label();
      this.label3 = new Label();
      this.trackBarBalanceExternal.BeginInit();
      this.SuspendLayout();
      this.buttonAboutBox.Location = new Point(104, 128);
      this.buttonAboutBox.Name = "buttonAboutBox";
      this.buttonAboutBox.Size = new Size(144, 32);
      this.buttonAboutBox.TabIndex = 7;
      this.buttonAboutBox.Text = "Display DSP's About box";
      this.buttonAboutBox.Click += new System.EventHandler(this.buttonAboutBox_Click);
      this.trackBarBalanceExternal.Location = new Point(72, 72);
      this.trackBarBalanceExternal.Maximum = 100;
      this.trackBarBalanceExternal.Minimum = -100;
      this.trackBarBalanceExternal.Name = "trackBarBalanceExternal";
      this.trackBarBalanceExternal.Size = new Size(208, 45);
      this.trackBarBalanceExternal.SmallChange = 5;
      this.trackBarBalanceExternal.TabIndex = 6;
      this.trackBarBalanceExternal.TickFrequency = 10;
      this.trackBarBalanceExternal.TickStyle = TickStyle.TopLeft;
      this.trackBarBalanceExternal.Scroll += new System.EventHandler(this.trackBarBalanceExternal_Scroll);
      this.label1.Location = new Point(12, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(328, 16);
      this.label1.TabIndex = 8;
      this.label1.Text = "This sample demonstrates how to apply a Balance custom DSP";
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(188, 200);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 10;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(60, 200);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 9;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.label2.Location = new Point(80, 56);
      this.label2.Name = "label2";
      this.label2.Size = new Size(48, 16);
      this.label2.TabIndex = 11;
      this.label2.Text = "Left";
      this.label3.Location = new Point(224, 56);
      this.label3.Name = "label3";
      this.label3.Size = new Size(48, 16);
      this.label3.TabIndex = 12;
      this.label3.Text = "Right";
      this.label3.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(352, 238);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonAboutBox);
      this.Controls.Add((Control) this.trackBarBalanceExternal);
      this.Name = nameof (FormBalance);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Balance DSP settings";
      this.Load += new System.EventHandler(this.FormBalance_Load);
      this.trackBarBalanceExternal.EndInit();
      this.ResumeLayout(false);
    }

    private void FormBalance_Load(object sender, EventArgs e)
    {
      if (!this.m_bUseInternal)
        return;
      this.buttonAboutBox.Visible = false;
    }

    private void trackBarBalanceExternal_Scroll(object sender, EventArgs e)
    {
      if (this.m_bUseInternal)
      {
        this.m_nBalancePercentage = (short) this.trackBarBalanceExternal.Value;
      }
      else
      {
        BALANCE_PARAMETERS balanceParameters = new BALANCE_PARAMETERS();
        balanceParameters.nBalancePercentage = (short) this.trackBarBalanceExternal.Value;
        IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf((object) balanceParameters));
        Marshal.StructureToPtr((object) balanceParameters, num1, true);
        int num2 = (int) this.audioSoundEditor1.Effects.CustomDspExternalSetParameters(this.m_idDspBalanceExternal, num1);
        Marshal.FreeHGlobal(num1);
      }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void buttonAboutBox_Click(object sender, EventArgs e)
    {
      this.audioSoundEditor1.Effects.CustomDspExternalSendCommand(this.m_idDspBalanceExternal, this.Handle, "AboutBox");
    }
  }
}
