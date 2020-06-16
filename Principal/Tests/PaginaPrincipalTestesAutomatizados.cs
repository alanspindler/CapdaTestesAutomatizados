using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTIS.SIMNAC.Teste.Automatizado.Principal.PageObjects;
using CTIS.SIMNAC.Teste.Automatizado.SharedObjects;
using CTIS.SIMNAC.Teste.Automatizado.Login.PageObjects;

namespace CTIS.SIMNAC.Teste.Automatizado.Principal.Tests
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
