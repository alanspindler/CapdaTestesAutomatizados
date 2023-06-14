using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lampp.CAPDA.Teste.Automatizado.Cadastros.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Lampp.CAPDA.Teste.Automatizado.Login.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects;
using System.Threading;
using System;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using OpenQA.Selenium;

namespace Lampp.CAPDA.Teste.Automatizado.Credenciamento.Tests
{
    [TestClass]
    public class CredenciamentoTestesAutomatizados
    {
        public Global global { get; set; }
        public PaginaBase paginaBase { get; set; }
        public PaginaInicial paginaInicial { get; set; }
        public PaginaPrincipal paginaPrincipal { get; set; }
        public PaginaInscricao paginaInscricao { get; set; }
        public PaginaGerenciarProcesso paginaGerenciarProcesso { get; set; }
        public PaginaCredenciamento paginaCredenciamento { get; set; }
        public PaginaAnalisarCredenciamento paginaAnalisarCredenciamento { get; set; }
        public PaginaDeliberarProcesso paginaDeliberarProcesso { get; set; }

        private string urlPaginaInscricao = "http://localhost:4200/#/inscricao";
        private string urlPaginaLogin = "http://localhost:4200/";
        private string urlPaginaGerenciarLocal = "http://localhost:4200/#/gerenciar-processo";
        private string urlPaginaAnalisarLocal = "http://localhost:4200/#/analisar-credenciamento";
        private string urlPaginaDeliberarLocal = "http://localhost:4200/#/deliberar-processo";

        private string urlPaginaLoginServidorDes = "http://1:1@capda.des.suframa.gov.br/";
        private string urlPaginaInscricaoServidorDes = "https://capda.des.suframa.gov.br/#/inscricao";
        private string urlPaginaGerenciarServidorDes = "https://capda.des.suframa.gov.br/#/gerenciar-processo";
        private string urlPaginaAnalisarServidorDes = "https://capda.des.suframa.gov.br/#/analisar-credenciamento";
        private string urlPaginaDeliberarServidorDes = "https://capda.des.suframa.gov.br/#/deliberar-processo";
        //private string urlPaginaLoginServidorDes = "http://1:1@capda.hom.suframa.gov.br/";
        //private string urlPaginaInscricaoServidorDes = "https://capda.hom.suframa.gov.br/#/inscricao";
        //private string urlPaginaGerenciarServidorDes = "https://capda.hom.suframa.gov.br/#/gerenciar-processo";
        //private string urlPaginaAnalisarServidorDes = "https://capda.hom.suframa.gov.br/#/analisar-credenciamento";
        //private string urlPaginaDeliberarServidorDes = "https://capda.hom.suframa.gov.br/#/deliberar-processo";
        public string CNPJ;


        public string FazerCredenciamento(WebDriver driver)
        {            
            paginaInscricao = new PaginaInscricao();
            //Abre a pagina inicial
            paginaBase = new PaginaBase();
            paginaInicial = new PaginaInicial();
            paginaPrincipal = new PaginaPrincipal();
            paginaCredenciamento = new PaginaCredenciamento();
            string codigoCredenciamento;
            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(driver, urlPaginaInscricao);
            }
            else
            {
                paginaInicial.AbrirPagina(driver, urlPaginaInscricaoServidorDes);
            }
            //Faz Login
            CNPJ = paginaInscricao.InscreverEmpresa(driver);
            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(driver,urlPaginaLogin);
                paginaInicial.FazerLogin(driver,CNPJ, "123456");
            }
            else
            {
                paginaInicial.AbrirPagina(driver, urlPaginaLoginServidorDes);
                paginaInicial.FazerLoginServidor(driver, CNPJ, "123456");
            }

            paginaPrincipal.ExpandireAbrirMenuCredenciamento(true);
            paginaCredenciamento.SolicitarCredenciamento();
            codigoCredenciamento = paginaCredenciamento.PreencherCredenciamento(driver);
            return codigoCredenciamento;
        }

        [TestMethod]
        public void EfetuarCredenciamento()
        {
            //Inicializa instância do driver do Selenium
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--ignore-certificate-errors");
            options.AddExtension(Constantes.CaminhoExtensao);
            options.Proxy = null;
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var driver = new ChromeDriver(options);            
            for (int i = 1; i <= Constantes.QuantidadeCredenciamentos; i++)
            {                
                string codigoCredenciamento = FazerCredenciamento(driver);
                paginaInicial = new PaginaInicial();
                if (Constantes.TesteSistemalocal)
                {
                    paginaInicial.AbrirPagina(driver, urlPaginaGerenciarLocal);
                }
                else
                {
                    paginaInicial.AbrirPagina(driver, urlPaginaGerenciarServidorDes);
                }
                paginaGerenciarProcesso = new PaginaGerenciarProcesso();
                paginaGerenciarProcesso.DesignarAnalista(codigoCredenciamento);

                if (Constantes.TesteSistemalocal)
                {
                    paginaInicial.FazerLogout(driver);
                    paginaInicial.AbrirPagina(driver, urlPaginaLogin);
                    paginaInicial.FazerLogin(driver,"00092385060", "lamppit@2020");
                    paginaInicial.AbrirPagina(driver, urlPaginaAnalisarLocal);
                }
                else
                {
                    paginaInicial.FazerLogout(driver);
                    paginaInicial.AbrirPagina(driver, urlPaginaLoginServidorDes);
                    paginaInicial.FazerLoginServidor(driver,"00092385060", "lamppit@2020");
                    paginaInicial.AbrirPagina(driver, urlPaginaAnalisarServidorDes);
                }

                paginaAnalisarCredenciamento = new PaginaAnalisarCredenciamento();
                paginaAnalisarCredenciamento.Analisar(codigoCredenciamento);                

                if (Constantes.TesteSistemalocal)
                {
                    paginaInicial.AbrirPagina(driver, urlPaginaGerenciarLocal);
                }
                else
                {
                    paginaInicial.AbrirPagina(driver, urlPaginaGerenciarServidorDes);
                }

                paginaGerenciarProcesso.DespacharImediato(codigoCredenciamento);
                Thread.Sleep(2000);
                paginaGerenciarProcesso.DespacharCoordenadorGeral(codigoCredenciamento);

                Thread.Sleep(2000);
                if (Constantes.TesteSistemalocal)
                {
                    paginaInicial.AbrirPagina(driver, urlPaginaDeliberarLocal);
                }
                else
                {
                    paginaInicial.AbrirPagina(driver, urlPaginaDeliberarServidorDes);
                }
                paginaDeliberarProcesso = new PaginaDeliberarProcesso();
                string cnpj = paginaDeliberarProcesso.Deliberar(codigoCredenciamento);
                paginaBase.GravarArquivoTexto(cnpj + " " + DateTime.Now.ToString());
                paginaBase.FazerLogout(driver);
                
            }
            driver.Quit();
        }
    }
}
