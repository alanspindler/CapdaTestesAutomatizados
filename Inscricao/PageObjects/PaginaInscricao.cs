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

        #endregion

        #region Métodos públicos

        /// <param name="rand">A Random that is used to pick names</param>
        public PaginaInscricao(RemoteWebDriver driver) : base(driver)
        {           
           geradorNome = new GeradorNome();
           geradorCNPJCPF = new GeradorCNPJCPF();
        }

        //Exclui o item da linha selecionado. Confirma a exclusão de acotdo com o parametro confirmar
        public string InscreverEmpresa()
        {
            AguardarProcessando();            
            PreencherCampo(campoInstituicao, geradorNome.GerarNome());
            string CNPJ = geradorCNPJCPF.GerarCNPJ();
            PreencherCampo(campoCNPJ, CNPJ);
            PreencherCampo(campoCPF, geradorCNPJCPF.CpfSemMascara(1));
            AguardarProcessando();
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

