using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Models
{
    public class ResponseModel
    {
        public int status { get; set; }
        public bool error { get; set; }
        public string detail { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }
}
