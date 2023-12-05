using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{{\"Id\": {Id}, \"Code\": \"{Code}\", \"Name\": \"{Name}\", \"Age\": {Age}}}";
        }
    }
}
