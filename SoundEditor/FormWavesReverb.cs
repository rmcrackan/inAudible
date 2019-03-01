// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormWavesReverb
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormWavesReverb : Form
  {
    private const int DSFX_WAVESREVERB_INGAIN_MAX = 0;
    private const int DSFX_WAVESREVERB_INGAIN_MIN = -96;
    private const int DSFX_WAVESREVERB_REVERBMIX_MAX = 0;
    private const int DSFX_WAVESREVERB_REVERBMIX_MIN = -96;
    private const float DSFX_WAVESREVERB_HIGHFREQRTRATIO_MAX = 0.999f;
    private const float DSFX_WAVESREVERB_HIGHFREQRTRATIO_MIN = 0.001f;
    private const int DSFX_WAVESREVERB_REVERBTIME_MAX = 3000;
    private const float DSFX_WAVESREVERB_REVERBTIME_MIN = 0.001f;
    private Button buttonCancel;
    private Button buttonOK;
    private Label label2;
    private Label label1;
    private TextBox textBoxInputGain;
    private TextBox textBoxReverbMix;
    private Label label3;
    private Label label4;
    private TextBox textBoxReverbTime;
    private TextBox textBoxHighFreqRTRatio;
    private Container components;
    public float m_fInGain;
    public float m_fReverbMix;
    public float m_fReverbTime;
    public float m_fHighFreqRTRatio;
    public bool m_bCancel;

    public FormWavesReverb()
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
      this.textBoxReverbTime = new TextBox();
      this.textBoxInputGain = new TextBox();
      this.label2 = new Label();
      this.label1 = new Label();
      this.textBoxHighFreqRTRatio = new TextBox();
      this.textBoxReverbMix = new TextBox();
      this.label3 = new Label();
      this.label4 = new Label();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(200, 144);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 12;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(72, 144);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 11;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.textBoxReverbTime.Location = new Point(24, 96);
      this.textBoxReverbTime.Name = "textBoxReverbTime";
      this.textBoxReverbTime.Size = new Size(88, 20);
      this.textBoxReverbTime.TabIndex = 10;
      this.textBoxReverbTime.Text = "";
      this.textBoxReverbTime.KeyPress += new KeyPressEventHandler(this.textBoxReverbTime_KeyPress);
      this.textBoxInputGain.Location = new Point(24, 40);
      this.textBoxInputGain.Name = "textBoxInputGain";
      this.textBoxInputGain.Size = new Size(88, 20);
      this.textBoxInputGain.TabIndex = 9;
      this.textBoxInputGain.Text = "";
      this.textBoxInputGain.KeyPress += new KeyPressEventHandler(this.textBoxInputGain_KeyPress);
      this.label2.Location = new Point(24, 72);
      this.label2.Name = "label2";
      this.label2.Size = new Size(160, 16);
      this.label2.TabIndex = 8;
      this.label2.Text = "Reverb time (expressed in ms)";
      this.label1.Location = new Point(24, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(168, 16);
      this.label1.TabIndex = 7;
      this.label1.Text = "Input gain (expressed in dB)";
      this.textBoxHighFreqRTRatio.Location = new Point(208, 96);
      this.textBoxHighFreqRTRatio.Name = "textBoxHighFreqRTRatio";
      this.textBoxHighFreqRTRatio.Size = new Size(88, 20);
      this.textBoxHighFreqRTRatio.TabIndex = 16;
      this.textBoxHighFreqRTRatio.Text = "";
      this.textBoxHighFreqRTRatio.KeyPress += new KeyPressEventHandler(this.textBoxHighFreqRTRatio_KeyPress);
      this.textBoxReverbMix.Location = new Point(208, 40);
      this.textBoxReverbMix.Name = "textBoxReverbMix";
      this.textBoxReverbMix.Size = new Size(88, 20);
      this.textBoxReverbMix.TabIndex = 15;
      this.textBoxReverbMix.Text = "";
      this.textBoxReverbMix.KeyPress += new KeyPressEventHandler(this.textBoxReverbMix_KeyPress);
      this.label3.Location = new Point(208, 72);
      this.label3.Name = "label3";
      this.label3.Size = new Size(160, 16);
      this.label3.TabIndex = 14;
      this.label3.Text = "High-frequency reverb time ratio";
      this.label4.Location = new Point(208, 16);
      this.label4.Name = "label4";
      this.label4.Size = new Size(168, 16);
      this.label4.TabIndex = 13;
      this.label4.Text = "Reverb mix (expressed in dB)";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(376, 182);
      this.ControlBox = false;
      this.Controls.Add((Control) this.textBoxHighFreqRTRatio);
      this.Controls.Add((Control) this.textBoxReverbMix);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.textBoxReverbTime);
      this.Controls.Add((Control) this.textBoxInputGain);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.KeyPreview = true;
      this.Name = nameof (FormWavesReverb);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Waves Reverb DMO settings";
      this.Load += new EventHandler(this.FormWavesReverb_Load);
      this.ResumeLayout(false);
    }

    private void FormWavesReverb_Load(object sender, EventArgs e)
    {
      this.textBoxInputGain.Text = Convert.ToString(0);
      this.textBoxReverbMix.Text = Convert.ToString(0);
      this.textBoxReverbTime.Text = Convert.ToString(1000);
      this.textBoxHighFreqRTRatio.Text = Convert.ToString(0.001);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_fInGain = Convert.ToSingle(this.textBoxInputGain.Text);
      this.m_fReverbMix = Convert.ToSingle(this.textBoxReverbMix.Text);
      this.m_fReverbTime = Convert.ToSingle(this.textBoxReverbTime.Text);
      this.m_fHighFreqRTRatio = Convert.ToSingle(this.textBoxHighFreqRTRatio.Text);
      if ((double) this.m_fInGain < -96.0 || (double) this.m_fInGain > 0.0)
      {
        this.textBoxInputGain.Focus();
        this.textBoxInputGain.SelectionStart = 0;
        this.textBoxInputGain.SelectionLength = 100;
        int num = (int) MessageBox.Show("Selected value is out of range");
      }
      else if ((double) this.m_fReverbMix < -96.0 || (double) this.m_fReverbMix > 0.0)
      {
        this.textBoxReverbMix.Focus();
        this.textBoxReverbMix.SelectionStart = 0;
        this.textBoxReverbMix.SelectionLength = 100;
        int num = (int) MessageBox.Show("Selected value is out of range");
      }
      else if ((double) this.m_fReverbTime < 1.0 / 1000.0 || (double) this.m_fReverbTime > 3000.0)
      {
        this.textBoxReverbTime.Focus();
        this.textBoxReverbTime.SelectionStart = 0;
        this.textBoxReverbTime.SelectionLength = 100;
        int num = (int) MessageBox.Show("Selected value is out of range");
      }
      else if ((double) this.m_fHighFreqRTRatio < 1.0 / 1000.0 || (double) this.m_fHighFreqRTRatio > 0.999000012874603)
      {
        this.textBoxHighFreqRTRatio.Focus();
        this.textBoxHighFreqRTRatio.SelectionStart = 0;
        this.textBoxHighFreqRTRatio.SelectionLength = 100;
        int num = (int) MessageBox.Show("Selected value is out of range");
      }
      else
      {
        this.m_bCancel = false;
        this.Close();
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void textBoxInputGain_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormMain.CheckKeyPress(this.textBoxInputGain, Convert.ToInt32(e.KeyChar));
    }

    private void textBoxReverbTime_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormMain.CheckKeyPress(this.textBoxReverbTime, Convert.ToInt32(e.KeyChar));
    }

    private void textBoxReverbMix_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormMain.CheckKeyPress(this.textBoxReverbMix, Convert.ToInt32(e.KeyChar));
    }

    private void textBoxHighFreqRTRatio_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormMain.CheckKeyPress(this.textBoxHighFreqRTRatio, Convert.ToInt32(e.KeyChar));
    }
  }
}
