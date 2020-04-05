using IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.SetOfRules.EmailHelper
{
	public class EmailHelper : IEmailHelper
	{
		private readonly string _header;

		public EmailHelper()
		{
			_header = @"<div style=""background-color: #FEBF3B; height: 63px;"">
			<ul style=""list-style-type: none;float: left;margin-top: 0px;margin-bottom: 0px;padding-left: 0px;"">
				<li>
					<div style=""height: 100%; width: 100%; margin: 0 auto; display: flex; justify-content: center; align-items: center; overflow: auto;"">
						<img alt=""Icone"" src=""https://66.media.tumblr.com/201a6b70ca50f294561e749843b0ab4b/9baf459185561f04-bf/s1280x1920/273808597d4185e2f208648e3c632b1529eb4867.png"" width=""60px"" height=""60px"">
					</div>
				</li>
			</ul>
			
			<div style=""display: inline-grid;"">
				<ul style='list-style-type: none;'>
					<li>
						<div style=""height: 100%; width: 100%; margin: 0 auto; display: flex; justify-content: center; align-items: center; overflow: auto;"">
							<span style=""font-family: 'Raleway';font-size: 16px;"">Porque morar com os amigos</span>
						</div>
					</li>
					<li>
						<div style=""height: 100%; width: 100%; margin: 0 auto; display: flex; justify-content: center; align-items: center; overflow: auto;"">
							<span style=""font-family: 'Raleway';font-size: 12px;font-weight: 300;text-align: center;"">deve ser divertido e organizado</span>
						</div>
					</li>
				</ul>
			</div>
		</div>";
		}

		private void SendEmail(string subjectText, string plainText, string htmlText, string email)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("intelligenthabitacion@gmail.com", "IntelligentHabitacion Admin");
            var subject = subjectText;
            var to = new EmailAddress(email, subjectText);
            var plainTextContent = plainText;
            var htmlContent = htmlText;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

			Task.Run(async () => await client.SendEmailAsync(msg));

		}

        public void ResetPassword(string email, string code, string userName)
        {
            var plainText = $"Olá {userName}, Precisa resetar sua senha para acessar sua conta, certo? Use o código abaixo para prosseguir com a ação:\n\n\n";
            plainText = $"{plainText}{code}\n\n\n";
            plainText = $"{plainText}Mas lembre-se, não deixe pra depois pois este código será valido por apenas 1 hora combinado?\n\n\n";
            plainText = $"{plainText}Obrigado,\nIntelligent Habitacion Admin Team.";

			var htmlText = $@"{_header}<div style=""margin-top: 50px;"">
			<span style=""font-family: 'Raleway';font-size: 14px;"">Olá {userName},</span>
			<span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Precisa resgatar sua senha para acessar sua conta certo? Use o código abaixo para prosseguir com a ação:</span>
			
			<div style=""margin-top: 50px;"">
				<span style=""color: #FEBF3B;font-family: 'Raleway';font-size: 30px;font-weight: 800;"">{code}</span>
			</div>
			
			<div style=""margin-top: 50px;"">
				<span style=""font-family: 'Raleway';font-size: 14px;"">Mas lembre-se, não deixe pra depois, pois este código será valido por apenas 1 hora, combinado?</span>
			</div>
		</div>";

			htmlText = $@"{htmlText}<div style=""margin-top: 100px;"">
			<span style=""font-family: 'Raleway';font-size: 14px;"">Obrigado,</span>
			<span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Intelligent Habitacion Admin Team.</span>
		</div>";

            SendEmail("Recuperar Senha", plainText, htmlText, email);
        }
    }
}
