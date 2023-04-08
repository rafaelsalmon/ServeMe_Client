using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirvaMe.ViewModels;
using System.Threading.Tasks;
using SirvaMe.Models;

namespace SirvaMe.Test
{
    [TestClass]
    public class Testes
    {
        [TestMethod]
        public void TestMethod1()
        {
            var nome = "Edgar Jastre";
            var nomes = nome.Split();
            nome = String.Format("{0} {1}.", nomes[0], nomes[1].Substring(0,1));

            Assert.IsNotNull(nome);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var end = new Endereco
            {
                Logradouro = " teste ",
                Numero = "123"
            };

            var teste = end.RetornaEnderecoCompleto();

            //var servicosVm = new ServicosVM();

            //Assert.IsNotNull(servicosVm.Servicos, "Erro");
            //Assert.IsTrue(servicosVm.Servicos.Count > 0, "Erro");
        }
    }
}
