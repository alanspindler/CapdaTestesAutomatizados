using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lampp.CAPDA.Teste.Automatizado.Principal.PageObjects;
using Lampp.CAPDA.Teste.Automatizado.SharedObjects;
using Lampp.CAPDA.Teste.Automatizado.Login.PageObjects;

namespace Lampp.CAPDA.Teste.Automatizado.Principal.Tests
{
    [TestClass]
    public class PaginaPrincipalTestesAutomatizados
    {
        private ItensTeste itens;

        private sealed class ItensTeste
        {
            public Global Global { get; set; }
            public PaginaPrincipal PaginaPrincipal { get; set; }
            public PaginaBase PaginaBase { get; set; }
            public PaginaInicial PaginaInicial { get; set; }
        }
    }
}
