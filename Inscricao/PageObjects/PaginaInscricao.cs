using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System.Security.Cryptography;
using System;

namespace Lampp.CAPDA.Teste.Automatizado.Cadastros.PageObjects
{
    [TestClass]
    public class PaginaInscricao : PaginaBase
    {

        public GeradorNome geradorNome { get; set; }
        public GeradorCNPJCPF geradorCNPJCPF { get; set; }

        #region Declaração de variáveis públicas da classe

        public By campoInstituicao = By.Id("txtNomeInstituicao");
        public By campoCNPJ = By.Id("txtCnpj");
        public By campoEmail = By.Id("txtEmail");
        public By campoResponsavel = By.Id("txtResponsavel");
        public By campoCPF = By.Id("txtCpf");
        public By campoSenha = By.Name("txtSenha");
        public By campoConfirmarSenha = By.Name("txtConfirmarSenha");
        public By botaoSalvar = By.ClassName("fa-save");
        public By MensagemErro = By.XPath("//p");
        public By botaoFechar = By.CssSelector("div.modal-footer > button.btn.btn-default.btn-sm");
        #endregion

        #region Métodos públicos

        /// <param name="rand">A Random that is used to pick names</param>
        public PaginaInscricao(RemoteWebDriver driver) : base(driver)
        {           
           geradorNome = new GeradorNome();
           geradorCNPJCPF = new GeradorCNPJCPF();
        }
                
        public string InscreverEmpresa()
        {            
            AguardarProcessando();            
            PreencherCampo(campoInstituicao, geradorNome.GerarNome());
            string CNPJ = geradorCNPJCPF.cnpj(false);
            PreencherCampo(campoCNPJ, CNPJ);            
            string textoCNPJ = driver.FindElement(campoCNPJ).GetAttribute("value");
            bool cnpjValido = geradorCNPJCPF.isCNPJ(textoCNPJ);
            while (cnpjValido == false)
            {
                CNPJ = geradorCNPJCPF.cnpj(false);
                PreencherCampo(campoCNPJ, CNPJ);
                textoCNPJ = driver.FindElement(campoCNPJ).GetAttribute("value");
                cnpjValido = geradorCNPJCPF.isCNPJ(textoCNPJ);
            }
            AguardarProcessando();
            PreencherCampo(campoCPF, geradorCNPJCPF.cpf(false));            
            string textoCpf = driver.FindElement(campoCPF).GetAttribute("value");
            bool cpfValido = geradorCNPJCPF.isCPF(textoCpf);
            while (cpfValido == false)
            {
                string CPF = geradorCNPJCPF.cpf(false);
                PreencherCampo(campoCPF, CPF);
                textoCpf = driver.FindElement(campoCPF).GetAttribute("value");
                cpfValido = geradorCNPJCPF.isCPF(textoCpf);
            }
            PreencherCampo(campoEmail, "alanspindler@live.com");
            PreencherCampo(campoResponsavel, geradorNome.GerarNome());
            PreencherCampo(campoSenha, "123456");
            PreencherCampo(campoConfirmarSenha, "123456");
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            return CNPJ;
        }

        #endregion
    }
}

