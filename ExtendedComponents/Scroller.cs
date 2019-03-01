// Decompiled with JetBrains decompiler
// Type: ExtendedComponents.Scroller
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedComponents
{
  public class Scroller : UserControl
  {
    private string[] m_text = new string[0];
    private int m_topPartSizePercent = 50;
    private Font m_font = new Font("Arial", 20f, FontStyle.Bold, GraphicsUnit.Pixel);
    private int m_scrollingOffset;
    private IContainer components;
    private Timer m_timer;

    public Scroller()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public string TextToScroll
    {
      get
      {
        return string.Join("\n", this.m_text);
      }
      set
      {
        this.m_text = value.Split('\n');
      }
    }

    public int Interval
    {
      get
      {
        return this.m_timer.Interval;
      }
      set
      {
        this.m_timer.Interval = value;
      }
    }

    public Font TextFont
    {
      get
      {
        return this.m_font;
      }
      set
      {
        this.m_font = value;
      }
    }

    public int TopPartSizePercent
    {
      get
      {
        return this.m_topPartSizePercent;
      }
      set
      {
        if (value < 10 || value > 100)
          throw new InvalidEnumArgumentException("The value must be more than zero. and less than 100.");
        this.m_topPartSizePercent = value;
      }
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.FillRectangle((Brush) new SolidBrush(this.BackColor), this.ClientRectangle);
      GraphicsPath path = new GraphicsPath();
      int num1 = 0;
      for (int index = this.m_text.Length - 1; index >= 0; --index)
      {
        Point origin = new Point((int) (((double) this.ClientSize.Width - (double) e.Graphics.MeasureString(this.m_text[index], this.m_font).Width) / 2.0), (int) ((double) (this.m_scrollingOffset + this.ClientSize.Height) - (double) (this.m_text.Length - index) * (double) this.m_font.Size));
        if ((double) origin.Y + (double) this.Font.Size > 0.0 && origin.Y < this.Height)
        {
          path.AddString(this.m_text[index], this.m_font.FontFamily, (int) this.m_font.Style, this.m_font.Size, origin, StringFormat.GenericTypographic);
          ++num1;
        }
      }
      if (num1 == 0 && this.m_scrollingOffset < 0)
        this.m_scrollingOffset = (int) this.Font.SizeInPoints * this.m_text.Length;
      int num2 = (int) ((double) (this.Width * this.m_topPartSizePercent) / 100.0);
      path.Warp(new PointF[4]
      {
        new PointF((float) ((this.Width - num2) / 2), 0.0f),
        new PointF((float) (this.Width - (this.Width - num2) / 2), 0.0f),
        new PointF(0.0f, (float) this.Height),
        new PointF((float) this.Width, (float) this.Height)
      }, new RectangleF((float) this.ClientRectangle.X, (float) this.ClientRectangle.Y, (float) this.ClientRectangle.Width, (float) this.ClientRectangle.Height), (Matrix) null, WarpMode.Perspective);
      e.Graphics.FillPath((Brush) new SolidBrush(this.ForeColor), path);
      path.Dispose();
      using (Brush brush = (Brush) new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.FromArgb((int) byte.MaxValue, this.BackColor), Color.FromArgb(0, this.BackColor)))
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
    }

    public void Start()
    {
      this.m_scrollingOffset = (int) this.Font.SizeInPoints * this.m_text.Length;
      this.m_timer.Start();
    }

    public void Stop()
    {
      this.m_timer.Stop();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      --this.m_scrollingOffset;
      this.Invalidate();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.m_timer = new Timer(this.components);
      this.SuspendLayout();
      this.m_timer.Interval = 50;
      this.m_timer.Tick += new EventHandler(this.OnTimerTick);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.Name = nameof (Scroller);
      this.Size = new Size(447, 429);
      this.Paint += new PaintEventHandler(this.OnPaint);
      this.ResumeLayout(false);
    }
  }
}
