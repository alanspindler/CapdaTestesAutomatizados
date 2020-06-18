using System;
using System.Collections.Generic;
using System.Linq;

namespace Lampp.CAPDA.Teste.Automatizado.SharedObjects
{
    public class GeradorCNPJCPF
    {
        Int64 soma1, soma2, i, erro, cnpj, cpf, parte1, parte2, parte3, dig1, parte5, parte6, parte7, dig2;
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
            if (!ValidarCPF(result))
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

        public static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool ValidarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

    }
}






     