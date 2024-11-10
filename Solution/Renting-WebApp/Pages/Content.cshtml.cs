 using ClassLibrary.Managers;
using ClassLibrary.ObjectClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    public class ContentModel : PageModel
    {
        public List<Equipment> EquipmentList { get; set; }
        private readonly EquipmentManager _equipmentManager;

        public ContentModel(EquipmentManager equipmentManager)
        {
            _equipmentManager = equipmentManager;
        }

        public void OnGet()
        {

            EquipmentList = _equipmentManager.GetAllEquipment();
        }
    }
}
