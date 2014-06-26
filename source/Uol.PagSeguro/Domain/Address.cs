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
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents an address location, typically for shipping or charging purposes.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Initializes a new instance of the Address class
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Address class
        /// </summary>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <param name="district"></param>
        /// <param name="postalCode"></param>
        /// <param name="street"></param>
        /// <param name="number"></param>
        /// <param name="complement"></param>
        public Address(string country, string state, string city, string district, string postalCode, string street, string number, string complement)
        {
            this.Country = country;
            this.State = state;
            this.City = city;
            this.District = district;
            this.PostalCode = PagSeguroUtil.GetOnlyNumbers(postalCode);
            this.Street = street;
            this.Number = PagSeguroUtil.GetOnlyNumbers(number);
            this.Complement = complement;
        }

        /// <summary>
        /// Country
        /// </summary>
        public string Country
        {
            get;
            set;
        }

        /// <summary>
        /// State or province
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// City
        /// </summary>
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// District, county or neighborhood, if applicable
        /// </summary>
        public string District
        {
            get;
            set;
        }

        /// <summary>
        /// Postal/Zip code
        /// </summary>
        public string PostalCode
        {
            get;
            set;
        }

        /// <summary>
        /// Street name
        /// </summary>
        public string Street
        {
            get;
            set;
        }

        /// <summary>
        /// Number
        /// </summary>
        public string Number
        {
            get;
            set;
        }

        /// <summary>
        /// Apartment, suite number or any other qualifier after the street/number pair.
        /// </summary>
        /// <example>
        /// Apt 274, building A. 
        /// </example>
        public string Complement
        {
            get;
            set;
        }
    }
}
