using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using System.IO;
using System.Threading;
using System;

namespace Lampp.CAPDA.Teste.Automatizado.Login.PageObjects
{
       public class PaginaInicial : PaginaBase
    {
        #region Declaração de variáveis publicas da classe

        public By m_campoLogin = By.Name("usuario");
        public By m_campoSenha = By.Name("senha");
        //public By m_campoLogin = By.XPath("//input[@placeholder='Email']");
        //public By m_campoSenha = By.XPath("//input[@placeholder='Password']");
        public By botaoLogin = By.XPath("//span[.='Login']");


        #endregion

        #region Declaração de variáveis privadas da classe

        private By btnEntrar = By.ClassName("btn-success");        
        private By btnSair = By.ClassName("i-logout");

        #endregion

        #region Declaração de variáveis protected da classe

        protected PaginaPrincipal PaginaPrincipal;

        #endregion

        #region Métodos privados

        private void carregarDadosLoginArquivoTexto(out string usuario, out string senha)
        {
            var caminhoArquivo = Path.Combine(Global.DIRETORIO_APLICACAO, "login.ini");
            var conteudoArquivo = File.ReadAllLines(caminhoArquivo);

            usuario = conteudoArquivo[0]; // Primeira linha do arquivo.
            senha = conteudoArquivo[1]; // Segunda linha do arquivo.
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Construtor da página criar email
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 26/11/2015</remarks>
        public PaginaInicial(RemoteWebDriver driver) : base(driver)
        {
            PaginaPrincipal = new PaginaPrincipal(driver);            
        }

        /// <summary>
        /// Abre a página de Login
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void AbrirPagina(string URL)
        {
            try
            {
                driver.Navigate().GoToUrl(URL);
                //AguardarCarregarPagina();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Faz login com dados informados, com parâmetro indicando se a página aberta é a página inicial
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void FazerLogin(string usuario, string senha)
        {
            Thread.Sleep(300);            
            PreencherCampo(m_campoLogin, usuario);
            PreencherCampo(m_campoSenha, senha);
            ClicarElementoPagina(btnEntrar);
            AguardarElemento(btnSair);
        }

        #endregion
    }
}
