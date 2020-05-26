using IntelligentHabitacion.App.Model;
using System;
using Xunit;

namespace IntelligentHabitacion.App.Test.Useful
{
    public class Model
    {
        [Fact]
        public void AcceptNewFriendModelTest()
        {
            var result = new AcceptNewFriendModel
            {
                EntryDate = DateTime.Today,
                Id = "@FriendId",
                Name = "Friend",
                ProfileColor = "#000000",
                MonthlyRent = 600
            };
            Assert.True(DateTime.Today == result.EntryDate);
            Assert.Equal("@FriendId", result.Id);
            Assert.Equal("Friend", result.Name);
            Assert.Equal("#000000", result.ProfileColor);
            Assert.True(result.MonthlyRent == 600);
        }
    }
}
