// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormDeHiss
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
  public class FormDeHiss : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    public bool m_bCancel;
    public int m_nWindowType;
    public int m_nWindowSize;
    public int m_nNoiseGain;
    public float m_fAttackDecayTimeInSec;
    public int m_nFrequencySmoothing;
    private IContainer components;
    private Button buttonCancel;
    private Button buttonOK;
    private TextBox textBoxNoiseGain;
    private TextBox textBoxWindowSize;
    private Label label2;
    private Label label1;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private TextBox textBoxFrequencySmoothing;
    private TextBox textBoxAttackDecayTime;
    private Label label7;
    private Label label8;
    private ComboBox comboBoxWindowType;
    private Label label9;

    public FormDeHiss()
    {
      this.InitializeComponent();
    }

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    private void FormDeHiss_Load(object sender, EventArgs e)
    {
      this.comboBoxWindowType.SelectedIndex = 4;
      FormDeHiss.SetWindowLong(this.textBoxWindowSize.Handle, -16, FormDeHiss.GetWindowLong(this.textBoxWindowSize.Handle, -16) | 8192);
      FormDeHiss.SetWindowLong(this.textBoxAttackDecayTime.Handle, -16, FormDeHiss.GetWindowLong(this.textBoxAttackDecayTime.Handle, -16) | 8192);
      FormDeHiss.SetWindowLong(this.textBoxFrequencySmoothing.Handle, -16, FormDeHiss.GetWindowLong(this.textBoxFrequencySmoothing.Handle, -16) | 8192);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nWindowType = this.comboBoxWindowType.SelectedIndex;
      this.m_nWindowSize = Convert.ToInt32(this.textBoxWindowSize.Text);
      if (this.m_nWindowSize < 1)
      {
        this.textBoxWindowSize.Focus();
        this.textBoxWindowSize.SelectAll();
        int num = (int) MessageBox.Show("Please, enter a positive numerical value");
      }
      else
      {
        try
        {
          this.m_nNoiseGain = Convert.ToInt32(this.textBoxNoiseGain.Text);
        }
        catch
        {
          this.textBoxNoiseGain.Focus();
          this.textBoxNoiseGain.SelectAll();
          int num = (int) MessageBox.Show("Please, enter a value between 0 and -100");
          return;
        }
        if (this.m_nNoiseGain < -100 || this.m_nNoiseGain > 0)
        {
          this.textBoxNoiseGain.Focus();
          this.textBoxNoiseGain.SelectAll();
          int num = (int) MessageBox.Show("Please, enter a value between 0 and -100");
        }
        else
        {
          int int32 = Convert.ToInt32(this.textBoxAttackDecayTime.Text);
          if (int32 < 0)
          {
            this.textBoxAttackDecayTime.Focus();
            this.textBoxAttackDecayTime.SelectAll();
            int num = (int) MessageBox.Show("Please, enter a positive numerical value");
          }
          else
          {
            this.m_fAttackDecayTimeInSec = (float) int32 / 1000f;
            this.m_nFrequencySmoothing = Convert.ToInt32(this.textBoxFrequencySmoothing.Text);
            if (this.m_nFrequencySmoothing < 0)
            {
              this.textBoxFrequencySmoothing.Focus();
              this.textBoxFrequencySmoothing.SelectAll();
              int num = (int) MessageBox.Show("Please, enter a positive numerical value");
            }
            else
            {
              this.m_bCancel = false;
              this.Close();
            }
          }
        }
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
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
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.textBoxNoiseGain = new TextBox();
      this.textBoxWindowSize = new TextBox();
      this.label2 = new Label();
      this.label1 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.textBoxFrequencySmoothing = new TextBox();
      this.textBoxAttackDecayTime = new TextBox();
      this.label7 = new Label();
      this.label8 = new Label();
      this.comboBoxWindowType = new ComboBox();
      this.label9 = new Label();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(184, 216);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 17;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(75, 216);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 16;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.textBoxNoiseGain.Location = new Point(186, 94);
      this.textBoxNoiseGain.Name = "textBoxNoiseGain";
      this.textBoxNoiseGain.Size = new Size(74, 20);
      this.textBoxNoiseGain.TabIndex = 23;
      this.textBoxNoiseGain.Text = "-40";
      this.textBoxWindowSize.Location = new Point(186, 59);
      this.textBoxWindowSize.Name = "textBoxWindowSize";
      this.textBoxWindowSize.Size = new Size(74, 20);
      this.textBoxWindowSize.TabIndex = 22;
      this.textBoxWindowSize.Text = "1024";
      this.label2.Location = new Point(51, 97);
      this.label2.Name = "label2";
      this.label2.Size = new Size(115, 13);
      this.label2.TabIndex = 21;
      this.label2.Text = "Noise gain";
      this.label2.TextAlign = ContentAlignment.MiddleRight;
      this.label1.Location = new Point(80, 62);
      this.label1.Name = "label1";
      this.label1.Size = new Size(86, 13);
      this.label1.TabIndex = 20;
      this.label1.Text = "Window size";
      this.label1.TextAlign = ContentAlignment.MiddleRight;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(268, 61);
      this.label3.Name = "label3";
      this.label3.Size = new Size(45, 13);
      this.label3.TabIndex = 25;
      this.label3.Text = "samples";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(266, 97);
      this.label4.Name = "label4";
      this.label4.Size = new Size(20, 13);
      this.label4.TabIndex = 26;
      this.label4.Text = "dB";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(266, 168);
      this.label5.Name = "label5";
      this.label5.Size = new Size(20, 13);
      this.label5.TabIndex = 32;
      this.label5.Text = "Hz";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(268, 132);
      this.label6.Name = "label6";
      this.label6.Size = new Size(20, 13);
      this.label6.TabIndex = 31;
      this.label6.Text = "ms";
      this.textBoxFrequencySmoothing.Location = new Point(186, 165);
      this.textBoxFrequencySmoothing.Name = "textBoxFrequencySmoothing";
      this.textBoxFrequencySmoothing.Size = new Size(74, 20);
      this.textBoxFrequencySmoothing.TabIndex = 30;
      this.textBoxFrequencySmoothing.Text = "370";
      this.textBoxAttackDecayTime.Location = new Point(186, 130);
      this.textBoxAttackDecayTime.Name = "textBoxAttackDecayTime";
      this.textBoxAttackDecayTime.Size = new Size(74, 20);
      this.textBoxAttackDecayTime.TabIndex = 29;
      this.textBoxAttackDecayTime.Text = "200";
      this.label7.Location = new Point(51, 168);
      this.label7.Name = "label7";
      this.label7.Size = new Size(115, 13);
      this.label7.TabIndex = 28;
      this.label7.Text = "Frequency smoothing";
      this.label7.TextAlign = ContentAlignment.MiddleRight;
      this.label8.Location = new Point(25, 133);
      this.label8.Name = "label8";
      this.label8.Size = new Size(141, 13);
      this.label8.TabIndex = 27;
      this.label8.Text = "Attack/Decay time";
      this.label8.TextAlign = ContentAlignment.MiddleRight;
      this.comboBoxWindowType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxWindowType.FormattingEnabled = true;
      this.comboBoxWindowType.Items.AddRange(new object[7]
      {
        (object) "BARTLETT",
        (object) "BLACKMAN",
        (object) "GAUSSIAN",
        (object) "HAMMING",
        (object) "HANNING",
        (object) "HARRIS",
        (object) "WELCH"
      });
      this.comboBoxWindowType.Location = new Point(186, 23);
      this.comboBoxWindowType.Name = "comboBoxWindowType";
      this.comboBoxWindowType.Size = new Size(122, 21);
      this.comboBoxWindowType.TabIndex = 33;
      this.label9.Location = new Point(51, 26);
      this.label9.Name = "label9";
      this.label9.Size = new Size(115, 13);
      this.label9.TabIndex = 34;
      this.label9.Text = "Window type";
      this.label9.TextAlign = ContentAlignment.MiddleRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(363, (int) byte.MaxValue);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.comboBoxWindowType);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.textBoxFrequencySmoothing);
      this.Controls.Add((Control) this.textBoxAttackDecayTime);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.textBoxNoiseGain);
      this.Controls.Add((Control) this.textBoxWindowSize);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormDeHiss);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Remove hiss noise";
      this.Load += new EventHandler(this.FormDeHiss_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
