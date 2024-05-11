using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace GearClub.Domain.Models
{
    public class Order
    {
        [Key]
        
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }
        public decimal ShippingFee
        {
            get
            {
                if (Subtotal <= 300000) return 15000;
                else if (Subtotal <= 500000) return 10000;
                else return 0;
            }
        }
        public decimal Total => Subtotal + ShippingFee;
        public string Status { get; set; } = "Chờ xác nhận";
        public bool IsDelivered { get; set; } = false;
        public bool IsCheckedOut { get; set; } = false;
        public int AddressId { get; set; }
        [StringLength(10)]
        public string Payment { get; set; } = null!;
        public virtual Address Address { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
