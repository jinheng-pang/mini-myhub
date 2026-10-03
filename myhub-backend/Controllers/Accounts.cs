using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myhub_backend.Controllers
{
    public record AccountHoldings(
        string AccountId,
        Holding[] Holdings);

    public record Holding(
        string HoldingId,
        string AssetCode,
        string AssetName,
        decimal Units,
        decimal UnitPrice,
        decimal MarketValue,
        string CustodianRef);

    [Route("api/[controller]")]
    [ApiController]
    public class Accounts : ControllerBase
    {
        private static readonly AccountHoldings[] _accountHoldings =
        [
            new AccountHoldings("a-5001",
            [
                new Holding("h-9001", "VAS", "Vanguard Australian Shares ETF", 1200.5m, 98.4m, 118129.2m, "CUST-X-114"),
                new Holding("h-9002", "VGS", "Vanguard International Shares ETF", 2100.0m, 112.75m, 236775.0m, "CUST-X-115"),
                new Holding("h-9003", "AAA", "Betashares Australian High Interest Cash ETF", 1150.0m, 50.0m, 57446.02m, "CUST-X-116")
            ]),
            new AccountHoldings("a-5002",
            [
                new Holding("h-9004", "VDHG", "Vanguard Diversified High Growth Index ETF", 1850.0m, 62.30m, 115255.0m, "CUST-X-117"),
                new Holding("h-9005", "IOZ", "iShares Core S&P/ASX 200 ETF", 980.25m, 34.15m, 33475.54m, "CUST-X-118"),
                new Holding("h-9006", "NDQ", "Betashares Nasdaq 100 ETF", 1240.0m, 32.40m, 40176.00m, "CUST-X-119")
            ]),
            new AccountHoldings("a-5003",
            [
                new Holding("h-9007", "VGB", "Vanguard Australian Government Bond ETF", 0.0m, 0.0m, 0.00m, "CUST-X-120"),
                new Holding("h-9008", "IOO", "iShares Global 100 ETF", 0.0m, 0.0m, 0.00m, "CUST-X-121"),
                new Holding("h-9009", "VAP", "Vanguard Australian Property Securities ETF", 0.0m, 0.0m, 0.00m, "CUST-X-122")
            ])
        ];

        [HttpGet("{accountId}/holdings")]
        public ActionResult<AccountHoldings> GetById(string accountId)
        {
            var holding = _accountHoldings.FirstOrDefault(a => a.AccountId == accountId);

            if (holding is null)
            {
                return NotFound();
            }

            return Ok(holding);
        }
    }
}
