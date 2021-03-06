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
    public class GemController : ControllerBase
    {
        readonly string _connectionString = "Data Source = DESKTOP-ECML7CE\\SQLEXPRESS; Initial Catalog=gemDB; User id=sa; Password=1234;";


        [HttpGet("Select/{id}")]
        public async Task<ActionResult> Select(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                    para.Add("@id", id);

                    var result = await connection.QueryAsync<Gem>("SelectGem", para, commandType: CommandType.StoredProcedure);

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

        [HttpGet("Select")]
        public async Task<ActionResult> Select()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                   

                    var result = await connection.QueryAsync<Gem>("SelectGems", para, commandType: CommandType.StoredProcedure);

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




        [HttpPost("insert")]
        public async Task<ActionResult> insert(Gem data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
              
                    para.Add("@gemName", data.gemName);
                    para.Add("@gemDescription", data.gemDescription);
                    para.Add("@price", data.price);
                    para.Add("@gemBid", data.gemBid);
                    para.Add("@gemImage", data.gemImage);
                    para.Add("@userId", data.userId);
                    para.Add("@approve", data.approve);
                    para.Add("@certificate", data.certificate);


                    var result = await connection.QueryAsync("[dbo].[InsertGem]", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = "Insert will be canceled!", errorType = "EX" });
            }
        }




        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(Gem data, int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@gemID", id);



                    var result = await connection.QueryAsync<Gem>("UpdateGem", para, commandType: CommandType.StoredProcedure);

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


        [HttpPut("Updates/{id}")]
        public async Task<ActionResult> Updates( int id,Gem data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@gemID", id);
                    para.Add("@price", data.price);
                    para.Add("@bidingTime", data.bidingTime);




                    var result = await connection.QueryAsync<Gem>("UpdateGem2", para, commandType: CommandType.StoredProcedure);

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


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                    para.Add("@id", id);

                    var result = await connection.QueryAsync("[dbo].[DeleteGem]", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = "Delete will be canceled!", errorType = "EX" });
            }
        }



    }
    public class Gem
    {
        public int gemID { get; set; }
        public string gemName { get; set; }
        public string gemDescription { get; set; }
        public int price  { get; set; }
        public bool gemBid { get; set; }
        public string gemImage { get; set; }
        public int userId { get; set; }
        public bool approve { get; set; }
        public string certificate { get; set; }
        public DateTime bidingTime { get; set; }
    }

}

