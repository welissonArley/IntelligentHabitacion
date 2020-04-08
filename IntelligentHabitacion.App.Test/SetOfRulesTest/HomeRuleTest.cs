using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.Test.Factory;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Moq;
using Xunit;

namespace IntelligentHabitacion.App.Test.SetOfRulesTest
{
    public class HomeRuleTest
    {
        private readonly HomeRule _homeRule;

        public HomeRuleTest()
        {
            _homeRule = new HomeRule(GetMokIntelligentHabitacionHttpClient(), new SQlite().GetMokSQLite());
        }

        [Fact]
        public async void ValidadeZipException()
        {
            try
            {
                await _homeRule.ValidadeZipCode("22.420-020");
                Assert.True(false);
            }
            catch (ResponseException ex)
            {
                Assert.True(!string.IsNullOrWhiteSpace(ex.Token));
                Assert.IsType<ZipCodeInvalidException>(ex.Exception);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidateZipCodeEmpty()
        {
            Assert.ThrowsAsync<ZipCodeEmptyException>(async () => await _homeRule.ValidadeZipCode(""));
        }

        [Fact]
        public void ValidateZipCodeInvalid()
        {
            Assert.ThrowsAsync<ZipCodeInvalidException>(async () => await _homeRule.ValidadeZipCode("123456789"));
        }

        [Fact]
        public async void ValidateZipCodeSucess()
        {
            try
            {
                var response = await _homeRule.ValidadeZipCode("00.000-000");
                Assert.True(!string.IsNullOrWhiteSpace(response.City));
                Assert.True(!string.IsNullOrWhiteSpace(response.Street));
                Assert.True(!string.IsNullOrWhiteSpace(response.Neighborhood));
                Assert.True(!string.IsNullOrWhiteSpace(response.State.Name));
                Assert.True(!string.IsNullOrWhiteSpace(response.State.Abbreviation));
                Assert.True(!string.IsNullOrWhiteSpace(response.State.Country.Name));
                Assert.True(!string.IsNullOrWhiteSpace(response.State.Country.Abbreviation));
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void ValidateZipCodeBrazilJson()
        {
            var result = await GetMokIntelligentHabitacionHttpClient().GetLocationBrazilByZipCode("00000000");
            Assert.True(!string.IsNullOrEmpty(result.Cep));
            Assert.True(!string.IsNullOrEmpty(result.Gia));
            Assert.True(!string.IsNullOrEmpty(result.Ibge));
            Assert.True(!string.IsNullOrEmpty(result.Unidade));
            Assert.True(!string.IsNullOrEmpty(result.Complemento));
        }

        [Fact]
        public void ValidadeNumberEmpty()
        {
            Assert.Throws<NumberEmptyException>(() => _homeRule.ValidadeNumber(""));
        }

        [Fact]
        public void ValidadeNumberSucess()
        {
            try
            {
                _homeRule.ValidadeNumber("1");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidadeNetWorkInformationEmpty()
        {
            Assert.Throws<NetworkInformationsInvalidException>(() => _homeRule.ValidadeNetWorkInformation("Network", ""));
        }

        [Fact]
        public void ValidadeNetWorkInformationSucess()
        {
            try
            {
                _homeRule.ValidadeNetWorkInformation("network", "password");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidadeNeighborhoodEmpty()
        {
            Assert.Throws<NeighborhoodEmptyException>(() => _homeRule.ValidadeNeighborhood(""));
        }

        [Fact]
        public void ValidadeNeighborhoodSucess()
        {
            try
            {
                _homeRule.ValidadeNeighborhood("neighborhood");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidadeCityEmpty()
        {
            Assert.Throws<CityEmptyException>(() => _homeRule.ValidadeCity(""));
        }

        [Fact]
        public void ValidadeCitySucess()
        {
            try
            {
                _homeRule.ValidadeCity("City");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void ValidadeAdressEmpty()
        {
            Assert.Throws<AddressEmptyException>(() => _homeRule.ValidadeAdress(""));
        }

        [Fact]
        public void ValidadeAdressSucess()
        {
            try
            {
                _homeRule.ValidadeAdress("Adress");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void CreateSucess()
        {
            try
            {
                await _homeRule.Create(new Model.RegisterHomeModel
                {
                    Address = "Address",
                    City = new Model.CityModel
                    {
                        Name = "City",
                        State = new Model.StateModel
                        {
                            Name = "State",
                            Abbreviation = "A",
                            Country = new Model.CountryModel
                            {
                                Name = "Country",
                                Abbreviation = "A"
                            }
                        }
                    },
                    Complement = "",
                    Neighborhood = "Neighborhood",
                    NetWork = new Model.WifiNetworkModel
                    {
                        Name = "Network",
                        Password = "password"
                    },
                    Number = "1",
                    ZipCode = "00.000-000"
                });
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
            mock.Setup(c => c.GetLocationBrazilByZipCode(It.IsAny<string>())).ReturnsAsync(new ResponseLocationBrazilJson
            {
                Bairro = "Neighborhood",
                Cep = "ZipCode",
                Complemento = "1",
                Gia = "1",
                Ibge = "1",
                Localidade = "City",
                Logradouro = "Street",
                Uf = "MG",
                Unidade = "A"
            });
            mock.Setup(c => c.GetLocationBrazilByZipCode("22.420-020")).ThrowsAsync(new ResponseException
            {
                Token = "Token",
                Exception = new ZipCodeInvalidException()
            });
            mock.Setup(c => c.CreateHome(It.IsAny<RequestRegisterHomeJson>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseJson
            {
                Token = "token"
            });

            return mock.Object;
        }
    }
}
