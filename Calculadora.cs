using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculadoraDeTerminal
{
    public class Calculadora
    {
        private const string PadraoDeExpressaoAritmetica = @"^(([\s]+)?([\d,]+)([\s]?)+([-+/*])([\s]?)([\d,]+)([\s]+)?)$";
        private readonly Regex FiltroDeExpressaoAritmetica = new(PadraoDeExpressaoAritmetica);
        private string ExpressaoAritmetica;
        private double PrimeiroNumero;
        private double SegundoNumero;
        private string Operador;
        private double Resultado;

        public Calculadora()
        {
            MostrarRegrasDaCalculadora();
        }

        private static void MostrarRegrasDaCalculadora()
        {
            Console.WriteLine("" +
                "Esta calculadora fará apenas cálculos de expressões aritméticas simples de 2 números.\n" +
                "Os operadores aceitos pela calculadora são:\n" +
                "Subtração (-)\n" +
                "Adição (+)\n" +
                "Divisão (/)\n" +
                "Multiplicação (*)");
        }

        public void CalcularExpressaoAritmetica()
        {
            string inputDoUsuario;
            do
            {
                inputDoUsuario = ObterInputDoUsuario();
            }
            while (!ChecarSeInputDoUsuarioEUmaExpressaoAritmeticaValida(inputDoUsuario));

            SepararNumerosEOperadorDaExpressaoAritmetica();

            EfetuarCalculo();

            MostrarResultado();

            CalcularExpressaoAritmetica();
        }

        private static string ObterInputDoUsuario()
        {
            Console.WriteLine("Digite uma expressão aritmética válida para obter o resultado ou digite 'sair' para finalizar o programa:");
            string inputDoUsuario = Console.ReadLine();

            if (inputDoUsuario == "sair")
                Environment.Exit(0);

            inputDoUsuario = inputDoUsuario.Replace(".", ",");

            return inputDoUsuario;
        }

        private Boolean ChecarSeInputDoUsuarioEUmaExpressaoAritmeticaValida(string inputDoUsuario)
        {
            Boolean inputDoUsuarioEUmaExpressaoAritmeticaValida = this.FiltroDeExpressaoAritmetica.IsMatch(inputDoUsuario);

            MostrarMensagemDeValidadeDaExpressaoAritmetica(inputDoUsuarioEUmaExpressaoAritmeticaValida, inputDoUsuario);

            AtribuirExpressaoAritmeticaValida(inputDoUsuarioEUmaExpressaoAritmeticaValida, inputDoUsuario);

            return inputDoUsuarioEUmaExpressaoAritmeticaValida;
        }

        private static void MostrarMensagemDeValidadeDaExpressaoAritmetica(Boolean expressaoAritmeticaEValida, string expressaoAritmetica)
        {
            if (expressaoAritmeticaEValida)
            {
                Console.WriteLine($"O resultado da expressão aritmética: ({expressaoAritmetica}) é:");
            }
            else
            {
                Console.WriteLine($"" +
                    $"A expressão: ({expressaoAritmetica}) , é uma expressão aritmética inválida, " +
                    $"por favor, digite uma expresão aritmética válida de acordo com os requisitos da calculadora."
                    );
                MostrarRegrasDaCalculadora();
            }
        }

        private void AtribuirExpressaoAritmeticaValida(Boolean expressaoAritmeticaEValida, string inputDoUsuario)
        {
            if (expressaoAritmeticaEValida)
                this.ExpressaoAritmetica = inputDoUsuario;
        }

        private void SepararNumerosEOperadorDaExpressaoAritmetica()
        {
            SepararNumerosDaExpressaoAritmetica();
            SepararOperadorDaExpressaoAritmetica();
        }

        private void SepararNumerosDaExpressaoAritmetica()
        {
            char[] separadores = new char[] { ' ', '-', '+', '/', '*' };
            string[] listaDeNumeros = this.ExpressaoAritmetica.Split(separadores, StringSplitOptions.RemoveEmptyEntries);

            this.PrimeiroNumero = Convert.ToDouble(listaDeNumeros.First());
            this.SegundoNumero = Convert.ToDouble(listaDeNumeros.Last());
        }

        private void SepararOperadorDaExpressaoAritmetica()
        {
            char[] separadores = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', ' ' };

            this.Operador = ExpressaoAritmetica.Trim(separadores);
        }

        private void EfetuarCalculo()
        {
            switch (this.Operador)
            {
                case "-":
                    this.Resultado = this.PrimeiroNumero - this.SegundoNumero;
                    break;
                case "+":
                    this.Resultado = this.PrimeiroNumero + this.SegundoNumero;
                    break;
                case "/":
                    this.Resultado = this.PrimeiroNumero / this.SegundoNumero;
                    break;
                case "*":
                    this.Resultado = this.PrimeiroNumero * this.SegundoNumero;
                    break;
            }
        }

        private void MostrarResultado()
        {
            Console.WriteLine(this.Resultado);
        }
    }
}
