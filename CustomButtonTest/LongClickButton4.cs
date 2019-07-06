using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CustomButtonTest
{
    public delegate void ButtonOnOff(string tag, bool isOn);
    public class LongClickButton4 : Control
    {
        public event ButtonOnOff OnOff;
        bool isDown = false;
        bool isLampOn = false;
        public bool IsLampOn { set { if (isLampOn != value) { isLampOn = value; Invalidate(); } } }

        public LongClickButton4()
        {
            SetStyle(ControlStyles.Opaque, true);
            Tag = this.Text.Replace(" ", "");
        }

        public override string Text { get => base.Text; set { base.Text = value; Tag = value.Replace(" ", ""); Invalidate(); } }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isDown = true;
            Invalidate();
            OnOff?.Invoke(this.Tag.ToString(), true);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isDown = false;
            Invalidate();
            OnOff?.Invoke(this.Tag.ToString(), false);
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // paint lamp button image
            Image image = MakeBitmap();
            e.Graphics.DrawImage(image, 0, 0);

            // if text to paint
            if (this.Text.Length > 0)
            {
                SizeF size = e.Graphics.MeasureString(this.Text, this.Font);
                
                // if text narrower than client area or text has no spaces
                if (size.Width < this.ClientSize.Width ||
                    this.Text.IndexOf(' ') == -1)
                {
                    // Center the text inside the client area of the LampButton.
                    e.Graphics.DrawString(
                        this.Text,
                        this.Font,
                        new SolidBrush(this.ForeColor),
                        (this.ClientSize.Width - size.Width) / 2,
                        (this.ClientSize.Height - size.Height) / 2);
                }
                // if text wider than client area and text has spaces
                else
                {
                    // determine where to break the string
                    int halfWay = this.Text.Length / 2;
                    int afterHalfWay = this.Text.IndexOf(' ', halfWay);
                    int ndx;
                    int beforeHalfWay = -1;
                    for (ndx = halfWay; ndx >= 0; ndx--)
                    {
                        beforeHalfWay = this.Text.IndexOf(' ', ndx);
                        if (beforeHalfWay != afterHalfWay)
                            break;
                    }
                    string s1, s2;
                    // if no space after text half way point
                    if (afterHalfWay == -1)
                    {
                        s1 = this.Text.Substring(0, beforeHalfWay);
                        s2 = this.Text.Substring(beforeHalfWay + 1);
                    }
                    // if space after half way point
                    else
                    {
                        // break text on space closest to half way point
                        int beforeDelta = halfWay - beforeHalfWay;
                        int afterDelta = afterHalfWay - halfWay;
                        if (beforeDelta <= afterDelta)
                        {
                            s1 = this.Text.Substring(0, beforeHalfWay);
                            s2 = this.Text.Substring(beforeHalfWay + 1);
                        }
                        else
                        {
                            s1 = this.Text.Substring(0, afterHalfWay);
                            s2 = this.Text.Substring(afterHalfWay + 1);
                        }
                    }
                    Debug.WriteLine("beforeHalfWay=" + beforeHalfWay.ToString() + " afterHalfWay=" + afterHalfWay.ToString() + " s1=" + s1 + " s2=" + s2);

                    size = e.Graphics.MeasureString(s1, this.Font);
                    e.Graphics.DrawString(
                        s1,
                        this.Font,
                        new SolidBrush(this.ForeColor),
                        (this.ClientSize.Width - size.Width) / 2,
                        (this.ClientSize.Height - size.Height) / 2 - size.Height / 2);

                    size = e.Graphics.MeasureString(s2, this.Font);
                    e.Graphics.DrawString(
                        s2,
                        this.Font,
                        new SolidBrush(this.ForeColor),
                        (this.ClientSize.Width - size.Width) / 2,
                        (this.ClientSize.Height - size.Height) / 2 + size.Height / 2);
                }
            }

            base.OnPaint(e);
        }

        //---------------------------------------------------------------------
        // Method MakeBitmap
        //
        // Create conventional button plus lamp box in upper left corner conditioned on boolean asserted
        // Implement button depressed effect on boolean mouseDown
        //---------------------------------------------------------------------
        Bitmap MakeBitmap()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.DimGray), 0, 0, bmp.Width, bmp.Height);
            if (isDown)
                g.FillRectangle(new SolidBrush(Color.Magenta), 1, 1, bmp.Width - 2, bmp.Height - 2);
            else
                g.FillRectangle(new SolidBrush(Color.White), 1, 1, bmp.Width - 2, bmp.Height - 2);
            g.FillRectangle(new SolidBrush(Color.DimGray), 2, 2, bmp.Width - 4, bmp.Height - 4);
            g.FillRectangle(new SolidBrush(Color.White), 2, 2, 10, 10);
            if (isLampOn)
                g.FillRectangle(new SolidBrush(Color.LimeGreen), 2, 2, 9, 9);
            else
                g.FillRectangle(new SolidBrush(Color.DarkGreen), 2, 2, 9, 9);
            g.Dispose();
            return bmp;
        }
    }
}
