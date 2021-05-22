using StoreDL;
using Models = StoreModels;
using Entity = StoreDL.Entities;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace StoreTest
{
    public class RepoTest
    {
        private readonly DbContextOptions<Entity.BearlyCampingDataContext> options;
        public DbContextOptions<StoreDL.Entities.BearlyCampingDataContext> Options => options;
        public RepoTest()
        {
            options = new DbContextOptionsBuilder<Entity.BearlyCampingDataContext>()
            .UseSqlite("Filename=Test.db")
            .Options;
            Seed();
        }

        /// <summary>
        /// Checks to see if RepoDB.GetAllStores will return the full list of stores
        /// </summary>
        [Fact]
        public void GetAllStoresShouldReturnAllStores()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange: passes in the test context
                DAO _repo = new RepoDB(context);

                //Act:
                var stores = _repo.GetAllStores();

                //Assert
                Assert.Equal(2, stores.Count);
            }
        }

        /// <summary>
        /// Checks to see if store is added when calling RepoDB.AddStore(store)
        /// </summary>
        [Fact]
        public void AddStoreShouldAddStore()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                DAO _repo = new RepoDB(context);
                _repo.AddStore
                (
                    new Models.Store("City", "State")
                );
            }
            using (var assertContext = new Entity.BearlyCampingDataContext(options))
            {
                var result = assertContext.Stores.FirstOrDefault(store => store.StoreCity.Equals("City") && store.StoreState.Equals("State"));
                Assert.NotNull(result);
                Assert.Equal("City", result.StoreCity);
            }
        }

        [Fact]
        public void UpdateProductUpdatesTheProduct()
        {
            using (var context = new Entity.BearlyCampingDataContext(options))
            {
                DAO _repoDB = new RepoDB(context);

                Models.Product product = new Models.Product()
                {
                    ISBN = "1111111111111",
                    Name = "Fishing Pole",
                    Price = 20.20M
                };
                
                _repoDB.UpdateProduct(product);

                using (var assertContext = new Entity.BearlyCampingDataContext(options))
            {
                var result = assertContext.Products.FirstOrDefault(product => product.Isbn.Equals("1111111111111"));
                Assert.NotNull(result);
                Assert.NotEqual("Fshing Pole", result.ProductName);
            }
            }

        }

        [Fact]
        public void RemoveProductRemovesTheProduct()
        {
            using (var context = new Entity.BearlyCampingDataContext(options))
            {
                DAO _repoDB = new RepoDB(context);

                Models.Product product = new Models.Product()
                {
                    ISBN = "1111111111111",
                    Name = "Fshing Pole",
                    Price = 20.20M
                };
                
                _repoDB.RemoveProduct(product);

                using (var assertContext = new Entity.BearlyCampingDataContext(options))
            {
                var result = assertContext.Products.FirstOrDefault(product => product.Isbn.Equals("1111111111111"));
                Assert.Null(result);
            }
            }

        }

        [Fact]
        public void GetAllProductsShouldReturnProducts()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange: passes in the test context
                DAO _repo = new RepoDB(context);

                //Act:
                var products = _repo.GetProducts();

                //Assert
                Assert.Equal(2, products.Count);
            }
        }

        [Fact]
        public void GetOrderForStoreReturnsOrderForStore()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange
                DAO _repoDB = new RepoDB(context);
                int storeID = 1;
                
                //Act
                var Orders = _repoDB.GetOrdersFor(storeID);

                //Assert
                Assert.Equal(1, Orders.Count);
            }
        }

        [Fact]
        public void GetUserShouldReturnUser()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange
                DAO _repoDB = new RepoDB(context);
                string test = "Robbie";
                //Act
                Models.User assertUser = _repoDB.GetUser(test);

                //Assert
                Assert.Equal(assertUser.LastName, "Fisher");
            }
        }

        [Fact]
        public void GetProductGetsAProduct()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange
                DAO _repoDB = new RepoDB(context);
                string test = "1111111111111";
                //Act
                Models.Product assertProduct = _repoDB.GetProduct(test);

                //Assert
                Assert.Equal(assertProduct.Name, "Fshing Pole");
            }
        }

        [Fact]
        public void GetOrdersForCustomerReturnsRelated()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange
                DAO _repoDB = new RepoDB(context);
                Models.User model = new Models.User()
                {
                    UserName = "Robbie",
                    Password = "Password",
                    FirstName = "Robbie",
                    LastName = "Fisher",
                    created = DateTime.Now
                };

                //Act
                List<Models.Order> test = _repoDB.GetOrdersFor(model);
                //Assert
                Assert.Equal(model.UserName, test[0].UserName);
            }
        }

        [Fact]
        public void AddOrderAddsOrder()
        {
            using(var context = new Entity.BearlyCampingDataContext(options))
            {
                //Arrange
                DAO _repoDB = new RepoDB(context);
                Models.Order toBeAdded = new Models.Order()
                {
                    Create = DateTime.Now,
                    UserName = "Robbie",
                    StoreID = 2,
                    Total = 232.30M
                };

                //Act
                Models.Order test = _repoDB.AddOrder(toBeAdded);

                //Assert
                Assert.Equal(test.UserName, toBeAdded.UserName);
            }
        }


        /// <summary>
        /// Instantiates a new test database for testing 
        /// </summary>
        private void Seed()
        {
            using(var context = new Entity.BearlyCampingDataContext(Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Stores.AddRange
                (
                    new Entity.Store
                    {
                        StoreId = 1,
                        StoreCity = "Chatanooga",
                        StoreState = "TN",
                    },
                    new Entity.Store
                    {
                        StoreId = 2,
                        StoreCity = "Canton",
                        StoreState = "Georgia"
                    }
                );

                context.Products.AddRange
                (
                    new Entity.Product
                    {
                        ProductName = "Fshing Pole",
                        Isbn = "1111111111111",
                        Price = 20.20M
                    },
                    new Entity.Product
                    {
                        ProductName = "Dry Bag",
                        Isbn = "1111111111112",
                        Price = 15.25M
                    }
                );

                context.Accounts.AddRange
                (
                    new Entity.Account
                    {
                        UserName = "Robbie",
                        UserPassword = "Password",
                        Created = DateTime.Now,
                        FirstName = "Robbie",
                        LastName = "Fisher"
                    },
                    new Entity.Account
                    {
                        UserName = "Falyn",
                        UserPassword = "Password",
                        Created = DateTime.Now,
                        FirstName = "Falyn",
                        LastName = "Fisher"
                    }
                );

                context.Orders.AddRange
                (
                    new Entity.Order
                    {
                        OrderNumber = 1,
                        StoreId = 1,
                        UserName = "Robbie",
                        Total = 145.00M,
                        DateCreated = DateTime.Now
                    },
                    new Entity.Order
                    {
                        OrderNumber = 2,
                        StoreId = 2,
                        UserName = "Falyn",
                        Total = 165.10M,
                        DateCreated = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}