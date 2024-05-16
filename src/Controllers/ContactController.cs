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
    public class contactController : Controller
    {
        ContactService contactService = new ContactService();
        [HttpGet("member")]
        public ActionResult<List<contactData>> GetMemberById(string memberId)
        {
            ContactModel _contact = new ContactModel();
            if (memberId != null)
            {
                _contact = contactService.getMeberById(memberId);
            }
            else
            {
                return ValidationProblem("Missing Member Id");
            }
            if (_contact.value == null || (_contact.value != null && _contact.value.Count == 0))
            {
                return ValidationProblem("No Data Found!");
            }
            return _contact.value;
        }

        #region UpsertAPI
        //[HttpPost("upsertMember")]
        //public ActionResult<APIResponse> UpsertMember([FromBody] PostMember memberData, string memberId)
        //{
        //    var responseData = contactService.UpsertMember(memberData, memberId);
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


        [HttpPost("member")]
        public ActionResult<APIResponse> CreateMember([FromBody] PostMember memberData)
        {
            var result = contactService.createMember(memberData);
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

        [HttpPatch("member")]
        public ActionResult<APIResponse> UpdateMember([FromBody] PostMember memberData)
        {
            if (memberData.memberId == null)
            {
                return ValidationProblem("Please provide MebmberId for Update!");
            }
            var result = contactService.updateMember(memberData, memberData.memberId);
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
