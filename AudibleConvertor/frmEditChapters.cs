// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.frmEditChapters
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class frmEditChapters : Form
  {
    public bool init = true;
    public string soxPath = "";
    public string flacFile = "";
    private Process process = new Process();
    private Lame myLame1 = new Lame();
    private bool decompressionComplete = true;
    private IContainer components;
    private TrackBar tbTime;
    private Button btnApply;
    public DataGridView dgvChapters;
    private TextBox txtChapterTime;
    private Button btnVerifyAudibleChapters;
    private DataGridViewCheckBoxColumn enabled;
    private DataGridViewTextBoxColumn chapterName;
    private DataGridViewTextBoxColumn time;
    private DataGridViewTextBoxColumn duration;
    private DataGridViewTextBoxColumn info;
    private TextBox txtThreshold;
    private Label label1;
    private Label lblOffset;
    private Label label2;
    private TextBox txtScale;
    private GroupBox groupBox1;
    private TextBox txtInstructions;
    private ProgressBar progressBar1;
    private Label label3;
    public bool applied;
    public bool flacMode;
    public bool unpackFlac;
    public Thread unpackThread;
    public string wavFile;
    public double totalWavSeconds;
    public VirtualWAV myVirtualWav;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmEditChapters));
      this.tbTime = new TrackBar();
      this.dgvChapters = new DataGridView();
      this.enabled = new DataGridViewCheckBoxColumn();
      this.chapterName = new DataGridViewTextBoxColumn();
      this.time = new DataGridViewTextBoxColumn();
      this.duration = new DataGridViewTextBoxColumn();
      this.info = new DataGridViewTextBoxColumn();
      this.btnApply = new Button();
      this.txtChapterTime = new TextBox();
      this.btnVerifyAudibleChapters = new Button();
      this.txtThreshold = new TextBox();
      this.label1 = new Label();
      this.lblOffset = new Label();
      this.label2 = new Label();
      this.txtScale = new TextBox();
      this.groupBox1 = new GroupBox();
      this.txtInstructions = new TextBox();
      this.progressBar1 = new ProgressBar();
      this.label3 = new Label();
      this.tbTime.BeginInit();
      ((ISupportInitialize) this.dgvChapters).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.tbTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbTime.Location = new Point(6, 19);
      this.tbTime.Maximum = 15;
      this.tbTime.Minimum = -15;
      this.tbTime.Name = "tbTime";
      this.tbTime.Size = new Size(599, 45);
      this.tbTime.TabIndex = 0;
      this.tbTime.Scroll += new EventHandler(this.tbTime_Scroll);
      this.tbTime.MouseUp += new MouseEventHandler(this.tbTime_MouseUp);
      this.dgvChapters.AllowUserToAddRows = false;
      this.dgvChapters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgvChapters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvChapters.Columns.AddRange((DataGridViewColumn) this.enabled, (DataGridViewColumn) this.chapterName, (DataGridViewColumn) this.time, (DataGridViewColumn) this.duration, (DataGridViewColumn) this.info);
      this.dgvChapters.Location = new Point(12, 173);
      this.dgvChapters.Name = "dgvChapters";
      this.dgvChapters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvChapters.Size = new Size(611, 343);
      this.dgvChapters.TabIndex = 1;
      this.dgvChapters.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvChapters_CellDoubleClick);
      this.dgvChapters.CellEndEdit += new DataGridViewCellEventHandler(this.dgvChapters_CellEndEdit);
      this.dgvChapters.SelectionChanged += new EventHandler(this.dgvChapters_SelectionChanged);
      this.enabled.HeaderText = "Enabled";
      this.enabled.Name = "enabled";
      this.enabled.Width = 50;
      this.chapterName.HeaderText = "Chapter";
      this.chapterName.Name = "chapterName";
      this.chapterName.ReadOnly = true;
      this.time.HeaderText = "Start Time";
      this.time.Name = "time";
      this.duration.HeaderText = "Duration";
      this.duration.Name = "duration";
      this.duration.ReadOnly = true;
      this.info.HeaderText = "Info";
      this.info.Name = "info";
      this.info.ReadOnly = true;
      this.info.Width = 200;
      this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnApply.Location = new Point(281, 522);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new Size(75, 23);
      this.btnApply.TabIndex = 2;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.txtChapterTime.Location = new Point(263, 70);
      this.txtChapterTime.Name = "txtChapterTime";
      this.txtChapterTime.ReadOnly = true;
      this.txtChapterTime.Size = new Size(66, 20);
      this.txtChapterTime.TabIndex = 3;
      this.btnVerifyAudibleChapters.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnVerifyAudibleChapters.Location = new Point(467, 522);
      this.btnVerifyAudibleChapters.Name = "btnVerifyAudibleChapters";
      this.btnVerifyAudibleChapters.Size = new Size(156, 23);
      this.btnVerifyAudibleChapters.TabIndex = 5;
      this.btnVerifyAudibleChapters.Text = "Auto-adjust chapter breaks";
      this.btnVerifyAudibleChapters.UseVisualStyleBackColor = true;
      this.btnVerifyAudibleChapters.Click += new EventHandler(this.btnVerifyAudibleChapters_Click);
      this.txtThreshold.Location = new Point(93, 64);
      this.txtThreshold.Name = "txtThreshold";
      this.txtThreshold.Size = new Size(23, 20);
      this.txtThreshold.TabIndex = 6;
      this.txtThreshold.Text = "10";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(16, 67);
      this.label1.Name = "label1";
      this.label1.Size = new Size(71, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Preview Size:";
      this.label1.TextAlign = ContentAlignment.MiddleRight;
      this.lblOffset.AutoSize = true;
      this.lblOffset.Location = new Point(335, 73);
      this.lblOffset.Name = "lblOffset";
      this.lblOffset.Size = new Size(13, 13);
      this.lblOffset.TabIndex = 8;
      this.lblOffset.Text = "()";
      this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(474, 67);
      this.label2.Name = "label2";
      this.label2.Size = new Size(92, 13);
      this.label2.TabIndex = 10;
      this.label2.Text = "Adjustment Scale:";
      this.label2.TextAlign = ContentAlignment.MiddleRight;
      this.txtScale.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtScale.Location = new Point(572, 64);
      this.txtScale.Name = "txtScale";
      this.txtScale.Size = new Size(23, 20);
      this.txtScale.TabIndex = 9;
      this.txtScale.Text = "30";
      this.txtScale.TextChanged += new EventHandler(this.txtScale_TextChanged);
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.tbTime);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.txtScale);
      this.groupBox1.Controls.Add((Control) this.txtChapterTime);
      this.groupBox1.Controls.Add((Control) this.lblOffset);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.txtThreshold);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(611, 97);
      this.groupBox1.TabIndex = 11;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Chapter Offset Editor";
      this.txtInstructions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtInstructions.Location = new Point(12, 115);
      this.txtInstructions.Multiline = true;
      this.txtInstructions.Name = "txtInstructions";
      this.txtInstructions.ReadOnly = true;
      this.txtInstructions.Size = new Size(613, 52);
      this.txtInstructions.TabIndex = 12;
      this.txtInstructions.Text = componentResourceManager.GetString("txtInstructions.Text");
      this.txtInstructions.TextAlign = HorizontalAlignment.Center;
      this.progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.progressBar1.Location = new Point(65, 522);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(210, 23);
      this.progressBar1.TabIndex = 13;
      this.progressBar1.Visible = false;
      this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(12, 527);
      this.label3.Name = "label3";
      this.label3.Size = new Size(53, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "Decoding";
      this.label3.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(637, 557);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.txtInstructions);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.btnVerifyAudibleChapters);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.dgvChapters);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmEditChapters);
      this.Text = "Edit Chapters";
      this.FormClosing += new FormClosingEventHandler(this.frmEditChapters_FormClosing);
      this.Shown += new EventHandler(this.frmEditChapters_Shown);
      this.tbTime.EndInit();
      ((ISupportInitialize) this.dgvChapters).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private short GetShortFromLittleEndianBytes(byte[] data, int startIndex)
    {
      return (short) ((int) data[startIndex + 1] << 8 | (int) data[startIndex]);
    }

    private byte[] GetLittleEndianBytesFromShort(short data)
    {
      return new byte[2]
      {
        (byte) data,
        (byte) ((int) data >> 8 & (int) byte.MaxValue)
      };
    }

    private float[] NormalizeFloats(byte[] input)
    {
      float num1 = (float) short.MinValue;
      int startIndex1 = 0;
      while (startIndex1 < input.Length)
      {
        float littleEndianBytes = (float) this.GetShortFromLittleEndianBytes(input, startIndex1);
        if ((double) littleEndianBytes > (double) num1)
          num1 = littleEndianBytes;
        startIndex1 += 2;
      }
      float num2 = (float) short.MaxValue - num1;
      float[] numArray = new float[input.Length / 2];
      int startIndex2 = 0;
      while (startIndex2 < input.Length)
      {
        numArray[startIndex2 / 2] = (float) this.GetShortFromLittleEndianBytes(input, startIndex2) + num2;
        startIndex2 += 2;
      }
      return numArray;
    }

    public float[] FloatArrayFromByteArray(byte[] input)
    {
      float[] numArray = new float[input.Length / 4];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = BitConverter.ToSingle(input, index * 4);
      return numArray;
    }

    private void NormalizeFloats(ref float[] arr)
    {
      float num = 0.0f;
      for (long index = 0; index < (long) arr.Length; ++index)
      {
        if ((double) arr[index] > (double) Math.Abs(num))
          num = Math.Abs(arr[index]);
      }
      for (long index = 0; index < (long) arr.Length; ++index)
      {
        arr[index] = arr[index] / num;
        if (!frmEditChapters.IsFinite((double) arr[index]))
          arr[index] = 0.0f;
      }
    }

    public static bool IsFinite(double value)
    {
      if (!double.IsNaN(value))
        return !double.IsInfinity(value);
      return false;
    }

    public static void DrawNormalizedAudio(ref float[] data, PictureBox pb, Color color)
    {
      Bitmap bitmap = pb.Image != null ? (Bitmap) pb.Image : new Bitmap(pb.Width, pb.Height);
      int num1 = 5;
      int num2 = bitmap.Width - 2 * num1;
      int num3 = bitmap.Height - 2 * num1;
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.Clear(Color.Black);
        Pen pen = new Pen(color);
        int length = data.Length;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          int num4 = (int) ((double) index1 * ((double) length / (double) num2));
          int num5 = (int) ((double) (index1 + 1) * ((double) length / (double) num2));
          float num6 = float.MaxValue;
          float num7 = float.MinValue;
          for (int index2 = num4; index2 < num5; ++index2)
          {
            float num8 = data[index2];
            num6 = (double) num8 < (double) num6 ? num8 : num6;
            num7 = (double) num8 > (double) num7 ? num8 : num7;
          }
          int y1 = num1 + num3 - (int) (((double) num7 + 1.0) * 0.5 * (double) num3);
          int y2 = num1 + num3 - (int) (((double) num6 + 1.0) * 0.5 * (double) num3);
          graphics.DrawLine(pen, index1 + num1, y1, index1 + num1, y2);
        }
      }
      pb.Image = (Image) bitmap;
    }

    public frmEditChapters()
    {
      this.InitializeComponent();
      this.myLame1.pcmBufferSize = 8192;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      this.applied = true;
      this.Close();
    }

    private void PlayFLACChapter(string file, long startTime, int threshold)
    {
      try
      {
        this.process.Kill();
      }
      catch
      {
      }
      this.process.StartInfo = new ProcessStartInfo();
      this.process.StartInfo.FileName = this.soxPath;
      this.process.StartInfo.Arguments = "\"" + file + "\" -d trim " + (object) startTime + " " + (object) threshold;
      this.process.StartInfo.UseShellExecute = false;
      this.process.StartInfo.CreateNoWindow = true;
      Audible.diskLogger(this.process.StartInfo.FileName.ToString() + " " + this.process.StartInfo.Arguments.ToString());
      this.process.Start();
    }

    private void MoveSlider(int steps)
    {
      for (int index = 0; index < steps; ++index)
      {
        Thread.Sleep(1000);
        Form1.SetControlPropertyThreadSafe((Control) this.tbTime, "Value", (object) index);
        Form1.SetControlPropertyThreadSafe((Control) this.lblOffset, "Text", (object) ("(" + (object) index + ")"));
      }
      Form1.SetControlPropertyThreadSafe((Control) this.tbTime, "Value", (object) 0);
      Form1.SetControlPropertyThreadSafe((Control) this.lblOffset, "Text", (object) "(0)");
    }

    private void PlayVirtualWAV(long startTime, int threshold)
    {
      double num1 = (double) startTime;
      double num2 = num1 + (double) threshold;
      Form1 form1 = new Form1();
      EncodingOptions myEncodingOptions = new EncodingOptions();
      myEncodingOptions.encoder = "soxplay";
      myEncodingOptions.startChap = (long) num1;
      myEncodingOptions.endChap = (long) num2;
      this.myLame1.soxPath = this.soxPath;
      List<string> stringList = new List<string>();
      this.myLame1.PreprocessVirtualWav(this.myVirtualWav, "", myEncodingOptions);
    }

    private void PreviewChapter()
    {
      if (!this.decompressionComplete)
        return;
      int rowIndex = this.dgvChapters.CurrentCell.RowIndex;
      if (!this.init)
      {
        long startTime = (long) double.Parse(this.dgvChapters.Rows[rowIndex].Cells[2].Value.ToString());
        while (this.myLame1.m4bThread)
          this.myLame1.cancel = true;
        this.myLame1.cancel = false;
        int previewSize = int.Parse(this.txtThreshold.Text.Trim());
        new Thread((ThreadStart) (() => this.PlayVirtualWAV(startTime, previewSize))).Start();
        long num1 = startTime - (long) ((double) previewSize / 2.0);
        long num2 = startTime + (long) previewSize;
        if (num1 < 0L)
          num1 = 0L;
        float[] arr = this.FloatArrayFromByteArray(this.myVirtualWav.GetByteSegment((double) num1, (double) num2));
        this.NormalizeFloats(ref arr);
      }
      else
      {
        this.dgvChapters.ClearSelection();
        this.init = false;
      }
    }

    private void dgvChapters_SelectionChanged(object sender, EventArgs e)
    {
      this.PreviewChapter();
      this.SetChapterUI(this.dgvChapters.CurrentCell.RowIndex);
    }

    private void SetChapterUI(int selectedChapter)
    {
      long secs = (long) double.Parse(this.dgvChapters.Rows[selectedChapter].Cells[2].Value.ToString());
      this.tbTime.Value = 0;
      this.lblOffset.Text = "(0)";
      this.txtChapterTime.Text = this.SecondsToTime(secs);
    }

    private string SecondsToTime(long secs)
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) secs);
      return ((int) Math.Floor(timeSpan.TotalHours)).ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" + timeSpan.Seconds.ToString("D2");
    }

    private void btnVerifyAudibleChapters_Click(object sender, EventArgs e)
    {
      Task.Factory.StartNew((Action) (() =>
      {
        Form1.SetControlPropertyThreadSafe((Control) this.btnVerifyAudibleChapters, "Enabled", (object) false);
        for (int index = 1; index < this.dgvChapters.Rows.Count - 1; ++index)
        {
          try
          {
            Form1 form1 = new Form1();
            form1.outputFileMask = this.flacFile;
            double chapter = double.Parse(this.dgvChapters.Rows[index].Cells[2].Value.ToString());
            double num = form1.VerifyChapterSplit(chapter, this.myVirtualWav);
            this.dgvChapters.Rows[index].Cells[2].Value = (object) (long) num;
            this.dgvChapters.Rows[index].Cells[4].Value = (object) ("offset by " + string.Format("{0:0.0}", (object) (Math.Round(num - chapter, 2).ToString() + "s")));
          }
          catch (Exception ex)
          {
            Audible.diskLogger("WARNING-Something bad happened - " + ex.ToString());
          }
        }
        Form1.SetControlPropertyThreadSafe((Control) this.btnVerifyAudibleChapters, "Enabled", (object) true);
      }));
    }

    private void dgvChapters_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void dgvChapters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      this.PreviewChapter();
    }

    private void tbTime_Scroll(object sender, EventArgs e)
    {
      this.lblOffset.Text = "(" + (object) this.tbTime.Value + ")";
      this.txtChapterTime.Text = this.SecondsToTime(long.Parse(this.dgvChapters.Rows[this.dgvChapters.CurrentCell.RowIndex].Cells[2].Value.ToString()) + (long) this.tbTime.Value);
    }

    private void tbTime_MouseUp(object sender, MouseEventArgs e)
    {
      int rowIndex = this.dgvChapters.CurrentCell.RowIndex;
      long num = long.Parse(this.dgvChapters.Rows[rowIndex].Cells[2].Value.ToString()) + (long) this.tbTime.Value;
      this.dgvChapters.Rows[rowIndex].Cells[2].Value = (object) num;
      this.tbTime.Value = 0;
      this.PreviewChapter();
    }

    private void txtScale_TextChanged(object sender, EventArgs e)
    {
      int num = (int) ((double) int.Parse(this.txtScale.Text) / 2.0);
      this.tbTime.Minimum = -num;
      this.tbTime.Maximum = num;
    }

    private void frmEditChapters_Shown(object sender, EventArgs e)
    {
      if (!this.unpackFlac)
        return;
      this.decompressionComplete = false;
      this.unpackThread.Start();
      this.progressBar1.Visible = true;
      this.label3.Visible = true;
      this.dgvChapters.Enabled = false;
      this.btnVerifyAudibleChapters.Enabled = false;
      new Thread((ThreadStart) (() =>
      {
        while (this.unpackThread.IsAlive)
        {
          try
          {
            long length = new FileInfo(this.wavFile).Length;
            Thread.Sleep(500);
            long num1 = (long) ((double) this.myVirtualWav.sampleRate * (double) this.myVirtualWav.channels * 2.0 * this.totalWavSeconds);
            int num2 = (int) ((double) length / (double) num1 * 100.0);
            if (num2 > 0)
            {
              if (num2 < 100)
                Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Value", (object) num2);
            }
          }
          catch
          {
          }
        }
        Form1.SetControlPropertyThreadSafe((Control) this.progressBar1, "Visible", (object) false);
        Form1.SetControlPropertyThreadSafe((Control) this.label3, "Visible", (object) false);
        Form1.SetControlPropertyThreadSafe((Control) this.dgvChapters, "Enabled", (object) true);
        Form1.SetControlPropertyThreadSafe((Control) this.btnVerifyAudibleChapters, "Enabled", (object) true);
        this.decompressionComplete = true;
      })).Start();
    }

    private void frmEditChapters_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.decompressionComplete)
        return;
      int num = (int) MessageBox.Show("You cannot proceed until the FLAC has been decompressed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
      e.Cancel = true;
    }
  }
}
