// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.DataGridViewProgressCell
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AudibleConvertor
{
  internal class DataGridViewProgressCell : DataGridViewImageCell
  {
    private static Image emptyImage = (Image) new Bitmap(1, 1, PixelFormat.Format32bppArgb);

    public DataGridViewProgressCell()
    {
      this.ValueType = typeof (int);
    }

    protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
    {
      return (object) DataGridViewProgressCell.emptyImage;
    }

    protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
      try
      {
        int num1 = (int) value;
        float num2 = (float) num1 / 100f;
        SolidBrush solidBrush = new SolidBrush(cellStyle.BackColor);
        Brush brush = (Brush) new SolidBrush(cellStyle.ForeColor);
        base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);
        if ((double) num2 > 0.0)
        {
          g.FillRectangle((Brush) new SolidBrush(Color.FromArgb(203, 235, 108)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((float) ((double) num2 * (double) cellBounds.Width - 4.0)), cellBounds.Height - 4);
          g.DrawString(num1.ToString() + "%", cellStyle.Font, brush, (float) (cellBounds.X + cellBounds.Width / 2 - 5), (float) (cellBounds.Y + 4));
        }
        else if (this.DataGridView.CurrentRow.Index == rowIndex)
          g.DrawString(num1.ToString() + "%", cellStyle.Font, brush, (float) (cellBounds.X + cellBounds.Width / 2 - 5), (float) (cellBounds.Y + 4));
        else
          g.DrawString(num1.ToString() + "%", cellStyle.Font, brush, (float) (cellBounds.X + cellBounds.Width / 2 - 5), (float) (cellBounds.Y + 4));
      }
      catch
      {
      }
    }
  }
}
