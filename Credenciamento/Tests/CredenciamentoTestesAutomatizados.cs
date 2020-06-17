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
            paginaInicial.AbrirPagina(urlPaginaInscricao);
            //Faz Login
            CNPJ = paginaInscricao.InscreverEmpresa();
            paginaInicial.AbrirPagina(urlPaginaLogin);
            paginaInicial.FazerLogin(CNPJ, "123456");
            paginaPrincipal.ExpandireAbrirMenuCredenciamento(true, paginaPrincipal.MenuAcompanharCredenciamento);
            paginaCredenciamento.SolicitarCredenciamento();
            paginaCredenciamento.PreencherCredenciamento();
                
            ////// Fecha o navegador            
            //selenium.EncerrarTeste();            
        }

        //[TestMethod]
        //public void AlterarPostoManaus()
        //{
        //    //Inicializa instância do driver do Selenium
        //    var selenium = Global.obterInstancia();
        //    var PaginaMunicipioTipoVistoria = new PaginaMunicipioTipoVistoria(selenium.driver);
        //    //Abre a pagina inicial
        //    PaginaBase = new PaginaBase(selenium.driver);
        //    PaginaInicial = new PaginaInicial(selenium.driver);
        //    PaginaInicial.AbrirPagina(urlPaginaInicial);            
        //    //Faz Login
        //    PaginaInicial.FazerLogin("04621975000198", "123");
        //    PaginaMunicipioTipoVistoria.AguardarProcessando();
        //    //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.            
        //    PaginaMunicipioTipoVistoria.PesquisarPostoVistoria("MANAUS");
        //    ////Valida a quantidade de resultados exibidos
        //    PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
        //    PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Fisica");
        //    //// Valida se o valor escolhido está na combo
        //    PaginaMunicipioTipoVistoria.AbrirAlterar(1);
        //    PaginaMunicipioTipoVistoria.ValidarValorCampo("MANAUS", PaginaMunicipioTipoVistoria.txtPostoVistoria);
        //    PaginaMunicipioTipoVistoria.ValidarValorCampo("Manaus/AM", PaginaMunicipioTipoVistoria.txtMunicipio);
        //    PaginaMunicipioTipoVistoria.AlterarTipoVistoria(false, "Teste Automatizado");
        //    ////Valida se o valores das combos volta, ao padrão após limpar            
        //    ////// Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
        //    PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
        //    PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Documental");
        //    PaginaMunicipioTipoVistoria.AbrirAlterar(1);
        //    PaginaMunicipioTipoVistoria.AlterarTipoVistoria(true, "Teste Automatizado");
        //    PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
        //    PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Fisica");
        //    //////Valida o Texto do botão antes de pressionar
        //    PaginaMunicipioTipoVistoria.ValidarTextoElemento("Ocultar Filtros", PaginaMunicipioTipoVistoria.botaoOcultarFiltros);
        //    //////Clica em Ocultar FIltros
        //    PaginaMunicipioTipoVistoria.OcultarFiltros();
        //    //////Valida que o botão Buscar não está sendo exibido
        //    PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.botaoPesquisar);
        //    //////Valida que o botão Buscar não está sendo exibido
        //    PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.botaoLimpar);
        //    //////Valida que a combo não está mais sendo exibida
        //    PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.comboPostoVistoria);
        //    ////Valida se texto do botão Ocultar FIltros foi alterado
        //    PaginaMunicipioTipoVistoria.ValidarTextoElemento("Exibir Filtros", PaginaMunicipioTipoVistoria.botaoExibirFiltros);
        //    //////Clica novamente no botão para voltar a exibir os filtros
        //    PaginaMunicipioTipoVistoria.ExibirFiltros();
        //    //////Valida que o botão Buscar está sendo exibido
        //    PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.botaoPesquisar);
        //    //////Valida que o botão Buscar está sendo exibido
        //    PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.botaoLimpar);
        //    //////Valida que a combo está mais sendo exibida
        //    PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.comboPostoVistoria);
        //    //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
        //    //////PaginaMunicipioTipoVistoria.ExportarPDF();
        //    ////// Fecha o navegador            
        //    selenium.EncerrarTeste();
        //}
    }
}
