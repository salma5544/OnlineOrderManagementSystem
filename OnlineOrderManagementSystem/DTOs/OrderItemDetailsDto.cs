namespace OnlineOrderManagementSystem.DTOs
{
    public class OrderItemDetailsDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
