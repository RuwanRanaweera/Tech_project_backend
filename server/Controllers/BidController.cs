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
    public class BidController : ControllerBase
    {
        readonly string _connectionString = "Data Source = DESKTOP-ECML7CE\\SQLEXPRESS; Initial Catalog=gemDB; User id=sa; Password=1234;";


        [HttpPost("insert")]
        public async Task<ActionResult> insert(Bid data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))

                {
                    DynamicParameters para = new DynamicParameters();
                  
                    para.Add("@userId", data.userId);
                    para.Add("@gemID", data.gemID);
                    para.Add("@bidValue", data.bidValue);




                    var result = await connection.QueryAsync("[dbo].[InsertBid]", para, commandType: CommandType.StoredProcedure);

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



        [HttpGet("Select/{gemID},{userId}")]
        public async Task<ActionResult> Select(int gemID,int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                    para.Add("@gemID", gemID);
                    para.Add("@userId", userId);

                    var result = await connection.QueryAsync<Gem>("SelectBid", para, commandType: CommandType.StoredProcedure);

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


        [HttpPut("Update/{gemID},{userId}")]
        public async Task<ActionResult> Update(int gemID, int userId,Bid data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@gemID", gemID);
                    para.Add("@userId", userId);
                    para.Add("@bidValue", data.bidValue);




                    var result = await connection.QueryAsync<Gem>("UpdateBid", para, commandType: CommandType.StoredProcedure);

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

    public class Bid
    {
        public int BidId { get; set; }
        public int userId { get; set; }
        public int gemID { get; set; }
        public int bidValue { get; set; }
        public int max { get; set; }

    }
}

