using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomButtonTest
{
    public partial class LongClickButton3 : Component
    {
        public LongClickButton3()
        {
            InitializeComponent();
        }

        public LongClickButton3(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
