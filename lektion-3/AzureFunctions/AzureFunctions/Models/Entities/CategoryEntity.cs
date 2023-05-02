using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models.Entities
{
    internal class CategoryEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
