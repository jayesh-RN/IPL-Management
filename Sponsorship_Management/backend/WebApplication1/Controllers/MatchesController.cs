using Microsoft.AspNetCore.Mvc;
using SponsorShipManagement.Data;
using SponsorShipManagement.Models;
//using WebApplication1.DAO;
//using WebApplication1.Models;

namespace SponsorShipManagement.Controllers
{


    public class MatchesController : ControllerBase
    {
        private readonly IMatchesDao _matchDao;

        //private readonly SponsorManagmentDao _sponDao;
        //public MatchesController(IMatchesDao IMatchDao, SponsorManagmentDao SponsorManagementDao)
        //{
        //    _matchDao = IMatchDao;
        //    _sponDao = SponsorManagementDao;

        //}


        public MatchesController(IMatchesDao IMatchDao)
        {
            _matchDao = IMatchDao;

        }

        [Route("/Matches")]
        [HttpGet]
        public async Task<ActionResult<List<Matches>>> GetAllMatches()
        {
            var q = await _matchDao.GetMatches();
            if (q == null)
            {
                return NotFound();
            }
                return Ok(q);
        }




        [HttpPost("/q1", Name = "InsertPayment")]
        public async Task<ActionResult<bool>> InsertPayment([FromBody] q1 q)
        {
            if (q == null)
            {
                return BadRequest("Payment Not Found");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var value = await _matchDao.InsertPayment(q);
                    if (value == 1)
                        return Ok(value);
                    else
                        return BadRequest("contractId not found");
                }
                return BadRequest();
            }
        }






        [Route("/q2")]
        [HttpGet]
        public async Task<ActionResult<List<q2>>> Getq2()
        {
            var q = await _matchDao.Getq2();
            if (q == null)
            {
                return NotFound();
            }
            return Ok(q);
        }



        [HttpGet("getByYear", Name = "GetByYear")]
        public async Task<ActionResult<List<q4>>> Getq4(string year)
        {
            var qFound = await _matchDao.Getq4(year);
            if (qFound == null)
            {
                return NotFound();
            }
            return Ok(qFound);
        }





    }
}











































//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebApplicationDemo.DAO;
//using WebApplicationDemo.Data;
//using WebApplicationDemo.Models;

//namespace WebApplicationDemo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductController : ControllerBase
//    {
//        //private readonly IProductRepository _productRepository;
//        private readonly IProductDao _productDao;

//        public ProductController(IProductDao productDao)
//        {
//            _productDao = productDao;
//        }

//        //[Route("/")]
//        //[Route("")]
//        //[Route("/index")]
//        //[HttpGet]
//        //public async Task<ActionResult<List<Product>>> GetProducts() 
//        //{ 
//        //    var products = await _productDao.GetProducts();
//        //    if (products == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    else
//        //    {
//        //        return Ok(products);
//        //    }
//        //}

//        [HttpGet("{id}", Name = "GetProduct")]
//        public async Task<ActionResult<Product>> GetProduct(int id)
//        {
//            Product? productFound = await _productDao.GetProductById(id);
//            if (productFound == null)
//            {
//                return NotFound();
//            }
//            return Ok(productFound);
//        }

//        [HttpGet("all", Name = "GetProducts")]
//        public async Task<ActionResult<List<Product>>> GetProducts()
//        {
//            var productFound = await _productDao.GetProducts();
//            if (productFound == null)
//            {
//                return NotFound();
//            }
//            return Ok(productFound);
//        }

//        [HttpGet("count", Name = "GetProductsCount")]
//        public async Task<ActionResult<int>> GetProductsCount()
//        {
//            var productFound = await _productDao.GetTotalProductsCount();
//            return Ok(productFound);
//        }

//        [HttpGet("getbypricerange", Name = "GetProductsByPriceRange")]
//        public async Task<ActionResult<List<Product>>> GetProductsByPriceRange(decimal min, decimal max)
//        {
//            var productFound = await _productDao.GetProductsByPriceRange(min, max);
//            if (productFound == null)
//            {
//                return NotFound();
//            }
//            return Ok(productFound);
//        }

//        [HttpGet("sortbyprice", Name = "SortProductsByPrice")]
//        public async Task<ActionResult<List<Product>>> SortProductsByPrice()
//        {
//            var productFound = await _productDao.SortProductsByPrice();
//            if (productFound == null)
//            {
//                return NotFound();
//            }
//            return Ok(productFound);
//        }


//        [HttpPost(Name = "CreateProduct")]
//        public async Task<ActionResult<bool>> CreateProduct(Product product)
//        {
//            if (product == null)
//            {
//                return BadRequest("Product Not Found");
//            }
//            else
//            {
//                if (ModelState.IsValid)
//                {
//                    var value = await _productDao.InsertProduct(product);
//                    return Ok(value);
//                }
//                return BadRequest();
//            }
//        }

//        [HttpPut(Name = "UpdatePriceById")]
//        public async Task<ActionResult<int>> UpdatePriceById(int id, decimal price)
//        {
//            if (ModelState.IsValid)
//            {
//                var value = await _productDao.UpdatePriceById(id, price);
//                return Ok(value);
//            }
//            return BadRequest();
//        }

//        [HttpDelete(Name = "DeleteById")]
//        public async Task<ActionResult<int>> DeleteById(int id)
//        {
//            if (ModelState.IsValid)
//            {
//                var value = await _productDao.DeleteById(id);
//                return Ok(value);
//            }
//            return BadRequest();
//        }

//        ////[HttpGet("name", Name = "GetProductByName")]
//        //[HttpGet(@"{name:regex(^[[a-zA-Z]]+$)}", Name = "GetProductByName")]
//        //public async Task<ActionResult<List<Product>>> GetProductByName(string name)
//        //{
//        //    var productsFound = await _productRepository.GetProductsByName(name);
//        //    if (productsFound == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(productsFound);
//        //}


//        //[HttpGet("byrating", Name = "GetProductsByRating")]
//        //public async Task<ActionResult<List<Product>>> GetProductsByRating(double rating)
//        //{
//        //    var productsFound = await _productRepository.GetProductsByRating(rating);
//        //    if (productsFound == null || productsFound.Count==0)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(productsFound);
//        //}


//        //[HttpPut("{id}",Name = "UpdateProduct")]
//        //public async Task<ActionResult<Product>> UpdateProduct(int id, double price)
//        //{
//        //    Product? productFound = null;
//        //    productFound = await _productRepository.UpdateProductPrice(id, price);
//        //    if (productFound == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(productFound);
//        //}


//        //[HttpDelete(Name = "DeleteProduct")]
//        //public async Task<ActionResult<bool>> DeleteProduct(int id) 
//        //{ 
//        //    bool value = await _productRepository.DeleteProduct(id);
//        //    return value? Ok(value): BadRequest("Product Not Found");
//        //}

//        //[HttpGet("{min},{max}", Name = "GetProductsByPriceRange")]
//        //public async Task<ActionResult<List<Product>>> GetProductsByPriceRange([FromQuery]double min, double max)
//        //{
//        //    var productsFound = await _productRepository.GetProductsByPriceRange(min,max);
//        //    if (productsFound == null || productsFound.Count == 0)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(productsFound);
//        //}

//        //[HttpGet("category", Name = "GetProductsByCategory")]
//        //public async Task<ActionResult<List<Product>>> GetProductsByCategory(string category)
//        //{
//        //    var productsFound = await _productRepository.GetProductsByCategory(category);
//        //    if (productsFound == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(productsFound);
//        //}

//        //[HttpGet(@"{code:regex(^[[A-Z]]{{3}}-[[A-Z]]{{3}}$)}",Name = "GetProductByProductCode")]
//        //public async Task<ActionResult<Product>> GetProductByProductCode(string code)
//        //{
//        //    var productFound = await _productRepository.GetProductByProductCode(code);
//        //    if (productFound == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(productFound);
//        //}
//    }
//}
