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
    public class responseController : Controller
    {
        ResponseService responseService = new ResponseService();
        [HttpGet("response")]
        public ActionResult<List<ResponseData>> GetResponsebyId(string responseId)
        {
            ResponseModel responseModel = new ResponseModel();
            if (responseId != null)
            {
                responseModel = responseService.getResponsebyId(responseId);
            }
            else
            {
                return ValidationProblem("Missing ResponseId");
            }
            if (responseModel.value == null || (responseModel.value != null && responseModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return responseModel.value;
        }


        #region UpsertAPI
        //[HttpPost("upsertResponse")]
        //public ActionResult<APIResponse> UpsertMember([FromBody] PostResponse postResponse, string responseId)
        //{
        //    var responseData = responseService.UpsertResponse(postResponse, responseId);
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

        [HttpPost("response")]
        public ActionResult<APIResponse> createResponse([FromBody] PostResponse postResponse)
        {
            var result = responseService.createResponse(postResponse);
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

        [HttpPatch("response")]
        public ActionResult<APIResponse> updateResponse([FromBody] PostResponse _postResponse)
        {
            if (_postResponse.responseId == null)
            {
                return ValidationProblem("Please provide ResponseId for Update!");
            }
            var result = responseService.updateResponse(_postResponse, _postResponse.responseId);
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

        [HttpPost("[controller]/createResponseData")]
        public ActionResult<APIResponse> createResponseData([FromBody] PostResponseData PostResponseData)
        {
            var result = responseService.createResponseData(PostResponseData);

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


    }
}
