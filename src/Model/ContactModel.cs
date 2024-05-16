namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class ContactModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<contactData> value { get; set; }
    }

    public class contactData
    {
        [JsonProperty("customertypecodeODataCommunityDisplayV1FormattedValue")]
        public string customertypecode { get; set; }

        [JsonProperty("merged")]
        public bool merged { get; set; }

        [JsonProperty("emailaddress1")]
        public string emailaddress1 { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("customersizecodeODataCommunityDisplayV1FormattedValue")]
        public string customersizecode { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("donotemailODataCommunityDisplayV1FormattedValue")]
        public string donotemail { get; set; }

        [JsonProperty("fullname")]
        public string fullname { get; set; }

        [JsonProperty("address1_addressid")]
        public string address1_addressid { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }
        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("aacn_contact_number")]
        public string contact_Number { get; set; }

        [JsonProperty("_createdby_valueODataCommunityDisplayV1FormattedValue")]
        public string createdBy { get; set; }

        [JsonProperty("contactid")]
        public string contactid { get; set; }

        [JsonProperty("telephone3")]
        public object telephone3 { get; set; }

        [JsonProperty("nickname")]
        public object nickname { get; set; }

        [JsonProperty("address2_city")]
        public object address2_city { get; set; }

        [JsonProperty("address2_line1")]
        public object address2_line1 { get; set; }

        [JsonProperty("address1_telephone3")]
        public object address1_telephone3 { get; set; }

        [JsonProperty("address1_telephone2")]
        public object address1_telephone2 { get; set; }

        [JsonProperty("address1_composite")]
        public object address1_composite { get; set; }

        [JsonProperty("address1_postalcode")]
        public object address1_postalcode { get; set; }

        [JsonProperty("address2_upszone")]
        public object address2_upszone { get; set; }

        [JsonProperty("address2_line3")]
        public object address2_line3 { get; set; }

        [JsonProperty("address1_country")]
        public object address1_country { get; set; }

        [JsonProperty("address2_longitude")]
        public object address2_longitude { get; set; }

        [JsonProperty("suffix")]
        public object suffix { get; set; }

        [JsonProperty("address1_line2")]
        public object address1_line2 { get; set; }

        [JsonProperty("address1_county")]
        public object address1_county { get; set; }

        [JsonProperty("_accountid_value")]
        public object _accountid_value { get; set; }


        [JsonProperty("address2_name")]
        public object address2_name { get; set; }

        [JsonProperty("address1_line1")]
        public object address1_line1 { get; set; }


        [JsonProperty("address2_county")]
        public object address2_county { get; set; }

        [JsonProperty("address1_stateorprovince")]
        public object address1_stateorprovince { get; set; }

        [JsonProperty("birthdate")]
        public string birthdate { get; set; }

        [JsonProperty("mobilephone")]
        public object mobilephone { get; set; }

        [JsonProperty("company")]
        public object company { get; set; }

        [JsonProperty("gendercode")]
        public int gendercode { get; set; }

        [JsonProperty("telephone2")]
        public object telephone2 { get; set; }


        [JsonProperty("description")]
        public object description { get; set; }

        [JsonProperty("address1_name")]
        public object address1_name { get; set; }

        [JsonProperty("telephone1")]
        public object telephone1 { get; set; }

        [JsonProperty("middlename")]
        public object middlename { get; set; }

        [JsonProperty("salutation")]
        public object salutation { get; set; }

        [JsonProperty("address2_country")]
        public object address2_country { get; set; }
    }

    public class PostMember
    {
        [JsonProperty("contactid", NullValueHandling = NullValueHandling.Ignore)]
        public string memberId { get; set; } 
        
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("gendercode")]
        public int GenderCode { get; set; }

        [JsonProperty("birthdate")]
        public string BirthDate { get; set; }

        [JsonProperty("aacn_contact_number")]
        public string Contact_Number { get; set; }

        [JsonProperty("emailaddress1")]
        public string Emailaddress { get; set; }
    }
}
