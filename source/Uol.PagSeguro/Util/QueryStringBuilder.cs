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
using System.Globalization;
using System.Text;
using System.Web;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.Util
{
    /// <summary>
    /// 
    /// </summary>
    internal class QueryStringBuilder
    {
        private const char Separator = '&';
        private const char Equal = '=';
        private StringBuilder builder;

        public QueryStringBuilder()
        {
            builder = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString"></param>
        public QueryStringBuilder(string queryString)
        {
            builder = new StringBuilder(queryString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        private void AppendCore(string parameterName, string value)
        {
            if (parameterName == null)
                throw new ArgumentNullException("parameterName");

            if (value == null)
                throw new ArgumentNullException("value");

            if (builder.Length > 0)
            {
                builder.Append(QueryStringBuilder.Separator);
            }
            builder.Append(HttpUtility.UrlEncode(parameterName));
            builder.Append(QueryStringBuilder.Equal);
            builder.Append(HttpUtility.UrlEncode(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QueryStringBuilder Append(string parameterName, string value)
        {
            AppendCore(parameterName, value);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QueryStringBuilder Append(string parameterName, int value)
        {
            AppendCore(parameterName, value.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QueryStringBuilder Append(string parameterName, long value)
        {
            AppendCore(parameterName, value.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QueryStringBuilder Append(string parameterName, decimal value)
        {
            AppendCore(parameterName, value.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QueryStringBuilder Append(string parameterName, DateTime value)
        {
            AppendCore(parameterName, PagSeguroUtil.FormatDateXml(value));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public QueryStringBuilder AppendToQuery(string value)
        {
            this.builder.Append(value);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public QueryStringBuilder EncodeCredentialsAsQueryString(Credentials credentials)
        {
            foreach (CredentialsNameValuePair nv in credentials.Attributes)
            {
                if (nv.Value.Length > 0)
                {
                    this.Append(nv.Name, nv.Value);
                }
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public QueryStringBuilder ReplaceValue(string oldValue, string newValue)
        {
            this.builder.Replace(oldValue,newValue);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
