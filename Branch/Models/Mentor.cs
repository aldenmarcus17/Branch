using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Branch.Models
{
    internal class Mentor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Tag { get; set; }
    }
}
