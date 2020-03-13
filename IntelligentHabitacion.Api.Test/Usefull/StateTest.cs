using IntelligentHabitacion.Useful;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Usefull
{
    public class StateTest
    {
        [Fact]
        public void StateAbbreviationToFullNameState()
        {
            var state = new State();
            Assert.Equal("Acre", state.StateAbbreviationToFullNameState("ac"));
            Assert.Equal("Alagoas", state.StateAbbreviationToFullNameState("al"));
            Assert.Equal("Amapá", state.StateAbbreviationToFullNameState("ap"));
            Assert.Equal("Amazonas", state.StateAbbreviationToFullNameState("am"));
            Assert.Equal("Bahia", state.StateAbbreviationToFullNameState("ba"));
            Assert.Equal("Ceará", state.StateAbbreviationToFullNameState("ce"));
            Assert.Equal("Espírito Santo", state.StateAbbreviationToFullNameState("es"));
            Assert.Equal("Goiás", state.StateAbbreviationToFullNameState("go"));
            Assert.Equal("Maranhão", state.StateAbbreviationToFullNameState("ma"));
            Assert.Equal("Mato Grosso", state.StateAbbreviationToFullNameState("mt"));
            Assert.Equal("Mato Grosso do Sul", state.StateAbbreviationToFullNameState("ms"));
            Assert.Equal("Minas Gerais", state.StateAbbreviationToFullNameState("mg"));
            Assert.Equal("Pará", state.StateAbbreviationToFullNameState("pa"));
            Assert.Equal("Paraíba", state.StateAbbreviationToFullNameState("pb"));
            Assert.Equal("Paraná", state.StateAbbreviationToFullNameState("pr"));
            Assert.Equal("Pernambuco", state.StateAbbreviationToFullNameState("pe"));
            Assert.Equal("Piauí", state.StateAbbreviationToFullNameState("pi"));
            Assert.Equal("Rio de Janeiro", state.StateAbbreviationToFullNameState("rj"));
            Assert.Equal("Rio Grande do Norte", state.StateAbbreviationToFullNameState("rn"));
            Assert.Equal("Rio Grande do Sul", state.StateAbbreviationToFullNameState("rs"));
            Assert.Equal("Rondônia", state.StateAbbreviationToFullNameState("ro"));
            Assert.Equal("Roraima", state.StateAbbreviationToFullNameState("rr"));
            Assert.Equal("Santa Catarina", state.StateAbbreviationToFullNameState("sc"));
            Assert.Equal("São Paulo", state.StateAbbreviationToFullNameState("sp"));
            Assert.Equal("Sergipe", state.StateAbbreviationToFullNameState("se"));
            Assert.Equal("Tocantins", state.StateAbbreviationToFullNameState("to"));
            Assert.Equal("Distrito Federal", state.StateAbbreviationToFullNameState("df"));
            Assert.Equal("", state.StateAbbreviationToFullNameState("N"));
        }
    }
}
