// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormVolumeAutomation
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
  public class FormVolumeAutomation : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    private Button buttonCancel;
    private Button buttonOK;
    private GroupBox groupBoxChannels;
    private RadioButton radioButtonRight;
    private RadioButton radioButtonLeft;
    private RadioButton radioButtonBoth;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private GroupBox groupBox1;
    private Label label7;
    private Label label8;
    private TextBox textBoxPositionPoint0;
    private TextBox textBoxVolumePoint0;
    private GroupBox groupBox2;
    private Label label9;
    private Label label10;
    private GroupBox groupBox3;
    private Label label11;
    private Label label12;
    private GroupBox groupBox4;
    private Label label13;
    private Label label14;
    private GroupBox groupBox5;
    private Label label15;
    private Label label16;
    private GroupBox groupBox6;
    private Label label17;
    private Label label18;
    private TextBox textBoxVolumePoint1;
    private TextBox textBoxPositionPoint1;
    private TextBox textBoxVolumePoint2;
    private TextBox textBoxPositionPoint2;
    private TextBox textBoxVolumePoint3;
    private TextBox textBoxPositionPoint3;
    private TextBox textBoxVolumePoint4;
    private TextBox textBoxPositionPoint4;
    private TextBox textBoxVolumePoint5;
    private TextBox textBoxPositionPoint5;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public enumChannels m_nAffectedChannels;
    public bool m_bCancel;
    private Container components;

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    public FormVolumeAutomation()
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
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.groupBoxChannels = new GroupBox();
      this.radioButtonRight = new RadioButton();
      this.radioButtonLeft = new RadioButton();
      this.radioButtonBoth = new RadioButton();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.groupBox1 = new GroupBox();
      this.textBoxVolumePoint0 = new TextBox();
      this.textBoxPositionPoint0 = new TextBox();
      this.label8 = new Label();
      this.label7 = new Label();
      this.groupBox2 = new GroupBox();
      this.textBoxVolumePoint1 = new TextBox();
      this.textBoxPositionPoint1 = new TextBox();
      this.label9 = new Label();
      this.label10 = new Label();
      this.groupBox3 = new GroupBox();
      this.textBoxVolumePoint2 = new TextBox();
      this.textBoxPositionPoint2 = new TextBox();
      this.label11 = new Label();
      this.label12 = new Label();
      this.groupBox4 = new GroupBox();
      this.textBoxVolumePoint3 = new TextBox();
      this.textBoxPositionPoint3 = new TextBox();
      this.label13 = new Label();
      this.label14 = new Label();
      this.groupBox5 = new GroupBox();
      this.textBoxVolumePoint4 = new TextBox();
      this.textBoxPositionPoint4 = new TextBox();
      this.label15 = new Label();
      this.label16 = new Label();
      this.groupBox6 = new GroupBox();
      this.textBoxVolumePoint5 = new TextBox();
      this.textBoxPositionPoint5 = new TextBox();
      this.label17 = new Label();
      this.label18 = new Label();
      this.groupBoxChannels.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(360, 344);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 9;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(360, 304);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(104, 24);
      this.buttonOK.TabIndex = 8;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.groupBoxChannels.Controls.Add((Control) this.radioButtonRight);
      this.groupBoxChannels.Controls.Add((Control) this.radioButtonLeft);
      this.groupBoxChannels.Controls.Add((Control) this.radioButtonBoth);
      this.groupBoxChannels.Location = new Point(348, 168);
      this.groupBoxChannels.Name = "groupBoxChannels";
      this.groupBoxChannels.Size = new Size(128, 112);
      this.groupBoxChannels.TabIndex = 7;
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
      this.label1.Location = new Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new Size(480, 40);
      this.label1.TabIndex = 10;
      this.label1.Text = "This is a small sample which demonstrates how to create volume automation. In this sample we will create 6 volume points at 6 different percentage positions: it's important to note that inside your application you could create as many volume points you need.";
      this.label1.TextAlign = ContentAlignment.TopCenter;
      this.label2.Location = new Point(8, 56);
      this.label2.Name = "label2";
      this.label2.Size = new Size(480, 40);
      this.label2.TabIndex = 11;
      this.label2.Text = "For each given volume point you can modify both the position's percentage (related to the whole loaded sound or to a selected range) and the percentage of volume level using the schema below: ";
      this.label2.TextAlign = ContentAlignment.TopCenter;
      this.label3.Location = new Point(56, 96);
      this.label3.Name = "label3";
      this.label3.Size = new Size(408, 16);
      this.label3.TabIndex = 12;
      this.label3.Text = "- 0% means silence";
      this.label4.Location = new Point(56, 112);
      this.label4.Name = "label4";
      this.label4.Size = new Size(408, 16);
      this.label4.TabIndex = 13;
      this.label4.Text = "- Values between 1% and 99% will cause an attenuation of the original sound";
      this.label5.Location = new Point(56, 128);
      this.label5.Name = "label5";
      this.label5.Size = new Size(408, 16);
      this.label5.TabIndex = 14;
      this.label5.Text = "- 100% keeps the original volume level";
      this.label6.Location = new Point(56, 144);
      this.label6.Name = "label6";
      this.label6.Size = new Size(408, 16);
      this.label6.TabIndex = 15;
      this.label6.Text = "- Values higher than 100% will cause an amplification of the original sound";
      this.groupBox1.Controls.Add((Control) this.textBoxVolumePoint0);
      this.groupBox1.Controls.Add((Control) this.textBoxPositionPoint0);
      this.groupBox1.Controls.Add((Control) this.label8);
      this.groupBox1.Controls.Add((Control) this.label7);
      this.groupBox1.Location = new Point(16, 168);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(312, 45);
      this.groupBox1.TabIndex = 16;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Volume point 0";
      this.textBoxVolumePoint0.Location = new Point(248, 16);
      this.textBoxVolumePoint0.Name = "textBoxVolumePoint0";
      this.textBoxVolumePoint0.Size = new Size(40, 20);
      this.textBoxVolumePoint0.TabIndex = 3;
      this.textBoxVolumePoint0.Text = "0";
      this.textBoxPositionPoint0.Location = new Point(88, 16);
      this.textBoxPositionPoint0.Name = "textBoxPositionPoint0";
      this.textBoxPositionPoint0.Size = new Size(40, 20);
      this.textBoxPositionPoint0.TabIndex = 2;
      this.textBoxPositionPoint0.Text = "0";
      this.label8.Location = new Point(144, 18);
      this.label8.Name = "label8";
      this.label8.Size = new Size(96, 16);
      this.label8.TabIndex = 1;
      this.label8.Text = "Volume level (%)";
      this.label8.TextAlign = ContentAlignment.MiddleRight;
      this.label7.Location = new Point(8, 18);
      this.label7.Name = "label7";
      this.label7.Size = new Size(72, 16);
      this.label7.TabIndex = 0;
      this.label7.Text = "Position (%)";
      this.label7.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox2.Controls.Add((Control) this.textBoxVolumePoint1);
      this.groupBox2.Controls.Add((Control) this.textBoxPositionPoint1);
      this.groupBox2.Controls.Add((Control) this.label9);
      this.groupBox2.Controls.Add((Control) this.label10);
      this.groupBox2.Location = new Point(16, 216);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(312, 45);
      this.groupBox2.TabIndex = 17;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Volume point 1";
      this.textBoxVolumePoint1.Location = new Point(248, 16);
      this.textBoxVolumePoint1.Name = "textBoxVolumePoint1";
      this.textBoxVolumePoint1.Size = new Size(40, 20);
      this.textBoxVolumePoint1.TabIndex = 3;
      this.textBoxVolumePoint1.Text = "100";
      this.textBoxPositionPoint1.Location = new Point(88, 16);
      this.textBoxPositionPoint1.Name = "textBoxPositionPoint1";
      this.textBoxPositionPoint1.Size = new Size(40, 20);
      this.textBoxPositionPoint1.TabIndex = 2;
      this.textBoxPositionPoint1.Text = "20";
      this.label9.Location = new Point(144, 18);
      this.label9.Name = "label9";
      this.label9.Size = new Size(96, 16);
      this.label9.TabIndex = 1;
      this.label9.Text = "Volume level (%)";
      this.label9.TextAlign = ContentAlignment.MiddleRight;
      this.label10.Location = new Point(8, 18);
      this.label10.Name = "label10";
      this.label10.Size = new Size(72, 16);
      this.label10.TabIndex = 0;
      this.label10.Text = "Position (%)";
      this.label10.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox3.Controls.Add((Control) this.textBoxVolumePoint2);
      this.groupBox3.Controls.Add((Control) this.textBoxPositionPoint2);
      this.groupBox3.Controls.Add((Control) this.label11);
      this.groupBox3.Controls.Add((Control) this.label12);
      this.groupBox3.Location = new Point(16, 264);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(312, 45);
      this.groupBox3.TabIndex = 18;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Volume point 2";
      this.textBoxVolumePoint2.Location = new Point(248, 16);
      this.textBoxVolumePoint2.Name = "textBoxVolumePoint2";
      this.textBoxVolumePoint2.Size = new Size(40, 20);
      this.textBoxVolumePoint2.TabIndex = 3;
      this.textBoxVolumePoint2.Text = "400";
      this.textBoxPositionPoint2.Location = new Point(88, 16);
      this.textBoxPositionPoint2.Name = "textBoxPositionPoint2";
      this.textBoxPositionPoint2.Size = new Size(40, 20);
      this.textBoxPositionPoint2.TabIndex = 2;
      this.textBoxPositionPoint2.Text = "40";
      this.label11.Location = new Point(144, 18);
      this.label11.Name = "label11";
      this.label11.Size = new Size(96, 16);
      this.label11.TabIndex = 1;
      this.label11.Text = "Volume level (%)";
      this.label11.TextAlign = ContentAlignment.MiddleRight;
      this.label12.Location = new Point(8, 18);
      this.label12.Name = "label12";
      this.label12.Size = new Size(72, 16);
      this.label12.TabIndex = 0;
      this.label12.Text = "Position (%)";
      this.label12.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox4.Controls.Add((Control) this.textBoxVolumePoint3);
      this.groupBox4.Controls.Add((Control) this.textBoxPositionPoint3);
      this.groupBox4.Controls.Add((Control) this.label13);
      this.groupBox4.Controls.Add((Control) this.label14);
      this.groupBox4.Location = new Point(16, 312);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(312, 45);
      this.groupBox4.TabIndex = 19;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Volume point 3";
      this.textBoxVolumePoint3.Location = new Point(248, 16);
      this.textBoxVolumePoint3.Name = "textBoxVolumePoint3";
      this.textBoxVolumePoint3.Size = new Size(40, 20);
      this.textBoxVolumePoint3.TabIndex = 3;
      this.textBoxVolumePoint3.Text = "20";
      this.textBoxPositionPoint3.Location = new Point(88, 16);
      this.textBoxPositionPoint3.Name = "textBoxPositionPoint3";
      this.textBoxPositionPoint3.Size = new Size(40, 20);
      this.textBoxPositionPoint3.TabIndex = 2;
      this.textBoxPositionPoint3.Text = "60";
      this.label13.Location = new Point(144, 18);
      this.label13.Name = "label13";
      this.label13.Size = new Size(96, 16);
      this.label13.TabIndex = 1;
      this.label13.Text = "Volume level (%)";
      this.label13.TextAlign = ContentAlignment.MiddleRight;
      this.label14.Location = new Point(8, 18);
      this.label14.Name = "label14";
      this.label14.Size = new Size(72, 16);
      this.label14.TabIndex = 0;
      this.label14.Text = "Position (%)";
      this.label14.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox5.Controls.Add((Control) this.textBoxVolumePoint4);
      this.groupBox5.Controls.Add((Control) this.textBoxPositionPoint4);
      this.groupBox5.Controls.Add((Control) this.label15);
      this.groupBox5.Controls.Add((Control) this.label16);
      this.groupBox5.Location = new Point(16, 360);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new Size(312, 45);
      this.groupBox5.TabIndex = 20;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Volume point 4";
      this.textBoxVolumePoint4.Location = new Point(248, 16);
      this.textBoxVolumePoint4.Name = "textBoxVolumePoint4";
      this.textBoxVolumePoint4.Size = new Size(40, 20);
      this.textBoxVolumePoint4.TabIndex = 3;
      this.textBoxVolumePoint4.Text = "100";
      this.textBoxPositionPoint4.Location = new Point(88, 16);
      this.textBoxPositionPoint4.Name = "textBoxPositionPoint4";
      this.textBoxPositionPoint4.Size = new Size(40, 20);
      this.textBoxPositionPoint4.TabIndex = 2;
      this.textBoxPositionPoint4.Text = "80";
      this.label15.Location = new Point(144, 18);
      this.label15.Name = "label15";
      this.label15.Size = new Size(96, 16);
      this.label15.TabIndex = 1;
      this.label15.Text = "Volume level (%)";
      this.label15.TextAlign = ContentAlignment.MiddleRight;
      this.label16.Location = new Point(8, 18);
      this.label16.Name = "label16";
      this.label16.Size = new Size(72, 16);
      this.label16.TabIndex = 0;
      this.label16.Text = "Position (%)";
      this.label16.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox6.Controls.Add((Control) this.textBoxVolumePoint5);
      this.groupBox6.Controls.Add((Control) this.textBoxPositionPoint5);
      this.groupBox6.Controls.Add((Control) this.label17);
      this.groupBox6.Controls.Add((Control) this.label18);
      this.groupBox6.Location = new Point(16, 408);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new Size(312, 45);
      this.groupBox6.TabIndex = 21;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Volume point 5";
      this.textBoxVolumePoint5.Location = new Point(248, 16);
      this.textBoxVolumePoint5.Name = "textBoxVolumePoint5";
      this.textBoxVolumePoint5.Size = new Size(40, 20);
      this.textBoxVolumePoint5.TabIndex = 3;
      this.textBoxVolumePoint5.Text = "0";
      this.textBoxPositionPoint5.Location = new Point(88, 16);
      this.textBoxPositionPoint5.Name = "textBoxPositionPoint5";
      this.textBoxPositionPoint5.Size = new Size(40, 20);
      this.textBoxPositionPoint5.TabIndex = 2;
      this.textBoxPositionPoint5.Text = "100";
      this.label17.Location = new Point(144, 18);
      this.label17.Name = "label17";
      this.label17.Size = new Size(96, 16);
      this.label17.TabIndex = 1;
      this.label17.Text = "Volume level (%)";
      this.label17.TextAlign = ContentAlignment.MiddleRight;
      this.label18.Location = new Point(8, 18);
      this.label18.Name = "label18";
      this.label18.Size = new Size(72, 16);
      this.label18.TabIndex = 0;
      this.label18.Text = "Position (%)";
      this.label18.TextAlign = ContentAlignment.MiddleRight;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(496, 462);
      this.ControlBox = false;
      this.Controls.Add((Control) this.groupBox6);
      this.Controls.Add((Control) this.groupBox5);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.groupBoxChannels);
      this.Name = nameof (FormVolumeAutomation);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Volume automation settings";
      this.Load += new System.EventHandler(this.FormVolumeAutomation_Load);
      this.groupBoxChannels.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox6.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void FormVolumeAutomation_Load(object sender, EventArgs e)
    {
      FormVolumeAutomation.SetWindowLong(this.textBoxPositionPoint0.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxPositionPoint0.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxPositionPoint1.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxPositionPoint1.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxPositionPoint2.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxPositionPoint2.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxPositionPoint3.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxPositionPoint3.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxPositionPoint4.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxPositionPoint4.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxPositionPoint5.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxPositionPoint5.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxVolumePoint0.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxVolumePoint0.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxVolumePoint1.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxVolumePoint1.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxVolumePoint2.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxVolumePoint2.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxVolumePoint3.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxVolumePoint3.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxVolumePoint4.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxVolumePoint4.Handle, -16) | 8192);
      FormVolumeAutomation.SetWindowLong(this.textBoxVolumePoint5.Handle, -16, FormVolumeAutomation.GetWindowLong(this.textBoxVolumePoint5.Handle, -16) | 8192);
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
      int num1 = (int) this.audioSoundEditor1.Effects.VolumeAutomationReset();
      int num2 = (int) this.audioSoundEditor1.Effects.VolumeAutomationPointAddNew(Convert.ToSingle(this.textBoxPositionPoint0.Text), Convert.ToSingle(this.textBoxVolumePoint0.Text), enumVolumeCurves.VOLUME_CURVE_NONE, 0);
      int num3 = (int) this.audioSoundEditor1.Effects.VolumeAutomationPointAddNew(Convert.ToSingle(this.textBoxPositionPoint1.Text), Convert.ToSingle(this.textBoxVolumePoint1.Text), enumVolumeCurves.VOLUME_CURVE_NONE, 0);
      int num4 = (int) this.audioSoundEditor1.Effects.VolumeAutomationPointAddNew(Convert.ToSingle(this.textBoxPositionPoint2.Text), Convert.ToSingle(this.textBoxVolumePoint2.Text), enumVolumeCurves.VOLUME_CURVE_NONE, 0);
      int num5 = (int) this.audioSoundEditor1.Effects.VolumeAutomationPointAddNew(Convert.ToSingle(this.textBoxPositionPoint3.Text), Convert.ToSingle(this.textBoxVolumePoint3.Text), enumVolumeCurves.VOLUME_CURVE_NONE, 0);
      int num6 = (int) this.audioSoundEditor1.Effects.VolumeAutomationPointAddNew(Convert.ToSingle(this.textBoxPositionPoint4.Text), Convert.ToSingle(this.textBoxVolumePoint4.Text), enumVolumeCurves.VOLUME_CURVE_NONE, 0);
      int num7 = (int) this.audioSoundEditor1.Effects.VolumeAutomationPointAddNew(Convert.ToSingle(this.textBoxPositionPoint5.Text), Convert.ToSingle(this.textBoxVolumePoint5.Text), enumVolumeCurves.VOLUME_CURVE_NONE, 0);
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
