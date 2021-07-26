using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorPattern
{
    abstract class СookiesDecorator : Cookies
    {
        public float IngredientCost { get; set; }

        protected Cookies cookies;

        public СookiesDecorator(Cookies cookieForDecorating) : base(cookieForDecorating.ProductionCost, cookieForDecorating.MarkUp)
        {
            this.cookies = cookieForDecorating;
            this.Name = cookies.Name;
        }

        public abstract string GetDescription();

    }

    class ChocolateDecorator : СookiesDecorator
    {
        public ChocolateDecorator(Cookies cookieForDecorating, float ingredientCost) : base(cookieForDecorating)
        {
            this.IngredientCost = ingredientCost;
        }

        public override float CalculatePrice()
        {
            return cookies.CalculatePrice() + this.IngredientCost;
        }

        public override string GetDescription()
        {
            this.Name += " + кусочки шоколада";
            return this.Name + ", по цене " + this.CalculatePrice().ToString("0.00");
        }

    }

    class NutsDecorator : СookiesDecorator
    {
        public float Cost { get; set; }

        public NutsDecorator(Cookies cookieForDecorating, float ingredientCost) : base(cookieForDecorating)
        {
            this.Cost = ingredientCost;
        }

        public override float CalculatePrice()
        {
            return cookies.CalculatePrice() + this.Cost;
        }

        public override string GetDescription()
        {
            this.Name += " + орешки";
            return this.Name + ", по цене " + this.CalculatePrice().ToString("0.00");
        }

    }
}
