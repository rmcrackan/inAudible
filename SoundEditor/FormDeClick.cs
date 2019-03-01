// Decompiled with JetBrains decompiler
// Type: SoundEditor.FormDeClick
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
  public class FormDeClick : Form
  {
    private const int GWL_STYLE = -16;
    private const int ES_NUMBER = 8192;
    public bool m_bCancel;
    public int m_nSensitivity;
    public int m_nGroupSensitivity;
    public bool m_bRecoverPitch;
    private IContainer components;
    private Button buttonCancel;
    private Button buttonOK;
    private Label label1;
    private Label label2;
    private TextBox textBoxSensitivity;
    private TextBox textBoxGroupSensitivity;
    private CheckBox checkBoxRecoverPitch;

    public FormDeClick()
    {
      this.InitializeComponent();
    }

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr window, int index, int value);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr window, int index);

    private void FormDeClick_Load(object sender, EventArgs e)
    {
      FormDeClick.SetWindowLong(this.textBoxSensitivity.Handle, -16, FormDeClick.GetWindowLong(this.textBoxSensitivity.Handle, -16) | 8192);
      FormDeClick.SetWindowLong(this.textBoxGroupSensitivity.Handle, -16, FormDeClick.GetWindowLong(this.textBoxGroupSensitivity.Handle, -16) | 8192);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_nSensitivity = Convert.ToInt32(this.textBoxSensitivity.Text);
      if (this.m_nSensitivity < 0 || this.m_nSensitivity > 100)
      {
        this.textBoxSensitivity.Focus();
        this.textBoxSensitivity.SelectAll();
        int num = (int) MessageBox.Show("Please, enter a value between 0 and 100");
      }
      else
      {
        this.m_nGroupSensitivity = Convert.ToInt32(this.textBoxGroupSensitivity.Text);
        if (this.m_nGroupSensitivity < 0 || this.m_nGroupSensitivity > 100)
        {
          this.textBoxGroupSensitivity.Focus();
          this.textBoxGroupSensitivity.SelectAll();
          int num = (int) MessageBox.Show("Please, enter a value between 0 and 100");
        }
        else
        {
          this.m_bRecoverPitch = this.checkBoxRecoverPitch.Checked;
          this.m_bCancel = false;
          this.Close();
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
      this.label1 = new Label();
      this.label2 = new Label();
      this.textBoxSensitivity = new TextBox();
      this.textBoxGroupSensitivity = new TextBox();
      this.checkBoxRecoverPitch = new CheckBox();
      this.SuspendLayout();
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(144, 153);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(104, 24);
      this.buttonCancel.TabIndex = 15;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Location = new Point(38, 153);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(96, 24);
      this.buttonOK.TabIndex = 14;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.label1.Location = new Point(47, 25);
      this.label1.Name = "label1";
      this.label1.Size = new Size(86, 13);
      this.label1.TabIndex = 16;
      this.label1.Text = "Sensitivity";
      this.label1.TextAlign = ContentAlignment.MiddleRight;
      this.label2.Location = new Point(18, 60);
      this.label2.Name = "label2";
      this.label2.Size = new Size(115, 13);
      this.label2.TabIndex = 17;
      this.label2.Text = "Group sensitivity";
      this.label2.TextAlign = ContentAlignment.MiddleRight;
      this.textBoxSensitivity.Location = new Point(153, 22);
      this.textBoxSensitivity.Name = "textBoxSensitivity";
      this.textBoxSensitivity.Size = new Size(74, 20);
      this.textBoxSensitivity.TabIndex = 18;
      this.textBoxSensitivity.Text = "99";
      this.textBoxGroupSensitivity.Location = new Point(153, 57);
      this.textBoxGroupSensitivity.Name = "textBoxGroupSensitivity";
      this.textBoxGroupSensitivity.Size = new Size(74, 20);
      this.textBoxGroupSensitivity.TabIndex = 19;
      this.textBoxGroupSensitivity.Text = "0";
      this.checkBoxRecoverPitch.AutoSize = true;
      this.checkBoxRecoverPitch.Checked = true;
      this.checkBoxRecoverPitch.CheckState = CheckState.Checked;
      this.checkBoxRecoverPitch.Location = new Point(99, 104);
      this.checkBoxRecoverPitch.Name = "checkBoxRecoverPitch";
      this.checkBoxRecoverPitch.Size = new Size(93, 17);
      this.checkBoxRecoverPitch.TabIndex = 20;
      this.checkBoxRecoverPitch.Text = "Recover pitch";
      this.checkBoxRecoverPitch.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 191);
      this.ControlBox = false;
      this.Controls.Add((Control) this.checkBoxRecoverPitch);
      this.Controls.Add((Control) this.textBoxGroupSensitivity);
      this.Controls.Add((Control) this.textBoxSensitivity);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Name = nameof (FormDeClick);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Remove clicks and pops";
      this.Load += new EventHandler(this.FormDeClick_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
