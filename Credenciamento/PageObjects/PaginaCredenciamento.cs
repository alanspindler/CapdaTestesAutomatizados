using OpenQA.Selenium;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Threading;
using System.Security.Cryptography;
using System;


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
            PreencherCampo(campoCnpjMantenedor, geradorCNPJCPF.GerarCNPJ());
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



        //label/i

        //div[@id='identificacao']/app-aba-identificacao/div[2]/div/div/div/div/label[3]/i

    }
}
