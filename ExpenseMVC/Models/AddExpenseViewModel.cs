namespace ExpenseMVC.Models
{
    public class AddExpenseViewModel
    {
        public string ItemName { get; set; }
        public long Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Category { get; set; }
    }
}
