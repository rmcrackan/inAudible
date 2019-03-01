// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormChapterNames
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormChapterNames : Form
  {
    private BindingList<ChapterList> chapterList = new BindingList<ChapterList>();
    public string fileName = "";
    public List<double> chapters;
    public List<string> chapterNames;
    public bool applied;
    public bool includeChapterNumbers;
    private IContainer components;
    private Button btnApply;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn TargetFile;
    private DataGridViewTextBoxColumn FullName;
    public CheckBox chkUseChapterAsTitle;
    public CheckBox chkChapterNumbers;
    public CheckBox chkNoTitleInFilename;
    private Button btnAddChapterText;
    private Button btnLoadCUE;
    public CheckBox chkTitleAndNumber;

    public FormChapterNames()
    {
      this.InitializeComponent();
    }

    public void init()
    {
      this.chapterList.Clear();
      for (int chapterNum = 0; chapterNum < this.chapters.Count; ++chapterNum)
        this.chapterList.Add(new ChapterList(chapterNum, this.chapterNames[chapterNum], this.fileName));
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.DataSource = (object) this.chapterList;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.chapterList.Count; ++index)
        this.chapterNames[index] = this.chapterList[index].ChapterName;
      this.applied = true;
      this.includeChapterNumbers = this.chkChapterNumbers.Checked;
      this.Close();
    }

    private void chkChapterNumbers_CheckedChanged(object sender, EventArgs e)
    {
      this.SetChapterDisplay();
    }

    public void SetChapterDisplay()
    {
      for (int index = 0; index < this.chapterList.Count; ++index)
        this.chapterList[index].SetFullName(this.chkChapterNumbers.Checked);
    }

    private void btnAddChapterText_Click(object sender, EventArgs e)
    {
      this.btnAddChapterText.Enabled = false;
      for (int index = 0; index < this.chapterList.Count; ++index)
        this.chapterList[index].ChapterName = "Chapter " + this.chapterList[index].ChapterName;
    }

    private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      ContextMenu contextMenu = new ContextMenu();
      int currentMouseOverRow = this.dataGridView1.HitTest(e.X, e.Y).RowIndex;
      if (currentMouseOverRow >= 0)
      {
        MenuItem menuItem1 = new MenuItem(string.Format("Set \"001\" at row {0}", (object) currentMouseOverRow.ToString()));
        menuItem1.Click += (EventHandler) ((s, eventArgs) => this.renumberChapters(currentMouseOverRow, ""));
        contextMenu.MenuItems.Add(menuItem1);
        MenuItem menuItem2 = new MenuItem(string.Format("Set \"Chapter 001\" at row {0}", (object) currentMouseOverRow.ToString()));
        menuItem2.Click += (EventHandler) ((s, eventArgs) => this.renumberChapters(currentMouseOverRow, "Chapter "));
        contextMenu.MenuItems.Add(menuItem2);
      }
      contextMenu.Show((Control) this.dataGridView1, new Point(e.X, e.Y));
    }

    private void renumberChapters(int row, string prefix)
    {
      for (int index = row; index < this.chapterList.Count; ++index)
        this.chapterList[index].ChapterName = prefix + (index - row + 1).ToString("D3");
    }

    private void btnLoadCUE_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "CUE files (*.cue)|*.cue|All files (*.*)|*.*";
      openFileDialog.Title = "Load a CUE file";
      if (openFileDialog.ShowDialog() == DialogResult.Cancel)
        return;
      this.ParseCueFile(openFileDialog.FileName);
      this.init();
      this.SetChapterDisplay();
    }

    private void ParseCueFile(string cueFile)
    {
      AdvancedSplitting.Chapters chapters = new AdvancedSplitting.Chapters();
      List<double> doubleList = new List<double>();
      List<string> stringList = new List<string>();
      string[] strArray1 = File.ReadAllText(cueFile).Split('\n');
      try
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          if (strArray1[index].Trim().StartsWith("INDEX"))
          {
            string[] strArray2 = strArray1[index].Trim().Split(' ')[2].Split(':');
            double num = double.Parse(strArray2[2]) * 0.01 + double.Parse(strArray2[1]) + double.Parse(strArray2[0]) * 60.0;
            doubleList.Add(num);
          }
          if (strArray1[index].Trim().StartsWith("TITLE"))
          {
            string[] strArray2 = strArray1[index].Trim().Split('"');
            stringList.Add(strArray2[1]);
          }
        }
        this.chapters = doubleList;
        this.chapterNames = stringList;
      }
      catch
      {
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormChapterNames));
      this.btnApply = new Button();
      this.dataGridView1 = new DataGridView();
      this.TargetFile = new DataGridViewTextBoxColumn();
      this.FullName = new DataGridViewTextBoxColumn();
      this.chkChapterNumbers = new CheckBox();
      this.chkUseChapterAsTitle = new CheckBox();
      this.chkNoTitleInFilename = new CheckBox();
      this.btnAddChapterText = new Button();
      this.btnLoadCUE = new Button();
      this.chkTitleAndNumber = new CheckBox();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.btnApply.Anchor = AnchorStyles.Bottom;
      this.btnApply.Location = new Point(194, 496);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 0;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.TargetFile, (DataGridViewColumn) this.FullName);
      this.dataGridView1.Location = new Point(12, 12);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(439, 437);
      this.dataGridView1.TabIndex = 2;
      this.dataGridView1.MouseClick += new MouseEventHandler(this.dataGridView1_MouseClick);
      this.TargetFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.TargetFile.DataPropertyName = "ChapterName";
      this.TargetFile.HeaderText = "New Name";
      this.TargetFile.Name = "TargetFile";
      this.TargetFile.Width = 78;
      this.FullName.DataPropertyName = "FullName";
      this.FullName.HeaderText = "Full Chapter Name";
      this.FullName.Name = "FullName";
      this.FullName.ReadOnly = true;
      this.chkChapterNumbers.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.chkChapterNumbers.AutoSize = true;
      this.chkChapterNumbers.Checked = true;
      this.chkChapterNumbers.CheckState = CheckState.Checked;
      this.chkChapterNumbers.Location = new Point(12, 455);
      this.chkChapterNumbers.Name = "chkChapterNumbers";
      this.chkChapterNumbers.Size = new Size(194, 17);
      this.chkChapterNumbers.TabIndex = 3;
      this.chkChapterNumbers.Text = "Include chapter number in file name";
      this.chkChapterNumbers.UseVisualStyleBackColor = true;
      this.chkChapterNumbers.CheckedChanged += new EventHandler(this.chkChapterNumbers_CheckedChanged);
      this.chkUseChapterAsTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chkUseChapterAsTitle.AutoSize = true;
      this.chkUseChapterAsTitle.Location = new Point(212, 455);
      this.chkUseChapterAsTitle.Name = "chkUseChapterAsTitle";
      this.chkUseChapterAsTitle.Size = new Size(246, 17);
      this.chkUseChapterAsTitle.TabIndex = 4;
      this.chkUseChapterAsTitle.Text = "Use chapter metadata instead of title metadata";
      this.chkUseChapterAsTitle.UseVisualStyleBackColor = true;
      this.chkNoTitleInFilename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.chkNoTitleInFilename.AutoSize = true;
      this.chkNoTitleInFilename.Location = new Point(12, 478);
      this.chkNoTitleInFilename.Name = "chkNoTitleInFilename";
      this.chkNoTitleInFilename.Size = new Size(170, 17);
      this.chkNoTitleInFilename.TabIndex = 5;
      this.chkNoTitleInFilename.Text = "Do not include title in file name";
      this.chkNoTitleInFilename.UseVisualStyleBackColor = true;
      this.btnAddChapterText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnAddChapterText.Location = new Point(327, 495);
      this.btnAddChapterText.Name = "btnAddChapterText";
      this.btnAddChapterText.Size = new Size(124, 23);
      this.btnAddChapterText.TabIndex = 6;
      this.btnAddChapterText.Text = "Add \"Chapter\" prefix";
      this.btnAddChapterText.UseVisualStyleBackColor = true;
      this.btnAddChapterText.Click += new EventHandler(this.btnAddChapterText_Click);
      this.btnLoadCUE.Anchor = AnchorStyles.Bottom;
      this.btnLoadCUE.Location = new Point(275, 496);
      this.btnLoadCUE.Name = "btnLoadCUE";
      this.btnLoadCUE.Size = new Size(46, 23);
      this.btnLoadCUE.TabIndex = 7;
      this.btnLoadCUE.Text = "CUE";
      this.btnLoadCUE.UseVisualStyleBackColor = true;
      this.btnLoadCUE.Click += new EventHandler(this.btnLoadCUE_Click);
      this.chkTitleAndNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chkTitleAndNumber.AutoSize = true;
      this.chkTitleAndNumber.Location = new Point(212, 478);
      this.chkTitleAndNumber.Name = "chkTitleAndNumber";
      this.chkTitleAndNumber.Size = new Size(230, 17);
      this.chkTitleAndNumber.TabIndex = 8;
      this.chkTitleAndNumber.Text = "Include title and chapter number in Title tag";
      this.chkTitleAndNumber.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(463, 531);
      this.Controls.Add((Control) this.chkTitleAndNumber);
      this.Controls.Add((Control) this.btnLoadCUE);
      this.Controls.Add((Control) this.btnAddChapterText);
      this.Controls.Add((Control) this.chkNoTitleInFilename);
      this.Controls.Add((Control) this.chkUseChapterAsTitle);
      this.Controls.Add((Control) this.chkChapterNumbers);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.btnApply);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormChapterNames);
      this.Text = "Edit Chapter Names";
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
