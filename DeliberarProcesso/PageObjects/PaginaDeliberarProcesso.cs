using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System;


namespace Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects
{
    [TestClass]
    public class PaginaDeliberarProcesso : PaginaBase
    {

        #region Pagina Principal

        public By campoProcesso = By.Id("processo");
        public By botaoBuscar = By.Id("btnBuscar");
        public By checkGrid = By.Name("options");
        public By botaoLimpar = By.XPath("(//button[@type='button'])[2]");
        public By botaoEnviarCapda = By.XPath("(//button[@type='button'])[4]");
        public By botaoFechar = By.XPath("(//button[@type='button'])[8]");
        public By botaoDeliberar = By.XPath("//span/button");
        public By campoDataPublicacao = By.Id("dataPublicacao");
        public By botaoArquivo = By.Id("arquivo");
        public By botaoSalvar = By.XPath("(//button[@type='button'])[6]");
        public By botaoNenhumRegistroEncontrado = By.XPath("(//button[@type='button'])[9]");
        public By cnpjEmpresa = By.XPath("//td[3]");
        public By campoNumeroResolucao = By.Id("numeroResolucao");
        public By campoAvaliacaoApartir = By.Id("dataAvaliacao");

     
        public string Deliberar(string codigoProcesso)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoLimpar);
            PreencherCampo(driver, campoProcesso, codigoProcesso);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            string cnpj = RetornaTextoElemento(driver, cnpjEmpresa);
            ClicarElementoPagina(driver, checkGrid);
            ClicarElementoPagina(driver, botaoEnviarCapda);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
            Thread.Sleep(2000);
            ClicarElementoPagina(driver, botaoDeliberar);            
            AguardarProcessando(driver);
            PreencherCampo(driver, campoDataPublicacao, "2021-01-23");
            PreencherCampo(driver, campoNumeroResolucao, "12345");
            PreencherCampo(driver, campoAvaliacaoApartir, "2021-01-23");
            PreencherCampo(driver, botaoArquivo, Constantes.CaminhoPDF);
            ClicarElementoPagina(driver, botaoSalvar);
            Thread.Sleep(2000);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNenhumRegistroEncontrado);
            Thread.Sleep(2000);
            ClicarElementoPagina(driver, botaoFechar);
            return cnpj;            
        }
    }

    #endregion
}