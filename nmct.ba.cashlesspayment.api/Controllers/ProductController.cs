using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class ProductController : ApiController
    {
        public List<Product> Get()
        {
            return Products.GetProducts();
        }

        public HttpResponseMessage Post(Product p)
        {
            Products.InsertProduct(p);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}