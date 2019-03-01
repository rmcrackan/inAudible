// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormTagEditor
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TagLib;
using TagLib.Mpeg;

namespace AudibleConvertor
{
  public class FormTagEditor : Form
  {
    private DataTable tagTable = new DataTable();
    private string customImagePath = "";
    private IContainer components;
    private Label label2;
    private Button btnOutputFile;
    private System.Windows.Forms.TextBox txtSourceFile;
    private System.Windows.Forms.TextBox txtFileType;
    private DataGridView dgvTags;
    private Button btnSave;
    private PictureBox pictureBox1;
    private Image currentImage;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label2 = new Label();
      this.btnOutputFile = new Button();
      this.txtSourceFile = new System.Windows.Forms.TextBox();
      this.txtFileType = new System.Windows.Forms.TextBox();
      this.dgvTags = new DataGridView();
      this.btnSave = new Button();
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.dgvTags).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.label2.AutoSize = true;
      this.label2.Location = new Point(14, 16);
      this.label2.Name = "label2";
      this.label2.Size = new Size(26, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "File:";
      this.btnOutputFile.AccessibleDescription = "Select target location";
      this.btnOutputFile.AccessibleName = "Save file";
      this.btnOutputFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnOutputFile.Location = new Point(412, 10);
      this.btnOutputFile.Name = "btnOutputFile";
      this.btnOutputFile.Size = new Size(27, 23);
      this.btnOutputFile.TabIndex = 8;
      this.btnOutputFile.Text = "...";
      this.btnOutputFile.UseVisualStyleBackColor = true;
      this.btnOutputFile.Click += new EventHandler(this.btnOutputFile_Click);
      this.txtSourceFile.AccessibleDescription = "Name and location of file or files to create";
      this.txtSourceFile.AccessibleName = "Output file";
      this.txtSourceFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtSourceFile.Location = new Point(46, 12);
      this.txtSourceFile.Name = "txtSourceFile";
      this.txtSourceFile.Size = new Size(360, 20);
      this.txtSourceFile.TabIndex = 7;
      this.txtFileType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtFileType.Location = new Point(445, 12);
      this.txtFileType.Name = "txtFileType";
      this.txtFileType.ReadOnly = true;
      this.txtFileType.Size = new Size(100, 20);
      this.txtFileType.TabIndex = 10;
      this.txtFileType.TextAlign = HorizontalAlignment.Center;
      this.dgvTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTags.Location = new Point(12, 39);
      this.dgvTags.Name = "dgvTags";
      this.dgvTags.Size = new Size(427, 256);
      this.dgvTags.TabIndex = 11;
      this.dgvTags.CellEndEdit += new DataGridViewCellEventHandler(this.dgvTags_CellEndEdit);
      this.btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnSave.Enabled = false;
      this.btnSave.Location = new Point(461, 143);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 12;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.pictureBox1.Location = new Point(445, 39);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(100, 98);
      this.pictureBox1.TabIndex = 13;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(548, 307);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.btnSave);
      this.Controls.Add((Control) this.dgvTags);
      this.Controls.Add((Control) this.txtFileType);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.btnOutputFile);
      this.Controls.Add((Control) this.txtSourceFile);
      this.Name = nameof (FormTagEditor);
      this.Text = "Tag/Metadata Editor";
      ((ISupportInitialize) this.dgvTags).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormTagEditor()
    {
      this.InitializeComponent();
      this.InitTagTable();
      this.dgvTags.Columns.Clear();
      this.dgvTags.DataSource = (object) this.tagTable;
    }

    private void InitTagTable()
    {
      this.tagTable.Clear();
      this.tagTable.Columns.Add("Tag");
      this.tagTable.Columns.Add("Value");
      this.tagTable.Rows.Add((object) "Album", (object) "");
      this.tagTable.Rows.Add((object) "Title", (object) "");
      this.tagTable.Rows.Add((object) "Track", (object) "");
      this.tagTable.Rows.Add((object) "Track Count", (object) "");
      this.tagTable.Rows.Add((object) "Performer", (object) "");
      this.tagTable.Rows.Add((object) "Composer", (object) "");
      this.tagTable.Rows.Add((object) "Date", (object) "");
      this.tagTable.Rows.Add((object) "Comment", (object) "");
    }

    private void btnOutputFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        this.txtSourceFile.Text = openFileDialog.FileName;
        this.GetTags(openFileDialog.FileName);
        this.customImagePath = "";
      }
      openFileDialog.Dispose();
    }

    private void GetTags(string filePath)
    {
      try
      {
        this.GetMP3Header(filePath);
      }
      catch
      {
      }
      try
      {
        TagLib.File file = TagLib.File.Create(filePath);
        this.txtFileType.Text = file.MimeType;
        foreach (ICodec codec in file.Properties.Codecs)
        {
          try
          {
            AudioHeader audioHeader = (AudioHeader) codec;
            int num1 = audioHeader.XingHeader.Present ? 1 : 0;
            int num2 = audioHeader.VBRIHeader.Present ? 1 : 0;
            Audible.diskLogger("Audio Layer: " + (object) audioHeader.AudioLayer);
            Audible.diskLogger("Audio Version: " + (object) audioHeader.Version);
            Audible.diskLogger("Sample Rate: " + (object) audioHeader.AudioSampleRate);
            Audible.diskLogger("Channel Mode: " + (object) audioHeader.ChannelMode);
            Audible.diskLogger("Description: " + audioHeader.Description);
            Audible.diskLogger("Xing: " + (object) audioHeader.XingHeader.Present);
            Audible.diskLogger("VBRI: " + (object) audioHeader.VBRIHeader.Present);
          }
          catch
          {
          }
        }
        this.SetTagProperty("Album", file.Tag.Album);
        this.SetTagProperty("Title", file.Tag.Title);
        this.SetTagProperty("Track", file.Tag.Track.ToString());
        this.SetTagProperty("Track Count", file.Tag.TrackCount.ToString());
        this.SetTagProperty("Performer", file.Tag.FirstPerformer);
        this.SetTagProperty("Composer", file.Tag.FirstComposer);
        this.SetTagProperty("Date", file.Tag.Year.ToString());
        this.SetTagProperty("Comment", file.Tag.Comment);
        this.dgvTags.Columns[0].ReadOnly = true;
        this.dgvTags.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        if (file.Tag.Pictures.Length > 0)
        {
          MemoryStream memoryStream = new MemoryStream(file.Tag.Pictures[0].Data.Data);
          if (memoryStream != null && memoryStream.Length > 4096L)
          {
            this.currentImage = Image.FromStream((Stream) memoryStream);
            this.pictureBox1.Image = this.currentImage.GetThumbnailImage(100, 100, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
          }
          memoryStream.Close();
        }
        file.Dispose();
      }
      catch
      {
      }
    }

    private void GetMP3Header(string filePath)
    {
      new MP3Header().ReadMP3Information(filePath);
    }

    private void ParseXing(string filePath)
    {
      byte[] numArray1 = new byte[1000];
      try
      {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
          fileStream.Read(numArray1, 0, numArray1.Length);
          fileStream.Close();
        }
        byte[] bytes1 = Encoding.ASCII.GetBytes("Xing");
        int sourceIndex = this.SearchBytes(numArray1, bytes1);
        if (sourceIndex == -1)
        {
          byte[] bytes2 = Encoding.ASCII.GetBytes("Info");
          sourceIndex = this.SearchBytes(numArray1, bytes2);
        }
        byte[] numArray2 = new byte[120];
        Array.Copy((Array) numArray1, sourceIndex, (Array) numArray2, 0, 120);
      }
      catch (Exception ex)
      {
      }
    }

    private int SearchBytes(byte[] haystack, byte[] needle)
    {
      int length = needle.Length;
      int num = haystack.Length - length;
      for (int index1 = 0; index1 <= num; ++index1)
      {
        int index2 = 0;
        while (index2 < length && (int) needle[index2] == (int) haystack[index1 + index2])
          ++index2;
        if (index2 == length)
          return index1;
      }
      return -1;
    }

    private void SetTagProperty(string tag, string value)
    {
      for (int index = 0; index < this.tagTable.Rows.Count; ++index)
      {
        if (this.tagTable.Rows[index][0].ToString() == tag)
        {
          this.tagTable.Rows[index][1] = (object) value;
          break;
        }
      }
    }

    private string GetTag(string tag)
    {
      string str = "";
      for (int index = 0; index < this.tagTable.Rows.Count; ++index)
      {
        if (this.tagTable.Rows[index][0].ToString() == tag)
        {
          str = this.tagTable.Rows[index][1].ToString();
          break;
        }
      }
      return str;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.SaveTags();
    }

    private void SaveTags()
    {
      TagLib.File file = TagLib.File.Create(this.txtSourceFile.Text);
      uint num1 = 0;
      uint num2 = 0;
      uint num3 = 0;
      if (this.GetTag("Track") != "")
        num1 = uint.Parse(this.GetTag("Track"));
      if (this.GetTag("Track Count") != "")
        num2 = uint.Parse(this.GetTag("Track Count"));
      if (this.GetTag("Date") != "")
        num3 = uint.Parse(this.GetTag("Date"));
      file.Tag.Album = this.GetTag("Album");
      file.Tag.Title = this.GetTag("Title");
      file.Tag.Track = num1;
      file.Tag.TrackCount = num2;
      file.Tag.Performers = (string[]) null;
      file.Tag.Performers = new string[1]
      {
        this.GetTag("Performer")
      };
      file.Tag.Composers = (string[]) null;
      file.Tag.Composers = new string[1]
      {
        this.GetTag("Composer")
      };
      file.Tag.Year = num3;
      file.Tag.Comment = this.GetTag("Comment");
      if (this.customImagePath != "")
      {
        IPicture picture = (IPicture) new Picture(this.customImagePath);
        picture.Description = "Cover Art";
        file.Tag.Pictures = new IPicture[1]{ picture };
      }
      file.Save();
      this.btnSave.Enabled = false;
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Image files|*.jpg;*.png|All files (*.*)|*.*";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        Image thumbnailImage = Image.FromFile(openFileDialog.FileName).GetThumbnailImage(100, 100, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
        this.customImagePath = openFileDialog.FileName;
        this.pictureBox1.Image = thumbnailImage;
        this.btnSave.Enabled = true;
      }
      openFileDialog.Dispose();
    }

    private void dgvTags_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      this.btnSave.Enabled = true;
    }
  }
}
