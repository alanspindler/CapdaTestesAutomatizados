using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System;


namespace Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects
{
    [TestClass]
    public class PaginaAnalisarCredenciamento : PaginaBase
    {

        public PaginaAnalisarCredenciamento(RemoteWebDriver driver) : base(driver)
        {

        }

        #region Pagina Principal

        public By campoProcesso = By.Id("txtProcesso");
        public By botaoBuscar = By.Id("btnBuscar");
        public By botaoReceber = By.XPath("//span/a/i");
        public By botaoAnalisar = By.XPath("//span/a/i");        
        public By botaoLimpar = By.XPath("(//button[@type='button'])[2]");
        public By botaoParecer = By.XPath("//button");
        public By optionPeloCredenciamento = By.Name("radio[analista]");
        public By botaoConcluirAnalise = By.XPath("(//button[@type='button'])[11]");
        public By botaoNaoContinuarAnalise = By.XPath("(//button[@type='button'])[13]");

        public void Analisar(string codigoProcesso)
        {
            AguardarProcessando();
            ClicarElementoPagina(botaoLimpar);
            PreencherCampo(campoProcesso, codigoProcesso);
            ClicarElementoPagina(botaoBuscar);
            AguardarProcessando();
            ClicarElementoPagina(botaoReceber);
            AguardarProcessando();
            Thread.Sleep(3000);
            ClicarElementoPagina(botaoAnalisar);
            AguardarProcessando();                        
            AguardarElementoClicavel(botaoParecer);
            AguardarProcessando();
            ClicarElementoPagina(botaoParecer);
            AguardarProcessando();
            ClicarElementoPagina(optionPeloCredenciamento);
            ClicarElementoPagina(botaoConcluirAnalise);
            AguardarProcessando();
            ClicarElementoPagina(botaoNaoContinuarAnalise);
            AguardarProcessando();
            Thread.Sleep(2000);
            AguardarProcessando();
        }



    }

    #endregion
}