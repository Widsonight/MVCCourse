using CoreBusiness;

namespace WebApp.Factory.Interfaces
{
    public interface ITransactionsClient
    {
        Task<ViewModels.TransactionsViewModel> GetTransactions(ViewModels.TransactionsViewModel transactionsViewModel);
        Task<IEnumerable<Transaction>> GetTransactionsToday(string name);
    }
}