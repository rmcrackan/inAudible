// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormACMCodecs
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormACMCodecs : Form
  {
    private ComboBox comboCodecFormats;
    private ComboBox comboCodecs;
    private Label label1;
    private Label label2;
    private Button buttonOK;
    private Button buttonCancel;
    private Container components;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public short m_ACMCodec;
    public short m_ACMCodecFormat;

    public FormACMCodecs()
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
      this.comboCodecFormats = new ComboBox();
      this.comboCodecs = new ComboBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.SuspendLayout();
      this.comboCodecFormats.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboCodecFormats.Location = new Point(32, 100);
      this.comboCodecFormats.Name = "comboCodecFormats";
      this.comboCodecFormats.Size = new Size(224, 21);
      this.comboCodecFormats.TabIndex = 7;
      this.comboCodecs.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboCodecs.Location = new Point(32, 44);
      this.comboCodecs.Name = "comboCodecs";
      this.comboCodecs.Size = new Size(224, 21);
      this.comboCodecs.TabIndex = 6;
      this.comboCodecs.SelectedIndexChanged += new System.EventHandler(this.comboCodecs_SelectedIndexChanged);
      this.label1.Location = new Point(32, 76);
      this.label1.Name = "label1";
      this.label1.Size = new Size(216, 24);
      this.label1.TabIndex = 5;
      this.label1.Text = "Audio format";
      this.label2.Location = new Point(32, 20);
      this.label2.Name = "label2";
      this.label2.Size = new Size(216, 24);
      this.label2.TabIndex = 4;
      this.label2.Text = "Audio format tag (Codec)";
      this.buttonOK.Location = new Point(40, 140);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 36);
      this.buttonOK.TabIndex = 8;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.buttonCancel.Location = new Point(152, 140);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(88, 36);
      this.buttonCancel.TabIndex = 9;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(288, 197);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.comboCodecFormats);
      this.Controls.Add((Control) this.comboCodecs);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.label2);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormACMCodecs);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Choose an encoder and a format";
      this.Load += new System.EventHandler(this.FormACMCodecs_Load);
      this.ResumeLayout(false);
    }

    private void UpdateCodecFormatsCombo(short nCodec)
    {
      this.comboCodecFormats.Items.Clear();
      int codecFormatsCount = this.audioSoundEditor1.EncodeFormats.ACM.GetCodecFormatsCount(nCodec);
      for (short nCodecFormatIndex = 0; (int) nCodecFormatIndex < codecFormatsCount; ++nCodecFormatIndex)
        this.comboCodecFormats.Items.Add((object) this.audioSoundEditor1.EncodeFormats.ACM.GetCodecFormatDesc(nCodec, nCodecFormatIndex));
      this.comboCodecFormats.SelectedIndex = 0;
    }

    private void FormACMCodecs_Load(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.EncodeFormats.ACM.InitCodecs();
      int codecsCount = this.audioSoundEditor1.EncodeFormats.ACM.GetCodecsCount();
      for (short nCodecIndex = 0; (int) nCodecIndex < codecsCount; ++nCodecIndex)
        this.comboCodecs.Items.Add((object) this.audioSoundEditor1.EncodeFormats.ACM.GetCodecDesc(nCodecIndex));
      this.comboCodecs.SelectedIndex = 0;
      this.UpdateCodecFormatsCombo((short) this.comboCodecs.SelectedIndex);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_ACMCodec = (short) this.comboCodecs.SelectedIndex;
      this.m_ACMCodecFormat = (short) this.comboCodecFormats.SelectedIndex;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void comboCodecs_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.UpdateCodecFormatsCombo((short) this.comboCodecs.SelectedIndex);
    }
  }
}
