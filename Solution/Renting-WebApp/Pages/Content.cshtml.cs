using BusinessLogic.Entities;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Renting_Website.Pages
{
    public class ContentModel : PageModel
    {
        private readonly EquipmentManager _equipmentManager;

        public List<Equipment> EquipmentList { get; set; } = new List<Equipment>();
        public List<Equipment> PagedEquipmentList { get; set; } = new List<Equipment>();

        [BindProperty(SupportsGet = true)]
        public string BrandFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }

        public ContentModel(EquipmentManager equipmentManager)
        {
            _equipmentManager = equipmentManager;
        }

        public void OnGet()
        {
            EquipmentList = _equipmentManager.GetFilteredEquipment(BrandFilter, NameFilter, MinPrice, MaxPrice);

            TotalPages = (int)Math.Ceiling(EquipmentList.Count / (double)PageSize);

            PagedEquipmentList = EquipmentList
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
