using System.ComponentModel.DataAnnotations;

namespace DomainTables
{
    public class Operation
    {
        [Key]
        public short CodeOperation { get; set; } //PK- число 1..50
        public short ShopNumber { get; set; } //1…20
        public short Time { get; set; } //1..8
        public double Price { get; set; } //Число з двома знаками після коми
    }
}
