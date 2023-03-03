using System.ComponentModel.DataAnnotations;

namespace InvoicePOCJaya.Models
{
    public class Invoice
    {
        [Key]
        public Guid InvoiceGuid { get; set; }
        public string InvoiceID { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceTotal { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
