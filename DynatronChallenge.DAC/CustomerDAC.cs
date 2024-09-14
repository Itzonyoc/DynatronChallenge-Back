using DynatronChallenge.Model;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data;

namespace DynatronChallenge.DAC
{
    public class CustomerDAC : ICustomerDAC
    {
        public CustomerDAC()
        {
        }

        public CustomerModel CreateCustomer(CustomerModel Customer)
        {
            ArrayList parametros = new ArrayList();
            DataSet ds = new DataSet();

            List<CustomerModel> CustomerCollection = GetCustomers();

            int maxId = CustomerCollection.Max(t => t.id);
            Customer.id = maxId + 1;
            Customer.created = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            Customer.last_updated = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");

            CustomerCollection.Add(Customer);

            string path = "Customers.json";
            string json = Serialization.SerializeToJson(CustomerCollection);
            System.IO.File.WriteAllText(path, json);

            return Customer;
        }

        public CustomerModel UpdateCustomer(CustomerModel Customer)
        {
            ArrayList parametros = new ArrayList();
            DataSet ds = new DataSet();

            List<CustomerModel> CustomerCollection = GetCustomers();

            var _customer = CustomerCollection.Find(t => t.id == Customer.id);
            _customer.last_updated = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            _customer.first_name = Customer.first_name;
            _customer.last_name = Customer.last_name;
            _customer.email = Customer.email;

            string path = "Customers.json";
            string json = Serialization.SerializeToJson(CustomerCollection);
            System.IO.File.WriteAllText(path, json);

            return Customer;
        }

        public int DeleteCustomer(int id)
        {
            ArrayList parametros = new ArrayList();
            DataSet ds = new DataSet();

            List<CustomerModel> CustomerCollection = GetCustomers();

            var index = CustomerCollection.FindIndex(t => t.id == id);
            CustomerCollection.RemoveAt(index);

            string path = "Customers.json";
            string json = Serialization.SerializeToJson(CustomerCollection);
            System.IO.File.WriteAllText(path, json);

            return id;
        }

        public List<CustomerModel> GetCustomers()
        {
            ArrayList parametros = new ArrayList();
            DataSet ds = new DataSet();

            List<CustomerModel> CustomerCollection = new List<CustomerModel>();

            string path = "Customers.json";
            using (StreamReader jsonStream = File.OpenText(path))
            {
                var json = jsonStream.ReadToEnd();
                CustomerCollection = Serialization.DeserializeFromJson<List<CustomerModel>>(json);
            }

            return CustomerCollection;
        }
    }
}
