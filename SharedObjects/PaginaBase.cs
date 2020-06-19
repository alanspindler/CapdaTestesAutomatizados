using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
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
        private readonly By m_botaoLogout = By.Id("logout");
        private By botaoCalendarioAberto = By.CssSelector("i.fa.fa-calendar-o");        
        public static string WeHighlightedColour = "arguments[0].style.border='5px solid red'";

        #endregion

        #region Métodos públicos ----------------------------------------------------------------------------------------------------

        /// <summary>
        ///  Inicializa o driver do Selenium
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public PaginaBase(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Clica no botão logout
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void FazerLogout()
        {
            ClicarElementoPagina(m_botaoLogout);
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
        public bool AguardarElemento(By elemento)
        {
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(12));
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
        public void AguardarProcessando()
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

        public void arrastarElementoparaElemento(By elementoMovido, By elementoDestino)
        {
            new Actions(driver)
            .DragAndDrop(driver.FindElement(elementoMovido), driver.FindElement(elementoDestino))
            .Perform();
        }

        public void arrastarElementoparaPosicao(By elementoMovido, int x, int y)
        {
            new Actions(driver)
            .DragAndDropToOffset(driver.FindElement(elementoMovido), x, y)
            .Perform();
        }


        //Valida texto de elementos, como botões e afins.
        public void ValidarTextoElemento(string texto, By element)
        {
            var descricaoElemento = driver.FindElement(element).GetAttribute("textContent");

            Assert.AreEqual(texto.Trim().ToLower(), descricaoElemento.Trim().ToLower(), "Texto Inválido! descricao esperado: " + texto + " valor informado: " + descricaoElemento);
        }

        public void ZoomIn()
        {
            new Actions(driver)
                .SendKeys(Keys.Control).SendKeys(Keys.Add)
                .Perform();
        }

        public void ZoomOut()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.body.style.zoom='90%'");
        }


        //// Aguarda carregar a página totalmente
        public void AguardarCarregarPagina()
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
        public void AguardarValorElemento(By elemento)
        {
            var waitUntil = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            waitUntil.Until(d => !driver.FindElement(elemento).GetAttribute("value").Equals(""));
        }

        /// <summary>
        /// Aguarda o texto da página ser exibido  
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/02/2016</remarks>
        public void AguardarTexto(By elemento, string texto)
        {
            AguardarTexto(elemento, texto, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Aguarda o texto da página ser exibido  
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/02/2016</remarks>
        public void AguardarTexto(By elemento, string texto, TimeSpan timeout)
        {
            wait = new WebDriverWait(driver, timeout);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(elemento, texto));
        }

        /// <summary>
        /// Recebe a descricao e o tipo do elemento a ser buscado. 
        /// Exemplo: <button type="button" id="btnIncluir"></button>
        /// Os dados são passados da seguinte maneira:
        /// AguardarElemento("btnIncluir", PaginaBase.TipoDadoElemento.Id);
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void AguardarElemento(string texto, TipoDadoElemento tipoDado)
        {
            switch (tipoDado)
            {
                case TipoDadoElemento.Id:
                    AguardarElemento(By.Id(texto));

                    break;
                case TipoDadoElemento.Xpath:
                    AguardarElemento(By.XPath(texto));

                    break;
                case TipoDadoElemento.CssSelector:
                    AguardarElemento(By.CssSelector(texto));

                    break;
                case TipoDadoElemento.TagName:
                    AguardarElemento(By.TagName(texto));

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
            AguardarElemento(campo);
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
            AguardarElemento(campo);
            AguardarProcessando();
            var descricaoElemento = driver.FindElement(campo).Text;
            Assert.AreEqual(true, regex.IsMatch(descricaoElemento), "Texto Inválido! descricao esperado: " + regex + " valor informado: " + descricaoElemento);
        }


        /// <summary>
        /// Valida o valor do campo texto
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void ValidarValorCampo(string texto, By campo)
        {
            AguardarProcessando();
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
            AguardarProcessando();
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
        public void LimparCampo(By campo)
        {
            AguardarElemento(campo);
            driver.FindElement(campo).Clear();
            InserirTeclaTab(campo);
            AguardarProcessando();
        }
        /// <summary>
        /// Preenche campo texto. Caso esteja vazio, apaga o conteúdo atual.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public bool PreencherCampo(By elemento, string TextoCampo)
        {
            DestacarElemento(driver, elemento);
            if (TextoCampo != "")
            {
                /*
                    Aguarda o elemento da página, 
                    caso não encontre o método retorna false 
                    com base na exception do AguardarElemento() 
                 */
                if (!AguardarElemento(elemento))
                {
                    return false;
                }
                driver.FindElement(elemento).Clear();
                InserirTexto(elemento, TextoCampo);
            }
            else
            {
                InserirTexto(elemento, Keys.Control + "a");
                InserirTexto(elemento, Keys.Delete);
                InserirTexto(elemento, Keys.Tab);
            }

            return true;
        }

        /// <summary>
        /// Preenche campo texto. Caso esteja vazio, apaga o conteúdo atual.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void PreencherCampoSemLimpar(By elemento, string TextoCampo)
        {
            DestacarElemento(driver, elemento);                
            InserirTexto(elemento, TextoCampo);
        }

        /// <summary>
        /// Preenche campo texto. É utilizado em combos, pois estas não funcionam com clear, por exemplo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public bool PreencherCampoCombo(By elemento, string TextoCampo)
        {
            if (!AguardarElemento(elemento))
            {
                return false;
            }
            AguardarElemento(elemento);
            InserirTexto(elemento, TextoCampo);

            return true;
        }

        /// <summary>
        /// Valida a quantidade de linhas exibidas no grid
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void ValidarLinhasGrid(int valorEsperado)
        {
            AguardarProcessando();
            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 1;
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
        public void ValidarLinhasGrid(By tabela, int valorEsperado)
        {
            AguardarProcessando();
            var quantidadeLinhas = ObterTotalLinhasTabela(tabela) - 2;
            Assert.AreEqual(valorEsperado, quantidadeLinhas, "Valor inválido! Números de linhas esperadas: " + valorEsperado + " linhas retornadas: " + quantidadeLinhas);
        }

        /// <summary>
        /// Retorna a quantidade de linhas exibidas na tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public int RetornarQuantidadeLinhasGrid()
        {
            AguardarProcessando();
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
            AguardarProcessando();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabela));
            return driver.FindElements(tabela).Count;
        }

        /// <summary>
        /// Preenche o valor de um campo da tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 11/07/2016</remarks>
        public bool PreencherCampoTabela(By elemento, string textoCampo)
        {
            if (textoCampo != "")
            {
                // Aguarda o elemento da página,
                // caso não encontre o método retorna false
                // com base na exception do AguardarElemento()
                if (!AguardarElemento(elemento))
                {
                    return false;
                }
                driver.FindElement(elemento).Clear();
                InserirTexto(elemento, textoCampo);
            }
            else
            {
                InserirTexto(elemento, Keys.Delete);
            }

            return true;
        }

        /// <summary>
        /// Chama o método para buscar um item realizado
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void BuscarEValidarResultado(string texto)
        {
            AguardarElemento(BotaoIncluiBase);
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
        public void ClicarElementoPagina(By elemento)
        {
            AguardarElemento(elemento);
            DestacarElemento(driver, elemento);
            driver.FindElement(elemento).Click();
        }

        /// <summary>
        /// Clica duas vezes (DoubleClick) em algum elemento da página, pode ser botões, campos listas
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 19/10/2016</remarks>
        public void ClicarDuploElementoPagina(By elemento)
        {
            AguardarProcessando();
            AguardarElemento(elemento);
            Actions action = new Actions(driver);
            action.DoubleClick(driver.FindElement(elemento)).DoubleClick();
            action.Build().Perform();
        }

        /// <summary>
        /// Insere texto em algum campo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 22/01/2016</remarks>
        public void InserirTexto(By elemento, string tecla)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(tecla);
        }

        /// <summary>
        /// Envia a tecla enter
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaEnter(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Envia a tecla tab
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaTab(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.Tab);
        }

        /// <summary>
        /// Envia a tecla delete
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaDelete(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.Delete);
        }

        /// <summary>
        /// Envia a tecla seta para cima
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaUp(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowUp);
        }

        /// <summary>
        /// Envia a tecla seta para baixo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaDown(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowDown);
        }

        /// <summary>
        /// Envia a tecla seta para cima
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaDireita(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowRight);
        }

        /// <summary>
        /// Clica com o botão direito
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void ClicarBotaoDireito(By elemento)
        {
            AguardarElemento(elemento);
            var action = new Actions(driver);
            var elementToClick = driver.FindElement(elemento);
            action.ContextClick(elementToClick);
            action.Perform();
        }

        /// <summary>
        /// Envia a tecla seta para cima
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 29/07/2016</remarks>
        public void InserirTeclaEsquerda(By elemento)
        {
            AguardarElemento(elemento);
            driver.FindElement(elemento).SendKeys(Keys.ArrowLeft);
        }

        /// <summary>
        /// Atualiza a página
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 12/01/2016</remarks>
        public void AtualizarPagina()
        {
            driver.Navigate().Refresh();
            AguardarCarregarPagina();
            AguardarProcessando();

        }

        /// <summary>
        /// Marca ou desmarca checkbox
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 24/06/2016</remarks>
        public void MarcarCheckbox(By elementoValidar, By elementoClicar, bool informarValor)
        {
            AguardarElemento(elementoValidar);
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
        public void SelecionarItemCombo(By elemento, string texto)
        {
            AguardarElemento(elemento);
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
        public void ValidarCheckboxouOption(By elemento, bool checkboxMarcado)
        {
            AguardarElemento(elemento);
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
        public void ValidarElementoPresente(By elemento)
        {
            Assert.AreEqual(true, IsElementDisplayed(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento é clicável
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 03/03/2016</remarks>
        public void ValidarElementoClicavel(By elemento)
        {
            Assert.AreEqual(true, isElementClickable(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento não é clicável
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 03/03/2016</remarks>
        public void ValidarElementoNaoClicavel(By elemento)
        {
            Assert.AreEqual(false, isElementClickable(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento dentro de uma tela especifica está presente
        /// </summary>
        /// <remarks>Escrita por Fernando Alex em 28/06/2016</remarks>
        public void ValidarElementoPresente(By parent, By elemento)
        {
            Assert.AreEqual(true, IsElementDisplayed(driver, parent, elemento));
        }

        /// <summary>
        /// Verifica de o elemento não está presente
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 28/06/2016</remarks>
        public void ValidarElementoNaoPresente(By elemento)
        {
            Assert.AreEqual(true, !IsElementDisplayed(driver, elemento));
        }

        /// <summary>
        /// Verifica de o elemento dentro de uma tela especifica está presente
        /// </summary>
        /// <remarks>Escrita por Fernando Alex em 28/06/2016</remarks>
        public void ValidarElementoNaoPresente(By parent, By elemento)
        {
            Assert.AreEqual(true, !IsElementDisplayed(driver, parent, elemento));
        }

        /// <summary>
        /// Valida o texto selecionado de uma combo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 28/06/2016</remarks>
        public void ValidarTextoElementoSelecionadoCombo(string texto, By elemento)
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
        public void ExcluirLinhasGrid()
        {
            ExcluirLinhasGrid(confirmar: true);
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
        public void ExcluirLinhasGrid(bool confirmar)
        {
            AguardarProcessando();
            AguardarElemento(BotaoExcluir);
            ClicarElementoPagina(CheckSelecionarTudo);
            Thread.Sleep(100);
            ClicarElementoPagina(BotaoExcluir);
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
                ClicarElementoPagina(CheckSelecionarTudo);
            }
            AguardarProcessando();
        }

        /// <summary>
        /// Exclui todas as linhas visíveis do grid de listagem da página
        /// Sobrescrita passando os parâmetros tabela, botão de exclusão e checkbox selecionar todos
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 25/10/2016</remarks>
        public void ExcluirLinhasGrid(By tabela, By botao, By checkbox, bool confirmar)
        {
            AguardarProcessando();
            AguardarElemento(botao);
            if (ObterTotalLinhasTabela(tabela) > 1)
            {
                if (!driver.FindElement(checkbox).Selected)
                {
                    ClicarElementoPagina(checkbox);
                }
                AguardarProcessando();
                ClicarElementoPagina(botao);
                AguardarProcessando();
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
        public void ExcluirTodosItensGrid(bool confirmar)
        {
            var quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 1;
            while (quantidadeLinhasGridRetornada > 0)
            {
                if (confirmar)
                {
                    ExcluirLinhasGrid(true);
                }
                else
                {
                    ExcluirLinhasGrid(false);
                    break;
                }
                AguardarProcessando();
                quantidadeLinhasGridRetornada = RetornarQuantidadeLinhasGrid() - 1;
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
            AguardarProcessando();
            AguardarProcessando();
            ClicarElementoPagina(CriarByCheckbox(indice));
            ClicarElementoPagina(BotaoExcluir);

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
        public void ValidarCampoHabilitado(By campo)
        {
            var estaHabilitado = IsElementEnabled(driver, campo);
            Assert.IsTrue(true == estaHabilitado, "Resultado incorreto");
        }

        /// <summary>
        /// Valida campo está habilitado e gera erro caso o resultado não seja o esperado.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 02/06/2016</remarks>
        public void ValidarCampoDesabilitado(By campo)
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
        public int ObterColunaTabela(By tabela, string texto)
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
        public int ObterLinhaTabela(By tabela, string texto, int coluna)
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
        public int ObterLinhaTabela(By tabela, string texto)
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
        public int ObterTotalLinhasTabela(By tabela)
        {
            return driver.FindElement(tabela).FindElements(By.TagName("tr")).Count;
        }

        /// <summary>
        /// Retorna a celula da tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public By ObterCelula(int linha, int tipoDado)
        {
            var item = "table > tbody > tr:nth-child(" + linha + ") > td:nth-child(" + tipoDado + ")";
            var celula = By.CssSelector(item);
            ClicarElementoPagina(celula);
            return celula;
        }

        /// <summary>
        /// Retorna a celula do header da tabela
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public By ObterCelulaHeader(int tipoDado)
        {
            var itemTabela = "table > thead > tr > th:nth-child(" + tipoDado + ")";
            var celula = By.CssSelector(itemTabela);
            ClicarElementoPagina(celula);
            return celula;
        }

        /// <summary>
        /// Valida se campo está como somente leitura
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public void ValidarCampoSomenteLeitura(int linha, int tipo)
        {
            var Item = ObterCelula(linha, tipo);
            bool rdonly = HasElementClassReadOnly(driver, Item);
            Assert.IsTrue(rdonly);
        }

        /// <summary>
        /// Valida se campo está como somente leitura
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 06/10/2016</remarks>
        public void ValidarCampoSomenteLeitura(By elemento)
        {
            bool rdonly = ElementHasAttributeReadOnlyOrDisabled(driver, elemento);
            Assert.IsTrue(rdonly);
        }

        /// <summary>
        /// Validar mensagem de campo obrigatório
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/10/2016</remarks>
        public void ValidarCampoObrigatorio(By elemento)
        {
            AguardarProcessando();
            AguardarElemento(elemento);
            ValidarTexto("Campo obrigatório.", elemento);
        }

        /// <summary>
        /// Vai na aba do relatório, aguarda carregar e verifica se todos os textos estão contidos no corpo do relatório.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 01/11/2016</remarks>
        public bool ValidarTextoRelatorio(params string[] textos)
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
        public bool ValidarAlert(bool fechar = false)
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
        public void MouseSobre(By elemento)
        {
            AguardarElemento(elemento);
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
        public void ValidarValorImagem(string imagem, By elemento, bool imagemEmBranco = false)
        {
            AguardarProcessando();
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
