using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Numerics;

namespace Calculator3
{
    public partial class Form2 : Form
    {
        private bool isPanel2Visible = false;
        public long lValue;
        public char op = '\0';
        public bool opFlag = false;
        public long memory;
        public bool memFlag = false;

        public Form2()
        {
            this.KeyPreview = true;  // 폼이 키보드 입력을 받을 수 있도록 설정
            InitializeComponent();

            btnMhistory.Enabled = false;

            //this.DoubleBuffered = true;

            // Resize 이벤트 핸들러를 연결 
            this.Resize += Form2_Resize;

            // 초기 폼 크기를 설정 
            this.Size = new Size(350, 500);

            // 초기 너비를 기반으로 패널 가시성을 설정 
            UpdatePanelVisibility();

            // tableLayoutPanel4를 먼저 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel4);
            
            // tableLayoutPanel3를 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel3);

            // tableLayoutPanel5를 추가
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
            txtResult.Font = new Font(txtResult.Font.FontFamily, fontSize, txtResult.Font.Style);
        }

        private void UpdatePanelVisibility()
        {
            // 폼의 너비를 확인 
            if (this.Width > 700)
            {
                // 너비가 700보다 크면 splitContainer.Panel2를 보이게 설정하고
                // splitContainer.Panel2의 너비를 350으로 고정하며, splitContainer.Panel1의 크기를 동적으로 조절 
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.SplitterDistance = this.Width - 350; // splitContainer.Panel1의 크기를 동적으로 조절
                isPanel2Visible = true;
            }
            else
            {
                // 너비가 700 이하이면 splitContainer.Panel2를 감추게 설정하고
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
            int y5 = Convert.ToInt32(this.Height * 0.55);
            tableLayoutPanel5.Height = y5;
            //Console.WriteLine(y);
            txtResult.Location = new Point(0, y4);

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
            txtResult.Dock = DockStyle.Bottom;

            tableLayoutPanel5.Visible = false;
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

        string strText = "";
        string strNum = "";
        long hex;
        long dec;
        long oct;
        long bin;

        private void btn0_Click(object sender, EventArgs e)
        {
            Btn0Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "0";
            btnDEC.Text += "0";
            btnOCT.Text += "0";
            btnBIN.Text += "0";

            //Console.WriteLine(btnHEX.Text);

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }


        private void btn1_Click(object sender, EventArgs e)
        {
            if (txtResult.Text == "0" || opFlag || memFlag)
            {
                txtResult.Text = "1";
                opFlag = false;
                memFlag = false;
            }
            else
            {
                txtResult.Text += "1";
            }

            btnHEX.Text += "1";
            btnDEC.Text += "1";
            btnOCT.Text += "1";
            btnBIN.Text += "1";

            strText = txtResult.Text;
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
            
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Btn2Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "2";
            btnDEC.Text += "2";
            btnOCT.Text += "2";
            btnBIN.Text += "2";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Btn3Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "3";
            btnDEC.Text += "3";
            btnOCT.Text += "3";
            btnBIN.Text += "3";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Btn4Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "4";
            btnDEC.Text += "4";
            btnOCT.Text += "4";
            btnBIN.Text += "4";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Btn5Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "5";
            btnDEC.Text += "5";
            btnOCT.Text += "5";
            btnBIN.Text += "5";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Btn6Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "6";
            btnDEC.Text += "6";
            btnOCT.Text += "6";
            btnBIN.Text += "6";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Btn7Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "7";
            btnDEC.Text += "7";
            btnOCT.Text += "7";
            btnBIN.Text += "7";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Btn8Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "8";
            btnDEC.Text += "8";
            btnOCT.Text += "8";
            btnBIN.Text += "8";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            toHEX();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            Btn9Handler.HandleClick(sender, e, txtResult, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
            btnHEX.Text += "9";
            btnDEC.Text += "9";
            btnOCT.Text += "9";
            btnBIN.Text += "9";

            strText = txtResult.Text;
            strNum = "";
            strNum = Regex.Replace(strText, @"\D", "");
            hex = Convert.ToInt64(strNum);
            dec = Convert.ToInt64(strNum);
            oct = Convert.ToInt64(strNum);
            bin = Convert.ToInt64(strNum);

            
            toHEX();
        }

        public void toHEX()
        {
            // 16진수 변환
            string hexString = hex.ToString("X"); // 4자리 이상도 모두 포함하기 위해 "X" 사용
            StringBuilder formattedHex = new StringBuilder();

            int digitCount = 0;

            for (int i = hexString.Length - 1; i >= 0; i--)
            {
                formattedHex.Insert(0, hexString[i]);

                // 4자리 이상의 자릿수에 대해서 공백 추가
                if (++digitCount > 3 && i > 0)
                {
                    formattedHex.Insert(0, " ");
                    digitCount = 0;
                }
            }

            int octDigitCount = 0;
            StringBuilder formattedOct = new StringBuilder();

            // 10진수 변환
            string decString = Convert.ToString(hex, 10);
            StringBuilder formattedDec = new StringBuilder();

            int decDigitCount = 0;

            for (int i = decString.Length - 1; i >= 0; i--)
            {
                formattedDec.Insert(0, decString[i]);

                // 3자리 이상의 자릿수에 대해서 쉼표 추가
                if (++decDigitCount > 2 && i > 0)
                {
                    formattedDec.Insert(0, ",");
                    decDigitCount = 0;
                }
            }

            // 8진수 변환
            string octString = Convert.ToString(hex, 8);

            for (int i = octString.Length - 1; i >= 0; i--)
            {
                formattedOct.Insert(0, octString[i]);

                // 3자리 이상의 자릿수에 대해서 공백 추가
                if (++octDigitCount > 2 && i > 0)
                {
                    formattedOct.Insert(0, " ");
                    octDigitCount = 0;
                }
            }

            // 2진수 변환
            string binString = Convert.ToString(bin, 2);
            StringBuilder formattedBin = new StringBuilder();

            int binDigitCount = 0;

            for (int i = binString.Length - 1; i >= 0; i--)
            {
                formattedBin.Insert(0, binString[i]);

                // 4자리 이상의 자릿수에 대해서 공백 추가
                if (++binDigitCount > 3 && i > 0)
                {
                    formattedBin.Insert(0, " ");
                    binDigitCount = 0;
                }
            }

            btnHEX.Text = "HEX  " + formattedHex.ToString();
            btnDEC.Text = "DEC  " + Convert.ToString(hex, 10);
            btnOCT.Text = "OCT  " + formattedOct.ToString();
            btnBIN.Text = "BIN  " + formattedBin.ToString();
        }
    }
}
