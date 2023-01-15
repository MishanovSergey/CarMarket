using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket
{
    public static class MarketLogger
    {
        public static Client EnterOrRegistateClient()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добрый день! Рады приветствовать вас в нашем автодилере!\nВпишите: \"Вход\", если у вас уже есть учетная запись. Если же хотите зарегистрировать учетную запись, то впишите: \"Регистрация\"");
                string enterResult = Console.ReadLine().ToLower();

                if (enterResult != "вход" && enterResult != "регистрация")
                {
                    Console.WriteLine("Выберите вход или регистрацию! Нажмите \"Enter\", чтобы попробовать снова.");
                    Console.ReadLine();
                }
                if (enterResult == "вход")
                {
                    Client? client = Market.Enter();
                    if (client != null)
                        return client;
                }
                if (enterResult == "регистрация")
                {
                    Client? client = Market.Registate();
                    if (client != null)
                        return client;
                }
            }
        }

        public static Brand SelectBrand()
        {
            using (ApplicationContext garage = new())
            {
                Console.Write("\nВыберите интересующий вас бренд: ");
                while (true)
                {
                    string brandChoice = Console.ReadLine().ToLower();
                    Brand? brand = garage.Brands.FirstOrDefault(b => b.Name.ToLower() == brandChoice);
                    if (brand == null)
                    {
                        Console.Write("Выберите бренд из списка доступных! Попробуйте снова: ");
                    }
                    else
                    {
                        return brand;
                    }
                }
            }
        }
        public static Model? SelectModel(List<Model> models)
        {
            using (ApplicationContext garage = new())
            {
                Console.Write("\nВыберите интересующую вас модель. Или впишите \"Назад\", чтобы вернуться на главное меню: ");
                while (true)
                {
                    string modelChoice = Console.ReadLine().ToLower();
                    if (modelChoice == "назад")
                        return null;
                    Model? model = models.FirstOrDefault(m => m.Name.ToLower() == modelChoice);

                    if (model == null)
                    {
                        Console.Write("Выберите модель из списка доступных! Попробуйте снова: ");
                    }

                    else
                    {
                        int numberOfCars = (from car in garage.Cars
                                            where car.ModelId == model.Id && car.IsAvailable == true
                                            select car).Count();

                        if (numberOfCars == 0)
                        {
                            Console.Write("Выберите модель из списка доступных! Попробуйте снова: ");
                        }

                        else
                        {
                            return model;
                        }
                    }
                }
            }
        }
        public static Car? SelectCar(List<Car> cars)
        {
                int idResult;
                Console.Write("\nВпишите номер ID выбранного вами автомобиля, чтобы совершить покупку. Или впишите \"Назад\", чтобы вернуться на главное меню: ");
                while (true)
                {
                    string carChoice = Console.ReadLine();
                    if (carChoice.ToLower() == "назад")
                        return null;
                    if (int.TryParse(carChoice, out idResult))
                    {
                        Car? car = cars.FirstOrDefault(c => c.Id == idResult);
                        if (car == null)
                        {
                            Console.Write("Введён некорректный номер ID! Попробуйте снова: ");
                        }
                        else
                        {
                            return car;
                        }
                    }
                    else
                        Console.Write("Введён некорректный номер ID! Попробуйте снова: ");
                }
        }
    }
}
