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
    public class assessmentLineController : Controller
    {
        AssessmentLineService _assessmentLineService = new AssessmentLineService();
        [HttpGet("assessmentLine")]
        public ActionResult<List<AssessmentLineModelData>> GetAssessmentLinebyId(string assessmentLineId)
        {
            AssessmentLineModel assessmentLineModel = new AssessmentLineModel();
            if (assessmentLineId != null)
            {
                assessmentLineModel = _assessmentLineService.getAssessmentlinebyId(assessmentLineId);
            }
            else
            {
                return ValidationProblem("Missing AssessmentLineId");
            }
            if (assessmentLineModel.value == null || (assessmentLineModel.value != null && assessmentLineModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return assessmentLineModel.value;
        }

        #region UpsertAPI
        //[HttpPost("upsertAssessmentLine")]
        //public ActionResult<APIResponse> UpsertAssesmentLine([FromBody] PostAssessMentLine postAssessMentLine, string assessmentLineId)
        //{
        //    var responseData = _assessmentLineService.UpsertAssessmentLine(postAssessMentLine, assessmentLineId);
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

        [HttpPost("assessmentLine")]
        public ActionResult<APIResponse> CreateAssesmentLine([FromBody] PostAssessMentLine postAssessMentLine)
        {
            var result = _assessmentLineService.CreateAssesmentLine(postAssessMentLine);
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
                //return BadRequest(result.Content.ReadAsStringAsync().Result);

            }
        }

        [HttpPatch("assessmentLine")]
        public ActionResult<APIResponse> UpdateAssesmentLine([FromBody] PostAssessMentLine assessmentlineData, string assessmentLineId)
        {

            if (assessmentlineData.assessment_LineId == null)
            {
                return ValidationProblem("Please provide AssesmentLineId for Update!");
            }
            var result = _assessmentLineService.updateAssessmentLine(assessmentlineData, assessmentlineData.assessment_LineId);
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                string _recordUrl = result.Headers.GetValues("OData-EntityId").FirstOrDefault();
                string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                return new APIResponse { RecordId = new Guid(splitRetrievedData[1].ToString()), StatusCode = (int)result.StatusCode, Status = "Updated" };

            }
            else
            {
                return BadRequest(new APIResponse
                { RecordId = Guid.Empty, StatusCode = (int)result.StatusCode, Status = result.Content.ReadAsStringAsync().Result });
            }
        }

    }
}
