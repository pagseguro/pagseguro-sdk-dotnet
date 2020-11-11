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
    /// Defines a list of known payment method codes.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new payment method codes
    /// without breaking this version of the library.
    /// </remarks>
    public static class PaymentMethodCode
    {
        /// <summary>
        /// VISA
        /// </summary>
        public static  int Visa = 101;

        /// <summary>
        /// Mastercard
        /// </summary>
        public static  int Mastercard = 102;

        /// <summary>
        /// American Express
        /// </summary>
        public static  int Amex = 103;

        /// <summary>
        /// Diners
        /// </summary>
        public static  int Diners = 104;

        /// <summary>
        /// Hipercard
        /// </summary>
        public static  int Hipercard = 105;

        /// <summary>
        /// Aura
        /// </summary>
        public static  int Aura = 106;

        /// <summary>
        /// Elo
        /// </summary>
        public static  int Elo = 107;

        /// <summary>
        /// PLENOCard
        /// </summary>
        public static  int PlenoCard = 108;

        /// <summary>
        /// PersonalCard
        /// </summary>
        public static  int PersonalCard = 109;

        /// <summary>
        /// JCB
        /// </summary>
        public static  int Jcb = 110;

        /// <summary>
        /// Discover
        /// </summary>
        public static  int Discover = 111;

        /// <summary>
        /// BrasilCard
        /// </summary>
        public static  int BrasilCard = 112;

        /// <summary>
        /// FORTBRASIL
        /// </summary>
        public static  int FortBrasil = 113;

        /// <summary>
        /// CARDBAN
        /// </summary>
        public static  int CardBan = 114;
        /// <summary>
        /// VALECARD
        /// </summary>
        public static  int ValeCard = 115;

        /// <summary>
        /// Cabal
        /// </summary>
        public static  int Cabal = 116;

        /// <summary>
        /// Mais!
        /// </summary>
        public static  int Mais = 117;

        /// <summary>
        /// Avista
        /// </summary>
        public static  int Avista = 118;

        /// <summary>
        /// GrandCard
        /// </summary>
        public static  int GrandCard = 119;

        /// <summary>
        /// Bradesco boleto
        /// </summary>
        public static  int BradescoBoleto = 201;

        /// <summary>
        /// Santander boleto
        /// </summary>
        public static  int SantanderBoleto = 202;

        /// <summary>
        /// Bradesco online transfer
        /// </summary>
        public static  int BradescoOnlineTransfer = 301;

        /// <summary>
        /// Itau online transfer
        /// </summary>
        public static  int ItauOnlineTransfer = 302;

        /// <summary>
        /// Unibanco online transfer
        /// </summary>
        public static  int UnibancoOnlineTransfer = 303;

        /// <summary>
        /// Banco do Brasil online transfer
        /// </summary>
        public static  int BancoBrasilOnlineTransfer = 304;

        /// <summary>
        /// Banco Real online transfer
        /// </summary>
        public static  int RealOnlineTransfer = 305;
        
        /// <summary>
        /// Banrisul online transfer
        /// </summary>
        public static  int BanrisulOnlineTransfer = 306;

        /// <summary>
        /// HSBC online transfer
        /// </summary>
        public static  int HsbcOnlineTransfer = 307;

        /// <summary>
        /// PagSeguro account balance
        /// </summary>
        public static  int PagSeguroBalance = 401;

        /// <summary>
        /// OiPaggo
        /// </summary>
        public static  int OiPaggo = 501;

        /// <summary>
        /// Banco do Brasil direct deposit
        /// </summary>
        public static  int BancoBrasilDirectDeposit = 701;
    }
}
