// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormOutputPath
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Inwards;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormOutputPath : Form
  {
    private IContainer components;
    private Button btnApply;
    private Label label1;
    private TextBox txtCharacters;
    public RichTextBox txtFullPath;
    private Label label2;
    private Label label3;
    private TextBox txtOutputDir;
    private TextBox txtAuthor;
    private TextBox txtTitle;
    private TextBox txtOutputDirSize;
    private TextBox txtAuthorSize;
    private TextBox txtTitleSize;
    private Label label5;
    private CheckBox chkAuthorTitle;
    private CheckBox chkBookTitle;
    private CheckBox chkAuthor;
    private GroupBox groupBox1;
    private Label label4;
    private Label label6;
    private Button btnBrowse;
    private TextBox txtNarratorSize;
    private TextBox txtNarrator;
    private Label label7;
    private TextBox txtMaxSize;
    private Label label8;
    private CheckBox chkExtension;
    public bool applied;
    public BatchFiles myBatchFiles;
    public int offset;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormOutputPath));
      this.btnApply = new Button();
      this.label1 = new Label();
      this.txtCharacters = new TextBox();
      this.txtFullPath = new RichTextBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.txtOutputDir = new TextBox();
      this.txtAuthor = new TextBox();
      this.txtTitle = new TextBox();
      this.txtOutputDirSize = new TextBox();
      this.txtAuthorSize = new TextBox();
      this.txtTitleSize = new TextBox();
      this.label5 = new Label();
      this.chkAuthorTitle = new CheckBox();
      this.chkBookTitle = new CheckBox();
      this.chkAuthor = new CheckBox();
      this.groupBox1 = new GroupBox();
      this.txtNarratorSize = new TextBox();
      this.txtNarrator = new TextBox();
      this.label7 = new Label();
      this.btnBrowse = new Button();
      this.label4 = new Label();
      this.label6 = new Label();
      this.txtMaxSize = new TextBox();
      this.label8 = new Label();
      this.chkExtension = new CheckBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.btnApply.Anchor = AnchorStyles.Bottom;
      this.btnApply.Enabled = false;
      this.btnApply.FlatStyle = FlatStyle.Popup;
      this.btnApply.Location = new Point(312, 306);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 1;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.label1.Anchor = AnchorStyles.Top;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(205, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(61, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Characters:";
      this.txtCharacters.Anchor = AnchorStyles.Top;
      this.txtCharacters.Location = new Point(272, 10);
      this.txtCharacters.Name = "txtCharacters";
      this.txtCharacters.ReadOnly = true;
      this.txtCharacters.Size = new Size(221, 20);
      this.txtCharacters.TabIndex = 3;
      this.txtCharacters.TextAlign = HorizontalAlignment.Center;
      this.txtCharacters.TextChanged += new EventHandler(this.textBox1_TextChanged);
      this.txtFullPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtFullPath.Location = new Point(12, 36);
      this.txtFullPath.Multiline = false;
      this.txtFullPath.Name = "txtFullPath";
      this.txtFullPath.Size = new Size(675, 20);
      this.txtFullPath.TabIndex = 1;
      this.txtFullPath.Text = "";
      this.txtFullPath.KeyUp += new KeyEventHandler(this.txtFullPath_KeyUp);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(19, 71);
      this.label2.Name = "label2";
      this.label2.Size = new Size(41, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Author:";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(30, 97);
      this.label3.Name = "label3";
      this.label3.Size = new Size(30, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Title:";
      this.txtOutputDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtOutputDir.Location = new Point(66, 42);
      this.txtOutputDir.Name = "txtOutputDir";
      this.txtOutputDir.Size = new Size(508, 20);
      this.txtOutputDir.TabIndex = 7;
      this.txtOutputDir.KeyUp += new KeyEventHandler(this.txtOutputDir_KeyUp);
      this.txtAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtAuthor.Location = new Point(66, 68);
      this.txtAuthor.Name = "txtAuthor";
      this.txtAuthor.Size = new Size(563, 20);
      this.txtAuthor.TabIndex = 8;
      this.txtAuthor.KeyUp += new KeyEventHandler(this.txtOutputDir_KeyUp);
      this.txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtTitle.Location = new Point(66, 94);
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.Size = new Size(563, 20);
      this.txtTitle.TabIndex = 9;
      this.txtTitle.KeyUp += new KeyEventHandler(this.txtOutputDir_KeyUp);
      this.txtOutputDirSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtOutputDirSize.Location = new Point(635, 42);
      this.txtOutputDirSize.Name = "txtOutputDirSize";
      this.txtOutputDirSize.ReadOnly = true;
      this.txtOutputDirSize.Size = new Size(34, 20);
      this.txtOutputDirSize.TabIndex = 10;
      this.txtOutputDirSize.TextAlign = HorizontalAlignment.Right;
      this.txtAuthorSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtAuthorSize.Location = new Point(635, 68);
      this.txtAuthorSize.Name = "txtAuthorSize";
      this.txtAuthorSize.ReadOnly = true;
      this.txtAuthorSize.Size = new Size(34, 20);
      this.txtAuthorSize.TabIndex = 11;
      this.txtAuthorSize.TextAlign = HorizontalAlignment.Right;
      this.txtTitleSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtTitleSize.Location = new Point(635, 94);
      this.txtTitleSize.Name = "txtTitleSize";
      this.txtTitleSize.ReadOnly = true;
      this.txtTitleSize.Size = new Size(34, 20);
      this.txtTitleSize.TabIndex = 12;
      this.txtTitleSize.TextAlign = HorizontalAlignment.Right;
      this.label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label5.AutoSize = true;
      this.label5.Location = new Point(110, 163);
      this.label5.Name = "label5";
      this.label5.Size = new Size(464, 13);
      this.label5.TabIndex = 13;
      this.label5.Text = "*** Changes made to Author and Title only change the path and not the audio book metadata! ***";
      this.chkAuthorTitle.AccessibleDescription = "Create folder with author and title  name";
      this.chkAuthorTitle.AccessibleName = "Author and title folder";
      this.chkAuthorTitle.AutoSize = true;
      this.chkAuthorTitle.Enabled = false;
      this.chkAuthorTitle.Location = new Point(339, 19);
      this.chkAuthorTitle.Name = "chkAuthorTitle";
      this.chkAuthorTitle.Size = new Size(86, 17);
      this.chkAuthorTitle.TabIndex = 16;
      this.chkAuthorTitle.Text = "Author - Title";
      this.chkAuthorTitle.UseVisualStyleBackColor = true;
      this.chkBookTitle.AccessibleDescription = "Create folder with book's name";
      this.chkBookTitle.AccessibleName = "Title folder";
      this.chkBookTitle.AutoSize = true;
      this.chkBookTitle.Enabled = false;
      this.chkBookTitle.Location = new Point(247, 19);
      this.chkBookTitle.Name = "chkBookTitle";
      this.chkBookTitle.Size = new Size(86, 17);
      this.chkBookTitle.TabIndex = 15;
      this.chkBookTitle.Text = "Book Title ->";
      this.chkBookTitle.UseVisualStyleBackColor = true;
      this.chkAuthor.AccessibleDescription = "Create folder with author's name";
      this.chkAuthor.AccessibleName = "Author folder";
      this.chkAuthor.AutoSize = true;
      this.chkAuthor.Enabled = false;
      this.chkAuthor.Location = new Point(172, 19);
      this.chkAuthor.Name = "chkAuthor";
      this.chkAuthor.Size = new Size(69, 17);
      this.chkAuthor.TabIndex = 14;
      this.chkAuthor.Text = "Author ->";
      this.chkAuthor.UseVisualStyleBackColor = true;
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.chkExtension);
      this.groupBox1.Controls.Add((Control) this.txtNarratorSize);
      this.groupBox1.Controls.Add((Control) this.txtNarrator);
      this.groupBox1.Controls.Add((Control) this.label7);
      this.groupBox1.Controls.Add((Control) this.btnBrowse);
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.chkAuthorTitle);
      this.groupBox1.Controls.Add((Control) this.chkBookTitle);
      this.groupBox1.Controls.Add((Control) this.chkAuthor);
      this.groupBox1.Controls.Add((Control) this.label5);
      this.groupBox1.Controls.Add((Control) this.txtTitleSize);
      this.groupBox1.Controls.Add((Control) this.txtAuthorSize);
      this.groupBox1.Controls.Add((Control) this.txtOutputDirSize);
      this.groupBox1.Controls.Add((Control) this.txtTitle);
      this.groupBox1.Controls.Add((Control) this.txtAuthor);
      this.groupBox1.Controls.Add((Control) this.txtOutputDir);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Location = new Point(12, 98);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(675, 202);
      this.groupBox1.TabIndex = 17;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Metadata options";
      this.txtNarratorSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtNarratorSize.Location = new Point(634, 120);
      this.txtNarratorSize.Name = "txtNarratorSize";
      this.txtNarratorSize.ReadOnly = true;
      this.txtNarratorSize.Size = new Size(34, 20);
      this.txtNarratorSize.TabIndex = 21;
      this.txtNarratorSize.TextAlign = HorizontalAlignment.Right;
      this.txtNarrator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtNarrator.Location = new Point(65, 120);
      this.txtNarrator.Name = "txtNarrator";
      this.txtNarrator.ReadOnly = true;
      this.txtNarrator.Size = new Size(563, 20);
      this.txtNarrator.TabIndex = 20;
      this.label7.AutoSize = true;
      this.label7.Location = new Point(12, 123);
      this.label7.Name = "label7";
      this.label7.Size = new Size(48, 13);
      this.label7.TabIndex = 19;
      this.label7.Text = "Narrator:";
      this.btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnBrowse.Location = new Point(580, 40);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new Size(48, 23);
      this.btnBrowse.TabIndex = 18;
      this.btnBrowse.Text = "...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(7, 45);
      this.label4.Name = "label4";
      this.label4.Size = new Size(58, 13);
      this.label4.TabIndex = 17;
      this.label4.Text = "Output Dir:";
      this.label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label6.AutoSize = true;
      this.label6.ForeColor = Color.Red;
      this.label6.Location = new Point(92, 70);
      this.label6.Name = "label6";
      this.label6.Size = new Size(514, 13);
      this.label6.TabIndex = 18;
      this.label6.Text = "You can edit EITHER the raw path above OR the Author / Title tags below.  They will overwrite each other.";
      this.txtMaxSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtMaxSize.Location = new Point(645, 10);
      this.txtMaxSize.Name = "txtMaxSize";
      this.txtMaxSize.ReadOnly = true;
      this.txtMaxSize.Size = new Size(35, 20);
      this.txtMaxSize.TabIndex = 20;
      this.txtMaxSize.TextAlign = HorizontalAlignment.Center;
      this.label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label8.AutoSize = true;
      this.label8.Location = new Point(587, 13);
      this.label8.Name = "label8";
      this.label8.Size = new Size(53, 13);
      this.label8.TabIndex = 19;
      this.label8.Text = "Max Size:";
      this.chkExtension.AccessibleDescription = "Create folder with author and title  name";
      this.chkExtension.AccessibleName = "Author and title folder";
      this.chkExtension.AutoSize = true;
      this.chkExtension.Enabled = false;
      this.chkExtension.Location = new Point(431, 19);
      this.chkExtension.Name = "chkExtension";
      this.chkExtension.Size = new Size(72, 17);
      this.chkExtension.TabIndex = 22;
      this.chkExtension.Text = "Extension";
      this.chkExtension.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.btnApply;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(699, 341);
      this.Controls.Add((Control) this.txtMaxSize);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.txtFullPath);
      this.Controls.Add((Control) this.txtCharacters);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.btnApply);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormOutputPath);
      this.Text = "Edit Output Path";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public void SetFileName(string fileName)
    {
      this.txtFullPath.Text = fileName;
      this.CheckKeyword("\\", Color.Blue, 0);
      this.txtMaxSize.Text = this.myBatchFiles.maxFileLength.ToString();
      this.txtOutputDir.Text = this.myBatchFiles.rootPath;
      this.txtAuthor.Text = Utilities.CleanFileName(this.myBatchFiles.myAudibles[this.offset].author);
      this.txtTitle.Text = Utilities.CleanFileName(this.myBatchFiles.myAudibles[this.offset].title);
      this.txtNarrator.Text = Utilities.CleanFileName(this.myBatchFiles.myAudibles[this.offset].narrator);
      this.chkAuthor.Checked = this.myBatchFiles.authorDir;
      this.chkBookTitle.Checked = this.myBatchFiles.titleDir;
      this.chkAuthorTitle.Checked = this.myBatchFiles.bothDirs;
      this.chkExtension.Checked = this.myBatchFiles.fileExtension;
      this.UpdateMetadataStats();
      this.ValidatePath();
    }

    private void UpdateMetadataStats()
    {
      this.txtOutputDirSize.Text = this.txtOutputDir.Text.Length.ToString();
      this.txtAuthorSize.Text = this.txtAuthor.Text.Length.ToString();
      this.txtTitleSize.Text = this.txtTitle.Text.Length.ToString();
      this.txtNarratorSize.Text = this.txtNarrator.Text.Length.ToString();
      string str = this.txtOutputDir.Text;
      if (this.myBatchFiles.authorDir)
        str = str + "\\" + Utilities.CleanFileName(this.txtAuthor.Text);
      if (this.myBatchFiles.titleDir)
        str = str + "\\" + Utilities.CleanFileName(this.txtTitle.Text);
      if (this.myBatchFiles.bothDirs)
        str = str + "\\" + Utilities.CleanFileName(this.txtAuthor.Text) + " - " + Utilities.CleanFileName(this.txtTitle.Text);
      if (this.myBatchFiles.fileExtension)
        str = str + "." + this.myBatchFiles.extensionName.ToUpper();
      this.txtFullPath.Text = str + "\\" + Utilities.CleanFileName(this.txtTitle.Text) + "." + this.myBatchFiles.extensionName;
      this.CheckKeyword("\\", Color.Blue, 0);
      this.ValidatePath();
    }

    private void ValidatePath()
    {
      int length = this.txtFullPath.Text.Length;
      this.txtCharacters.Text = this.GetPathBreakdown(this.txtFullPath.Text);
      BatchFiles batchFiles = new BatchFiles();
      if (length > batchFiles.maxFileLength)
      {
        this.txtCharacters.BackColor = Color.Red;
        this.txtCharacters.ForeColor = Color.Yellow;
        this.btnApply.Enabled = false;
      }
      else
      {
        this.txtCharacters.BackColor = Color.LightGreen;
        this.txtCharacters.ForeColor = Color.Black;
        this.btnApply.Enabled = true;
      }
    }

    private string GetPathBreakdown(string path)
    {
      string str = " ";
      string[] strArray = path.Split('\\');
      for (int index = 0; index < strArray.Length; ++index)
      {
        str += (string) (object) strArray[index].Length;
        if (index + 1 != strArray.Length)
          str += " + ";
      }
      return str + " = " + (object) path.Length;
    }

    public FormOutputPath()
    {
      this.InitializeComponent();
    }

    private void txtFullPath_KeyUp(object sender, KeyEventArgs e)
    {
      this.ValidatePath();
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      Size size = TextRenderer.MeasureText(this.txtCharacters.Text, this.txtCharacters.Font);
      this.txtCharacters.Width = size.Width;
      this.txtCharacters.Height = size.Height;
    }

    private void CheckKeyword(string word, Color color, int startIndex)
    {
      if (!this.txtFullPath.Text.Contains(word))
        return;
      int num = -1;
      int selectionStart = this.txtFullPath.SelectionStart;
      while ((num = this.txtFullPath.Text.IndexOf(word, num + 1)) != -1)
      {
        this.txtFullPath.Select(num + startIndex, word.Length);
        this.txtFullPath.SelectionFont = new Font(this.txtFullPath.Font, FontStyle.Bold);
        this.txtFullPath.SelectionColor = color;
        this.txtFullPath.Select(selectionStart, 0);
        this.txtFullPath.SelectionColor = Color.Black;
      }
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      this.applied = true;
      this.Close();
    }

    private void txtOutputDir_KeyUp(object sender, KeyEventArgs e)
    {
      this.UpdateMetadataStats();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
      commonOpenFileDialog.IsFolderPicker = true;
      commonOpenFileDialog.Title = "Select Target Folder";
      commonOpenFileDialog.InitialDirectory = this.txtOutputDir.Text;
      if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
        this.txtOutputDir.Text = commonOpenFileDialog.FileName;
      this.UpdateMetadataStats();
      commonOpenFileDialog.Dispose();
    }
  }
}
