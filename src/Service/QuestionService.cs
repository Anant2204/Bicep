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

    public class QuestionService : BaseService
    {
        public QuestionModel getQuestionById(string questionId)
        {
            QuestionModel questionModel = new QuestionModel();
            string query = string.Format(Utility.getQuestionbyId, questionId);
            HttpResponseMessage answerResponse = GetRecords(query);
            if (answerResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(answerResponse.Content.ReadAsStringAsync().Result))
                {
                    questionModel = JsonConvert.DeserializeObject<QuestionModel>(Utility.RemoveJsonNulls(answerResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return questionModel;
        }

        public HttpResponseMessage createQuestion(PostQuestion postQuestion)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                string odataQuery = "aacn_questions";
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(postQuestion));
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

        public HttpResponseMessage updateQuestion(PostQuestion postQuestion, string questionId)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                if (questionId != null)
                {
                    string odataQuery = "aacn_questions(" + questionId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(postQuestion));
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

        public APIResponse UpsertQuestion(PostQuestion postQuestion, string questionId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(questionId))
                {
                    QuestionModel questionModel = getQuestionById(questionId);
                    if (questionModel.value != null && questionModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateQuestion(postQuestion, questionId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createQuestion(postQuestion);
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
