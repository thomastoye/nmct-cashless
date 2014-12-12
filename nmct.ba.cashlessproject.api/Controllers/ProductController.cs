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

        public Product Post(Product p)
        {
            int id = Products.InsertProduct(p);
            p.ID = id;
            return p;
        }

        public HttpStatusCode Put(long id, Product prod)
        {
            Products.UpdateProduct(id, prod);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            Products.DeleteProduct(id);
            return HttpStatusCode.OK;
        }
    }
}