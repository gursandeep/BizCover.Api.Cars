namespace BizCover.Api.Cars.Models
{
    public class DiscountedCarDTO
    {
        public CarDTO Car { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}