using System.Collections.Generic;

namespace Uol.PagSeguro.Domain.Installment
{
    /// <summary>
    /// </summary>
    public class Installments
    {
        private readonly List<Installment> _installments = new List<Installment>();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<Installment> Get()
        {
            return _installments;
        }

        /// <summary>
        /// </summary>
        /// <param name="installment"></param>
        public void Add(Installment installment)
        {
            _installments.Add(installment);
        }
    }
}
