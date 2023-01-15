using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CarMarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SetEncoding();
            Client client = MarketLogger.EnterOrRegistateClient();
            while (true)
            {
                List<Brand> brands = Market.BuildBrandsList();
                Market.PrintMainMenu(brands);
                Brand brand = MarketLogger.SelectBrand();
                List<Model> models = Market.BuildModelsList(brand);
                Market.PrintModelMenu(brand, models);
                Model? model = MarketLogger.SelectModel(models);
                if (model != null)
                {
                    List<Car> cars = Market.BuildCarsList(model);
                    Market.PrintCarMenu(model, cars);
                    Car? car = MarketLogger.SelectCar(cars);
                    if(car != null)
                    {
                        client.Pay(car, client);
                        break;
                    }
                }
            }
        }
        private static void SetEncoding()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
        }
    }
}