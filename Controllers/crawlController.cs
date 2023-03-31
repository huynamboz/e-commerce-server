using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using e_commerce_server.Model;
using System.Threading;
using OpenQA.Selenium.DevTools.V109.Database;
using OpenQA.Selenium.Edge;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class crawlController : ControllerBase
    {
        [HttpGet]
        public IActionResult get(string key)
        {
            var options = new EdgeOptions();// Chạy Chrome ở chế độ ẩn
            var driver = new EdgeDriver(options);

            // Mở trang web Shopee.vn
            driver.Navigate().GoToUrl("https://shopee.vn/search?keyword=" + key);

            // Tìm kiếm sản phẩm và nhấn nút tìm kiếm
            //var searchBox = driver.FindElement(By.ClassName("shopee-searchbar-input__input"));
            //Console.WriteLine(searchBox.Text);
            //searchBox.SendKeys("laptop");
            //var searchButton = driver.FindElement(By.ClassName("shopee-searchbar__search-button"));
            //searchButton.Click();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var productElements = driver.FindElements(By.CssSelector(".shopee-search-item-result__item"));
                List<compareCost> strings = new List<compareCost>();

                for (int i = 0; i < 4; i++)
                {
                    compareCost item = new compareCost();
                    var nameElement = productElements[i].FindElement(By.CssSelector(".Cve6sh"));
                    var cost = productElements[i].FindElement(By.CssSelector(".ZEgDH9"));
                    var eleimg = productElements[i].FindElement(By.CssSelector("img"));
                    string img = eleimg.GetAttribute("src");
                    item.name = nameElement.Text;
                    item.cost = cost.Text;
                    item.url_img = img;
                    strings.Add(item);
                }

                // Đóng trình duyệt
                driver.Quit();
                return Ok(strings);
            } catch(Exception ex)
            {
                driver.Quit();
                return NotFound(ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
        
    }
}
