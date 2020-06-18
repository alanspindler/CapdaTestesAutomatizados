using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System.Security.Cryptography;
using System;
using System.Security.Policy;

namespace Lampp.CAPDA.Teste.Automatizado.Credenciamento.PageObjects
{
    [TestClass]
    public class PaginaCredenciamento : PaginaBase
    {
        public GeradorNome geradorNome { get; set; }
        public GeradorCNPJCPF geradorCNPJCPF { get; set; }
        public PaginaCredenciamento(RemoteWebDriver driver) : base(driver)
        {
            geradorNome = new GeradorNome();
            geradorCNPJCPF = new GeradorCNPJCPF();
        }

        #region Pagina Principal

        public By botaoSolicitarCredenciamento = By.ClassName("fa-plus");
        public void SolicitarCredenciamento()
        {
            ClicarElementoPagina(botaoSolicitarCredenciamento);
        }

        #endregion

        #region Geral

        public By botaoSalvar = By.ClassName("fa-save");
        public By botaoNovo = By.ClassName("fa-plus-square");
        public By botaoFechar = By.CssSelector("div.modal-footer > button.btn.btn-default.btn-sm");
        public By botaoFecharMensagemConfirmacao = By.XPath("(//button[@type='button'])[5]");
        public By botaoFecharMensagemConfirmacaoNenhumRegistroEncontrado = By.XPath("//dialog-wrapper[2]/div/app-modal/div/div/div[3]/button");
        public By mensagemRetorno = By.CssSelector("p");

        //public By botaoFechar = By.ClassName("btn-default");

        //div[3]/button
        #endregion

        #region Identificacao

        //Instituicao
        public By optionPrivado = By.XPath("//label/i");
        public By optionEnsino = By.XPath("//label[2]");
        public By optionPesquisa = By.XPath("//label[3]/i");

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

        public void PreencherCredenciamento()
        {
            PreencherIdentificacaoAbaInstituicao();
            PreencherIdentificacaoAbaUnidadeAcademica();
            PreencherIdentificacaoAbaMantenedor();
            PreencherIdentificacaoAbaRepresentacao();
            PreencherRegularizacao();
            PreencherOrcamentoFaturamento();
            PreencherForcaTrabalho();
            PreencherAreaAtuacao();
            PreencherPesquisador();
            PreencherLaboratorio();
            PreencherDocumentacao();
        }

        public void PreencherIdentificacaoAbaInstituicao()
        {            
            AguardarProcessando();
            ClicarElementoPagina(optionPrivado);
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
        }

        public void PreencherIdentificacaoAbaUnidadeAcademica()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaUnidadeAcademica);
            AguardarProcessando();
            ClicarElementoPagina(botaoNovo);
            PreencherCampo(campoUnidadeAcademica, "Unidade Teste Automatizado");
            SelecionarItemCombo(comboTipoUnidadeAcademica, "Departamento");
            PreencherCampo(campoEmail, "teste@teste.com");
            PreencherCampo(campoSite, "www.teste.com");
            PreencherCampo(campoTelefone, "92986150323");
            PreencherCampo(campoEndereco, "Rua Teste 3");
            PreencherCampo(campoCep, "68030260");
            PreencherCampo(campoBairro, "Centro");
            PreencherCampo(campoCidade, "Manaus");
            SelecionarItemCombo(comboUF, "AM");
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
        }

        public void PreencherIdentificacaoAbaMantenedor()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaUnidadeMantenedor);
            AguardarProcessando();
            ClicarElementoPagina(botaoNovo);
            string a = geradorCNPJCPF.cnpj(true);
            PreencherCampo(campoCnpjMantenedor, a);
            PreencherCampo(campoNomeMantenedor, geradorNome.GerarNome());
            PreencherCampo(campoTelefoneMantenedor, "92986150323");
            PreencherCampo(campoEmailMantenedor, "teste@teste.com");
            PreencherCampo(campoSiteMantenedor, "www.teste.com");
            PreencherCampo(campoEnderecoMantenedor, "Rua Teste 3");
            PreencherCampo(campoCepMantenedor, "61030260");
            PreencherCampo(campoBairroMantenedor, "Centro");
            PreencherCampo(campoCidadeMantenedor, "Manaus");
            SelecionarItemCombo(comboUfMantenedor, "AM");
            ClicarElementoPagina(botaoSalvarMantenedor);            
            AguardarProcessando();            
            ClicarElementoPagina(botaoFechar);            
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

        public void PreencherIdentificacaoAbaRepresentacao()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaRepresentacao);
            AguardarProcessando();
            ClicarElementoPagina(botaoNovo);
            AguardarProcessando();
            SelecionarItemCombo(comboTipoRepresentacao, "Dirigente da Instituição");
            string a = geradorCNPJCPF.cpf(true);            
            PreencherCampo(campoCpfRepresentacao, a);
            PreencherCampo(campoNomeRepresentacao, geradorNome.GerarNome());
            PreencherCampo(campoTelefoneRepresentacao, "92986150323");
            PreencherCampo(campoFaxRepresentacao, "9236150323");
            PreencherCampo(campoEmailRepresentacao, "teste@teste.com");
            PreencherCampo(campoCargoRepresentacao, "Teste");
            PreencherCampo(campoIdentidadeRepresentacao, "2363040411");
            PreencherCampo(campoEmissorRepresentacao, "Teste");
            ClicarElementoPagina(botaoSalvarRepresentacao);            
            AguardarProcessando();
            VerificarMensagemRetorno(mensagemRetorno);
            ClicarElementoPagina(botaoFechar);
        }

        #endregion

        #region Regularização

        public By abaRegularizacao = By.LinkText("3.Regularização");
        public By textAreaRegularizacao = By.XPath("//textarea");
        public By botaoEscolherArquivoRegularizacao = By.Id("arquivo");
        public By botaoSalvarRegularizacao = By.XPath("//div[@id='regularizacao']/app-aba-regulamentarizacao/div/div[3]/div/div/button");

        public void PreencherRegularizacao()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaRegularizacao);
            AguardarProcessando();            
            PreencherCampo(botaoEscolherArquivoRegularizacao, Constantes.CaminhoPDF);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
            AguardarProcessando();
            PreencherCampo(textAreaRegularizacao, "Teste Teste Teste Teste");
            ClicarElementoPagina(botaoSalvarRegularizacao);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
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

        public void PreencherOrcamentoFaturamento()
        {
            Random random = new Random();
            int randomNumber;
            string numero;
            AguardarProcessando();
            ClicarElementoPagina(abaAtividadePeD);
            AguardarProcessando();
            ClicarElementoPagina(abaOrcamentoFaturamento);
            AguardarProcessando();

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoPesquisaDesenvolvimentoAnoAnterior, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoPesquisaDesenvolvimentoAnoAtual, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoPesquisaDesenvolvimentoAnoSeguinte, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoOutrasAtividadesAnoAnterior, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoOutrasAtividadesAnoAtual, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoOutrasAtividadesAnoSeguinte, numero);        
            
            ClicarElementoPagina(botaoSalvarOrcamentoFaturamento);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
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
     
        public void PreencherForcaTrabalho()
        {
            Random random = new Random();
            int randomNumber;
            string numero;
            AguardarProcessando();            

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoQuadroEfetivoSuperiorAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoQuadroEfetivoOutrosAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoContratadosSuperiorAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoContratadosOutrosAtividadePD, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoQuadroEfetivoNivelSuperiorEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoQuadroEfetivoOutrosEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoContratadosSuperiorEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoEfetivoSuperiorOutros, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoContratadosOutrosEnsino, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoEfetivoOutrosOutros, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoContratadorSuperiorOutros, numero);

            randomNumber = random.Next(1, 10);
            numero = randomNumber.ToString();
            PreencherCampo(campoContratadorOutrosOutros, numero);

            ClicarElementoPagina(botaoSalvarForcaTrabalho);

            AguardarProcessando();
            Thread.Sleep(1000);
            //Aparecem 2 mensagens, uma de confirmacao que salvou e outra Nenhum registro encontrado. Tenta clicar nas duas uma por vez.
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
                    AguardarProcessando();
                }                
            }
        }

        //Area de Atuacao

        public By botaoNovoAreaAtuacao = By.ClassName("fa-plus");
        public By comboAreaPrincipal = By.Id("drop-list");
        public By comboAreaAtuacao = By.Id("combo-area-atuacao");
        public By botaoSalvarAreaAtuacao = By.XPath("(//button[@type='button'])[2]");
        public void PreencherAreaAtuacao()
        {
            AguardarProcessando();
            ClicarElementoPagina(botaoNovoAreaAtuacao);
            SelecionarItemCombo(comboAreaPrincipal, "Engenharias");
            SelecionarItemCombo(comboAreaAtuacao, "Estruturas");
            ClicarElementoPagina(botaoSalvarAreaAtuacao);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
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

        public void PreencherPesquisador()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaPesquisadores);
            AguardarProcessando();
            ClicarElementoPagina(botaoNovo);
            AguardarProcessando();
            SelecionarItemCombo(comboTipoPesquisador, "Pesquisador");
            SelecionarItemCombo(comboUnidadeAcademica, "Unidade Teste Automatizado");
            PreencherCampo(campoNomePesquisador, geradorNome.GerarNome());
            PreencherCampo(campoFormacaoAcademicaPesquisador, "Formacao em Pesquisa");
            SelecionarItemCombo(comboTitulacaoPesquisador, "Mestrado");
            PreencherCampo(campoLinhaPesquisaPesquisador, "Pesquisa Tecnológica");
            PreencherCampo(campoVinculoInstitucionalPesquisador, "Pesquisador Chefe");
            PreencherCampo(anexarArquivoPesquisador, Constantes.CaminhoPDF);
            AguardarProcessando();
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
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

        public void PreencherLaboratorio()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaLaboratorio);
            AguardarProcessando();
            ClicarElementoPagina(botaoNovo);
            AguardarProcessando();
            SelecionarItemCombo(comboTipoLaboratorio, "Montado");
            PreencherCampo(campoResponsavelLaboratorio, geradorNome.GerarNome());
            SelecionarItemCombo(comboLocalLaboratorio, "Instituição");
            PreencherCampo(campoObjetivoLaboratorio, "Teste Teste");
            PreencherCampo(campoAtividadesLaboratorio, "Teste Teste");
            PreencherCampo(campoAreaFisicaLaboratorio, "Teste Teste");
            PreencherCampo(campoRelacaoEquipamentosLaboratorio, "Teste Teste");            
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
        }

        #endregion

        #region Documentação

        public By abaDocumentacao = By.LinkText("7.Documentação");        
        public By comboTipoDcoumento = By.Name("tipo");
        public By campoDiscriminacao = By.Id("discriminacao");
        public By anexarArquivoDocumentacao = By.Id("arquivo");

        public void PreencherDocumentacao()
        {
            AguardarProcessando();
            ClicarElementoPagina(abaDocumentacao);
            AguardarProcessando();
            ClicarElementoPagina(botaoNovo);
            AguardarProcessando();
            SelecionarItemCombo(comboTipoDcoumento, "Outros");
            PreencherCampo(campoDiscriminacao, "Teste");
            PreencherCampo(anexarArquivoDocumentacao, Constantes.CaminhoPDF);
            AguardarProcessando();
            ClicarElementoPagina(botaoSalvar);
            AguardarProcessando();
            ClicarElementoPagina(botaoFechar);
        } 

        #endregion
    }
}
