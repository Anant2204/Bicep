namespace AACN.API.Service
{
    using AACN.API.Model;
    using AACN.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class SessionService : BaseService
    {
        public SessionModel getSessionbyId(string sessionId)
        {
            SessionModel sessionModel = new SessionModel();
            string query = string.Format(Utility.getSessionbyId, sessionId);
            HttpResponseMessage sessionResponse = GetRecords(query);
            if (sessionResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(sessionResponse.Content.ReadAsStringAsync().Result))
                {
                    sessionModel = JsonConvert.DeserializeObject<SessionModel>(Utility.RemoveJsonNulls(sessionResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return sessionModel;
        }

        public HttpResponseMessage CreateSession(PostSession session)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                string odataQuery = "aacn_sessions";
                if (!string.IsNullOrEmpty(session.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(session.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        session.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                    else
                    {
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + session.survey_Provider_Name);
                        return answerResponse;
                    }
                }
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(session));
                if (answerResponse.StatusCode == HttpStatusCode.NoContent)//204
                {
                    string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                    string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                    answerResponse.StatusCode = System.Net.HttpStatusCode.NoContent; ;
                }
                else
                {
                    answerResponse.Content = new StringContent(answerResponse.Content.ReadAsStringAsync().Result);
                    answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }

                return answerResponse;
            }
            catch (Exception ex)
            {
                answerResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public HttpResponseMessage updateSession(PostSession session, string sessionId)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                if (sessionId != null)
                {
                    if (!string.IsNullOrEmpty(session.survey_Provider_Name))
                    {
                        string survey_Provider_ID = getSurveryProviderByName(session.survey_Provider_Name);
                        if (!string.IsNullOrEmpty(survey_Provider_ID))
                        {
                            session.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                        }
                        else
                        {
                            answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                            answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + session.survey_Provider_Name);
                            return answerResponse;
                        }
                    }
                    string odataQuery = "aacn_sessions(" + sessionId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(session));
                    if (answerResponse.StatusCode == HttpStatusCode.NoContent)//204
                    {
                        string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        answerResponse.StatusCode = System.Net.HttpStatusCode.NoContent; ;
                    }
                    else
                    {
                        answerResponse.Content = new StringContent(answerResponse.Content.ReadAsStringAsync().Result);
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }

                return answerResponse;
            }
            catch (Exception ex)
            {
                answerResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public APIResponse UpsertSession(PostSession session, string sessionId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(session.survey_Provider_Name))
                {
                    //string survey_Provider_ID = getSurveryProviderByName(session.survey_Provider_Name);
                    //if (!string.IsNullOrEmpty(survey_Provider_ID))
                    //{
                    //    session.survey_Provider = $"/aacn_survey_providers({survey_Provider_ID})";
                    //}
                }
                if (!string.IsNullOrEmpty(sessionId))
                {
                    SessionModel sessionModel = getSessionbyId(sessionId);
                    if (sessionModel.value != null && sessionModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateSession(session, sessionId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = CreateSession(session);
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
