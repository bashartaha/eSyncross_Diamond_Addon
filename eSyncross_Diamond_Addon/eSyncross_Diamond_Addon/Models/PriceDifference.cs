namespace Diamond_Addon.Models
{
    class PriceDifference
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal NewTagPrice { get; set; }
    }
}
