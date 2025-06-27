namespace shared.DataTransferObjects.OrderDTO_S
{
    public class OrderItemDTO
    {

        //public int productId { get; set; }
        public string ProductName { get; set; } = default!;

        public string PictureUrl { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quantity {  get; set; }
    }
}