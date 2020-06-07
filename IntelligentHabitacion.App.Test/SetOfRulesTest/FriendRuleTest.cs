using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.Test.Factory;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Useful;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IntelligentHabitacion.App.Test.SetOfRulesTest
{
    public class FriendRuleTest
    {
        private readonly FriendRule _friendRule;

        public FriendRuleTest()
        {
            _friendRule = new FriendRule(GetMokIntelligentHabitacionHttpClient(), new SQlite().GetMokSQLite());
        }

        [Fact]
        public async void GetHouseFriendsSucess()
        {
            try
            {
                var response = await _friendRule.GetHouseFriends();
                Assert.True(response.Count > 0);
                var friend = response.First();
                Assert.True(!string.IsNullOrEmpty(friend.Id));
                Assert.True(!string.IsNullOrWhiteSpace(friend.Name));
                Assert.True(!string.IsNullOrWhiteSpace(friend.Phonenumber1));
                Assert.True(!string.IsNullOrWhiteSpace(friend.Phonenumber2));
                Assert.True(!string.IsNullOrWhiteSpace(friend.ProfileColor));
                Assert.NotNull(friend.EmergencyContact1);
                Assert.NotNull(friend.EmergencyContact2);
                Assert.True(friend.JoinedOn.Date == DateTime.Today);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void ChangeDateJoinOnSucess()
        {
            try
            {
                var friend = await _friendRule.ChangeDateJoinOn("1", DateTime.Today);
                Assert.True(!string.IsNullOrEmpty(friend.Id));
                Assert.True(!string.IsNullOrWhiteSpace(friend.Name));
                Assert.True(!string.IsNullOrWhiteSpace(friend.Phonenumber1));
                Assert.True(!string.IsNullOrWhiteSpace(friend.Phonenumber2));
                Assert.True(!string.IsNullOrWhiteSpace(friend.ProfileColor));
                Assert.True(!string.IsNullOrWhiteSpace(friend.DescriptionDateJoined));
                Assert.NotNull(friend.EmergencyContact1);
                Assert.NotNull(friend.EmergencyContact2);
                Assert.True(friend.JoinedOn.Date == DateTime.Today);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void NotifyFriendOrderHasArrivedSucess()
        {
            try
            {
                await _friendRule.NotifyFriendOrderHasArrived("1");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void RequestCodeToChangeAdministratorSucess()
        {
            try
            {
                await _friendRule.RequestCodeToChangeAdministrator();
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void ChangeAdministratorCodeEmpty()
        {
            await Assert.ThrowsAsync<CodeEmptyException>(() => _friendRule.ChangeAdministrator("", "", ""));
        }

        [Fact]
        public async void ChangeAdministratorPasswordEmpty()
        {
            await Assert.ThrowsAsync<PasswordEmptyException>(() => _friendRule.ChangeAdministrator("1", "", ""));
        }

        [Fact]
        public async void ChangeAdministratorSucess()
        {
            try
            {
                await _friendRule.ChangeAdministrator("1", "1", "1");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        private IIntelligentHabitacionHttpClient GetMokIntelligentHabitacionHttpClient()
        {
            var mock = new Mock<IIntelligentHabitacionHttpClient>();
            mock.Setup(c => c.GetHouseFriends(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token",
                Response = new List<ResponseFriendJson>
                {
                    new ResponseFriendJson
                    {
                        Id = "A1",
                        Name = "Friend 1",
                        JoinedOn = DateTime.Today,
                        ProfileColor = "#FFFFFF",
                        Phonenumbers = new List<ResponsePhonenumberJson>
                        {
                            new ResponsePhonenumberJson
                            {
                                Number = "(31) 9 1111-1111"
                            },
                            new ResponsePhonenumberJson
                            {
                                Number = "(31) 9 2222-2222"
                            }
                        },
                        EmergencyContact = new List<ResponseEmergencyContactJson>
                        {
                            new ResponseEmergencyContactJson
                            {
                                Name = "Contact 1",
                                Phonenumber = "(31) 9 0000-0000",
                                Relationship = "M"
                            },
                            new ResponseEmergencyContactJson
                            {
                                Name = "Contact 2",
                                Phonenumber = "(31) 9 0000-0000",
                                Relationship = "M"
                            }
                        }
                    }
                }
            });
            mock.Setup(c => c.ChangeDateJoinHome(It.IsAny<RequestChangeDateJoinHomeJson>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token",
                Response = new ResponseFriendJson
                {
                    Id = "A1",
                    Name = "Friend 1",
                    DescriptionDateJoined = DateTimeController.DateToStringYearMonthAndDay(DateTime.Today.AddYears(-2).AddMonths(-3).AddDays(-4)),
                    JoinedOn = DateTime.Today,
                    ProfileColor = "#FFFFFF",
                    Phonenumbers = new List<ResponsePhonenumberJson>
                    {
                        new ResponsePhonenumberJson
                        {
                            Number = "(31) 9 1111-1111"
                        },
                        new ResponsePhonenumberJson
                        {
                            Number = "(31) 9 2222-2222"
                        }
                    },
                    EmergencyContact = new List<ResponseEmergencyContactJson>
                    {
                        new ResponseEmergencyContactJson
                        {
                            Name = "Contact 1",
                            Phonenumber = "(31) 9 0000-0000",
                            Relationship = "M"
                        },
                        new ResponseEmergencyContactJson
                        {
                            Name = "Contact 2",
                            Phonenumber = "(31) 9 0000-0000",
                            Relationship = "M"
                        }
                    }
                }
            });
            mock.Setup(c => c.NotifyFriendOrderHasArrived(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token",
                Response = null
            });
            mock.Setup(c => c.RequestCodeToChangeAdministrator(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token",
                Response = null
            });
            mock.Setup(c => c.ChangeAdministrator(It.IsAny<RequestAdminActionsOnFriendJson>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token",
                Response = null
            });

            return mock.Object;
        }
    }
}
