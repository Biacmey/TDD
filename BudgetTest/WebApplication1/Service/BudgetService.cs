using WebApplication1.Repo;

namespace WebApplication1;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime startDate, DateTime endDate)
    {
        var budgets = _budgetRepo.GetAll();
        return budgets.Where(x =>
        {
            var yearMonth = DateTime.ParseExact(x.YearMonth, "yyyyMM", null);
            
            return startDate<=yearMonth && endDate>=yearMonth;
        }).Sum(x=>x.Amount);
    }
}