﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Orders.Dtos
{
    public class ProductForOrderListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Number { get; set; }
    }
}
