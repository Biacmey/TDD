using Microsoft.VisualBasic;
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
        var includeBudget = budgets.Where(x =>
        {
            var yearMonth = DateTime.ParseExact(x.YearMonth, "yyyyMM", null);

            return new DateTime(startDate.Year,startDate.Month,1) <= yearMonth && new DateTime(endDate.Year,endDate.Month,1) >= yearMonth;
        });
        var amountIndex = includeBudget.Select(s =>
        {
            var FirstDayOfMonth = DateTime.ParseExact(s.YearMonth, "yyyyMM", null);
            var AmountByDay = s.Amount / DateTime.DaysInMonth(FirstDayOfMonth.Year, FirstDayOfMonth.Month);
            return (FirstDayOfMonth, AmountByDay);
        });
        var result = 0;
        foreach (var amount in amountIndex)
        {
            if (startDate > amount.FirstDayOfMonth && (endDate > amount.FirstDayOfMonth.AddMonths(1)))
            {
                var days = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
                result += days * amount.AmountByDay;
            }
            else if (startDate <= amount.FirstDayOfMonth && (endDate < amount.FirstDayOfMonth.AddMonths(1)))
            {
                var days = endDate.Day;
                result += days * amount.AmountByDay;
            }
            else if (startDate > amount.FirstDayOfMonth && (endDate < amount.FirstDayOfMonth))
            {
                var daysStart = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
                var daysEnd = DateTime.DaysInMonth(endDate.Year, endDate.Month) - endDate.Day + 1;
                var days = daysStart + daysEnd - DateTime.DaysInMonth(startDate.Year, startDate.Month);
                result += days * amount.AmountByDay;
            }
            else
            {
                var days = DateTime.DaysInMonth(amount.FirstDayOfMonth.Year, amount.FirstDayOfMonth.Month);
                result += days * amount.AmountByDay;
            }
        }
        return result;
    }
}