// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormOptions
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using AudioSoundEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormOptions : Form
  {
    private Button buttonOK;
    private Button buttonCancel;
    private GroupBox groupBox1;
    private RadioButton radioButtonMemoryBuffer;
    private RadioButton radioButtonTempFile;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    private GroupBox groupBox2;
    private ComboBox comboOutputDevices;
    private ComboBox comboBoxDownmix;
    private GroupBox groupBox3;
    private Container components;

    public FormOptions()
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
      this.buttonCancel = new Button();
      this.groupBox1 = new GroupBox();
      this.radioButtonTempFile = new RadioButton();
      this.radioButtonMemoryBuffer = new RadioButton();
      this.groupBox2 = new GroupBox();
      this.comboOutputDevices = new ComboBox();
      this.comboBoxDownmix = new ComboBox();
      this.groupBox3 = new GroupBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.buttonOK.Location = new Point(100, 301);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(88, 32);
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      this.buttonCancel.Location = new Point(212, 301);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(96, 32);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.groupBox1.Controls.Add((Control) this.radioButtonTempFile);
      this.groupBox1.Controls.Add((Control) this.radioButtonMemoryBuffer);
      this.groupBox1.Location = new Point(37, 24);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(334, 79);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Sound storage mode";
      this.radioButtonTempFile.Location = new Point(19, 48);
      this.radioButtonTempFile.Name = "radioButtonTempFile";
      this.radioButtonTempFile.Size = new Size(176, 18);
      this.radioButtonTempFile.TabIndex = 1;
      this.radioButtonTempFile.Text = "Inside temporary file";
      this.radioButtonMemoryBuffer.Location = new Point(19, 28);
      this.radioButtonMemoryBuffer.Name = "radioButtonMemoryBuffer";
      this.radioButtonMemoryBuffer.Size = new Size(184, 18);
      this.radioButtonMemoryBuffer.TabIndex = 0;
      this.radioButtonMemoryBuffer.Text = "Inside memory buffer";
      this.groupBox2.Controls.Add((Control) this.comboOutputDevices);
      this.groupBox2.Location = new Point(37, 109);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(334, 79);
      this.groupBox2.TabIndex = 3;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Playback devices";
      this.comboOutputDevices.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboOutputDevices.Location = new Point(19, 33);
      this.comboOutputDevices.Name = "comboOutputDevices";
      this.comboOutputDevices.Size = new Size(297, 21);
      this.comboOutputDevices.TabIndex = 36;
      this.comboBoxDownmix.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxDownmix.Location = new Point(19, 33);
      this.comboBoxDownmix.Name = "comboBoxDownmix";
      this.comboBoxDownmix.Size = new Size(297, 21);
      this.comboBoxDownmix.TabIndex = 36;
      this.groupBox3.Controls.Add((Control) this.comboBoxDownmix);
      this.groupBox3.Location = new Point(37, 197);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(334, 79);
      this.groupBox3.TabIndex = 37;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Multichannel downmix mode";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(408, 345);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormOptions);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Options";
      this.Load += new System.EventHandler(this.FormOptions_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void FormOptions_Load(object sender, EventArgs e)
    {
      switch (this.audioSoundEditor1.GetStoreMode())
      {
        case enumStoreModes.STORE_MODE_MEMORY_BUFFER:
          this.radioButtonMemoryBuffer.Checked = true;
          this.radioButtonTempFile.Checked = false;
          break;
        case enumStoreModes.STORE_MODE_TEMP_FILE:
          this.radioButtonTempFile.Checked = true;
          this.radioButtonMemoryBuffer.Checked = false;
          break;
      }
      int count = this.audioSoundEditor1.OutputDeviceGetCount();
      for (int nOutputIndex = 0; nOutputIndex < count; ++nOutputIndex)
        this.comboOutputDevices.Items.Add((object) this.audioSoundEditor1.OutputDeviceGetDesc(nOutputIndex));
      this.comboOutputDevices.SelectedIndex = (int) this.audioSoundEditor1.OutputDeviceGet();
      this.comboBoxDownmix.Items.Add((object) "Downmix to stereo");
      this.comboBoxDownmix.Items.Add((object) "Downmix to stereo and invert channels");
      this.comboBoxDownmix.Items.Add((object) "Downmix to stereo and merge channels");
      this.comboBoxDownmix.Items.Add((object) "Downmix to mono");
      this.comboBoxDownmix.Items.Add((object) "No downmix, keep multi-channel");
      enumMultiChanDownmixModes nMode = enumMultiChanDownmixModes.MULTICHAN_DOWNMIX_STEREO_SEPAR;
      int num = (int) this.audioSoundEditor1.MultiChannelLoadingModeGet(ref nMode);
      this.comboBoxDownmix.SelectedIndex = (int) nMode;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (this.radioButtonMemoryBuffer.Checked)
      {
        int num1 = (int) this.audioSoundEditor1.SetStoreMode(enumStoreModes.STORE_MODE_MEMORY_BUFFER);
      }
      else if (this.radioButtonTempFile.Checked)
      {
        int num2 = (int) this.audioSoundEditor1.SetStoreMode(enumStoreModes.STORE_MODE_TEMP_FILE);
      }
      int num3 = (int) this.audioSoundEditor1.OutputDeviceSet(this.comboOutputDevices.SelectedIndex);
      int num4 = (int) this.audioSoundEditor1.MultiChannelLoadingModeSet((enumMultiChanDownmixModes) this.comboBoxDownmix.SelectedIndex);
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
