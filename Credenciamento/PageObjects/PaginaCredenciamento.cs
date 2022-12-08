using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System;


namespace Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects
{
    [TestClass]
    public class PaginaCredenciamento : PaginaBase
    {
        public GeradorNome geradorNome { get; set; }
        public GeradorCNPJCPF geradorCNPJCPF { get; set; }
        public PaginaCredenciamento()
        {
            geradorNome = new GeradorNome();
            geradorCNPJCPF = new GeradorCNPJCPF();
        }

        #region Pagina Principal

        public By botaoSolicitarCredenciamento = By.ClassName("fa-plus");
        public By botaoBuscar = By.Id("btnBuscar");
        public By botaoSubemeter = By.ClassName("fa-paper-plane-o");

        public void SolicitarCredenciamento()
        {
            ClicarElementoPagina(driver, botaoSolicitarCredenciamento);
        }

        public void SubmeterCredenciamento(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoSubemeter);
            AguardarProcessando(driver);
            AguardarProcessando(driver);
            AguardarProcessando(driver);
        }

        public string RetornarCodigoCredenciamento()
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoBuscar);
            AguardarProcessando(driver);
            string valorCodigoCredenciamento = RetornaTextoElemento(driver, codigoCredenciamento);
            return valorCodigoCredenciamento;
        }

        #endregion

        #region Geral

        //public By botaoSalvar = By.ClassName("fa-save");
        public By botaoSalvar = By.XPath("//button[contains(.,'Salvar')]");
        public By botaoNovo = By.ClassName("fa-plus-square");
        //public By botaoFechar = By.ClassName("fa-times");
        public By botaoFechar = By.XPath("/html/body/app-root/dialog-holder/dialog-wrapper/div/app-modal/div[1]/div/div[3]/button");
        //public By botaoFecharMensagemConfirmacao = By.XPath("(//button[@type='button'])[5]");
        public By botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado = By.CssSelector("body>app-root:nth-child(1)>dialog-holder:nth-child(4)>dialog-wrapper:nth-child(2)>div:nth-child(1)>app-modal:nth-child(1)>div:nth-child(1)>div:nth-child(1)>div:nth-child(3)>button:nth-child(1)");
        public By mensagemRetorno = By.CssSelector("p");
        private By botaoVoltar = By.ClassName("fa-long-arrow-left");
        private By codigoCredenciamento = By.XPath("//section[@id='timeLine']/app-acompanhar-credenciamento-grid/app-grid/section/div[2]/table/tbody/tr/td[2]");

        //public By botaoFechar = By.ClassName("btn-default");

        //div[3]/button
        #endregion

        #region Identificacao

        //Instituicao
        public By optionPrivado = By.XPath("//label[2]/i");
        //public By optionEnsino = By.XPath("//label[3]/i");
        public By optionPesquisa = By.XPath("//div[2]/div/div/label/i");

        //Unidade Academica
        public By abaUnidadeAcademica = By.LinkText("1.2. Unidade Acadêmica");
        public By campoUnidadeAcademica = By.Id("txtUndAcademica");
        public By comboTipoUnidadeAcademica = By.Id("drop-list");
        public By campoEmail = By.Id("email");
        public By campoSite = By.Id("site");
        public By campoTelefone = By.Id("tel");
        public By campoEndereco = By.Id("endereco");
        public By campoBairro = By.Id("bairroUnidAcademica");
        public By campoCep = By.Id("cepUnidAcademica");
        public By campoCidade = By.Id("cidadeUnidAcademica");
        public By comboUF = By.Id("estados-brasil");

        //Mantenedor
        public By abaUnidadeMantenedor = By.LinkText("1.3. Mantenedor");
        public By campoCnpjMantenedor = By.Id("cnpjMantenedor");
        public By campoNomeMantenedor = By.Id("nomeMantenedor");
        public By campoTelefoneMantenedor = By.Id("telefoneMantenedor");
        public By campoEmailMantenedor = By.Id("emailMantenedor");
        public By campoSiteMantenedor = By.Id("siteMantenedor");
        public By campoEnderecoMantenedor = By.Id("enderecoMantenedor");
        public By campoCepMantenedor = By.Id("cepMantenedor");
        public By campoBairroMantenedor = By.Id("bairroMantenedor");
        public By campoCidadeMantenedor = By.Id("cidadeMantenedor");
        public By comboUfMantenedor = By.Id("estados-brasil-Mantenedor");
        public By botaoSalvarMantenedor = By.XPath("//div[@id='identificacao']/app-aba-identificacao/app-modal-mantenedor/div/div/div/div[2]/div[2]/div/div/button");

        public string PreencherCredenciamento(WebDriver driver)
        {
            PreencherIdentificacaoAbaInstituicao(driver);
            PreencherIdentificacaoAbaUnidadeAcademica(driver);
            PreencherIdentificacaoAbaMantenedor(driver);
            PreencherIdentificacaoAbaRepresentacao(driver);
            PreencherRegularizacao(driver);
            PreencherOrcamentoFaturamento(driver);
            PreencherForcaTrabalho(driver);
            PreencherAreaAtuacao(driver);
            PreencherPesquisador(driver);
            PreencherLaboratorio(driver);
            PreencherDocumentacao(driver);
            PreencherPlanoExecucao(driver);
            PreencherObjetivosMetas(driver);
            PreencherRecursosHumanos(driver);
            PreencherResultados(driver);
            PreencherOrcamento(driver);
            PreencherProjetoPD(driver);
            ClicarElementoPagina(driver, botaoVoltar);
            AguardarProcessando(driver);
            SubmeterCredenciamento(driver);
            string valorCodigoCredenciamento = RetornarCodigoCredenciamento();
            return valorCodigoCredenciamento;
        }

        public void PreencherIdentificacaoAbaInstituicao(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, optionPrivado);
            ClicarElementoPagina(driver, optionPesquisa);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            AguardarProcessando(driver);
            //Thread.Sleep(2000);
            ClicarElementoPagina(driver, botaoFechar);
        }

        public void PreencherIdentificacaoAbaUnidadeAcademica(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaUnidadeAcademica);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovo);
            PreencherCampo(driver, campoUnidadeAcademica, "Unidade Teste Automatizado");
            SelecionarItemCombo(driver,comboTipoUnidadeAcademica, "Departamento");
            PreencherCampo(driver, campoEmail, "teste@teste.com");
            PreencherCampo(driver, campoSite, "www.teste.com");
            PreencherCampo(driver, campoTelefone, "92986150323");
            PreencherCampo(driver, campoEndereco, "Rua Teste 3");
            PreencherCampo(driver, campoCep, "68030260");
            PreencherCampo(driver, campoBairro, "Centro");
            PreencherCampo(driver, campoCidade, "Manaus");
            SelecionarItemCombo(driver, comboUF, "AM");
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        public void PreencherIdentificacaoAbaMantenedor(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaUnidadeMantenedor);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovo);
            string a = geradorCNPJCPF.cnpj(true);
            PreencherCampo(driver, campoCnpjMantenedor, a);
            PreencherCampo(driver, campoNomeMantenedor, geradorNome.GerarNome());
            PreencherCampo(driver, campoTelefoneMantenedor, "92986150323");
            PreencherCampo(driver, campoEmailMantenedor, "teste@teste.com");
            PreencherCampo(driver, campoSiteMantenedor, "www.teste.com");
            PreencherCampo(driver, campoEnderecoMantenedor, "Rua Teste 3");
            PreencherCampo(driver, campoCepMantenedor, "61030260");
            PreencherCampo(driver, campoBairroMantenedor, "Centro");
            PreencherCampo(driver, campoCidadeMantenedor, "Manaus");
            SelecionarItemCombo(driver, comboUfMantenedor, "AM");
            AguardarProcessando(driver);
            driver.SwitchTo().ActiveElement();
            ClicarElementoPagina(driver, botaoSalvarMantenedor);
            //ClicarElementoPagina(driver, botaoSalvarMantenedor);            
            AguardarProcessando(driver);
            bool validouRetorno = VerificarMensagemRetorno(mensagemRetorno);
            while (!validouRetorno)
            {
                AguardarProcessando(driver);
                ClicarElementoPagina(driver, botaoFechar);
                a = geradorCNPJCPF.cnpj(true);
                PreencherCampo(driver, campoCnpjMantenedor, a);
                AguardarProcessando(driver);
                //ClicarElementoPagina(driver, botaoSalvarMantenedor);
                //driver.SwitchTo().ActiveElement();
                ClicarElementoPagina(driver, botaoSalvarMantenedor);
                AguardarProcessando(driver);
                validouRetorno = VerificarMensagemRetorno(mensagemRetorno);
            }
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Representação

        public By abaRepresentacao = By.LinkText("2.Representação");
        public By comboTipoRepresentacao = By.Id("tipoRep");
        public By campoCpfRepresentacao = By.Id("txtcpf");
        public By campoNomeRepresentacao = By.Id("txtnome");
        public By campoTelefoneRepresentacao = By.Id("telefone");
        public By campoFaxRepresentacao = By.Id("fax");
        public By campoEmailRepresentacao = By.Id("email");
        public By campoCargoRepresentacao = By.Id("cargo");
        public By campoIdentidadeRepresentacao = By.Id("identidade");
        public By campoEmissorRepresentacao = By.Id("emissor");
        public By botaoSalvarRepresentacao = By.CssSelector("div.pull-right > button.btn.btn-sm.btn-primary");

        public void PreencherIdentificacaoAbaRepresentacao(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaRepresentacao);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovo);
            AguardarProcessando(driver);
            SelecionarItemCombo(driver, comboTipoRepresentacao, "Dirigente da Instituição");
            string a = geradorCNPJCPF.cpf(true);
            PreencherCampo(driver, campoCpfRepresentacao, a);
            PreencherCampo(driver, campoNomeRepresentacao, geradorNome.GerarNome());
            PreencherCampo(driver, campoTelefoneRepresentacao, "92986150323");
            PreencherCampo(driver, campoFaxRepresentacao, "9236150323");
            PreencherCampo(driver, campoEmailRepresentacao, "teste@teste.com");
            PreencherCampo(driver, campoCargoRepresentacao, "Teste");
            PreencherCampo(driver, campoIdentidadeRepresentacao, "2363040411");
            PreencherCampo(driver, campoEmissorRepresentacao, "Teste");
            ClicarElementoPagina(driver, botaoSalvarRepresentacao);
            AguardarProcessando(driver);
            bool validouRetorno = VerificarMensagemRetorno(mensagemRetorno);
            while (!validouRetorno)
            {
                AguardarProcessando(driver);
                ClicarElementoPagina(driver, botaoFechar);
                a = geradorCNPJCPF.cpf(true);
                PreencherCampo(driver, campoCpfRepresentacao, a);
                AguardarProcessando(driver);
                ClicarElementoPagina(driver, botaoSalvarRepresentacao);
                AguardarProcessando(driver);
                validouRetorno = VerificarMensagemRetorno(mensagemRetorno);
            }
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Regularização

        public By abaRegularizacao = By.LinkText("3.Regularização");
        public By textAreaRegularizacao = By.XPath("//textarea");
        public By botaoEscolherArquivoRegularizacao = By.Id("arquivo");
        public By botaoSalvarRegularizacao = By.XPath("//div[@id='regularizacao']/app-aba-regulamentarizacao/div/div[3]/div/div/button");

        public void PreencherRegularizacao(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaRegularizacao);
            AguardarProcessando(driver);
            PreencherCampo(driver, botaoEscolherArquivoRegularizacao, Constantes.CaminhoPDF);
            AguardarProcessando(driver);
            Thread.Sleep(2000);
            ClicarElementoPagina(driver, botaoFechar);
            AguardarProcessando(driver);
            PreencherCampo(driver, textAreaRegularizacao, "Teste Teste Teste Teste");
            //ClicarElementoPagina(driver, botaoSalvarRegularizacao);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Atividade em P&D

        public By abaAtividadePeD = By.LinkText("4.Atividade em P&D");

        //Orçamento/Faturamento

        public By abaOrcamentoFaturamento = By.LinkText("4.1. Orçamento/Faturamento");
        public By campoPesquisaDesenvolvimentoAnoAnterior = By.Id("pesquisa-desenvolvimento-ano-anterior");
        public By campoPesquisaDesenvolvimentoAnoAtual = By.Id("pesquisa-desenvolvimento-ano-atual");
        public By campoPesquisaDesenvolvimentoAnoSeguinte = By.Id("pesquisa-desenvolvimento-ano-seguinte");
        public By campoOutrasAtividadesAnoAnterior = By.Id("outras-atividades-ano-anterior");
        public By campoOutrasAtividadesAnoAtual = By.Id("outras-atividades-ano-atual");
        public By campoOutrasAtividadesAnoSeguinte = By.Id("outras-atividades-ano-seguinte");
        public By botaoSalvarOrcamentoFaturamento = By.XPath("//div[@id='atividade-pd']/app-aba-atividade-pd/div[2]/div[3]/div/div/button");

        public void PreencherOrcamentoFaturamento(WebDriver driver)
        {
            Random random = new Random();
            int randomNumber;
            string numero;
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaAtividadePeD);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaOrcamentoFaturamento);
            AguardarProcessando(driver);
            if (Constantes.TesteSistemalocal)
            {
                Thread.Sleep(6000);
            }
            else
            {
                Thread.Sleep(1500);
            }
            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampoSemLimpar(driver,campoPesquisaDesenvolvimentoAnoAnterior, numero);
            Thread.Sleep(300);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampoSemLimpar(driver, campoPesquisaDesenvolvimentoAnoAtual, numero);
            Thread.Sleep(300);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampoSemLimpar(driver, campoPesquisaDesenvolvimentoAnoSeguinte, numero);
            Thread.Sleep(300);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampoSemLimpar(driver, campoOutrasAtividadesAnoAnterior, numero);
            Thread.Sleep(300);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampoSemLimpar(driver, campoOutrasAtividadesAnoAtual, numero);
            Thread.Sleep(300);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampoSemLimpar(driver, campoOutrasAtividadesAnoSeguinte, numero);
            //InserirTeclaTab(campoContratadosOutrosAtividadePD);
            Thread.Sleep(300);

            ////ClicarElementoPagina(driver, botaoSalvarOrcamentoFaturamento);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        //Força de Trabalho

        public By campoQuadroEfetivoSuperiorAtividadePD = By.Id("quadro-efetivo-superio-atividade-pd");
        public By campoQuadroEfetivoOutrosAtividadePD = By.Id("quadro-efetivo-outros-atividade-pd");
        public By campoContratadosSuperiorAtividadePD = By.Id("contratados-nivel-superior-atividade-pd");
        public By campoContratadosOutrosAtividadePD = By.Id("contratados-outros-atividade-pd");
        public By campoQuadroEfetivoNivelSuperiorEnsino = By.Id("contratados-nivel-superior-atividade-ensino02");
        public By campoQuadroEfetivoOutrosEnsino = By.Id("quadro-efetivo-outros-atividade-ensino");
        public By campoContratadosSuperiorEnsino = By.Id("contratados-nivel-superior-atividade-ensino01");
        public By campoContratadosOutrosEnsino = By.Id("contratados-outros-atividade-ensino");
        public By campoEfetivoSuperiorOutros = By.Id("quadro-efetivo-nivel-superior-outras-atividades");
        public By campoEfetivoOutrosOutros = By.Id("quadro-efetivo-outras-atividades");
        public By campoContratadorSuperiorOutros = By.Id("contratados-nivel-superior-outras-atividades");
        public By campoContratadorOutrosOutros = By.Id("contratados-outros-outras-atividades");

        public By botaoSalvarForcaTrabalho = By.XPath("//div[@id='atividade-pd']/app-aba-atividade-pd/div[2]/div[4]/div/div/button");

        public void PreencherForcaTrabalho(WebDriver driver)
        {
            Random random = new Random();
            int randomNumber;
            string numero;
            AguardarProcessando(driver);
            if (Constantes.TesteSistemalocal)
            {
                Thread.Sleep(5000);
            }
            else
            {
                Thread.Sleep(1500);
            }
            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoQuadroEfetivoSuperiorAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoQuadroEfetivoOutrosAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoContratadosSuperiorAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoContratadosOutrosAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoQuadroEfetivoNivelSuperiorEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoQuadroEfetivoOutrosEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoContratadosSuperiorEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoEfetivoSuperiorOutros, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoContratadosOutrosEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoEfetivoOutrosOutros, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoContratadorSuperiorOutros, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campoContratadorOutrosOutros, numero);

            //ClicarElementoPagina(driver, botaoSalvarForcaTrabalho);
            ClicarElementoPagina(driver, botaoSalvar);

            AguardarProcessando(driver);
            Thread.Sleep(1000);
            //Aparecem 2 mensagens, uma de confirmacao que salvou e outra Nenhum registro encontrado. Tenta clicar nas duas uma por vez.
            if (IsElementDisplayed(driver, botaoFechar) || IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
            {
                AguardarProcessando(driver);
                AguardarProcessando(driver);
                if (IsElementDisplayed(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado))
                {
                    ClicarElementoPagina(driver, botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado);
                }

                AguardarProcessando(driver);

                if (IsElementDisplayed(driver, botaoFechar))
                {
                    ClicarElementoPagina(driver, botaoFechar);
                }
                AguardarProcessando(driver);
            }
        }

        //Area de Atuacao

        public By botaoNovoAreaAtuacao = By.ClassName("fa-plus");
        public By comboAreaPrincipal = By.Id("drop-list");
        public By comboAreaAtuacao = By.Id("combo-area-atuacao");
        public By botaoSalvarAreaAtuacao = By.XPath("(//button[@type='button'])[2]");
        public void PreencherAreaAtuacao(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovoAreaAtuacao);
            SelecionarItemCombo(driver, comboAreaPrincipal, "Engenharias");
            AguardarProcessando(driver);
            SelecionarItemCombo(driver, comboAreaAtuacao, "Estruturas");
            AguardarProcessando(driver);
            //ClicarElementoPagina(driver, botaoSalvarAreaAtuacao);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Pesquisadores

        public By abaPesquisadores = By.LinkText("5.Pesquisadores");
        public By comboTipoPesquisador = By.Name("tipo");
        public By comboUnidadeAcademica = By.Id("drop-list");
        public By campoNomePesquisador = By.Id("nomePesquisador");
        public By campoFormacaoAcademicaPesquisador = By.Id("Formacao");
        public By comboTitulacaoPesquisador = By.Id("Titulacao");
        public By campoLinhaPesquisaPesquisador = By.Id("LinhaPesquisa");
        public By campoVinculoInstitucionalPesquisador = By.Id("vinculoInstitucional");
        public By anexarArquivoPesquisador = By.Id("arquivo");

        public void PreencherPesquisador(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaPesquisadores);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovo);
            AguardarProcessando(driver);
            SelecionarItemCombo(driver, comboTipoPesquisador, "Pesquisador");
            SelecionarItemCombo(driver, comboUnidadeAcademica, "Unidade Teste Automatizado");
            PreencherCampo(driver, campoNomePesquisador, geradorNome.GerarNome());
            PreencherCampo(driver, campoFormacaoAcademicaPesquisador, "Formacao em Pesquisa");
            SelecionarItemCombo(driver, comboTitulacaoPesquisador, "Mestrado");
            PreencherCampo(driver, campoLinhaPesquisaPesquisador, "Pesquisa Tecnológica");
            PreencherCampo(driver, campoVinculoInstitucionalPesquisador, "Pesquisador Chefe");
            PreencherCampo(driver, anexarArquivoPesquisador, Constantes.CaminhoPDF);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Laboratorio

        public By abaLaboratorio = By.LinkText("6.Laboratório");
        public By comboTipoLaboratorio = By.Name("tipo");
        public By campoResponsavelLaboratorio = By.Id("responsavel");
        public By comboLocalLaboratorio = By.XPath("(//select[@name='tipo'])[2]");
        public By campoObjetivoLaboratorio = By.Id("objetivo");
        public By campoAtividadesLaboratorio = By.Id("atividadeDesenvolvida");
        public By campoAreaFisicaLaboratorio = By.Id("areaFisica");
        public By campoRelacaoEquipamentosLaboratorio = By.Id("relacao");
        public By anexarArquivoLaboratorio = By.Id("arquivo");

        public void PreencherLaboratorio(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaLaboratorio);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovo);
            AguardarProcessando(driver);
            SelecionarItemCombo(driver, comboTipoLaboratorio, "Montado");
            PreencherCampo(driver, campoResponsavelLaboratorio, geradorNome.GerarNome());
            SelecionarItemCombo(driver, comboLocalLaboratorio, "Instituição");
            PreencherCampo(driver, campoObjetivoLaboratorio, "Teste Teste");
            PreencherCampo(driver, campoAtividadesLaboratorio, "Teste Teste");
            PreencherCampo(driver, campoAreaFisicaLaboratorio, "Teste Teste");
            PreencherCampo(driver, campoRelacaoEquipamentosLaboratorio, "Teste Teste");
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Documentação

        public By abaDocumentacao = By.LinkText("7.Documentação");
        public By comboTipoDcoumento = By.Name("tipo");
        public By campoDiscriminacao = By.Id("discriminacao");
        public By anexarArquivoDocumentacao = By.Id("arquivo");

        public void PreencherDocumentacao(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaDocumentacao);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovo);
            AguardarProcessando(driver);
            SelecionarItemCombo(driver, comboTipoDcoumento, "Outros");
            PreencherCampo(driver, campoDiscriminacao, "Teste");
            PreencherCampo(driver, anexarArquivoDocumentacao, Constantes.CaminhoPDF);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Anexo

        public By abaAnexo = By.LinkText("8.Anexo");

        //Aba 8.1.1

        private By abaDadosPlano = By.LinkText("8.1.1 Dados do Plano");
        private By campoTituloPlano = By.Id("titulo");
        private By campoCoordenadorPlano = By.Id("coordenador-es");
        private By campoExecutorPlano = By.Id("executor-es");
        private By checkboxPesquisaPlano = By.XPath("//form[@id='formulario']/div[4]/div/div/label/i");
        private By campoPrazoExecucaoPlano = By.Id("prazo-de-execucao");
        private By anexarArquivoPlano = By.Id("arquivo");

        private void PreencherPlanoExecucao(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaAnexo);
            //Tem 3 processando aqui por algum motivo
            AguardarProcessando(driver);
            AguardarProcessando(driver);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaDadosPlano);
            AguardarProcessando(driver);
            PreencherCampo(driver, campoTituloPlano, "Teste Teste Teste");
            PreencherCampo(driver, campoCoordenadorPlano, "Teste Teste Teste");
            PreencherCampo(driver, campoExecutorPlano, "Teste Teste Teste");
            ClicarElementoPagina(driver, checkboxPesquisaPlano);
            PreencherCampo(driver, campoPrazoExecucaoPlano, "Teste Teste Teste");
            PreencherCampo(driver, anexarArquivoPlano, Constantes.CaminhoPDF);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        //Aba 8.1.2

        private By abaObjetivosMetas = By.LinkText("8.1.2 Objetivos e Metas");
        private By campoObjetivosMetas = By.Id("objetivos-e-metas-text");

        private void PreencherObjetivosMetas(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaObjetivosMetas);
            AguardarProcessando(driver);
            PreencherCampo(driver, campoObjetivosMetas, "Teste Teste Teste");
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }


        //Aba 8.1.3

        private By abaRecursosHumanos = By.LinkText("8.1.3 Recursos Humanos");
        private By campoRecursosHumanos = By.Id("rh-text");

        private void PreencherRecursosHumanos(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaRecursosHumanos);
            AguardarProcessando(driver);
            PreencherCampo(driver, campoRecursosHumanos, "Teste Teste Teste");
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        //Aba 8.1.4

        private By abaResultados = By.LinkText("8.1.4 Resultados");
        private By campoResultados = By.Id("resultados-serem-alcancados");

        private void PreencherResultados(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaResultados);
            AguardarProcessando(driver);
            PreencherCampo(driver, campoResultados, "Teste Teste Teste");
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }

        #endregion

        #region Aba 8.1.5

        //Aba 8.1.5

        private By abaOrcamento = By.LinkText("8.1.5 Orçamento");

        private By campo0_0 = By.Id("0-0");
        private By campo0_1 = By.Id("0-1");
        private By campo0_2 = By.Id("0-2");
        private By campo0_3 = By.Id("0-3");
        private By campo0_4 = By.Id("0-4");
        private By campo0_5 = By.Id("0-5");
        private By campo0_6 = By.Id("0-6");
        private By campo0_7 = By.Id("0-7");

        private By campo1_0 = By.Id("1-0");
        private By campo1_1 = By.Id("1-1");
        private By campo1_2 = By.Id("1-2");
        private By campo1_3 = By.Id("1-3");
        private By campo1_4 = By.Id("1-4");
        private By campo1_5 = By.Id("1-5");
        private By campo1_6 = By.Id("1-6");
        private By campo1_7 = By.Id("1-7");

        private By campo2_0 = By.Id("2-0");
        private By campo2_1 = By.Id("2-1");
        private By campo2_2 = By.Id("2-2");
        private By campo2_3 = By.Id("2-3");
        private By campo2_4 = By.Id("2-4");
        private By campo2_5 = By.Id("2-5");
        private By campo2_6 = By.Id("2-6");
        private By campo2_7 = By.Id("2-7");

        private By campo3_0 = By.Id("3-0");
        private By campo3_1 = By.Id("3-1");
        private By campo3_2 = By.Id("3-2");
        private By campo3_3 = By.Id("3-3");
        private By campo3_4 = By.Id("3-4");
        private By campo3_5 = By.Id("3-5");
        private By campo3_6 = By.Id("3-6");
        private By campo3_7 = By.Id("3-7");

        private By campo4_0 = By.Id("4-0");
        private By campo4_1 = By.Id("4-1");
        private By campo4_2 = By.Id("4-2");
        private By campo4_3 = By.Id("4-3");
        private By campo4_4 = By.Id("4-4");
        private By campo4_5 = By.Id("4-5");
        private By campo4_6 = By.Id("4-6");
        private By campo4_7 = By.Id("4-7");

        private By campo5_0 = By.Id("5-0");
        private By campo5_1 = By.Id("5-1");
        private By campo5_2 = By.Id("5-2");
        private By campo5_3 = By.Id("5-3");
        private By campo5_4 = By.Id("5-4");
        private By campo5_5 = By.Id("5-5");
        private By campo5_6 = By.Id("5-6");
        private By campo5_7 = By.Id("5-7");

        private By campo6_0 = By.Id("6-0");
        private By campo6_1 = By.Id("6-1");
        private By campo6_2 = By.Id("6-2");
        private By campo6_3 = By.Id("6-3");
        private By campo6_4 = By.Id("6-4");
        private By campo6_5 = By.Id("6-5");
        private By campo6_6 = By.Id("6-6");
        private By campo6_7 = By.Id("6-7");

        private By campo7_0 = By.Id("7-0");
        private By campo7_1 = By.Id("7-1");
        private By campo7_2 = By.Id("7-2");
        private By campo7_3 = By.Id("7-3");
        private By campo7_4 = By.Id("7-4");
        private By campo7_5 = By.Id("7-5");
        private By campo7_6 = By.Id("7-6");
        private By campo7_7 = By.Id("7-7");

        private By campo8_0 = By.Id("8-0");
        private By campo8_1 = By.Id("8-1");
        private By campo8_2 = By.Id("8-2");
        private By campo8_3 = By.Id("8-3");
        private By campo8_4 = By.Id("8-4");
        private By campo8_5 = By.Id("8-5");
        private By campo8_6 = By.Id("8-6");
        private By campo8_7 = By.Id("8-7");

        private By campo9_0 = By.Id("9-0");
        private By campo9_1 = By.Id("9-1");
        private By campo9_2 = By.Id("9-2");
        private By campo9_3 = By.Id("9-3");
        private By campo9_4 = By.Id("9-4");
        private By campo9_5 = By.Id("9-5");
        private By campo9_6 = By.Id("9-6");
        private By campo9_7 = By.Id("9-7");

        private By campo10_0 = By.Id("10-0");
        private By campo10_1 = By.Id("10-1");
        private By campo10_2 = By.Id("10-2");
        private By campo10_3 = By.Id("10-3");
        private By campo10_4 = By.Id("10-4");
        private By campo10_5 = By.Id("10-5");
        private By campo10_6 = By.Id("10-6");
        private By campo10_7 = By.Id("10-7");

        private void PreencherOrcamento(WebDriver driver)
        {
            Random random = new Random();
            int randomNumber;
            string numero;

            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaOrcamento);
            AguardarProcessando(driver);
            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo0_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo1_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo2_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo3_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo4_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo5_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo6_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo7_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo8_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo9_7, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_0, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_1, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_2, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_3, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_4, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_5, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_6, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(driver, campo10_7, numero);

            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
        }
        #endregion

        #region 8.2

        private By abaProjetoPD = By.LinkText("8.2. Projeto de P&D");
        public By botaoNovoProjetoPD = By.ClassName("fa-plus");
        public By comboTipoProjeto = By.Id("tipo");
        public By campoTituloProjeto = By.Id("titulo");
        public By campoDescricaoProjeto = By.Id("descricaoProjeto");
        public By campoEquipeCoordenadorProjeto = By.Id("coordenador-es");
        public By campoValorProjeto = By.Id("executor-es");
        public By checkboxPesquisa = By.XPath("//div[@id='projeto-pd']/div[2]/app-aba-projeto-pd/app-modal-anexo-pd/div/div/div/div[2]/div/div[5]/div/div/label/i");
        public By campoRelacaoEquipamentos = By.Id("prazo-de-execucao");

        private void PreencherProjetoPD(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, abaProjetoPD);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoNovoProjetoPD);
            AguardarProcessando(driver);
            SelecionarItemCombo(driver, comboTipoProjeto, "Projeto");
            PreencherCampo(driver, campoTituloProjeto, "Teste Teste");
            PreencherCampo(driver, campoDescricaoProjeto, "Teste Teste Teste");
            PreencherCampo(driver, campoEquipeCoordenadorProjeto, "Teste Teste Teste");
            PreencherCampo(driver, campoValorProjeto, "Teste Teste Teste");
            //ClicarElementoPagina(driver, checkboxPesquisa);
            PreencherCampo(driver, campoRelacaoEquipamentos, "Teste Teste Teste");
            ClicarElementoPagina(driver, botaoSalvar);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, botaoFechar);
            AguardarProcessando(driver);
        }


        #endregion


    }
}
