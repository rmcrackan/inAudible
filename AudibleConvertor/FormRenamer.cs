// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.FormRenamer
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AudibleConvertor
{
  public class FormRenamer : Form
  {
    public BindingList<RenameList> renameList = new BindingList<RenameList>();
    public AdvancedOptions myAdvancedOptions = new AdvancedOptions();
    private IContainer components;
    private Button btnSelectFiles;
    private DataGridView dataGridView1;
    private ComboBox comboBox1;
    private ComboBox comboBox2;
    private ComboBox comboBox3;
    private ComboBox comboBox4;
    private DataGridViewTextBoxColumn SourceFile;
    private DataGridViewTextBoxColumn TargetFile;
    private Label label1;
    private TextBox txtDeliminator;
    private Button btnRename;
    private Button btnQuit;
    private Button btnLoadSession;
    public bool loadSession;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnSelectFiles = new Button();
      this.dataGridView1 = new DataGridView();
      this.SourceFile = new DataGridViewTextBoxColumn();
      this.TargetFile = new DataGridViewTextBoxColumn();
      this.comboBox1 = new ComboBox();
      this.comboBox2 = new ComboBox();
      this.comboBox3 = new ComboBox();
      this.comboBox4 = new ComboBox();
      this.label1 = new Label();
      this.txtDeliminator = new TextBox();
      this.btnRename = new Button();
      this.btnQuit = new Button();
      this.btnLoadSession = new Button();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.btnSelectFiles.Location = new Point(12, 12);
      this.btnSelectFiles.Name = "btnSelectFiles";
      this.btnSelectFiles.Size = new Size(75, 23);
      this.btnSelectFiles.TabIndex = 0;
      this.btnSelectFiles.Text = "Select Files";
      this.btnSelectFiles.UseVisualStyleBackColor = true;
      this.btnSelectFiles.Click += new EventHandler(this.btnSelectFiles_Click);
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.SourceFile, (DataGridViewColumn) this.TargetFile);
      this.dataGridView1.Location = new Point(12, 41);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(493, 359);
      this.dataGridView1.TabIndex = 1;
      this.SourceFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.SourceFile.DataPropertyName = "SourceFileName";
      this.SourceFile.HeaderText = "Original";
      this.SourceFile.Name = "SourceFile";
      this.SourceFile.Width = 67;
      this.TargetFile.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.TargetFile.DataPropertyName = "TargetFileName";
      this.TargetFile.HeaderText = "New Name";
      this.TargetFile.Name = "TargetFile";
      this.TargetFile.Width = 85;
      this.comboBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[5]
      {
        (object) "",
        (object) "Author",
        (object) "Title",
        (object) "Date",
        (object) "Track"
      });
      this.comboBox1.Location = new Point(12, 406);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(109, 21);
      this.comboBox1.TabIndex = 2;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.comboBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Items.AddRange(new object[5]
      {
        (object) "",
        (object) "Author",
        (object) "Title",
        (object) "Date",
        (object) "Track"
      });
      this.comboBox2.Location = new Point(137, 406);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new Size(109, 21);
      this.comboBox2.TabIndex = 3;
      this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
      this.comboBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox3.FormattingEnabled = true;
      this.comboBox3.Items.AddRange(new object[5]
      {
        (object) "",
        (object) "Author",
        (object) "Title",
        (object) "Date",
        (object) "Track"
      });
      this.comboBox3.Location = new Point(268, 406);
      this.comboBox3.Name = "comboBox3";
      this.comboBox3.Size = new Size(109, 21);
      this.comboBox3.TabIndex = 4;
      this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
      this.comboBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox4.FormattingEnabled = true;
      this.comboBox4.Items.AddRange(new object[5]
      {
        (object) "",
        (object) "Author",
        (object) "Title",
        (object) "Date",
        (object) "Track"
      });
      this.comboBox4.Location = new Point(396, 406);
      this.comboBox4.Name = "comboBox4";
      this.comboBox4.Size = new Size(109, 21);
      this.comboBox4.TabIndex = 5;
      this.comboBox4.SelectedIndexChanged += new EventHandler(this.comboBox4_SelectedIndexChanged);
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(415, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(62, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Deliminator:";
      this.txtDeliminator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.txtDeliminator.Location = new Point(483, 10);
      this.txtDeliminator.Name = "txtDeliminator";
      this.txtDeliminator.Size = new Size(23, 20);
      this.txtDeliminator.TabIndex = 7;
      this.txtDeliminator.Text = " - ";
      this.txtDeliminator.TextChanged += new EventHandler(this.txtDeliminator_TextChanged);
      this.btnRename.Anchor = AnchorStyles.Bottom;
      this.btnRename.Location = new Point(215, 436);
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new Size(75, 23);
      this.btnRename.TabIndex = 8;
      this.btnRename.Text = "Rename";
      this.btnRename.UseVisualStyleBackColor = true;
      this.btnRename.Click += new EventHandler(this.button1_Click);
      this.btnQuit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnQuit.Location = new Point(430, 436);
      this.btnQuit.Name = "btnQuit";
      this.btnQuit.Size = new Size(75, 23);
      this.btnQuit.TabIndex = 9;
      this.btnQuit.Text = "Exit";
      this.btnQuit.UseVisualStyleBackColor = true;
      this.btnQuit.Click += new EventHandler(this.btnQuit_Click);
      this.btnLoadSession.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnLoadSession.Location = new Point(12, 436);
      this.btnLoadSession.Name = "btnLoadSession";
      this.btnLoadSession.Size = new Size(164, 23);
      this.btnLoadSession.TabIndex = 10;
      this.btnLoadSession.Text = "Load this session into inAudible";
      this.btnLoadSession.UseVisualStyleBackColor = true;
      this.btnLoadSession.Click += new EventHandler(this.btnLoadSession_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(518, 467);
      this.Controls.Add((Control) this.btnLoadSession);
      this.Controls.Add((Control) this.btnQuit);
      this.Controls.Add((Control) this.btnRename);
      this.Controls.Add((Control) this.txtDeliminator);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.comboBox4);
      this.Controls.Add((Control) this.comboBox3);
      this.Controls.Add((Control) this.comboBox2);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.btnSelectFiles);
      this.Name = nameof (FormRenamer);
      this.Text = "File Renamer";
      this.FormClosing += new FormClosingEventHandler(this.FormRenamer_FormClosing);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormRenamer()
    {
      this.InitializeComponent();
    }

    public void LoadPrefs()
    {
      if (this.myAdvancedOptions.renameOptions == null)
        return;
      string[] strArray = this.myAdvancedOptions.renameOptions.Split(',');
      if (strArray.Length != 4)
        return;
      this.comboBox1.SelectedIndex = this.comboBox1.FindString(strArray[0].Trim());
      this.comboBox2.SelectedIndex = this.comboBox2.FindString(strArray[1].Trim());
      this.comboBox3.SelectedIndex = this.comboBox3.FindString(strArray[2].Trim());
      this.comboBox4.SelectedIndex = this.comboBox4.FindString(strArray[3].Trim());
    }

    private void btnSelectFiles_Click(object sender, EventArgs e)
    {
      CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
      commonOpenFileDialog.Multiselect = true;
      if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
        return;
      Stopwatch stopwatch = Stopwatch.StartNew();
      this.renameList.Clear();
      foreach (string fileName in commonOpenFileDialog.FileNames)
        this.renameList.Add(new RenameList(fileName, ""));
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.DataSource = (object) this.renameList;
      this.InitNewNames();
      stopwatch.Stop();
      Audible.diskLogger("Total load time = " + (object) ((double) stopwatch.ElapsedMilliseconds / 1000.0));
    }

    private void InitNewNames()
    {
      Stopwatch stopwatch1 = Stopwatch.StartNew();
      for (int index = 0; index < this.renameList.Count; ++index)
      {
        Audible myAudible = new Audible();
        Stopwatch stopwatch2 = Stopwatch.StartNew();
        myAudible.GetMetaDataTaglib(this.renameList[index].SourceFile);
        stopwatch2.Stop();
        string str = this.ConstructFileName(myAudible);
        this.renameList[index].TargetFileName = str + Path.GetExtension(this.renameList[index].SourceFile);
        this.renameList[index].Audible = myAudible;
      }
      stopwatch1.Stop();
      Audible.diskLogger("Total init time = " + (object) ((double) stopwatch1.ElapsedMilliseconds / 1000.0));
    }

    private void RegenerateNewNames()
    {
      for (int index = 0; index < this.renameList.Count; ++index)
      {
        string str = this.ConstructFileName(this.renameList[index].Audible);
        this.renameList[index].TargetFileName = str + Path.GetExtension(this.renameList[index].SourceFile);
      }
    }

    private string ConstructFileName(Audible myAudible)
    {
      string str1 = "";
      if (this.comboBox1.SelectedIndex > 0)
        str1 += this.FillComboValue(this.comboBox1.SelectedItem.ToString(), myAudible);
      if (this.comboBox2.SelectedIndex > 0)
      {
        string str2 = this.FillComboValue(this.comboBox2.SelectedItem.ToString(), myAudible);
        if (str1 != "" && str2 != "")
          str1 = str1 + this.txtDeliminator.Text + str2;
      }
      if (this.comboBox3.SelectedIndex > 0)
      {
        string str2 = this.FillComboValue(this.comboBox3.SelectedItem.ToString(), myAudible);
        if (str1 != "" && str2 != "")
          str1 = str1 + this.txtDeliminator.Text + str2;
      }
      if (this.comboBox4.SelectedIndex > 0)
      {
        string str2 = this.FillComboValue(this.comboBox4.SelectedItem.ToString(), myAudible);
        if (str1 != "" && str2 != "")
          str1 = str1 + this.txtDeliminator.Text + str2;
      }
      return str1;
    }

    private string FillComboValue(string value, Audible myAudible)
    {
      string str = "";
      switch (value)
      {
        case "":
          str = "";
          break;
        case "Author":
          str = myAudible.GetASCIITag(myAudible.author);
          break;
        case "Title":
          str = myAudible.GetASCIITag(myAudible.title);
          break;
        case "Date":
          str = myAudible.GetASCIITag(myAudible.year);
          break;
        case "Track":
          str = myAudible.GetASCIITag(myAudible.trackNum);
          break;
      }
      return str;
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.RegenerateNewNames();
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.RegenerateNewNames();
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.RegenerateNewNames();
    }

    private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.RegenerateNewNames();
    }

    private void txtDeliminator_TextChanged(object sender, EventArgs e)
    {
      this.RegenerateNewNames();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.DoRename();
    }

    private void DoRename()
    {
      for (int index = 0; index < this.renameList.Count; ++index)
      {
        string str = this.ConstructFileName(this.renameList[index].Audible);
        this.renameList[index].TargetFileName = str + Path.GetExtension(this.renameList[index].SourceFile);
        try
        {
          string destFileName = this.VerifyTargetFileName(Path.GetDirectoryName(this.renameList[index].SourceFile) + "\\" + str + Path.GetExtension(this.renameList[index].SourceFile));
          int num = 0;
          while (this.IsFileLocked(new FileInfo(this.renameList[index].SourceFile)) && num < 10)
          {
            ++num;
            Audible.diskLogger("file locked, retry count = " + (object) num);
            Thread.Sleep(1000);
          }
          File.Move(this.renameList[index].SourceFile, destFileName);
          this.renameList[index].SourceFileName = str + Path.GetExtension(this.renameList[index].SourceFile);
          this.renameList[index].SourceFile = destFileName;
        }
        catch (Exception ex)
        {
          Audible.diskLogger(ex.ToString());
        }
      }
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
      }
      catch (IOException ex)
      {
        return true;
      }
      finally
      {
        if (fileStream != null)
          fileStream.Close();
      }
      return false;
    }

    private string VerifyTargetFileName(string newWholeName)
    {
      string path = newWholeName;
      int num = 2;
      while (File.Exists(path))
      {
        path = Path.GetDirectoryName(newWholeName) + "\\" + Path.GetFileNameWithoutExtension(newWholeName) + " (" + (object) num + ")" + Path.GetExtension(newWholeName);
        ++num;
      }
      return path;
    }

    private void FormRenamer_FormClosing(object sender, FormClosingEventArgs e)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.comboBox1.SelectedItem != null)
        stringBuilder.Append(this.comboBox1.SelectedItem.ToString() + ",");
      else
        stringBuilder.Append(",");
      if (this.comboBox2.SelectedItem != null)
        stringBuilder.Append(this.comboBox2.SelectedItem.ToString() + ",");
      else
        stringBuilder.Append(",");
      if (this.comboBox3.SelectedItem != null)
        stringBuilder.Append(this.comboBox3.SelectedItem.ToString() + ",");
      else
        stringBuilder.Append(",");
      if (this.comboBox4.SelectedItem != null)
        stringBuilder.Append(this.comboBox4.SelectedItem.ToString());
      this.myAdvancedOptions.renameOptions = stringBuilder.ToString();
    }

    private void btnQuit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnLoadSession_Click(object sender, EventArgs e)
    {
      this.loadSession = true;
      this.Close();
    }
  }
}
