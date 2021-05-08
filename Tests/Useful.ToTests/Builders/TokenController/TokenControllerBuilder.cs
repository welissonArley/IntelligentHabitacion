namespace Useful.ToTests.Builders.TokenController
{
    public class TokenControllerBuilder
    {
        private static TokenControllerBuilder _instance;
        private readonly IntelligentHabitacion.Api.Application.Services.Token.TokenController _service;

        private TokenControllerBuilder()
        {
            if (_service == null)
            {
                _service = new IntelligentHabitacion.Api.Application.Services.Token.TokenController(60, "VW5pdFRlc3QxMjNVbml0VGVzdDEyM1VuaXRUZXN0MTIzVW5pdFRlc3QxMjNVbml0VGVzdDEyM1VuaXRUZXN0MTIzVW5pdFRlc3QxMjNVbml0VGVzdDEyM1VuaXRUZXN0MTIzVW5pdFQ=");
            }
        }

        public static TokenControllerBuilder Instance()
        {
            _instance = new TokenControllerBuilder();
            return _instance;
        }

        public IntelligentHabitacion.Api.Application.Services.Token.TokenController Build()
        {
            return _service;
        }
    }
}
