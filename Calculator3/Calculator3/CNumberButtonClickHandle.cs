using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/********************************************************************
	created:	2023/12/15
	created:	15:12:2023   10:19
	filename: 	C:\Users\SW DEV2\Desktop\Calculator3-main\Calculator3\Calculator3\CNumberButtonClickHandle.cs
	file path:	C:\Users\SW DEV2\Desktop\Calculator3-main\Calculator3\Calculator3
	file base:	CNumberButtonClickHandle
	file ext:	cs
	author:		Author
	
	purpose:	
*********************************************************************/
/**
 @class CNumberButtonClickHandler
 
 @brief 
  숫자버튼을 클릭 핸들러 및 진법 변환 함수
 
 @details
  숫자 버튼을 클릭하면 txtResult.Text에 숫자가 추가되고 txtResult.Text는 temporary.Text에 적용
 * 16진수, 10진수, 8진수, 2진수 버튼을 클릭하면 temporary.Text의 숫자가 선택한 진법 버튼에 따라 맞는 진법으로 변환
  
 @par History
  클래스 변경 이력
  
 @todo
  추가 작업 사항
  
*/

namespace Calculator3
{
    public class NumberButtonClickHandler
    {
        public TextBox txtResult;
        public Button btnHEX;
        public Button btnDEC;
        public Button btnOCT;
        public Button btnBIN;
        public TextBox temporary;
        public bool opFlag = false;
        public bool memFlag = false;

        public NumberButtonClickHandler(TextBox txtResult, Button btnHEX, Button btnDEC, Button btnOCT, Button btnBIN, TextBox temporary)
        {
            this.txtResult = txtResult;
            this.btnHEX = btnHEX;
            this.btnDEC = btnDEC;
            this.btnOCT = btnOCT;
            this.btnBIN = btnBIN;
            this.temporary = temporary;
            
        }

        public void HandleNumberButtonClick(object sender, EventArgs e, int number, bool opFlag, bool memFlag)
        {
            if (txtResult.Text == "0" || opFlag == true || memFlag == true)
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
            temporary.Text = txtResult.Text;
            string ConvertingNum = txtResult.Text.Replace(",", ""); // txtResult.Text에서 콤마만 제거. 마이너스는 제거X
            
            long hex = Convert.ToInt64(ConvertingNum);
            long dec = Convert.ToInt64(ConvertingNum);
            long oct = Convert.ToInt64(ConvertingNum);
            long bin = Convert.ToInt64(ConvertingNum);

            toHEX(hex);
            toDEC(dec);
            toOCT(oct);
            toBIN(bin);

            temporary.Text = txtResult.Text;
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
            temporary.Text = formattedHex.ToString();
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
            temporary.Text = formattedDec.ToString();
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
            temporary.Text = formattedOct.ToString();
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
            temporary.Text = formattedBin.ToString();
        }
    }
}
