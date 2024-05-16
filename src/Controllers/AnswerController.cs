namespace AACN.API.Controllers
{
    using AACN.API.Model;
    using AACN.API.Service;
    using AACN.Services;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    [Route("api/")]
    [ApiController]
    public class answerController : Controller
    {
        AnswerService _answerService = new AnswerService();
        [HttpGet("answer")]
        public ActionResult<List<AnswerData>> GetAnswerById(string answerId)
        {
            AnswerModel _answerModel = new AnswerModel();
            if (answerId != null)
            {
                _answerModel = _answerService.getAnswerbyId(answerId);
            }
            else
            {
                return ValidationProblem("Missing Anser Id");
            }
            if (_answerModel.value == null || (_answerModel.value != null && _answerModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return _answerModel.value;
        }

        #region upsertAPI
        //[HttpPost("upsertAnswer")]
        //public ActionResult<APIResponse> UpsertAnswer([FromBody] PostAnswer answerData, string answerId)
        //{
        //    var responseData = _answerService.UpsertAnswer(answerData, answerId);
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

        [HttpPost("answer")]
        public ActionResult<APIResponse> CreateAnswer([FromBody] PostAnswer answerData)

        {
            var result = _answerService.createAnswer(answerData);
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

        [HttpPatch("answer")]
        public ActionResult<APIResponse> UpdateAnswer([FromBody] PostAnswer answerData)
        {
            if (answerData.answerId == null)
            {
                return ValidationProblem("Please provide answerId for Update!");
            }

            var result = _answerService.updateAnswer(answerData, answerData.answerId);
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
