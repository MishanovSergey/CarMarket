using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket
{
    public class Car
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string VIN { get; set; }
        public string TypeOfEngine { get; set; }
        public string EngineCapacity { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

    }
}
