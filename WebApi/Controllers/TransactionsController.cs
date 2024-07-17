using AutoMapper;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ISearchTransactionsUseCase searchTransactionsUseCase;
        private readonly IGetTodayTransactionsUseCase getTodayTransactionsUseCase;
        private readonly IMapper _mapper;

        public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase, IGetTodayTransactionsUseCase getTodayTransactionsUseCase,IMapper mapper)
        {
            this.searchTransactionsUseCase = searchTransactionsUseCase;
            this.getTodayTransactionsUseCase = getTodayTransactionsUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public Task<TransactionsViewModel> Search(TransactionsViewModel transactionsModel)
        {
            var transactions = searchTransactionsUseCase.Execute(
                transactionsModel.CashierName ?? string.Empty,
                transactionsModel.StartDate,
                transactionsModel.EndDate);

            transactionsModel.Transactions = transactions;

            return Task.FromResult(transactionsModel);
        }


        [HttpGet("Today")]
        public Task<IEnumerable<Transaction>> GetTodayTransactions([FromQuery]string cashierName)
        {
            var transactionsModel = getTodayTransactionsUseCase.Execute(cashierName);
            return Task.FromResult(transactionsModel);
        }
    }
}
