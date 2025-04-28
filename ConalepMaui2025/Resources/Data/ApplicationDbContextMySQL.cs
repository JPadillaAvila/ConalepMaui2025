using ConalepMaui2025.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConalepMaui2025.Resources.Data
{
    public class ApplicationDbContextMySQL : IdentityDbContext
    {
        public ApplicationDbContextMySQL(DbContextOptions<ApplicationDbContextMySQL> options)
            : base(options)
        {
        }

        public DbSet<Expediente> Expedientes { get; set; }
        //public DbSet<SatIdData> SatId { get; set; }

        ////public DbSet<reg_nominas> reg_nominas { get; set; }
        //public DbSet<SericaHeaderModel> sericaReporteModels { get; set; }
        //public DbSet<SericaDetalleReporteModel> sericaDetalleReporteModels { get; set; }
        //public DbSet<PartidasModel> partidasModels { get; set; }
        //public DbSet<LineaModel> lineaModels { get; set; } 
        //public DbSet<FovissteReportModel> fovissteReportModels { get;set; }
        //public DbSet<FonacotReportModel> fonacotReportsModels { get; set; }


    }
}
