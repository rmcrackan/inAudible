// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormRemoveSilence
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
  public class FormRemoveSilence : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    private Button buttonCancel;
    public Button buttonOK;
    private Label label1;
    private Label label2;
    private TextBox textBoxSilenceThreshold;
    private TextBox textBoxSilenceMinLength;
    private Container components;
    public short m_nSilenceThreshold;
    public int m_nSilenceMinLengthInMs;
    public bool m_bTrimOnly;
    public bool m_bCancel;

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    public FormRemoveSilence()
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
      this.label2 = new Label();
      this.textBoxSilenceThreshold = new TextBox();
      this.textBoxSilenceMinLength = new TextBox();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(154, 136);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(80, 24);
      this.buttonCancel.TabIndex = 8;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.BackColor = SystemColors.Control;
      this.buttonOK.Cursor = Cursors.Default;
      this.buttonOK.DialogResult = DialogResult.OK;
      this.buttonOK.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonOK.ForeColor = SystemColors.ControlText;
      this.buttonOK.Location = new Point(58, 136);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.RightToLeft = RightToLeft.No;
      this.buttonOK.Size = new Size(80, 24);
      this.buttonOK.TabIndex = 7;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.label1.Location = new Point(38, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(216, 16);
      this.label1.TabIndex = 9;
      this.label1.Text = "Silence threshold (value range 1-32767)";
      this.label1.TextAlign = ContentAlignment.TopCenter;
      this.label2.Location = new Point(42, 72);
      this.label2.Name = "label2";
      this.label2.Size = new Size(208, 16);
      this.label2.TabIndex = 10;
      this.label2.Text = "Silence minimal length in milliseconds";
      this.label2.TextAlign = ContentAlignment.TopCenter;
      this.textBoxSilenceThreshold.Location = new Point(82, 40);
      this.textBoxSilenceThreshold.Name = "textBoxSilenceThreshold";
      this.textBoxSilenceThreshold.Size = new Size(128, 20);
      this.textBoxSilenceThreshold.TabIndex = 11;
      this.textBoxSilenceThreshold.Text = "1000";
      this.textBoxSilenceMinLength.Location = new Point(82, 96);
      this.textBoxSilenceMinLength.Name = "textBoxSilenceMinLength";
      this.textBoxSilenceMinLength.Size = new Size(128, 20);
      this.textBoxSilenceMinLength.TabIndex = 12;
      this.textBoxSilenceMinLength.Text = "500";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(292, 174);
      this.ControlBox = false;
      this.Controls.Add((Control) this.textBoxSilenceMinLength);
      this.Controls.Add((Control) this.textBoxSilenceThreshold);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormRemoveSilence);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Configure silence removal";
      this.Load += new EventHandler(this.FormRemoveSilence_Load);
      this.ResumeLayout(false);
    }

    private void FormRemoveSilence_Load(object sender, EventArgs e)
    {
      if (this.m_bTrimOnly)
      {
        this.label2.Visible = false;
        this.textBoxSilenceMinLength.Visible = false;
      }
      else
      {
        this.label2.Visible = true;
        this.textBoxSilenceMinLength.Visible = true;
      }
      FormRemoveSilence.SetWindowLong(this.textBoxSilenceThreshold.Handle, -16, FormRemoveSilence.GetWindowLong(this.textBoxSilenceThreshold.Handle, -16) | 8192);
      FormRemoveSilence.SetWindowLong(this.textBoxSilenceMinLength.Handle, -16, FormRemoveSilence.GetWindowLong(this.textBoxSilenceMinLength.Handle, -16) | 8192);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nSilenceThreshold = Convert.ToInt16(this.textBoxSilenceThreshold.Text);
      this.m_nSilenceMinLengthInMs = Convert.ToInt32(this.textBoxSilenceMinLength.Text);
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
