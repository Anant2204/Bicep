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
    public class speakerController : Controller
    {
        SpeakerService speakerService = new SpeakerService();
        [HttpGet("speaker")]
        public ActionResult<List<speakerData>> GetResponsebyId(string speakerId)
        {
            SpeakerModel speakerModel = new SpeakerModel();
            if (speakerId != null)
            {
                speakerModel = speakerService.getSpeakerbyId(speakerId);
            }
            else
            {
                return ValidationProblem("Missing SpeakerId");
            }
            if (speakerModel.value == null || (speakerModel.value != null && speakerModel.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return speakerModel.value;
        }


        //#region UpsertAPI
        //[HttpPost("upsertSpeaker")]
        //public ActionResult<APIResponse> UpsertSpeaker([FromBody] PostSpeaker postSpeaker, string speakerId)
        //{
        //    var result = speakerService.UpsertSpeaker(postSpeaker, speakerId);
        //    if (result.RecordId != null)
        //    {
        //        return new APIResponse { RecordId = result.RecordId, StatusCode = result.StatusCode, Status = result.Status };

        //    }
        //    else
        //    {
        //        return BadRequest(new APIResponse
        //        { RecordId = Guid.Empty, StatusCode = (int)result.StatusCode, Status = result.Status });
        //    }
        //}

        //#endregion

        [HttpPost("speaker")]
        public ActionResult<APIResponse> createSpeaker([FromBody] PostSpeaker postSpeaker)
        {
            var result = speakerService.createSpeaker(postSpeaker);
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

        [HttpPatch("speaker")]
        public ActionResult<APIResponse> updateSpeaker([FromBody] PostSpeaker postSpeaker)
        {
            if (postSpeaker.speakerId == null)
            {
                return ValidationProblem("Please provide SpeakerId for Update!");
            }
            var result = speakerService.updateSpeaker(postSpeaker, postSpeaker.speakerId);
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
