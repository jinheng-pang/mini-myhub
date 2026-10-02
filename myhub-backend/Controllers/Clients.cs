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
        string OpenedDate,
        string Balance,
        string InternalLedgerCode);

    [Route("api/[controller]")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private static readonly Client[] _clients =
        [
            new Client("c-1001", "Margaret Chen", "margaret.chen@example.com", "AMBER", "adv-77",
            [
                new Account("a-5001", "Super", "Active", "2019-03-14", "412350.22", "SUP-AU-03"),
                new Account("a-5002", "Pension", "Active", "2021-07-01", "188900.00", "PEN-AU-01"),
                new Account("a-5003", "Investment", "Closed", "2017-11-20", "0.00", "INV-AU-09")
            ]),
            new Client("c-1002", "David Okafor", "david.okafor@example.com", "GREEN", "adv-12",
            [
                new Account("a-5004", "Super", "Active", "2015-06-02", "275480.10", "SUP-AU-01"),
                new Account("a-5005", "Investment", "Active", "2020-02-18", "96200.75", "INV-AU-04"),
                new Account("a-5006", "Pension", "Pending", "2024-09-30", "0.00", "PEN-AU-02")
            ]),
            new Client("c-1003", "Priya Nair", "priya.nair@example.com", "RED", "adv-41",
            [
                new Account("a-5007", "Pension", "Active", "2018-01-09", "531020.00", "PEN-AU-05"),
                new Account("a-5008", "Super", "Suspended", "2016-10-25", "142775.60", "SUP-AU-07"),
                new Account("a-5009", "Investment", "Closed", "2013-04-11", "0.00", "INV-AU-02")
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
