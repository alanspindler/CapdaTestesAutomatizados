using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using CTIS.SIMNAC.Teste.Automatizado.Cadastros.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;


namespace CTIS.SIMNAC.Teste.Automatizado.Principal.PageObjects
{
    /// <summary>
    /// Página que contém funções de chamada de menu que contem em todas as páginas
    /// </summary>
    /// <remarks>Escrita por Alan Spindler em 26/11/2015</remarks>
    public class PaginaPrincipal : PaginaBase
    {
        #region Declaração de variáveis privadas da classe

        private By m_nomePresente = By.CssSelector("#info-usuario-dados p");

        #endregion

        #region Declaração de variáveis públicas da classe        

        public By MenuCadastros = By.LinkText("Cadastros");
        public By MenuCapacidadePerfil = By.LinkText("Capacidade por Perfil");
        
        public By MenuRelatorios = By.LinkText("Relatórios");        
        #endregion

        #region Métodos públicos

        /// <summary>
        /// Inicializa o driver e inicializa outras classes (para que seus objetos possam ser utilizados)
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public PaginaPrincipal(RemoteWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Clica em um item do menu e expandi o submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        public void ExpandireAbrirMenuCadastros(bool expandirMenu, By itemSubMenu)
        {
            ExpandireAbrirMenu(expandirMenu, MenuCadastros, itemSubMenu);
        }

        /// <summary>
        /// Clica em um item do menu e expandi o submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        public void ExpandireAbrirMenu(bool expandirMenu, By menu, By itemSubMenu)
        {
            AguardarElemento("logout", TipoDadoElemento.Id);

            if (expandirMenu)
            {
                AguardarProcessando();
                AguardarElemento(menu);
                driver.FindElement(menu).Click();
            }
            AbrirPaginaListagem(itemSubMenu);
        }


        /// <summary>
        /// Clica em um item do menu e expandi o submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        public void AbrirSubMenu(By itemSubMenu)
        {
            ClicarElementoPagina(itemSubMenu);
        }


        /// <summary>
        /// Valida o texto da tabela de status
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 05/12/2016</remarks>
        public void ValidarTabelaStatus(string texto, int linha, int coluna)
        {
            ValidarTexto(texto, ObterCelula(linha, coluna));
        }

        /// <summary>
        /// Clicar no item do submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        private void AbrirPaginaListagem(By itemSubMenu)
        {
            AguardarProcessando();
            driver.FindElement(itemSubMenu).Click();                        
        }



        #endregion
    }
}
