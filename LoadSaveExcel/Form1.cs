using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace LoadSaveExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.ApplicationClass xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Open(
                @"C:\Personal\Projects\FoundationSol\book2.xlsx", Type.Missing, false); 
                //0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Sheet1"];

            range = xlWorkSheet.UsedRange;
            int rw = range.Rows.Count;
            int cl = range.Columns.Count;

            for( int rCnt = 1; rCnt <= rw; rCnt++ )
                for( int cCnt = 1; cCnt <= cl; cCnt++ )
                {
                    Console.WriteLine( 
                        (range.Cells[rCnt, cCnt] as Excel.Range).Value2.ToString() + "\t" +
                        range.Cells[rCnt, cCnt].ToString() + "\t" +
                        (range.Cells[rCnt, cCnt] as Excel.Range).Value.ToString() + "\t" +
                        (range.Cells[rCnt, cCnt] as Excel.Range).Formula.ToString() + "\t"
                        );

                    if( cCnt == cl ) (range.Cells[rCnt, cCnt] as Excel.Range).Value2 = 1234;
                }


            xlWorkBook.SaveAs(@"C:\Personal\Projects\FoundationSol\book3.xlsx");
            xlWorkBook.Close(true);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Clear();

            Console.WriteLine(list.ToArray());
        }
    }
}
