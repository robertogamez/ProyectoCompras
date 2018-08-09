using Pedidos.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedidos.WebApp.Controllers.MVC
{
    public class HomeController : Controller
    {

        public IProductService ProductoServicios { get; set; }

        public HomeController()
        {
           
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}