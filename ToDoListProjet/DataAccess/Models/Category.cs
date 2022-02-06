using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace ToDoListProjet.DataAccess.Models
{
    [Alias ("categories")]
   public class Category
    {
        [PrimaryKey]
        [AutoIncrement]

        public  int id { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
