using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrgyLink.Hubs
{
	public class ChatHub : Hub
	{
		private static ConcurrentDictionary<string, UserContext> userContexts = new ConcurrentDictionary<string, UserContext>();

		public override async Task OnConnectedAsync()
		{
			string connectionId = Context.ConnectionId;
			if (userContexts.ContainsKey(connectionId))
			{
				userContexts[connectionId].ClearContext();
			}
			else
			{
				userContexts[connectionId] = new UserContext();
			}
			await Clients.Caller.SendAsync("ReceiveMessage", "PCV", "Hi, my name is PCV, your chatbot assistant! What language would you like me to use, Bisaya or English?");

			await base.OnConnectedAsync();
		}

		public async Task SendMessage(string user, string message)
		{
			if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
			{
				return;
			}

			await Clients.All.SendAsync("ReceiveMessage", user, message);
			await Task.Delay(2000);

			var botResponse = GetBotResponse(Context.ConnectionId, message);
			await Clients.All.SendAsync("ReceiveMessage", "PCV", botResponse);
		}

		private string GetBotResponse(string connectionId, string userInput)
		{
			userInput = userInput?.ToLowerInvariant() ?? string.Empty;

			if (!userContexts.ContainsKey(connectionId))
			{
				userContexts[connectionId] = new UserContext();
			}

			var userContext = userContexts[connectionId];
			userContext.History.Add(userInput);

			return GenerateDynamicResponse(userInput, userContext);
		}

		private string GenerateDynamicResponse(string userInput, UserContext userContext)
		{
			string response;

			// Detect language preference and set preferred language
			if (userInput.Contains("bisaya") || userInput.Contains("cebuano") || userInput.Contains("musta") || userInput.Contains("unsa"))
			{
				userContext.PreferredLanguage = "Cebuano";
				response = "Kamusta, unsa akong matabang nimo karon?";
				userContext.LastQuestion = "Unsa akong matabang nimo?";
			}
			else if (userInput.Contains("hello") || userInput.Contains("hi") || userInput.Contains("good morning"))
			{
				userContext.PreferredLanguage = "English";
				response = "Hi there! How can I assist you?";
				userContext.LastQuestion = "How can I assist you?";
			}
			else if (userInput.Contains("time"))
			{
				response = GetCurrentTime(userContext.PreferredLanguage);
			}
			else if (userInput.Contains("date"))
			{
				response = GetCurrentDate(userContext.PreferredLanguage);
			}
			else if (userInput.Contains("joke"))
			{
				response = GetRandomJoke(userContext.PreferredLanguage);
			}
			else if (userInput.Contains("riddle"))
			{
				response = GetRandomRiddle(userContext.PreferredLanguage);
			}
			else
			{
				response = HandleUserResponse(userInput, userContext);
			}

			return response;
		}

		private string GetCurrentTime(string preferredLanguage)
		{
			var currentTime = DateTime.Now.ToString("hh:mm tt");
			return preferredLanguage == "Cebuano" ?
				$"Ang oras karon kay {currentTime}." :
				$"The current time is {currentTime}.";
		}

		private string GetCurrentDate(string preferredLanguage)
		{
			var currentDate = DateTime.Now.ToString("MMMM dd, yyyy");
			return preferredLanguage == "Cebuano" ?
				$"Ang petsa karon kay {currentDate}." :
				$"Today's date is {currentDate}.";
		}

		private string GetRandomJoke(string preferredLanguage)
		{
			var jokes = new List<string>
			{
				"Why don't scientists trust atoms? Because they make up everything!",
				"What do you call fake spaghetti? An impasta!",
				"Why did the scarecrow win an award? Because he was outstanding in his field!",
				"Knock, knock. Who’s there? Lettuce. Lettuce who? Lettuce in, it’s freezing out here!",
				"What do you get when you cross a snowman and a vampire? Frostbite!"
			};

			var random = new Random();
			var joke = jokes[random.Next(jokes.Count)];
			return preferredLanguage == "Cebuano" ?
				"Ania ang usa ka joke: " + "Ngano man nga dili mosalig ang mga siyentista sa mga atom? Tungod kay sila ang nagbuhat sa tanan!" : // Translate the joke to Cebuano
				joke;
		}

		private string GetRandomRiddle(string preferredLanguage)
		{
			var riddles = new List<string>
			{
				"What has keys but can't open locks? (Answer: A piano)",
				"I speak without a mouth and hear without ears. I have no body, but I come alive with the wind. What am I? (Answer: An echo)",
				"The more you take, the more you leave behind. What am I? (Answer: Footsteps)",
				"What can travel around the world while staying in a corner? (Answer: A stamp)",
				"What has many teeth, but cannot bite? (Answer: A comb)"
			};

			var random = new Random();
			var riddle = riddles[random.Next(riddles.Count)];
			return preferredLanguage == "Cebuano" ?
				"Ania ang usa ka riddle: " + "Unsay naay daghang ngipon, pero dili maka kagat? (Tubag: Usa ka suklay)" : // Translate the riddle to Cebuano
				riddle;
		}

		private string HandleUserResponse(string userInput, UserContext userContext)
		{
			string response;

			if (userContext.LastQuestion != null)
			{
				response = HandleAnswerToLastQuestion(userInput, userContext);
				userContext.LastQuestion = null; // Clear the last question after handling
			}
			else
			{
				response = DetermineIntent(userInput, userContext);
			}

			return response;
		}

		private string HandleAnswerToLastQuestion(string userInput, UserContext userContext)
		{
			string response;
			bool isCebuano = userContext.PreferredLanguage == "Cebuano";
			bool isEnglish = userContext.PreferredLanguage == "English";

			if (userContext.LastQuestion == (isCebuano ? "Unsa akong matabang nimo?" : "How can I assist you?"))
			{
				response = userInput.Contains("help") ?
					(isCebuano ? "Sige, unsa'y matabang nako?" : "Of course, I'm here to help! Can you specify what you need assistance with?") :
					(isCebuano ? "Sige lang! Sugba lang kung naa kay pangutana." : "Alright! Let me know if you have any specific questions.");
			}
			else if (userContext.LastQuestion == (isCebuano ? "Gusto ba nimo makahibalo pa og dugang bahin sa among serbisyo?" : "Would you like to know more about our services?"))
			{
				if (userInput.ToLower() == "yes")
				{
					response = isCebuano ?
						"Maayo! Daghan mi og serbisyo para sa community. Asa nga serbisyo imong gusto mahibal-an?" :
						"Great! We offer various community services. Which specific service would you like to know about?";
				}
				else if (userInput.ToLower() == "no" || userInput.ToLower() == "ok")
				{
					response = isCebuano ?
						"Sige, ingna lang ko kung naa kay pangutana pa." :
						"Alright, let me know if you have other questions!";
				}
				else
				{
					response = GenerateDynamicFallbackResponse(userInput, userContext);
				}
			}
			else
			{
				response = GenerateDynamicFallbackResponse(userInput, userContext);
			}

			return response;
		}

		private string DetermineIntent(string userInput, UserContext userContext)
		{
			bool isCebuano = userContext.PreferredLanguage == "Cebuano";

			if (userInput.Contains("help") || userInput.Contains("assist"))
			{
				userContext.LastQuestion = isCebuano ? "Unsa akong matabang nimo?" : "How can I assist you?";
				return isCebuano ?
					"Sige, unsa'y matabang nako?" :
					"Of course, I'm here to help. What do you need assistance with?";
			}
			else if (userInput.Contains("services") || userInput.Contains("offer"))
			{
				userContext.LastQuestion = isCebuano ? "Gusto ba nimo makahibalo pa og dugang bahin sa among serbisyo?" : "Would you like to know more about our services?";
				return isCebuano ?
					"Nag-offer mi og daghang serbisyo para sa community." :
					"We offer a variety of services for the community.";
			}
			else if (userInput.Contains("thank you") || userInput.Contains("salamat"))
			{
				return isCebuano ?
					"Walay sapayan! Kontaka lang ko kung naa pa kay pangutana." :
					"You're welcome! Feel free to reach out if you have more questions.";
			}
			else
			{
				return GenerateDynamicFallbackResponse(userInput, userContext);
			}
		}

		private string GenerateDynamicFallbackResponse(string userInput, UserContext userContext)
		{
			bool isCebuano = userContext.PreferredLanguage == "Cebuano";
			string response;

			// Check for keywords in user input to tailor fallback responses
			if (userInput.Contains("not sure") || userInput.Contains("di ko sigurado"))
			{
				response = isCebuano ?
					"Walay problema! Ang uban pang mga detalye makahatag og kasayuran sa imong pangutana." :
					"No problem! Additional details can help clarify your question.";
			}
			else if (userInput.Contains("confused") || userInput.Contains("nalibog"))
			{
				response = isCebuano ?
					"Pasensya na, kasabot ko nga nalibog ka. Unsay imong gustong mahibal-an?" :
					"I'm sorry, I understand that you're confused. What would you like to know?";
			}
			else if (userInput.Contains("no") || userInput.Contains("dili") || userInput.Contains("diko") || userInput.Contains("ok") || userInput.Contains("yes"))
			{
				response = isCebuano ?
					"Sige, ingna lang ko kung naa kay pangutana pa." :
					"Alright, let me know if you have other questions!";
			}
			else if (userInput.Contains("repeat") || userInput.Contains("balik"))
			{
				response = isCebuano ?
					"Sige, akong i-rephrase ang akong giingon. Pasensya na kung nalibog ka." :
					"Sure, I'll rephrase what I said. I'm sorry if it was confusing.";
			}
			else
			{
				// Default fallback response
				userContext.LastQuestion = isCebuano ? "Pwede ba nimo i-rephrase ang imong pangutana?" : "Could you rephrase your question?";
				response = isCebuano ?
					"Pasensya na, wala pa nako nahibaw-i na. Pwede ba nimo i-rephrase ang imong pangutana?" :
					"I'm sorry, I don't understand that yet. Could you rephrase your question?";
			}

			return response;
		}
	}

	public class UserContext
	{
		public List<string> History { get; set; } = new List<string>();
		public string PreferredLanguage { get; set; } = "English";
		public string LastQuestion { get; set; }

		public void ClearContext()
		{
			History.Clear();
			LastQuestion = null;
			PreferredLanguage = "English";
		}
	}
}
