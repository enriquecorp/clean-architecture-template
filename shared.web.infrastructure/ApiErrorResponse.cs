using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.web.infrastructure
{
    //[DataContract]
    public partial class ApiErrorResponse
    {
        /// <summary>
        /// Gets or Sets the HttpStatusCodeError Code
        /// </summary>
        //[DataMember(Name = "error")]
        public int HttpStatusCodeError { get; set; }

        /// <summary>
        /// Gets or Sets the Error Description
        /// </summary>
        //[DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; set; }

        public string ErrorCode { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApiErrorResponse {\n");
            sb.Append("  HttpStatusCodeError: ").Append(this.HttpStatusCodeError).Append("\n");
            sb.Append("  ErrorCode: ").Append(this.ErrorCode).Append("\n");
            sb.Append("  ErrorDescription: ").Append(this.ErrorDescription).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        ///// <summary>
        ///// Returns the JSON string presentation of the object
        ///// </summary>
        ///// <returns>JSON string presentation of the object</returns>
        //public string ToJson()
        //{
        //    return JsonConvert.SerializeObject(this, Formatting.Indented);
        //}
    }
}
