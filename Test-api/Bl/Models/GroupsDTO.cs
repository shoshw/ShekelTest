using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Bl
{
    public class GroupsDTO
    {
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public List<CustomerToViewDTO> customers = new List<CustomerToViewDTO>();
    }
}
