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
        private bool isPanel2Visible = false;

        public Form2()
        {
            InitializeComponent();

            //this.DoubleBuffered = true;

            // Resize 이벤트 핸들러를 연결 
            this.Resize += Form2_Resize;

            // 초기 폼 크기를 설정 
            this.Size = new Size(350, 500);

            // 초기 너비를 기반으로 패널 가시성을 설정 
            UpdatePanelVisibility();

            // tableLayoutPanel3를 먼저 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel4);
            
            // tableLayoutPanel3를 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel3);

            splitContainer1.Panel1.Controls.Add(tableLayoutPanel5);

            // tableLayoutPanel2를 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);

            // tableLayoutPanel1을 다음에 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
                       
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            // 폼 크기가 변경될 때마다 가시성을 업데이트 
            UpdatePanelVisibility();
            UpdateControlFont();
        }
        private void UpdateControlFont()
        {
            // 폼의 높이에 따라 폰트 크기를 동적으로 조절
            int fontSize = Convert.ToInt32(this.Height * 0.05); // 높이의 5% 크기로 설정
            textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize, textBox1.Font.Style);
        }

        private void UpdatePanelVisibility()
        {
            // 폼의 너비를 확인 
            if (this.Width > 600)
            {
                // 너비가 600보다 크면 splitContainer.Panel2를 보이게 설정하고
                // splitContainer.Panel2의 너비를 300으로 고정하며, splitContainer.Panel1의 크기를 동적으로 조절 
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.SplitterDistance = this.Width - 350; // splitContainer.Panel1의 크기를 동적으로 조절
                isPanel2Visible = true;
            }
            else
            {
                // 너비가 600 이하이면 splitContainer.Panel2를 감추게 설정하고
                // splitContainer.Panel1의 크기를 동적으로 조절 
                splitContainer1.Panel2Collapsed = true;
                isPanel2Visible = false;
            }

            int y1 = Convert.ToInt32(this.Height * 0.4);
            tableLayoutPanel1.Height = y1;
            int y2 = Convert.ToInt32(this.Height * 0.05);
            tableLayoutPanel2.Height = y2; 
            int y3 = Convert.ToInt32(this.Height * 0.05);
            tableLayoutPanel3.Height = y3; 
            int y4 = Convert.ToInt32(this.Height * 0.2);
            tableLayoutPanel4.Height = y4;
            int y5 = Convert.ToInt32(this.Height * 0.45);
            tableLayoutPanel5.Height = y5;
            //Console.WriteLine(y);
            textBox1.Location = new Point(0, y4);

            // splitContainer.Panel2가 나타날 때만 tableLayoutPanel3의 열 구조를 변경
            if (isPanel2Visible)
            {
                // tableLayoutPanel3을 4열로 변경
                ChangeTableLayoutPanel3ColumnCount(4);
            }
            else
            {
                // tableLayoutPanel3을 5열로 변경
                ChangeTableLayoutPanel3ColumnCount(5);
            }

        }

        private void ChangeTableLayoutPanel3ColumnCount(int columnCount)
        {
            // tableLayoutPanel3의 열 구조를 변경
            tableLayoutPanel3.ColumnCount = columnCount;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new System.Drawing.Size(350, 550);
            timer1.Start();

            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel3.Dock = DockStyle.Bottom;
            tableLayoutPanel4.Dock = DockStyle.Bottom;
            tableLayoutPanel5.Dock = DockStyle.Bottom;
            textBox1.Dock = DockStyle.Bottom;
           
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button33_MouseHover(object sender, EventArgs e)
        {
            button33.BackgroundImage= pictureBox1.Image;
        }

        private void button33_MouseLeave(object sender, EventArgs e)
        {
            button33.BackgroundImage = pictureBox2.Image;
        }

        private void button34_MouseHover(object sender, EventArgs e)
        {
            button34.BackgroundImage = pictureBox3.Image;
        }

        private void button34_MouseLeave(object sender, EventArgs e)
        {
            button34.BackgroundImage = pictureBox4.Image;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = false;
            tableLayoutPanel5.Visible = true;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = true;
            tableLayoutPanel5.Visible = false;
        }

    }
}
