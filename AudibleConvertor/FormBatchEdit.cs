// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormBatchEdit
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Inwards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormBatchEdit : Form
  {
    private IContainer components;
    private DataGridView dataGridView1;
    private Button btnApply;
    private CheckBox chkFilterGood;
    private DataGridViewTextBoxColumn Characters;
    private DataGridViewTextBoxColumn SourceFile;
    private DataGridViewTextBoxColumn TargetFile;
    private DataGridViewButtonColumn Browse;
    private Label lblInstructions;
    private TextBox txtRed;
    private TextBox txtYellow;
    private DataGridViewTextBoxColumn id;
    public BatchFiles myBatchFiles;
    public bool applied;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormBatchEdit));
      this.dataGridView1 = new DataGridView();
      this.Characters = new DataGridViewTextBoxColumn();
      this.SourceFile = new DataGridViewTextBoxColumn();
      this.TargetFile = new DataGridViewTextBoxColumn();
      this.Browse = new DataGridViewButtonColumn();
      this.btnApply = new Button();
      this.chkFilterGood = new CheckBox();
      this.lblInstructions = new Label();
      this.txtRed = new TextBox();
      this.txtYellow = new TextBox();
      this.id = new DataGridViewTextBoxColumn();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.Characters, (DataGridViewColumn) this.SourceFile, (DataGridViewColumn) this.TargetFile, (DataGridViewColumn) this.Browse, (DataGridViewColumn) this.id);
      this.dataGridView1.Location = new Point(12, 12);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(760, 212);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
      this.dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
      this.Characters.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Characters.HeaderText = "Size";
      this.Characters.Name = "Characters";
      this.Characters.ReadOnly = true;
      this.Characters.Width = 52;
      this.SourceFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.SourceFile.HeaderText = "Source File";
      this.SourceFile.Name = "SourceFile";
      this.SourceFile.ReadOnly = true;
      this.SourceFile.Width = 85;
      this.TargetFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.TargetFile.HeaderText = "TargetFile";
      this.TargetFile.Name = "TargetFile";
      this.Browse.HeaderText = "Browse";
      this.Browse.Name = "Browse";
      this.Browse.Width = 50;
      this.btnApply.Anchor = AnchorStyles.Bottom;
      this.btnApply.Enabled = false;
      this.btnApply.Location = new Point(355, 230);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 1;
      this.btnApply.Text = "Apply Changes";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.chkFilterGood.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chkFilterGood.AutoSize = true;
      this.chkFilterGood.Location = new Point(636, 236);
      this.chkFilterGood.Name = "chkFilterGood";
      this.chkFilterGood.Size = new Size(136, 17);
      this.chkFilterGood.TabIndex = 2;
      this.chkFilterGood.Text = "Only show problem files";
      this.chkFilterGood.UseVisualStyleBackColor = true;
      this.chkFilterGood.CheckedChanged += new EventHandler(this.chkFilterGood_CheckedChanged);
      this.lblInstructions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblInstructions.AutoSize = true;
      this.lblInstructions.Location = new Point(9, 237);
      this.lblInstructions.Name = "lblInstructions";
      this.lblInstructions.Size = new Size(313, 13);
      this.lblInstructions.TabIndex = 3;
      this.lblInstructions.Text = "Click on the File Name to edit, or click Browse for a new location.";
      this.txtRed.Anchor = AnchorStyles.Bottom;
      this.txtRed.BackColor = Color.Red;
      this.txtRed.Location = new Point(452, 232);
      this.txtRed.Name = "txtRed";
      this.txtRed.Size = new Size(71, 20);
      this.txtRed.TabIndex = 4;
      this.txtRed.Text = "Path too long";
      this.txtRed.TextAlign = HorizontalAlignment.Center;
      this.txtRed.Visible = false;
      this.txtYellow.Anchor = AnchorStyles.Bottom;
      this.txtYellow.BackColor = Color.Yellow;
      this.txtYellow.Location = new Point(539, 232);
      this.txtYellow.Name = "txtYellow";
      this.txtYellow.Size = new Size(71, 20);
      this.txtYellow.TabIndex = 5;
      this.txtYellow.Text = "Duplicates";
      this.txtYellow.TextAlign = HorizontalAlignment.Center;
      this.txtYellow.Visible = false;
      this.id.HeaderText = "id";
      this.id.Name = "id";
      this.id.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 262);
      this.Controls.Add((Control) this.txtYellow);
      this.Controls.Add((Control) this.txtRed);
      this.Controls.Add((Control) this.lblInstructions);
      this.Controls.Add((Control) this.chkFilterGood);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.dataGridView1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FormBatchEdit);
      this.Text = "Batch Editor";
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormBatchEdit()
    {
      this.InitializeComponent();
    }

    public void init()
    {
      for (int index = 0; index < this.myBatchFiles.inputFiles.Length; ++index)
      {
        this.dataGridView1.Rows.Add((object[]) new string[5]
        {
          this.myBatchFiles.outputFiles[index].Length.ToString(),
          Path.GetFileNameWithoutExtension(this.myBatchFiles.inputFiles[index]),
          this.myBatchFiles.outputFiles[index],
          "...",
          index.ToString()
        });
        if (this.myBatchFiles.outputFiles[index].Length >= this.myBatchFiles.maxFileLength)
        {
          this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
          this.txtRed.Visible = true;
        }
      }
      this.MarkDupes(false);
      if (this.AllRowsValid())
        this.btnApply.Enabled = true;
      else
        this.btnApply.Enabled = false;
    }

    private void MarkDupes(bool fromGrid)
    {
      this.txtYellow.Visible = false;
      if (Utilities.CountDuplicates(this.myBatchFiles.outputFiles) == 0)
        return;
      List<string> list = new List<string>((IEnumerable<string>) this.myBatchFiles.outputFiles);
      if (fromGrid)
      {
        for (int index1 = 0; index1 < this.dataGridView1.Rows.Count; ++index1)
        {
          int index2 = int.Parse(this.dataGridView1.Rows[index1].Cells["id"].Value.ToString());
          list[index2] = this.dataGridView1.Rows[index2].Cells["TargetFile"].Value.ToString();
        }
      }
      for (int index = 0; index < this.dataGridView1.Rows.Count; ++index)
      {
        if (this.dataGridView1.Rows[index].DefaultCellStyle.BackColor == Color.Yellow)
          this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.White;
      }
      using (IEnumerator<KeyValuePair<string, int>> enumerator = Utilities.FindDuplicates(list.ToArray()).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, int> dupe = enumerator.Current;
          foreach (int index in Enumerable.Range(0, list.Count).Where<int>((System.Func<int, bool>) (i => list[i] == dupe.Key)).ToList<int>())
          {
            this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
            this.txtYellow.Visible = true;
          }
        }
      }
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.dataGridView1.Rows.Count; ++index)
        this.myBatchFiles.outputFiles[int.Parse(this.dataGridView1.Rows[index].Cells["id"].Value.ToString())] = this.dataGridView1.Rows[index].Cells["TargetFile"].Value.ToString();
      this.applied = true;
      this.Close();
    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (!(this.dataGridView1.Columns[e.ColumnIndex].Name == "TargetFile"))
        return;
      this.UpdateRow(e.RowIndex);
    }

    private void UpdateRow(int row)
    {
      int length = this.dataGridView1.Rows[row].Cells["TargetFile"].Value.ToString().Length;
      this.dataGridView1.Rows[row].Cells[0].Value = (object) length.ToString();
      if (length > this.myBatchFiles.maxFileLength)
        this.dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.Red;
      else
        this.dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.LightGreen;
      if (this.AllRowsValid())
        this.btnApply.Enabled = true;
      else
        this.btnApply.Enabled = false;
      if (!this.chkFilterGood.Checked)
        return;
      this.HideGoodRows();
    }

    private bool AllRowsValid()
    {
      bool flag = true;
      for (int index = 0; index < this.dataGridView1.Rows.Count; ++index)
      {
        if (this.dataGridView1.Rows[index].Cells["TargetFile"].Value.ToString().Length > this.myBatchFiles.maxFileLength)
          flag = false;
      }
      this.txtRed.Visible = !flag;
      return flag;
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 3 && e.RowIndex > -1)
      {
        Thread thread = new Thread((ThreadStart) (() =>
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          saveFileDialog.FileName = Path.GetFileName(this.dataGridView1.Rows[e.RowIndex].Cells["TargetFile"].Value.ToString());
          if (saveFileDialog.ShowDialog() == DialogResult.OK)
            this.dataGridView1.Rows[e.RowIndex].Cells["TargetFile"].Value = (object) saveFileDialog.FileName;
          saveFileDialog.Dispose();
        }));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
      }
      else
      {
        if (e.ColumnIndex != 2 || e.RowIndex <= -1)
          return;
        FormOutputPath formOutputPath = new FormOutputPath();
        formOutputPath.myBatchFiles = this.myBatchFiles;
        formOutputPath.offset = int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
        formOutputPath.SetFileName(this.dataGridView1.Rows[e.RowIndex].Cells["TargetFile"].Value.ToString());
        int num = (int) formOutputPath.ShowDialog();
        if (!formOutputPath.applied)
          return;
        this.dataGridView1.Rows[e.RowIndex].Cells["TargetFile"].Value = (object) formOutputPath.txtFullPath.Text;
        this.MarkDupes(true);
      }
    }

    private void chkFilterGood_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkFilterGood.Checked)
      {
        this.HideGoodRows();
      }
      else
      {
        for (int index = 0; index < this.dataGridView1.Rows.Count; ++index)
          this.dataGridView1.Rows[index].Visible = true;
      }
    }

    private void HideGoodRows()
    {
      for (int index = 0; index < this.dataGridView1.Rows.Count; ++index)
      {
        if (this.dataGridView1.Rows[index].Cells["TargetFile"].Value.ToString().Length > this.myBatchFiles.maxFileLength || this.dataGridView1.Rows[index].DefaultCellStyle.BackColor == Color.Yellow)
          this.dataGridView1.Rows[index].Visible = true;
        else
          this.dataGridView1.Rows[index].Visible = false;
      }
    }
  }
}
