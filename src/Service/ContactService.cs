namespace AACN.API.Service
{
    using AACN.API.Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AACN.Services;
    using System.Net;

    public class ContactService : BaseService
    {
        public ContactModel getMeberById(string memberId)
        {
            ContactModel contactModel = new ContactModel();
            string query = string.Format(Utility.GetMemberById, memberId);
            HttpResponseMessage memberResponse = GetRecords(query);
            if (memberResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(memberResponse.Content.ReadAsStringAsync().Result))
                {
                    contactModel = JsonConvert.DeserializeObject<ContactModel>(Utility.RemoveJsonNulls(memberResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return contactModel;
        }

        public HttpResponseMessage createMember(PostMember member)
        {
            HttpResponseMessage memberResponse = null;
            try
            {
                string odataQuery = "contacts";
                memberResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(member));
                if (memberResponse.StatusCode == HttpStatusCode.NoContent)//204
                {
                    string _recordUrl = memberResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                    string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                    memberResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                }
                else
                {
                    memberResponse.Content = new StringContent(memberResponse.Content.ReadAsStringAsync().Result);
                    memberResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }

                return memberResponse;
            }
            catch (Exception ex)
            {
                memberResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public HttpResponseMessage updateMember(PostMember member, string memberId)
        {
            HttpResponseMessage memberResponse = null;
            try
            {
                if (memberId != null)
                {
                    string odataQuery = "contacts(" + memberId + ")";
                    memberResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(member));
                    if (memberResponse.StatusCode == HttpStatusCode.NoContent)//204
                    {
                        string _recordUrl = memberResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        memberResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                    }
                    else
                    {
                        memberResponse.Content = new StringContent(memberResponse.Content.ReadAsStringAsync().Result);
                        memberResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }

                return memberResponse;
            }
            catch (Exception ex)
            {
                memberResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public APIResponse UpsertMember(PostMember member, string memberId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(memberId))
                {
                    ContactModel contactModel = getMeberById(memberId);
                    if (contactModel.value != null && contactModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateMember(member, memberId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createMember(member);
                }
                if (answerResponse != null)
                {
                    if (answerResponse.StatusCode == HttpStatusCode.OK)
                    {
                        if (recordAvailable == false)
                        {
                            apiResponse.Status = "Created";
                        }
                        else
                        {
                            apiResponse.Status = "Updated";
                        }
                        string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        apiResponse.RecordId = new Guid(splitRetrievedData[1]);
                        apiResponse.StatusCode = (int)answerResponse.StatusCode;
                        return apiResponse;
                    }
                    else
                    {
                        apiResponse.RecordId = Guid.Empty;
                        apiResponse.Status = answerResponse.Content.ReadAsStringAsync().Result;
                        apiResponse.StatusCode = (int)answerResponse.StatusCode;
                    }

                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse.Status = ex.Message;
                apiResponse.RecordId = Guid.Empty;
                apiResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }
    }
}
