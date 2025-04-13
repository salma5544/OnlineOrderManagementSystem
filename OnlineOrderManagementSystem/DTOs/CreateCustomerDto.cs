namespace OnlineOrderManagementSystem.DTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
