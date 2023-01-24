using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Models.ValueTypes
{
    public class Money
    {
        public Money() : this(Currency.EUR, 0.00m)
        {
        }

        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
            Currency = currency;
        }

        private decimal amount = 0;

        public decimal Amount //proprietà per il prezzo 
        {
            get
            {
                return amount;
            }
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("L'importo non può essere inferiore a 0");
                }
                amount = value;
            }
        }

        public Currency Currency //proprietà per la valuta 
        {
            get; set;
        }

        public override bool Equals(object obj)
        {
            var money = obj as Money;
            return money != null &&
            Amount == money.Amount &&
            Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public override string ToString()
        {
            return $"{Currency} {Amount:#.00}";
        }
    }


}
