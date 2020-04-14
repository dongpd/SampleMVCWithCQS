using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using MediatR;

using SampleMVCWithCQS2.Application.Commands;
using SampleMVCWithCQS2.Application.Queries;
using SampleMVCWithCQS2.Models;



namespace SampleMVCWithCQS2.Controllers
{

    public class HomeController : Controller
    {
        private readonly IProductQueries _productQueries;
        private readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public HomeController(IProductQueries productQueries, IMediator mediator, IMapper mapper)
        {
            _productQueries = productQueries;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Products()
        {
            var products = await _productQueries.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new Product() { IsNew = true };
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createProductCommand = _mapper.Map<CreateProductCommand>(product);

                    await _mediator.Send(createProductCommand);

                    //Redirect to historic list.
                    return RedirectToAction("Products");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"It was not possible to create a new product, please try later on ({ex.GetType().Name} - {ex.Message})");
            }

            return View("Create", product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productQueries.GetProductAsync(productId);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var saveProductCommand = _mapper.Map<SaveProductCommand>(product);

                    await _mediator.Send(saveProductCommand);

                    //Redirect to historic list.
                    return RedirectToAction("Products");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"It was not possible to edit the product, please try later on ({ex.GetType().Name} - {ex.Message})");
            }

            return View("Edit", product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {

            var product = await _productQueries.GetProductAsync(productId);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _productQueries.GetProductAsync(productId);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deleteProductCommand = _mapper.Map<DeleteProductCommand>(product);

                    await _mediator.Send(deleteProductCommand);

                    //Redirect to historic list.
                    return RedirectToAction("Products");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"It was not possible to remove the product, please try later on ({ex.GetType().Name} - {ex.Message})");
            }

            return View("Delete", product);
        }
    }
}