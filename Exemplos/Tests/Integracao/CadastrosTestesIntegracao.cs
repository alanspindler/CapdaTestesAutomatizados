using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;
using System.Threading;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.Tests
{
    [TestClass]
    public class TestesIntegracaoTestesAutomatizados
    {
        public Global Global { get; set; }
        public PaginaBase PaginaBase { get; set; }
        public PaginaInicial PaginaInicial { get; set; }
        public PaginaCapacidadePerfil paginaCapacidadePerfil { get; set; }        

        [TestMethod]
        public void IncluirNovaCapacidadeParaOutrosTestes()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            var paginaCapacidadePerfil = new PaginaCapacidadePerfil(selenium.driver);
            //Abre a pagina inicial
            PaginaBase = new PaginaBase(selenium.driver);
            PaginaInicial = new PaginaInicial(selenium.driver);
            //PaginaInicial.AbrirPagina(urlPaginaInicial);
            PaginaInicial.AbrirPagina("http://localhost:4200/#/capacidade-perfil");
            //Faz login
            PaginaInicial.FazerLogin("04621975000198", "123");
            paginaCapacidadePerfil.AguardarProcessando();
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Seleciona os valres das combos, preenche a quantidade de notas e clicar em salvar. Também confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //// Fecha o navegador            
            selenium.EncerrarTeste();
        }

        [TestMethod]
        public void IncluirNovoVistoriadorParaOutrosTestes()
        {
            //Inicializa instância do driver do Selenium
            var selenium = Global.obterInstancia();
            var paginaManterVistoriador = new PaginaManterVistoriador(selenium.driver);
            //Abre a pagina inicial
            PaginaBase = new PaginaBase(selenium.driver);
            PaginaInicial = new PaginaInicial(selenium.driver);
            //PaginaInicial.AbrirPagina(urlPaginaInicial);
            PaginaInicial.AbrirPagina("http://localhost:4200/#/vistoriador");        
            //Faz login
            PaginaInicial.FazerLogin("04621975000198", "123");
            PaginaBase.AguardarProcessando();
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            PaginaBase.AguardarProcessando();
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.AguardarProcessando();
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            PaginaBase.AguardarProcessando();
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            selenium.EncerrarTeste();
        }
    }
}

