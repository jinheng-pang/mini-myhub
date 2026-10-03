using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myhub_backend.Controllers
{
    public record Client(
        string ClientId,
        string ClientFullName,
        string ClientEmail,
        string InternalRiskRating,
        string LastReviewedByAdviserId,
        Account[] Accounts);

    public record Account(
        string AccountId,
        string AccountType,
        string Status,
        DateOnly OpenedDate,
        decimal Balance,
        string InternalLedgerCode);

    [Route("api/[controller]")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private static readonly Client[] _clients =
        [
            new Client("c-1001", "Margaret Chen", "margaret.chen@example.com", "AMBER", "adv-77",
            [
                new Account("a-5001", "Super", "Active", new DateOnly(2019, 3, 14), 412350.22m, "SUP-AU-03"),
                new Account("a-5002", "Pension", "Active", new DateOnly(2021, 7, 1), 188900.00m, "PEN-AU-01"),
                new Account("a-5003", "Investment", "Closed", new DateOnly(2017, 11, 20), 0.00m, "INV-AU-09")
            ]),
            new Client("c-1002", "David Okafor", "david.okafor@example.com", "GREEN", "adv-12",
            [
                new Account("a-5004", "Super", "Active", new DateOnly(2015, 6, 2), 275480.10m, "SUP-AU-01"),
                new Account("a-5005", "Investment", "Active", new DateOnly(2020, 2, 18), 96200.75m, "INV-AU-04"),
                new Account("a-5006", "Pension", "Pending", new DateOnly(2024, 9, 30), 0.00m, "PEN-AU-02")
            ]),
            new Client("c-1003", "Priya Nair", "priya.nair@example.com", "RED", "adv-41",
            [
                new Account("a-5007", "Pension", "Active", new DateOnly(2018, 1, 9), 531020.00m, "PEN-AU-05"),
                new Account("a-5008", "Super", "Suspended", new DateOnly(2016, 10, 25), 142775.60m, "SUP-AU-07"),
                new Account("a-5009", "Investment", "Closed", new DateOnly(2013, 4, 11), 0.00m, "INV-AU-02")
            ])
        ];

        [HttpGet("{clientId}/accounts")]
        public ActionResult<Client> GetById(string clientId)
        {
            var client = _clients.FirstOrDefault(c => c.ClientId == clientId);

            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }
    }
}
