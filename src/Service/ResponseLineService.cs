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

    public class ResponseLineService : BaseService
    {
        public ResponseLineModel getResponseLineById(string ResponeseLineId)
        {
            ResponseLineModel responseLineModel = new ResponseLineModel();
            string query = string.Format(Utility.getResponseLinebyId, ResponeseLineId);
            HttpResponseMessage assessmentLineResponse = GetRecords(query);
            if (assessmentLineResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(assessmentLineResponse.Content.ReadAsStringAsync().Result))
                {
                    responseLineModel = JsonConvert.DeserializeObject<ResponseLineModel>(Utility.RemoveJsonNulls(assessmentLineResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return responseLineModel;
        }

        public HttpResponseMessage CreateResponseLine(PostResponseLine responeLine)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                string odataQuery = "aacn_response_lines";
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

        public HttpResponseMessage updateResponseLine(PostResponseLine responeLine, string ResponeseLineId)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                if (ResponeseLineId != null)
                {
                    string odataQuery = "aacn_response_lines(" + ResponeseLineId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(responeLine));
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

        public APIResponse UpsertResponseLine(PostResponseLine responeLine, string responeseLineId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(responeseLineId))
                {
                    ResponseLineModel responseLineModel = getResponseLineById(responeseLineId);
                    if (responseLineModel.value != null && responseLineModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateResponseLine(responeLine, responeseLineId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = CreateResponseLine(responeLine);
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
