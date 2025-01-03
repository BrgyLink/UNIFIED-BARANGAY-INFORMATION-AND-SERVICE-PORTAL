﻿let connection;

function startConnection() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.on("ReceiveMessage", (user, message) => {
        const messagesContainer = document.getElementById("messagesContainer");
        const newMessage = document.createElement("div");
        newMessage.classList.add("message", user === "PCV" ? "bg-light" : "bg-primary", "text-dark", "p-2", "rounded", "mb-2", "fade-in");
        newMessage.textContent = `${user}: ${message}`;

        // Add profile image for bot messages
        if (user === "PCV") {
            const profileImg = document.createElement("img");
            profileImg.src = "https://cdn-icons-png.flaticon.com/512/9203/9203764.png"; // New bot image URL
            profileImg.classList.add("profile-img", "rounded-circle", "me-2");
            newMessage.prepend(profileImg); // Add image before the text
        }

        messagesContainer.appendChild(newMessage);
        messagesContainer.scrollTop = messagesContainer.scrollHeight; // Auto-scroll to the bottom
    });

    connection.start()
        .then(() => console.log("SignalR connected."))
        .catch(err => console.error("SignalR connection error: " + err));
}

function sendMessage() {
    const userInput = document.getElementById("userInput");
    const message = userInput.value;

    if (message.trim() !== "" && connection) {
        // Send user's message immediately
        connection.invoke("SendMessage", "You", message)
            .catch(err => console.error("SendMessage error: " + err.toString()));
        userInput.value = ''; // Clear input after sending

        // Delay before showing typing indicator
        setTimeout(() => {
            const messagesContainer = document.getElementById("messagesContainer");
            const typingMessage = document.createElement("div");
            typingMessage.classList.add("message", "text-muted", "p-2", "mb-2", "fade-in");
            typingMessage.textContent = "PCV is typing...";
            messagesContainer.appendChild(typingMessage);
            messagesContainer.scrollTop = messagesContainer.scrollHeight; // Auto-scroll to the bottom

            // Simulate bot's delayed response
            setTimeout(() => {
                // Remove the typing indicator
                messagesContainer.removeChild(typingMessage);
            }, 2000); // Typing delay of 2 seconds
        }, 100); // Delay of 100ms before showing the typing indicator
    } else {
        console.warn("Connection is not initialized or message is empty.");
    }
}

document.addEventListener("DOMContentLoaded", startConnection);

document.getElementById("toggleChat").onclick = function () {
    const chatbox = document.getElementById("chatbox");
    chatbox.style.display = chatbox.style.display === "none" ? "block" : "none";

    // Add opening animation
    if (chatbox.style.display === "block") {
        chatbox.classList.add("fade-in"); // Add fade-in class
        chatbox.classList.remove("fade-out"); // Ensure fade-out class is removed
        greetUser(); // Call the greet function if not already displayed
    } else {
        chatbox.classList.remove("fade-in"); // Remove fade-in effect when hiding
        chatbox.classList.add("fade-out"); // Add fade-out effect
    }
};

// Close chatbox functionality
document.getElementById("closeChat").onclick = function () {
    const chatbox = document.getElementById("chatbox");
    chatbox.style.display = "none"; // Hide chatbox when close button is clicked
    // Do not clear messages, allowing them to remain visible
};