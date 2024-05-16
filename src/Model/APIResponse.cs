namespace AACN.API.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class APIResponse
    {
        public Guid RecordId { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
    }
}
