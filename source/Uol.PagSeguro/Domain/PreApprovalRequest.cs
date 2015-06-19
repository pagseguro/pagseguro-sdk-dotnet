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
using System.Text;
using Uol.PagSeguro.Service;


namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents a pre-approval request
    /// </summary>
    public class PreApprovalRequest
    {
        private MetaData _metaData;
        private Parameter _parameter;

        /// <summary>
        /// Uri to where the PagSeguro payment page should redirect the user after the payment information is processed.
        /// </summary>
        /// <remarks>
        /// Typically this is a confirmation page on your web site.
        /// </remarks>
        public Uri RedirectUri
        {
            get;
            set;
        }

        /// <summary>
        /// Uri to where the user review the signature or read the rules.
        /// </summary>
        /// <remarks>
        /// Typically this is the signature page on your web site.
        /// </remarks>
        public Uri ReviewUri
        {
            get;
            set;
        }

        /// <summary>
        /// Reference code
        /// </summary>
        /// <remarks>
        /// Optional. You can use the reference code to store an identifier so you can 
        /// associate the PagSeguro transaction to a transaction in your system.
        /// </remarks>
        public string Reference
        {
            get;
            set;
        }

        /// <summary>
        /// Party that will be sending the money
        /// </summary>
        public Sender Sender
        {
            get;
            set;
        }

        /// <summary>
        /// PreApproval
        /// </summary>
        public PreApproval PreApproval
        {
            get;
            set;
        }

        /// <summary>
        /// Payment currency. 
        /// </summary>
        /// <remarks>
        /// The expected currency values are defined in the <c cref="T:Uol.PagSeguro.Domain.Currency">Currencty</c> class.
        /// </remarks>
        public string Currency
        {
            get;
            set;
        }        

        /// <summary>
        /// Determines for which url PagSeguro will send the order related notifications codes.
        /// <remarks>
        /// Optional. Any change happens in the transaction status, a new notification request will be send
        /// to this url. You can use that for update the related order.
        /// </remarks>    
        /// </summary>
        public string NotificationURL 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Meta Data reference
        /// </summary>
        public MetaData MetaData
        {
            get
            {
                if (this._metaData == null)
                {
                    this._metaData = new MetaData();
                }
                return this._metaData;
            }

            set { this._metaData = value; }
        }

        /// <summary>
        /// Parameter reference
        /// </summary>
        public Parameter Parameter
        {
            get
            {
                if (this._parameter == null)
                {
                    this._parameter = new Parameter();
                }
                return this._parameter;
            }
            set { this._parameter = value; }
        }

        /// <summary>
        /// Initializes a new instance of the PreApprovalRequest class
        /// </summary>
        public PreApprovalRequest()
        {
            this.Currency = Uol.PagSeguro.Constants.Currency.Brl;
        }

        /// <summary>
        /// Calls the PagSeguro web service and register this request for payment
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public Uri Register(Credentials credentials)
        {
            return PreApprovalService.CreatePreApproval(credentials, this);
        }

        /// <summary>
        /// Add a parameter for PagSeguro metadata pre-approval request 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddMetaData(string key, string value)
        {
            this.MetaData.Items.Add(new MetaDataItem(key, value));
        }

        /// <summary>
        /// Add a parameter for PagSeguro metadata pre-approval request 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        public void AddMetaData(string key, string value, int? group)
        {
            this.MetaData.Items.Add(new MetaDataItem(key, value, group));
        }

        /// <summary>
        /// Add a parameter for PagSeguro pre-approval request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddParameter(string key, string value)
        {
            this.Parameter.Items.Add(new ParameterItem(key, value));
        }

        /// <summary>
        /// Add a parameter for PagSeguro pre-approval request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        public void AddIndexedParameter(string key, string value, int? group)
        {
            this.Parameter.Items.Add(new ParameterItem(key, value, group));
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().Name).Append("(");
            builder.Append("Reference=").Append(this.Reference).Append(", ");
            string email = this.Sender == null ? null : this.Sender.Email;
            builder.Append("Sender.Email=").Append(email).Append(")");
            return builder.ToString();
        }
    }
}
