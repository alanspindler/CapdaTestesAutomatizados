using System;
using System.Collections.Generic;
using System.Linq;

namespace Lampp.CAPDA.Teste.Automatizado.SharedObjects
{
    public class GeradorCNPJCPF
    {
        Int64 soma1, soma2, i, erro, cpf, parte1, parte2, parte3, dig1, parte5, parte6, parte7, dig2;
        Int64[] numero = new Int64[13];
        Random rand = new Random();

        public String CpfSemMascara(Int64 quant)
        {
            String result = "";
            for (cpf = 1; cpf <= quant; cpf++)
            {
                for (i = 1; i <= 9; i++)
                {
                    erro = 1;
                    do
                    {
                        if (erro > 1)
                        {
                            //MessageBox.Show("Numero invalido.\n");
                            erro = 1;
                        }
                        numero[i] = (rand.Next()) % 9;
                        erro++;
                    } while (numero[i] > 9 || numero[i] < 0);
                }
                //*==========================================*
                //|       Primeiro digito verificador        |
                //*==========================================*
                soma1 = ((numero[1] * 10) +
                      (numero[2] * 9) +
                      (numero[3] * 8) +
                      (numero[4] * 7) +
                      (numero[5] * 6) +
                      (numero[6] * 5) +
                      (numero[7] * 4) +
                      (numero[8] * 3) +
                      (numero[9] * 2));
                parte1 = (soma1 / 11);
                parte2 = (parte1 * 11);
                parte3 = (soma1 - parte2);
                dig1 = (11 - parte3);
                if (dig1 > 9) dig1 = 0;
                //*==========================================*
                //|        Segundo digito verificador        |
                //*==========================================*
                soma2 = ((numero[1] * 11) +
                      (numero[2] * 10) +
                      (numero[3] * 9) +
                      (numero[4] * 8) +
                      (numero[5] * 7) +
                      (numero[6] * 6) +
                      (numero[7] * 5) +
                      (numero[8] * 4) +
                      (numero[9] * 3) +
                      (dig1 * 2));
                parte5 = (soma2 / 11);
                parte6 = (parte5 * 11);
                parte7 = (soma2 - parte6);
                dig2 = (11 - parte7);
                if (dig2 > 9) dig2 = 0;
                //*==========================================*
                //|       Impressao do numero completo       | 
                //*==========================================*
                for (i = 1; i <= 9; i++)
                {
                    //numeros do CPF
                    result += Convert.ToString(numero[i]);
                }
                // dois últimos digitos
                result += dig1 + "" + dig2;
            }

            //Äs vezes gera um CPF inválido, não sei o motivo. Se for inválido, faz de novo
            if (!ValidaCPF(result))
            {
                result = CpfSemMascara(1);
            }
            return result;
        }

        public string GerarCNPJ()
        {
            int Mod(int dividendo, int divisor)
            {
                return (dividendo - (dividendo / divisor) * divisor);
            }

            Random rnd = new Random();

            int n1 = rnd.Next(10);
            int n2 = rnd.Next(10);
            int n3 = rnd.Next(10);
            int n4 = rnd.Next(10);
            int n5 = rnd.Next(10);
            int n6 = rnd.Next(10);
            int n7 = rnd.Next(10);
            int n8 = rnd.Next(10);
            int n9 = 0;
            int n11 = 0;
            int n10 = 0;
            int n12 = 1;
            int d1 = n12 * 2 + n11 * 3 + n10 * 4 + n9 * 5 + n8 * 6 + n7 * 7 + n6 * 8 + n5 * 9 + n4 * 2 + n3 * 3 + n2 * 4 + n1 * 5;
            d1 = 11 - (Mod(d1, 11));
            if (d1 >= 10) d1 = 0;
            int d2 = d1 * 2 + n12 * 3 + n11 * 4 + n10 * 5 + n9 * 6 + n8 * 7 + n7 * 8 + n6 * 9 + n5 * 2 + n4 * 3 + n3 * 4 + n2 * 5 + n1 * 6;
            d2 = 11 - (Mod(d2, 11));
            if (d2 >= 10) d2 = 0;
            string cnpj = string.Concat(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, d1, d2);
            //Äs vezes gera um CNPJ inválido, não sei o motivo. Se for inválido, faz de novo
            if (!ValidaCNPJ(cnpj))
            {
               cnpj = GerarCNPJ();
            }
            return cnpj;

        }

        public static bool ValidaCNPJ(string vrCNPJ)

        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;
            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }
                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaCPF(string vrCPF)

        {
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");
            if (valor.Length != 11)
                return false;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(

                  valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }

    }
}






     