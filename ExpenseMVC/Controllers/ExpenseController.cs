using ExpenseMVC.Data;
using ExpenseMVC.Models;
using ExpenseMVC.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMVC.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly MvcDbContext mvcDbContext;

        public ExpenseController(MvcDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
           var expense = await mvcDbContext.Expenses.ToListAsync();
           return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddExpenseViewModel addExpenseRequest)
        {
            var expense = new Expense()
            {
                Id = Guid.NewGuid(),
                ItemName = addExpenseRequest.ItemName,
                Amount = addExpenseRequest.Amount,
                ExpenseDate = addExpenseRequest.ExpenseDate,
                Category = addExpenseRequest.Category
            };

            await mvcDbContext.Expenses.AddAsync(expense);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var expense =await mvcDbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if(expense != null)
            {
                var viewModel = new UpdateExpenseViewModel()
                {
                    Id = expense.Id,
                    ItemName = expense.ItemName,
                    Amount = expense.Amount,
                    ExpenseDate = expense.ExpenseDate,
                    Category = expense.Category
                };
                return await Task.Run(() =>View("View", viewModel));
            }
            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> View(UpdateExpenseViewModel model)
        {
            var expense = await mvcDbContext.Expenses.FindAsync(model.Id);
            if(expense != null)
            {
                expense.ItemName = model.ItemName;
                expense.Amount = model.Amount;
                expense.Category = model.Category;
                expense.ExpenseDate = model.ExpenseDate;

                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateExpenseViewModel model)
        {
            var expense = await mvcDbContext.Expenses.FindAsync(model.Id);
             if(expense != null)
            {
                mvcDbContext.Expenses.Remove(expense);
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
