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
    public class responseLineController : Controller
    {
        ResponseLineService _ResponseLineService = new ResponseLineService();
        [HttpGet("responseLine")]
        public ActionResult<List<ResponeLineData>> GetAssessmentbyId(string responseLineId)
        {
            ResponseLineModel _ResponseLineModel = new ResponseLineModel();
            if (responseLineId != null)
            {
                _ResponseLineModel = _ResponseLineService.getResponseLineById(responseLineId);
            }
            else
            {
                return ValidationProblem("Missing ResponseLineId");
            }
            if (_ResponseLineModel.value == null || (_ResponseLineModel.value != null && _ResponseLineModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return _ResponseLineModel.value;
        }


        #region UpsertAPI
        //[HttpPost("upsertResponseLine")]
        //public ActionResult<APIResponse> UpsertResponseLine([FromBody] PostResponseLine postResponseLine, string responseLineId)
        //{
        //    var responseData = _ResponseLineService.UpsertResponseLine(postResponseLine, responseLineId);
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

        [HttpPost("responseLine")]
        public ActionResult<APIResponse> CreateResponseLine([FromBody] PostResponseLine responeLine)
        {
            var result = _ResponseLineService.CreateResponseLine(responeLine);
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

        [HttpPatch("responseLine")]
        public ActionResult<APIResponse> UpdateResponseLine([FromBody] PostResponseLine postResponseLine)
        {
            if (postResponseLine.response_LineId == null)
            {
                return ValidationProblem("Please provide ResponseLineId for Update!");
            }
            var result = _ResponseLineService.updateResponseLine(postResponseLine, postResponseLine.response_LineId);
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
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
