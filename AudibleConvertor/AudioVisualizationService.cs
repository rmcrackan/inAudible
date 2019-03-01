// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.AudioVisualizationService
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AudibleConvertor
{
  public static class AudioVisualizationService
  {
    public static void DrawWave(float[] data, ref Bitmap bitmap, AudioVisualizationService.WaveVisualizationConfiguration config = null)
    {
      Color color1 = Color.FromArgb(2139606778);
      Color color2 = Color.DarkSlateBlue;
      int num1 = 2;
      int num2 = 2;
      double num3 = 0.100000001490116;
      Rectangle rectangle = Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height);
      if (config != null)
      {
        num1 = config.EdgeSize;
        if (config.AreaColor.HasValue)
          color1 = config.AreaColor.GetValueOrDefault();
        if (config.EdgeColor.HasValue)
          color2 = config.EdgeColor.GetValueOrDefault();
        if (config.Bounds.HasValue)
          rectangle = config.Bounds.GetValueOrDefault();
        num2 = Math.Max(1, config.Step);
        num3 = config.Overlap;
      }
      float width = (float) rectangle.Width;
      float height = (float) rectangle.Height;
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        Pen pen = new Pen(color2);
        pen.LineJoin = LineJoin.Round;
        pen.Width = (float) num1;
        Brush brush = (Brush) new SolidBrush(color1);
        float length = (float) data.Length;
        PointF[] pointFArray1 = new PointF[(int) width / num2];
        PointF[] pointFArray2 = new PointF[(int) width / num2];
        int index = 0;
        float num4 = 0.0f;
        while ((double) num4 < (double) width)
        {
          int num5 = (int) ((double) num4 * ((double) length / (double) width));
          int num6 = (int) (((double) num4 + (double) num2) * ((double) length / (double) width));
          int num7 = num6 - num5;
          int startIndex = num5 - (int) (num3 * (double) num7);
          int endIndex = num6 + (int) (num3 * (double) num7);
          if (startIndex < 0)
            startIndex = 0;
          if (endIndex > data.Length)
            endIndex = data.Length;
          float posAvg;
          float negAvg;
          AudioVisualizationService.averages(data, startIndex, endIndex, out posAvg, out negAvg);
          float y1 = height - (float) (((double) posAvg + 1.0) * 0.5) * height;
          float y2 = height - (float) (((double) negAvg + 1.0) * 0.5) * height;
          float x = num4 + (float) rectangle.Left;
          if (index >= pointFArray1.Length)
            index = pointFArray1.Length - 1;
          pointFArray1[index] = new PointF(x, y1);
          pointFArray2[pointFArray2.Length - index - 1] = new PointF(x, y2);
          ++index;
          num4 += (float) num2;
        }
        PointF[] points = new PointF[pointFArray1.Length * 2];
        Array.Copy((Array) pointFArray1, (Array) points, pointFArray1.Length);
        Array.Copy((Array) pointFArray2, 0, (Array) points, pointFArray1.Length, pointFArray2.Length);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.FillClosedCurve(brush, points, FillMode.Winding, 0.15f);
        if (num1 <= 0)
          return;
        graphics.DrawClosedCurve(pen, points, 0.15f, FillMode.Winding);
      }
    }

    private static void averages(float[] data, int startIndex, int endIndex, out float posAvg, out float negAvg)
    {
      posAvg = 0.0f;
      negAvg = 0.0f;
      int num1 = 0;
      int num2 = 0;
      for (int index = startIndex; index < endIndex; ++index)
      {
        if ((double) data[index] > 0.0)
        {
          ++num1;
          posAvg += data[index];
        }
        else
        {
          ++num2;
          negAvg += data[index];
        }
      }
      if (num1 > 0)
        posAvg /= (float) num1;
      if (num2 <= 0)
        return;
      negAvg /= (float) num2;
    }

    public class WaveVisualizationConfiguration
    {
      public Color? AreaColor { get; set; }

      public Color? EdgeColor { get; set; }

      public int EdgeSize { get; set; }

      public Rectangle? Bounds { get; set; }

      public double Overlap { get; set; }

      public int Step { get; set; }
    }
  }
}
