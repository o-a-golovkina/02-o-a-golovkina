namespace DomainTables
{
    public class Production
    {
        public short CodeDetail { get; set; } //FK
        public short OperationNumber { get; set; } //// Від 1 до 100, разом з кодом операції складають ПК
        public short CodeOperation { get; set; } //FK

        // Навігаційні властивості
        public Detail? Detail { get; set; }
        public Operation? Operation { get; set; }
    }
}
