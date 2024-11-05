
namespace BrgyLink.Models
{
	public class ChatMessages
	{
		public string User { get; set; }
		public string Content { get; set; }
		public bool IsBot { get; set; } // Indicator to show if the message is from the bot or the user


	}
}
