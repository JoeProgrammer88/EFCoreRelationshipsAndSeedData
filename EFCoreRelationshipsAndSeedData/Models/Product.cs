using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreRelationshipsAndSeedData.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
