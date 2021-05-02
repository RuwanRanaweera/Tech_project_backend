using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly string _connectionString = "Data Source = DESKTOP-ECML7CE\\SQLEXPRESS; Initial Catalog=gemDB; User id=sa; Password=1234;";


        [HttpPost("insert")]
        public async Task<ActionResult> insert(Admin data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))

                {
                    DynamicParameters para = new DynamicParameters();
                    String passHash = test.CreateMD5(data.password);
                    para.Add("@name", data.name);
                    para.Add("@email", data.email);
                    para.Add("@password", passHash);
                   
           


                    var result = await connection.QueryAsync("[dbo].[InsertAdmin]", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = "Action will be canceled!", errorType = "EX" });
            }
        }
    }





    public class Admin
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
     
    }

}
