using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace ReceiptTrackerWPF
{
    class RecieptData : DbContext
    {
        public DbSet<Receipt> receipts { get; set; }
        public DbSet<Item> items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=blogging.db");
            //options.UseLazyLoadingProxies();
            base.OnConfiguring(options);
        }
    }
    public class Receipt
    {   [Key]
        [Range(0, int.MaxValue)]
        public int id { get; set; }
        [StringLength(50)]
        public String store { get; set; }
        [Required]
        public DateTime day { get; set; }
        [Display(Name ="Cost")]
        [Required]
        public decimal price { get; set; }

        public ICollection<Item> items { get; set; }
    }

    public class Item {
        [Key]
        [Range(0, int.MaxValue)]
        public int id { get; set; }
        [StringLength(100)]
        public String name { get; set; }
        [ForeignKey("Receipt")]
        public int ReceiptId { get; set; }
        public Receipt receipt { get; set; }
}

}
