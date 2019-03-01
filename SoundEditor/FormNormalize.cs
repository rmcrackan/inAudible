// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormNormalize
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SoundEditor
{
  public class FormNormalize : Form
  {
    private TextBox textBoxNormalizeTarget;
    private Label label2;
    private Button buttonCancel;
    private Button buttonOK;
    private Label label1;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private TextBox textBoxNormalizeBelow;
    private TextBox textBoxNormalizeAbove;
    private CheckBox checkBoxDcOffsetRemoval;
    private Container components;
    public bool m_bCancel;
    public float m_fLevelTarget;
    public float m_fLevelBelow;
    public float m_fLevelAbove;
    public bool m_bRemoveDCOffset;

    public FormNormalize()
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
      this.textBoxNormalizeTarget = new TextBox();
      this.label2 = new Label();
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.label1 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.textBoxNormalizeBelow = new TextBox();
      this.label5 = new Label();
      this.label6 = new Label();
      this.textBoxNormalizeAbove = new TextBox();
      this.label7 = new Label();
      this.checkBoxDcOffsetRemoval = new CheckBox();
      this.SuspendLayout();
      this.textBoxNormalizeTarget.Location = new Point(152, 23);
      this.textBoxNormalizeTarget.Name = "textBoxNormalizeTarget";
      this.textBoxNormalizeTarget.Size = new Size(48, 20);
      this.textBoxNormalizeTarget.TabIndex = 15;
      this.textBoxNormalizeTarget.Text = "98";
      this.label2.Location = new Point(48, 24);
      this.label2.Name = "label2";
      this.label2.Size = new Size(88, 16);
      this.label2.TabIndex = 14;
      this.label2.Text = "Normalize to";
      this.label2.TextAlign = ContentAlignment.MiddleLeft;
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(125, 197);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 13;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(19, 197);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 12;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.label1.Location = new Point(200, 25);
      this.label1.Name = "label1";
      this.label1.Size = new Size(16, 16);
      this.label1.TabIndex = 16;
      this.label1.Text = "%";
      this.label1.TextAlign = ContentAlignment.MiddleLeft;
      this.label3.Location = new Point(48, 64);
      this.label3.Name = "label3";
      this.label3.Size = new Size(152, 16);
      this.label3.TabIndex = 17;
      this.label3.Text = "only if Peak Level is";
      this.label4.Location = new Point(200, 89);
      this.label4.Name = "label4";
      this.label4.Size = new Size(16, 16);
      this.label4.TabIndex = 20;
      this.label4.Text = "%";
      this.label4.TextAlign = ContentAlignment.MiddleLeft;
      this.textBoxNormalizeBelow.Location = new Point(152, 87);
      this.textBoxNormalizeBelow.Name = "textBoxNormalizeBelow";
      this.textBoxNormalizeBelow.Size = new Size(48, 20);
      this.textBoxNormalizeBelow.TabIndex = 19;
      this.textBoxNormalizeBelow.Text = "85";
      this.label5.Location = new Point(80, 88);
      this.label5.Name = "label5";
      this.label5.Size = new Size(48, 16);
      this.label5.TabIndex = 18;
      this.label5.Text = "below";
      this.label5.TextAlign = ContentAlignment.MiddleLeft;
      this.label6.Location = new Point(200, 121);
      this.label6.Name = "label6";
      this.label6.Size = new Size(16, 16);
      this.label6.TabIndex = 23;
      this.label6.Text = "%";
      this.label6.TextAlign = ContentAlignment.MiddleLeft;
      this.textBoxNormalizeAbove.Location = new Point(152, 119);
      this.textBoxNormalizeAbove.Name = "textBoxNormalizeAbove";
      this.textBoxNormalizeAbove.Size = new Size(48, 20);
      this.textBoxNormalizeAbove.TabIndex = 22;
      this.textBoxNormalizeAbove.Text = "99";
      this.label7.Location = new Point(80, 120);
      this.label7.Name = "label7";
      this.label7.Size = new Size(56, 16);
      this.label7.TabIndex = 21;
      this.label7.Text = "or above";
      this.label7.TextAlign = ContentAlignment.MiddleLeft;
      this.checkBoxDcOffsetRemoval.AutoSize = true;
      this.checkBoxDcOffsetRemoval.Location = new Point(19, 152);
      this.checkBoxDcOffsetRemoval.Name = "checkBoxDcOffsetRemoval";
      this.checkBoxDcOffsetRemoval.Size = new Size(115, 17);
      this.checkBoxDcOffsetRemoval.TabIndex = 24;
      this.checkBoxDcOffsetRemoval.Text = "Remove DC Offset";
      this.checkBoxDcOffsetRemoval.UseVisualStyleBackColor = true;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(248, 233);
      this.ControlBox = false;
      this.Controls.Add((Control) this.checkBoxDcOffsetRemoval);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.textBoxNormalizeAbove);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.textBoxNormalizeBelow);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.textBoxNormalizeTarget);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormNormalize);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Normalize to target level";
      this.Load += new EventHandler(this.FormNormalize_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_fLevelTarget = Convert.ToSingle(this.textBoxNormalizeTarget.Text);
      this.m_fLevelBelow = Convert.ToSingle(this.textBoxNormalizeBelow.Text);
      this.m_fLevelAbove = Convert.ToSingle(this.textBoxNormalizeAbove.Text);
      this.m_bRemoveDCOffset = this.checkBoxDcOffsetRemoval.Checked;
      this.m_bCancel = false;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bCancel = true;
      this.Close();
    }

    private void FormNormalize_Load(object sender, EventArgs e)
    {
    }
  }
}
