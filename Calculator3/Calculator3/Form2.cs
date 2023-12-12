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
using System.Drawing.Drawing2D;

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
        public CheckBox[] bitCheckBoxes;
        long hex = 0;
        long dec = 0;
        long oct = 0;
        long bin = 0;
        string a;
        private NumberButtonClickHandler numberButtonClickHandler;

        public Form2()
        {
            this.KeyPreview = true;  // 폼이 키보드 입력을 받을 수 있도록 설정
            InitializeComponent();
            InitializeBitCheckBoxes();

            btnMhistory.Enabled = false;

            //this.DoubleBuffered = true;

            // Resize 이벤트 핸들러를 연결 
            this.Resize += Form2_Resize;

            // 초기 폼 크기를 설정 
            this.Size = new Size(400, 600);

            // 초기 너비를 기반으로 패널 가시성을 설정 
            UpdatePanelVisibility();

            // tableLayoutPanel4를 먼저 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel4);

            // tableLayoutPanel3를 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel3);

            // tableLayoutPanel5를 추가
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits8);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits7);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits6);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits5);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits4);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits3);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits2);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelBits1);

            // tableLayoutPanel2를 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);

            // tableLayoutPanel1을 다음에 추가
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);

            // NumberButtonClickHandler 클래스의 인스턴스를 생성하고 필요한 컨트롤을 전달
            numberButtonClickHandler = new NumberButtonClickHandler(txtResult, btnHEX, btnDEC, btnOCT, btnBIN);

            // 각 숫자 버튼에 대한 이벤트 핸들러 등록

            btn0.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 0);
            btn1.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 1);
            btn2.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 2);
            btn3.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 3);
            btn4.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 4);
            btn5.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 5);
            btn6.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 6);
            btn7.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 7);
            btn8.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 8);
            btn9.Click += (sender, e) => numberButtonClickHandler.HandleNumberButtonClick(sender, e, 9);

        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 애플리케이션 종료 전에 마지막에 열린 폼의 이름을 저장
            Properties.Settings.Default.LastOpenedForm = this.Name;
            Properties.Settings.Default.Save();
            Application.Exit();
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
            int fontSize = Convert.ToInt32(this.Width * 0.05); // 높이의 5% 크기로 설정
            txtResult.Font = new Font(txtResult.Font.FontFamily, fontSize, txtResult.Font.Style);
        }

        private void UpdatePanelVisibility()
        {
            // 폼의 너비를 확인 
            if (this.Width > 800)
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

            int y1 = Convert.ToInt32(this.Height * 0.42);
            tableLayoutPanel1.Height = y1;
            int y2 = Convert.ToInt32(this.Height * 0.07);
            tableLayoutPanel2.Height = y2;
            int y3 = Convert.ToInt32(this.Height * 0.05);
            tableLayoutPanel3.Height = y3;
            int y4 = Convert.ToInt32(this.Height * 0.20);
            tableLayoutPanel4.Height = y4;
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
            flowLayoutPanelBits1.Dock = DockStyle.Bottom;
            this.MinimumSize = new System.Drawing.Size(420, 600);
            timer1.Start();
            // textBoxResult가 10진수가 입력되는 TextBox인 것으로 가정
            txtResult.Text = "0";
            txtResult.TextChanged += txtResult_TextChanged;

            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel3.Dock = DockStyle.Bottom;
            tableLayoutPanel4.Dock = DockStyle.Bottom;
            txtResult.Dock = DockStyle.Bottom;

            btnHEX.Text = "HEX  0";
            btnDEC.Text = "DEC  0";
            btnOCT.Text = "OCT  0";
            btnBIN.Text = "BIN  0";

        }

        private void InitializeBitCheckBoxes()
        {
            // 비트 수 설정
            int bitCount = 64;

            bitCheckBoxes = new CheckBox[bitCount];


            for (int i = bitCount - 1; i >= 48; i--)
            {
                // CheckBox 생성
                CheckBox checkBox = new CheckBox();
                checkBox.Text = "0";
                checkBox.CheckedChanged += BitCheckBox_CheckedChanged;

                // 폼에 추가
                flowLayoutPanelBits8.Controls.Add(checkBox);

                // 배열에 저장
                bitCheckBoxes[i] = checkBox;

                // 체크박스 디자인
                checkBox.Appearance = Appearance.Button;
                checkBox.FlatStyle = FlatStyle.Flat;
                checkBox.FlatAppearance.BorderSize = 0;
                checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                checkBox.Font = new Font("검은 고딕", 14, FontStyle.Bold);
                checkBox.Margin = new Padding(0, 0, 0, 0);
                checkBox.Dock = DockStyle.Fill;
                checkBox.FlatAppearance.MouseOverBackColor = Color.Transparent;

                // FlatAppearance 설정
                checkBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(243, 243, 243); // 클릭 시 배경색
                checkBox.FlatAppearance.CheckedBackColor = Color.FromArgb(243, 243, 243); // 체크 시 배경색

            }

            for (int i = 47; i >= 32; i--)
            {
                // CheckBox 생성
                CheckBox checkBox = new CheckBox();
                checkBox.Text = "0";
                checkBox.CheckedChanged += BitCheckBox_CheckedChanged;

                // 폼에 추가
                flowLayoutPanelBits6.Controls.Add(checkBox);

                // 배열에 저장
                bitCheckBoxes[i] = checkBox;

                // 체크박스 디자인
                checkBox.Appearance = Appearance.Button;
                checkBox.FlatStyle = FlatStyle.Flat;
                checkBox.FlatAppearance.BorderSize = 0;
                checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                checkBox.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                checkBox.Margin = new Padding(0, 0, 0, 0);
                checkBox.Dock = DockStyle.Left;
                checkBox.FlatAppearance.MouseOverBackColor = Color.Transparent;

                // FlatAppearance 설정
                checkBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(243, 243, 243); // 클릭 시 배경색
                checkBox.FlatAppearance.CheckedBackColor = Color.FromArgb(243, 243, 243); // 체크 시 배경색

            }

            for (int i = 31; i >= 16; i--)
            {
                // CheckBox 생성
                CheckBox checkBox = new CheckBox();
                checkBox.Text = "0";
                checkBox.CheckedChanged += BitCheckBox_CheckedChanged;

                // 폼에 추가
                flowLayoutPanelBits4.Controls.Add(checkBox);

                // 배열에 저장
                bitCheckBoxes[i] = checkBox;

                // 체크박스 디자인
                checkBox.Appearance = Appearance.Button;
                checkBox.FlatStyle = FlatStyle.Flat;
                checkBox.FlatAppearance.BorderSize = 0;
                checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                checkBox.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                checkBox.Margin = new Padding(0, 0, 0, 0);
                checkBox.Dock = DockStyle.Left;
                checkBox.FlatAppearance.MouseOverBackColor = Color.Transparent;

                // FlatAppearance 설정
                checkBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(243, 243, 243); // 클릭 시 배경색
                checkBox.FlatAppearance.CheckedBackColor = Color.FromArgb(243, 243, 243); // 체크 시 배경색

            }

            for (int i = 15; i >= 0; i--)
            {
                // CheckBox 생성
                CheckBox checkBox = new CheckBox();
                checkBox.Text = "0";
                checkBox.CheckedChanged += BitCheckBox_CheckedChanged;

                // 폼에 추가
                flowLayoutPanelBits2.Controls.Add(checkBox);

                // 배열에 저장
                bitCheckBoxes[i] = checkBox;

                // 체크박스 디자인
                checkBox.Appearance = Appearance.Button;
                checkBox.FlatStyle = FlatStyle.Flat;
                checkBox.FlatAppearance.BorderSize = 0;
                checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                checkBox.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                checkBox.Margin = new Padding(0, 0, 0, 0);
                checkBox.Dock = DockStyle.Left;
                checkBox.FlatAppearance.MouseOverBackColor = Color.Transparent;

                // FlatAppearance 설정
                checkBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(243, 243, 243); // 클릭 시 배경색
                checkBox.FlatAppearance.CheckedBackColor = Color.FromArgb(243, 243, 243); // 체크 시 배경색

            }
        }

        private void BitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // CheckBox가 변경되었을 때 처리할 로직 추가
            UpdateResult();
        }

        // 체크된 체크박스 처리 영역
        private void UpdateResult()
        {
                       
            // 선택된 비트를 이용한 계산 로직 추가
            long result = 0;
            for (int i = 0; i < bitCheckBoxes.Length; i++)
            {
                if (bitCheckBoxes[i].Checked)
                {
                    result |= (1L << i);
                    bitCheckBoxes[i].Text = "1";
                    bitCheckBoxes[i].ForeColor = Color.FromArgb(0, 99, 153);
                    //long temp = 1;
                    //for (int j = 0; j < i; j++)
                    //{
                    //    temp *= 2;
                    //}
                    //result |= temp;
                }
                else
                {
                    bitCheckBoxes[i].Text = "0";
                    bitCheckBoxes[i].ForeColor = Color.FromArgb(0, 0, 0);
                }
            }

            // 계산 결과를 텍스트 상자에 표시
            txtResult.Text = result.ToString();

            hex = Convert.ToInt64(a);
            dec = Convert.ToInt64(a);
            oct = Convert.ToInt64(a);
            bin = Convert.ToInt64(a);

            numberButtonClickHandler.toHEX(hex);
            numberButtonClickHandler.toDEC(dec);
            numberButtonClickHandler.toOCT(oct);
            numberButtonClickHandler.toBIN(bin);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int y = Convert.ToInt32(this.Height * 0.06);
            flowLayoutPanelBits8.Height = y;
            flowLayoutPanelBits7.Height = y;
            flowLayoutPanelBits6.Height = y;
            flowLayoutPanelBits5.Height = y;
            flowLayoutPanelBits4.Height = y;
            flowLayoutPanelBits3.Height = y;
            flowLayoutPanelBits2.Height = y;
            flowLayoutPanelBits1.Height = y;

            if (txtResult.Text == "")
            {
                txtResult.Text = "0";
            }
            if (txtResult.Text != "0")
            {
                btnAllClear.Text = "CE";
            }
            else
            {
                btnAllClear.Text = "C";
            }

            txtResult.SelectionStart = txtResult.Text.Length;
        }

        long decimalValue = 0;
        
        private void UpdateCheckboxes(long decimalValue)
        {
            // 10진수 값을 기반으로 체크박스 상태를 업데이트
            for (int i = 0; i < bitCheckBoxes.Length; i++)
            {
                long mask = 1L << i;
                bitCheckBoxes[i].Checked = (decimalValue & mask) != 0;
            }
        }

        private void textBoxResult_Click(object sender, EventArgs e)
        {
            // 텍스트 박스를 클릭할 때 다른 컨트롤에 포커스를 이동시켜 커서를 가리키지 않도록 함
        }

        private void button33_MouseHover(object sender, EventArgs e)
        {
            button33.BackgroundImage = pictureBox1.Image;
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
            flowLayoutPanelBits8.Visible = true;
            flowLayoutPanelBits7.Visible = true;
            flowLayoutPanelBits6.Visible = true;
            flowLayoutPanelBits5.Visible = true;
            flowLayoutPanelBits4.Visible = true;
            flowLayoutPanelBits3.Visible = true;
            flowLayoutPanelBits2.Visible = true;
            flowLayoutPanelBits1.Visible = true;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = true;
            flowLayoutPanelBits8.Visible = false;
            flowLayoutPanelBits7.Visible = false;
            flowLayoutPanelBits6.Visible = false;
            flowLayoutPanelBits5.Visible = false;
            flowLayoutPanelBits4.Visible = false;
            flowLayoutPanelBits3.Visible = false;
            flowLayoutPanelBits2.Visible = false;
            flowLayoutPanelBits1.Visible = false;
        }

        private void HandleNumberButtonClick(object sender, EventArgs e, int number)
        {
            // 현재 txtResult.Text 값을 long으로 변환하여 성공 여부를 확인
            if (long.TryParse(txtResult.Text, out long currentValue))
            {
                // 변환이 성공한 경우에만 처리
                // currentValue에는 현재 txtResult.Text의 값이 long으로 변환된 결과가 들어있음
                // 이 값을 활용하여 필요한 작업을 수행
                // 예: currentValue에 현재 클릭된 숫자를 추가하거나 새로운 값을 할당
                // ...

                // 클릭한 숫자를 추가하거나 새로운 값을 할당하는 예시:
                currentValue = currentValue * 10 + number;

                // 처리가 완료된 값을 txtResult.Text에 반영
                txtResult.Text = currentValue.ToString();
            }
            else
            {
                // 변환이 실패한 경우 (범위를 벗어나는 경우 등)
                // 이 경우에는 클릭 이벤트를 무시하거나 다른 예외 처리를 수행할 수 있음
                // 여기에서는 클릭을 무시하도록 처리
            }
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

            for (int i = binString.Length-1 ; i >= 0; i--)
            {
                formattedBin.Insert(0, binString[i]);

                // 4자리 이상의 자릿수에 대해서 공백 추가
                if (++binDigitCount > 3 && i > 0)
                {
                    formattedBin.Insert(0, " ");
                    binDigitCount = 0;
                }
            }

            // 이진수 변환시 4자리씩 출력
            int addedZeros = 4 - (formattedBin.Length % 5); 
            if (addedZeros != 5)
            {
                for (int i = 0; i < addedZeros; i++)
                {
                    formattedBin.Insert(0, "0");
                }
            }

            btnHEX.Text = "HEX  " + formattedHex.ToString();
            btnDEC.Text = "DEC  " + formattedDec.ToString();
            btnOCT.Text = "OCT  " + formattedOct.ToString();
            btnBIN.Text = "BIN  " + formattedBin.ToString();
            txtResult.Text = formattedDec.ToString();
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            // textBoxResult의 텍스트가 변경되면 체크박스를 업데이트
            a = txtResult.Text.Replace(",", ""); // a = 텍스트 박스에서 콤마 제거, 필수 코드
            if (long.TryParse(a, out decimalValue))
            {
                UpdateCheckboxes(decimalValue);
                // 여기에서 hex, dec, oct, bin 값을 업데이트할 필요가 없음
                // 이미 TryParse를 통해 decimalValue에 성공적으로 값을 변환했기 때문에
                // decimalValue를 사용
            }
            else
            {
                // 입력된 텍스트가 유효한 10진수가 아닌 경우에 대한 처리
            }
        }
        private void btnHEX_Click(object sender, EventArgs e)
        {
            // 보라색의 Color 객체를 생성
            Color lineColor = Color.Purple;

            // 직사각형 선을 그리기 위한 좌표와 크기 계산
            int lineThickness = 3; // 선의 두께
            int lineHeight = (int)(btnHEX.Height * 0.6); // 선의 높이를 1.5로 조절
            int lineX = 0; // 선의 X 좌표 (버튼의 왼쪽에 그릴 것이므로 0)
            int lineY = (int)(lineThickness / 0.75); // 선의 Y 좌표 (선의 중간에 맞게 조절)

            // GraphicsPath를 사용하여 라운드를 가진 도형을 정의
            using (GraphicsPath path = new GraphicsPath())
            {
                // AddRectangle 메서드로 직사각형을 추가하고
                // AddRectangle 메서드의 두 번째 매개변수를 통해 라운드를 조절
                path.AddRectangle(new Rectangle(lineX, lineY, lineThickness, lineHeight));

                // 선 그리기 (라운드가 있는 도형)
                using (Pen pen = new Pen(lineColor, lineThickness))
                {
                    using (Graphics g = btnHEX.CreateGraphics())
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btn9.Enabled = true;
            btn8.Enabled = true;
            btn7.Enabled = true;
            btn6.Enabled = true;
            btn5.Enabled = true;
            btn4.Enabled = true;
            btn3.Enabled = true;
            btn2.Enabled = true;
        }

        private void btnDEC_Click(object sender, EventArgs e)
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btn9.Enabled = true;
            btn8.Enabled = true;
            btn7.Enabled = true;
            btn6.Enabled = true;
            btn5.Enabled = true;
            btn4.Enabled = true;
            btn3.Enabled = true;
            btn2.Enabled = true;
        }

        private void btnOCT_Click(object sender, EventArgs e)
        {
            btn9.Enabled = false;
            btn8.Enabled = false;
            btn7.Enabled = true;
            btn6.Enabled = true;
            btn5.Enabled = true;
            btn4.Enabled = true;
            btn3.Enabled = true;
            btn2.Enabled = true;
        }

        private void btnBIN_Click(object sender, EventArgs e)
        {
            btn7.Enabled = false;
            btn6.Enabled = false;
            btn5.Enabled = false;
            btn4.Enabled = false;
            btn3.Enabled = false;
            btn2.Enabled = false;
        }


        int i = 0;
        private void btnFormationConvert_Click(object sender, EventArgs e)
        {
            i++;
            
            if (i == 0)
            {
                btnFormationConvert.Text = "QWORD";

            }
            if (i == 1)
            {
                btnFormationConvert.Text = "DWORD";
                
            }
            if (i == 2)
            {
                btnFormationConvert.Text = "WORD";
                
            }
            if (i == 3)
            {
                btnFormationConvert.Text = "BYTE";
                i = -1;
            }
        }

        private void btnAllClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            //txtExp.Text = "";
            lValue = 0;
            op = '\0';
            opFlag = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
            if (txtResult.Text.Length == 0)
            {
                txtResult.Text = "0";
            }
        }

        private void btnPM_Click(object sender, EventArgs e)
        {
            long v = long.Parse(txtResult.Text);
            txtResult.Text = (-v).ToString();
        }
    }
}
