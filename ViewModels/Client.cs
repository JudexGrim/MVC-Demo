﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class Client : IViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
