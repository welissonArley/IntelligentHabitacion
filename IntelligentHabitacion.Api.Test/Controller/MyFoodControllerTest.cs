using IntelligentHabitacion.Api.Controllers.V1;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Test.FactoryFake;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class MyFoodControllerTest : BaseControllerTest
    {
        private readonly MyFoodController _controller;

        public MyFoodControllerTest()
        {
            _controller = new MyFoodController(new MyFoodFactoryFake().GetRule())
            {
                ControllerContext = GetHttpContext()
            };
        }

        [Fact]
        public void CreateInvalidFood()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/AddFoods/");
            var result = _controller.AddFood(new RequestAddMyFoodJson());
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 2);
        }

        [Fact]
        public void CreateFoodSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/AddFoods/");
            var result = _controller.AddFood(new RequestAddMyFoodJson
            {
                Amount = 5,
                DueDate = DateTime.Today,
                Manufacturer = "M",
                Name = "N",
                Type = Communication.Response.Type.Box
            });
            Assert.IsType<CreatedResult>(result);
            var value = (string)((CreatedResult)result).Value;
            Assert.True(!string.IsNullOrEmpty(value));
        }

        [Fact]
        public void GetMyFoods()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/MyFoods/");
            var result = _controller.MyFoods();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetMyFoodsListEmpty()
        {
            var controller = new MyFoodController(new MyFoodFactoryFake().GetRuleUserWithoutFood())
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/MyFood/MyFoods/");
            var result = controller.MyFoods();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/Delete/");
            var encryptedId = new MyFood { Id = 700 }.EncryptedId();
            var result = _controller.DeleteFoods(encryptedId);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/Delete/");
            var encryptedId = new MyFood { Id = 1 }.EncryptedId();
            var result = _controller.DeleteFoods(encryptedId);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void EditItemNotFound()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/EditFood/");
            var result = _controller.EditFood(new RequestEditMyFoodJson
            {
                Id = new MyFood { Id = 700 }.EncryptedId()
            });
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void EditItemInvalid()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/EditFood/");
            var result = _controller.EditFood(new RequestEditMyFoodJson
            {
                Id = new MyFood { Id = 1 }.EncryptedId()
            });
            Assert.IsType<BadRequestObjectResult>(result);
            var value = (ErrorJson)((BadRequestObjectResult)result).Value;
            Assert.True(value.Errors.Count == 2);
        }

        [Fact]
        public void EditFoodSucess()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/EditFood/");
            var result = _controller.EditFood(new RequestEditMyFoodJson
            {
                Id = new MyFood { Id = 1 }.EncryptedId(),
                Amount = 5,
                DueDate = DateTime.Today,
                Manufacturer = "M",
                Name = "N",
                Type = Communication.Response.Type.Box
            });
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void ChangeQuantityFoodNotFound()
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/ChangeQuantity/");
            var result = _controller.ChangeQuantity(new RequestChangeQuantityJson
            {
                Id = new MyFood { Id = 700 }.EncryptedId(),
                Quantity = 5
            });
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(ChangeQuantityData))]
        public void ChangeQuantitySucess(RequestChangeQuantityJson request)
        {
            _controller.HttpContext.Request.Path = new PathString("/MyFood/ChangeQuantity/");
            var result = _controller.ChangeQuantity(request);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void GetWithError()
        {
            var controller = new MyFoodController(null)
            {
                ControllerContext = GetHttpContext()
            };
            controller.HttpContext.Request.Path = new PathString("/MyFood/MyFoods/");
            var result = controller.MyFoods();
            Assert.IsType<ObjectResult>(result);
        }

        public static IEnumerable<object[]> ChangeQuantityData => new List<object[]>
        {
            new object[] { new RequestChangeQuantityJson { Id = new MyFood { Id = 1 }.EncryptedId(), Quantity = 0 } },
            new object[] { new RequestChangeQuantityJson { Id = new MyFood { Id = 1 }.EncryptedId(), Quantity = 5 } }
        };
}
}
