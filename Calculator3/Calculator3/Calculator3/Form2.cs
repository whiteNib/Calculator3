using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        static public void ShowSplitContainerPanel(SplitContainer control, int panelIndex, bool show)
        {
            Form form = new Form();

            if (form.Width >= 600)
            {
                if (show)
                {
                    control.Panel1Collapsed = false;
                    control.Panel1.Show();
                }
                else
                {
                    control.Panel1Collapsed = true;
                    control.Panel1.Hide();
                }
            }
            else if (panelIndex == 2)
            {
                if (show)
                {
                    control.Panel2Collapsed = false;
                    control.Panel2.Show();
                }
                else
                {
                    control.Panel2Collapsed = true;
                    control.Panel2.Hide();
                }
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowSplitContainerPanel(splitContainer1, 1, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowSplitContainerPanel(splitContainer1, 1, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowSplitContainerPanel(splitContainer1, 2, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowSplitContainerPanel(splitContainer1, 2, false);
        }
        
    }
}
