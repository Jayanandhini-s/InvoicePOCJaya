using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoicePOCJaya.Models
{
    public class Item
    {
        [Key]
        public Guid ItemGuid { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string LineTotal { get; set; }
        public Guid InvoiceGuid { get; set; }
        [ForeignKey("InvoiceGuid")]
        public virtual Invoice Invoice { get; set; }
    }
}
