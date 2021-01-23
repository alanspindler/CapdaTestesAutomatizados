using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lampp.CAPDA.Teste.Automatizado.Cadastros.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Lampp.CAPDA.Teste.Automatizado.Login.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects;
using System.Threading;

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
        public string CNPJ;


        public string FazerCredenciamento()
        {

            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            paginaInscricao = new PaginaInscricao(selenium.driver);
            //Abre a pagina inicial
            paginaBase = new PaginaBase(selenium.driver);
            paginaInicial = new PaginaInicial(selenium.driver);
            paginaPrincipal = new PaginaPrincipal(selenium.driver);
            paginaCredenciamento = new PaginaCredenciamento(selenium.driver);
            string codigoCredenciamento;
            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(urlPaginaInscricao);
            }
            else
            {
                paginaInicial.AbrirPagina(urlPaginaInscricaoServidorDes);
            }
            //Faz Login
            CNPJ = paginaInscricao.InscreverEmpresa();
            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(urlPaginaLogin);
                paginaInicial.FazerLogin(CNPJ, "123456");
            }
            else
            {
                paginaInicial.AbrirPagina(urlPaginaLoginServidorDes);
                paginaInicial.FazerLoginServidor(CNPJ, "123456");
            }

            paginaPrincipal.ExpandireAbrirMenuCredenciamento(true);
            paginaCredenciamento.SolicitarCredenciamento();
            codigoCredenciamento = paginaCredenciamento.PreencherCredenciamento();
            return codigoCredenciamento;
        }

        [TestMethod]
        public void EfetuarCredenciamento()
        {
            var selenium = Global.obterInstancia();
            string codigoCredenciamento = FazerCredenciamento();
            paginaInicial = new PaginaInicial(selenium.driver);
            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(urlPaginaGerenciarLocal);
            }
            else
            {
                paginaInicial.AbrirPagina(urlPaginaGerenciarServidorDes);
            }
            paginaGerenciarProcesso = new PaginaGerenciarProcesso(selenium.driver);
            paginaGerenciarProcesso.DesignarAnalista(codigoCredenciamento);

            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.FazerLogout();
                paginaInicial.AbrirPagina(urlPaginaLogin);
                paginaInicial.FazerLogin("00092385060", "lamppit@2020");
                paginaInicial.AbrirPagina(urlPaginaAnalisarLocal);
            }
            else
            {
                paginaInicial.FazerLogout();
                paginaInicial.AbrirPagina(urlPaginaLoginServidorDes);
                paginaInicial.FazerLoginServidor("00092385060", "lamppit@2020");
                paginaInicial.AbrirPagina(urlPaginaAnalisarServidorDes);
            }

            paginaAnalisarCredenciamento = new PaginaAnalisarCredenciamento(selenium.driver);
            paginaAnalisarCredenciamento.Analisar(codigoCredenciamento);
            //paginaAnalisarCredenciamento.Analisar("CRE0252021");

            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(urlPaginaGerenciarLocal);
            }
            else
            {
                paginaInicial.AbrirPagina(urlPaginaGerenciarServidorDes);
            }

            paginaGerenciarProcesso.DespacharImediato(codigoCredenciamento);
            Thread.Sleep(2000);
            paginaGerenciarProcesso.DespacharCoordenadorGeral(codigoCredenciamento);

            Thread.Sleep(2000);
            if (Constantes.TesteSistemalocal)
            {
                paginaInicial.AbrirPagina(urlPaginaDeliberarLocal);
            }
            else
            {
                paginaInicial.AbrirPagina(urlPaginaDeliberarServidorDes);
            }
            paginaDeliberarProcesso = new PaginaDeliberarProcesso(selenium.driver);
            string cnpj = paginaDeliberarProcesso.Deliberar(codigoCredenciamento);
            paginaBase.GravarArquivoTexto(cnpj);
        }
    }
}
