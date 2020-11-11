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
        private  readonly  int Visa = 101;

        /// <summary>
        /// Mastercard
        /// </summary>
        private  readonly  int Mastercard = 102;

        /// <summary>
        /// American Express
        /// </summary>
        private  readonly  int Amex = 103;

        /// <summary>
        /// Diners
        /// </summary>
        private  readonly  int Diners = 104;

        /// <summary>
        /// Hipercard
        /// </summary>
        private  readonly  int Hipercard = 105;

        /// <summary>
        /// Aura
        /// </summary>
        private  readonly  int Aura = 106;

        /// <summary>
        /// Elo
        /// </summary>
        private  readonly  int Elo = 107;

        /// <summary>
        /// PLENOCard
        /// </summary>
        private  readonly  int PlenoCard = 108;

        /// <summary>
        /// PersonalCard
        /// </summary>
        public readonly  int PersonalCard = 109;

        /// <summary>
        /// JCB
        /// </summary>
        private  readonly  int Jcb = 110;

        /// <summary>
        /// Discover
        /// </summary>
        private  readonly  int Discover = 111;

        /// <summary>
        /// BrasilCard
        /// </summary>
        public readonly  int BrasilCard = 112;

        /// <summary>
        /// FORTBRASIL
        /// </summary>
        public readonly  int FortBrasil = 113;

        /// <summary>
        /// CARDBAN
        /// </summary>
        private  readonly  int CardBan = 114;
        /// <summary>
        /// VALECARD
        /// </summary>
        private  readonly  int ValeCard = 115;

        /// <summary>
        /// Cabal
        /// </summary>
        private  readonly  int Cabal = 116;

        /// <summary>
        /// Mais!
        /// </summary>
        public readonly  int Mais = 117;

        /// <summary>
        /// Avista
        /// </summary>
        public readonly  int Avista = 118;

        /// <summary>
        /// GrandCard
        /// </summary>
        public readonly  int GrandCard = 119;

        /// <summary>
        /// Bradesco boleto
        /// </summary>
        public readonly  int BradescoBoleto = 201;

        /// <summary>
        /// Santander boleto
        /// </summary>
        private  readonly  int SantanderBoleto = 202;

        /// <summary>
        /// Bradesco online transfer
        /// </summary>
        private  readonly  int BradescoOnlineTransfer = 301;

        /// <summary>
        /// Itau online transfer
        /// </summary>
        private  readonly  int ItauOnlineTransfer = 302;

        /// <summary>
        /// Unibanco online transfer
        /// </summary>
        private  readonly  int UnibancoOnlineTransfer = 303;

        /// <summary>
        /// Banco do Brasil online transfer
        /// </summary>
        private  readonly  int BancoBrasilOnlineTransfer = 304;

        /// <summary>
        /// Banco Real online transfer
        /// </summary>
        private  readonly  int RealOnlineTransfer = 305;
        
        /// <summary>
        /// Banrisul online transfer
        /// </summary>
        private  readonly  int BanrisulOnlineTransfer = 306;

        /// <summary>
        /// HSBC online transfer
        /// </summary>
        private  readonly  int HsbcOnlineTransfer = 307;

        /// <summary>
        /// PagSeguro account balance
        /// </summary>
        private  readonly  int PagSeguroBalance = 401;

        /// <summary>
        /// OiPaggo
        /// </summary>
        private  readonly  int OiPaggo = 501;

        /// <summary>
        /// Banco do Brasil direct deposit
        /// </summary>
        private  readonly  int BancoBrasilDirectDeposit = 701;
    }
}
