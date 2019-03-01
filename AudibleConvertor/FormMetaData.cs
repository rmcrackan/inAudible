// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormMetaData
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormMetaData : Form
  {
    public bool cancelled = true;
    private IContainer components;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    public TextBox txtAuthor;
    public TextBox txtYear;
    public TextBox txtNarrator;
    private Button button1;
    private Button button2;
    private Label label5;
    private Label label6;
    public TextBox txtAlbum;
    public CheckBox chkTrackToTitle;
    private Label label7;
    private Label label8;
    private Label label9;
    public ComboBox cmbGenre;
    public TextBox txtTrackNum;
    public TextBox txtTrackTotal;
    private Button btnAudibleMetadata;
    private Button btnNext;
    private Button btnPrev;
    public Label lblPosition;
    public TextBox txtTitle;
    public RichTextBox txtComments;
    private Button btnCopyiTunesComment;
    private Button btnRemoveUnabridged;
    public TextBox txtPublisher;
    private Label label10;
    public Audible myAudible;
    public Audible[] myAudibles;
    public string[] aaxFiles;
    public int aaxNum;
    public string[] genres;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormMetaData));
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.txtAuthor = new TextBox();
      this.txtYear = new TextBox();
      this.txtNarrator = new TextBox();
      this.button1 = new Button();
      this.button2 = new Button();
      this.label5 = new Label();
      this.label6 = new Label();
      this.txtAlbum = new TextBox();
      this.chkTrackToTitle = new CheckBox();
      this.label7 = new Label();
      this.cmbGenre = new ComboBox();
      this.label8 = new Label();
      this.txtTrackNum = new TextBox();
      this.label9 = new Label();
      this.txtTrackTotal = new TextBox();
      this.btnAudibleMetadata = new Button();
      this.btnNext = new Button();
      this.btnPrev = new Button();
      this.lblPosition = new Label();
      this.txtTitle = new TextBox();
      this.txtComments = new RichTextBox();
      this.btnCopyiTunesComment = new Button();
      this.btnRemoveUnabridged = new Button();
      this.txtPublisher = new TextBox();
      this.label10 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(29, 12);
      this.label1.Name = "label1";
      this.label1.Size = new Size(27, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Title";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(18, 34);
      this.label2.Name = "label2";
      this.label2.Size = new Size(38, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Author";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(27, 56);
      this.label3.Name = "label3";
      this.label3.Size = new Size(29, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Year";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(11, 78);
      this.label4.Name = "label4";
      this.label4.Size = new Size(45, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Narrator";
      this.txtAuthor.AccessibleDescription = "Author name or names";
      this.txtAuthor.AccessibleName = "Author";
      this.txtAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtAuthor.Location = new Point(62, 34);
      this.txtAuthor.Name = "txtAuthor";
      this.txtAuthor.Size = new Size(598, 20);
      this.txtAuthor.TabIndex = 2;
      this.txtYear.AccessibleDescription = "Year published";
      this.txtYear.AccessibleName = "Year";
      this.txtYear.Location = new Point(62, 56);
      this.txtYear.Name = "txtYear";
      this.txtYear.Size = new Size(55, 20);
      this.txtYear.TabIndex = 3;
      this.txtNarrator.AccessibleDescription = "Name of narrator";
      this.txtNarrator.AccessibleName = "Narrator";
      this.txtNarrator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtNarrator.Location = new Point(62, 78);
      this.txtNarrator.Name = "txtNarrator";
      this.txtNarrator.Size = new Size(598, 20);
      this.txtNarrator.TabIndex = 4;
      this.button1.AccessibleDescription = "Save metadata";
      this.button1.AccessibleName = "Save metadata";
      this.button1.Anchor = AnchorStyles.Bottom;
      this.button1.Location = new Point(311, 418);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 8;
      this.button1.Text = "Apply";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.AccessibleDescription = "Abort changes and quit";
      this.button2.AccessibleName = "Cancel";
      this.button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button2.Location = new Point(593, 418);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 9;
      this.button2.Text = "Cancel";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(1, 198);
      this.label5.Name = "label5";
      this.label5.Size = new Size(56, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Comments";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(18, 103);
      this.label6.Name = "label6";
      this.label6.Size = new Size(36, 13);
      this.label6.TabIndex = 12;
      this.label6.Text = "Album";
      this.txtAlbum.AccessibleDescription = "Album name. Usually the same as title";
      this.txtAlbum.AccessibleName = "Album name";
      this.txtAlbum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtAlbum.Location = new Point(62, 100);
      this.txtAlbum.Name = "txtAlbum";
      this.txtAlbum.Size = new Size(598, 20);
      this.txtAlbum.TabIndex = 5;
      this.chkTrackToTitle.AccessibleDescription = "Add the track number to the title when tagging the file";
      this.chkTrackToTitle.AccessibleName = "Add track number to title when tagging";
      this.chkTrackToTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.chkTrackToTitle.AutoSize = true;
      this.chkTrackToTitle.Location = new Point(547, 15);
      this.chkTrackToTitle.Name = "chkTrackToTitle";
      this.chkTrackToTitle.Size = new Size(113, 17);
      this.chkTrackToTitle.TabIndex = 14;
      this.chkTrackToTitle.Text = "Add track # to title";
      this.chkTrackToTitle.UseVisualStyleBackColor = true;
      this.label7.AutoSize = true;
      this.label7.Location = new Point(18, 147);
      this.label7.Name = "label7";
      this.label7.Size = new Size(36, 13);
      this.label7.TabIndex = 15;
      this.label7.Text = "Genre";
      this.cmbGenre.AccessibleDescription = "Audibook genre.  Adding a new value will add it to the dropdown list permanently";
      this.cmbGenre.AccessibleName = "Genre";
      this.cmbGenre.FormattingEnabled = true;
      this.cmbGenre.Items.AddRange(new object[2]
      {
        (object) "Audiobook",
        (object) "Radio Play"
      });
      this.cmbGenre.Location = new Point(63, 147);
      this.cmbGenre.Name = "cmbGenre";
      this.cmbGenre.Size = new Size(121, 21);
      this.cmbGenre.TabIndex = 6;
      this.label8.AutoSize = true;
      this.label8.Location = new Point(20, 173);
      this.label8.Name = "label8";
      this.label8.Size = new Size(35, 13);
      this.label8.TabIndex = 17;
      this.label8.Text = "Track";
      this.txtTrackNum.AccessibleDescription = "Track number";
      this.txtTrackNum.AccessibleName = "Track number";
      this.txtTrackNum.Location = new Point(63, 173);
      this.txtTrackNum.Name = "txtTrackNum";
      this.txtTrackNum.Size = new Size(28, 20);
      this.txtTrackNum.TabIndex = 7;
      this.label9.AutoSize = true;
      this.label9.Location = new Point(97, 176);
      this.label9.Name = "label9";
      this.label9.Size = new Size(16, 13);
      this.label9.TabIndex = 19;
      this.label9.Text = "of";
      this.txtTrackTotal.AccessibleDescription = "Total number of tracks or files in this series. Optional";
      this.txtTrackTotal.AccessibleName = "Number of tracks";
      this.txtTrackTotal.Location = new Point(119, 173);
      this.txtTrackTotal.Name = "txtTrackTotal";
      this.txtTrackTotal.Size = new Size(28, 20);
      this.txtTrackTotal.TabIndex = 8;
      this.btnAudibleMetadata.AccessibleDescription = "View all the data in the non-standard tags used by Audible";
      this.btnAudibleMetadata.AccessibleName = "View Audible Metadata";
      this.btnAudibleMetadata.Anchor = AnchorStyles.Bottom;
      this.btnAudibleMetadata.Location = new Point(420, 418);
      this.btnAudibleMetadata.Name = "btnAudibleMetadata";
      this.btnAudibleMetadata.Size = new Size(142, 23);
      this.btnAudibleMetadata.TabIndex = 21;
      this.btnAudibleMetadata.Text = "View Audible Metadata";
      this.btnAudibleMetadata.UseVisualStyleBackColor = true;
      this.btnAudibleMetadata.Click += new EventHandler(this.btnAudibleMetadata_Click);
      this.btnNext.AccessibleDescription = "Show metadata for next file";
      this.btnNext.AccessibleName = "Next File";
      this.btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnNext.Location = new Point(83, 418);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new Size(30, 23);
      this.btnNext.TabIndex = 22;
      this.btnNext.Text = ">>";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new EventHandler(this.btnNext_Click);
      this.btnPrev.AccessibleDescription = "Show metadata for previous file";
      this.btnPrev.AccessibleName = "Previous file";
      this.btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnPrev.Location = new Point(12, 418);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new Size(30, 23);
      this.btnPrev.TabIndex = 23;
      this.btnPrev.Text = "<<";
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new EventHandler(this.btnPrev_Click);
      this.lblPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblPosition.AutoSize = true;
      this.lblPosition.Location = new Point(48, 423);
      this.lblPosition.Name = "lblPosition";
      this.lblPosition.Size = new Size(36, 13);
      this.lblPosition.TabIndex = 24;
      this.lblPosition.Text = "00/00";
      this.txtTitle.AccessibleDescription = "Title of audio boook";
      this.txtTitle.AccessibleName = "Title";
      this.txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtTitle.Location = new Point(62, 9);
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.Size = new Size(479, 20);
      this.txtTitle.TabIndex = 1;
      this.txtComments.AccessibleDescription = "Brief description of book.  Limited to 255 characters for most codecs.";
      this.txtComments.AccessibleName = "Comments";
      this.txtComments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.txtComments.Location = new Point(62, 199);
      this.txtComments.Name = "txtComments";
      this.txtComments.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.txtComments.Size = new Size(598, 213);
      this.txtComments.TabIndex = 9;
      this.txtComments.Text = "";
      this.btnCopyiTunesComment.AccessibleDescription = "Copies short 255-character comment";
      this.btnCopyiTunesComment.AccessibleName = "Use iTunes short description";
      this.btnCopyiTunesComment.Location = new Point(2, 214);
      this.btnCopyiTunesComment.Name = "btnCopyiTunesComment";
      this.btnCopyiTunesComment.Size = new Size(52, 54);
      this.btnCopyiTunesComment.TabIndex = 27;
      this.btnCopyiTunesComment.Text = "Copy iTunes \"cmt\"";
      this.btnCopyiTunesComment.UseVisualStyleBackColor = true;
      this.btnCopyiTunesComment.Click += new EventHandler(this.btnCopyiTunesComment_Click);
      this.btnRemoveUnabridged.Anchor = AnchorStyles.Bottom;
      this.btnRemoveUnabridged.Location = new Point(140, 418);
      this.btnRemoveUnabridged.Name = "btnRemoveUnabridged";
      this.btnRemoveUnabridged.Size = new Size(140, 23);
      this.btnRemoveUnabridged.TabIndex = 28;
      this.btnRemoveUnabridged.Text = "Remove \"(Unabridged)\"";
      this.btnRemoveUnabridged.UseVisualStyleBackColor = true;
      this.btnRemoveUnabridged.Click += new EventHandler(this.btnRemoveUnabridged_Click);
      this.txtPublisher.AccessibleDescription = "Name of the publisher";
      this.txtPublisher.AccessibleName = "Publisher Name";
      this.txtPublisher.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtPublisher.Location = new Point(62, 123);
      this.txtPublisher.Name = "txtPublisher";
      this.txtPublisher.Size = new Size(598, 20);
      this.txtPublisher.TabIndex = 29;
      this.label10.AutoSize = true;
      this.label10.Location = new Point(6, 126);
      this.label10.Name = "label10";
      this.label10.Size = new Size(50, 13);
      this.label10.TabIndex = 30;
      this.label10.Text = "Publisher";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(672, 444);
      this.Controls.Add((Control) this.txtPublisher);
      this.Controls.Add((Control) this.label10);
      this.Controls.Add((Control) this.btnRemoveUnabridged);
      this.Controls.Add((Control) this.btnCopyiTunesComment);
      this.Controls.Add((Control) this.txtComments);
      this.Controls.Add((Control) this.txtTitle);
      this.Controls.Add((Control) this.lblPosition);
      this.Controls.Add((Control) this.btnPrev);
      this.Controls.Add((Control) this.btnNext);
      this.Controls.Add((Control) this.btnAudibleMetadata);
      this.Controls.Add((Control) this.txtTrackTotal);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.txtTrackNum);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.cmbGenre);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.chkTrackToTitle);
      this.Controls.Add((Control) this.txtAlbum);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.txtNarrator);
      this.Controls.Add((Control) this.txtYear);
      this.Controls.Add((Control) this.txtAuthor);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormMetaData);
      this.Text = "Audible Meta Data";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormMetaData()
    {
      this.InitializeComponent();
    }

    public void FormatComments()
    {
      if (this.txtComments.Text.Length <= (int) byte.MaxValue)
        return;
      string text = this.txtComments.Text;
      this.txtComments.Text = text.Substring(0, (int) byte.MaxValue);
      this.txtComments.AppendText(text.Substring((int) byte.MaxValue), Color.Red);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.cancelled = false;
      this.SaveCurrentData();
      this.genres = new string[this.cmbGenre.Items.Count];
      for (int index = 0; index < this.cmbGenre.Items.Count; ++index)
        this.genres[index] = this.cmbGenre.Items[index].ToString();
      for (int index = 0; index < this.myAudibles.Length; ++index)
      {
        if (!((IEnumerable<string>) this.genres).Contains<string>(this.myAudibles[index].genre) && this.myAudibles[index].genre.Length > 0)
        {
          Array.Resize<string>(ref this.genres, this.genres.Length + 1);
          this.genres[this.genres.Length - 1] = this.myAudibles[index].genre;
          Array.Sort<string>(this.genres);
        }
      }
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.cancelled = true;
      this.Close();
    }

    private void btnAudibleMetadata_Click(object sender, EventArgs e)
    {
      frmMetaDataGrid frmMetaDataGrid = new frmMetaDataGrid();
      frmMetaDataGrid.myAAXMetaData = this.myAudible.GetCompleteAAXMetaData(this.aaxFiles[this.aaxNum]);
      frmMetaDataGrid.initGrid();
      frmMetaDataGrid.Show();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      this.SaveCurrentData();
      ++this.aaxNum;
      if (this.aaxNum == this.myAudibles.Length)
        this.aaxNum = 0;
      this.PopulateMetaData();
    }

    private void PopulateMetaData()
    {
      this.txtTitle.Text = this.myAudibles[this.aaxNum].title;
      this.txtAlbum.Text = this.myAudibles[this.aaxNum].album;
      this.txtAuthor.Text = this.myAudibles[this.aaxNum].author;
      this.txtNarrator.Text = this.myAudibles[this.aaxNum].narrator;
      this.txtYear.Text = this.myAudibles[this.aaxNum].year;
      this.chkTrackToTitle.Checked = this.myAudibles[this.aaxNum].addTrackToTitle;
      this.txtComments.Text = this.myAudibles[this.aaxNum].GetComments();
      this.FormatComments();
      this.txtTrackNum.Text = this.myAudibles[this.aaxNum].trackNum;
      this.txtTrackTotal.Text = this.myAudibles[this.aaxNum].trackTotal;
      this.cmbGenre.Text = this.myAudibles[this.aaxNum].genre;
      this.lblPosition.Text = (this.aaxNum + 1).ToString() + "/" + (object) this.myAudibles.Length;
    }

    private void SaveCurrentData()
    {
      this.myAudibles[this.aaxNum].title = this.txtTitle.Text;
      this.myAudibles[this.aaxNum].album = this.txtAlbum.Text;
      this.myAudibles[this.aaxNum].author = this.txtAuthor.Text;
      this.myAudibles[this.aaxNum].narrator = this.txtNarrator.Text;
      this.myAudibles[this.aaxNum].year = this.txtYear.Text;
      this.myAudibles[this.aaxNum].addTrackToTitle = this.chkTrackToTitle.Checked;
      this.myAudibles[this.aaxNum].SetComments(this.txtComments.Text);
      this.myAudibles[this.aaxNum].trackNum = this.txtTrackNum.Text;
      this.myAudibles[this.aaxNum].trackTotal = this.txtTrackTotal.Text;
      this.myAudibles[this.aaxNum].genre = this.cmbGenre.Text;
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
      this.SaveCurrentData();
      --this.aaxNum;
      if (this.aaxNum < 0)
        this.aaxNum = this.myAudibles.Length - 1;
      this.PopulateMetaData();
    }

    private void btnCopyiTunesComment_Click(object sender, EventArgs e)
    {
      this.txtComments.Text = this.myAudibles[this.aaxNum].cmt;
    }

    private void btnRemoveUnabridged_Click(object sender, EventArgs e)
    {
      this.txtTitle.Text = this.txtTitle.Text.Replace(" (Unabridged)", "");
      this.txtAlbum.Text = this.txtAlbum.Text.Replace(" (Unabridged)", "");
    }
  }
}
