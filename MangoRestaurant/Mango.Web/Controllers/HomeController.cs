using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductService _productService;
		

		public HomeController(ILogger<HomeController> logger, IProductService productService)
		{
			_logger = logger;
			
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			List<ProductDto> list = new();
			var response = await _productService.GetAllProductsAsync<ResponseDto>();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		[Authorize]
		public async Task<IActionResult> Details(int productId)
		{
			ProductDto model = new();
			var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
			if (response != null && response.IsSuccess)
			{
				model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
			}
			return View(model);
		}

		[HttpPost]
		[ActionName("Details")]
		[Authorize]
		public async Task<IActionResult> DetailsPost(ProductDto productDto)
		{
			

			

			var resp = await _productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId);
			if (resp != null && resp.IsSuccess)
			{
				
			}
			
		

			return View(productDto);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[Authorize]
		public async Task<IActionResult> Login()
		{

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}

	}
}