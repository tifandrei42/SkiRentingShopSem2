using ClassLibrary.Managers;
using ClassLibrary.ObjectClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    public class ContentModel : PageModel
    {
        public List<Equipment> EquipmentList { get; set; }

        public void OnGet()
        {
            EquipmentManager equipmentManager = new EquipmentManager();
            EquipmentList = equipmentManager.GetAllEquipment();
        }
    }
}
