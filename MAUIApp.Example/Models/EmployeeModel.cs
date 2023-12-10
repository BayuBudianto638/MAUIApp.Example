using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class EmployeeDetail
    {
        public List<EmployeeModel> data { get; set; }
        public int total { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }

    public class ListEmployee
    {
        public int status { get; set; }
        public bool error { get; set; }
        public string detail { get; set; }
        public string message { get; set; }
        public EmployeeDetail data { get; set; }
    }

    public class EmployeeData
    {
        public int status { get; set; }
        public bool error { get; set; }
        public string detail { get; set; }
        public string message { get; set; }
        public EmployeeModel data { get; set; }
    }
}
