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
    public class questionController : Controller
    {
        QuestionService _questionService = new QuestionService();
        [HttpGet("question")]
        public ActionResult<List<QuestionData>> GetAnswerById(string questionId)
        {
            QuestionModel questionModel = new QuestionModel();
            if (questionId != null)
            {
                questionModel = _questionService.getQuestionById(questionId);
            }
            else
            {
                return ValidationProblem("Missing QuestionId");
            }
            if (questionModel.value == null || (questionModel.value != null && questionModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return questionModel.value;
        }


        #region UpsertAPI
        //[HttpPost("upsertQuestion")]
        //public ActionResult<APIResponse> UpsertMember([FromBody] PostQuestion postQuestion, string questionId)
        //{
        //    var responseData = _questionService.UpsertQuestion(postQuestion, questionId);
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
        [HttpPost("question")]
        public ActionResult<APIResponse> createQuestion([FromBody] PostQuestion postQuestion)
        {
            var result = _questionService.createQuestion(postQuestion);
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

        [HttpPatch("question")]
        public ActionResult<APIResponse> updateQuestion([FromBody] PostQuestion postQuestion, string questionId)
        {
            if (postQuestion.question_Id == null)
            {
                return ValidationProblem("Please provide QuestionId for Update!");
            }
            var result = _questionService.updateQuestion(postQuestion, postQuestion.question_Id);
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
