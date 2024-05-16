namespace AACN.API.Controllers
{
    using AACN.API.Model;
    using AACN.API.Service;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    [Route("api/")]
    [ApiController]
    public class assessmentController : Controller
    {
        private readonly AssessmentService _assessmentService = new AssessmentService();
        private readonly ILogger _logger;
        public assessmentController(ILogger<assessmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("assessment")]
        public ActionResult<List<Assessmentdata>> GetAssessmentbyId(string assessmentId)
        {
            AssessmentModel _assessmentModel = new AssessmentModel();
            if (assessmentId != null)
            {
                _assessmentModel = _assessmentService.getAssessmentbyId(assessmentId);
            }
            else
            {
                return ValidationProblem("Missing AssessmentId");
            }
            if (_assessmentModel.value == null || (_assessmentModel.value != null && _assessmentModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return _assessmentModel.value;
        }

        [HttpGet("[controller]/getAssessmentDetailsByNumber")]
        public ActionResult<List<Assessments>> getAssessmentDetailsByNumber(string assessmentNumber)
        {
            AssessmentRootNew assessmentRoot = new AssessmentRootNew();
            if (assessmentNumber != null)
            {
                assessmentRoot = _assessmentService.getAssessmentDetailsByNumber(assessmentNumber);
            }
            else
            {
                return ValidationProblem("Missing AssessmentNumber");
            }
            if (assessmentRoot.value == null || (assessmentRoot.value != null && assessmentRoot.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }

            return assessmentRoot.value;
        }

        //#region upsertAPI
        //[HttpPost("upsertAssessment")]
        //public ActionResult<APIResponse> UpsertAnswer([FromBody] PostAssessment postAssessment, string assessmentId)
        //{
        //    var responseData = _assessmentService.UpsertAssessment(postAssessment, assessmentId);
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
        //#endregion

        [HttpPost("assessment")]
        public ActionResult<APIResponse> CreateAssesment([FromBody] PostAssessment assessmentData)
        {
            var result = _assessmentService.createAssessment(assessmentData);
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

        [HttpPatch("assessment")]
        public ActionResult<APIResponse> UpdateAssesment([FromBody] PostAssessment assessmentData)
        {
            if (assessmentData.assessment_Id == null)
            {
                return ValidationProblem("Please provide AssesmentId for Update!");
            }

            var result = _assessmentService.updateAssessment(assessmentData, assessmentData.assessment_Id);
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
