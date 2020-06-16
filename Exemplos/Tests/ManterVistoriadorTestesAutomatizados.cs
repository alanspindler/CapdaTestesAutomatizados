using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.Tests
{
    [TestClass]
    public class ManterVistoriadorTestesAutomatizados
    {
        public Global Global { get; set; }
        public PaginaBase PaginaBase { get; set; }
        public PaginaInicial PaginaInicial { get; set; }
        public PaginaManterVistoriador paginaManterVistoriador { get; set; }
        public string urlPaginaInicial = "http://localhost:4200/#/vistoriador";
        Global selenium;

        [TestInitialize]
        public void IniciarTeste()
        {
            //Inicializa instância do driver do Selenium
            selenium = Global.obterInstancia();
            paginaManterVistoriador = new PaginaManterVistoriador(selenium.driver);
            //Abre a pagina inicial
            PaginaBase = new PaginaBase(selenium.driver);
            PaginaInicial = new PaginaInicial(selenium.driver);
            PaginaInicial.AbrirPagina(urlPaginaInicial);
            //Faz login
            PaginaInicial.FazerLogin(Constantes.USUARIO_COORDENADOR, Constantes.SENHA_COORDENADOR);
            PaginaBase.AguardarProcessando();
        }

        [TestCleanup]
        public void FinalizarTeste()
        {
            //Fecha o navegador            
            selenium.EncerrarTeste();
        }


        [TestMethod]
        public void ExcluirTodosVistoriadores()
        {
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ExcluirTodosItensGrid(true);
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Valida que o grid foi atualizado e não contém itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
        }

        [TestMethod]
        public void PesquisarItem()
        {
            //Chama a função de efetuar a pesquisa por nome do texto que deve ser selecionado.            
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            //Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(0);
            // Valida se o valor escolhido está na combo            
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            //Clica no botão Limpar
            paginaManterVistoriador.Limpar();
            //Valida se o valores das combos volta, ao padrão após limpar
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção", paginaManterVistoriador.comboPostoVistoria);
            //Valida o Texto do botão antes de pressionar
            paginaManterVistoriador.ValidarTextoElemento("Ocultar Filtros", paginaManterVistoriador.botaoOcultarFiltros);
            //Clica em Ocultar FIltros
            paginaManterVistoriador.OcultarFiltros();
            //Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoBuscar);
            //Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoLimpar);
            //Valida que as combos não estão mais sendo exibidas
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.comboPostoVistoria);
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.campoLogin);
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.campoNomeVistoriador);
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.optionTodos);
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.optionAtivo);
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.optionInativo);
            PaginaBase.AguardarProcessando();
            //Valida se texto do botão Ocultar FIltros foi alterado
            paginaManterVistoriador.ValidarTextoElemento("Exibir Filtros", paginaManterVistoriador.botaoExibirFiltros);
            //Clica novamente no botão para voltar a exibir os filtros
            paginaManterVistoriador.ExibirFiltros();
            //Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoBuscar);
            //Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoLimpar);
            //Valida que as combos estão sendo exibidas
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.comboPostoVistoria);
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.campoLogin);
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.campoNomeVistoriador);
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.optionTodos);
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.optionAtivo);
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.optionInativo);
        }

        [TestMethod]
        public void IncluirNovoVistoriador()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            //Valida se o vistoriador está ativo.
            //paginaManterVistoriador.ValidarAtivoInativo(linha, true);
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaManterVistoriador.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção", paginaManterVistoriador.comboPostoVistoria);
            //Valida o Texto do botão antes de pressionar
            paginaManterVistoriador.ValidarTextoElemento("Ocultar Filtros", paginaManterVistoriador.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaManterVistoriador.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.comboPostoVistoria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            PaginaBase.AguardarProcessando();
            paginaManterVistoriador.ValidarTextoElemento("Exibir Filtros", paginaManterVistoriador.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaManterVistoriador.ExibirFiltros();
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoBuscar);
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.comboPostoVistoria);
        }

        [TestMethod]
        public void ValidarPesquisaLogin()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa sem utilizar pontuação no login
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "46591946047", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            paginaManterVistoriador.Limpar();
            //Faz a pesquisa com os formato CPF
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "465.919.460-47", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);            
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaManterVistoriador.Limpar();      
            //Valida se o valor da combo volta ao padrão após limpar
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção", paginaManterVistoriador.comboPostoVistoria);
            //Valida o Texto do botão antes de pressionar
            paginaManterVistoriador.ValidarTextoElemento("Ocultar Filtros", paginaManterVistoriador.botaoOcultarFiltros);
            ////Clica em Ocultar Filtros
            paginaManterVistoriador.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.comboPostoVistoria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            PaginaBase.AguardarProcessando();
            paginaManterVistoriador.ValidarTextoElemento("Exibir Filtros", paginaManterVistoriador.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaManterVistoriador.ExibirFiltros();
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoBuscar);
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.comboPostoVistoria);
        }


        [TestMethod]
        public void IncluirVistoriadorDuplicado()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            PaginaBase.AguardarProcessando();
            PaginaBase.AguardarElemento(paginaManterVistoriador.botaoSalvar);
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            PaginaBase.AguardarProcessando();
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            PaginaBase.AguardarProcessando();
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Fecha a mensagem de Registro já existente
            paginaManterVistoriador.ValidarTexto("Registro já existente", paginaManterVistoriador.mensagemRetorno);
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            PaginaBase.AguardarElemento(paginaManterVistoriador.botaoCancelar);
            PaginaBase.AguardarProcessando();
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoCancelar);
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaManterVistoriador.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção", paginaManterVistoriador.comboPostoVistoria);
            //Valida o Texto do botão antes de pressionar
            paginaManterVistoriador.ValidarTextoElemento("Ocultar Filtros", paginaManterVistoriador.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaManterVistoriador.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.comboPostoVistoria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaManterVistoriador.AguardarProcessando();
            paginaManterVistoriador.ValidarTextoElemento("Exibir Filtros", paginaManterVistoriador.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaManterVistoriador.ExibirFiltros();
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoBuscar);
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.comboPostoVistoria);
            //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            ////paginaManterVistoriador.ExportarPDF();           
        }

        [TestMethod]
        public void DesativarVistoriador()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            PaginaBase.AguardarProcessando();
            PaginaBase.AguardarElemento(paginaManterVistoriador.botaoSalvar);
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            //Valida se vistoriador está ativo.
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, true, true, "MANAUS", "COVIS", "6", "300");
            paginaManterVistoriador.AlterarItemLinhaSelecionada(1);
            paginaManterVistoriador.AlterarVistoriador(true, "6", "", "", "Alteração", false, true);
            PaginaBase.AguardarProcessando();
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            PaginaBase.AguardarProcessando();
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Faz a pesquisa por ativo
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //Faz a pesquisa por inativo
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 2);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, false, true, "MANAUS", "COVIS", "6", "0");
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //Clica no botão Limpar
            paginaManterVistoriador.Limpar();
            //Valida se o valor da combo volta ao padrão após limpar
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("Selecione uma Opção", paginaManterVistoriador.comboPostoVistoria);
            //Valida o Texto do botão antes de pressionar
            paginaManterVistoriador.ValidarTextoElemento("Ocultar Filtros", paginaManterVistoriador.botaoOcultarFiltros);
            ////Clica em Ocultar FIltros
            paginaManterVistoriador.OcultarFiltros();
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoBuscar);
            ////Valida que o botão Buscar não está sendo exibido
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo não está mais sendo exibida
            paginaManterVistoriador.ValidarElementoNaoPresente(paginaManterVistoriador.comboPostoVistoria);
            ////Valida se texto do botão Ocultar FIltros foi alterado
            paginaManterVistoriador.AguardarProcessando();
            paginaManterVistoriador.ValidarTextoElemento("Exibir Filtros", paginaManterVistoriador.botaoExibirFiltros);
            ////Clica novamente no botão para voltar a exibir os filtros
            paginaManterVistoriador.ExibirFiltros();
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoBuscar);
            ///Valida que o botão Buscar está sendo exibido
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.botaoLimpar);
            ////Valida que a combo está mais sendo exibida
            paginaManterVistoriador.ValidarElementoPresente(paginaManterVistoriador.comboPostoVistoria);
            //////Clicar em Exportar em PDF - Não faz nada nesse prototipo
            ////paginaManterVistoriador.ExportarPDF();            
        }

        [TestMethod]
        public void AtivarVistoriador()
        {
            //Clica no botão Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            PaginaBase.AguardarProcessando();
            PaginaBase.AguardarElemento(paginaManterVistoriador.botaoSalvar);
            //Seleciona os valores dos campos/combos, marca como ativo. Também confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", false, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de êxito.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa por todas as situações
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Faz a pesquisa por ativo
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //Faz a pesquisa por inativo
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 2);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            //Valida se vistoriador está ativo.
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, false, true, "MANAUS", "COVIS", "6", "0");
            paginaManterVistoriador.AlterarItemLinhaSelecionada(1);
            paginaManterVistoriador.AlterarVistoriador(true, "6", "", "", "Alteração", true, true);
            PaginaBase.AguardarProcessando();
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            PaginaBase.AguardarProcessando();
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Faz a pesquisa por ativo
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 1);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Faz a pesquisa por inativo
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 2);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(0);
            //// Valida se o valor escolhido está na combo
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Valida se está exibindo como ativo ou inativo
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, true, true, "MANAUS", "COVIS", "6", "300");
            //Exclui o item selecionado. O parametro true é para confirmar a exclusão.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que o grid foi atualizado e não contém itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
        }

        [TestMethod]
        public void ValidarHistoricoInclusao()
        {
            //Clica no botão Adicionar.
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            paginaManterVistoriador.AguardarProcessando();
            //Inclui os dados, clica em Salvar e em Confirmar.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de confirmação.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            paginaManterVistoriador.AguardarProcessando();
            //Faz a pesquisa
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Valida a quantidade de itens exibidos.
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Valida os dados do item exibido.
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            //Clica no botao do Historico do Item            
            paginaManterVistoriador.AbrirHistorico(1);
            paginaManterVistoriador.AguardarProcessando();
            //Valida os itens do histórico            
            paginaManterVistoriador.ValidarItensHistorico(1, "Inclusão", paginaManterVistoriador.GerarDataAtual(), Constantes.USUARIO_COORDENADOR, "MANAUS", true, false, false, "6", "300", true, true, "");
            //Fecha o histórico            
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharHistorico);
            //Exclui o item
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
        }


        [TestMethod]
        public void ValidarHistoricoAlteracao()
        {
            //Clica no botão Adicionar.
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            paginaManterVistoriador.AguardarProcessando();
            //Inclui os dados, clica em Salvar e em Confirmar.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", false, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de confirmação.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            paginaManterVistoriador.AguardarProcessando();
            //Faz a pesquisa
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Valida a quantidade de itens exibidos.
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Valida os dados do item exibido.
            paginaManterVistoriador.ValidarTextoElementoSelecionadoCombo("MANAUS", paginaManterVistoriador.comboPostoVistoria);
            paginaManterVistoriador.AlterarItemLinhaSelecionada(1);
            paginaManterVistoriador.AlterarVistoriador(true, "6", "", "", "Alteração", true, true);
            paginaManterVistoriador.AguardarProcessando();
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            paginaManterVistoriador.AguardarProcessando();
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Clica no botao do Historico do Item            
            paginaManterVistoriador.AbrirHistorico(1);
            paginaManterVistoriador.AguardarProcessando();
            //Valida os itens do histórico
            paginaManterVistoriador.ValidarItensHistorico(1, "Inclusão", paginaManterVistoriador.GerarDataAtual(), Constantes.USUARIO_COORDENADOR, "MANAUS", true, false, false, "6", "0", false, true, "");
            paginaManterVistoriador.ValidarItensHistorico(2, "Alteração", paginaManterVistoriador.GerarDataAtual(), Constantes.USUARIO_COORDENADOR, "MANAUS", true, false, false, "6", "300", true, true, "Alteração");
            //Fecha o histórico            
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharHistorico);
            //Exclui o item
            paginaManterVistoriador.ExcluirTodosItensGrid(true);
        }


        [TestMethod]
        public void IncluirDoisVistoriadoresExcluirUm()
        {
            //Clica em Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            paginaManterVistoriador.AguardarProcessando();
            //Preenche os dados, clica em Salvar e Confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", false, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha mensagem de sucesso.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            paginaManterVistoriador.AguardarProcessando();
            //Efetua a pesquisa
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Clica em Adicionar.
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            //Preenche os dados, clica em Salvar e Confirma.Campo Bom, RSMariano Moro, RS, 99790-000
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_2, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de sucesso.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(2);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, false, true, "MANAUS", "COVIS", "6", "0");
            paginaManterVistoriador.ValidarItensResultadoPesquisa(2, "749.359.890-84", "SUFRAMA", Constantes.CPF_COORDENADOR_2, true, false, false, true, true, "MANAUS", "COVIS", "6", "300");
            //Exclui o primeiro item
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que só existe um item após a exclusão.
            paginaManterVistoriador.ValidarLinhasGrid(1);
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "749.359.890-84", "SUFRAMA", Constantes.CPF_COORDENADOR_2, true, false, false, true, true, "MANAUS", "COVIS", "6", "300");
            //Exclui o item restante.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que não existem mais itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
        }

        [TestMethod]
        public void IncluirDoisVistoriadoresExcluirASegunda()
        {
            //Clica em Adicionar
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            paginaManterVistoriador.AguardarProcessando();
            //Preenche os dados, clica em Salvar e Confirma.
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_1, "Teste do Alan", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", false, false);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha mensagem de sucesso.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            paginaManterVistoriador.AguardarProcessando();
            //Efetua a pesquisa
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            //Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(1);
            //Clica em Adicionar.
            PaginaBase.ClicarElementoPagina(paginaManterVistoriador.botaoAdicionar);
            //Preenche os dados, clica em Salvar e Confirma.Campo Bom, RSMariano Moro, RS, 99790-000
            paginaManterVistoriador.IncluirVistoriador(true, Constantes.CPF_COORDENADOR_2, "Teste do Alan 2", "6", "", "", "123456789", "Unidade Xuller", "MANAUS", "Coordenador Xuller", true, true);
            //Valida mensagem de sucesso
            paginaManterVistoriador.ValidarTexto("Operação realizada com sucesso!", paginaManterVistoriador.mensagemRetorno);
            //Fecha a mensagem de sucesso.
            paginaManterVistoriador.ClicarElementoPagina(paginaManterVistoriador.botaoFecharMensagemConfirmacaoCadastro);
            //Faz a pesquisa.
            paginaManterVistoriador.PesquisarVistoriador("MANAUS", "", "", 0);
            ////Valida a quantidade de resultados exibidos
            paginaManterVistoriador.ValidarLinhasGrid(2);
            //Valida as duas linhas retornadas, para verificar se os dados estão corretos.
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, false, false, "MANAUS", "COVIS", "6", "0");
            paginaManterVistoriador.ValidarItensResultadoPesquisa(2, "749.359.890-84", "SUFRAMA", Constantes.CPF_COORDENADOR_2, true, false, false, true, true, "MANAUS", "COVIS", "6", "300");
            //Exclui o primeiro item
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 2);
            //Valida que só existe um item após a exclusão.
            paginaManterVistoriador.ValidarLinhasGrid(1);
            paginaManterVistoriador.ValidarItensResultadoPesquisa(1, "465.919.460-47", "SUFRAMA", Constantes.CPF_COORDENADOR_1, true, false, false, false, false, "MANAUS", "COVIS", "6", "0");
            //Exclui o item restante.
            paginaManterVistoriador.ExcluirItemLinhaSelecionada(true, 1);
            //Valida que não existem mais itens.
            paginaManterVistoriador.ValidarLinhasGrid(0);
        }
    }
}

