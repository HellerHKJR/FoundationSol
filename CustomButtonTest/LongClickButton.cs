using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomButtonTest
{
    public delegate void ButtonOnOffw(string tag, bool isOn);
    public partial class LongClickButton : UserControl
    {
        public event ButtonOnOff OnOff;
        bool isDown = false;

        public LongClickButton() : base()
        {
            SetStyle(ControlStyles.Opaque, false);
        }

        bool isLampOn = false;
        public bool IsLampOn { set { if (isLampOn != value) { isLampOn = value; Invalidate(); } } }

        protected override void OnPrint(PaintEventArgs e)
        {
            Console.WriteLine("OnPrint");
            base.OnPrint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Console.WriteLine("OnPaintBackground");

            //base.OnPaintBackground(pevent);
            Image image = MakeBitmap(this.ClientSize.Width, this.ClientSize.Height, true, true);
            pevent.Graphics.DrawImage(image, 0, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            Console.WriteLine("OnResize");
            base.OnResize(e);
            Invalidate();
        }

        

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Console.WriteLine("OnPaint");
            //base.OnPaint(pevent);
            pevent.Graphics.DrawString(this.Tag.ToString(), this.Font, new SolidBrush(this.ForeColor), 0, 0);

        }


        Bitmap MakeBitmap(int width, int height, bool asserted, bool mouseDown)
        {
            Bitmap bmp = new Bitmap(width, height);
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

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            isDown = true;
            Invalidate();
            OnOff?.Invoke(this.Tag.ToString(), true);

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            isDown = false;
            Invalidate();
            OnOff?.Invoke(this.Tag.ToString(), false);

            base.OnMouseUp(mevent);
        }

    }
}
