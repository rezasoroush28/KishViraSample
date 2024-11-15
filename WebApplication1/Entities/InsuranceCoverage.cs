namespace WebApplication1.Entities
{
    public class InsuranceCoverage
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
    }
}
