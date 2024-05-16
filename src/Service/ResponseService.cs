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

    public class ResponseService : BaseService
    {
        public ResponseModel getResponsebyId(string ResponseId)
        {
            ResponseModel responseModel = new ResponseModel();
            string query = string.Format(Utility.getResponsebyId, ResponseId);
            HttpResponseMessage answerResponse = GetRecords(query);
            if (answerResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(answerResponse.Content.ReadAsStringAsync().Result))
                {
                    responseModel = JsonConvert.DeserializeObject<ResponseModel>(Utility.RemoveJsonNulls(answerResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }
            }
            return responseModel;
        }

        public HttpResponseMessage createResponse(PostResponse postResponse)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                string odataQuery = "aacn_responses";
                if (!string.IsNullOrEmpty(postResponse.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(postResponse.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        postResponse.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                    else
                    {
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + postResponse.survey_Provider_Name);
                        return answerResponse;
                    }
                }
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(postResponse));
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

        public HttpResponseMessage updateResponse(PostResponse postResponse, string ResponseId)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                if (ResponseId != null)
                {
                    if (!string.IsNullOrEmpty(postResponse.survey_Provider_Name))
                    {
                        string survey_Provider_ID = getSurveryProviderByName(postResponse.survey_Provider_Name);
                        if (!string.IsNullOrEmpty(survey_Provider_ID))
                        {
                            postResponse.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                        }
                        else
                        {
                            answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                            answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + postResponse.survey_Provider_Name);
                            return answerResponse;
                        }
                    }
                    string odataQuery = "aacn_responses(" + ResponseId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(postResponse));
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

        public APIResponse UpsertResponse(PostResponse postResponse, string responseId)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(postResponse.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(postResponse.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        postResponse.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                }
                if (!string.IsNullOrEmpty(responseId))
                {
                    ResponseModel responseModel = getResponsebyId(responseId);
                    if (responseModel.value != null && responseModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateResponse(postResponse, responseId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createResponse(postResponse);
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

        public HttpResponseMessage createResponseData(PostResponseData responeLine)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                string odataQuery = "aacn_responses";
                if (!string.IsNullOrEmpty(responeLine.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(responeLine.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        responeLine.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                    else
                    {
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + responeLine.survey_Provider_Name);
                        return answerResponse;
                    }
                }
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(responeLine));
                if (answerResponse.StatusCode == HttpStatusCode.NoContent)//204
                {
                    string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                    string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                    answerResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
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
    }
}
