using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Diagnostics;

namespace Lampp.CAPDA.Teste.Automatizado.SharedObjects
{
    /// <summary>
    /// Classe para armazenar métodos utilizados pelo projeto inteiro o projeto.
    /// </summary>
    /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
    public class Global
    {
        #region Declaração de variáveis privadas da classe

        private static Global instancia;
        private static string diretorioAplicacao = string.Empty;
        private readonly string m_data = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");

        #endregion

        #region Declaração de variáveis públicas da classe

        public RemoteWebDriver driver;      

        //Essa função é necessária pois testes ordenados geram erro em relação ao caminho do driver.
        public static string DIRETORIO_APLICACAO
        {
            get
            {
                if (string.IsNullOrEmpty(diretorioAplicacao))
                {
                    if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "geckodriver.exe")) || (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "chromedriver.exe"
                        )) || (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "IEDriverServer.exe")))))
                        
                    {
                        diretorioAplicacao = Directory.GetCurrentDirectory();
                    }
                    else
                    {
                        // Alan - 04/11/2016: Quando uma lista de 
                        // pre-requisitos é executada, o caminho do assembly
                        // está na pasta /Out/ do projeto. Deve redirecionar
                        // para a /bin/Debug/, que é onde o driver do chrome
                        // se encontra.
                        diretorioAplicacao = Path.GetFullPath(@"..\..\..\bin\Debug");
                    }
                }
                return diretorioAplicacao;
            }
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Inicia driver do Selenium, o navegador selecionado e maximiza a tela.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        private Global()
        {
            //encerrarOutrasInstanciasDriver();
            //TestarNoChrome();            
            TestarNoFirefox();
            //AguardarTeste();
        }

        public static Global obterInstancia()
        {
            if (instancia == null)
            {
                instancia = new Global();
            }
            return instancia;
        }

        private void encerrarOutrasInstanciasDriver()
        {
            foreach (var processo in Process.GetProcessesByName("geckodriver"))
            {
                processo.Kill();
            }
            foreach (var processo in Process.GetProcessesByName("chromedriver"))
            {
                processo.Kill();
            }
        }

        /// <summary>
        /// Fecha o Navegador e encerra o driver do Selenium
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void EncerrarTeste()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
                instancia = null;
            }
        }

        /// <summary>
        /// Tira um screenshot da tela e salva no caminho da variavel nomeArquivo
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 23/11/2015</remarks>
        public void TirarScreenshot(string nomePasta, string nomeTela)
        {
            var nomeArquivo = CriarPasta("TesteScreenShots") + $"\\{nomePasta}_{nomeTela}_{m_data}.png";

            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(nomeArquivo, ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// Grava log da tela e salva no caminho da variavel nomeArquivo
        /// </summary>
        /// <remarks>Escrita por Alan/Alan Spindler em 16/11/2016</remarks>
        public void GravarLog(string nomeTela, string texto)
        {
            var nomeArquivo = CriarPasta("TesteLogs") + $"\\{nomeTela}_{m_data}.txt";

            File.WriteAllText(nomeArquivo, texto);
        }

        /// <summary>
        /// Cria a pasta no caminho da variavel nomeArquivo se não existir
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 16/11/2016</remarks>
        public string CriarPasta(string nomePasta)
        {
            //Alan Spindler - 21/06/2016: Na primeira vez verifica se existe a pasta, senão cria
            var nomeArquivo = DIRETORIO_APLICACAO + $"\\{nomePasta}";
            if (!Directory.Exists(nomeArquivo))
            {
                Directory.CreateDirectory(nomeArquivo);
            }
            return nomeArquivo;
        }
        #endregion

        #region Métodos Privados

        /// <summary>
        /// Instancia o driver do Firefox e  maximiza a tela do navegador
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/03/2016</remarks>
        private void TestarNoFirefox()
        {            
            Environment.SetEnvironmentVariable("PATH", (DIRETORIO_APLICACAO));
            var options = new FirefoxOptions();
            options.AddAdditionalCapability("acceptInsecureCerts", true, true);
            driver = new FirefoxDriver(DIRETORIO_APLICACAO, options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Instancia o driver do Chrome e  maximiza a tela do navegador
        /// OBS.: Para funcionar a suite de teste do visual studio utilizando o drive do chrome é necessário passar o caminho do driver.
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/03/2016</remarks>
        private void TestarNoChrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddExtension(Constantes.CaminhoExtensao);
            options.Proxy = null;
            driver = new ChromeDriver(DIRETORIO_APLICACAO, options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //DiminuirZoomParaResolucoesPequenas();
            //driver.Manage().Timeouts().ImplicitWait(TimeSpan.FromSeconds(50));
        }

        /// <summary>
        /// Instancia o driver do ie e  maximiza a tela do navegador
        /// Para funcionar no IE, acessar o link e fazer as configurações de "Required Configurations"
        /// https://code.google.com/p/selenium/wiki/InternetExplorerDriver#Required_Configuration
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/03/2016</remarks>
        private void TestarNoIE()
        {
            Environment.SetEnvironmentVariable("PATH", (DIRETORIO_APLICACAO));
            driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Instancia o driver do Edge e  maximiza a tela do navegador
        /// </summary>
        /// <remarks>Escrita por Alan Spindler em 07/03/2016</remarks>
        private void TestarNoEdge()
        {
            Environment.SetEnvironmentVariable("PATH", (DIRETORIO_APLICACAO));
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
        }




        #endregion
    }

}
