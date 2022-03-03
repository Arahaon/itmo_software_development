using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppCoreProduct.Models;

namespace WebAppCoreProduct.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;
        public Product Product { get; set; }
        public string MessageResult { get; set; }

        public ProductModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public void OnGet(string name, decimal? price)
        {
            MessageResult = "Для товара можно определить скидку";
        }

        public void OnPost(string name, decimal? price)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name))
            {
                MessageResult = "Переданы некорректные данные. Повторите ввод";
                return;
            }
            var result = price * (decimal?)0.18;
            MessageResult = $"Для товара {name} с ценой {price} скидка получится {result}";
            Product.Price = price;
            Product.Name = name;
        }

        public void OnPostDiscont(string name, decimal? price, double discont)
        {
            Product = new Product();
            var result = price * (decimal?)discont / 100;
            MessageResult = $"Для товара {name} с ценой {price} и скидкой {discont} получится {result}";
            Product.Price = price;
            Product.Name = name;
        }

        public async Task OnPostDollar(string name, decimal? price)
        {
            var content = (await httpClientFactory.CreateClient().GetAsync("https://www.cbr-xml-daily.ru/daily_json.js")).Content;
            var responseString = await content.ReadAsStringAsync();
            var usd = JsonConvert.DeserializeAnonymousType(responseString, new { Valute = new { USD = new { Value = default(double) } } }).Valute.USD.Value;
            Product = new Product();
            var result = (price.HasValue) ? (double)price.Value * 0.18 / usd : 0;
            MessageResult = $"Для товара {name} с ценой {price} скидка в долларах получится ${result:F3}";
            Product.Price = price;
            Product.Name = name;
        }
    }

    public class CurrencyState
    {
        public class USDCurrency
        {
            public string Value { get; set; }
        }

        public USDCurrency USD { get; set; }
    }
}
