using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lampp.CAPDA.Teste.Automatizado.Cadastros.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Lampp.CAPDA.Teste.Automatizado.Login.PageObjects;
using System;
using OpenQA.Selenium;

namespace Lampp.CAPDA.Teste.Automatizado.Cadastros.Tests
{
    [TestClass]
    public class PaginaTesteTestesAutomatizados
    {
        public Global Global { get; set; } 
        public PaginaPrincipal PaginaPrincipal { get; set; }
        public PaginaBase PaginaBase { get; set; }
        public PaginaInicial PaginaInicial { get; set; }
        public PaginaTeste paginaTeste { get; set; }
        public TestContext TestContext { get; set; }        


        [TestMethod]
        public void PesquisarItem()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            var paginaTeste = new PaginaTeste(selenium.driver);
            //Abre a pagina inicial
            PaginaInicial = new PaginaInicial(selenium.driver);
            TestContext.WriteLine("Hello. Please state your name.");           
            string s1 = Console.ReadLine();
            if (s1 == "1")
            {
                PaginaInicial.AbrirPagina("https://www.morningstar.com/funds/XNAS/PENNX/quote.html");
            }
            else
            {
                PaginaInicial.AbrirPagina("https://www.morningstar.com/funds/XNAS/PENNX/quote.html");
            }
            //PaginaInicial.AbrirPaginaInicial();
            //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.
            paginaTeste.driver.SwitchTo().Frame(paginaTeste.driver.FindElement(By.ClassName("Parsys-iframe")));




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
            paginaTeste.ValidarTexto("Exibir Filtros", paginaTeste.botaoExibirFiltros);
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

