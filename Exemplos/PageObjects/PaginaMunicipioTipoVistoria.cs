using OpenQA.Selenium;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects
{
    [TestClass]
    public class PaginaMunicipioTipoVistoria : PaginaBase
    {
        #region Declaração de variáveis públicas da classe

        public By comboPostoVistoria = By.Id("drop-list");
        public By botaoPesquisar = By.ClassName("fa-search");
        public By botaoLimpar = By.ClassName("fa-eraser");
        public By botaoOcultarFiltros = By.XPath("(//button[@type='button'])[2]");
        public By botaoExibirFiltros = By.XPath("//button[@type='button']");                                         
        public By botaoSalvarAlteracao = By.ClassName("fa-save");
        public By botaoConfirmar = By.ClassName("fa-check");
        public By botaoNaoConfirmar = By.ClassName("fa-times");
        public By botaoFecharMensagemConfirmacaoCadastro = By.ClassName("fa-times");
        public By labelVistoriaNoPosto = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-posto-vistoria/div/div[2]/div/section/article/form/fieldset/div[3]/div/div/div/label");
        public By labelVistoriaExterna = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-posto-vistoria/div/div[2]/div/section/article/form/fieldset/div[3]/div/div/div[2]/label");
        public By labelVistoriaDocumental = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-posto-vistoria/div/div[2]/div/section/article/form/fieldset/div[3]/div/div/div[3]/label");
        public By labelTotalVistoria = By.XPath("//section[@id='content']/section/section/section/section/app-manter-capacidade-posto-vistoria/div/div[2]/div/section/article/form/fieldset/div[3]/div/div/div[4]/label");
        public By txtQuantidadeVistoriaNoPosto = By.Id("txtQtde3");
        public By txtQuantidadeVistoriaExterna = By.Id("txtQtde6");
        public By txtQuantidadeVistoriaDocumental = By.Id("txtQtde9");
        public By txtQuantidadeVistoriaTotal = By.Id("txtQtde12");
        public By txtJustificativa = By.Id("txtJustificativa");
        public By txtPostoVistoria = By.Id("txtDscPostoVistoria");
        public By txtMunicipio = By.Id("txtDscMunicipio");
        public By optionVistoriaDocumental = By.XPath("//form[@id='formulario']/div[3]/div/div/div/label[2]");
        public By optionVistoriaFisica = By.XPath("//form[@id='formulario']/div[3]/div/div/div/label");
        

        #endregion

        #region Métodos públicos

        public PaginaMunicipioTipoVistoria(RemoteWebDriver driver) : base(driver)
        { }

        public void PesquisarPostoVistoria(string postoVistoria)
        {
            AguardarProcessando();
            SelecionarItemCombo(comboPostoVistoria, postoVistoria);
            ClicarElementoPagina(botaoPesquisar);
            AguardarProcessando();
        }

        //Clica no botão Limpar da tela de pesquisa
        public void Limpar()
        {
            ClicarElementoPagina(botaoLimpar);
            Thread.Sleep(1500);
        }


        //Clica no botão Exibir Filtros
        public void OcultarFiltros()
        {
            ClicarElementoPagina(botaoOcultarFiltros);
            AguardarProcessando();
        }
        //Clica no botão Exibir Filtros
        public void ExibirFiltros()
        {
            ClicarElementoPagina(botaoExibirFiltros);
            AguardarProcessando();
        }

        //Retorna o Xpath do item específico (exemplo Posto de Vistoria) do resultado da pesquisa, usado para fazer assert ce textos do resultados, etc.
        public By RetornaItemResultadoPesquisa(int linha, int item)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-posto-vistoria/div/div[3]/div/section/app-manter-capacidade-posto-vistoria-grid/app-grid/div/table/tbody/tr[{linha}]/td[{item}]");
        }

        //Retorna o Xpath do item específico (exemplo Posto de Vistoria) do histórico de inclusão/alteração, usado para fazer assert ce textos do resultados, etc.
        public By RetornaItemHistorico(int linha, int item)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-perfil/div/div[3]/div/section/app-manter-capacidade-perfil-grid/app-modal-historico-capacidade-perfil/div/div/div/div[2]/form/section/article/div/div/app-manter-historico-capacidade-perfil-grid/app-grid/div/table/tbody/tr[{linha}]/td[{item}]");
        }

        //Retorna o XPath do botão Alterar da linha informada
        public By BotaoAlterarLinhaSelecionada(int linha)
        {
            return By.XPath($"//section[@id='content']/section/section/section/section/app-manter-capacidade-posto-vistoria/div/div[3]/div/section/app-manter-capacidade-posto-vistoria-grid/app-grid/div/table/tbody/tr[{linha}]/td[4]/a/i");
        }


        //Valida o texto do item do resultado da pesquisa da linha informada. 
        public void ValidarItensResultadoPesquisa(int linha, string codigo, string municipio, string tipoVistoria)
        {
            AguardarProcessando();
            ValidarTexto(codigo, RetornaItemResultadoPesquisa(linha, 1));
            ValidarTexto(municipio, RetornaItemResultadoPesquisa(linha, 2));
            ValidarTexto(tipoVistoria, RetornaItemResultadoPesquisa(linha, 3));
        }

        //Abre a tela de alteração do item da linha informada.
        public void AbrirAlterar(int linha)
        {
            AguardarProcessando();
            ClicarElementoPagina(BotaoAlterarLinhaSelecionada(linha));
            AguardarProcessando();
        }

        public void AlterarTipoVistoria(bool vistoriaFisica, string justificativa)
        {
            if (vistoriaFisica)
            {
                ClicarElementoPagina(optionVistoriaFisica);
            }
            else
            {
                ClicarElementoPagina(optionVistoriaDocumental);
            }
            PreencherCampo(txtJustificativa, justificativa);
            ClicarElementoPagina(botaoSalvarAlteracao);
            AguardarProcessando();
            ClicarElementoPagina(botaoConfirmar);
            AguardarProcessando();
            ClicarElementoPagina(botaoFecharMensagemConfirmacaoCadastro);
            AguardarProcessando();
        }


        #endregion

    }
}