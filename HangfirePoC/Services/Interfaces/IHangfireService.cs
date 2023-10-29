using HangfirePoC.Viewmodels;

namespace HangfirePoC.Services.Interfaces
{
	/// <summary>
	/// Hangfire.
	/// </summary>
	public interface IHangfireService
	{
		/// <summary>
		/// Register a new user by using Hangfire.
		/// </summary>
		string RegisterUser(RegisterUserRequestModel registerUserRequestModel);

		/// <summary>
		/// Register a new user by using Hangfire and adding a delay.
		/// </summary>
		string RegisterUserDelayed(RegisterUserDelayedRequestModel registerUserDelayedRequestModel);

		/// <summary>
		/// Register a new user by using Hangfire, simulating a long-running job.
		/// </summary>
		string RegisterUserExpensive(RegisterUserRequestModel registerUserRequestModel, int computationTimeInMs);

		/// <summary>
		/// Register the user, simulating a time-expensive job and after that, starting another one.
		/// </summary>
		string RegisterUserChain(RegisterUserRequestModel registerUserRequestModel, int computationTimeInMs);
	}
}
