using OpenQA.Selenium;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using CTIS.SIMNAC.Teste.Automatizado.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;
using System.Threading;

namespace CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects
{
    [TestClass]
    public class PaginaTeste : PaginaBase
    {

        #region Declaração de variáveis públicas da classe

        public By comboCapituloNCM = By.Id("ano");

        public By botaoPesquisar = By.ClassName("btn-primary");
        public By botaoLimpar = By.ClassName("btn-default");
        public By botaoOcultarFiltros = By.ClassName("f");
        public By botaoExibirFiltros = By.ClassName("z");
        public By botaoExportar = By.ClassName("dropdown-toggle");
        public By botaoSalvarPDF = By.ClassName("fa-file-pdf-o");



        public PaginaInicial PaginaInicial { get; private set; }

        #endregion

        #region Métodos públicos

        public PaginaTeste(RemoteWebDriver driver) : base(driver)
        { }

        /// <summary>
        /// Preenche os campos da tela de Departamento e clica no botão salvar.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void PesquisarCapituloNCM(string Descricao)
        {          
            SelecionarItemCombo(comboCapituloNCM, Descricao);
            ClicarElementoPagina(botaoPesquisar);
        }

        public void Limpar()
        {         
            ClicarElementoPagina(botaoLimpar);
            Thread.Sleep(800);
        }

        public void OcultarFiltros()
        {            
            ClicarElementoPagina(botaoOcultarFiltros);
            Thread.Sleep(800);
        }

        public void ExibirFiltros()
        {
            ClicarElementoPagina(botaoExibirFiltros);
            Thread.Sleep(800);
        }

        public void ExportarPDF()
        {
            ClicarElementoPagina(botaoExportar);
            Thread.Sleep(700);
            ClicarElementoPagina(botaoSalvarPDF);
        }


        /// <summary>
        /// Cancela o cadastro 
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void CancelarCadastro()
        {
            ClicarElementoPagina(BotaoCancelar);
        }

        #endregion
    }
}
