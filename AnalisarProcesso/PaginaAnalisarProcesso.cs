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
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoLimpar);
            PreencherCampo(driver, campoProcesso, codigoProcesso);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoReceber);
            AguardarProcessando(driver);
            Thread.Sleep(3000);
            ClicarElementoPagina(driver, botaoAnalisar);
            AguardarProcessando(driver);
            AguardarElementoClicavel(driver, botaoParecer);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoParecer);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, optionPeloCredenciamento);
            ClicarElementoPagina(driver, botaoConcluirAnalise);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNaoContinuarAnalise);
            AguardarProcessando(driver);
            Thread.Sleep(2000);
            AguardarProcessando(driver);
        }



    }

    #endregion
}