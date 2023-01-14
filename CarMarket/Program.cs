using Microsoft.EntityFrameworkCore;
namespace CarMarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client client = MarketLogger.ClientEnterOrRegistate();
            while (true)
            {
                Market.PrintMainMenu();
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
    }
}