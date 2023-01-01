using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Bl
{
    public class CustomerBl
    {
        public void AddNewCustomer(CustomerToAddDTO customerInGroup)
        {
            var ctx = new ShekelTestEntities();
            Factories factories = ctx.Factories.FirstOrDefault(x => x.factoryCode == customerInGroup.FactoryCode);
            Groups groups = ctx.Groups.FirstOrDefault(x => x.groupCode == customerInGroup.GroupCode);
            Customers customer = ctx.Customers.FirstOrDefault(x => x.customerId == customerInGroup.CustomerId);
            if (customer != null)
                throw new Exception("customer is already exists");
            else
            if (factories == null && groups == null)
                throw new Exception("factory and group aren't in list");
            else
            if (factories == null)
                throw new Exception("factory isn't in list");
            else
            if (groups == null)
                throw new Exception("group isn't in list");

            ctx.Customers.Add(new Customers()
            {
                customerId = customerInGroup.CustomerId,
                address = customerInGroup.Address,
                name = customerInGroup.Name,
                phone = customerInGroup.Phone,

            });

            ctx.FactoriesToCustomer.Add(new FactoriesToCustomer()
            {
                customerId = customerInGroup.CustomerId,
                factoryCode = customerInGroup.FactoryCode,
                groupCode = customerInGroup.GroupCode
            });
            ctx.SaveChanges();
        }

        public List<GroupsDTO> GetGroupsDetailes()
        {
            ShekelTestEntities ctx = new ShekelTestEntities();
            List<Groups> groups = ctx.Groups.ToList();
            List<FactoriesToCustomer> factoriesToCustomer = new List<FactoriesToCustomer>();
            List<CustomerToViewDTO> customers = new List<CustomerToViewDTO>();
            List<GroupsDTO> allGroupsAndCustomers = new List<GroupsDTO>();
            foreach (Groups myGroup in groups)
            {
                factoriesToCustomer = ctx.FactoriesToCustomer.Where(x => x.groupCode == myGroup.groupCode).ToList();
                foreach (FactoriesToCustomer myfactoriesToCustomer in factoriesToCustomer)
                {
                    Customers customer = ctx.Customers.FirstOrDefault(x => x.customerId == myfactoriesToCustomer.customerId);
                    customers.Add(new CustomerToViewDTO { CustomerId = customer.customerId, Name = customer.name });
                }
                allGroupsAndCustomers.Add(new GroupsDTO { GroupCode = myGroup.groupCode, GroupName = myGroup.groupName, customers = customers });
                customers = new List<CustomerToViewDTO>();

            }
            return allGroupsAndCustomers;
        }
    }
}
