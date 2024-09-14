using DynatronChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynatronChallenge.DAC
{
    public interface ICustomerDAC
    {
        CustomerModel CreateCustomer(CustomerModel Customer);
        CustomerModel UpdateCustomer(CustomerModel Customer);
        int DeleteCustomer(int id);
        List<CustomerModel> GetCustomers();
    }
}
