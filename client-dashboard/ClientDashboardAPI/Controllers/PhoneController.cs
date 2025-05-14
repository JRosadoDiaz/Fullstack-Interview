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
    public class PhoneController : Controller
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PhoneController(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        private IDbConnection CreateConnection() => _connectionFactory.CreateConnection();

        // GET: phone/5
        [HttpGet("{id}")]
        public ActionResult<Phone> GetPhone(int id)
        {
            using (var connection = CreateConnection())
            {
                string sql = $"SELECT * FROM ClientPhone WHERE id = {id};";
                return connection.QuerySingleOrDefault<Phone>(sql);
            }
        }

        // GET: phone/client/5
        [HttpGet("client/{clientId}")]
        public ActionResult<IEnumerable<Phone>> GetAllPhonesByClient(int clientId)
        {
            using (var connection = CreateConnection())
            {
                string sql = $"SELECT * FROM ClientPhone WHERE clientId = {clientId};";
                return connection.Query<Phone>(sql).ToList();
            }
        }

        // POST
        [HttpPost]
        public ActionResult<Phone> CreatePhone([FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var connection = CreateConnection())
            {
                string sql = $"INSERT INTO ClientPhone (clientId, phoneNumber) VALUES (@ClientId, @PhoneNumber); SELECT LAST_INSERT_ID();";
                int phoneId = connection.QuerySingle<int>(sql, phone);
                phone.Id = phoneId;
                return CreatedAtAction(nameof(GetPhone), new { id = phoneId }, phone);
            }
        }

        // PUT: phone/5
        [HttpPut("{id}")]
        public IActionResult UpdatePhone(int id, [FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var connection = CreateConnection())
            {
                string sql = $"UPDATE ClientPhone SET PhoneNumber = @PhoneNumber WHERE Id = {id};";
                int rowsAffected = connection.Execute(sql, phone);
                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // DELETE: phone/5
        [HttpDelete("{id}")]
        public ActionResult DeletePhone(int id)
        {
            using (var connection = CreateConnection())
            {
                string sql = $"DELETE FROM ClientPhone WHERE Id = {id};";
                int rowsAffected = connection.Execute(sql);

                if (rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
    }
}
