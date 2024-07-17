using Microsoft.AspNetCore.Mvc;
using CoreBusiness;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WebApp.Factory.Interfaces;

namespace WebApp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsClient _transactionClient;

        public TransactionsController(ITransactionsClient transactionsClient)
        {
            _transactionClient = transactionsClient;
        }

        public IActionResult Index()
        {
            ViewModels.TransactionsViewModel transactionsViewModel = new ViewModels.TransactionsViewModel();
            return View(transactionsViewModel);
        }

        public async Task<IActionResult> Search(ViewModels.TransactionsViewModel transactionsViewModel)
        {
            var transactions = await _transactionClient.GetTransactions(transactionsViewModel);


            return View("Index", transactions);
        }
    }
}
