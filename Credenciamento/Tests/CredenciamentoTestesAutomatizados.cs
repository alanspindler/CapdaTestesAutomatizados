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

        public PaginaCredenciamento paginaCredenciamento { get; set; }

        private string urlPaginaInscricao = "http://localhost:4200/#/inscricao";
        private string urlPaginaLogin = "http://localhost:4200/";
        
        private string urlPaginaLoginServidorDes = "http://1:1@capda.des.suframa.gov.br/";        
        private string urlPaginaInscricaoServidorDes = "https://capda.des.suframa.gov.br/#/inscricao";
        public string CNPJ;

        [TestMethod]
        public void FazerCredenciamento()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            paginaInscricao = new PaginaInscricao(selenium.driver);
            //Abre a pagina inicial
            paginaBase = new PaginaBase(selenium.driver);
            paginaInicial = new PaginaInicial(selenium.driver);
            paginaPrincipal = new PaginaPrincipal(selenium.driver);
            paginaCredenciamento = new PaginaCredenciamento(selenium.driver);
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
            paginaCredenciamento.PreencherCredenciamento();                
            ////// Fecha o navegador            
            //selenium.EncerrarTeste();            
        }

        
    }
}
