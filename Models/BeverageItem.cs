namespace BeverageAPI.Models
{
    public class BeverageItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool inStock { get; set; }
    }
}
