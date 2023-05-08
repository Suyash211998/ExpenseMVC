namespace ExpenseMVC.Models
{
    public class UpdateExpenseViewModel
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; }
        public long Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Category { get; set; }

    }
}
