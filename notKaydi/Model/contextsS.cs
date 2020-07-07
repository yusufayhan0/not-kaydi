using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notKaydi.Model
{
    public class contextsS : DbContext
    {
        public contextsS() : base("ConnectionString")
        {
            
        }
        public DbSet<dersler> derslers { get; set; }
        public DbSet<nots> notss { get; set; }
    }
}
