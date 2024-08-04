using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/*There are many ways to form a one to many relationship, but in our case we're going to do so by convention, it basically means that Entity Framework, dotnet core is going to go ahead, search through our code and form this relationship for us, you can do it manually through something called fluent api*/

namespace dotnet_api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        /*StockId is the actual key, this is what's going to form the relationship within the database and Entity Framework is going to set this up for us*/
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }

        /*The way that we actually form the relationship is this
        we have stock and it's going to connect our stock through
        our comment
        Stock navigation property: it's what's going to allow us to navigation within our models, it'll allow us navigate within this relationship, so we can go like this Stock.CompanyName*/
        public Stock? Stock { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}