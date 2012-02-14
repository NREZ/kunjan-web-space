using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DemoApplication.Models
{ 
    public class CustomerRepository : ICustomerRepository
    {
        NorthwindEntities context = new NorthwindEntities();

        public IQueryable<Customer> All
        {
            get { return context.Customers; }
        }

        public IQueryable<Customer> AllIncluding(params Expression<Func<Customer, object>>[] includeProperties)
        {
            IQueryable<Customer> query = context.Customers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Customer Find(string id)
        {
            return context.Customers.Where(c => c.Customer_ID == id).FirstOrDefault();
        }

        public void InsertOrUpdate(Customer customer)
        {
            var _customer = Find(customer.Customer_ID);
            if (_customer != null)
            {
                _customer.Address = customer.Address;
                _customer.City = customer.City;
                _customer.Company_Name = customer.Company_Name;
                _customer.Contact_Name = customer.Contact_Name;
                _customer.Contact_Title = customer.Contact_Title;
                _customer.Country = customer.Contact_Title;
                _customer.Fax = customer.Fax;
                _customer.Phone = customer.Phone;
                _customer.Postal_Code = customer.Postal_Code;
                _customer.Region = customer.Region;
                // Haven't taken orders in update just for demo purpose only
            }
            else
            {
                context.Customers.AddObject(customer);
            }
        }

        public void Delete(string id)
        {
            var customer = Find(id);
            context.Customers.DeleteObject(customer);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface ICustomerRepository
    {
        IQueryable<Customer> All { get; }
        IQueryable<Customer> AllIncluding(params Expression<Func<Customer, object>>[] includeProperties);
        Customer Find(string id);
        void InsertOrUpdate(Customer customer);
        void Delete(string id);
        void Save();
    }
}