using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator3
{
    public class NumberButtonClickHandler
    {
        private TextBox txtResult;
        private Button btnHEX;
        private Button btnDEC;
        private Button btnOCT;
        private Button btnBIN;

        private bool opFlag;
        private bool memFlag;

        public NumberButtonClickHandler(TextBox txtResult, Button btnHEX, Button btnDEC, Button btnOCT, Button btnBIN)
        {
            this.txtResult = txtResult;
            this.btnHEX = btnHEX;
            this.btnDEC = btnDEC;
            this.btnOCT = btnOCT;
            this.btnBIN = btnBIN;
        }

        public void HandleNumberButtonClick(object sender, EventArgs e, int number)
        {
            if (txtResult.Text == "0" || opFlag || memFlag)
            {
                txtResult.Text = number.ToString();
                opFlag = false;
                memFlag = false;
            }
            else
            {
                txtResult.Text += number.ToString();
            }

            btnHEX.Text += number.ToString();
            btnDEC.Text += number.ToString();
            btnOCT.Text += number.ToString();
            btnBIN.Text += number.ToString();

            string a = txtResult.Text.Replace(",", "");
            long hex = Convert.ToInt64(a);
            long dec = Convert.ToInt64(a);
            long oct = Convert.ToInt64(a);
            long bin = Convert.ToInt64(a);

            toHEX(hex);
            toDEC(dec);
            toOCT(oct);
            toBIN(bin);
        }

        public void toHEX(long value)
        {
            // 16진수 변환
            string hexString = value.ToString("X"); // 4자리 이상도 모두 포함하기 위해 "X" 사용
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
            btnHEX.Text = "HEX  " + formattedHex.ToString();
            
            
            
            

        }

        public void toDEC(long value)
        {
            // 10진수 변환
            string decString = value.ToString();
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
            btnDEC.Text = "DEC  " + formattedDec.ToString();
            txtResult.Text = formattedDec.ToString();
        }

        public void toOCT(long value)
        {
            // 8진수 변환
            string octString = Convert.ToString(value, 8);
            StringBuilder formattedOct = new StringBuilder();

            int octDigitCount = 0;

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
            btnOCT.Text = "OCT  " + formattedOct.ToString();
        }

        public void toBIN(long value)
        {
            // 2진수 변환
            string binString = Convert.ToString(value, 2);
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

            // 이진수 변환시 4자리씩 출력
            int addedZeros = 4 - (formattedBin.Length % 5);
            if (addedZeros != 5)
            {
                for (int i = 0; i < addedZeros; i++)
                {
                    formattedBin.Insert(0, "0");
                }
            }
            btnBIN.Text = "BIN  " + formattedBin.ToString();
        }
    }
}
