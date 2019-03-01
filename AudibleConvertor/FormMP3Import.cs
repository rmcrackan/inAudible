// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormMP3Import
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormMP3Import : Form
  {
    public FormMP3Import.TAG_MODE tagMode;
    public FormMP3Import.TITLE_MODE titleMode;
    private IContainer components;
    private GroupBox groupBox1;
    private RadioButton rbTitleTag;
    private System.Windows.Forms.TextBox txtTitleTag;
    private RadioButton rbTitleFile;
    private System.Windows.Forms.TextBox txtTitleFileName;
    private GroupBox groupBox2;
    private RadioButton rbChapterGenerated;
    private System.Windows.Forms.TextBox txtChapterGenerated;
    private RadioButton rbChapterTag;
    private System.Windows.Forms.TextBox txtChapterTagname;
    private RadioButton rbChapterFileName;
    private System.Windows.Forms.TextBox txtChapterFilename;
    private Button button1;

    public FormMP3Import()
    {
      this.InitializeComponent();
    }

    public void LoadFile(string firstFile)
    {
      TagLib.File file = TagLib.File.Create(firstFile);
      this.txtTitleFileName.Text = Path.GetFileNameWithoutExtension(firstFile);
      this.txtTitleTag.Text = file.Tag.Title;
      this.txtChapterFilename.Text = Path.GetFileNameWithoutExtension(firstFile);
      this.txtChapterTagname.Text = file.Tag.Title;
      this.txtChapterGenerated.Text = "01";
      file.Dispose();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.titleMode = !this.rbTitleFile.Checked ? FormMP3Import.TITLE_MODE.TAG : FormMP3Import.TITLE_MODE.FILE;
      this.tagMode = !this.rbChapterFileName.Checked ? (!this.rbChapterTag.Checked ? FormMP3Import.TAG_MODE.GENERATED : FormMP3Import.TAG_MODE.TAG) : FormMP3Import.TAG_MODE.FILE;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormMP3Import));
      this.groupBox1 = new GroupBox();
      this.txtTitleFileName = new System.Windows.Forms.TextBox();
      this.rbTitleFile = new RadioButton();
      this.rbTitleTag = new RadioButton();
      this.txtTitleTag = new System.Windows.Forms.TextBox();
      this.groupBox2 = new GroupBox();
      this.rbChapterTag = new RadioButton();
      this.txtChapterTagname = new System.Windows.Forms.TextBox();
      this.rbChapterFileName = new RadioButton();
      this.txtChapterFilename = new System.Windows.Forms.TextBox();
      this.rbChapterGenerated = new RadioButton();
      this.txtChapterGenerated = new System.Windows.Forms.TextBox();
      this.button1 = new Button();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.rbTitleTag);
      this.groupBox1.Controls.Add((Control) this.txtTitleTag);
      this.groupBox1.Controls.Add((Control) this.rbTitleFile);
      this.groupBox1.Controls.Add((Control) this.txtTitleFileName);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(526, 82);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Book Title";
      this.txtTitleFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtTitleFileName.Enabled = false;
      this.txtTitleFileName.Location = new Point(112, 24);
      this.txtTitleFileName.Name = "txtTitleFileName";
      this.txtTitleFileName.Size = new Size(408, 20);
      this.txtTitleFileName.TabIndex = 1;
      this.rbTitleFile.AutoSize = true;
      this.rbTitleFile.Location = new Point(6, 25);
      this.rbTitleFile.Name = "rbTitleFile";
      this.rbTitleFile.Size = new Size(100, 17);
      this.rbTitleFile.TabIndex = 2;
      this.rbTitleFile.Text = "MP3 File Name:";
      this.rbTitleFile.UseVisualStyleBackColor = true;
      this.rbTitleTag.AutoSize = true;
      this.rbTitleTag.Checked = true;
      this.rbTitleTag.Location = new Point(6, 47);
      this.rbTitleTag.Name = "rbTitleTag";
      this.rbTitleTag.Size = new Size(95, 17);
      this.rbTitleTag.TabIndex = 4;
      this.rbTitleTag.TabStop = true;
      this.rbTitleTag.Text = "MP3 Title Tag:";
      this.rbTitleTag.UseVisualStyleBackColor = true;
      this.txtTitleTag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtTitleTag.Enabled = false;
      this.txtTitleTag.Location = new Point(112, 47);
      this.txtTitleTag.Name = "txtTitleTag";
      this.txtTitleTag.Size = new Size(408, 20);
      this.txtTitleTag.TabIndex = 3;
      this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox2.Controls.Add((Control) this.rbChapterGenerated);
      this.groupBox2.Controls.Add((Control) this.txtChapterGenerated);
      this.groupBox2.Controls.Add((Control) this.rbChapterTag);
      this.groupBox2.Controls.Add((Control) this.txtChapterTagname);
      this.groupBox2.Controls.Add((Control) this.rbChapterFileName);
      this.groupBox2.Controls.Add((Control) this.txtChapterFilename);
      this.groupBox2.Location = new Point(12, 100);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(526, 97);
      this.groupBox2.TabIndex = 5;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Chapter Source";
      this.rbChapterTag.AutoSize = true;
      this.rbChapterTag.Location = new Point(6, 48);
      this.rbChapterTag.Name = "rbChapterTag";
      this.rbChapterTag.Size = new Size(95, 17);
      this.rbChapterTag.TabIndex = 4;
      this.rbChapterTag.Text = "MP3 Title Tag:";
      this.rbChapterTag.UseVisualStyleBackColor = true;
      this.txtChapterTagname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtChapterTagname.Enabled = false;
      this.txtChapterTagname.Location = new Point(112, 47);
      this.txtChapterTagname.Name = "txtChapterTagname";
      this.txtChapterTagname.Size = new Size(408, 20);
      this.txtChapterTagname.TabIndex = 3;
      this.rbChapterFileName.AutoSize = true;
      this.rbChapterFileName.Checked = true;
      this.rbChapterFileName.Location = new Point(6, 25);
      this.rbChapterFileName.Name = "rbChapterFileName";
      this.rbChapterFileName.Size = new Size(100, 17);
      this.rbChapterFileName.TabIndex = 2;
      this.rbChapterFileName.TabStop = true;
      this.rbChapterFileName.Text = "MP3 File Name:";
      this.rbChapterFileName.UseVisualStyleBackColor = true;
      this.txtChapterFilename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtChapterFilename.Enabled = false;
      this.txtChapterFilename.Location = new Point(112, 24);
      this.txtChapterFilename.Name = "txtChapterFilename";
      this.txtChapterFilename.Size = new Size(408, 20);
      this.txtChapterFilename.TabIndex = 1;
      this.rbChapterGenerated.AutoSize = true;
      this.rbChapterGenerated.Location = new Point(6, 71);
      this.rbChapterGenerated.Name = "rbChapterGenerated";
      this.rbChapterGenerated.Size = new Size(78, 17);
      this.rbChapterGenerated.TabIndex = 6;
      this.rbChapterGenerated.Text = "Generated:";
      this.rbChapterGenerated.UseVisualStyleBackColor = true;
      this.txtChapterGenerated.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtChapterGenerated.Enabled = false;
      this.txtChapterGenerated.Location = new Point(112, 70);
      this.txtChapterGenerated.Name = "txtChapterGenerated";
      this.txtChapterGenerated.Size = new Size(408, 20);
      this.txtChapterGenerated.TabIndex = 5;
      this.button1.Anchor = AnchorStyles.Bottom;
      this.button1.Location = new Point(238, 203);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 6;
      this.button1.Text = "Accept";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(550, 233);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormMP3Import);
      this.Text = "Import MP3 Properties";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
    }

    public enum TITLE_MODE
    {
      FILE,
      TAG,
    }

    public enum TAG_MODE
    {
      FILE,
      TAG,
      GENERATED,
    }
  }
}
