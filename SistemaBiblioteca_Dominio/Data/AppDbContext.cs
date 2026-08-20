using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca_Projeto_Autoral.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Carrinho> Carrinhos { get; set; }

        public DbSet<ItemCarrinho> ItemCarrinhos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ItemPedido> ItemPedidos { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;port=3306;database=livraria_db;user=root;password=root;";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

    }
}
