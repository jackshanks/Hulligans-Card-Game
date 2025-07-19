using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using web_service.Models;

namespace web_service.Controllers;

[ApiController]
[Route("api/[controller]")] // Sets the base route to /api/users
public class UsersController(NpgsqlDataSource dataSource, ILogger<UsersController> logger) : ControllerBase
{
    // This method will handle GET requests to /api/users/id
    [HttpGet("id")]
    public async Task<IActionResult> GetUsers(string email, string auth)
    {
        try
        {
           //Connection to DB
            await using var connection = await dataSource.OpenConnectionAsync();
            const string sql = "SELECT user_id AS UserId, display_name AS DisplayName, email, auth FROM public.users WHERE email = @email";
            // Dapper automatically maps the columns to your user model
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { email });

            if (user != null && user.Auth == auth)
            {
                return Ok(user);
            }

            return Unauthorized("Invalid auth or email.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting users");
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}