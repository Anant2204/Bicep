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
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Serialization;

    public class SpeakerService : BaseService
    {
        public SpeakerModel getSpeakerbyId(string speakerId)
        {
            SpeakerModel speakerModel = new SpeakerModel();
            string query = string.Format(Utility.getSpeakerbyId, speakerId);
            HttpResponseMessage answerResponse = GetRecords(query);
            if (answerResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(answerResponse.Content.ReadAsStringAsync().Result))
                {
                    speakerModel = JsonConvert.DeserializeObject<SpeakerModel>(Utility.RemoveJsonNulls(answerResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return speakerModel;
        }

        public HttpResponseMessage createSpeaker(PostSpeaker postSpeaker)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                string odataQuery = "aacn_speakers";
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(postSpeaker));
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

        public HttpResponseMessage updateSpeaker(PostSpeaker postSpeaker, string speakerId)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                if (speakerId != null)
                {
                    string odataQuery = "aacn_speakers(" + speakerId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(postSpeaker));
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


        public APIResponse UpsertSpeaker(PostSpeaker postSpeaker, string speakerId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(speakerId))
                {
                    SpeakerModel speakerModel = getSpeakerbyId(speakerId);
                    if (speakerModel.value != null && speakerModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateSpeaker(postSpeaker, speakerId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createSpeaker(postSpeaker);
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
