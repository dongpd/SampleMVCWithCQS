using System.Collections.Generic;
using System;
using SampleMVCWithCQS2Core.Domain;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SampleMVCWithCQS2.Application.Queries
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }

        public bool InStock { get; set; }

        public List<SelectListItem> ColorsFullList
        {
            get
            {
                return Enum.GetValues(typeof(Colors)).Cast<Colors>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString(),
                    Selected = Color == v.ToString()
                }).ToList();
            }
        }

    }
}