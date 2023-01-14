using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal MoneyAccount { get; set; }

        public void AccoutTop_up(Car car, Client client)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Стоимость автомобиля: \t{car.Price}р." +
                          $"\nБаланс вашего счета: \t{client.MoneyAccount}р." +
                          $"\nНе хватает для покупки:\t{car.Price - client.MoneyAccount}р.\n");

                decimal top_up;
                Console.Write("Введите сумму, на которую хотите пополнить свой счет: ");
                using (ApplicationContext garage = new())
                {
                    while (true)
                    {
                        string result = Console.ReadLine();
                        if (decimal.TryParse(result, out top_up))
                        {
                            if (top_up < 0)
                                Console.Write("!!Ошибка пополнения счета!! Введите корректную сумму пополнения: ");

                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"Стоимость автомобиля: \t{car.Price}р." +
                                    $"\nБаланс вашего счета: \t{client.MoneyAccount + top_up}р." +
                                    $"\nНе хватает для покупки:\t{car.Price - (client.MoneyAccount + top_up)}р.\n");
                                client.MoneyAccount += top_up;
                                garage.Clients.Update(client);
                                garage.SaveChanges();
                                return;
                            }
                        }
                        else
                            Console.Write("!!Ошибка пополнения счета!! Введите корректную сумму пополнения: ");
                    }
                }
            }
        }

        public void Pay(Car car, Client client)
        {
            using (ApplicationContext garage = new())
            {
                while (true)
                {
                    if (client.MoneyAccount < car.Price)
                    {
                        Console.WriteLine("На вашем счёте недостаточно средств для оплаты. Нажмите \"Enter\", чтобы пополнить его!");
                        Console.ReadLine();
                        AccoutTop_up(car, client);
                    }
                    else
                    {
                        client.MoneyAccount -= car.Price;
                        garage.Clients.Update(client);
                        car.IsAvailable = false;
                        garage.Cars.Update(car);
                        garage.SaveChanges();
                        Console.Clear();
                        Console.WriteLine($"{DateTime.Now}\t|| Оплата прошла успешно! состояние вашего счета: {client.MoneyAccount}р. ||\n\n" +
                            $"От всего сердца поздравляем вас с приобретением этого замечательного автомобиля! Все ваши документы аккуратно сложены в бардачке, " +
                            $"а сам автомобиль ожидает вас на парковке перед дилерским центром. Держите ваши ключи и помните, что мы всегда будем рады видеть вас снова!" +
                            $"\nДоброго пути!");
                        return;
                    }
                }
            }
        }
    }
}
