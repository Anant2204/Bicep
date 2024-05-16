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
    public class eventController : Controller
    {
        EventService _eventService = new EventService();
        [HttpGet("event")]
        public ActionResult<List<EventData>> GetEventById(string eventId)
        {
            EventModel _event = new EventModel();
            if (eventId != null)
            {
                _event = _eventService.getEventById(eventId);
            }
            else
            {
                return ValidationProblem("Missing eventId");
            }
            if (_event.value == null || (_event.value != null && _event.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return _event.value;
        }


        #region upsertAPI
        //[HttpPost("upsertEvent")]
        //public ActionResult<APIResponse> UpsertEvent([FromBody] PostEvent eventObject, string eventId)
        //{
        //    var responseData = _eventService.UpsertEvent(eventObject, eventId);
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
        [HttpPost("event")]
        public ActionResult<APIResponse> CreateEvent([FromBody] PostEvent eventObject)
        {
            var result = _eventService.createEvent(eventObject);
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

        [HttpPatch("event")]
        public ActionResult<APIResponse> UpdateEvent([FromBody] PostEvent eventObject, string eventId)
        {
            if (eventObject.eventId == null)
            {
                return ValidationProblem("Please provide eventId for Update!");
            }
            var result = _eventService.updateEvent(eventObject, eventObject.eventId);
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