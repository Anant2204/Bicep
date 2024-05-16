namespace AACN.API.Controllers
{
    using AACN.API.Model;
    using AACN.API.Service;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Threading.Tasks;


    [Route("api/")]
    [ApiController]
    public class assessmentLineOptionController : Controller
    {
        AssessmentLineOptionService assessmentLineOptionService = new AssessmentLineOptionService();
        [HttpGet("assessmentLineOption")]
        public ActionResult<List<AssessmentLineOptionModelData>> GetassessmentLineOption(string assessmentLineOptionId)
        {
            AssessmentLineOptionModel assessmentLineOptionModel = new AssessmentLineOptionModel();
            if (assessmentLineOptionId != null)
            {
                assessmentLineOptionModel = assessmentLineOptionService.getAssessmentLineOptionbyId(assessmentLineOptionId);
            }
            else
            {
                return ValidationProblem("Missing AssessmentLineOptionId");
            }
            if (assessmentLineOptionModel.value == null || (assessmentLineOptionModel.value != null && assessmentLineOptionModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return assessmentLineOptionModel.value;
        }


        #region UpsertAPI
        //[HttpPost("upsertAssessmentLineOption")]
        //public ActionResult<APIResponse> UpsertAssessmentLineOption([FromBody] PostAssessMentLineOption postAssessMentLineOption, string assessmentLineOptionId)
        //{
        //    var responseData = assessmentLineOptionService.UpsertAssessmentLineOption(postAssessMentLineOption, assessmentLineOptionId);
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

        [HttpPost("assessmentLineOption")]
        public ActionResult<APIResponse> createAssessmentLineOption([FromBody] PostAssessMentLineOption postAssessMentLineOption)
        {
            var result = assessmentLineOptionService.createAssessmentLineOption(postAssessMentLineOption);
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

        [HttpPatch("assessmentLineOption")]
        public ActionResult<APIResponse> UpdateAssessmentLineOption([FromBody] PostAssessMentLineOption postAssessMentLineOption)
        {
            if (postAssessMentLineOption.assessment_Line_OptionId == null)
            {
                return ValidationProblem("Please provide assessmentLineOptionId for Update!");
            }
            var result = assessmentLineOptionService.updateAssessmentLineOption(postAssessMentLineOption, postAssessMentLineOption.assessment_Line_OptionId);
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
