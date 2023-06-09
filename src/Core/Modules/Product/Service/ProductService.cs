using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using e_commerce_server.src.Core.Utils;
using OpenQA.Selenium.Support.UI;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        private readonly UserRepository userRepository;
        private readonly ProductRepository productRepository;
        private readonly UserService userService;
        public ProductService(AppDbContext context)
        {
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
            userService = new UserService(context);
        }

        public object GetProductsByUserId(int page, int userId)
        {
            var user = Optional.Of(userRepository.GetUserById(userId)).ThrowIfNotPresent(new BadRequestException(UserEnum.USER_NOT_FOUND)).Get();

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            var products = productRepository.GetProductsByUserId(userId);
            
            var paginatedProducts = productRepository.GetProductsByUserIdByPage(page, userId);
            
            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts.Select(product => new
                    {
                        product.id,
                        product.name,
                        product.price,
                        product.discount,
                        product.description,
                        product.created_at,
                        product.updated_at,
                        product.active_status,
                        product.product_status.status,
                        user = new
                        {
                            product.user.id,
                            product.user.name,
                            product.user.phone_number,
                            product.user.avatar,
                            product.user.district_id,
                            city_id = product.user.district.city.id,
                            location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
                    }
                ),
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
        }
        public object GetProductByUserId(int userId, int productId)
        {
            var user = Optional.Of(userRepository.GetUserById(userId)).ThrowIfNotPresent(new BadRequestException(UserEnum.USER_NOT_FOUND)).Get();

            var product = productRepository.GetProductByIdAndUserId(userId, productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            return new
            {
                data = new
                {
                    product.id,
                    product.name,
                    product.price,
                    product.discount,
                    product.description,
                    product.created_at,
                    product.updated_at,
                    product.product_status.status,
                    user = new
                    {
                        product.user.id,
                        product.user.name,
                        product.user.phone_number,
                        product.user.avatar,
                        product.user.active_status,
                        product.user.district_id,
                        city_id = product.user.district.city.id,
                        location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                    },
                    thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                    category = product.category.name,
                }
            };
        }
        public object EditProductById(AddProductDto productDto, int productId, int userId)
        {
            var user = Optional.Of(userRepository.GetUserById(userId)).ThrowIfNotPresent(new BadRequestException(UserEnum.USER_NOT_FOUND)).Get();


            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            if (product.user.id != userId)
            {
                throw new ForbiddenException(ProductEnum.NOT_HAVE_PERMISSION);
            }

            product.name = productDto.name;
            product.description = productDto.description;
            product.price = productDto.price;
            product.status_id = productDto.status_id;
            product.category_id = productDto.category_id;
            product.updated_at = DateTime.Now;
            product.active_status = false;

            productRepository.AddOrUpdateProduct(product, productDto.thumbnailUrls);

            return new
            {
                message = ProductEnum.UPDATE_PRODUCT_SUCCESS,
                data = new
                {
                    product.id,
                    product.name,
                    product.price,
                    product.discount,
                    product.description,
                    product.created_at,
                    product.updated_at,
                    product.product_status.status,
                    product.active_status,
                    user = new
                    {
                        product.user.id,
                        product.user.name,
                        product.user.phone_number,
                        product.user.avatar,
                        product.user.district_id,
                        city_id = product.user.district.city.id,
                        location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                    },
                    thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                    category = product.category.name,
                }
            };
        }
        public object DeleteProductById(int userId, int productId)
        {
            var user = Optional.Of(userRepository.GetUserById(userId)).ThrowIfNotPresent(new BadRequestException(UserEnum.USER_NOT_FOUND)).Get();

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            if (product.user.id != userId)
            {
               throw new ForbiddenException(ProductEnum.NOT_HAVE_PERMISSION);
            }

            productRepository.DeleteProduct(product);

            return new
            {
                message = ProductEnum.DELETE_PRODUCT_SUCCESS
            };
        }
        public object AddProduct(AddProductDto productDto, int userId)
        {
            var user = Optional.Of(userRepository.GetUserById(userId)).ThrowIfNotPresent(new BadRequestException(UserEnum.USER_NOT_FOUND)).Get();

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            ProductData product = new ProductData 
            {
                name = productDto.name,
                description = productDto.description,
                price = productDto.price,
                user_id = userId,
                discount = productDto.discount,
                status_id = productDto.status_id,
                category_id = productDto.category_id,
                active_status = false,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            var newProduct = productRepository.GetProductById(productRepository.AddOrUpdateProduct(product, productDto.thumbnailUrls).id) ?? throw new InternalException(ProductEnum.ADD_PRODUCT_FAILED);

            return new
            {
                message = ProductEnum.ADD_PRODUCT_SUCCESS,
                data = new 
                {
                    newProduct.id,
                    newProduct.name,
                    newProduct.price,
                    newProduct.discount,
                    newProduct.description,
                    newProduct.created_at,
                    newProduct.updated_at,
                    newProduct.product_status.status,
                    newProduct.active_status,
                    user = new
                    {
                        newProduct.user.id,
                        newProduct.user.name,
                        newProduct.user.phone_number,
                        newProduct.user.avatar,
                        location = Convert.ToBoolean(newProduct.user.district_id) ? $"{newProduct.user.district.name}, {newProduct.user.district.city.name}" : null
                    },
                    thumbnails = newProduct.thumbnails.Select(t => t.thumbnail_url),
                    category = newProduct.category.name,
                }
            };
        }
        public object GetAllProducts(int page)
        {
            var products = productRepository.GetProducts();

            var paginatedProducts = productRepository.GetProductsByPage(page);

            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts.Select(product => new 
                    {
                        product.id,
                        product.name,
                        product.price,
                        product.discount,
                        product.description,
                        product.created_at,
                        product.updated_at,
                        product.product_status.status,
                        user = new
                        {
                            product.user.id,
                            product.user.name,
                            product.user.phone_number,
                            product.user.avatar,
                            product.user.district_id,
                            city_id = product.user.district.city.id,
                            location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
                    }),
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
        }
        public object GetProductById(int id)
        {
            var product = productRepository.GetProductById(id);

            if(product == null || !product.active_status)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            return new
            {
                data = new
                {
                    product.id,
                    product.name,
                    product.price,
                    product.discount,
                    product.description,
                    product.created_at,
                    product.updated_at,
                    product.category_id,
                    product.status_id,
                    product.product_status.status,
                    user = new
                    {
                        product.user.id,
                        product.user.name,
                        product.user.phone_number,
                        product.user.avatar,
                        product.user.district_id,
                        city_id = product.user.district.city.id,
                        location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                    },
                    thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                    category = product.category.name,
                }
            };
        }

        public object DeleteProductById(int productId)
        {
            var product = Optional.Of(productRepository.GetProductById(productId)).ThrowIfNotPresent(new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND)).Get();

            productRepository.DeleteProduct(product);

            return new
            {
                message = ProductEnum.DELETE_PRODUCT_SUCCESS
            };
		}
        public object GetPricesComparison(int productId)
        {
            var product = Optional.Of(productRepository.GetProductById(productId)).ThrowIfNotPresent(new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND)).Get();

            var options = new ChromeOptions();// Chạy Chrome ở chế độ ẩn

            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("disable-blink-features=AutomationControlled");

            var driverService = ChromeDriverService.CreateDefaultService();

            driverService.HideCommandPromptWindow = true;

            driverService.Port = 3003;

            var driver = new ChromeDriver(driverService, options);

            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                driver.ExecuteScript("Object.defineProperty(navigator, 'webdriver', { get: () => undefined })");

                driver.Navigate().GoToUrl("https://beecost.vn");

                IWebElement input = driver.FindElement(By.ClassName("focus:outline-none"));

                input.SendKeys(product.name);

                input.SendKeys(Keys.Enter);

                var productElements = driver.FindElements(By.CssSelector(".product-item"));

                List<object> ListProduct = new List<object>();

                for (int i = 0; i < 4; i++)
                {
                    var nameElement = productElements[i].FindElement(By.CssSelector(".line-clamp__2"));

                    var cost = productElements[i].FindElement(By.CssSelector(".text-red-500"));

                    var imgElement = productElements[i].FindElement(By.CssSelector("img"));

                    string img = imgElement.GetAttribute("src");

                    var item = new
                    {
                        name = nameElement.Text,
                        cost = cost.Text,
                        img_url = img
                    };

                    ListProduct.Add(item);
                }

                return new
                {
                    data = ListProduct
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new InternalException(ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }

        public object SearchProducts(string name, int district_id, int city_id, int category, int page)
        {
            var products = productRepository.GetProductsBySearch(name, district_id, city_id, category); 
            
            var paginatedProducts = productRepository.GetProductsBySearchByPage(name, district_id, city_id, category, page);
            
            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts.Select(product => new 
                    {
                        product.id,
                        product.name,
                        product.price,
                        product.discount,
                        product.description,
                        product.created_at,
                        product.updated_at,
                        product.product_status.status,
                        user = new
                        {
                            product.user.id,
                            product.user.name,
                            product.user.phone_number,
                            product.user.avatar,
                            product.user.district_id,
                            city_id = product.user.district.city.id,
                            location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
                }),
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count,
                    currentPage = page
                }
            };
        }
        
        public object GetCategories()
        {
            var categories = productRepository.GetCategories();

            return new
            {
                data = categories.Select(category => new
                {
                    category.id,
                    category.name,
                    category.thumbnail
                })
            };
        }

        public object GetAllProductsByCategories(int id, int page)
        {
            var category = Optional.Of(productRepository.GetCategoryById(id)).ThrowIfNotPresent(new BadRequestException(ProductEnum.CATEGORY_NOT_FOUND)).Get();

            var products = productRepository.GetProductsByCategories(id);

            var paginatedProducts = productRepository.GetProductsByCategoryIdByPage(page, id);

            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts.Select(product => new
                {
                    product.id,
                    product.name,
                    product.price,
                    product.discount,
                    product.description,
                    product.created_at,
                    product.updated_at,
                    product.product_status.status,
                    user = new
                    {
                        product.user.id,
                        product.user.name,
                        product.user.phone_number,
                        product.user.avatar,
                        product.user.district_id,
                        city_id = product.user.district.city.id,
                        location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                    },
                    thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                    category = product.category.name,
                }),
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
        }
        public object AcceptPublishProduct(int productId)
        {

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            productRepository.AcceptProduct(product);

            return new
            {
                message = ProductEnum.ACCEPT_PRODUCT_SUCCESS
            };
        }
        public object GetAllProductsWaiting(int page)
        {
            var products = productRepository.GetProducts();

            var paginatedProducts = productRepository.GetProductsWaitingByPage(page);

            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts.Select(product => new
                {
                    product.id,
                    product.name,
                    product.price,
                    product.discount,
                    product.description,
                    product.created_at,
                    product.updated_at,
                    product.product_status.status,
                    user = new
                    {
                        product.user.id,
                        product.user.name,
                        product.user.phone_number,
                        product.user.avatar,
                        product.user.district_id,
                        city_id = product.user.district.city.id,
                        location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                    },
                    thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                    category = product.category.name,
                }),
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
        }
    }
}
