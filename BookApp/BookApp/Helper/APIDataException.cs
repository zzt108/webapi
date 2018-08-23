using BookApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace BookApp.Helper {
    /// <summary>
    /// Api Data Exception
    /// </summary>
    [Serializable]
    [DataContract]
    public class APIDataException : Exception, IAPIException {
        #region Public Serializable properties.
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ErrorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ErrorDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        string reasonPhrase = "ApiDataException";

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ReasonPhrase {
            get { return this.reasonPhrase; }

            set { this.reasonPhrase = value; }
        }

        #endregion

        #region Public Constructor.
        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        public APIDataException(int errorCode, string errorDescription, HttpStatusCode httpStatus) {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            HttpStatus = httpStatus;
        }
        #endregion
    }
}