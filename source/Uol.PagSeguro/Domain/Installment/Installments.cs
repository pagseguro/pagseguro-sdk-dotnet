using System.Collections.Generic;

namespace Uol.PagSeguro.Domain.Installment
{
    public class Installments
    {
        private List<Installment> installments = new List<Installment>();

        public List<Installment> Get()
        {
            return installments;
        }

        public void Add(Installment installment)
        {
            installments.Add(installment);
        }
    }
}