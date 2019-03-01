// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormBassBoost
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormBassBoost : Form
  {
    private Button buttonCancel;
    private Button buttonOK;
    private Label label1;
    private Button buttonAboutBox;
    private Label labelDspUIPosition;
    private Container components;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    internal int m_idDspBassBoostExternal;
    public bool m_bCancel;

    public FormBassBoost()
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
      this.label1 = new Label();
      this.buttonAboutBox = new Button();
      this.labelDspUIPosition = new Label();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(240, 232);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 14;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(112, 232);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 13;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.label1.Location = new Point(16, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(392, 32);
      this.label1.TabIndex = 12;
      this.label1.Text = "This sample demonstrates how to apply a custom DSP (Bass Boost), contained inside an external DLL, which comes with its own User Interface";
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.buttonAboutBox.Location = new Point(152, 176);
      this.buttonAboutBox.Name = "buttonAboutBox";
      this.buttonAboutBox.Size = new Size(144, 32);
      this.buttonAboutBox.TabIndex = 11;
      this.buttonAboutBox.Text = "Display DSP's About box";
      this.buttonAboutBox.Click += new System.EventHandler(this.buttonAboutBox_Click);
      this.labelDspUIPosition.Location = new Point(24, 72);
      this.labelDspUIPosition.Name = "labelDspUIPosition";
      this.labelDspUIPosition.Size = new Size(80, 32);
      this.labelDspUIPosition.TabIndex = 15;
      this.labelDspUIPosition.Text = "label2";
      this.labelDspUIPosition.Visible = false;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(456, 270);
      this.ControlBox = false;
      this.Controls.Add((Control) this.labelDspUIPosition);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonAboutBox);
      this.Name = nameof (FormBassBoost);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Bass Boost settings";
      this.Load += new System.EventHandler(this.FormBassBoost_Load);
      this.ResumeLayout(false);
    }

    private void FormBassBoost_Load(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.CustomDspExternalEditorShow(this.m_idDspBassBoostExternal, true, this.Handle, this.labelDspUIPosition.Left, this.labelDspUIPosition.Top);
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
      this.audioSoundEditor1.Effects.CustomDspExternalSendCommand(this.m_idDspBassBoostExternal, this.Handle, "AboutBox");
    }
  }
}
