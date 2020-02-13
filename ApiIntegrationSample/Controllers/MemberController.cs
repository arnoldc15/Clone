using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace ApiIntegrationSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void Post(Member member)
        {
            using (var connection = new SqlConnection(
                @"Server=cbhs-dbdev02;Database=loggingclone;Trusted_Connection=True;"))
            {
                var command =
                    new SqlCommand(
                        $"INSERT INTO [dbo].[TestLog] ([EventClass],[TextData]) VALUES ({member.MemberId},'{member.Name}')",
                        connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "it works!";
        }

    }
}