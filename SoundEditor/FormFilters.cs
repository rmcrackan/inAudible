// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormFilters
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
  public class FormFilters : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    private Button buttonCancel;
    private Button buttonOK;
    private Container components;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private RadioButton radioButtonBessel;
    private RadioButton radioButtonButterworth;
    private RadioButton radioButtonHamming;
    private RadioButton radioButtonHanning;
    private RadioButton radioButtonBlackman;
    private RadioButton radioButtonRectangular;
    private RadioButton radioButtonKaiser;
    private TextBox textBoxFrequency1;
    private TextBox textBoxFrequency2;
    private TextBox textBoxGain;
    private TextBox textBoxCutoffWidth;
    public bool m_bCancel;
    public enumFilterNames m_nFilterName;
    public enumFilterTypes m_nFilterType;
    public float m_fFrequency1;
    public float m_fFrequency2;
    public float m_fGain;
    public float m_fCutoffWidth;

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    public FormFilters()
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
      this.groupBox1 = new GroupBox();
      this.groupBox2 = new GroupBox();
      this.groupBox3 = new GroupBox();
      this.radioButtonBessel = new RadioButton();
      this.radioButtonButterworth = new RadioButton();
      this.radioButtonHamming = new RadioButton();
      this.radioButtonHanning = new RadioButton();
      this.radioButtonBlackman = new RadioButton();
      this.radioButtonRectangular = new RadioButton();
      this.radioButtonKaiser = new RadioButton();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.textBoxFrequency1 = new TextBox();
      this.textBoxFrequency2 = new TextBox();
      this.textBoxGain = new TextBox();
      this.textBoxCutoffWidth = new TextBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(204, 248);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 16;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(76, 248);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 15;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.groupBox1.Controls.Add((Control) this.radioButtonKaiser);
      this.groupBox1.Controls.Add((Control) this.radioButtonRectangular);
      this.groupBox1.Controls.Add((Control) this.radioButtonBlackman);
      this.groupBox1.Controls.Add((Control) this.radioButtonHanning);
      this.groupBox1.Controls.Add((Control) this.radioButtonHamming);
      this.groupBox1.Controls.Add((Control) this.radioButtonButterworth);
      this.groupBox1.Controls.Add((Control) this.radioButtonBessel);
      this.groupBox1.Location = new Point(8, 16);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(168, 216);
      this.groupBox1.TabIndex = 17;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Filter name";
      this.groupBox2.Controls.Add((Control) this.textBoxFrequency2);
      this.groupBox2.Controls.Add((Control) this.textBoxFrequency1);
      this.groupBox2.Controls.Add((Control) this.label2);
      this.groupBox2.Controls.Add((Control) this.label1);
      this.groupBox2.Location = new Point(184, 16);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(192, 96);
      this.groupBox2.TabIndex = 18;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Cut-off frequencies";
      this.groupBox3.Controls.Add((Control) this.textBoxCutoffWidth);
      this.groupBox3.Controls.Add((Control) this.textBoxGain);
      this.groupBox3.Controls.Add((Control) this.label4);
      this.groupBox3.Controls.Add((Control) this.label3);
      this.groupBox3.Location = new Point(184, 136);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(192, 96);
      this.groupBox3.TabIndex = 19;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Settings for FIR filters";
      this.radioButtonBessel.Location = new Point(16, 32);
      this.radioButtonBessel.Name = "radioButtonBessel";
      this.radioButtonBessel.Size = new Size(128, 16);
      this.radioButtonBessel.TabIndex = 0;
      this.radioButtonBessel.Text = "Bessel (IIR)";
      this.radioButtonBessel.Click += new System.EventHandler(this.radioButtonBessel_Click);
      this.radioButtonButterworth.Location = new Point(16, 57);
      this.radioButtonButterworth.Name = "radioButtonButterworth";
      this.radioButtonButterworth.Size = new Size(128, 16);
      this.radioButtonButterworth.TabIndex = 1;
      this.radioButtonButterworth.Text = "Butterworth (IIR)";
      this.radioButtonButterworth.Click += new System.EventHandler(this.radioButtonButterworth_Click);
      this.radioButtonHamming.Location = new Point(16, 82);
      this.radioButtonHamming.Name = "radioButtonHamming";
      this.radioButtonHamming.Size = new Size(128, 16);
      this.radioButtonHamming.TabIndex = 2;
      this.radioButtonHamming.Text = "Hamming (FIR)";
      this.radioButtonHamming.Click += new System.EventHandler(this.radioButtonHamming_Click);
      this.radioButtonHanning.Location = new Point(16, 107);
      this.radioButtonHanning.Name = "radioButtonHanning";
      this.radioButtonHanning.Size = new Size(128, 16);
      this.radioButtonHanning.TabIndex = 3;
      this.radioButtonHanning.Text = "Hanning (FIR)";
      this.radioButtonHanning.Click += new System.EventHandler(this.radioButtonHanning_Click);
      this.radioButtonBlackman.Location = new Point(16, 132);
      this.radioButtonBlackman.Name = "radioButtonBlackman";
      this.radioButtonBlackman.Size = new Size(128, 16);
      this.radioButtonBlackman.TabIndex = 4;
      this.radioButtonBlackman.Text = "Blackman (FIR)";
      this.radioButtonBlackman.Click += new System.EventHandler(this.radioButtonBlackman_Click);
      this.radioButtonRectangular.Location = new Point(16, 157);
      this.radioButtonRectangular.Name = "radioButtonRectangular";
      this.radioButtonRectangular.Size = new Size(128, 16);
      this.radioButtonRectangular.TabIndex = 5;
      this.radioButtonRectangular.Text = "Rectangular (FIR)";
      this.radioButtonRectangular.Click += new System.EventHandler(this.radioButtonRectangular_Click);
      this.radioButtonKaiser.Location = new Point(16, 182);
      this.radioButtonKaiser.Name = "radioButtonKaiser";
      this.radioButtonKaiser.Size = new Size(128, 16);
      this.radioButtonKaiser.TabIndex = 6;
      this.radioButtonKaiser.Text = "Kaiser (FIR)";
      this.radioButtonKaiser.Click += new System.EventHandler(this.radioButtonKaiser_Click);
      this.label1.Location = new Point(8, 26);
      this.label1.Name = "label1";
      this.label1.Size = new Size(104, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Frequency 1 (Hz)";
      this.label2.Location = new Point(8, 58);
      this.label2.Name = "label2";
      this.label2.Size = new Size(112, 22);
      this.label2.TabIndex = 1;
      this.label2.Text = "Frequency 2 (Hz)";
      this.label3.Location = new Point(8, 32);
      this.label3.Name = "label3";
      this.label3.Size = new Size(112, 16);
      this.label3.TabIndex = 1;
      this.label3.Text = "Gain (%)";
      this.label4.Location = new Point(8, 62);
      this.label4.Name = "label4";
      this.label4.Size = new Size(120, 24);
      this.label4.TabIndex = 2;
      this.label4.Text = "Cut-off transition width (ms)";
      this.textBoxFrequency1.Location = new Point(136, 24);
      this.textBoxFrequency1.Name = "textBoxFrequency1";
      this.textBoxFrequency1.Size = new Size(48, 20);
      this.textBoxFrequency1.TabIndex = 2;
      this.textBoxFrequency1.Text = "1000";
      this.textBoxFrequency2.Location = new Point(136, 56);
      this.textBoxFrequency2.Name = "textBoxFrequency2";
      this.textBoxFrequency2.Size = new Size(48, 20);
      this.textBoxFrequency2.TabIndex = 3;
      this.textBoxFrequency2.Text = "4000";
      this.textBoxGain.Location = new Point(136, 32);
      this.textBoxGain.Name = "textBoxGain";
      this.textBoxGain.Size = new Size(48, 20);
      this.textBoxGain.TabIndex = 3;
      this.textBoxGain.Text = "100";
      this.textBoxCutoffWidth.Location = new Point(136, 64);
      this.textBoxCutoffWidth.Name = "textBoxCutoffWidth";
      this.textBoxCutoffWidth.Size = new Size(48, 20);
      this.textBoxCutoffWidth.TabIndex = 4;
      this.textBoxCutoffWidth.Text = "1000";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(384, 286);
      this.ControlBox = false;
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormFilters);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Filter";
      this.Load += new System.EventHandler(this.FormFilters_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void FormFilters_Load(object sender, EventArgs e)
    {
      FormFilters.SetWindowLong(this.textBoxFrequency1.Handle, -16, FormFilters.GetWindowLong(this.textBoxFrequency1.Handle, -16) | 8192);
      FormFilters.SetWindowLong(this.textBoxFrequency2.Handle, -16, FormFilters.GetWindowLong(this.textBoxFrequency2.Handle, -16) | 8192);
      FormFilters.SetWindowLong(this.textBoxGain.Handle, -16, FormFilters.GetWindowLong(this.textBoxGain.Handle, -16) | 8192);
      FormFilters.SetWindowLong(this.textBoxCutoffWidth.Handle, -16, FormFilters.GetWindowLong(this.textBoxCutoffWidth.Handle, -16) | 8192);
      switch (this.m_nFilterType)
      {
        case enumFilterTypes.FILTER_TYPE_LOW_PASS:
          this.Text = "LOW-PASS Filter";
          this.textBoxFrequency2.Enabled = false;
          break;
        case enumFilterTypes.FILTER_TYPE_HIGH_PASS:
          this.Text = "HIGH-PASS Filter";
          this.textBoxFrequency2.Enabled = false;
          break;
        case enumFilterTypes.FILTER_TYPE_BAND_PASS:
          this.Text = "BAND-PASS Filter";
          this.textBoxFrequency2.Enabled = true;
          break;
        case enumFilterTypes.FILTER_TYPE_BAND_STOP:
          this.Text = "BAND-STOP Filter";
          this.textBoxFrequency2.Enabled = true;
          break;
      }
      this.radioButtonBessel.Checked = true;
      this.textBoxGain.Enabled = false;
      this.textBoxCutoffWidth.Enabled = false;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (this.radioButtonBessel.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_BESSEL;
      else if (this.radioButtonButterworth.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_BUTTERWORTH;
      else if (this.radioButtonHamming.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_HAMMING;
      else if (this.radioButtonHanning.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_HANNING;
      else if (this.radioButtonBlackman.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_BLACKMAN;
      else if (this.radioButtonRectangular.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_RECTANGULAR;
      else if (this.radioButtonKaiser.Checked)
        this.m_nFilterName = enumFilterNames.FILTER_NAME_KAISER;
      this.m_fFrequency1 = Convert.ToSingle(this.textBoxFrequency1.Text);
      this.m_fFrequency2 = Convert.ToSingle(this.textBoxFrequency2.Text);
      this.m_fGain = Convert.ToSingle(this.textBoxGain.Text);
      this.m_fCutoffWidth = Convert.ToSingle(this.textBoxCutoffWidth.Text);
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void radioButtonBessel_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = false;
      this.textBoxCutoffWidth.Enabled = false;
    }

    private void radioButtonButterworth_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = false;
      this.textBoxCutoffWidth.Enabled = false;
    }

    private void radioButtonHamming_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = true;
      this.textBoxCutoffWidth.Enabled = true;
    }

    private void radioButtonHanning_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = true;
      this.textBoxCutoffWidth.Enabled = true;
    }

    private void radioButtonBlackman_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = true;
      this.textBoxCutoffWidth.Enabled = true;
    }

    private void radioButtonRectangular_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = true;
      this.textBoxCutoffWidth.Enabled = true;
    }

    private void radioButtonKaiser_Click(object sender, EventArgs e)
    {
      this.textBoxGain.Enabled = true;
      this.textBoxCutoffWidth.Enabled = true;
    }
  }
}
