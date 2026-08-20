using SistemaBiblioteca_Projeto_Autoral.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class ItemCarrinho
    {

        public int Id { get; private set; }

        public int CarrinhoId { get; private set; }


        public void DefinirCarrinhoId(int carrinhoId)
        {
            CarrinhoId = carrinhoId;
        }
            
        public Livro Livro { get; private set; }


        private int quantidade;
        public int Quantidade 
        { get {  return quantidade; }
           set {quantidade = Validador.ValidarNumeroint(value, "Quantidade item carrinho"); } 
        }


        public ItemCarrinho(Livro livro, int quantidade)
        {
            Livro = livro;
            Quantidade = quantidade;
        }

        private ItemCarrinho() { }

    }

    
    } 
