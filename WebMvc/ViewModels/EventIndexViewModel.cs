using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventIndexViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<EventItem> EventItems { get; set; }
        public IEnumerable<SelectListItem> Addresses { get; set; }
        public PaginationInfo PaginationInfo { get; set; }

        public int? CategoryFilterApplied { get; set; }
        public int? TypeFilterApplied { get; set; }
        public int? AddressesFilterApplied { get; set; }
    }
}
