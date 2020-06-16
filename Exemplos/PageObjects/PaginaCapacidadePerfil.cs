using OpenQA.Selenium;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects
{
    [TestClass]
    public class PaginaCapacidadePerfil : PaginaBase
    {

        #region Declaração de variáveis públicas da classe

        public By comboPostoVistoria = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[2]/div/section/article/form/fieldset/div/div/app-drop-list/select");
        public By comboPerfil = By.Id("perfil");
        public By comboCargaHoraria = By.Id("cargaHoraria");
        public By botaoAdicionar = By.ClassName("fa-plus");
        public By botaoAlterar = By.ClassName("fa-pencil");
        public By botaoSalvar = By.ClassName("fa-save");
        public By botaoCancelar = By.ClassName("fa-long-arrow-left");
        public By botaoBuscar = By.ClassName("fa-search");
        public By botaoLimpar = By.ClassName("fa-eraser");
        public By botaoOcultarFiltros = By.XPath("(//button[@type='button'])[2]");
        public By botaoExibirFiltros = By.XPath("//button[@type='button']");
        public By botaoConfirmar = By.ClassName("fa-check");
        public By botaoNaoConfirmar = By.ClassName("fa-times");
        public By botaoFechar = By.ClassName("fa-times");
        public By botaoFecharMensagemConfirmacaoCadastro = By.ClassName("fa-times");        
        public By botaoFecharMensagemConfirmacao = By.XPath("//app-modal-resposta/div/div/div[3]/button");        
        public By botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado = By.XPath("//app-modal/div/div/div[3]/button");
        public By comboPostoVistoriaCadastro = By.XPath("//form[@id='formulario']/div/div/app-drop-list/select");
        public By comboTipoVistoriadorCadastro = By.Name("tipoVistoriador");
        public By campoCargaHorariaCadastro = By.Id("cargaHoraria");
        public By campoQtdadeNF = By.Id("capacidade");
        public By campoObservacao = By.Id("observacao");
        public By mensagemRetorno = By.XPath("//p");
        public By ordernarPostoVistoria = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/thead/tr/th[1]/app-ordenacao/div");
        public By ordernarPerfil = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/thead/tr/th[2]/app-ordenacao/div");
        public By ordernarCargaHoraria = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/thead/tr/th[3]/app-ordenacao/div");
        public By ordernarQtdadeNF = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/thead/tr/th[4]/app-ordenacao/div");
        public By tabelaResultadoPesquisa = By.ClassName("table-striped");
    

        #endregion

        #region Métodos públicos

        public PaginaCapacidadePerfil(RemoteWebDriver driver) : base(driver)
        { }

        //Retorna o XPath do botão excluir da linha informada        
        public By BotaoExcluirLinhaSelecionada(int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[5]/a[2]");
        }

        public By BotaoExcluirLinhaSelecionada(string texto)
        {
            int linha = ObterLinhaTabela(tabelaResultadoPesquisa, texto, 4);
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[5]/a[2]");
        }

        //Retorna o XPath do botão historico da linha informada
        public By BotaoAbrirHistoricoLinhaSelecionada(int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[5]/a[3]");            
        }
        //Retorna o XPath do botão Alterar da linha informada
        public By BotaoAlterarLinhaSelecionada(int linha)
        {
            //return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/section/app-manter-capacidade-perfil-grid/app-grid/div/table/tbody/tr[{linha}]/td[5]/a[1]");
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[5]/a[1]");
        }

        //Faa a pesquisa da capacidade do perfil
        public void PesquisarCapacidadePerfil(string PostoVistoria, string Perfil)
        {
            AguardarProcessando();
            AguardarElemento(botaoBuscar);
            AguardarTexto(comboPostoVistoria, PostoVistoria);
            SelecionarItemCombo(comboPostoVistoria, PostoVistoria);
            SelecionarItemCombo(comboPerfil, Perfil);
            ClicarElementoPagina(botaoBuscar);
            AguardarProcessando();
            if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
            {
                ClicarElementoPagina(botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                AguardarProcessando();
            }
            if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
            {
                ClicarElementoPagina(botaoFecharMensagemConfirmacao);
            }
            AguardarProcessando();
        }

        //Preenche os campos da capacidade do perfil, e clica em salvar. Confirma ou cancela de acordo com o parametro confirmar.
        public void IncluirCapacidadePesquisa(bool confirmar, string PostoVistoria, string Perfil, string CargaHoraria, string quantidade, string msgErro = "")
        {
            AguardarElemento(botaoSalvar);
            AguardarProcessando();
            SelecionarItemCombo(comboPostoVistoriaCadastro, PostoVistoria);
            SelecionarItemCombo(comboTipoVistoriadorCadastro, Perfil);
            PreencherCampo(campoCargaHorariaCadastro, CargaHoraria);
            PreencherCampo(campoQtdadeNF, quantidade);
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            if (IsElementDisplayed(driver, mensagemRetorno) && msgErro != "")
            {
                ValidarTexto(msgErro, mensagemRetorno);
            }
            else
            {
                if (confirmar)
                {
                    //AguardarProcessando();
                    ClicarElementoPagina(botaoConfirmar);
                }
                else
                {
                    //AguardarProcessando();
                    ClicarElementoPagina(botaoNaoConfirmar);
                }
            }
            AguardarProcessando();
        }

        //Altera a quantidade de NF e preenche a justificativa, e clica em salvar. Confirma ou cancela de acordo com o parametro confirmar.
        public void AlterarCapacidadePesquisa(bool confirmar, string quantidade, string justificativa)
        {
            AguardarElemento(botaoSalvar);
            AguardarProcessando();
            PreencherCampo(campoQtdadeNF, quantidade);
            PreencherCampo(campoObservacao, justificativa);
            ClicarElementoPagina(botaoSalvar);
            if (confirmar)
            {
                AguardarProcessando();
                ClicarElementoPagina(botaoConfirmar);
            }
            else
            {
                AguardarProcessando();
                ClicarElementoPagina(botaoNaoConfirmar);
            }
            AguardarProcessando();
        }
        //Clica no botão Limpar da tela de pesquisa
        public void Limpar()
        {
            ClicarElementoPagina(botaoLimpar);
            Thread.Sleep(1500);
        }

        //Clica no botão OcularFiltros
        public void OcultarFiltros()
        {
            ClicarElementoPagina(botaoOcultarFiltros);
            Thread.Sleep(1500);
        }

        //Caso abra tela de histórico ou outra com grid, as linhas do grid passam a ser contadas, então é necessário utilizar esse modificador
        public new void ExcluirTodosItensGrid(bool confirmar)
        {
            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 1;
            while (quantidadeLinhasGridRetornada > 0)
            {
                if (confirmar)
                {
                    ExcluirItemLinhaSelecionada(true, 1);
                }
                else
                {
                    ExcluirItemLinhaSelecionada(false, 1);
                    break;
                }
                AguardarProcessando();
                quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 1;
            }
            AguardarProcessando();
        }

        //Retorna o Xpath do item específico (exemplo Posto de Vistoria) do resultado da pesquisa, usado para fazer assert ce textos do resultados, etc.
        public By RetornaItemResultadoPesquisa(int linha, int item)
        {
            //return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/section/app-manter-capacidade-perfil-grid/app-grid/div/table/tbody/tr[{linha}]/td[{item}]");
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[{item}]");
        }

        //Retorna o Xpath do item específico (exemplo Posto de Vistoria) do histórico de inclusão/alteração, usado para fazer assert ce textos do resultados, etc.
        public By RetornaItemHistorico(int linha, int item)
        {
            //return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/section/app-manter-capacidade-perfil-grid/app-modal-historico-capacidade-perfil/div/div/div/div[2]/form/section/article/div/div/app-manter-historico-capacidade-perfil-grid/app-grid/div/table/tbody/tr[{linha}]/td[{item}]");
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/app-manter-capacidade-perfil-grid/app-modal-historico-capacidade-perfil/div/div/div/div[2]/form/div/div/div/app-manter-historico-capacidade-perfil-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[{item}]");
        }

        ///// <summary>
        ///// Valida a quantidade de linhas exibidas no grid
        ///// </summary>
        ///// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        //public new void ValidarLinhasGrid(int valorEsperado)
        //{            
        //    var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 2;
        //    Assert.AreEqual(valorEsperado, quantidadeLinhasGridRetornada, "Valor inválido! Números e linhas esperadas: " + valorEsperado + " linhas retornadas: " + quantidadeLinhasGridRetornada);
        //}

        //Exclui o item da linha selecionado. Confirma a exclusão de acotdo com o parametro confirmar
        public void ExcluirItemLinhaSelecionada(bool confirmar, int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoExcluirLinhaSelecionada(linha));

            if (confirmar)
            {
                ClicarElementoPagina(botaoConfirmar);
            }
            else
            {
                ClicarElementoPagina(botaoNaoConfirmar);
            }

            AguardarProcessando();
            AguardarProcessando();
            Thread.Sleep(1000);
            if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao) || IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
            {
                AguardarProcessando();
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                    AguardarProcessando();
                }
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacao);
                }
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacao);
                }
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                    AguardarProcessando();
                }
            }
            AguardarProcessando();
            if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
            {
                AguardarProcessando();
                ClicarElementoPagina(botaoFecharMensagemConfirmacao);
            }
            AguardarProcessando();
        }

        public void ExcluirItemTextoSelecionado(bool confirmar, string texto)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoExcluirLinhaSelecionada(texto));

            if (confirmar)
            {
                ClicarElementoPagina(botaoConfirmar);
            }
            else
            {
                ClicarElementoPagina(botaoNaoConfirmar);
            }

            AguardarProcessando();
            AguardarProcessando();
            Thread.Sleep(1000);
            if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao) || IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
            {
                AguardarProcessando();
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                    AguardarProcessando();
                }
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacao);
                }
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacao);
                }
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                    AguardarProcessando();
                }
            }
            AguardarProcessando();
            if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacao))
            {
                AguardarProcessando();
                ClicarElementoPagina(botaoFecharMensagemConfirmacao);
            }
            AguardarProcessando();
        }


        //Exclui todos os itens até que não restem itens no grid. Utilizado para limpar antes de testes, por exemplo.
        public void ExcluirTodosItens()
        {
            AguardarProcessando();

            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 2;
            while (quantidadeLinhasGridRetornada > 0)
            {
                ExcluirItemLinhaSelecionada(true, 1);
                AguardarProcessando();
                quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 2;
            }
        }

        //Clica no botão Exibir Filtros
        public void ExibirFiltros()
        {
            ClicarElementoPagina(botaoExibirFiltros);
            AguardarProcessando();
        }

        //public void ExportarPDF()
        //{
        //    ClicarElementoPagina(botaoExportar);
        //    Thread.Sleep(700);
        //    ClicarElementoPagina(botaoSalvarPDF);
        //}

        //Na tela de cadastro/alteração, clica em Cancelar.
        public void CancelarCadastro()
        {
            AguardarProcessando();
            ClicarElementoPagina(botaoCancelar);
            AguardarProcessando();
        }

        //Albre o histórico do item da linha informada.
        public void AbrirHistorico(int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoAbrirHistoricoLinhaSelecionada(linha));
            AguardarProcessando();
        }

        //Abre a tela de alteração do item da linha informada.
        public void AbrirAlterar(int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoAlterarLinhaSelecionada(linha));
            AguardarProcessando();
        }

        //Valida o texto do item do resultado da pesquisa da linha informada. 
        public void ValidarItensResultadoPesquisa(int linha, string postoVistoria, string perfil, string cargaHoraria, string QtdadeNF)
        {
            AguardarProcessando();
            ValidarTexto(postoVistoria, RetornaItemResultadoPesquisa(linha, 1));
            ValidarTexto(perfil, RetornaItemResultadoPesquisa(linha, 2));
            ValidarTexto(cargaHoraria, RetornaItemResultadoPesquisa(linha, 3));
            ValidarTexto(QtdadeNF, RetornaItemResultadoPesquisa(linha, 4));
        }

        //Valida o histórico aberto, na linha informada.
        public void ValidarItensHistorico(int linha, string PostoVistoria, string Perfil, string CargaHoraria, string QtdadeNF, string Operacao, string Justificativa)
        {
            AguardarProcessando();
            ValidarTexto(PostoVistoria, RetornaItemHistorico(linha, 1));
            ValidarTexto(Perfil, RetornaItemHistorico(linha, 2));
            ValidarTexto(CargaHoraria, RetornaItemHistorico(linha, 3));
            ValidarTexto(QtdadeNF, RetornaItemHistorico(linha, 4));
            ValidarTexto(Operacao, RetornaItemHistorico(linha, 5));
            ValidarTexto(Justificativa, RetornaItemHistorico(linha, 8));
        }

        #endregion
    }
}
