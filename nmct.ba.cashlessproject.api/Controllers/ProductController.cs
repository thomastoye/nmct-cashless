using nmct.ba.cashlessproject.api.Helpers;
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
    [System.Web.Http.Authorize(Roles = "OrganisationManager")]
    public class ProductController : ApiController
    {
        public List<Product> Get()
        {
            return Products.GetProducts(Claims.GetConnectionString(User));
        }

        public Product Post(Product p)
        {
            int id = Products.InsertProduct(Claims.GetConnectionString(User), p);
            p.ID = id;
            return p;
        }

        public HttpStatusCode Put(long id, Product prod)
        {
            Products.UpdateProduct(Claims.GetConnectionString(User), id, prod);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            Products.DeleteProduct(Claims.GetConnectionString(User), id);
            return HttpStatusCode.OK;
        }
    }
}