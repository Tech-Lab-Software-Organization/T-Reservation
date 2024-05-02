using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_RESERVATION.AccesoDatos
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            const string conn = "workstation id=TRerservationdb.mssql.somee.com;packet size=4096;user id=ingridgabriel_SQLLogin_1;pwd=lyro9bm4bu;data source=TRerservationdb.mssql.somee.com;persist security info=False;initial catalog=TRerservationdb;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(conn);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
