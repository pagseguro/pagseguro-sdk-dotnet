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


namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Defines a list of known accepted payment names.
    /// </summary>
    public static class ListPaymentMethodNames
    {
        /// <summary>
        /// Bradesco debit
        /// </summary>
        public static  string DebitoBradesco = "DEBITO_BRADESCO";

        /// <summary>
        /// Itaú debit
        /// </summary>
        public static  string DebitoItau = "DEBITO_ITAU";

        /// <summary>
        /// Unibanco debit
        /// </summary>
        public static string DebitoUnibanco = "DEBITO_UNIBANCO";

        /// <summary>
        /// Banco do Brasil debit
        /// </summary>
        public static string DebitoBancoDoBrasil = "DEBITO_BANCO_BRASIL";

        /// <summary>
        /// Banrisul debit
        /// </summary>
        public static string DebitoBanrisul = "DEBITO_BANRISUL";

        /// <summary>
        /// HSBC bank debit
        /// </summary>
        public static string DebitoHSBC = "DEBITO_HSBC";

        /// <summary>
        /// Boleto
        /// </summary>
        public static string Boleto = "BOLETO";
        
        /// <summary>
        /// Visa brand
        /// </summary>
        public static string Visa = "VISA";
        
        /// <summary>
        /// Mastercard brand
        /// </summary>
        public static string Mastercard = "MASTERCARD";
        
        /// <summary>
        /// Amex brand
        /// </summary>
        public static string Amex = "AMEX";
        
        /// <summary>
        /// Diners brand
        /// </summary>
        public const string Diners = "DINERS";
        
        /// <summary>
        /// Hipercard brand
        /// </summary>
        public static string Hipercard = "HIPERCARD";
        
        /// <summary>
        /// Aura brand
        /// </summary>
        public static string Aura = "AURA";

        /// <summary>
        /// Elo brand
        /// </summary>
        public const string Elo = "ELO";

        /// <summary>
        /// Aura brand
        /// </summary>
        public static string Plenocard = "PLENOCARD";

        /// <summary>
        /// Personalcard brand
        /// </summary>
        public static string Personalcard = "PERSONALCARD";

        /// <summary>
        /// JCB brand
        /// </summary>
        public static string JCB = "JCB";

        /// <summary>
        /// Discover brand
        /// </summary>
        public static string Discover = "DISCOVER";

        /// <summary>
        /// Brasilcard brand
        /// </summary>
        public static string Brasilcard = "BRASILCARD";

        /// <summary>
        /// FortBrasil brand
        /// </summary>
        public static string FortBrasil = "FORTBRASIL";

        /// <summary>
        /// Cardban brand
        /// </summary>
        public static string Cardban = "CARDBAN";

        /// <summary>
        /// Valecard brand
        /// </summary>
        public static string Valecard = "VALECARD";

        /// <summary>
        /// Cabal brand
        /// </summary>
        public static string Cabal = "CABAL";

        /// <summary>
        /// Mais brand
        /// </summary>
        public static string Mais = "MAIS";

        /// <summary>
        /// AVista brand
        /// </summary>
        public static string AVista = "AVISTA";

        /// <summary>
        /// Grandcard brand
        /// </summary>
        public static string Grandcard = "GRANDCARD";

        /// <summary>
        /// Sorocred brand
        /// </summary>
        public static string Sorocred = "SOROCRED";
    }
}
