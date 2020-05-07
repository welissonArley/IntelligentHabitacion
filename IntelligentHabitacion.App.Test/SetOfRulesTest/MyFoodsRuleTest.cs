using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.Test.Factory;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.App.Test.SetOfRulesTest
{
    public class MyFoodsRuleTest
    {
        private readonly MyFoodsRule _myFoodRule;

        public MyFoodsRuleTest()
        {
            _myFoodRule = new MyFoodsRule(GetMokIntelligentHabitacionHttpClient(), new SQlite().GetMokSQLite());
        }

        [Fact]
        public void CreateWithoutName()
        {
            Assert.ThrowsAsync<ProductNameEmptyException>(() => _myFoodRule.AddItem(new Model.FoodModel
            {
                Amount = 5,
                DueDate = DateTime.Today,
                Manufacturer = "M",
                Type = Model.Type.Box
            }));
        }

        [Fact]
        public void CreateWithInvalidQuantity()
        {
            Assert.ThrowsAsync<AmountProductsInvalidException>(() => _myFoodRule.AddItem(new Model.FoodModel
            {
                Amount = -5,
                DueDate = DateTime.Today,
                Name = "N",
                Manufacturer = "M",
                Type = Model.Type.Box
            }));
        }

        [Fact]
        public async void CreateSucess()
        {
            try
            {
                var response = await _myFoodRule.AddItem(new Model.FoodModel
                {
                    Amount = 5,
                    DueDate = DateTime.Today,
                    Manufacturer = "M",
                    Name = "N",
                    Type = Model.Type.Box
                });
                Assert.NotEmpty(response);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void DeleteSucess()
        {
            try
            {
                await _myFoodRule.DeleteMyFood("Id");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void GetMyFoods()
        {
            try
            {
                var list = await _myFoodRule.GetMyFoods();
                Assert.True(list.Count > 0);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void ChangeQuantity()
        {
            try
            {
                await _myFoodRule.ChangeQuantity(new Model.FoodModel
                {
                    Id = "Id",
                    Amount = 5,
                    DueDate = DateTime.Today,
                    Manufacturer = "M",
                    Name = "N",
                    Type = Model.Type.Box
                });
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void EditItem()
        {
            try
            {
                await _myFoodRule.EditItem(new Model.FoodModel
                {
                    Id = "Id",
                    Amount = 5,
                    DueDate = DateTime.Today,
                    Manufacturer = "M",
                    Name = "N",
                    Type = Model.Type.Box
                });
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void CloneTest()
        {
            var x1 = new FoodModel
            {
                Amount = 5,
                DueDate = DateTime.Today,
                Id = "1",
                Manufacturer = "M",
                Name = "N",
                Type = Model.Type.Box
            };

            var x2 = x1.Clone();

            Assert.Equal(x1.Manufacturer, x2.Manufacturer);
            Assert.Equal(x1.Name, x2.Name);
            Assert.Equal(x1.Amount, x2.Amount);
            Assert.NotEqual(x1, x2);
        }

        private IIntelligentHabitacionHttpClient GetMokIntelligentHabitacionHttpClient()
        {
            var mock = new Mock<IIntelligentHabitacionHttpClient>();
            mock.Setup(c => c.AddMyFood(It.IsAny<RequestAddMyFoodJson>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Response = "Id",
                Token = "token"
            });
            mock.Setup(c => c.DeleteMyFood(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token"
            });
            mock.Setup(c => c.GetMyFoods(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token",
                Response = new List<ResponseMyFoodJson>
                {
                    new ResponseMyFoodJson
                    {
                        Id = "1",
                        Manufacturer = "M",
                        Amount = 1,
                        DueDate = DateTime.Today,
                        Name = "N",
                        Type = Communication.Response.Type.Box
                    }
                }
            });
            mock.Setup(c => c.EditMyFood(It.IsAny<RequestEditMyFoodJson>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token"
            });
            mock.Setup(c => c.ChangeQuantityMyFood(It.IsAny<RequestChangeQuantityMyFoodJson>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token"
            });

            return mock.Object;
        }
    }
}
