using Hangfire;
using HangfirePoC.Services.Interfaces;
using HangfirePoC.Viewmodels;

namespace HangfirePoC.Services
{
	/// <inheritdoc/>
	public class HangfireService : IHangfireService
	{
		private readonly IHangfireJobs _hangfireJobs;

		/// <summary>
		/// Constructor.
		/// </summary>
		public HangfireService(IHangfireJobs hangfireJobs)
		{
			_hangfireJobs = hangfireJobs;
		}

		/// <inheritdoc/>
		public string RegisterUser(RegisterUserRequestModel registerUserRequestModel)
		{
			// <Create user in persistent storage here>

			// Queue job in Hangfire. Note that these methods, functions, etc. must be public.
			string jobId = BackgroundJob.Enqueue(() => _hangfireJobs.SendWelcomeEmail($"Welcome {registerUserRequestModel.UserName}."));
			Console.WriteLine($"JobId: [{jobId}]");
			return $"[{jobId}] Welcome {registerUserRequestModel.UserName}.";
		}

		/// <inheritdoc/>
		public string RegisterUserDelayed(RegisterUserDelayedRequestModel registerUserDelayedRequestModel)
		{
			// <Create user in persistent storage here>

			// Queue job in Hangfire. Note that these methods, functions, etc. must be public.
			string jobId = BackgroundJob.Schedule(() => _hangfireJobs.SendWelcomeEmail($"Welcome {registerUserDelayedRequestModel.UserName}."), TimeSpan.FromMilliseconds(registerUserDelayedRequestModel.DelayInMs));
			Console.WriteLine($"JobId: [{jobId}]");
			return $"[{jobId}] Welcome {registerUserDelayedRequestModel.UserName}.";
		}

		/// <inheritdoc/>
		public string RegisterUserExpensive(RegisterUserRequestModel registerUserRequestModel, int computationTimeInMs)
		{
			string jobId = BackgroundJob.Enqueue(() => _hangfireJobs.SendWelcomeEmailExpensive($"Welcome {registerUserRequestModel.UserName}.", computationTimeInMs));
			Console.WriteLine($"JobId: [{jobId}]");
			return $"[{jobId}] Welcome {registerUserRequestModel.UserName}.";
		}

		/// <inheritdoc/>
		public string RegisterUserChain(RegisterUserRequestModel registerUserRequestModel, int computationTimeInMs)
		{
			string parentJobId = BackgroundJob.Enqueue(() => _hangfireJobs.SendWelcomeEmailExpensive($"Welcome {registerUserRequestModel.UserName}.", computationTimeInMs));
			Console.WriteLine($"Parent JobId: [{parentJobId}]");
			string childJobId = BackgroundJob.ContinueJobWith(parentJobId, () => _hangfireJobs.SendFollowUpWelcomeEmail($"Follow-up e-mail here for user {registerUserRequestModel.UserName}."));
			Console.WriteLine($"Child JobId: [{childJobId}]");

			return $"[{parentJobId}] Welcome {registerUserRequestModel.UserName}. A follow-up welcome e-mail will be send to an inbox near you.";
		}
	}
}
