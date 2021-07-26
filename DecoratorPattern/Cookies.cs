using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorPattern
{
   abstract class Cookies
    {
        public string Name { get; set; }
        public float ProductionCost { get; set; }
        public float MarkUp { get; set; } 

        public Cookies(float prodCost, float markUp)
        {
            this.ProductionCost = prodCost;
            this.MarkUp = markUp;
        }

        public abstract float CalculatePrice();
    }

    class FrenchCookies : Cookies
    { 
        public FrenchCookies(float prodCost, float markUp) : base(prodCost, markUp) 
        { this.Name = "Французское печенье"; }
        public override float CalculatePrice()
        {
            return this.ProductionCost*this.MarkUp;
        }
    }

    class AmericanCookies : Cookies
    {
        public float AdditionalCosts { get; set; }
        public AmericanCookies(float prodCost, float markUp, float additionalCosts) : base(prodCost, markUp)
        {
            this.Name = "Американское печенье";
            this.AdditionalCosts = additionalCosts; 
        }

        public override float CalculatePrice()
        {
            return (this.ProductionCost + this.AdditionalCosts)*this.MarkUp;
        }

    }

}
