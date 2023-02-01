using System;
using System.Collections.Generic;
using WebApp.Models.ValueTypes;

namespace webApp.Models.Entities
{
    public partial class Course
    {
        public Course(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Il corso deve avere un titolo");
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("il corso deve avere un autore");
            }

            Title = title;
            Author = author;
            Lessons = new HashSet<Lesson>();
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Author { get; private set; }
        public string Email { get; private set; }
        public double Rating { get; private set; }
        public Money FullPrice { get; private set; }
        public Money CurrentPrice { get; private set; }


        public void changTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("Il corso deve avere un titolo");
            }
            Title = newTitle;
        }

        public void changePrices(Money newFullPrice, Money newCurrentprice)
        {
            if (newFullPrice == null || newCurrentprice == null)
            {
                throw new ArgumentException("il prezzo deve esistere");
            } 
            if (newFullPrice.Currency != newCurrentprice.Currency)
            {
                throw new ArgumentException("La valuta deve essere uguale");
            }
            if (newFullPrice.Amount <= newCurrentprice.Amount)
            {
                throw new ArgumentException("il prezzo in saldo deve essere più basso del prezzo");
            }

            FullPrice = newFullPrice;
            CurrentPrice = newCurrentprice;
        }

        public virtual ICollection<Lesson> Lessons { get; private set; }
    }
}
