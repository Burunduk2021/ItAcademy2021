using System;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Cookies simpleFrenchCookies = new FrenchCookies(50f, 1.4f);
            Console.WriteLine("Название: {0}, стоимость: {1}", simpleFrenchCookies.Name, simpleFrenchCookies.CalculatePrice());

            Cookies FrenchCookiesWithChocolate = new ChocolateDecorator(simpleFrenchCookies, 10f);
            Console.WriteLine("Результат работы одного декоратора: {0}", ((СookiesDecorator)FrenchCookiesWithChocolate).GetDescription());

            Console.WriteLine("\n************************************************************************\n");

            Cookies simpleAmericanCookies = new AmericanCookies(50f, 1.4f, 20f);
            Console.WriteLine("Название: {0}, стоимость: {1}", simpleAmericanCookies.Name, simpleAmericanCookies.CalculatePrice());

            СookiesDecorator AmericanCookiesWithChocolateAndNuts = new ChocolateDecorator(simpleAmericanCookies, 10f);
            Console.WriteLine("Результат работы первого декоратора: {0}", AmericanCookiesWithChocolateAndNuts.GetDescription());
            AmericanCookiesWithChocolateAndNuts = new NutsDecorator(AmericanCookiesWithChocolateAndNuts, 20f);
            Console.WriteLine("Результат работы второго декоратора: {0}", AmericanCookiesWithChocolateAndNuts.GetDescription());

            
        }
    }
}
