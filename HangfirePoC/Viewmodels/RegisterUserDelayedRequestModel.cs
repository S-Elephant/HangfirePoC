using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangfirePoC.Viewmodels
{
	/// <summary>
	/// Register user request model.
	/// </summary>
	public class RegisterUserDelayedRequestModel
	{
		/// <summary>
		/// Username for user registration.
		/// </summary>
		[Required(AllowEmptyStrings = false)]
		[DefaultValue("Mr. Pikachu")]
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// How long to delay the task.
		/// </summary>
		[Required]
		[Range(1, int.MaxValue)]
		[DefaultValue(5000)]
		public int DelayInMs { get; set; } = 5000;
	}
}
