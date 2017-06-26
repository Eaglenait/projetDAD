using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dechifr_client
{
    public partial class BfControl : UserControl
    {
        public BfControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// sets the filename label
        /// </summary>
        /// <param name="i">filename</param>
        public void setLabel(string i)
        {
            label1.Text = i;
        }
    }
}
