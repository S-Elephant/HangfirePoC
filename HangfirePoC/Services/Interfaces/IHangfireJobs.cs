namespace HangfirePoC.Services.Interfaces
{
	/// <summary>
	/// Hangfire tasks.
	/// </summary>
	public interface IHangfireJobs
	{
		/// <summary>
		/// Send a welcome e-mail by enqueueing it in Hangfire.
		/// </summary>
		void SendWelcomeEmail(string welcomeText);

		/// <summary>
		/// Send a welcome e-mail by scheduling it in Hangfire.
		/// </summary>
		void SendWelcomeEmailDelayed(string welcomeText, int delayInMs);

		/// <summary>
		/// Send a welcome e-mail through Hangfire. The sending will for testing purposes take <paramref name="computationTimeInMs"/> milliseconds.
		/// </summary>
		/// <param name="welcomeText">E-mail welcome text.</param>
		/// <param name="computationTimeInMs">Simulated processing time in ms.</param>
		Task SendWelcomeEmailExpensive(string welcomeText, int computationTimeInMs);

		/// <summary>
		/// Send a follow-up welcome e-mail through Hangfire.
		/// </summary>
		void SendFollowUpWelcomeEmail(string welcomeText);
	}
}
