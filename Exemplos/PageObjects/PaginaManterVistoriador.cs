using OpenQA.Selenium;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;
using System.Threading;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects
{
    [TestClass]
    public class PaginaManterVistoriador : PaginaBase
    {

        #region Declaração de variáveis públicas da classe

        public By comboPostoVistoria = By.XPath("//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[2]/div/section/article/form/div/div/app-drop-list/select");
        public By campoLogin = By.Id("txtloginUsuario");
        public By campoNomeVistoriador = By.Id("txtNomeVistoriador");
        public By campoJustificativa = By.Id("justificativa");
        public By campoCapacidade = By.Id("txtCapacidade");
        public By campoCargaHorariaVistoriaPosto = By.Id("txtCargaHorariaInterno");
        public By campoCargaHorariaVistoriaExterna = By.Name("cargaHorariaExterna");
        public By campoCargaHorariaVistoriaDocumental = By.Name("cargaHorariaDocumental");
        public By campoCapacidadeVistoriaPosto = By.XPath("//input[@id='txtUnidadeCadastrada'])[4]");
        public By campoCapacidadeVistoriaExterna = By.XPath("//input[@id='txtUnidadeCadastrada'])[5]");
        public By campoCapacidadeVistoriaDocumental = By.XPath("//input[@id='txtUnidadeCadastrada'])[6]");
        public By optionTodos = By.XPath("//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[2]/div/section/article/form/div[3]/div/div/label/i");
        public By optionAtivo = By.XPath("//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[2]/div/section/article/form/div[3]/div/div/label[2]");
        public By optionInativo = By.XPath("//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[2]/div/section/article/form/div[3]/div/div/label[3]");
        public By botaoBuscar = By.ClassName("fa-search");
        public By botaoLimpar = By.ClassName("fa-eraser");
        public By botaoOcultarFiltros = By.XPath("(//button[@type='button'])[2]");
        public By botaoExibirFiltros = By.XPath("//button[@type='button']");
        public By botaoConfirmar = By.ClassName("fa-check");
        //public By botaoConfirmarExclusao = By.XPath("(//button[@type='button'])[8]");
        public By botaoConfirmarExclusao = By.XPath("(//button[contains(.,' Sim')])");
        //xpath=//button[contains(.,' Sim')]
        //public By botaoNaoConfirmar = By.ClassName("fa-times");
        public By botaoNaoConfirmar = By.XPath("(//button[contains(.,' Não')])");
        public By botaoFechar = By.ClassName("fa-times");
        public By botaoFecharMensagemConfirmacaoCadastro = By.ClassName("fa-times");
        //public By botaoFecharMensagemConfirmacao = By.XPath("(//button[@type='button'])[8]");
        public By botaoFecharMensagemConfirmacao = By.CssSelector("app-modal-resposta .btn");
        //public By botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado = By.XPath("(//button[@type='button'])[9]");
        public By botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado = By.CssSelector("app-modal .btn");                
        public By botaoFecharHistorico = By.XPath("(//button[@type='button'])[8]");
        public By botaoAdicionar = By.ClassName("fa-plus");
        public By botaoAlterar = By.ClassName("fa-pencil");
        public By botaoSalvar = By.ClassName("fa-save");
        public By botaoCancelar = By.ClassName("fa-long-arrow-left");
        public By telaIncluirCampoLogin = By.Id("txtLogin");
        public By telaIncluirBotaoPesquisar = By.ClassName("fa-search");
        public By telaIncluirCampoNomeVistoriador = By.Id("txtNomeVistoriador");
        public By telaIncluirComboPerfil = By.Id("selcPerfil");
        public By telaIncluirCampoCpf = By.Id("txtNumeroCPF");
        public By telaIncluirCampoUnidadeCadastradora = By.Id("txtUnidadeCadastrada");
        public By telaIncluirComboPostoVistoria = By.XPath("//form[@id='formulario']/fieldset/div[3]/div[3]/app-drop-list/select");
        public By telaIncluirCampoUnidadeCoordenacao = By.Id("txtDescricaoCordenacao");
        public By telaIncluircomboCargaHoraria = By.Name("parametros.cargaHoraria");
        public By telaIncluirCampoCapacidadeVistoria = By.Id("txtCapacidade");
        public By telaIncluirCheckboxAtivoValidar = By.Name("situacaoVistoriador");
        public By telaIncluirCheckboxAtivoClicar = By.XPath("//form[@id='formulario']/fieldset/div[5]/div[2]/div[5]/div/label/span");
        //form[@id='formulario']/fieldset/div[5]/div[2]/div[5]/div/label/span
        public By telaIncluirCheckboxMasterValidar = By.Name("situacaoPerfilMaster");
        public By telaIncluirCheckboxMasterClicar = By.XPath("//form[@id='formulario']/fieldset/div[5]/div[5]/div[5]/div/label/span");
        //form[@id='formulario']/fieldset/div[5]/div[5]/div[5]/div/label/span
        public By mensagemRetorno = By.XPath("//p");
        public By statusVistoriadorAtivo = By.ClassName("fa-check");
        public By statusVistoriadorInativo = By.ClassName("fa-times");


        public PaginaInicial PaginaInicial { get; set; }

        #endregion

        #region Métodos públicos

        public PaginaManterVistoriador(RemoteWebDriver driver) : base(driver)
        { }

        //Retorna o XPath do botão excluir da linha informada        
        public By BotaoExcluirLinhaSelecionada(int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[11]/a[4]");
        }

        //Retorna o XPath do botão historico da linha informada
        public By BotaoAbrirHistoricoLinhaSelecionada(int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[11]/a[3]");
        }

        //Retorna o XPath do botão Alterar da linha informada
        public By BotaoAlterarLinhaSelecionada(int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[11]/a[1]");
        }

        public By StatusMasterLinhaSelecionada (int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[7]/span");
        }

        //Retorna o XPath do do Status do Vistoriador da linha informada.
        public By StatusMasterHistorico(int linha)
        {
            return By.XPath($"//div[@id='painelHistorico']/app-historico-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[6]/span");                              
        }

        //Retorna o XPath do do Status do Vistoriador da linha informada.
        public By StatusVistoriador(int linha)
        {
            //return By.XPath($"//div[@id='painelHistorico']/app-historico-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[9]/span");
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[10]/span");
        }

        //Retorna o XPath do do Status do Vistoriador da linha informada.
        public By StatusVistoriadorHistorico(int linha)
        {
            return By.XPath($"//div[@id='painelHistorico']/app-historico-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[9]/span");
        }

        public By RetornaIconePerfil(int linha, int perfil)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[6]/span[{perfil}]");
        }

        public By RetornaIconeHistorico(int linha, int perfil)
        {
            return By.XPath($"//div[@id='painelHistorico']/app-historico-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[5]/span[{perfil}]");
            //div[@id='painelHistorico']/app-historico-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[5]/span[{perfil}]");
        }

        //Faz a pesquisa da capacidade do perfil
        public void PesquisarVistoriador(string PostoVistoria, string Login, string NomeVIstoriador, int StatusVistoriador)
        {
            AguardarProcessando();
            AguardarElemento(botaoBuscar);
            SelecionarItemCombo(comboPostoVistoria, PostoVistoria);
            PreencherCampo(campoLogin, Login);
            PreencherCampo(campoNomeVistoriador, NomeVIstoriador);
            if (StatusVistoriador == 0)
            {
                ClicarElementoPagina(optionTodos);
            }
            if (StatusVistoriador == 1)
            {
                ClicarElementoPagina(optionAtivo);
            }
            if (StatusVistoriador == 2)
            {
                ClicarElementoPagina(optionInativo);
            }
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
        public void IncluirVistoriador(bool confirmar, string Login, string Nome, string CargaHorariaVistoriaPosto, string CargaHorariaVistoriaExterna, string CargaHorariaVistoriaDocumental, string CPF, string UnidadeCadastradora, string PostoVistoria, string Coordenacao, bool ativo, bool master)
        {
            AguardarElemento(botaoSalvar);
            AguardarProcessando();
            PreencherCampo(telaIncluirCampoLogin, Login);
            ClicarElementoPagina(telaIncluirBotaoPesquisar);
            AguardarProcessando();
            //DestacarElemento(driver, campoCargaHorariaVistoriaPosto);            
            PreencherCampo(campoCargaHorariaVistoriaPosto, CargaHorariaVistoriaPosto);
            AguardarProcessando();
            PreencherCampo(campoCargaHorariaVistoriaExterna, CargaHorariaVistoriaExterna);
            AguardarProcessando();
            PreencherCampo(campoCargaHorariaVistoriaDocumental, CargaHorariaVistoriaDocumental);
            AguardarProcessando();
            MarcarCheckbox(telaIncluirCheckboxAtivoValidar, telaIncluirCheckboxAtivoClicar, ativo);
            AguardarProcessando();
            MarcarCheckbox(telaIncluirCheckboxMasterValidar, telaIncluirCheckboxMasterClicar, master);
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

        public void ValidarAtivoInativo( int linha, bool ativo)
        {
            if (ativo == true)
            {
                Assert.AreEqual(true, ElementoAtivoInativo(driver, StatusVistoriador(linha)));
            }
            else
            {
                Assert.AreEqual(false, ElementoAtivoInativo(driver, StatusVistoriador(linha)));
            }
        }

        public void ValidarMasterHistorico(int linha, bool ativo)
        {
            DestacarElemento(driver, StatusMasterHistorico(linha));
            if (ativo == true)
            {
                Assert.AreEqual(true, ElementoMaster(driver, StatusMasterHistorico(linha)));
            }
            else
            {
                Assert.AreEqual(false, ElementoMaster(driver, StatusMasterHistorico(linha)));
            }
        }

        public void ValidarAtivoInativoHistorico(int linha, bool ativo)
        {
            DestacarElemento(driver, StatusVistoriadorHistorico(linha));
            if (ativo == true)
            {
                Assert.AreEqual(true, ElementoAtivoInativo(driver, StatusVistoriadorHistorico(linha)));
            }
            else
            {
                Assert.AreEqual(false, ElementoAtivoInativo(driver, StatusVistoriadorHistorico(linha)));
            }
        }

        public void ValidarMasterPesquisa(int linha, bool master)
        {
            DestacarElemento(driver, StatusMasterLinhaSelecionada(linha));
            if (master == true)
            {
                Assert.AreEqual(true, ElementoMaster(driver, StatusMasterLinhaSelecionada(linha)));
            }
            else
            {
                Assert.AreEqual(false, ElementoMaster(driver, StatusMasterLinhaSelecionada(linha)));
            }
        }

        public static bool ElementoAtivoInativo(IWebDriver driver, By element)
        {
            return ElementHasClass(driver, element, "btn-primary");
        }

        public bool ElementoMaster(IWebDriver driver, By element)
        {
            if (ElementHasClass(driver, element, "fa-star-o"))
            {
                return false;
            }
            if (ElementHasClass(driver, element, "fa-star"))
            {
                return true;
            }
           
            return false;
        }



        //Preenche os campos da capacidade do perfil, e clica em salvar. Confirma ou cancela de acordo com o parametro confirmar.
        public void AlterarVistoriador(bool confirmar, string CargaHorariaVistoriaPosto, string CargaHorariaVistoriaExterna, string CargaHorariaVistoriaDocumental, string justificativa, bool ativo, bool master)
        {
            AguardarElemento(campoCargaHorariaVistoriaPosto);
            AguardarProcessando();
            PreencherCampo(campoCargaHorariaVistoriaPosto, CargaHorariaVistoriaPosto);
            AguardarProcessando();
            PreencherCampo(campoCargaHorariaVistoriaExterna, CargaHorariaVistoriaExterna);
            AguardarProcessando();
            PreencherCampo(campoCargaHorariaVistoriaDocumental, CargaHorariaVistoriaDocumental);
            AguardarProcessando();
            MarcarCheckbox(telaIncluirCheckboxAtivoValidar, telaIncluirCheckboxAtivoClicar, ativo);
            AguardarProcessando();
            MarcarCheckbox(telaIncluirCheckboxAtivoValidar, telaIncluirCheckboxAtivoClicar, ativo);
            AguardarProcessando();
            MarcarCheckbox(telaIncluirCheckboxMasterValidar, telaIncluirCheckboxMasterClicar, master);
            AguardarProcessando();
            PreencherCampo(campoJustificativa, justificativa);
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

        //Exclui o item da linha selecionado. Confirma a exclusão de acotdo com o parametro confirmar
        public void AlterarItemLinhaSelecionada(int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoAlterarLinhaSelecionada(linha));
            AguardarProcessando();
        }

        public string RetornaCorPerfil(int linha, string perfil)
        {
            By elementoPerfil = By.XPath("");
            string cor;
            if (perfil == "Interno")
            {
                elementoPerfil = RetornaIconePerfil(linha, 1);
            }
            if ((perfil == "Externo") || (perfil == "Externa"))
            {
                elementoPerfil = RetornaIconePerfil(linha, 2);
            }
            if (perfil == "Documental")
            {
                elementoPerfil = RetornaIconePerfil(linha, 3);
            }
            cor = driver.FindElement(elementoPerfil).GetAttribute("style");
            return cor;
        }

        public string RetornaCorPerfilHistorico(int linha, string perfil)
        {
            By elementoPerfil = By.XPath("");
            string cor;
            if (perfil == "Interno")
            {
                elementoPerfil = RetornaIconeHistorico(linha, 1);
            }
            if ((perfil == "Externo") || (perfil == "Externa"))
            {
                elementoPerfil = RetornaIconeHistorico(linha, 2);
            }
            if (perfil == "Documental")
            {
                elementoPerfil = RetornaIconeHistorico(linha, 3);
            }
            cor = driver.FindElement(elementoPerfil).GetAttribute("style");
            return cor;
        }

        //Exclui o item da linha selecionado. Confirma a exclusão de acotdo com o parametro confirmar
        public void ExcluirItemLinhaSelecionada(bool confirmar, int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoExcluirLinhaSelecionada(linha));
            AguardarProcessando();
            if (confirmar)
            {
                ClicarElementoPagina(botaoConfirmarExclusao);
            }
            else
            {
                ClicarElementoPagina(botaoNaoConfirmar);
            }
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
                AguardarProcessando();
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

            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 1;
            if (quantidadeLinhasGridRetornada == -1)
            {
                quantidadeLinhasGridRetornada = 0;
            }
            if (quantidadeLinhasGridRetornada == 0)
            {
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
                {
                    ClicarElementoPagina(botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                    AguardarProcessando();
                }
                AguardarProcessando();
            }
        }

        public void AbrirHistorico(int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoAbrirHistoricoLinhaSelecionada(linha));
            AguardarProcessando();
        }

        //Valida o texto do item do resultado da pesquisa da linha informada. 
        public void ValidarItensResultadoPesquisa(int linha, string login, string nomeVistoriador, string cpf, bool vistoriaInterna, bool vistoriaExterna, bool vistoriaDocumental, bool ativo, bool master, string postoVistoria, string coordenacao, string cargaHoraria, string capacidade)
        {
            AguardarProcessando();
            ValidarTexto(login, RetornaItemResultadoPesquisa(linha, 1));
            ValidarTexto(nomeVistoriador, RetornaItemResultadoPesquisa(linha, 2));
            ValidarTexto(cpf, RetornaItemResultadoPesquisa(linha, 3));
            ValidarTexto(postoVistoria, RetornaItemResultadoPesquisa(linha, 4));
            ValidarTexto(coordenacao, RetornaItemResultadoPesquisa(linha, 5));
            if (vistoriaInterna)
            {
                Assert.AreEqual("color: blue;", RetornaCorPerfil(linha, "Interno"));
            }
            else
            {
                Assert.AreEqual("color: gray;", RetornaCorPerfil(linha, "Interno"));
            }
            if (vistoriaExterna)
            {
                Assert.AreEqual("color: orange;", RetornaCorPerfil(linha, "Externa"));
            }
            else
            {
                Assert.AreEqual("color: gray;", RetornaCorPerfil(linha, "Externa"));
            }
            if (vistoriaDocumental)
            {
                Assert.AreEqual("color: green;", RetornaCorPerfil(linha, "Documental"));
            }
            else
            {
                Assert.AreEqual("color: gray;", RetornaCorPerfil(linha, "Documental"));
            }
            ValidarMasterPesquisa(linha, master);
            ValidarAtivoInativo(linha, ativo);
            ValidarTexto(cargaHoraria, RetornaItemResultadoPesquisa(linha, 8));
            ValidarTexto(capacidade, RetornaItemResultadoPesquisa(linha, 9));

        }

        //Valida o texto do item do resultado da pesquisa da linha informada. 
        public void ValidarItensHistorico(int linha, string operacao, string dataoperacao, string usuario, string postoVistoria, bool vistoriaInterna, bool vistoriaExterna, bool vistoriaDocumental, string cargaHoraria, string qtdadeNF, bool ativo, bool master, string justificativa)
        {
            AguardarProcessando();
            ValidarTexto(operacao, RetornaItemHistorico(linha, 1));
            ValidarTexto(dataoperacao, RetornaItemHistorico(linha, 2));
            ValidarTexto(usuario, RetornaItemHistorico(linha, 3));
            ValidarTexto(postoVistoria, RetornaItemHistorico(linha, 4));
            if (vistoriaInterna)
            {
                Assert.AreEqual("color: blue;", RetornaCorPerfilHistorico(linha, "Interno"));
            }
            else
            {
                Assert.AreEqual("color: gray;", RetornaCorPerfilHistorico(linha, "Interno"));
            }
            if (vistoriaExterna)
            {
                Assert.AreEqual("color: orange;", RetornaCorPerfilHistorico(linha, "Externa"));
            }
            else
            {
                Assert.AreEqual("color: gray;", RetornaCorPerfilHistorico(linha, "Externa"));
            }
            if (vistoriaDocumental)
            {
                Assert.AreEqual("color: green;", RetornaCorPerfilHistorico(linha, "Documental"));
            }
            else
            {
                Assert.AreEqual("color: gray;", RetornaCorPerfilHistorico(linha, "Documental"));
            }
            ValidarTexto(cargaHoraria, RetornaItemHistorico(linha, 7));
            ValidarTexto(qtdadeNF, RetornaItemHistorico(linha, 8));
            ValidarAtivoInativoHistorico(linha, ativo);
            ValidarMasterHistorico(linha, master);
            ValidarTexto(justificativa, RetornaItemHistorico(linha, 10));
        }

        //Retorna o Xpath do item específico (exemplo Posto de Vistoria) do resultado da pesquisa, usado para fazer assert ce textos do resultados, etc.
        public By RetornaItemResultadoPesquisa(int linha, int item)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-vistoriador/div/div[3]/div/section/app-manter-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[{item}]");
        }

        //Retorna o Xpath do item específico (exemplo Posto de Vistoria) do histórico de inclusão/alteração, usado para fazer assert ce textos do resultados, etc.
        public By RetornaItemHistorico(int linha, int item)
        {
            return By.XPath($"//div[@id='painelHistorico']/app-historico-vistoriador-grid/app-grid/section/div/table/tbody/tr[{linha}]/td[{item}]");
        }

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
        }

        //Clica no botão Exibir Filtros
        public void ExibirFiltros()
        {
            ClicarElementoPagina(botaoExibirFiltros);
            AguardarProcessando();
        }

        #endregion
    }
}

