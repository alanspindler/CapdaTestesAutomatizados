﻿using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;


namespace Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects
{
    /// <summary>
    /// Página que contém funções de chamada de menu que contem em todas as páginas
    /// </summary>
    /// <remarks>Escrita por Alan Spindler em 26/11/2015</remarks>
    public class PaginaPrincipal : PaginaBase
    {
        #region Declaração de variáveis privadas da classe        

        #endregion

        #region Declaração de variáveis públicas da classe        

        //public By MenuCredenciamentoLocal = By.LinkText("CAPDA");
        public By MenuCredenciamentoLocal = By.XPath("/html/body/app-root/app-layout/section/section/section/aside/section/section/app-menu/div/nav/div[3]/li/a");        
        public By MenuAcompanharCredenciamentoLocal = By.XPath("//ul[@id='2']/li/a/span");

        public By MenuCredenciamentoServidorDes = By.XPath("//aside[@id='nav']/section/section/app-menu/div/nav/div[4]/li/a");
        public By MenuAcompanharCredenciamentoServidorDes = By.XPath("//ul[@id='760']/li/a/span");
        #endregion

        #region Métodos públicos

        
        /// <summary>
        /// Clica em um item do menu e expandi o submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        public void ExpandireAbrirMenuCredenciamento(bool expandirMenu)
        {
            if (Constantes.TesteSistemalocal)
            {
                ExpandireAbrirMenu(expandirMenu, MenuCredenciamentoLocal, MenuAcompanharCredenciamentoLocal);
            }
            else
            {
                ExpandireAbrirMenu(expandirMenu, MenuCredenciamentoServidorDes, MenuAcompanharCredenciamentoServidorDes);
            }
        }       

        /// <summary>
        /// Clica em um item do menu e expandi o submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        public void ExpandireAbrirMenu(bool expandirMenu, By menu, By itemSubMenu)
        {
            //AguardarElemento(driver, "logout", TipoDadoElemento.Id);

            if (expandirMenu)
            {
                AguardarProcessando(driver);
                AguardarElemento(driver, menu);
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
            ClicarElementoPagina(driver, itemSubMenu);
        }

        /// <summary>
        /// Clicar no item do submenu
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 30/03/2016</remarks>
        private void AbrirPaginaListagem(By itemSubMenu)
        {
            AguardarProcessando(driver);
            driver.FindElement(itemSubMenu).Click();                        
        }

        #endregion
    }
}
