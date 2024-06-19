using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi_dotnet_core.Data;
using webapi_dotnet_core.Dont_tansfer_Objects;
using webapi_dotnet_core.Models;

namespace webapi_dotnet_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Dappercontext _dapper;
        public UserController(IConfiguration config) {
            _dapper = new Dappercontext(config);
        }
        [HttpGet("/user")]

        public IEnumerable<Usermodel> GetUser() {
            String sql = "select * from Dotnet.Usertab";
            return _dapper.LoadData<Usermodel>(sql);    
        }

        [HttpGet("/singleuser/{userid}")]
        public Usermodel GetUsersingle(int userid)
        {

            String sql = "select * from Dotnet.Usertab where UserId=" + userid;
            return _dapper.LoadDataSingle<Usermodel>(sql);
           
         
        }
        [HttpPut("/Updateuser/{userid}")]
        public IActionResult Updateuser(int userid,Usermodel user)
        {
            String sql = @"UPDATE  Dotnet.Usertab SET 
                            UserName='"+ user.Username+ "'," +
                            "Email='" + user.Email+ "', " +
                            "Age = '" + user.Age + "'," +
                            " Active = '" + user.Active+"' " +
                            "where UserId="+userid;
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("failed to update user");
           
        }
        [HttpPost("/Adduser")]
        public IActionResult Adduser(DtosTOadduser user)
        {
            String sql = @"Insert Into  Dotnet.Usertab Values(
                            '" + user.Username + "'," +
                            "'" + user.Email + "', '" +
                            user.Age + "','" +
                            user.Active + "')";
                        
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("failed to add user");

        }
        [HttpDelete("/Deleteuser/{userid}")]

        public IActionResult DeleteData(int userid)
        {
            string sql = "DELETE FROM Dotnet.Usertab where UserId="+ userid;
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("failed to delete user");
        }



    }
}
