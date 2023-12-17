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
        public bool isPanel2Visible = false;// 메모리 영역 플래그
        public long lValue;// 연산 전 초기 입력값
        public char op = '\0';// 연산 값
        public bool opFlag = false;// 연산이 입력되는지 체크하는 플래그
        public string memory;// 메모리값
        public bool memFlag = false;// 메모리가 입력되는지 체크하는 플래그
        public CheckBox[] bitCheckBoxes;// 체크박스 정의
        long hex = 0;// 16진수 변환
        long dec = 0;// 10진수 변환
        long oct = 0;// 8진수 변환
        long bin = 0;// 2진수 변환
        int FormationConvert = 0;// 비트 전환 키패트의 체크박스 비활성화 코드
        string ConvertingNum ="0";// 진수 변환된 숫자 값
        public NumberButtonClickHandler numberButtonClickHandler;// 클래스 호출
        int btnClickedState = 10;// 진수 변환 버튼 클릭 상태
        private Panel dynamicPanel; // 동적으로 생성할 패널을 나타내는 변수
        private bool isPanelVisible = false;// 비트 버튼을 클릭하면 나타나는 패널 플래그 값
        int y1;
        int y2;
        int y3;
        int y4;
        int y5;
        int y6;
        int y7;


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
            numberButtonClickHandler = new NumberButtonClickHandler(txtResult, btnHEX, btnDEC, btnOCT, btnBIN, temporary);

            InitializeDynamicPanel();

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
            int fontSize = Convert.ToInt32(this.Height * 0.035); // 높이의 3.5% 크기로 설정
            txtResult.Font = new Font(txtResult.Font.FontFamily, fontSize, txtResult.Font.Style);
            temporary.Font = new Font(txtResult.Font.FontFamily, fontSize, txtResult.Font.Style);
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

            y1 = Convert.ToInt32(this.Height * 0.42);
            tableLayoutPanel1.Height = y1;
            y2 = Convert.ToInt32(this.Height * 0.07);
            tableLayoutPanel2.Height = y2;
            y3 = Convert.ToInt32(this.Height * 0.05);
            tableLayoutPanel3.Height = y3;
            y4 = Convert.ToInt32(this.Height * 0.20);
            tableLayoutPanel4.Height = y4;
            //Console.WriteLine(y);
            txtResult.Location = new Point(0, y4);
            y5 = Convert.ToInt32(splitContainer1.Panel1.Width * 0.98);
            temporary.Width = y5;
            temporary.Location = new Point(5, 60);
            y6 = Convert.ToInt32(this.Height * 0.06);
            flowLayoutPanelBits8.Height = y6;
            flowLayoutPanelBits7.Height = y6;
            flowLayoutPanelBits6.Height = y6;
            flowLayoutPanelBits5.Height = y6;
            flowLayoutPanelBits4.Height = y6;
            flowLayoutPanelBits3.Height = y6;
            flowLayoutPanelBits2.Height = y6;
            flowLayoutPanelBits1.Height = y6;

            y7 = (tableLayoutPanel1.Location.Y);

            Console.WriteLine(y7);


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
            temporary.Text = "0";
            txtResult.TextChanged += txtResult_TextChanged;

            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel3.Dock = DockStyle.Bottom;
            tableLayoutPanel4.Dock = DockStyle.Bottom;
            txtResult.Dock = DockStyle.Bottom;

            btnHEX.Text = "HEX  0";
            btnDEC.Text = "DEC  0";
            btnOCT.Text = "OCT  0";
            btnBIN.Text = "BIN  0000";

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
                checkBox.Font = new Font("검은 고딕", 14, FontStyle.Bold);
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
                checkBox.Font = new Font("검은 고딕", 14, FontStyle.Bold);
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
                checkBox.Font = new Font("검은 고딕", 14, FontStyle.Bold);
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

            hex = Convert.ToInt64(ConvertingNum);
            dec = Convert.ToInt64(ConvertingNum);
            oct = Convert.ToInt64(ConvertingNum);
            bin = Convert.ToInt64(ConvertingNum);

            if (btnClickedState == 16)
            {
                numberButtonClickHandler.toDEC(dec);
                numberButtonClickHandler.toOCT(oct);
                numberButtonClickHandler.toBIN(bin);
                numberButtonClickHandler.toHEX(hex);
            }
            if (btnClickedState == 10)
            {
                numberButtonClickHandler.toHEX(hex);
                numberButtonClickHandler.toOCT(oct);
                numberButtonClickHandler.toBIN(bin);
                numberButtonClickHandler.toDEC(dec);
            }
            if (btnClickedState == 8)
            {
                numberButtonClickHandler.toHEX(hex);
                numberButtonClickHandler.toDEC(dec);
                numberButtonClickHandler.toBIN(bin);
                numberButtonClickHandler.toOCT(oct);
            }
            if (btnClickedState == 2)
            {
                numberButtonClickHandler.toHEX(hex);
                numberButtonClickHandler.toDEC(dec);
                numberButtonClickHandler.toOCT(oct);
                numberButtonClickHandler.toBIN(bin);
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (txtResult.Text != "0")
            {
                btnAllClear.Text = "CE";
            }
            else
            {
                btnAllClear.Text = "C";
            }

            txtResult.SelectionStart = txtResult.Text.Length;
            dynamicPanel.Location = new Point(1, y7); // 패널의 위치 설정

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

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            // textBoxResult의 텍스트가 변경되면 체크박스를 업데이트
            ConvertingNum = txtResult.Text.Replace(",", ""); // ConvertingNum = 텍스트 박스에서 콤마 제거, 필수 코드
            if (long.TryParse(ConvertingNum, out decimalValue))
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
            btnClickedState = 16;
            //// 보라색의 Color 객체를 생성
            //Color lineColor = Color.Purple;

            //// 직사각형 선을 그리기 위한 좌표와 크기 계산
            //int lineThickness = 3; // 선의 두께
            //int lineHeight = (int)(btnHEX.Height * 0.6); // 선의 높이를 1.5로 조절
            //int lineX = 0; // 선의 X 좌표 (버튼의 왼쪽에 그릴 것이므로 0)
            //int lineY = (int)(lineThickness / 0.75); // 선의 Y 좌표 (선의 중간에 맞게 조절)

            //// GraphicsPath를 사용하여 라운드를 가진 도형을 정의
            //using (GraphicsPath path = new GraphicsPath())
            //{
            //    // AddRectangle 메서드로 직사각형을 추가하고
            //    // AddRectangle 메서드의 두 번째 매개변수를 통해 라운드를 조절
            //    path.AddRectangle(new Rectangle(lineX, lineY, lineThickness, lineHeight));

            //    // 선 그리기 (라운드가 있는 도형)
            //    using (Pen pen = new Pen(lineColor, lineThickness))
            //    {
            //        using (Graphics g = btnHEX.CreateGraphics())
            //        {
            //            g.DrawPath(pen, path);
            //        }
            //    }
            //}

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

            hex = Convert.ToInt64(ConvertingNum);

            numberButtonClickHandler.toHEX(hex);
        }

        private void btnDEC_Click(object sender, EventArgs e)
        {
            btnClickedState = 10;

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

            numberButtonClickHandler.toDEC(dec);
        }

        private void btnOCT_Click(object sender, EventArgs e)
        {
            btnClickedState = 8;

            btn9.Enabled = false;
            btn8.Enabled = false;
            btn7.Enabled = true;
            btn6.Enabled = true;
            btn5.Enabled = true;
            btn4.Enabled = true;
            btn3.Enabled = true;
            btn2.Enabled = true;

            numberButtonClickHandler.toOCT(oct);
        }

        private void btnBIN_Click(object sender, EventArgs e)
        {
            btnClickedState = 2;

            btn7.Enabled = false;
            btn6.Enabled = false;
            btn5.Enabled = false;
            btn4.Enabled = false;
            btn3.Enabled = false;
            btn2.Enabled = false;

            numberButtonClickHandler.toBIN(bin);
        }

        
        private void btnFormationConvert_Click(object sender, EventArgs e)
        {
            FormationConvert++;
            
            if (FormationConvert == 0)
            {
                btnFormationConvert.Text = "QWORD";
                SetCheckBoxesEnabled(true); // 모든 체크박스를 활성화
            }
            if (FormationConvert == 1)
            {
                btnFormationConvert.Text = "DWORD";
                SetCheckBoxesEnabled(false, 63, 32); // 63번부터 32번 체크박스 비활성화
            }
            if (FormationConvert == 2)
            {
                btnFormationConvert.Text = "WORD";
                SetCheckBoxesEnabled(false, 63, 16); // 63번부터 16번 체크박스 비활성화
            }
            if (FormationConvert == 3)
            {
                btnFormationConvert.Text = "BYTE";
                SetCheckBoxesEnabled(false, 63,8); // 63번부터 16번 체크박스 비활성화
                FormationConvert = -1;
            }
        }
        private void SetCheckBoxesEnabled(bool enabled, int startIdx = 63, int endIdx = 0)
        {
            // 체크박스를 시작 인덱스부터 끝 인덱스까지 활성화 또는 비활성화
            for (int i = startIdx; i >= endIdx; i--)
            {
                bitCheckBoxes[i].Enabled = enabled;

                // 만약 체크박스를 비활성화하면 체크를 해제하고 색상 변경
                if (!enabled)
                {
                    bitCheckBoxes[i].Checked = false;
                    bitCheckBoxes[i].ForeColor = Color.FromArgb(200, 200, 200); // 텍스트 색상 변경
                }
                else
                {
                    bitCheckBoxes[i].ForeColor = Color.Black; // 텍스트 색상을 블랙으로 변경
                }
            }
        }

        private void btnAllClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            temporary.Text = "0";
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
            if (txtResult.Text == "0")
            {

            }
            else
            {
                long v = long.Parse(ConvertingNum);
                txtResult.Text = (-v).ToString();
            }
            
        }

        private long rValue = 0;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lValue = long.Parse(ConvertingNum);
            txtExp.Text = txtResult.Text + " + ";
            op = '+';
            opFlag = true;

        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            lValue = long.Parse(ConvertingNum);
            txtExp.Text = txtResult.Text + " - ";
            op = '-';
            opFlag = true;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            lValue = long.Parse(ConvertingNum);
            txtExp.Text = txtResult.Text + " × ";
            op = '*';
            opFlag = true;
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            lValue = long.Parse(ConvertingNum);
            txtExp.Text = txtResult.Text + " ÷ ";
            op = '/';
            opFlag = true;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            long rValue = long.Parse(ConvertingNum);
            
            switch (op)
            {
                case '+':
                    txtResult.Text = (lValue + rValue).ToString();
                    break;
                case '-':
                    txtResult.Text = (lValue - rValue).ToString();
                    break;
                case '*':
                    txtResult.Text = (lValue * rValue).ToString();
                    break;
                case '/':
                    txtResult.Text = (lValue / rValue).ToString();
                    break;
                case '&':
                    // AND 연산 수행
                    txtResult.Text = (lValue & rValue).ToString();
                    break;
                case '|':
                    // OR 연산 수행
                    txtResult.Text = (lValue | rValue).ToString();
                    break;
                case '~':
                    // NOT 연산 수행
                    txtResult.Text = (~lValue).ToString();
                    break;
                case '@':
                    // NAND 연산 수행
                    txtResult.Text = (~(lValue & rValue)).ToString();
                    break;
                case '#':
                    // NOR 연산 수행
                    txtResult.Text = (~(lValue | rValue)).ToString();
                    break;
                case '$':
                    // XOR 연산 수행
                    txtResult.Text = (lValue ^ rValue).ToString();
                    break;
            }
            txtExp.Text = "";
            Console.WriteLine("컨버팅 넘버:" + ConvertingNum);
            Console.WriteLine("왼쪽 값:" + lValue);
            Console.WriteLine("오른쪽 값:" + rValue);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 0, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 1 , opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 2, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 3, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 4, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 5, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 6, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 7, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 8, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            numberButtonClickHandler.HandleNumberButtonClick(sender, e, 9, opFlag, memFlag);
            opFlag = false;
            memFlag = false;
        }
        private void InitializeDynamicPanel()
        {
            this.Click += Form_Click;

            // 새로운 라운드 패널 생성
            dynamicPanel = new RoundedPanel();
            dynamicPanel.Size = new Size(200, 100); // 패널의 크기 설정

            dynamicPanel.BackColor = Color.FromArgb(243, 243, 243);

            // 패널에 버튼 6개 추가
            AddButtonsToDynamicPanel();

            // 폼에 라운드 패널 추가
            Controls.Add(dynamicPanel);
            dynamicPanel.Visible = false; // 초기에는 보이지 않도록 설정
        }

        private void AddButtonsToDynamicPanel()
        {
            string[] buttonTexts = { "AND", "OR", "NOT", "NAND", "NOR", "XOR" };

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button dynamicButton = new Button();
                    dynamicButton.Size = new Size(64, 47);
                    dynamicButton.Location = new Point(col * 66 + 3, row * 49 + 1);
                    dynamicButton.FlatStyle = FlatStyle.Flat;
                    dynamicButton.Padding = new Padding(2);
                    dynamicButton.Name = $"btnDynamic{row * 3 + col + 1}";

                    // Owner Drawn 설정
                    dynamicButton.FlatStyle = FlatStyle.Flat;
                    dynamicButton.FlatAppearance.BorderSize = 0;
                    dynamicButton.Paint += DynamicButton_Paint;

                    dynamicButton.Click += DynamicButton_Click;
                    dynamicButton.Text = buttonTexts[row * 3 + col];
                    dynamicButton.BackColor = Color.White;
                    dynamicButton.Font = new Font("맑은 고딕", 10);


                    dynamicPanel.Controls.Add(dynamicButton);
                }
            }
        }

        private void DynamicButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            using (GraphicsPath path = RoundedRectangle(btn.ClientRectangle, 5)) // 10은 라운드 처리 정도
            {
                btn.Region = new Region(path);
            }
        }

        private GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            path.AddArc(arc, 180, 90);

            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }

        private void DynamicButton_Click(object sender, EventArgs e)
        {
            // 클릭된 버튼의 이름 가져오기
            string buttonName = ((Button)sender).Name;
            long rValue = long.Parse(ConvertingNum);
            // 버튼에 따라 원하는 동작 수행
            switch (buttonName)
            {

                case "btnDynamic1":
                    // AND 연산 수행
                    lValue = long.Parse(ConvertingNum);
                    txtExp.Text = txtResult.Text + " AND ";
                    op = '&';
                    opFlag = true;
                    dynamicPanel.Visible = false;
                    break;
                case "btnDynamic2":
                    // OR 연산 수행
                    lValue = long.Parse(ConvertingNum);
                    txtExp.Text = txtResult.Text + " OR ";
                    op = '|';
                    opFlag = true;
                    dynamicPanel.Visible = false;
                    break;
                case "btnDynamic3":
                    // NOT 연산 수행
                    lValue = long.Parse(ConvertingNum);
                    txtExp.Text = "NOT("+ txtResult.Text + ")";
                    op = '~';
                    opFlag = true;
                    dynamicPanel.Visible = false;
                    break;
                case "btnDynamic4":
                    // NAND 연산 수행
                    lValue = long.Parse(ConvertingNum);
                    txtExp.Text = txtResult.Text + " NAND ";
                    op = '@';
                    opFlag = true;
                    dynamicPanel.Visible = false;
                    break;
                case "btnDynamic5":
                    // NOR 연산 수행
                    lValue = long.Parse(ConvertingNum);
                    txtExp.Text = txtResult.Text + " NOR ";
                    op = '#';
                    opFlag = true;
                    dynamicPanel.Visible = false;
                    break;
                case "btnDynamic6":
                    // XOR 연산 수행
                    lValue = long.Parse(ConvertingNum);
                    txtExp.Text = txtResult.Text + " XOR ";
                    op = '$';
                    opFlag = true;
                    dynamicPanel.Visible = false;
                    break;
            }
        }

        private void btnBIT_Click(object sender, EventArgs e)
        {
            // 패널의 가시성 상태를 토글
            isPanelVisible = !isPanelVisible;

            // 패널의 Visible 속성을 설정하여 보이거나 숨김
            dynamicPanel.Visible = isPanelVisible;

            if (isPanelVisible)
            {
                // 패널을 가장 위로 보냄
                dynamicPanel.BringToFront();
            }
        }

        private void Form_Click(object sender, EventArgs e)
        {
            // 패널이 보이는 상태에서 폼을 클릭하면 isPanelVisible을 false로 설정
            if (isPanelVisible)
            {
                Point relativePoint = dynamicPanel.PointToClient(Cursor.Position);
                if (!dynamicPanel.ClientRectangle.Contains(relativePoint))
                {
                    isPanelVisible = false;
                    dynamicPanel.Visible = false;
                }
            }
            else
            {
                isPanelVisible = false;
                dynamicPanel.Visible = false;
            }
        }

        private int btnYCoordinate = 60;
        private int btnHeightOffset = 10;
        private void ButtonMemory_Click(object sender, EventArgs e)
        {
            // 클릭된 버튼의 텍스트를 txtResult.Text로 설정
            txtResult.Text = ((Button)sender).Text;
        }

        private void btnMS_Click(object sender, EventArgs e)
        {
            // Panel2의 스크롤을 최상단으로 이동
            splitContainer1.Panel2.VerticalScroll.Value = 0;

            memory = txtResult.Text;
            btnMhistory.Enabled = true;
            memFlag = true;

            // Button을 생성하고 속성 설정
            Button btnMemory = new Button();
            btnMemory.Text = memory;
            btnMemory.Size = new Size(310, 60);
            btnMemory.TextAlign = ContentAlignment.TopRight;
            btnMemory.Font = new Font("맑은 고딕", 17, FontStyle.Bold);
            btnMemory.FlatStyle = FlatStyle.Flat;
            btnMemory.FlatAppearance.BorderSize = 0;
            btnMemory.Dock = DockStyle.None; // Dock을 None으로 설정
            btnMemory.AutoSize = true; // AutoSize를 true로 설정하여 내용에 맞게 크기 조절

            // y 좌표를 업데이트하여 다음 Button이 겹치지 않도록 함
            btnYCoordinate -= btnMemory.Height + btnHeightOffset;

            // 가장 위 버튼의 Y좌표가 0 미만이면 0으로 설정
            if (btnYCoordinate < 60)
            {
                btnYCoordinate = 60;
            }

            btnMemory.Location = new Point(0, btnYCoordinate);

            // 새로운 Button을 Panel2에 추가
            splitContainer1.Panel2.Controls.Add(btnMemory);

            // 다른 Button들도 함께 내려가도록 조정
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control is Button && control != btnMemory)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + btnMemory.Height + btnHeightOffset);
                }
            }

            btnMemory.Click += ButtonMemory_Click;
            // 가로 스크롤 비활성화
            splitContainer1.Panel2.HorizontalScroll.Enabled = false;
            splitContainer1.Panel2.HorizontalScroll.Visible = false;

        }
    }
}
