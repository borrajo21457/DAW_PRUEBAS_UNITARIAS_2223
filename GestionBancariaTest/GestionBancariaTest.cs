using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestionBancariaAppNS;
using System;

namespace GestionBancariaTest
    {
    [TestClass]
    public class GestionBancariaTest
        {
        [TestMethod]
        public void Reintegrovalido()
            {
            double Saldoinicial = 1000;
            double retirada = 250;
            double saldoEsperado = 750;

            GestionBancariaApp Apptest = new GestionBancariaApp(Saldoinicial);
            Apptest.RealizarReintegro(retirada);

            Assert.AreEqual(saldoEsperado, Apptest.ObtenerSaldo(), 0.0001, "Se produjo un error al realizar el reintegro, saldo" + "incorrecto.");
            }

        [TestMethod]
        public void Saldoinsuficiente() // reescrito el codigo con los try catch   hay que quitar [ExpectedException(typeof(ArgumentOutOfRangeException))]
            {
            double Saldoinicial = 1000;
            double retirada_OSK2223 = 1001;
            double saldoEsperado = Saldoinicial - retirada_OSK2223;

            GestionBancariaApp Apptext = new GestionBancariaApp(Saldoinicial);
            try
                {
                Apptext.RealizarReintegro(retirada_OSK2223);
                }
            catch (ArgumentOutOfRangeException escepcion)
                {
                StringAssert.Contains(escepcion.Message, GestionBancariaApp.ERR_SALDO_INSUFICIENTE);
                return;
                }

            Assert.Fail("Error. Se debía haber producido una excepción.");
            }

        [TestMethod]
        public void RetiradaNegativa()
            {
            double Saldoinicial = 1000;
            double retirada_osk2223 = -500;
            double saldoEsperado = 1000;

            GestionBancariaApp Apptext = new GestionBancariaApp(Saldoinicial);
            try
                {
                Apptext.RealizarReintegro(retirada_osk2223);
                //Assert.AreEqual(saldoEsperado, saldoEsperado, 0.01);
                }
            catch (ArgumentOutOfRangeException excepcion)
                {
                StringAssert.Contains(excepcion.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
                }
            Assert.Fail("Error. Se debía haber producido una excepción.");
            }

        [TestMethod]
        public void IngresoNegativo()
            {
            double Saldoinicial = 1000;
            double ingreso = -1;
            double saldoEsperado = 1000;

            GestionBancariaApp Apptext = new GestionBancariaApp(Saldoinicial);
            try
                {
                Apptext.RealizarIngreso(ingreso);
                }
            catch (ArgumentOutOfRangeException escepcion)
                {
                StringAssert.Contains(escepcion.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
                }
            Assert.Fail("Error. Se debía haber producido una excepción.");
            }

        [TestMethod]
        public void IngresoValido()
            {
            double Saldoinicial = 1000;
            double ingreso_OSK2223 = 1;
            double saldoEsperado = 1001;

            GestionBancariaApp Apptext = new GestionBancariaApp(Saldoinicial);
            Apptext.RealizarIngreso(ingreso_OSK2223);

            Assert.AreEqual(saldoEsperado, Apptext.ObtenerSaldo(), 0.001);
            }

        /*
          // reescrito el codigo con los try catch   hay que quitar [ExpectedException(typeof(ArgumentOutOfRangeException))]

          [TestMethod]
          [ExpectedException(typeof(ArgumentOutOfRangeException))]
          public void validarReintegroCantidadNoValida()
              {
              double saldoInicial = 1000;
              double reintegro = 1250;
              double saldoFinal = saldoInicial - reintegro;

              GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
              miApp.RealizarReintegro(reintegro);
              }
        */

        [TestMethod]
        public void validarReintegroCantidadNoValida_Reescrita()
            {
            double saldoInicial = 1000;
            double reintegro = -250;
            double saldoFinal = saldoInicial - reintegro;

            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
            try
                {
                miApp.RealizarReintegro(reintegro);
                }
            catch (ArgumentOutOfRangeException exception)
                {
                // assert
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
                }
            Assert.Fail("Error. Se debía haber producido una excepción.");
            }
        }
    }