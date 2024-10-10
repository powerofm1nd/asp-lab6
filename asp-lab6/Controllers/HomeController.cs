using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using asp_lab6.Models;
using asp_lab6.Services;

namespace asp_lab6.Controllers;

public class HomeController(IProductService productService, IOrderService orderService, IUserService userService)
    : Controller
{
    private readonly IProductService _productService = productService;
    private readonly IOrderService _orderService = orderService;
    private readonly IUserService _userService = userService;

    public IActionResult Index()
    {
        return View();
    }
    
    
    /// <summary>
    /// Action для отримання кількості Видів товару для замовлення
    /// </summary>
    public IActionResult StartOrder(string firstName, string lastName, int age)
    {
        var sb = new StringBuilder();
        
        if (age < 16 || age > 100)
        {
            sb.Append("Something wrong with your age. We only allow to order more than 16 years. \n");
        }
        
        if (string.IsNullOrEmpty(firstName))
        {
            sb.Append("You must enter your first name! \n");
        }
        
        if (string.IsNullOrEmpty(lastName))
        {
            sb.Append("You must enter your last name! \n");
        }
        
        ViewData["ErrorMessage"] = sb.ToString();

        if (string.IsNullOrEmpty(ViewData["ErrorMessage"] as string))
        {
            var newUser = _userService.CreateUser(firstName, lastName, age);
            ViewData["newUserGuid"] = newUser.Id;
            ViewData["AvailableProducts"] = _productService.GetProducts();
        }
        
        
        return View();
    }

    
    public IActionResult CreateOrder(Guid userGuid,
                                     int quantityOfProductTypes, 
                                     int currentProductId, 
                                     int quantityOfCurrentProduct, 
                                     int currentNumberOfForm = 1,
                                     Guid? orderId = null)
    {
        ViewData["currentNumberOfForm"] = currentNumberOfForm;
        ViewData["quantityOfProductTypes"] = quantityOfProductTypes;
        
        if (quantityOfProductTypes >= 1 || quantityOfProductTypes <= _productService.GetProducts().Count)
        {
            OrderModel currentOrder = orderId.HasValue ? _orderService.GetOrder((Guid)orderId) : _orderService.CreateOrder(userGuid);
            
            var products = _productService.GetProducts();
            ViewData["AvailableProducts"] = products;
            ViewData["orderId"] = currentOrder.Id;
            
            //Якщо товар не обрано
            if (currentProductId == 0)
            {
                return View(); 
            }
            else
            {
                //Оновлення замовлення
                var currentProduct = _productService.FindProductById(currentProductId);
                _orderService.AddProduct(currentOrder, currentProduct, quantityOfCurrentProduct);
                
                //Видалення продуктів, які вже були обрані
                currentOrder.Products.ForEach(orderItem =>
                {
                    products.RemoveAll(p => p.id == orderItem.Product.id);
                });
                ViewData["AvailableProducts"] = products;
            }

            //Якщо остання форма
            if (currentNumberOfForm.Equals(quantityOfProductTypes))
            {
                //Показ створенного замовлення
                return RedirectToAction("ShowOrder",  new { orderId = currentOrder.Id }); 
            }
            else
            {
                //Наступна форма для заповнення
                ViewData["currentNumberOfForm"] = ++currentNumberOfForm;
                return View(); 
            }
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    public IActionResult ShowOrder(Guid orderId)
    {
        var order = _orderService.GetOrder(orderId);
        ViewData["totalPrice"] = order.Products.Sum(p => p.Product.Price * p.Quantity);
        
        var sb = new StringBuilder();
        
        foreach (var orderItemModel in order.Products)
        {
            if (orderItemModel.Quantity > orderItemModel.Product.AvailableQuantity)
            {
                sb.Append($"You have ordered more '{orderItemModel.Product.Name}' pizzas than we currently have available.");
            }
        }

        if (sb.ToString() != String.Empty)
        {
            ViewData["warningMessage"] = sb.ToString();
        }
        
        return View(order);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}