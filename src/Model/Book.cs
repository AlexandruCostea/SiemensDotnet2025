namespace LibraryAPI.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int BorrowedQuantity { get; set; }
        public int TotalBorrowedCount { get; set; } = 0;
    }
}