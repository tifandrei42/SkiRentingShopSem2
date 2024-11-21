using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class StaffMemberManager
    {
        private readonly StaffMemberMediator _staffMemberMediator;

        public StaffMemberManager()
        {
            _staffMemberMediator = new StaffMemberMediator();
        }

        public int AddStaffMember(StaffMember staffMember)
        {
            return _staffMemberMediator.CreateStaffMember(staffMember);
        }

        public StaffMember GetStaffMemberById(int staffId)
        {
            return _staffMemberMediator.GetStaffMemberById(staffId);
        }

        public List<StaffMember> GetAllStaffMembers()
        {
            return _staffMemberMediator.GetAllStaffMembers();
        }

        public void UpdateStaffMember(StaffMember staffMember)
        {
            _staffMemberMediator.UpdateStaffMember(staffMember);
        }

        public void RemoveStaffMember(int staffId)
        {
            _staffMemberMediator.DeleteStaffMember(staffId);
        }

        public StaffMember CheckCredentials(string email, string password)
        {
            var staffMember = _staffMemberMediator.GetStaffMemberByEmail(email);

            if (staffMember != null && BCrypt.Net.BCrypt.Verify(password, staffMember.Password))
            {
                return staffMember;
            }

            return null;
        }
    }
}
