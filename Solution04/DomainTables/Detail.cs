namespace DomainTables
{
    public class Detail
    {
        public short CodeDetail { get; set; } //PK
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty; //до 10 символів
        public double Mass { get; set; } //три цифри до та після коми

        // Навігаційна властивість
        public ICollection<Production>? Productions { get; set; }
    }
}
