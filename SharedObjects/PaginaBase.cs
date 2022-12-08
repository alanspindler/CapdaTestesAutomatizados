using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lampp.CAPDA.Teste.Automatizado.SharedObjects
{
    /// <summary>
    /// Página que contém os elementos e métodos genéricos de todas as páginas
    /// </summary>
    /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
    public class PaginaBase
    {
        #region Declaração de variáveis públicas da classe --------------------------------------------------------------------------
        public RemoteWebDriver driver;
        //Páginas de cadastros
        public By BotaoSalvar { get; set; } = By.Id("btnSalvar");
        public By BotaoCancelar { get; set; } = By.Id("btnCancelar");
        public By BotaoAtualizar { get; set; } = By.Id("btnAtualizar");

        //Páginas de listagem dos dados cadastrados
        public By BotaoIncluiBase { get; set; } = By.Id("btnIncluir");
        public By BotaoExcluir { get; set; } = By.Id("btnRemover");
        public By CampoBusca { get; set; } = By.Id("pesquisar");
        public By CheckSelecionarTudo { get; set; } = By.CssSelector("input[type=\"checkbox\"]");
        public By ErroExcluirCadastro = By.CssSelector(".erro li");
        public By DivProcessando = By.Id("loading");
        //public By LinhasTabela = By.XPath("tr");
        public By LinhasTabela = By.XPath("//tr");
        public By Relatorio = By.Id("plugin");
        public By ConfirmacaoLogout = By.XPath("//div[@id='msg']/h2");

        public enum TipoDadoElemento
        {
            Id = 0,
            Xpath = 1,
            CssSelector = 2,
            TagName = 3
        }
        #endregion     

        #region Declaração de variáveis protected da classe -------------------------------------------------------------------------
        protected WebDriverWait wait;
        #endregion

        #region Declaração de variáveis privadas da classe --------------------------------------------------------------------------
        private readonly By m_botaoLogout = By.XPath("//span");
        public static string WeHighlightedColour = "arguments[0].style.border='5px solid red'";

        #endregion

        #region Métodos públicos ----------------------------------------------------------------------------------------------------       


        /// <summary>
        /// Clica no botão logout
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void FazerLogout(WebDriver driver)
        {
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, m_botaoLogout);
            AguardarElemento(driver, ConfirmacaoLogout);

        }

        //Destaca o elemento em uma cor
        public static object DestacarElemento(IWebDriver driver, By elemento)
        {
            var myLocator = driver.FindElement(elemento);
            var js = (IJavaScriptExecutor)driver;
            return js.ExecuteScript(WeHighlightedColour, myLocator);
        }

        /// <summary>
        /// Aguarda elemento da página ser exibido  
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public bool AguardarElemento(WebDriver driver, By elemento)
        {
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elemento));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerificarMensagemRetorno(By elemento)
        {
            var texto = driver.FindElement(elemento).GetAttribute("textContent");
            if (texto == "Operação realizada com sucesso!")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Aguarda a div "Processando" ser exibida e desaparecer antes de efetuar passos do teste
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 18/04/2016</remarks>
        public void AguardarProcessando(WebDriver driver)
        {
            ////Verifica se Processando está sendo exibido
            var divSendoExibida = false;

            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(500));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(DivProcessando));
                divSendoExibida = true;
            }
            catch
            {
            }

            ////Enquanto Processando estiver sendo exibido, fica testando para ver se já sumiu
            while (divSendoExibida)
            {
                try
                {
                    wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(300));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(DivProcessando));
                }
                catch
                {
                    divSendoExibida = false;
                }
            }
            Thread.Sleep(500);
        }

        public string RetornaTextoElemento(WebDriver driver, By elemento)
        {
            DestacarElemento(driver, elemento);
            var textoElemento = driver.FindElement(elemento).Text;
            return textoElemento;
        }

        public void arrastarElementoparaElemento(WebDriver driver, By elementoMovido, By elementoDestino)
        {
            new Actions(driver)
            .DragAndDrop(driver.FindElement(elementoMovido), driver.FindElement(elementoDestino))
            .Perform();
        }

        public void arrastarElementoparaPosicao(WebDriver driver, By elementoMovido, int x, int y)
        {
            new Actions(driver)
            .DragAndDropToOffset(driver.FindElement(elementoMovido), x, y)
            .Perform();
        }


        //Valida texto de elementos, como botões e afins.
        public void ValidarTextoElemento(WebDriver driver, string texto, By element)
        {
            var descricaoElemento = driver.FindElement(element).GetAttribute("textContent");

            Assert.AreEqual(texto.Trim().ToLower(), descricaoElemento.Trim().ToLower(), "Texto Inválido! descricao esperado: " + texto + " valor informado: " + descricaoElemento);
        }

        public void ZoomIn(RemoteWebDriver driver)
        {
            new Actions(driver)
                .SendKeys(Keys.Control).SendKeys(Keys.Add)
                .Perform();
        }

        public void ZoomOut(RemoteWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.body.style.zoom='90%'");
        }

        public void DiminuirZoomParaResolucoesPequenas()
        {
            var ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            var ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            if (ScreenHeight < 900)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                //js.ExecuteScript("document.body.style.zoom='90%'");                
                js.ExecuteScript("document.body.style.zoom = '0.8'");

            }
        }


        //// Aguarda carregar a página totalmente
        public void AguardarCarregarPagina(WebDriver driver)
        {
            try
            {
                Thread.Sleep(8000);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(150);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Aguarda o elemento carregar algum valor.
        /// Ex.: Utilizado para esperar o valor do atributo value de um campo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 04/04/2016</remarks>
        public void AguardarValorElemento(WebDriver driver, By elemento)
        {
            var waitUntil = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            waitUntil.Until(d => !driver.FindElement(elemento).GetAttribute("value").Equals(""));
        }

        /// <summary>
        /// Aguarda o texto da página ser exibido  
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/02/2016</remarks>
        public void AguardarTexto(WebDriver driver, By elemento, string texto)
        {
            AguardarTexto(driver, elemento, texto, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Aguarda o texto da página ser exibido  
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/02/2016</remarks>
        public void AguardarTexto(WebDriver driver, By elemento, string texto, TimeSpan timeout)
        {
            wait = new WebDriverWait(driver, timeout);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(elemento, texto));
        }

        /// <summary>
        /// Recebe a descricao e o tipo do elemento a ser buscado. 
        /// Exemplo: <button type="button" id="btnIncluir"></button>
        /// Os dados são passados da seguinte maneira:
        /// AguardarElemento(driver, "btnIncluir", PaginaBase.TipoDadoElemento.Id);
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void AguardarElemento(WebDriver driver, string texto, TipoDadoElemento tipoDado)
        {
            switch (tipoDado)
            {
                case TipoDadoElemento.Id:
                    AguardarElemento(driver, By.Id(texto));

                    break;
                case TipoDadoElemento.Xpath:
                    AguardarElemento(driver, By.XPath(texto));

                    break;
                case TipoDadoElemento.CssSelector:
                    AguardarElemento(driver, By.CssSelector(texto));

                    break;
                case TipoDadoElemento.TagName:
                    AguardarElemento(driver, By.TagName(texto));

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Verifica o texto de um elemento
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 04/03/2016</remarks>
        public void ValidarTexto(string texto, By campo, bool aguardarTexto = false)
        {
            AguardarElemento(driver, campo);
            var descricaoElemento = driver.FindElement(campo).Text;
            DestacarElemento(driver, campo);

            if (aguardarTexto)
            {
                string.IsNullOrEmpty(descricaoElemento);

                while (string.IsNullOrEmpty(descricaoElemento))
                {
                    Thread.Sleep(50);
                    descricaoElemento = driver.FindElement(campo).Text;
                }
            }

            Assert.AreEqual(texto, descricaoElemento, "Texto Inválido! descricao esperado: " + texto + " valor informado: " + descricaoElemento);
        }

        /// <summary>
        /// Verifica o texto de um elemento. Utilizado apenas para se trabalhar com batidas
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 04/03/2016</remarks>
        public void ValidarTextoBatidas(string texto, By campo, bool aguardarTexto = false)
        {
            var descricaoElemento = driver.FindElement(campo).Text;
            descricaoElemento = descricaoElemento.Replace("*", "").Replace("^", "").Replace("¨", "").Replace("!", "").Replace("\r\n", "");

            if (aguardarTexto)
            {
                string.IsNullOrEmpty(descricaoElemento);

                while (string.IsNullOrEmpty(descricaoElemento))
                {
                    Thread.Sleep(50);
                    descricaoElemento = driver.FindElement(campo).Text;
                    descricaoElemento = descricaoElemento.Replace("*", "").Replace("^", "").Replace("¨", "").Replace("!", "").Replace("\r\n", "");
                }
            }

            Assert.AreEqual(texto, descricaoElemento, "Texto Inválido! descricao esperado: " + texto + " valor informado: " + descricaoElemento);
        }

        /// <summary>
        /// Verifica o texto de um elemento
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 04/03/2016</remarks>
        public void ValidarTexto(Regex regex, By campo)
        {
            AguardarElemento(driver, campo);
            AguardarProcessando(driver);
            var descricaoElemento = driver.FindElement(campo).Text;
            Assert.AreEqual(true, regex.IsMatch(descricaoElemento), "Texto Inválido! descricao esperado: " + regex + " valor informado: " + descricaoElemento);
        }


        /// <summary>
        /// Valida o valor do campo texto
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void ValidarValorCampo(string texto, By campo)
        {
            AguardarProcessando(driver);
            var valorElemento = driver.FindElement(campo).GetAttribute("value");
            Assert.AreEqual(texto, valorElemento, "Valor inválido! valor esperado: " + texto + " valor informado: " + valorElemento);
        }

        //Aguarda combo carrega um valor específico
        public void AguardarValorCombobox(By element, string texto)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(25000));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(element, texto));
        }

        /// <summary>
        /// Valida o valor do campo texto
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/01/2016</remarks>
        public void ValidarValorCampoDiferente(string texto, By campo)
        {
            AguardarProcessando(driver);
            var valorElemento = driver.FindElement(campo).GetAttribute("value");
            Assert.AreNotEqual(texto, valorElemento, "Valor inválido! valor esperado: " + texto + " valor informado: " + valorElemento);
        }

        /// <summary>
        /// Este método verifica o chama a verifição de visibilidade do elemento, 
        /// se for visívél ele fecha o calendário
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/01/2016</remarks>
        public void EsconderCalendario(By campo)
        {
            // Ao clicar duas vezes no campo com calendário, ele some
            driver.FindElement(campo).Click();
            driver.FindElement(campo).Click();
        }

        //Limpa o texto de um campo
        public void LimparCampo(WebDriver driver, By campo)
        {
            AguardarElemento(driver, campo);
            driver.FindElement(campo).Clear();
            InserirTeclaTab(driver, campo);
            AguardarProcessando(driver);
        }
        /// <summary>
        /// Preenche campo texto. Caso esteja vazio, apaga o conteúdo atual.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void PreencherCampo(WebDriver driver, By elemento, string TextoCampo)
        {
            DestacarElemento(driver, elemento);
            if (TextoCampo != "")
            {
                driver.FindElement(elemento).Clear();
                InserirTexto(driver, elemento, TextoCampo);
            }
            else
            {
                InserirTexto(driver, elemento, Keys.Control + "a");
                InserirTexto(driver, elemento, Keys.Delete);
                InserirTexto(driver, elemento, Keys.Tab);
            }
        }

        /// <summary>
        /// Preenche campo texto. Caso esteja vazio, apaga o conteúdo atual.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void PreencherCampoSemLimpar(WebDriver driver, By elemento, string TextoCampo)
        {
            DestacarElemento(driver, elemento);
            ClicarElementoPagina(driver, elemento);
            InserirTexto(driver, elemento, TextoCampo);
        }

        /// <summary>
        /// Preenche campo texto. É utilizado em combos, pois estas não funcionam com clear, por exemplo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public bool PreencherCampoCombo(WebDriver driver, By elemento, string TextoCampo)
        {
            if (!AguardarElemento(driver, elemento))
            {
                return false;
            }
            AguardarElemento(driver, elemento);
            InserirTexto(driver, elemento, TextoCampo);

            return true;
        }

        /// <summary>
        /// Valida a quantidade de linhas exibidas no grid
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void ValidarLinhasGrid(WebDriver driver, int valorEsperado)
        {
            AguardarProcessando(driver);
            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid(driver) - 1;
            if (quantidadeLinhasGridRetornada == -1)
            {
                quantidadeLinhasGridRetornada = 0;
            }
            Assert.AreEqual(valorEsperado, quantidadeLinhasGridRetornada, "Valor inválido! Números e linhas esperadas: " + valorEsperado + " linhas retornadas: " + quantidadeLinhasGridRetornada);
        }

        /// <summary>
        /// Valida a quantidade de linhas exibidas no grid
        /// Sobrescrita passando a tabela como parâmetro
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/10/2016</remarks>
        public void ValidarLinhasGrid(WebDriver driver, By tabela, int valorEsperado)
        {
            AguardarProcessando(driver);
            var quantidadeLinhas = ObterTotalLinhasTabela(driver, tabela) - 2;
            Assert.AreEqual(valorEsperado, quantidadeLinhas, "Valor inválido! Números de linhas esperadas: " + valorEsperado + " linhas retornadas: " + quantidadeLinhas);
        }

        /// <summary>
        /// Retorna a quantidade de linhas exibidas na tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public int RetornarQuantidadeLinhasGrid(WebDriver driver)
        {
            AguardarProcessando(driver);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(LinhasTabela));
            return driver.FindElements(LinhasTabela).Where(ele => ele.Displayed).Count();
        }

        /// <summary>
        /// Retorna a quantidade de linhas exibidas na tabela
        /// Sobrescrita passando a tabela como parâmetro
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/10/2016</remarks>
        public int RetornarQuantidadeLinhasGrid(By tabela)
        {
            AguardarProcessando(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabela));
            return driver.FindElements(tabela).Count;
        }

        /// <summary>
        /// Preenche o valor de um campo da tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/07/2016</remarks>
        public bool PreencherCampoTabela(WebDriver driver, By elemento, string textoCampo)
        {
            if (textoCampo != "")
            {
                // Aguarda o elemento da página,
                // caso não encontre o método retorna false
                // com base na exception do AguardarElemento(driver, )
                if (!AguardarElemento(driver, elemento))
                {
                    return false;
                }
                driver.FindElement(elemento).Clear();
                InserirTexto(driver, elemento, textoCampo);
            }
            else
            {
                InserirTexto(driver, elemento, Keys.Delete);
            }

            return true;
        }

        /// <summary>
        /// Chama o método para buscar um item realizado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void BuscarEValidarResultado(string texto)
        {
            AguardarElemento(driver, BotaoIncluiBase);
            BuscarDadosCadastrados(texto);
        }

        /// <summary>
        /// Realiza a pesquisa de itens cadastrados e depois valida o resultado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        private void BuscarDadosCadastrados(string texto)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(5000));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(BotaoIncluiBase));
            driver.FindElement(CampoBusca).SendKeys(texto);

            ValidarResultadoBusca(texto);
        }

        /// <summary>
        /// Valida o resultado da busca
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        private void ValidarResultadoBusca(string textoEsperado)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(LinhasTabela));

            var elemento = driver.FindElement(CampoBusca).GetAttribute("value");

            Assert.AreEqual(textoEsperado, elemento, "Resultado esperado: " + textoEsperado + " Resultado encontrado:" + elemento);
        }

        /// <summary>
        /// Clica em algum elemento da página, pode ser botões, campos listas
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 22/01/2016</remarks>
        public void ClicarElementoPagina(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            DestacarElemento(driver, elemento);
            driver.FindElement(elemento).Click();
        }

        /// <summary>
        /// Clica duas vezes (DoubleClick) em algum elemento da página, pode ser botões, campos listas
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 19/10/2016</remarks>
        public void ClicarDuploElementoPagina(WebDriver driver, By elemento)
        {
            AguardarProcessando(driver);
            AguardarElemento(driver, elemento);
            Actions action = new Actions(driver);
            action.DoubleClick(driver.FindElement(elemento)).DoubleClick();
            action.Build().Perform();
        }

        /// <summary>
        /// Insere texto em algum campo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 22/01/2016</remarks>
        public void InserirTexto(WebDriver driver, By elemento, string tecla)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(tecla);
        }

        /// <summary>
        /// Envia a tecla enter
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaEnter(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Envia a tecla tab
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaTab(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.Tab);
        }

        /// <summary>
        /// Envia a tecla delete
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaDelete(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.Delete);
        }

        /// <summary>
        /// Envia a tecla seta para cima
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaUp(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowUp);
        }

        /// <summary>
        /// Envia a tecla seta para baixo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaDown(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowDown);
        }

        /// <summary>
        /// Envia a tecla seta para cima
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaDireita(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowRight);
        }

        /// <summary>
        /// Clica com o botão direito
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void ClicarBotaoDireito(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            var action = new Actions(driver);
            var elementToClick = driver.FindElement(elemento);
            action.ContextClick(elementToClick);
            action.Perform();
        }

        /// <summary>
        /// Envia a tecla seta para cima
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaEsquerda(WebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowLeft);
        }

        /// <summary>
        /// Atualiza a página
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void AtualizarPagina(WebDriver driver)
        {
            driver.Navigate().Refresh();
            AguardarCarregarPagina(driver);
            AguardarProcessando(driver);

        }

        /// <summary>
        /// Marca ou desmarca checkbox
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 24/06/2016</remarks>
        public void MarcarCheckbox(WebDriver driver, By elementoValidar, By elementoClicar, bool informarValor)
        {
            AguardarElemento(driver, elementoValidar);
            DestacarElemento(driver, elementoClicar);
            var checkboxMarcado = driver.FindElement(elementoValidar).GetAttribute("checked");

            //Se deve MARCAR o checkbox, verifica se está DESMARCADO e clica. Se estiver MARCADO, não faz nada.
            if (informarValor)
            {
                if (checkboxMarcado != "true")
                {
                    driver.FindElement(elementoClicar).Click();
                }
            }
            //Se deve DESMARCAR o checkbox, verifica se está MARCADO e clica. Se estiver DESMARCADO, não faz nada.
            else
            {
                if (checkboxMarcado == "true")
                {
                    driver.FindElement(elementoClicar).Click();
                }
            }
        }

        /// <summary>
        /// Seleciona o item de uma combo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 27/06/2016</remarks>
        public void SelecionarItemCombo(WebDriver driver, By elemento, string texto)
        {
            AguardarElemento(driver, elemento);
            AguardarValorCombobox(elemento, texto);
            DestacarElemento(driver, elemento);
            var elementoSelecionado = driver.FindElement(elemento);
            var selectElement = new SelectElement(elementoSelecionado);
            selectElement.SelectByText(texto);
        }

        /// <summary>
        /// Verifica de o elemento está marcado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 03/03/2016</remarks>
        public void ValidarCheckboxouOption(WebDriver driver, By elemento, bool checkboxMarcado)
        {
            AguardarElemento(driver, elemento);
            var checkbox = driver.FindElement(elemento).Selected;

            if (checkboxMarcado)
            {
                Assert.AreEqual(true, checkbox, "Checkbox não está selecionado!");
            }
        }

        /// <summary>
        /// Verifica de o elemento está presente
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 03/03/2016</remarks>
        public void ValidarElementoPresente(WebDriver driver, By elemento)
        {
            Assert.AreEqual(true, IsElementDisplayed(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento é clicável
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 03/03/2016</remarks>
        public bool ValidarElementoClicavel(WebDriver driver, By elemento)
        {
            return (isElementClickable(driver, elemento));
        }

        public void AguardarElementoClicavel(WebDriver driver, By elemento)
        {
            bool pronto = ValidarElementoClicavel(driver, elemento);
            if (pronto)
            {
                return;
            }
            else
            {
                while (!pronto)
                {
                    Thread.Sleep(200);
                    pronto = ValidarElementoClicavel(driver, elemento);
                }
            }
        }

        /// <summary>
        /// Verifica de o elemento não é clicável
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 03/03/2016</remarks>
        public void ValidarElementoNaoClicavel(WebDriver driver, By elemento)
        {
            Assert.AreEqual(false, isElementClickable(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento dentro de uma tela especifica está presente
        /// </summary>
        /// <remarks>Escrita por Fernando Alex em 28/06/2016</remarks>
        public void ValidarElementoPresente(WebDriver driver, By parent, By elemento)
        {
            Assert.AreEqual(true, IsElementDisplayed(driver, parent, elemento));
        }

        /// <summary>
        /// Verifica de o elemento não está presente
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 28/06/2016</remarks>
        public void ValidarElementoNaoPresente(WebDriver driver, By elemento)
        {
            Assert.AreEqual(true, !IsElementDisplayed(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento dentro de uma tela especifica está presente
        /// </summary>
        /// <remarks>Escrita por Fernando Alex em 28/06/2016</remarks>
        public void ValidarElementoNaoPresente(WebDriver driver, By parent, By elemento)
        {
            Assert.AreEqual(true, !IsElementDisplayed(driver, parent, elemento));
        }

        /// <summary>
        /// Valida o texto selecionado de uma combo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 28/06/2016</remarks>
        public void ValidarTextoElementoSelecionadoCombo(WebDriver driver, string texto, By elemento)
        {
            var combobox = driver.FindElement(elemento);
            SelectElement selectedValue = new SelectElement(combobox);
            string textoexistente = selectedValue.SelectedOption.Text;
            Assert.AreEqual(texto.Trim().ToLower(), textoexistente.Trim().ToLower());
        }

        /// <summary>
        /// Método para chamar a exclusão das linhas de um grid
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 09/03/2016</remarks>
        public void ExcluirLinhasGrid(WebDriver driver)
        {
            ExcluirLinhasGrid(driver, confirmar: true);
        }

        /// <summary>
        /// Método para comparar propriedades de um elemento.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 09/03/2016</remarks>
        public static bool ChecarPropriedadesElemento(IWebDriver driver, By elemento, string propriedade, string valor)
        {
            return driver.FindElement(elemento).GetAttribute(propriedade).Equals(valor);
        }

        /// <summary>
        /// Exclui todas as linhas visíveis do grid de listagem da página
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 22/01/2016</remarks>
        public void ExcluirLinhasGrid(WebDriver driver, bool confirmar)
        {
            AguardarProcessando(driver);
            AguardarElemento(driver, BotaoExcluir);
            ClicarElementoPagina(driver, CheckSelecionarTudo);
            Thread.Sleep(100);
            ClicarElementoPagina(driver, BotaoExcluir);
            Thread.Sleep(100);
            if (confirmar)
            {
                Thread.Sleep(500);
                driver.SwitchTo().Alert().Accept();
            }
            else
            {
                Thread.Sleep(500);
                driver.SwitchTo().Alert().Dismiss();
                ClicarElementoPagina(driver, CheckSelecionarTudo);
            }
            AguardarProcessando(driver);
        }

        /// <summary>
        /// Exclui todas as linhas visíveis do grid de listagem da página
        /// Sobrescrita passando os parâmetros tabela, botão de exclusão e checkbox selecionar todos
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/10/2016</remarks>
        public void ExcluirLinhasGrid(WebDriver driver, By tabela, By botao, By checkbox, bool confirmar)
        {
            AguardarProcessando(driver);
            AguardarElemento(driver, botao);
            if (ObterTotalLinhasTabela(driver, tabela) > 1)
            {
                if (!driver.FindElement(checkbox).Selected)
                {
                    ClicarElementoPagina(driver, checkbox);
                }
                AguardarProcessando(driver);
                ClicarElementoPagina(driver, botao);
                AguardarProcessando(driver);
                if (confirmar)
                {
                    driver.SwitchTo().Alert().Accept();
                }
                else
                {
                    driver.SwitchTo().Alert().Dismiss();
                }
            }
        }

        /// <summary>
        /// Valida se existem itens no grid e caham a funcao ExcluirLinhasGrid para excluir
        /// as linhas e aceitar o alert (se necessário)
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 19/04/2016</remarks>
        public void ExcluirTodosItensGrid(WebDriver driver, bool confirmar)
        {
            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid(driver) - 1;
            while (quantidadeLinhasGridRetornada > 0)
            {
                if (confirmar)
                {
                    ExcluirLinhasGrid(driver, true);
                }
                else
                {
                    ExcluirLinhasGrid(driver, false);
                    break;
                }
                AguardarProcessando(driver);
                quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid(driver) - 1;
            }
        }

        /// <summary>
        /// Este método verifica se o elemento da página está visível
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/01/2016</remarks>
        public static bool IsElementDisplayed(IWebDriver driver, By element)
        {
            if (driver.FindElements(element).Count > 0)
            {
                return true;
                //return driver.FindElement(element).Displayed;
                //return driver.FindElement(element).

            }
            else
            {
                return false;
            }

        }

        //Verifica se elemento pode ser clicado no momento
        public static bool isElementClickable(IWebDriver driver, By element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void GravarArquivoTexto(string texto)
        {
            // Create a file to write to.            
            var caminhoArquivo = Path.Combine(Global.DIRETORIO_APLICACAO, "Credenciamentos.txt");
            using (StreamWriter sw = File.AppendText(caminhoArquivo))
            {
                sw.WriteLine(texto);
            }

        }


        /// <summary>
        /// Este método verifica se um texto está visível
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public static bool IsTextDisplayed(IWebDriver driver, By element, string text)
        {
            string TextoExibido = driver.FindElement(element).Text;
            return TextoExibido.Contains(text);
        }

        /// <summary>
        /// Este método verifica se um elemento da página, dentro de outro elemento, não está visível
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 28/06/2016</remarks>
        public static bool IsElementDisplayed(IWebDriver driver, By parent, By element)
        {
            var parentElement = driver.FindElement(parent);

            if (parentElement.FindElements(element).Count > 0)
            {
                return parentElement.FindElement(element).Displayed;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Este método verifica se o elemento da página está habilitado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 02/06/2016</remarks>
        public static bool IsElementEnabled(IWebDriver driver, By element)
        {
            // O método Any() retorna 'true' se houver algum elemento na lista.
            if (driver.FindElements(element).Any())
            {
                return driver.FindElement(element).Enabled;
            }
            return false;
        }

        /// <summary>
        /// Este método verifica se o elemento da página está como readonly
        /// </summary>
        /// <remarks>Escrita por Fernando Alex em 27/06/2016</remarks>
        public static bool ElementHasClass(IWebDriver driver, By element, string className)
        {
            if (driver.FindElements(element).Any())
            {
                string classNames = driver.FindElement(element).GetAttribute("class");
                return classNames != null && classNames.Contains(className);
            }
            return false;
        }


        /// <summary>
        /// Este método verifica se o elemento da página está como readonly
        /// </summary>
        /// <remarks>Escrita por Fernando Alex em 27/06/2016</remarks>
        public static bool ElementHasAttributeReadOnlyOrDisabled(IWebDriver driver, By element)
        {
            if (driver.FindElements(element).Any())
            {
                string classNames = driver.FindElement(element).GetAttribute("readonly");
                if (classNames is null)
                {
                    classNames = driver.FindElement(element).GetAttribute("disabled");
                }
                if (classNames != null && classNames.Contains("true"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Este método verifica se o elemento da página está como readonly
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 02/06/2016</remarks>
        public static bool HasElementClassReadOnly(IWebDriver driver, By element)
        {
            return ElementHasClass(driver, element, "readonly");
        }

        /// <summary>
        /// Chama o método de excluir um item do grid
        /// sobrecarga do método ExcluirLinhasGrid()
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/01/2016</remarks>
        public void ExcluirUmaLinhaGrid()
        {
            ExcluirUmaLinhaGrid(confirmar: true, indice: 1);
        }

        /// <summary>
        /// Exclui apenas um item exibido no grid
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/01/2016</remarks>
        public void ExcluirUmaLinhaGrid(bool confirmar, int indice)
        {
            AguardarProcessando(driver);
            AguardarProcessando(driver);
            ClicarElementoPagina(driver, CriarByCheckbox(indice));
            ClicarElementoPagina(driver, BotaoExcluir);

            if (confirmar)
            {
                driver.SwitchTo().Alert().Accept();
            }
            else
            {
                driver.SwitchTo().Alert().Dismiss();
            }
        }



        /// <summary>
        /// Valida campo está habilitado e gera erro caso o resultado não seja o esperado.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 02/06/2016</remarks>
        public void ValidarCampoHabilitado(WebDriver driver, By campo)
        {
            var estaHabilitado = IsElementEnabled(driver, campo);
            Assert.IsTrue(true == estaHabilitado, "Resultado incorreto");
        }

        /// <summary>
        /// Valida campo está habilitado e gera erro caso o resultado não seja o esperado.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 02/06/2016</remarks>
        public void ValidarCampoDesabilitado(WebDriver driver, By campo)
        {
            var estaHabilitado = IsElementEnabled(driver, campo);
            Assert.IsTrue(false == estaHabilitado, "Resultado incorreto");
        }

        /// <summary>
        /// Retorna posição do checkbox para ser marcado.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public By CriarByCheckbox(int indice)
        {
            return By.XPath($"//tr[{indice}]/td/input");
        }


        /// <summary>
        /// Gera uma data aleatória entre duas datas no formato indicado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 21/11/2016</remarks>
        public string GerarDataAleatoria(string dataInicio, string dataFim, string formatoSaida)
        {
            Random aleatorio = new Random();
            DateTime dtIni = DateTime.Parse(dataInicio);
            DateTime dtFim = DateTime.Parse(dataFim);
            int faixa = (dtFim - dtIni).Days;
            return dtIni.AddDays(aleatorio.Next(faixa)).ToString(formatoSaida);
        }

        /// <summary>
        /// Gera uma data aleatória entre duas datas no formato indicado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 21/11/2016</remarks>
        public string GerarDataAtual()
        {
            DateTime date = DateTime.Now;
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Gera CPF aleatório.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public string GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        /// <summary>
        /// Retorna o número da coluna do texto informado
        /// Retorna 0 quando não encontra o texto.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/01/2017</remarks>
        public int ObterColunaTabela(WebDriver driver, By tabela, string texto)
        {
            var driverTabela = driver.FindElement(tabela);
            var colunasTabela = driverTabela.FindElements(By.CssSelector("th")).Count;

            for (int coluna = 1; coluna < colunasTabela; coluna++)
            {
                var linhaColuna = By.CssSelector("thead > tr > th:nth-child(" + coluna + ")");
                var textoColuna = driver.FindElement(linhaColuna).Text;

                if (textoColuna == texto)
                {
                    return coluna;
                }

            }
            return 0;
        }


        /// <summary>
        /// Retorna o número da linha da tabela que contém o texto na coluna.
        /// Retorna 0 quando não encontra o texto.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/10/2016</remarks>
        public int ObterLinhaTabela(WebDriver driver, By tabela, string texto, int coluna)
        {
            var driverTabela = driver.FindElement(tabela);
            var linhasTabela = driverTabela.FindElements(By.CssSelector("tbody > tr")).Count;

            for (int linha = 1; linha < linhasTabela; linha++)
            {
                var linhaColuna = By.CssSelector("tbody > tr:nth-child(" + linha + ") > td:nth-child(" + coluna + ")");
                var textoColuna = driver.FindElement(linhaColuna).Text;

                if (textoColuna.Contains(texto))
                {
                    return linha;
                }
            }
            return 0;
        }

        /// <summary>
        /// Retorna o número da linha da tabela que contém o texto.
        /// Pesquisa em todas a linhas e colunas, inclusive cabeçalho.
        /// Retorna 0 quando não encontra o texto.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 13/10/2016</remarks>
        public int ObterLinhaTabela(WebDriver driver, By tabela, string texto)
        {
            var linha = 1;
            var linhas = driver.FindElement(tabela).FindElements(By.TagName("tr"));
            foreach (var dados in linhas)
            {
                if (dados.Text.Contains(texto))
                {
                    return linha;
                }
                linha++;
            }
            return 0;
        }

        /// <summary>
        /// Retorna o total de linhas da tabela.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/10/2016</remarks>
        public int ObterTotalLinhasTabela(WebDriver driver, By tabela)
        {
            return driver.FindElement(tabela).FindElements(By.TagName("tr")).Count;
        }

        /// <summary>
        /// Retorna a celula da tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public By ObterCelula(WebDriver driver, int linha, int tipoDado)
        {
            var item = "table > tbody > tr:nth-child(" + linha + ") > td:nth-child(" + tipoDado + ")";
            var celula = By.CssSelector(item);
            ClicarElementoPagina(driver, celula);
            return celula;
        }

        /// <summary>
        /// Retorna a celula do header da tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public By ObterCelulaHeader(WebDriver driver, int tipoDado)
        {
            var itemTabela = "table > thead > tr > th:nth-child(" + tipoDado + ")";
            var celula = By.CssSelector(itemTabela);
            ClicarElementoPagina(driver, celula);
            return celula;
        }

        /// <summary>
        /// Valida se campo está como somente leitura
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public void ValidarCampoSomenteLeitura(WebDriver driver, int linha, int tipo)
        {
            var Item = ObterCelula(driver, linha, tipo);
            bool rdonly = HasElementClassReadOnly(driver, Item);
            Assert.IsTrue(rdonly);
        }

        /// <summary>
        /// Valida se campo está como somente leitura
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public void ValidarCampoSomenteLeitura(WebDriver driver, By elemento)
        {
            bool rdonly = ElementHasAttributeReadOnlyOrDisabled(driver, elemento);
            Assert.IsTrue(rdonly);
        }

        /// <summary>
        /// Validar mensagem de campo obrigatório
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/10/2016</remarks>
        public void ValidarCampoObrigatorio(WebDriver driver, By elemento)
        {
            AguardarProcessando(driver);
            AguardarElemento(driver, elemento);
            ValidarTexto("Campo obrigatório.", elemento);
        }

        /// <summary>
        /// Vai na aba do relatório, aguarda carregar e verifica se todos os textos estão contidos no corpo do relatório.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 01/11/2016</remarks>
        public bool ValidarTextoRelatorio(WebDriver driver, params string[] textos)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Relatorio));
            Thread.Sleep(3000);

            var elemento = driver.FindElement(Relatorio);
            elemento.Click();
            elemento.SendKeys(Keys.Control + "a");
            elemento.SendKeys(Keys.Control + "c");
            Thread.Sleep(1000);

            var areaTransferencia = System.Windows.Forms.Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);

            return textos.All(texto => areaTransferencia.Contains(texto));
        }

        /// <summary>
        /// Verifica se tem janela de Alert na tela
        /// Se "fechar" vier com "true" então também já fecha a janela Alert
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/12/2016</remarks>
        public bool ValidarAlert(WebDriver driver, bool fechar = false)
        {
            try
            {
                if (fechar)
                {
                    driver.SwitchTo().Alert().Accept();
                }
                else
                {
                    driver.SwitchTo().Alert();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Move mouse sobre elemento (Mouse Hover)
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/12/2016</remarks>
        public void MouseSobre(RemoteWebDriver driver, By elemento)
        {
            AguardarElemento(driver, elemento);
            var action = new Actions(driver);
            var elementoMouseSobre = driver.FindElement(elemento);
            action.MoveToElement(elementoMouseSobre);
            action.Perform();
        }

        /// <summary>
        /// Valida imagem. Testa se a imagem está em branco ou preenchida.
        /// "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" é o valor padrão (em branco)
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void ValidarValorImagem(WebDriver driver, string imagem, By elemento, bool imagemEmBranco = false)
        {
            AguardarProcessando(driver);
            if (!imagemEmBranco)
            {
                Assert.AreEqual(imagem, driver.FindElement(elemento).GetAttribute("src"));
                //Assert.AreNotEqual(valorPadraoImagemEmBranco, driver.FindElement(elemento).GetAttribute("src"));
            }
            else
            {
                Assert.AreEqual(imagem, driver.FindElement(elemento).GetAttribute("src"));
            }
        }

        #endregion
    }
}
