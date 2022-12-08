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
        public By botaoLogin = By.XPath("//span[.='Login']");
        
        public By campoLoginServidor = By.Id("username");
        public By campoSenhaServidor = By.Id("password");
        public By botaoEfetuarLoginServidor = By.Name("efetuar-login");      


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
        /// Abre a página de Login
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void AbrirPagina(WebDriver driver, string URL)
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
        public void FazerLogin(WebDriver driver, string usuario, string senha)
        {
            Thread.Sleep(300);            
            PreencherCampo(driver, m_campoLogin, usuario);
            PreencherCampo(driver,m_campoSenha, senha);
            ClicarElementoPagina(driver, btnEntrar);
            AguardarElemento(driver, btnSair);
        }

        public void FazerLoginServidor(WebDriver driver, string usuario, string senha)
        {
            Thread.Sleep(300);
            AguardarElemento(driver, campoLoginServidor);
            PreencherCampo(driver, campoLoginServidor, usuario);
            DiminuirZoomParaResolucoesPequenas();
            PreencherCampo(driver, campoSenhaServidor, senha);            
            ClicarElementoPagina(driver, botaoEfetuarLoginServidor);
            AguardarElemento(driver, btnSair);
        }


        #endregion
    }
}
