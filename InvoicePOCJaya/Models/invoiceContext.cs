using Microsoft.EntityFrameworkCore;

namespace InvoicePOCJaya.Models
{
    public class invoiceContext:DbContext
    {
        public invoiceContext() { }
        public invoiceContext(DbContextOptions<invoiceContext> options)
           : base(options)
        {
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.               
                optionsBuilder.UseSqlServer("Server=.;Database=InvoiceAutomateNew;Encrypt=False;Trusted_Connection=True;");
            }
        }
    }
}

