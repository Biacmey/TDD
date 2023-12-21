using WebApplication1.Model;

namespace WebApplication1.Repo;

public interface IBudgetRepo
{
    IEnumerable<Budget> GetAll();
}