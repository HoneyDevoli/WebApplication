﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        

    }
}
