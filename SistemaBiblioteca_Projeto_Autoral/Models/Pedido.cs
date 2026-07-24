using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Pedido
    {
        public Usuario UsuarioLogado { get; private set; }

        private List<ItemPedido> itenspedido = new List<ItemPedido>();

        public List<ItemPedido> ItensPedido
        {
            get { return new List<ItemPedido>(itenspedido); }
        }
        
        public DateTime DataPedido { get; private set; }
        

        public string Status { get; private set; }

        public decimal TotalPedido
        {
            get
            {
                decimal total = 0;
                foreach (var item in itenspedido)
                {
                    total += item.PrecoUnitario * item.Quantidade;
                }
                return total;
            }
            
        }

        public Pedido(Usuario usuarioLogado, List<ItemPedido> itensPedido)
        {
            UsuarioLogado = usuarioLogado;
            itenspedido = new List<ItemPedido>(itensPedido);
            Status = "PENDENTE";
            DataPedido = DateTime.Now;
        }

    }
}
