using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delete.ClassLibrary1.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Gb { get; set; }
        public string Baground { get; set; }
        public int Sale { get; set; }
        public string VNalichie { get; set; }
        public int Amount { get; set; }


        public int PhoneId { get; set; }
        public int UserId { get; set; }
    }
}
