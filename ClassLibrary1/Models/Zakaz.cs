using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Delete.ClassLibrary1.Models
{
    public class Zakaz
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public int Number { get; set; }
        public int PokupkaId { get; set; }

        public List<Pokupka> Pokupkas { get; set; } = new List<Pokupka>();

    }
}
