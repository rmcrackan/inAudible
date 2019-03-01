// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormBardAccount
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormBardAccount : Form
  {
    private IContainer components;
    private Label lblInstructions;
    private Label lblUser;
    private Label label1;
    private Button btnOkay;
    private Button btnCancel;
    public TextBox txtUser;
    public TextBox txtPassword;
    public bool applied;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormBardAccount));
      this.lblInstructions = new Label();
      this.lblUser = new Label();
      this.label1 = new Label();
      this.txtUser = new TextBox();
      this.txtPassword = new TextBox();
      this.btnOkay = new Button();
      this.btnCancel = new Button();
      this.SuspendLayout();
      this.lblInstructions.AutoSize = true;
      this.lblInstructions.BorderStyle = BorderStyle.Fixed3D;
      this.lblInstructions.Location = new Point(12, 9);
      this.lblInstructions.Name = "lblInstructions";
      this.lblInstructions.Size = new Size(306, 106);
      this.lblInstructions.TabIndex = 0;
      this.lblInstructions.Text = componentResourceManager.GetString("lblInstructions.Text");
      this.lblUser.AutoSize = true;
      this.lblUser.Location = new Point(13, 121);
      this.lblUser.Name = "lblUser";
      this.lblUser.Size = new Size(55, 13);
      this.lblUser.TabIndex = 1;
      this.lblUser.Text = "Username";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(16, 147);
      this.label1.Name = "label1";
      this.label1.Size = new Size(53, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Password";
      this.txtUser.Location = new Point(75, 118);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new Size(240, 20);
      this.txtUser.TabIndex = 1;
      this.txtPassword.Location = new Point(75, 144);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new Size(240, 20);
      this.txtPassword.TabIndex = 4;
      this.btnOkay.Location = new Point(19, 170);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new Size(75, 23);
      this.btnOkay.TabIndex = 5;
      this.btnOkay.Text = "Apply";
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new EventHandler(this.btnOkay_Click);
      this.btnCancel.Location = new Point(240, 170);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(327, 203);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOkay);
      this.Controls.Add((Control) this.txtPassword);
      this.Controls.Add((Control) this.txtUser);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.lblUser);
      this.Controls.Add((Control) this.lblInstructions);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormBardAccount);
      this.Text = "BARD Account";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormBardAccount()
    {
      this.InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.applied = false;
      this.Close();
    }

    private void btnOkay_Click(object sender, EventArgs e)
    {
      this.applied = true;
      this.Close();
    }
  }
}
