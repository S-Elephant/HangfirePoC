using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangfirePoC.Viewmodels
{
	/// <summary>
	/// Register user request model.
	/// </summary>
	public class RegisterUserRequestModel
	{
		/// <summary>
		/// Username for user registration.
		/// </summary>
		[DefaultValue("Mr. Pikachu")]
		[Required(AllowEmptyStrings = false)]
		public string UserName { get; set; } = string.Empty;
	}
}
