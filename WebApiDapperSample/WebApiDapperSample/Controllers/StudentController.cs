using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;

namespace WebApiDapperSample.Controllers
{
    public class StudentController : ApiController
    {

        private IDbConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);


        [HttpGet] 
        // GET api/values
        public HttpResponseMessage Get()
        {
            try
            {
                List<clsStudents> _getAllStudents = dbConnection.Query<clsStudents>("select RID,Firstname, Lastname, MobileNo FROM TBL_STUDENTS WITH(NOLOCK)").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, _getAllStudents);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error in delete:" + ex.Message);
            }
        }

        [HttpGet]
        // GET api/values/5
        public HttpResponseMessage Get(long id)
        {
            try
            {
                clsStudents _getByStudents = dbConnection.Query<clsStudents>("select RID,Firstname, Lastname, MobileNo FROM TBL_STUDENTS  WITH(NOLOCK) WHERE RID =" + id).SingleOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK, _getByStudents);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error in delete:" + ex.Message);
            }


        }

        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody]clsStudents _students)
        {
            try
            {
                if (_students == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input.");
                }
                else
                {
                    int rowsAffected = dbConnection.Execute(@"INSERT TBL_STUDENTS(Firstname,LastName,MobileNo) values (@FN, @LN, @MobileNo)", new { FN = _students.FirstName, LN = _students.LastName, MobileNo = _students.MobileNo });

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "Values are created.");
                    }
                    else return Request.CreateResponse(HttpStatusCode.NotImplemented, "Values are created.");
                }



            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error in Create:" + ex.Message);
            }

        }

        [HttpPut]
        // PUT api/values/5
        public HttpResponseMessage Put(long id, [FromBody]clsStudents _students)
        {
            try
            {
                if (_students == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input.");
                }
                else
                {
                    int rowsAffected = dbConnection.Execute(@"Update TBL_STUDENTS set FirstName=@FN, LastName=@LN, MobileNo=@MBN where RID= " +id, new { FN = _students.FirstName, LN = _students.LastName, MBN = _students.MobileNo });

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Values are updates.");
                    }
                    else return Request.CreateResponse(HttpStatusCode.NotModified, "Values are not Modified.");
                }



            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error in Update:" + ex.Message);
            }
        }

        [HttpDelete]
        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int rowsAffected = dbConnection.Execute(@"Delete from TBL_STUDENTS  where RID= " + id);

                if (rowsAffected > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Values are deleted.");
                }
                else return Request.CreateResponse(HttpStatusCode.NotModified, "Values are not deleted.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error in delete:" + ex.Message);
            }

        }
   
    }



    public class clsStudents
    {
        public Int64 RID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
    }

}