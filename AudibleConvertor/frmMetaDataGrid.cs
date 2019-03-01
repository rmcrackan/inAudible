// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.frmMetaDataGrid
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class frmMetaDataGrid : Form
  {
    private IContainer components;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn tag;
    private DataGridViewTextBoxColumn data;
    public AAXMetaData myAAXMetaData;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmMetaDataGrid));
      this.dataGridView1 = new DataGridView();
      this.tag = new DataGridViewTextBoxColumn();
      this.data = new DataGridViewTextBoxColumn();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.tag, (DataGridViewColumn) this.data);
      this.dataGridView1.Location = new Point(2, 2);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(538, 413);
      this.dataGridView1.TabIndex = 0;
      this.tag.HeaderText = "Tag";
      this.tag.Name = "tag";
      this.data.HeaderText = "Data";
      this.data.Name = "data";
      this.data.Width = 1000;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(542, 416);
      this.Controls.Add((Control) this.dataGridView1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmMetaDataGrid);
      this.Text = "Audible Metadata";
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
    }

    public frmMetaDataGrid()
    {
      this.InitializeComponent();
    }

    public void initGrid()
    {
      for (int index1 = 0; index1 < this.myAAXMetaData.tagCollection.Count; ++index1)
      {
        int index2 = this.dataGridView1.Rows.Add();
        string[] strArray = this.myAAXMetaData.tagCollection[index1].Split('|');
        this.dataGridView1.Rows[index2].Cells[0].Value = (object) strArray[0];
        this.dataGridView1.Rows[index2].Cells[1].Value = (object) strArray[1];
      }
    }
  }
}
