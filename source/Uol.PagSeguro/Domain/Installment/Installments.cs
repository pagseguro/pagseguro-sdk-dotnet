using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Installment
{
    public class Installments
    {
        private List<Installment> installments = new List<Installment>();

        public List<Installment> Get()
        {
            return this.installments;
        }

        public void Add(Installment installment)
        {
            this.installments.Add(installment);
        }
    }
}
