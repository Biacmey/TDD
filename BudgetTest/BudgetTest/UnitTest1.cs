using FluentAssertions;
using NSubstitute;
using WebApplication1;
using WebApplication1.Model;
using WebApplication1.Repo;

namespace BudgetTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
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
        
        var budgetService = new BudgetService();
        var result = budgetService.Query(new DateTime(2023,1,1), new DateTime(2023,1,31));
        result.Should().Be(3100);
    }
    
}