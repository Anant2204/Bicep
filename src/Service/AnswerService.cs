namespace AACN.API.Service
{
    using AACN.API.Model;
    using AACN.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class AnswerService : BaseService
    {
        public AnswerModel getAnswerbyId(string answerId)
        {
            AnswerModel answerModel = new AnswerModel();
            string query = string.Format(Utility.GetAnswerById, answerId);
            HttpResponseMessage answerResponse = GetRecords(query);
            if (answerResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(answerResponse.Content.ReadAsStringAsync().Result))
                {
                    answerModel = JsonConvert.DeserializeObject<AnswerModel>(Utility.RemoveJsonNulls(answerResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return answerModel;
        }
        public HttpResponseMessage createAnswer(PostAnswer answerObject)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                string odataQuery = "aacn_answers";
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(answerObject));
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
        public HttpResponseMessage updateAnswer(PostAnswer answer, string answerId)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                if (answerId != null)
                { 
                    string odataQuery = "aacn_answers(" + answerId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(answer));
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
                }

                return answerResponse;
            }
            catch (Exception ex)
            {
                answerResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }
        public APIResponse UpsertAnswer(PostAnswer answer, string answerId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(answerId))
                {
                    AnswerModel answerModel = getAnswerbyId(answerId);
                    if (answerModel.value != null && answerModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateAnswer(answer, answerId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createAnswer(answer);
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
