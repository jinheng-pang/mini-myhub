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
        string Units,
        string UnitPrice,
        string MarketValue,
        string CustodianRef);

    [Route("api/[controller]")]
    [ApiController]
    public class Accounts : ControllerBase
    {
        private static readonly AccountHoldings[] _accountHoldings =
        [
            new AccountHoldings("a-5001",
            [
                new Holding("h-9001", "VAS", "Vanguard Australian Shares ETF", "1200.5", "98.4", "118129.2", "CUST-X-114"),
                new Holding("h-9002", "VGS", "Vanguard International Shares ETF", "2100.0", "112.75", "236775.0", "CUST-X-115"),
                new Holding("h-9003", "AAA", "Betashares Australian High Interest Cash ETF", "1150.0", "50.0", "57446.02", "CUST-X-116")
            ]),
            new AccountHoldings("a-5002",
            [
                new Holding("h-9004", "VDHG", "Vanguard Diversified High Growth Index ETF", "1850.0", "62.30", "115255.0", "CUST-X-117"),
                new Holding("h-9005", "IOZ", "iShares Core S&P/ASX 200 ETF", "980.25", "34.15", "33475.54", "CUST-X-118"),
                new Holding("h-9006", "NDQ", "Betashares Nasdaq 100 ETF", "1240.0", "32.40", "40176.00", "CUST-X-119")
            ]),
            new AccountHoldings("a-5003",
            [
                new Holding("h-9007", "VGB", "Vanguard Australian Government Bond ETF", "0.0", "0.0", "0.00", "CUST-X-120"),
                new Holding("h-9008", "IOO", "iShares Global 100 ETF", "0.0", "0.0", "0.00", "CUST-X-121"),
                new Holding("h-9009", "VAP", "Vanguard Australian Property Securities ETF", "0.0", "0.0", "0.00", "CUST-X-122")
            ])
        ];

        [HttpGet("{accountId}/holdings")]
        public ActionResult<Holding> GetById(string accountId)
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
