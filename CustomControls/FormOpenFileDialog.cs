// Decompiled with JetBrains decompiler
// Type: CustomControls.FormOpenFileDialog
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using CustomControls.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CustomControls
{
  public class FormOpenFileDialog : OpenFileDialogEx
  {
    private IContainer components;
    private GroupBox groupBox1;
    private Label lblColors;
    private PictureBox pbxPreview;
    private Label lblFormat;
    private Label lblSize;
    private Label lblSizeValue;
    private Label lblFormatValue;
    private Label lblColorsValue;
    private Label lblResolution;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.groupBox1 = new GroupBox();
      this.pbxPreview = new PictureBox();
      this.lblColors = new Label();
      this.lblFormat = new Label();
      this.lblSize = new Label();
      this.lblSizeValue = new Label();
      this.lblFormatValue = new Label();
      this.lblColorsValue = new Label();
      this.lblResolution = new Label();
      this.groupBox1.SuspendLayout();
      ((ISupportInitialize) this.pbxPreview).BeginInit();
      this.SuspendLayout();
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.pbxPreview);
      this.groupBox1.Location = new Point(5, 30);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(226, 220);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Preview";
      this.pbxPreview.Dock = DockStyle.Fill;
      this.pbxPreview.Location = new Point(3, 16);
      this.pbxPreview.Name = "pbxPreview";
      this.pbxPreview.Size = new Size(220, 201);
      this.pbxPreview.SizeMode = PictureBoxSizeMode.Zoom;
      this.pbxPreview.TabIndex = 4;
      this.pbxPreview.TabStop = false;
      this.lblColors.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblColors.Location = new Point(2, 296);
      this.lblColors.Name = "lblColors";
      this.lblColors.Size = new Size(42, 13);
      this.lblColors.TabIndex = 3;
      this.lblColors.Text = "Colors:";
      this.lblFormat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblFormat.Location = new Point(2, 260);
      this.lblFormat.Name = "lblFormat";
      this.lblFormat.Size = new Size(42, 13);
      this.lblFormat.TabIndex = 4;
      this.lblFormat.Text = "Format:";
      this.lblSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblSize.Location = new Point(2, 278);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new Size(42, 13);
      this.lblSize.TabIndex = 5;
      this.lblSize.Text = "Size:";
      this.lblSizeValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblSizeValue.Location = new Point(50, 278);
      this.lblSizeValue.Name = "lblSizeValue";
      this.lblSizeValue.Size = new Size(35, 13);
      this.lblSizeValue.TabIndex = 8;
      this.lblFormatValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblFormatValue.Location = new Point(50, 260);
      this.lblFormatValue.Name = "lblFormatValue";
      this.lblFormatValue.Size = new Size(178, 13);
      this.lblFormatValue.TabIndex = 7;
      this.lblColorsValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblColorsValue.Location = new Point(50, 296);
      this.lblColorsValue.Name = "lblColorsValue";
      this.lblColorsValue.Size = new Size(178, 13);
      this.lblColorsValue.TabIndex = 6;
      this.lblResolution.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblResolution.Location = new Point(101, 278);
      this.lblResolution.Name = "lblResolution";
      this.lblResolution.Size = new Size(64, 13);
      this.lblResolution.TabIndex = 9;
      this.Controls.Add((Control) this.lblResolution);
      this.Controls.Add((Control) this.lblSizeValue);
      this.Controls.Add((Control) this.lblFormatValue);
      this.Controls.Add((Control) this.lblColorsValue);
      this.Controls.Add((Control) this.lblSize);
      this.Controls.Add((Control) this.lblFormat);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.lblColors);
      this.Name = nameof (FormOpenFileDialog);
      this.Size = new Size(237, 318);
      this.groupBox1.ResumeLayout(false);
      ((ISupportInitialize) this.pbxPreview).EndInit();
      this.ResumeLayout(false);
    }

    public FormOpenFileDialog()
    {
      this.InitializeComponent();
    }

    public override void OnFileNameChanged(string filePath)
    {
      if (filePath.ToLower().EndsWith(".bmp") || filePath.ToLower().EndsWith(".jpg") || (filePath.ToLower().EndsWith(".png") || filePath.ToLower().EndsWith(".tif")) || filePath.ToLower().EndsWith(".gif"))
      {
        if (this.pbxPreview.Image != null)
          this.pbxPreview.Image.Dispose();
        try
        {
          FileInfo fileInfo = new FileInfo(filePath);
          this.pbxPreview.Image = Image.FromFile(filePath);
          this.lblSizeValue.Text = (fileInfo.Length / 1024L).ToString() + "KB";
          this.lblColorsValue.Text = this.GetColorsCountFromImage(this.pbxPreview.Image);
          this.lblFormatValue.Text = this.GetFormatFromImage(this.pbxPreview.Image);
          this.lblResolution.Text = "(" + (object) Image.FromFile(filePath).Width + "x" + (object) Image.FromFile(filePath).Height + ")";
        }
        catch (Exception ex)
        {
        }
      }
      else
      {
        if (this.pbxPreview.Image != null)
          this.pbxPreview.Image.Dispose();
        this.pbxPreview.Image = (Image) null;
      }
    }

    public override void OnFolderNameChanged(string folderName)
    {
      if (this.pbxPreview.Image != null)
        this.pbxPreview.Image.Dispose();
      this.pbxPreview.Image = (Image) null;
      this.lblSizeValue.Text = string.Empty;
      this.lblColorsValue.Text = string.Empty;
      this.lblFormatValue.Text = string.Empty;
    }

    public override void OnClosingDialog()
    {
      if (this.pbxPreview.Image == null)
        return;
      this.pbxPreview.Image.Dispose();
    }

    private string GetColorsCountFromImage(Image image)
    {
      switch (image.PixelFormat)
      {
        case PixelFormat.Format32bppPArgb:
        case PixelFormat.Format32bppArgb:
        case PixelFormat.Format32bppRgb:
          return "32 bits (Alpha Channel)";
        case PixelFormat.Format16bppGrayScale:
        case PixelFormat.Format16bppArgb1555:
        case PixelFormat.Format16bppRgb555:
        case PixelFormat.Format16bppRgb565:
          return "16 bits (65536 colors)";
        case PixelFormat.Format8bppIndexed:
          return "8 bits (256 colors)";
        case PixelFormat.Format1bppIndexed:
          return "1 bit (Black & White)";
        case PixelFormat.Format4bppIndexed:
          return "4 bits (16 colors)";
        case PixelFormat.Format24bppRgb:
          return "24 bits (True Colors)";
        default:
          return string.Empty;
      }
    }

    private string GetFormatFromImage(Image image)
    {
      if (image.RawFormat.Equals((object) ImageFormat.Bmp))
        return "BMP";
      if (image.RawFormat.Equals((object) ImageFormat.Gif))
        return "GIF";
      if (image.RawFormat.Equals((object) ImageFormat.Jpeg))
        return "JPG";
      if (image.RawFormat.Equals((object) ImageFormat.Png))
        return "PNG";
      if (image.RawFormat.Equals((object) ImageFormat.Tiff))
        return "TIFF";
      return string.Empty;
    }
  }
}
