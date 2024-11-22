using BusinessLogic.Entities;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Renting_Website.Pages
{
    public class ContentModel : PageModel
    {
        public List<Equipment> EquipmentList { get; set; } = new List<Equipment>();

        private readonly EquipmentManager _equipmentManager;

        [BindProperty(SupportsGet = true)]
        public string BrandFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        public ContentModel(EquipmentManager equipmentManager)
        {
            _equipmentManager = equipmentManager;
        }

        public void OnGet()
        {
            EquipmentList = _equipmentManager.GetFilteredEquipment(BrandFilter, MinPrice, MaxPrice);
        }
    }
}
