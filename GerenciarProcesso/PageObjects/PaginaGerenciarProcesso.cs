using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System;


namespace Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects
{
    [TestClass]
    public class PaginaGerenciarProcesso : PaginaBase
    {



        #region Pagina Principal

        public By campoProcesso = By.Id("processo");
        public By botaoBuscar = By.Id("btnBuscar");
        public By botaoDesignar = By.XPath("//section[@id='content']/section/section/section/section/app-gerenciar-processo/div/div[3]/div/section/app-designarprocesso-grid/app-grid/section/div[2]/table/tbody/tr/td[7]/span/button/i");
        public By comboAnalista = By.XPath("//div[2]/div/div/div/app-drop-list/select");
        public By botaoLimpar = By.XPath("(//button[@type='button'])[3]");
        public By botaoOk = By.XPath("(//button[@type='button'])[5]");
        public By botaoFechar = By.XPath("(//button[@type='button'])[7]");
        public By botaoDespachar = By.XPath("//span/a/i");
        public By botaoDespacho = By.XPath("//button");
        public By abaDespachoImediato = By.LinkText("Despacho do Coordenador Imediato");
        public By abaDespachoGeral = By.LinkText("Despacho do Coordenador Geral");
        public By optionDeAcordoSimImediato = By.Id("opcaoImediatoSim");
        public By optionDeAcordoSimGeral = By.Id("opcaoGeralSim");
        public By optionSubmeterCapadaSIm = By.Id("opcaoGeralSubmeterSim");
        public By botaoSalvar = By.XPath("(//button[@type='button'])[9]");

        public void DesignarAnalista(string codigoProcesso)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoLimpar);
            PreencherCampo(driver, campoProcesso, codigoProcesso);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoDesignar);
            AguardarProcessando(driver);
            SelecionarItemCombo(driver,comboAnalista, "ALAN RAFAEL SPINDLER");
            ClicarElementoPagina(driver, botaoOk);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        public void DespacharImediato(string codigoProcesso)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoLimpar);
            PreencherCampo(driver, campoProcesso, codigoProcesso);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoDespachar);
            AguardarProcessando(driver);
            AguardarElementoClicavel(driver,botaoDespacho);
            ClicarElementoPagina(driver, botaoDespacho);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaDespachoImediato);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, optionDeAcordoSimImediato);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
        }

        public void DespacharCoordenadorGeral(string codigoProcesso)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoLimpar);
            PreencherCampo(driver, campoProcesso, codigoProcesso);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoDespachar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoDespacho);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaDespachoGeral);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, optionDeAcordoSimGeral);
            ClicarElementoPagina(driver, optionSubmeterCapadaSIm);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
        }

    }

    #endregion
}