using HangfirePoC.Services.Interfaces;

namespace HangfirePoC.Services
{
	/// <inheritdoc/>
	public class HangfireJobs : IHangfireJobs
	{
		/// <inheritdoc/>
		public void SendWelcomeEmail(string welcomeText)
		{
			// Log to console instead of sending an e-mail for PoC purposes.
			Console.WriteLine(welcomeText);
		}

		/// <inheritdoc/>
		public void SendWelcomeEmailDelayed(string welcomeText, int delayInMs)
		{
			// Log to console instead of sending an e-mail for PoC purposes.
			Console.WriteLine(welcomeText);
		}

		/// <inheritdoc/>
		public async Task SendWelcomeEmailExpensive(string welcomeText, int computationTimeInMs)
		{
			await Task.Delay(computationTimeInMs);
		}

		/// <inheritdoc/>
		public void SendFollowUpWelcomeEmail(string welcomeText)
		{
			Console.WriteLine(welcomeText);
		}
	}
}
