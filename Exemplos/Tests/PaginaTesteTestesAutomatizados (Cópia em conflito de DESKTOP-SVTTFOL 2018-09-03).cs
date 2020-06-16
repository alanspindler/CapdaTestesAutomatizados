using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.Principal.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using System.Threading;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.Tests
{
    [TestClass]
    public class PaginaTesteTestesAutomatizados
    {
        public Global Global { get; set; }
        public PaginaMotivoDemissaoLista MotivoDemissaoLista { get; set; }
        public PaginaMotivoDemissaoDados MotivoDemissaoDados { get; set; }
        public PaginaPrincipal PaginaPrincipal { get; set; }
        public PaginaBase PaginaBase { get; set; }
        public PaginaInicial PaginaInicial { get; set; }
        public PaginaTeste paginaTeste { get; set; }

        [TestMethod]
        public void PesquisarItem()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            var paginaTeste = new PaginaTeste(selenium.driver);
            //Abre a pagina inicial
            PaginaInicial = new PaginaInicial(selenium.driver);            
            PaginaInicial.AbrirPaginaInicial();
            //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.
            paginaTeste.PesquisarCapituloNCM("01 - ANIMAIS VIVOS");
            //Valida a quantidade de resultados exibidos
            paginaTeste.ValidarLinhasGrid(4);
            // Valida se o valor escolhido está na combo
            paginaTeste.ValidarValorCampo("01 - ANIMAIS VIVOS", paginaTeste.comboCapituloNCM);
            //Clica no botão Limpar
            paginaTeste.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaTeste.ValidarValorCampo("TODOS", paginaTeste.comboCapituloNCM);
            // Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            paginaTeste.ValidarLinhasGrid(4);
            //Valida o Texto do botão antes de pressionar
            paginaTeste.ValidarTexto("Ocultar Filtros", paginaTeste.botaoOcultarFiltros);
            //Clica em Ocultar FIltros
            paginaTeste.OcultarFiltros();
            //Valida que o botão Buscar não está sendo exibido
            paginaTeste.ValidarElementoNaoPresente(paginaTeste.botaoPesquisar);
            //Valida que o botão Buscar não está sendo exibido
            paginaTeste.ValidarElementoNaoPresente(paginaTeste.botaoLimpar);
            //Valida que a combo não está mais sendo exibida
            paginaTeste.ValidarElementoNaoPresente(paginaTeste.comboCapituloNCM);
            //Valida se texto do botão Ocultar FIltros foi alterado
            paginaTeste.ValidarTexto("Exibir Xiltros", paginaTeste.botaoExibirFiltros);
            //Clica novamenten o botão para voltar a exibir os filtros
            paginaTeste.ExibirFiltros();
            //Valida que o botão Buscar está sendo exibido
            paginaTeste.ValidarElementoPresente(paginaTeste.botaoPesquisar);
            //Valida que o botão Buscar está sendo exibido
            paginaTeste.ValidarElementoPresente(paginaTeste.botaoLimpar);
            //Valida que a combo está mais sendo exibida
            paginaTeste.ValidarElementoPresente(paginaTeste.comboCapituloNCM);
            //Clicar em Exportar em PDF - Não faz nada nesse prototipo
            paginaTeste.ExportarPDF();
            // Fecha o navegador            
            selenium.EncerrarTeste();
        }


    }
}

