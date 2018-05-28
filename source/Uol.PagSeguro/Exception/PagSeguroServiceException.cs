// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// Encapsulates a problem that occurred calling a PagSeguro web service
    /// </summary>
    [Serializable]
    public sealed class PagSeguroServiceException : System.Exception
    {
        private const string CrLf = "\n";
        private const string HttpStatusCodeField = "HttpStatusCode";
        private const string ErrorsField = "Errors";
        private List<ServiceError> _errors;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PagSeguroServiceException class
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public PagSeguroServiceException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PagSeguroServiceException class
        /// </summary>
        /// <param name="statusCode"></param>
        public PagSeguroServiceException(HttpStatusCode statusCode) :
            base(string.Format(CultureInfo.InvariantCulture, "HttpStatusCode: {0}", statusCode))
        {
            StatusCode = statusCode;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PagSeguroServiceException class
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errors"></param>
        public PagSeguroServiceException(HttpStatusCode statusCode, IEnumerable<ServiceError> errors) :
            base(string.Format(CultureInfo.InvariantCulture, "HttpStatusCode: {0}", statusCode))
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            _errors = new List<ServiceError>(errors);
            StatusCode = statusCode;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PagSeguroServiceException class
        /// </summary>
        /// <param name="message"></param>
        public PagSeguroServiceException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PagSeguroServiceException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        // ReSharper disable once UnusedMember.Global
        public PagSeguroServiceException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PagSeguroServiceException class
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="innerException"></param>
        public PagSeguroServiceException(HttpStatusCode statusCode, System.Exception innerException) :
            base(string.Format(CultureInfo.InvariantCulture, "HttpStatusCode: {0} ({1})", statusCode, (int)statusCode), innerException)
        {
            StatusCode = statusCode;
        }

        private PagSeguroServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _errors = (List<ServiceError>)info.GetValue(ErrorsField, typeof(List<ServiceError>));
            StatusCode = (HttpStatusCode)info.GetValue(HttpStatusCodeField, typeof(HttpStatusCode));
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(HttpStatusCodeField, StatusCode);
            info.AddValue(ErrorsField, Errors);
        }

        /// <summary>
        /// List of errors returned by the PagSeguro web service
        /// </summary>
        public IList<ServiceError> Errors
        {
            get
            {
                if (_errors == null)
                    _errors = new List<ServiceError>();

                return _errors.AsReadOnly();
            }
        }

        /// <summary>
        /// Http status code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <inheritdoc />
        /// <summary>
        /// Returns a textual representation of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder(base.ToString());
            builder.Append(CrLf);

            foreach (var error in Errors)
                builder.Append(error).Append(CrLf);

            return builder.ToString();
        }
    }
}
