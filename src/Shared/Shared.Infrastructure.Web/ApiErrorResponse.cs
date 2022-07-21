using System.Text;

namespace Shared.Infrastructure.Web
{
    //[DataContract]
    public partial class ApiErrorResponse
    {

        /// <summary>
        /// Gets or Sets the HttpStatusCode Code
        /// </summary>
        //[DataMember(Name = "error")]
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or Sets the Error Description
        /// </summary>
        //[DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; set; }

        public string ErrorKey { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>F
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApiErrorResponse {\n");
            sb.Append($"  {nameof(this.HttpStatusCode)}: ").Append(this.HttpStatusCode).Append("\n");
            sb.Append($"  {nameof(this.ErrorKey)}: ").Append(this.ErrorKey).Append("\n");
            sb.Append($"  {nameof(this.ErrorDescription)}: ").Append(this.ErrorDescription).Append("\n");
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
