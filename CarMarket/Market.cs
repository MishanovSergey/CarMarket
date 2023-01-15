using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket
{
    public static class Market
    {
        private const string loginRegistrateMessage = "Придумайте логин (Логин должен содержать не более 20). Или введите \"Назад\", чтобы вернуться на предыдущую страницу: ";
        private const string passwordRegistrateMessage = "Придумайте пароль (Пароль должен содержать не более 20). Или введите \"Назад\", чтобы вернуться на предыдущую страницу: ";
        private const string nameRegistrateMessage = "Напишите ваше имя (Имя должно состоять не более чем из 20-ти символов). Или введите \"Назад\", чтобы вернуться на предыдущую страницу: ";
        private const string nameError = "Имя должно состоять не более чем из 20-ти символов. Попробуйте снова: ";
        private const string loginError = "Логин должен содержать не более 20-ти символов. Попробуйте снова: ";
        private const string passwordError = "Пароль должен содержать не более 20-ти символов. Попробуйте снова: ";
        public static Client? Enter()
        {
            using (ApplicationContext garage = new())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Введите ваш логин. Или введите \"Назад\", чтобы вернуться на предыдущую страницу: ");
                    string login = Console.ReadLine().ToLower();
                    if (login == "назад")
                        return null;
                    Console.WriteLine("Введите ваш пароль. Или введите \"Назад\", чтобы вернуться на предыдущую страницу: ");
                    string password = Console.ReadLine().ToLower();
                    if (password == "назад")
                        return null;
                    Client? client = garage.Clients.FirstOrDefault(cl => cl.Login == login && cl.Password == password);
                    if (client == null)
                    {
                        Console.WriteLine("Неверно указан логин или пароль! Нажмите \"Enter\", чтобы попробовать снова.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Вы успешно вошли в учетную запись! Нажмите \"Enter\", чтобы продолжить.");
                        Console.ReadLine();
                        return client;
                    }
                }
            }
        }
        public static Client? Registate()
        {
            while (true)
            {
                bool isLogin = false;
                string name = CreateClientData(nameRegistrateMessage, nameError, isLogin);
                if (name.ToLower() == "назад")
                    return null;
                string login = CreateClientData(loginRegistrateMessage, loginError, isLogin = true);
                if (login.ToLower() == "назад")
                    return null;
                string password = CreateClientData(passwordRegistrateMessage, passwordError, isLogin = false);
                if (password.ToLower() == "назад")
                    return null;
                using (ApplicationContext garage = new())
                {
                    Client client = new Client { Name = name, Login = login, Password = password };
                    garage.Clients.Add(client);
                    garage.SaveChanges();
                    Console.WriteLine($"Поздравляем, {client.Name}! Вы успешно зарегестрировали учетную запись.\nНажмите \"Enter\", чтобы продолжить.");
                    Console.ReadLine();
                    return client;
                }
            }
        }
        public static string CreateClientData(string message, string error, bool isLogin)
        {
            Console.Clear();
            Console.WriteLine(message);
            while (true)
            {
                string data = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(data))
                    Console.Write("Вы ввели пустую строку. Попробуйте снова: ");
                else
                {
                    if (data.Length > 20)
                        Console.Write(error);
                    else
                    {
                        if (isLogin == true)
                        {
                            using (ApplicationContext garage = new())
                            {
                                if (garage.Clients.FirstOrDefault(c => c.Login.ToLower() == data) != null)
                                    Console.Write("Пользователь с таким логином уже существует. Попробуйте снова: ");
                                else
                                    return data;
                            }
                        }
                        else
                            return data;
                    }
                }
            }
        }
        public static List<Brand> BuildBrandsList()
        {
            using (ApplicationContext garage = new())
            {
                return (from brand in garage.Brands
                        select brand).ToList();
            }
        }
        public static void PrintMainMenu(List<Brand> brands)
        {
                Console.Clear();
                Console.WriteLine("$$$$$$$$$$$$$$$ |!|!| Магазин авто Twisted Metall: Fury Road |!|!| $$$$$$$$$$$$$$$\n\n\nНиже представленны доступные бренды автомобилей:");
                foreach (Brand b in brands)
                {
                    Console.WriteLine($"\n||{b.Name}||");
                }
        }
        public static List<Model> BuildModelsList(Brand brand)
        {
            using (ApplicationContext garage = new())
            {
                return (from model in garage.Models
                        where model.BrandId == brand.Id
                        select model).ToList();
            }
        }
        
        public static void PrintModelMenu(Brand brand, List<Model> models)
        {
            Console.Clear();
            Console.WriteLine($"Доступные модели бренда {brand.Name}:");
            foreach (var model in models)
            {
                using (ApplicationContext garage = new())
                {
                    int numberOfCars = (from car in garage.Cars
                                 where car.ModelId == model.Id && car.IsAvailable == true
                                 select car).Count();
                    Console.WriteLine($"\n||{model.Name}|| Машин в наличии: {numberOfCars}");
                }
            }
        }
        public static List<Car> BuildCarsList(Model model)
        {
            using (ApplicationContext garage = new())
            {
                return (from car in garage.Cars
                        where car.ModelId == model.Id
                        select car).ToList();
            }
        }
        public static void PrintCarMenu(Model model, List<Car> cars)
        {
            Console.Clear();
                foreach (Car c in cars)
                {
                    Console.WriteLine($"\n||{model.Name}|| ID: {c.Id}; VIN: {c.VIN}; Двигатель: {c.TypeOfEngine} {c.EngineCapacity}; Трансмиссия: {c.Transmission}; Цвет: {c.Color}; Цена: {c.Price}");
                }
        }
    }
}
