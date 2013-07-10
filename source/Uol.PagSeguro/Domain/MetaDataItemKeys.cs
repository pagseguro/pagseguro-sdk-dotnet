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
    /// Represents available keys for MetaData item use in checkout transactions
    /// </summary>
    public class MetaDataItemKeys
    {
        /// <summary>
        /// List of available keys and values for MetaData item
        /// </summary>
        private static IDictionary<string, string> AvailableItemKeysList = new Dictionary<string, string>()
        {
            {"PASSENGER_CPF","CPF do passageiro"}
           ,{"PASSENGER_PASSPORT","Passaporte do passageiro"}
           ,{"ORIGIN_CITY","Cidade de origem"}
           ,{"DESTINATION_CITY","Cidade de destino"}
           ,{"ORIGIN_AIRPORT_CODE","Código do aeroporto de origem"}
           ,{"GAME_NAME","Nome do jogo"}
           ,{"PLAYER_ID","Id do jogador"}
           ,{"TIME_IN_GAME_DAYS","Tempo no jogo em dias"}
           ,{"MOBILE_NUMBER","Celular de recarga"}
           ,{"PASSENGER_NAME","Nome do passageiro"}
        };
        
        /// <summary>
        /// Get available  keys and values list for metadata item
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetAvailableItemKeysList()
        {
            return AvailableItemKeysList;
        }

        /// <summary>
        /// Check if key is available for PagSeguro
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public static bool IsItemKeyAvailable(string itemKey)
        {
            return AvailableItemKeysList.ContainsKey(itemKey.ToUpper());
        }

        /// <summary>
        /// Gets key description by key
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public static string GetItemDescriptionByKey(string itemKey)
        {
            if (IsItemKeyAvailable(itemKey))
            {
                return AvailableItemKeysList[itemKey.ToUpper()];
            }
            return null;
        }

        /// <summary>
        /// Gets key type by description
        /// </summary>
        /// <param name="itemDescription"></param>
        /// <returns></returns>
        public static string GetItemKeyByDescription(string itemDescription)
        {
            itemDescription = itemDescription.ToLower();
            foreach (KeyValuePair<string, string> pair in AvailableItemKeysList)
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
