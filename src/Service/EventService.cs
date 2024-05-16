namespace AACN.API.Service
{
    using AACN.API.Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net;
    using System.Threading.Tasks;
    using AACN.Services;

    public class EventService : BaseService
    {
        public EventModel getEventById(string eventId)
        {
            EventModel eventModel = new EventModel();
            string query = string.Format(Utility.GetEventById, eventId);
            HttpResponseMessage memberResponse = GetRecords(query);
            if (memberResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(memberResponse.Content.ReadAsStringAsync().Result))
                {
                    eventModel = JsonConvert.DeserializeObject<EventModel>(Utility.RemoveJsonNulls(memberResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return eventModel;
        }

        public HttpResponseMessage createEvent(PostEvent eventObject)
        {
            HttpResponseMessage eventResponse = new HttpResponseMessage();
            try
            {
                string odataQuery = "aacn_events";
                if (!string.IsNullOrEmpty(eventObject.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(eventObject.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        eventObject.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                    else
                    {
                        eventResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        eventResponse.Content = new StringContent("Survey Provider does not exists with name - " + eventObject.survey_Provider_Name);
                        return eventResponse;
                    }
                }
                eventResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(eventObject));
                if (eventResponse.StatusCode == HttpStatusCode.NoContent)//204
                {
                    string _recordUrl = eventResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                    string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                    eventResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                }
                else
                {
                    eventResponse.Content = new StringContent(eventResponse.Content.ReadAsStringAsync().Result);
                    eventResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }

                return eventResponse;
            }
            catch (Exception ex)
            {
                eventResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public HttpResponseMessage updateEvent(PostEvent eventObject, string eventId)
        {
            HttpResponseMessage eventResponse = new HttpResponseMessage();
            try
            {
                if (eventId != null)
                {
                    if (!string.IsNullOrEmpty(eventObject.survey_Provider_Name))
                    {
                        string survey_Provider_ID = getSurveryProviderByName(eventObject.survey_Provider_Name);
                        if (!string.IsNullOrEmpty(survey_Provider_ID))
                        {
                            eventObject.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                        }
                        else
                        {
                            eventResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                            eventResponse.Content = new StringContent("Survey Provider does not exists with name - " + eventObject.survey_Provider_Name);
                            return eventResponse;
                        }
                    }
                    string odataQuery = "aacn_events(" + eventId + ")";
                    eventResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(eventObject));
                    if (eventResponse.StatusCode == HttpStatusCode.NoContent)//204
                    {
                        string _recordUrl = eventResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        eventResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                    }
                    else
                    {
                        eventResponse.Content = new StringContent(eventResponse.Content.ReadAsStringAsync().Result);
                        eventResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }

                return eventResponse;
            }
            catch (Exception ex)
            {
                eventResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public APIResponse UpsertEvent(PostEvent eventObject, string eventId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(eventObject.survey_Provider_Name))
                {
                    //string survey_Provider_ID = getSurveryProviderByName(eventObject.survey_Provider_Name);
                    //if (!string.IsNullOrEmpty(survey_Provider_ID))
                    //{
                    //    eventObject.survey_Provider = $"/aacn_survey_providers({survey_Provider_ID})";
                    //}
                }

                if (!string.IsNullOrEmpty(eventId))
                {
                    EventModel eventModel = getEventById(eventId);
                    if (eventModel.value != null && eventModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateEvent(eventObject, eventId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createEvent(eventObject);
                }
                if (answerResponse != null)
                {
                    if (answerResponse.StatusCode == HttpStatusCode.NoContent)
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
