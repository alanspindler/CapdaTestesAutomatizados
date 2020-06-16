using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;
using System.Threading;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.Tests
{
    [TestClass]
    public class MunicipioTipoVistoriaTestesAutomatizados
    {
        public Global Global { get; set; }
        public PaginaBase PaginaBase { get; set; }
        public PaginaInicial PaginaInicial { get; set; }
        public PaginaMunicipioTipoVistoria PaginaMunicipioTipoVistoria { get; set; }
        private string urlPaginaInicial = "http://localhost:4200/#/capacidade-posto-vistoria";


        [TestMethod]
        public void PesquisarItem()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            var PaginaMunicipioTipoVistoria = new PaginaMunicipioTipoVistoria(selenium.driver);
            //Abre a pagina inicial
            PaginaBase = new PaginaBase(selenium.driver);
            PaginaInicial = new PaginaInicial(selenium.driver);
            PaginaInicial.AbrirPagina(urlPaginaInicial);         
            //Faz Login
            PaginaInicial.FazerLogin("04621975000198", "123");
            PaginaMunicipioTipoVistoria.AguardarProcessando();
            //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.            
            PaginaMunicipioTipoVistoria.PesquisarPostoVistoria("MANAUS");
            ////Valida a quantidade de resultados exibidos
            PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Vistoria no Posto: ", PaginaMunicipioTipoVistoria.labelVistoriaNoPosto);
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Vistoria Externa: ", PaginaMunicipioTipoVistoria.labelVistoriaExterna);
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Vistoria Documental: ", PaginaMunicipioTipoVistoria.labelVistoriaDocumental);
            PaginaMunicipioTipoVistoria.ValidarTexto("Total:", PaginaMunicipioTipoVistoria.labelTotalVistoria);            
            PaginaMunicipioTipoVistoria.ValidarValorCampo("300", PaginaMunicipioTipoVistoria.txtQuantidadeVistoriaNoPosto);
            PaginaMunicipioTipoVistoria.ValidarValorCampo("0", PaginaMunicipioTipoVistoria.txtQuantidadeVistoriaExterna);
            PaginaMunicipioTipoVistoria.ValidarValorCampo("0", PaginaMunicipioTipoVistoria.txtQuantidadeVistoriaDocumental);
            PaginaMunicipioTipoVistoria.ValidarValorCampo("300", PaginaMunicipioTipoVistoria.txtQuantidadeVistoriaTotal);
            PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Fisica");
            PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(2, "1300029", "Alvarães/AM", "Vistoria Documental");
            PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(9, "1300409", "Barcelos/AM", "Vistoria Documental");            
            //// Valida se o valor escolhido está na combo
            PaginaMunicipioTipoVistoria.ValidarTextoElementoSelecionadoCombo("MANAUS", PaginaMunicipioTipoVistoria.comboPostoVistoria);
            ////Clica no botão Limpar
            PaginaMunicipioTipoVistoria.Limpar();
            PaginaMunicipioTipoVistoria.AguardarProcessando();
            ////Valida se o valores das combos volta, ao padrão após limpar
            PaginaMunicipioTipoVistoria.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção", PaginaMunicipioTipoVistoria.comboPostoVistoria);                        
            PaginaMunicipioTipoVistoria.ValidarLinhasGrid(0);          
            //////Valida o Texto do botão antes de pressionar
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Ocultar Filtros", PaginaMunicipioTipoVistoria.botaoOcultarFiltros);
            //////Clica em Ocultar FIltros
            PaginaMunicipioTipoVistoria.OcultarFiltros();
            //////Valida que o botão Buscar não está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.botaoPesquisar);
            //////Valida que o botão Buscar não está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.botaoLimpar);
            //////Valida que a combo não está mais sendo exibida
            PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.comboPostoVistoria);                        
            ////Valida se texto do botão Ocultar FIltros foi alterado
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Exibir Filtros", PaginaMunicipioTipoVistoria.botaoExibirFiltros);
            //////Clica novamente no botão para voltar a exibir os filtros
            PaginaMunicipioTipoVistoria.ExibirFiltros();
            //////Valida que o botão Buscar está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.botaoPesquisar);
            //////Valida que o botão Buscar está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.botaoLimpar);
            //////Valida que a combo está mais sendo exibida
            PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.comboPostoVistoria);            
            //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            //////PaginaMunicipioTipoVistoria.ExportarPDF();
            ////// Fecha o navegador            
            selenium.EncerrarTeste();
        }

        [TestMethod]
        public void AlterarPostoManaus()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            var PaginaMunicipioTipoVistoria = new PaginaMunicipioTipoVistoria(selenium.driver);
            //Abre a pagina inicial
            PaginaBase = new PaginaBase(selenium.driver);
            PaginaInicial = new PaginaInicial(selenium.driver);
            PaginaInicial.AbrirPagina(urlPaginaInicial);            
            //Faz Login
            PaginaInicial.FazerLogin("04621975000198", "123");
            PaginaMunicipioTipoVistoria.AguardarProcessando();
            //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.            
            PaginaMunicipioTipoVistoria.PesquisarPostoVistoria("MANAUS");
            ////Valida a quantidade de resultados exibidos
            PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
            PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Fisica");
            //// Valida se o valor escolhido está na combo
            PaginaMunicipioTipoVistoria.AbrirAlterar(1);
            PaginaMunicipioTipoVistoria.ValidarValorCampo("MANAUS", PaginaMunicipioTipoVistoria.txtPostoVistoria);
            PaginaMunicipioTipoVistoria.ValidarValorCampo("Manaus/AM", PaginaMunicipioTipoVistoria.txtMunicipio);
            PaginaMunicipioTipoVistoria.AlterarTipoVistoria(false, "Teste Automatizado");
            ////Valida se o valores das combos volta, ao padrão após limpar            
            ////// Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
            PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Documental");
            PaginaMunicipioTipoVistoria.AbrirAlterar(1);
            PaginaMunicipioTipoVistoria.AlterarTipoVistoria(true, "Teste Automatizado");
            PaginaMunicipioTipoVistoria.ValidarLinhasGrid(10);
            PaginaMunicipioTipoVistoria.ValidarItensResultadoPesquisa(1, "1302603", "Manaus/AM", "Vistoria Fisica");
            //////Valida o Texto do botão antes de pressionar
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Ocultar Filtros", PaginaMunicipioTipoVistoria.botaoOcultarFiltros);
            //////Clica em Ocultar FIltros
            PaginaMunicipioTipoVistoria.OcultarFiltros();
            //////Valida que o botão Buscar não está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.botaoPesquisar);
            //////Valida que o botão Buscar não está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.botaoLimpar);
            //////Valida que a combo não está mais sendo exibida
            PaginaMunicipioTipoVistoria.ValidarElementoNaoPresente(PaginaMunicipioTipoVistoria.comboPostoVistoria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            PaginaMunicipioTipoVistoria.ValidarTextoElemento("Exibir Filtros", PaginaMunicipioTipoVistoria.botaoExibirFiltros);
            //////Clica novamente no botão para voltar a exibir os filtros
            PaginaMunicipioTipoVistoria.ExibirFiltros();
            //////Valida que o botão Buscar está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.botaoPesquisar);
            //////Valida que o botão Buscar está sendo exibido
            PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.botaoLimpar);
            //////Valida que a combo está mais sendo exibida
            PaginaMunicipioTipoVistoria.ValidarElementoPresente(PaginaMunicipioTipoVistoria.comboPostoVistoria);
            //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            //////PaginaMunicipioTipoVistoria.ExportarPDF();
            ////// Fecha o navegador            
            selenium.EncerrarTeste();
        }
    }
}
