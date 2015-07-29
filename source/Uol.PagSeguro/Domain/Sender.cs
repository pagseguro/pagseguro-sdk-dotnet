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
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents the party on the transaction that is sending the money
    /// </summary>
    public class Sender
    {

        private IList<SenderDocument> _documents;

        /// <summary>
        /// Sender name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Sender email
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Sender phone number
        /// </summary>
        public Phone Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Sender hash code
        /// </summary>
        public String Hash
        {
            get;
            set;
        }

        /// <summary>
        /// Sender documents
        /// </summary>
        public IList<SenderDocument> Documents
        {
            get 
            { 
                if(this._documents == null)
                {
                    this._documents = new List<SenderDocument>();
                }
                return this._documents;  
            }
            set { this._documents = value; }
        }

        internal Sender()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Sender class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="areaCode"></param>
        /// <param name="number"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public Sender(string name, string email, string areaCode, string number, string type, string value)
        {
            this.Name = name;
            this.Email = email;
            this.Phone = new Phone(areaCode, number);
            this.Documents.Add(new SenderDocument(type,value));
        }
        
        /// <summary>
        /// Initializes a new instance of the Sender class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        public Sender(string name, string email, Phone phone)
        {
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
        }

        /// <summary>
        /// Gets toString class
        /// </summary>
        /// <returns>string</returns>
        public override string  ToString()
        {
            return "Sender [name=" + Name + ", email=" + Email + ", phone=" + Phone
                    + ", documents=" + Documents + "]";
        }
    }
}
