using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using web_service.Models;

namespace web_service.Controllers;

[ApiController]
[Route("api/[controller]")] // Sets the base route to /api/collection
public class CollectionController(NpgsqlDataSource dataSource, ILogger<CollectionController> logger) : ControllerBase
{
    // This method will handle GET requests to /api/collection
    [HttpGet()]
    public async Task<IActionResult> GetCollection(int userId)
    {
        try
        {
            //"Generate Series" produces duplicate cards depending on the amount column
            await using var connection = await dataSource.OpenConnectionAsync();
            const string sql = @"
                SELECT
                    c.*
                FROM
                    cards AS c
                INNER JOIN
                    users_cards AS uc ON c.card_id = uc.card_id
                INNER JOIN
                    generate_series(1, uc.amount) ON true
                WHERE
                    uc.user_id = @userId;";
            // Dapper automatically maps the columns to your user model
            var collection = await connection.QueryAsync<Card>(sql, new { userId });

            if (collection.Any())
            {
                return Ok(collection);
            }

            return Unauthorized("Invalid collection.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting users");
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}