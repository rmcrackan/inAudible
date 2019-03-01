// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormAudibleDownloader
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormAudibleDownloader : Form
  {
    public bool applied;
    public bool autoEncode;
    public AdvancedOptions myAdvancedOptions;
    private IContainer components;
    private Button btnYes;
    private Button btnCancel;
    private Label lblInstructions;
    public Label lblDlTitle;
    public CheckBox checkBox1;

    public FormAudibleDownloader()
    {
      this.InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnYes_Click(object sender, EventArgs e)
    {
      this.applied = true;
      this.autoEncode = this.checkBox1.Checked;
      this.myAdvancedOptions.EncodeAfterDownload = this.autoEncode;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormAudibleDownloader));
      this.lblDlTitle = new Label();
      this.btnYes = new Button();
      this.btnCancel = new Button();
      this.lblInstructions = new Label();
      this.checkBox1 = new CheckBox();
      this.SuspendLayout();
      this.lblDlTitle.Dock = DockStyle.Top;
      this.lblDlTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.lblDlTitle.Location = new Point(0, 0);
      this.lblDlTitle.Name = "lblDlTitle";
      this.lblDlTitle.Size = new Size(450, 13);
      this.lblDlTitle.TabIndex = 20;
      this.lblDlTitle.Text = "lblDlTitle";
      this.lblDlTitle.TextAlign = ContentAlignment.MiddleCenter;
      this.btnYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnYes.Location = new Point(12, 105);
      this.btnYes.Name = "btnYes";
      this.btnYes.Size = new Size(75, 23);
      this.btnYes.TabIndex = 21;
      this.btnYes.Text = "Download";
      this.btnYes.UseVisualStyleBackColor = true;
      this.btnYes.Click += new EventHandler(this.btnYes_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(363, 105);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 22;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.lblInstructions.AutoSize = true;
      this.lblInstructions.Location = new Point(28, 37);
      this.lblInstructions.Name = "lblInstructions";
      this.lblInstructions.Size = new Size(391, 13);
      this.lblInstructions.TabIndex = 23;
      this.lblInstructions.Text = "Would you like inAudible to download this book directly from Audible.com for you?";
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(74, 64);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(298, 17);
      this.checkBox1.TabIndex = 24;
      this.checkBox1.Text = "Automatically convert this book using your default settings";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(450, 140);
      this.Controls.Add((Control) this.checkBox1);
      this.Controls.Add((Control) this.lblInstructions);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnYes);
      this.Controls.Add((Control) this.lblDlTitle);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormAudibleDownloader);
      this.Text = "Audible Download";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
