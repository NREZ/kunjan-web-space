using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApplication;
using DemoApplication.Models;

namespace DemoApplication.Controllers
{   
	public class CustomersController : Controller
	{
		private readonly ICustomerRepository customerRepository;

		public CustomersController(ICustomerRepository customerRepository)
		{
			this.customerRepository = customerRepository;
		}

		//
		// GET: /Customers/

		public ViewResult Index()
		{
			return View(customerRepository.AllIncluding(customer => customer.Orders));
		}

		//
		// GET: /Customers/Details/5

		public ViewResult Details(string id)
		{
			return View(customerRepository.Find(id));
		}

		//
		// GET: /Customers/Create

		public ActionResult Create()
		{
			return View();
		} 

		//
		// POST: /Customers/Create

		[HttpPost]
		public ActionResult Create(Customer customer)
		{
			if (ModelState.IsValid) {
				customerRepository.InsertOrUpdate(customer);
				customerRepository.Save();
				return RedirectToAction("Index");
			} else {
				return View();
			}
		}
		
		//
		// GET: /Customers/Edit/5
 
		public ActionResult Edit(string id)
		{
			 return View(customerRepository.Find(id));
		}

		//
		// POST: /Customers/Edit/5

		[HttpPost]
		public ActionResult Edit(Customer customer)
		{
			if (ModelState.IsValid) {
				customerRepository.InsertOrUpdate(customer);
				customerRepository.Save();
				return RedirectToAction("Index");
			} else {
				return View();
			}
		}

		//
		// GET: /Customers/Delete/5
 
		public ActionResult Delete(string id)
		{
			return View(customerRepository.Find(id));
		}

		//
		// POST: /Customers/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(string id)
		{
			customerRepository.Delete(id);
			customerRepository.Save();

			return RedirectToAction("Index");
		}
	}
}

