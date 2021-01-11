using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace ReceiptTrackerWPF
{
    class RecieptData:DbContext
    {
        public DbSet<Receipt> receipts { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=blogging.db");
            //options.UseLazyLoadingProxies();
            base.OnConfiguring(options);
        }
    }
    public class Receipt { 
    
        public int id { get; set; }
        public String store { get; set; }
        public DateTime day { get; set; }
        
        public decimal price { get; set; }


    }
}
