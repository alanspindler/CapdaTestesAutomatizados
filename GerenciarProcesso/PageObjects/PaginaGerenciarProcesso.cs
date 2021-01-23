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

        public PaginaGerenciarProcesso(RemoteWebDriver driver) : base(driver)
        {

        }

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
        public By botaoSalvar = By.XPath("(//button[@type='button'])[8]");

        public void DesignarAnalista(string codigoProcesso)
        {
            AguardarProcessando();
            ClicarElementoPagina(botaoLimpar);
            PreencherCampo(campoProcesso, codigoProcesso);
            ClicarElementoPagina(botaoBuscar);
            AguardarProcessando();
            ClicarElementoPagina(botaoDesignar);
            AguardarProcessando();
            SelecionarItemCombo(comboAnalista, "ALAN RAFAEL SPINDLER");
            ClicarElementoPagina(botaoOk);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
        }

        public void DespacharImediato(string codigoProcesso)
        {
            AguardarProcessando();
            ClicarElementoPagina(botaoLimpar);
            PreencherCampo(campoProcesso, codigoProcesso);
            ClicarElementoPagina(botaoBuscar);
            AguardarProcessando();
            ClicarElementoPagina(botaoDespachar);
            AguardarProcessando();            
            ClicarElementoPagina(botaoDespacho);
            AguardarProcessando();
            ClicarElementoPagina(abaDespachoImediato);
            AguardarProcessando();
            ClicarElementoPagina(optionDeAcordoSimImediato);
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();            
        }

        public void DespacharCoordenadorGeral(string codigoProcesso)
        {
            AguardarProcessando();
            ClicarElementoPagina(botaoLimpar);
            PreencherCampo(campoProcesso, codigoProcesso);
            ClicarElementoPagina(botaoBuscar);
            AguardarProcessando();
            ClicarElementoPagina(botaoDespachar);
            AguardarProcessando();
            ClicarElementoPagina(botaoDespacho);
            AguardarProcessando();
            ClicarElementoPagina(abaDespachoGeral);
            AguardarProcessando();
            ClicarElementoPagina(optionDeAcordoSimGeral);
            ClicarElementoPagina(optionSubmeterCapadaSIm);
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
        }

    }

    #endregion
}