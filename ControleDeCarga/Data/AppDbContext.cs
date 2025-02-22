using ControleDeCarga.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCarga.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UsuarioHierarquiaEntity> UsuarioHierarquia {  get; set; }
        public DbSet<CategoriaVeiculoEntity> CategoriaVeiculo { get; set; }
        public DbSet<TransportadoraEntity> Transportadoras { get; set; }
        public DbSet<StatusCarregamentoEntity> StatusCarregamento { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<VeiculoEntity> Veiculos { get; set; }
        public DbSet<MotoristaEntity> Motoristas { get; set; }
        public DbSet<PlanejamentoCargaEntity> PlanejamentoCargas { get; set; }
        public DbSet<HistoricoMovimentacaoEntity> HistoricoMovimentacao { get; set; }

    }
}
