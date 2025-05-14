using ClientDashboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using ClientDashboardAPI.Interfaces;
using System.Data;


namespace ClientDashboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ClientController(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        private IDbConnection CreateConnection() => _connectionFactory.CreateConnection();

        // GET: client
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetAllClients()
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<Client>("SELECT * FROM Client WHERE archived IS NULL").ToList();
            }
        }

        // GET: client/5
        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            using (var connection = CreateConnection())
            {
                var client = connection.QuerySingleOrDefault<Client>("SELECT * FROM Client WHERE id = @Id", new { Id = id });
                if (client == null)
                {
                    return NotFound();
                }
                return client;
            }
        }

        // POST
        [HttpPost]
        public ActionResult<Client> CreateClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var connection = CreateConnection())
            {
                string sql = "INSERT INTO Client (UserName, Email) VALUES (@UserName, @Email); SELECT LAST_INSERT_ID();";
                int clientId = connection.QuerySingle<int>(sql, client);
                client.Id = clientId;
                return CreatedAtAction(nameof(GetClient), new { id = clientId }, client);
            }
        }

        // PUT: client/5
        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var connection = CreateConnection())
            {
                string sql = $"UPDATE Client SET UserName = @UserName, Email = @Email WHERE Id = {id}";
                int rowsAffected = connection.Execute(sql, client);
                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // PUT: client/ArchiveClient/5
        [HttpPut("archive/{id}")]
        public IActionResult ArchiveClient(int id)
        {
            using var connection = CreateConnection();
            string sql = $"UPDATE Client SET archived = CURRENT_TIMESTAMP() WHERE id = {id}";
            int rowsAffected = connection.Execute(sql);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
