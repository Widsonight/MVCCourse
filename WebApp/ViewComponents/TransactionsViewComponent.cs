using CoreBusiness;
using Microsoft.AspNetCore.Mvc;
using WebApp.Factory.Interfaces;

namespace WebApp.ViewComponents
{
    [ViewComponent]
    public class TransactionsViewComponent : ViewComponent
    {
        private readonly ITransactionsClient _transactionsClient;

        public TransactionsViewComponent(ITransactionsClient transactionsClient)
        {
            _transactionsClient = transactionsClient;
        }

        public IViewComponentResult Invoke(string username)
        {
            IEnumerable<Transaction> transactions= new List<Transaction>();
            Task.Run(async () =>
            {
                transactions = await _transactionsClient.GetTransactionsToday(username);
            }).Wait();

            return View(transactions);
        }
    }
}
