using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; } = String.Empty;
        public string AuthorName { get; set; } = String.Empty;
    }
}