// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormSilence
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
  public class FormSilence : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    public Label Label1;
    public Button buttonOK;
    public TextBox textboxSilenceLength;
    private Button buttonCancel;
    private Container components;
    public int m_nSilenceLengthInMs;

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    public FormSilence()
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
      this.buttonOK = new Button();
      this.textboxSilenceLength = new TextBox();
      this.Label1 = new Label();
      this.buttonCancel = new Button();
      this.SuspendLayout();
      this.buttonOK.BackColor = SystemColors.Control;
      this.buttonOK.Cursor = Cursors.Default;
      this.buttonOK.DialogResult = DialogResult.OK;
      this.buttonOK.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonOK.ForeColor = SystemColors.ControlText;
      this.buttonOK.Location = new Point(20, 68);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.RightToLeft = RightToLeft.No;
      this.buttonOK.Size = new Size(80, 24);
      this.buttonOK.TabIndex = 5;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.textboxSilenceLength.AcceptsReturn = true;
      this.textboxSilenceLength.AutoSize = false;
      this.textboxSilenceLength.BackColor = SystemColors.Window;
      this.textboxSilenceLength.Cursor = Cursors.IBeam;
      this.textboxSilenceLength.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textboxSilenceLength.ForeColor = SystemColors.WindowText;
      this.textboxSilenceLength.Location = new Point(52, 36);
      this.textboxSilenceLength.MaxLength = 0;
      this.textboxSilenceLength.Name = "textboxSilenceLength";
      this.textboxSilenceLength.RightToLeft = RightToLeft.No;
      this.textboxSilenceLength.Size = new Size(109, 17);
      this.textboxSilenceLength.TabIndex = 4;
      this.textboxSilenceLength.Text = "1000";
      this.Label1.BackColor = SystemColors.Control;
      this.Label1.Cursor = Cursors.Default;
      this.Label1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Label1.ForeColor = SystemColors.ControlText;
      this.Label1.Location = new Point(14, 12);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = RightToLeft.No;
      this.Label1.Size = new Size(185, 17);
      this.Label1.TabIndex = 3;
      this.Label1.Text = "Enter silence length in milliseconds";
      this.Label1.TextAlign = ContentAlignment.MiddleCenter;
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(112, 68);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(80, 24);
      this.buttonCancel.TabIndex = 6;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(212, 105);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.textboxSilenceLength);
      this.Controls.Add((Control) this.Label1);
      this.Name = nameof (FormSilence);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Silence length";
      this.Load += new EventHandler(this.FormSilence_Load);
      this.ResumeLayout(false);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nSilenceLengthInMs = Convert.ToInt32(this.textboxSilenceLength.Text);
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void FormSilence_Load(object sender, EventArgs e)
    {
      FormSilence.SetWindowLong(this.textboxSilenceLength.Handle, -16, FormSilence.GetWindowLong(this.textboxSilenceLength.Handle, -16) | 8192);
      this.m_nSilenceLengthInMs = -1;
    }
  }
}
