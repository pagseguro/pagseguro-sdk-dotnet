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

using System.Collections.Generic;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    ///  Represents available documents for Sender use in checkout transactions
    /// </summary>
    public static class Documents
    {
        /// <summary>
        /// List of available documents for Sender use in PagSeguro transactions
        /// </summary>
        private static IDictionary<string, string> AvailableDocumentList = new Dictionary<string, string>()
        {
            {"CPF","Cadastro de Pessoa Física"},
            {"CNPJ","Cadastro de Pessoa Jurídica"}
        };

        /// <summary>
        /// Get available document list for Sender use in PagSeguro transactions
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetAvailableDocumentList()
        {
            return AvailableDocumentList;
        }

        /// <summary>
        /// Check if document type is available for PagSeguro
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public static bool IsDocumentTypeAvailable(string itemKey)
        {
            return AvailableDocumentList.ContainsKey(itemKey.ToUpper());
        }

        /// <summary>
        /// Gets document description by type
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public static string GetDocumentByType(string itemKey)
        {
            if (IsDocumentTypeAvailable(itemKey))
            {
                return AvailableDocumentList[itemKey.ToUpper()];
            }
            return null;
        }

        /// <summary>
        /// Gets document type by description
        /// </summary>
        /// <param name="itemDescription"></param>
        /// <returns></returns>
        public static string GetDocumentByDescription(string itemDescription)
        {
            itemDescription = itemDescription.ToLower();
            foreach (KeyValuePair<string, string> pair in AvailableDocumentList)
            {
                if (itemDescription.Equals(pair.Value.ToLower()))
                {
                    return pair.Key;
                }
            }
            return null;
        }
    }
}
