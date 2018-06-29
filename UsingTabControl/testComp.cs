using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace UsingTabControl
{
    public partial class testComp : Component
    {
        public testComp()
        {
            InitializeComponent();
        }

        public testComp(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
