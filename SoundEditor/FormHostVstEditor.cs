// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormHostVstEditor
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
  public class FormHostVstEditor : Form
  {
    private Button buttonApply;
    private Button buttonCancel;
    private Button buttonHide;
    private Label labelVstEditorPosition;
    private Label labelEffectName;
    private Container components;
    internal AudioSoundEditor.AudioSoundEditor audioSoundEditor1;
    public int m_idVst;
    private ComboBox comboBoxVstPrograms;
    private Label label3;
    public bool m_bCancel;

    public FormHostVstEditor()
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
      this.buttonApply = new Button();
      this.buttonCancel = new Button();
      this.buttonHide = new Button();
      this.labelEffectName = new Label();
      this.labelVstEditorPosition = new Label();
      this.comboBoxVstPrograms = new ComboBox();
      this.label3 = new Label();
      this.SuspendLayout();
      this.buttonApply.Location = new Point(16, 8);
      this.buttonApply.Name = "buttonApply";
      this.buttonApply.Size = new Size(136, 40);
      this.buttonApply.TabIndex = 0;
      this.buttonApply.Text = "Apply VST";
      this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
      this.buttonCancel.Location = new Point(168, 8);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(128, 40);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      this.buttonHide.Location = new Point(376, 8);
      this.buttonHide.Name = "buttonHide";
      this.buttonHide.Size = new Size(160, 40);
      this.buttonHide.TabIndex = 2;
      this.buttonHide.Text = "Hide VST's User Interface";
      this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
      this.labelEffectName.Location = new Point(16, 64);
      this.labelEffectName.Name = "labelEffectName";
      this.labelEffectName.Size = new Size(352, 16);
      this.labelEffectName.TabIndex = 3;
      this.labelEffectName.Text = "- ";
      this.labelVstEditorPosition.Location = new Point(8, 104);
      this.labelVstEditorPosition.Name = "labelVstEditorPosition";
      this.labelVstEditorPosition.Size = new Size(360, 32);
      this.labelVstEditorPosition.TabIndex = 4;
      this.labelVstEditorPosition.Text = "Reference label for VST's editor placement";
      this.labelVstEditorPosition.Visible = false;
      this.comboBoxVstPrograms.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxVstPrograms.Location = new Point(376, 72);
      this.comboBoxVstPrograms.Name = "comboBoxVstPrograms";
      this.comboBoxVstPrograms.Size = new Size(192, 21);
      this.comboBoxVstPrograms.TabIndex = 52;
      this.comboBoxVstPrograms.SelectedIndexChanged += new System.EventHandler(this.comboBoxVstPrograms_SelectedIndexChanged);
      this.label3.Location = new Point(376, 56);
      this.label3.Name = "label3";
      this.label3.Size = new Size(192, 16);
      this.label3.TabIndex = 51;
      this.label3.Text = "Choose a VST effect program:";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(779, 532);
      this.ControlBox = false;
      this.Controls.Add((Control) this.comboBoxVstPrograms);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.labelVstEditorPosition);
      this.Controls.Add((Control) this.labelEffectName);
      this.Controls.Add((Control) this.buttonHide);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonApply);
      this.Name = nameof (FormHostVstEditor);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "VST editor";
      this.Load += new System.EventHandler(this.FormHostVstEditor_Load);
      this.ResumeLayout(false);
    }

    private void FormHostVstEditor_Load(object sender, EventArgs e)
    {
      string infoString1 = this.audioSoundEditor1.Effects.VstGetInfoString(this.m_idVst, enumVstInfo.VST_INFO_EFFECT_NAME);
      string infoString2 = this.audioSoundEditor1.Effects.VstGetInfoString(this.m_idVst, enumVstInfo.VST_INFO_VENDOR_NAME);
      VstEffectInfo Info1 = new VstEffectInfo();
      int info1 = (int) this.audioSoundEditor1.Effects.VstGetInfo(this.m_idVst, ref Info1);
      string str = Info1.nVersion.ToString();
      this.labelEffectName.Text = infoString1;
      this.labelEffectName.Text += " ver. ";
      this.labelEffectName.Text += str;
      this.labelEffectName.Text += " developed by ";
      this.labelEffectName.Text += infoString2;
      short count = this.audioSoundEditor1.Effects.VstProgramsGetCount(this.m_idVst);
      for (short nProgramIndex = 0; (int) nProgramIndex < (int) count; ++nProgramIndex)
        this.comboBoxVstPrograms.Items.Add((object) this.audioSoundEditor1.Effects.VstProgramNameGet(this.m_idVst, nProgramIndex));
      this.comboBoxVstPrograms.SelectedIndex = 0;
      VstEditorInfo Info2 = new VstEditorInfo();
      int info2 = (int) this.audioSoundEditor1.Effects.VstEditorGetInfo(this.m_idVst, ref Info2);
      if ((int) Info2.nEditorWidth > this.ClientRectangle.Width)
      {
        int num = this.Width - this.ClientRectangle.Width;
        this.Width = this.labelVstEditorPosition.Location.X * 3 + (int) Info2.nEditorWidth + num;
      }
      if ((int) Info2.nEditorHeight > this.ClientRectangle.Height - this.labelVstEditorPosition.Location.Y)
      {
        int num = this.Height - this.ClientRectangle.Height;
        this.Height = this.labelVstEditorPosition.Location.Y + (int) Info2.nEditorHeight + num + 10;
      }
      int num1 = (int) this.audioSoundEditor1.Effects.VstEditorShow(this.m_idVst, true, this.Handle, this.labelVstEditorPosition.Left, this.labelVstEditorPosition.Top);
      this.buttonHide.Text = "Hide VST's User Interface";
    }

    private void buttonApply_Click(object sender, EventArgs e)
    {
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void buttonHide_Click(object sender, EventArgs e)
    {
      VstEditorInfo Info = new VstEditorInfo();
      int info = (int) this.audioSoundEditor1.Effects.VstEditorGetInfo(this.m_idVst, ref Info);
      if (!Info.bIsEditorVisible)
      {
        int num = (int) this.audioSoundEditor1.Effects.VstEditorShow(this.m_idVst, true, this.Handle, this.labelVstEditorPosition.Left, this.labelVstEditorPosition.Top);
        this.buttonHide.Text = "Hide VST's User Interface";
      }
      else
      {
        int num = (int) this.audioSoundEditor1.Effects.VstEditorShow(this.m_idVst, false, this.Handle, this.labelVstEditorPosition.Left, this.labelVstEditorPosition.Top);
        this.buttonHide.Text = "Display VST's User Interface";
      }
    }

    private void comboBoxVstPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
      int num = (int) this.audioSoundEditor1.Effects.VstProgramSetCurrent(this.m_idVst, (short) this.comboBoxVstPrograms.SelectedIndex);
    }
  }
}
