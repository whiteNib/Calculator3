using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class NumberButtonHandler
{
    private TextBox txtResult;
    private Button btnHEX;
    private Button btnDEC;
    private Button btnOCT;
    private Button btnBIN;
    long hex;
    long dec;
    long oct;
    long bin;
    private bool opFlag;
    private bool memFlag;

    public NumberButtonHandler(TextBox txtResult, Button btnHEX, Button btnDEC, Button btnOCT, Button btnBIN)
    {
        this.txtResult = txtResult;
        this.btnHEX = btnHEX;
        this.btnDEC = btnDEC;
        this.btnOCT = btnOCT;
        this.btnBIN = btnBIN;
    }

    // 숫자 버튼 클릭을 처리하는 메서드
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

        // 각 진법 버튼에 숫자 추가
        btnHEX.Text += number.ToString();
        btnDEC.Text += number.ToString();
        btnOCT.Text += number.ToString();
        btnBIN.Text += number.ToString();

        // 콤마 제거 후 숫자로 변환
        string a = txtResult.Text.Replace(",", "");
        long hex = Convert.ToInt64(a);
        long dec = Convert.ToInt64(a);
        long oct = Convert.ToInt64(a);
        long bin = Convert.ToInt64(a);

        // 각 진법으로 변환하는 메서드 호출
        toHEX(hex);
        toDEC(dec);
        toOCT(oct);
        toBIN(bin);
    }

    // HEX로 변환하는 메서드
    private void toHEX(long value)
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

    // DEC로 변환하는 메서드
    private void toDEC(long value)
    {
        // DEC로 변환하는 논리 구현
    }

    // OCT로 변환하는 메서드
    private void toOCT(long value)
    {
        // OCT로 변환하는 논리 구현
    }

    // BIN으로 변환하는 메서드
    private void toBIN(long value)
    {
        // BIN으로 변환하는 논리 구현
    }
}
