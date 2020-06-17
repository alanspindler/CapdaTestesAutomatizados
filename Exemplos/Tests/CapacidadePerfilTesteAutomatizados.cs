using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lampp.CAPDA.Teste.Automatizado.Cadastros.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Lampp.CAPDA.Teste.Automatizado.Login.PageObjects;
using System.Threading;
using Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects;
using System;

namespace Lampp.CAPDA.Teste.Automatizado.Cadastros.Tests
{
    [TestClass]
    public class CapacidadePerfilTestesAutomatizados
    {
        static Global Global { get; set; }
        static PaginaBase PaginaBase { get; set; }
        static PaginaInicial PaginaInicial { get; set; }
        static PaginaPrincipal PaginaPrincipal { get; set; }
        static PaginaCapacidadePerfil paginaCapacidadePerfil { get; set; }
        static string urlPaginaInicial = "http://localhost:4200/#/capacidade-perfil";
        static Global selenium;
        

        [ClassInitialize]
        public static void IniciarSuiteTeste(TestContext testContext)
        {            
            //////Inicializa instância do driver do Selenium
            //selenium = Global.obterInstancia();            
            //paginaCapacidadePerfil = new PaginaCapacidadePerfil(selenium.driver);
            //////Abre a pagina inicial
            //PaginaBase = new PaginaBase(selenium.driver);
            //PaginaInicial = new PaginaInicial(selenium.driver);
            //PaginaPrincipal = new PaginaPrincipal(selenium.driver);
            //PaginaInicial.AbrirPagina(urlPaginaInicial);
            //////Faz login
            //PaginaInicial.FazerLogin(Constantes.USUARIO_COORDENADOR, Constantes.SENHA_COORDENADOR);
            //PaginaBase.AguardarProcessando();
        }

        //[ClassCleanup]
        //public static void FinalizarSuiteTeste()
        //{
        //    //Fecha o navegador            
        //    selenium.EncerrarTeste();
        //}


        [TestInitialize]
        public void IniciarTeste()
        {
            //Inicializa instância do driver do Selenium
            selenium = Global.obterInstancia();            
            paginaCapacidadePerfil = new PaginaCapacidadePerfil(selenium.driver);
            //Abre a pagina inicial
            PaginaBase = new PaginaBase(selenium.driver);
            PaginaInicial = new PaginaInicial(selenium.driver);
            PaginaPrincipal = new PaginaPrincipal(selenium.driver);
            PaginaInicial.AbrirPagina(urlPaginaInicial);
            //Faz login
            PaginaInicial.FazerLogin(Constantes.USUARIO_COORDENADOR, Constantes.SENHA_COORDENADOR);
            PaginaBase.AguardarProcessando();
        }

        [TestCleanup]
        public void FinalizarTeste()
        {
            //Fecha o navegador            
            //selenium.EncerrarTeste();
        }

        [TestMethod]
        public void PesquisarItem()
        {
            //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.            
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //// Valida se o valor escolhido está na combo
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("MANAUS",
                paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("VISTORIADOR INTERNO",
                paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Todas",
            //paginaCapacidadePerfil.comboCargaHoraria);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valores das combos volta, ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
                paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Todos", paginaCapacidadePerfil.comboPerfil);           
            //paginaCapacidadePerfil.ValidarLinhasGrid(0);
            ////Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            paginaCapacidadePerfil.AguardarProcessando();
            //Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            ////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            ////paginaCapacidadePerfil.ExportarPDF();            
        }

        [TestMethod]
        public void IncluirNovaCapacidade()
        {
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
            //// Valida se o valor escolhido está na combo
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("MANAUS",
                paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("VISTORIADOR INTERNO",
                paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Todas",
            //paginaCapacidadePerfil.comboCargaHoraria);
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
                paginaCapacidadePerfil.comboPostoVistoria);
            //// Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            //paginaCapacidadePerfil.ValidarLinhasGrid(0);
            ////Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            ////paginaCapacidadePerfil.ExportarPDF();
            //// Fecha o navegador            
        }


        [TestMethod]
        public void IncluirNovaCapacidadeComCargaHoraria11()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Seleciona os valres das combos, preenche a quantidade de notas e clicar em salvar. Também confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR EXTERNO", "11",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR EXTERNO");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Externo", "11", "300");
            //// Valida se o valor escolhido está na combo
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("MANAUS",
                paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("VISTORIADOR EXTERNO",
                paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Todas",
            //paginaCapacidadePerfil.comboCargaHoraria);
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
                paginaCapacidadePerfil.comboPostoVistoria);
            //// Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            //paginaCapacidadePerfil.ValidarLinhasGrid(0);
            ////Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            ////paginaCapacidadePerfil.ExportarPDF();                    
        }


        [TestMethod]
        public void IncluirNovaCapacidadeComCargaHorariaInvalida()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Seleciona os valres das combos, preenche a quantidade de notas e clicar em salvar. Também confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR EXTERNO", "13",
                "300", "Informe um valor entre 1 e 12");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Informe um valor entre 1 e 12", paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.CancelarCadastro();
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "Todos");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(0);            
        }

        [TestMethod]
        public void IncluirNovaCapacidadeComCargaHorariaVazia()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Seleciona os valres das combos, preenche a quantidade de notas e clicar em salvar. Também confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR EXTERNO", "",
                "300", "Selecione a Carga Horária");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Selecione a Carga Horária", paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.CancelarCadastro();
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR EXTERNO");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
        }

        [TestMethod]
        public void IncluirNovaCapacidadeComQtdadeNfeVazia()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Seleciona os valres das combos, preenche a quantidade de notas e clicar em salvar. Também confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR EXTERNO", "10",
                "", "Informe a Capacidade de NFs");
            //Valida mensagem de Falha
            paginaCapacidadePerfil.ValidarTexto("Informe a Capacidade de NFs", paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de Falha
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.CancelarCadastro();
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR EXTERNO");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
        }

        [TestMethod]
        public void IncluirCapacidadeDuplicada()
        {
            //Faz a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Exclui todos os itens, caso encontre (caso outro teste não finalize corretamente e deixe itens cadastrados.
            paginaCapacidadePerfil.ExcluirTodosItens();
            //Clica em Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Preenche os dados da capacidade de Perfil, clica em Salvar e Confirma
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Fecha a mensagem de sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Clica em adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            //Preenche os dados da capacidade de perfil, clica em Salvar e Confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "400");
            //Valida retorno do erro.
            paginaCapacidadePerfil.ValidarTexto(
                "Já existe uma capacidade cadastrada para o Posto de Vistoria, Perfil e Carga Horária informados",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de erro.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            //Clica em Cancelar para voltar à pagina de pesquisa.
            paginaCapacidadePerfil.CancelarCadastro();
            //Efetua novamente a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            ////Valida a quantidade de resultados exibidos. Deve ser 1, pois o segundo cadastro gerou erro.
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //// Exclui o item
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida se item foi corretamente excluido.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
                paginaCapacidadePerfil.comboPostoVistoria);
            //// Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            //paginaCapacidadePerfil.ValidarLinhasGrid(0);
            ////Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            ////Clica novamenten o botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);            
        }

        [TestMethod]
        public void IncluirDuasCapacidadesExcluirUma()
        {
            //Clica em Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Preenche os dados, clica em Salvar e Confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha mensagem de sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Efetua a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Clica em Adicionar.
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            //Preenche os dados, clica em Salvar e Confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR EXTERNO", "8",
                "400");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "Todos");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(2);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Externo", "8",
                "400");
            //Exclui o primeiro item
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que só existe um item após a exclusão.
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Exclui o item restante.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que não existem mais itens.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
                paginaCapacidadePerfil.comboPostoVistoria);            
            ////Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);            
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            ////Clica novamenten o botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);            
        }

        [TestMethod]
        public void AlterarCapacidadePerfil()
        {
            //Clica em Incluir.
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Inclui os dados, clica em Salvar e Confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de confirmação.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida se resultado da pesquisa está de acordo com item cadastrado.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Clica em Alterar.
            paginaCapacidadePerfil.AbrirAlterar(1);
            //Altera a Quantidade de BF e Insere Justificativa. CLica em Salvar e em Confirmar.
            paginaCapacidadePerfil.AlterarCapacidadePesquisa(true, "650", "Teste Alteracao para 650");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha Mensagem de Sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida valores alterados no resultado da pesquisa.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "650");
            //Valida a quantidade de itens exibidos.
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Exclui o item.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida se o item foi realmente foi excluido.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
        }

        [TestMethod]
        public void ValidarHistoricoInclusao()
        {
            //Clica no botão Adicionar.
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Inclui os dados, clica em Salvar e em Confirmar.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de confirmação.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida a quantidade de itens exibidos.
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Valida os dados do item exibido.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            //Clica no botao do Historico do Item            
            paginaCapacidadePerfil.AbrirHistorico(1);
            //Valida os itens do histórico
            paginaCapacidadePerfil.ValidarItensHistorico(1, "MANAUS", "Vistoriador Interno", "6", "300",
                "Inclusão", "");
            //Fecha o histórico
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFechar);
            //Clica em Alterar
            paginaCapacidadePerfil.AbrirAlterar(1);
            //Altera a quantidade de NF
            paginaCapacidadePerfil.AlterarCapacidadePesquisa(true, "650", "Teste Alteracao para 650");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de confirmação.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            //Efetua a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida o texto do item da pesquisa
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "650");
            //Abrir histórico do item exibido
            paginaCapacidadePerfil.AbrirHistorico(1);
            //Valida a primeira linha do histórico
            paginaCapacidadePerfil.ValidarItensHistorico(1, "MANAUS", "Vistoriador Interno", "6", "300",
                "Inclusão", "");
            //Valida a segunda linha do histórico
            paginaCapacidadePerfil.ValidarItensHistorico(2, "MANAUS", "Vistoriador Interno", "6", "650",
                "Alteração", "Teste Alteracao para 650");
            //Fecha o histórico
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFechar);
            //Exclui o item
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
        }


        [TestMethod]
        public void IncluirDuasCapacidadesExcluirASegunda()
        {
            //Clicar em Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Preenche os dados, clica em Salvar e confirmar o cadastro.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha mensagem de confirmação.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Clica em Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            //Preenche os dados, clica em Salvar e confirmar o cadastro.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "8",
                "400");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(2);
            //Valida os dados do primeiro item da pesquisa.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            //Valida os dados do segundo item da pesquisa.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Interno", "8",
                "400");
            //Excluir o segundo item da pesquisa.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 2);
            //paginaCapacidadePerfil.ExcluirItemTextoSelecionado(true, "400");
            //Valida os dados do primeiro item da pesquisa.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            //Valida a quantidade de itens retornados.
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Exclui o item restante.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida a exclusão do item.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
                paginaCapacidadePerfil.comboPostoVistoria);
            //// Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            //paginaCapacidadePerfil.ValidarLinhasGrid(0);
            ////Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboCargaHoraria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            ////Clica novamenten o botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            ////Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboCargaHoraria);
        }

        [TestMethod]
        public void ValidarOrdenacaoPesquisa()
        {
            //Clica em Adicionar
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            paginaCapacidadePerfil.AguardarProcessando();
            //Preenche os dados, clica em Salvar e Confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR INTERNO", "6",
                "300");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha mensagem de sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            paginaCapacidadePerfil.AguardarProcessando();
            //Efetua a pesquisa
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "VISTORIADOR INTERNO");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Clica em Adicionar.
            PaginaBase.ClicarElementoPagina(paginaCapacidadePerfil.botaoAdicionar);
            //Preenche os dados, clica em Salvar e Confirma.
            paginaCapacidadePerfil.IncluirCapacidadePesquisa(true, "MANAUS", "VISTORIADOR EXTERNO", "8",
                "400");
            //Valida mensagem de sucesso
            paginaCapacidadePerfil.ValidarTexto("Operação realizada com sucesso!",
                paginaCapacidadePerfil.mensagemRetorno);
            //Fecha a mensagem de sucesso.
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa.
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "Todos");
            ////Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ValidarLinhasGrid(2);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Externo", "8",
                "400");
            //Orderna o resultado da pesquisa por Tipo de Vistoriador
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.ordernarCargaHoraria);
            Thread.Sleep(2000);
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Externo", "8",
                "400");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.ordernarCargaHoraria);
            Thread.Sleep(2000);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Externo", "8",
                "400");
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.ordernarPerfil);
            Thread.Sleep(2000);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Externo", "8",
                "400");
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.ordernarPerfil);
            Thread.Sleep(2000);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Externo", "8",
                "400");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ClicarElementoPagina(paginaCapacidadePerfil.ordernarPerfil);
            Thread.Sleep(2000);
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(1, "MANAUS", "Vistoriador Interno", "6",
                "300");
            paginaCapacidadePerfil.ValidarItensResultadoPesquisa(2, "MANAUS", "Vistoriador Externo", "8",
                "400"); //Exclui o primeiro item
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que só existe um item após a exclusão.
            paginaCapacidadePerfil.ValidarLinhasGrid(1);
            //Exclui o item restante.
            paginaCapacidadePerfil.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que não existem mais itens.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaCapacidadePerfil.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaCapacidadePerfil.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção",
            paginaCapacidadePerfil.comboPostoVistoria);
            // Aqui o valor deveria ser 0, mas a função de limpar do protótipo não limpa o grid de pesquisa
            
            //Valida o Texto do botão antes de pressionar
            paginaCapacidadePerfil.ValidarTextoElemento("Ocultar Filtros", paginaCapacidadePerfil.botaoOcultarFiltros);
            //Clica em Ocultar FIltros
            paginaCapacidadePerfil.OcultarFiltros();
            //Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoBuscar);
            //Valida que o botão Buscar não está sendo exibido
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.botaoLimpar);
            //Valida que a combo não está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoNaoPresente(paginaCapacidadePerfil.comboPerfil);            
            //Valida se texto do botão Ocultar FIltros foi alterado
            paginaCapacidadePerfil.AguardarProcessando();
            paginaCapacidadePerfil.ValidarTextoElemento("Exibir Filtros", paginaCapacidadePerfil.botaoExibirFiltros);
            //Clica novamenten o botão para voltar a exibir os filtros
            paginaCapacidadePerfil.ExibirFiltros();
            //Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoBuscar);
            //Valida que o botão Buscar está sendo exibido
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.botaoLimpar);
            //Valida que a combo está mais sendo exibida
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPostoVistoria);
            paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboPerfil);
            //paginaCapacidadePerfil.ValidarElementoPresente(paginaCapacidadePerfil.comboCargaHoraria);
        }

        [TestMethod]
        public void ExcluirTodosCapacidadePerfil()
        {
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "Todos");
            //Valida a quantidade de resultados exibidos
            paginaCapacidadePerfil.ExcluirTodosItensGrid(true);
            paginaCapacidadePerfil.PesquisarCapacidadePerfil("MANAUS", "Todos");
            //Valida que o grid foi atualizado e não contém itens.
            paginaCapacidadePerfil.ValidarLinhasGrid(0);
        }

    }
}

