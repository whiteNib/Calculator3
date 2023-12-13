using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator3
{
    class Dummy
    {

        public void toHEX()
        {
            //// 16진수 변환
            //string hexString = hex.ToString("X"); // 4자리 이상도 모두 포함하기 위해 "X" 사용
            //StringBuilder formattedHex = new StringBuilder();

            //int digitCount = 0;

            //for (int i = hexString.Length - 1; i >= 0; i--)
            //{
            //    formattedHex.Insert(0, hexString[i]);

            //    // 4자리 이상의 자릿수에 대해서 공백 추가
            //    if (++digitCount > 3 && i > 0)
            //    {
            //        formattedHex.Insert(0, " ");
            //        digitCount = 0;
            //    }
            //}

            //int octDigitCount = 0;
            //StringBuilder formattedOct = new StringBuilder();

            //// 10진수 변환
            //string decString = Convert.ToString(hex, 10);
            //StringBuilder formattedDec = new StringBuilder();

            //int decDigitCount = 0;

            //for (int i = decString.Length - 1; i >= 0; i--)
            //{
            //    formattedDec.Insert(0, decString[i]);

            //    // 3자리 이상의 자릿수에 대해서 쉼표 추가
            //    if (++decDigitCount > 2 && i > 0)
            //    {
            //        formattedDec.Insert(0, ",");
            //        decDigitCount = 0;
            //    }
            //}

            //// 8진수 변환
            //string octString = Convert.ToString(hex, 8);

            //for (int i = octString.Length - 1; i >= 0; i--)
            //{
            //    formattedOct.Insert(0, octString[i]);

            //    // 3자리 이상의 자릿수에 대해서 공백 추가
            //    if (++octDigitCount > 2 && i > 0)
            //    {
            //        formattedOct.Insert(0, " ");
            //        octDigitCount = 0;
            //    }
            //}

            //// 2진수 변환
            //string binString = Convert.ToString(bin, 2);
            //StringBuilder formattedBin = new StringBuilder();

            //int binDigitCount = 0;

            //for (int i = binString.Length - 1; i >= 0; i--)
            //{
            //    formattedBin.Insert(0, binString[i]);

            //    // 4자리 이상의 자릿수에 대해서 공백 추가
            //    if (++binDigitCount > 3 && i > 0)
            //    {
            //        formattedBin.Insert(0, " ");
            //        binDigitCount = 0;
            //    }
            //}

            //// 이진수 변환시 4자리씩 출력
            //int addedZeros = 4 - (formattedBin.Length % 5);
            //if (addedZeros != 5)
            //{
            //    for (int i = 0; i < addedZeros; i++)
            //    {
            //        formattedBin.Insert(0, "0");
            //    }
            //}

            //btnHEX.Text = "HEX  " + formattedHex.ToString();
            //btnDEC.Text = "DEC  " + formattedDec.ToString();
            //btnOCT.Text = "OCT  " + formattedOct.ToString();
            //btnBIN.Text = "BIN  " + formattedBin.ToString();
            //txtResult.Text = formattedDec.ToString();
        }
    }
}
