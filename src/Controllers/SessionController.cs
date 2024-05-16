namespace AACN.API.Controllers
{
    using AACN.API.Model;
    using AACN.API.Service;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/")]
    [ApiController]
    public class sessionController : Controller
    {
        SessionService sessionService = new SessionService();
        [HttpGet("session")]
        public ActionResult<List<SessionData>> GetSessionbyId(string sessionId)
        {
            SessionModel sessionModel = new SessionModel();
            if (sessionId != null)
            {
                sessionModel = sessionService.getSessionbyId(sessionId);
            }
            else
            {
                return ValidationProblem("Missing SessionId");
            }
            if (sessionModel.value == null || (sessionModel.value != null && sessionModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return sessionModel.value;
        }

        #region UpsertAPI
        //[HttpPost("upsertSession")]
        //public ActionResult<APIResponse> UpsertSession([FromBody] PostSession sessiondata, string sessionId)
        //{
        //    var responseData = sessionService.UpsertSession(sessiondata, sessionId);
        //    if (responseData.RecordId != null && responseData.RecordId != Guid.Empty)
        //    {
        //        return new APIResponse { RecordId = responseData.RecordId, StatusCode = responseData.StatusCode, Status = responseData.Status };
        //    }
        //    else
        //    {
        //        return BadRequest(new APIResponse
        //        { RecordId = Guid.Empty, StatusCode = responseData.StatusCode, Status = responseData.Status });
        //    }
        //}

        #endregion


        [HttpPost("session")]
        public ActionResult<APIResponse> CreateSession([FromBody] PostSession sessiondata)
        {
            var result = sessionService.CreateSession(sessiondata);
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                string _recordUrl = result.Headers.GetValues("OData-EntityId").FirstOrDefault();
                string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                return new APIResponse { RecordId = new Guid(splitRetrievedData[1].ToString()), StatusCode = (int)result.StatusCode, Status = "Created" };
                //return Ok(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return BadRequest(new APIResponse
                { RecordId = Guid.Empty, StatusCode = (int)result.StatusCode, Status = result.Content.ReadAsStringAsync().Result });
            }
        }

        [HttpPatch("session")]
        public ActionResult<APIResponse> updateSession([FromBody] PostSession sessiondata)
        {
            if (sessiondata.sessionId == null)
            {
                return ValidationProblem("Please provide SessionId for Update!");
            }
            var result = sessionService.updateSession(sessiondata, sessiondata.sessionId);
            if(result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                string _recordUrl = result.Headers.GetValues("OData-EntityId").FirstOrDefault();
                string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                return new APIResponse { RecordId = new Guid(splitRetrievedData[1].ToString()), StatusCode = (int)result.StatusCode, Status = "Updated" };
                //return Ok(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return BadRequest(new APIResponse
                { RecordId = Guid.Empty, StatusCode = (int)result.StatusCode, Status = result.Content.ReadAsStringAsync().Result });
            }
        }


    }
}
