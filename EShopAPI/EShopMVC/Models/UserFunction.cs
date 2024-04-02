﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopMVC.Models
{
    public class UserFunction
    {
        public int? UserFunctionID { get; set; }
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public int IsView { get; set; }
        public int IsUpdate { get; set; }
        public int IsDelete { get; set; }
    }
}