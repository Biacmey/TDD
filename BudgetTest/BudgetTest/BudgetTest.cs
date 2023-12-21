using FluentAssertions;
using NSubstitute;
using NUnit.Framework.Internal;
using WebApplication1;
using WebApplication1.Model;
using WebApplication1.Repo;

namespace BudgetTest;

public class BudgetTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void full_month()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();
        budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202301",
                Amount = 3100,
            }
        });
        
        var budgetService = new BudgetService(budgetRepo);
        var result = budgetService.Query(new DateTime(2023,1,1), new DateTime(2023,1,31));
        result.Should().Be(3100);
    }
    [Test]
    public void partical_month()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();
        budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202301",
                Amount = 3100,
            },
            new Budget()
            {
                YearMonth = "202302",
                Amount = 2800,
            },
            new Budget()
            {
                YearMonth = "202212",
                Amount = 3100,
            },
        });
        
        var budgetService = new BudgetService(budgetRepo);
        var result = budgetService.Query(new DateTime(2022,12,25), new DateTime(2023,2, 10));
        result.Should().Be(4800);
    }

    
}