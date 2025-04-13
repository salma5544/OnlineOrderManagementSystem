using OnlineOrderManagementSystem.Models;
using System.Text.Json.Serialization;

namespace OnlineOrderManagementSystem.DTOs
{
    public class UpdateOrderStatusDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
    }
}
