using SistemaBiblioteca_Projeto_Autoral.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class ItemPedido
    {
        public Livro Livro { get; private set; }

        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            private set { quantidade = Validador.ValidarNumeroint(value, "Quantidade item carrinho"); }
        }

        private decimal precoUnitario;
        public decimal PrecoUnitario
        {
            get { return precoUnitario; }
            private set { precoUnitario = Validador.ValidarNumeroDecimal(value, "Preço unitário item carrinho"); }
        }

        public ItemPedido(Livro livro, int quantidade, decimal precoUnitario)
        {
            Livro = livro;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }


    }
}
