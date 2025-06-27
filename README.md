Project details:
ST10453225_Programming_PART03
Netframe: 4.0.8
Template:WPF Application

This project is the final part of a multi-stage cybersecurity awareness chatbot built using C# and WPF in Visual Studio 2022.
In this part, the chatbot is fully GUI-based and includes:

 Cybersecurity Task Assistant

 Cybersecurity Quiz Game

Reminder and Task Management

 NLP Simulation for Flexible User Commands

 Activity Logging for User Actions

The chatbot helps users manage cybersecurity-related tasks, test their knowledge through an interactive quiz, and set reminders for cybersecurity best practices.

Features:

Task Assistant:
Add, view, complete, and delete cybersecurity-related tasks like enabling two-factor authentication or reviewing privacy settings.

Reminders:
Set reminders for cybersecurity tasks with timeframes like "in 3 days" or "tomorrow."

Cybersecurity Quiz Game:
Test your cybersecurity knowledge with a 10-question quiz covering topics like phishing, password safety, and safe browsing.

NLP Simulation:
The chatbot understands natural variations of user input using simple keyword detection.

Activity Log:
Tracks user actions like adding tasks, setting reminders, starting quizzes, and provides a summary on request.

Setup Instructions
Prerequisites
Visual Studio 2022

.NET Framework (WPF project template)

Steps to Run
Open Visual Studio 2022.

Create a new project → Select WPF App (.NET Framework).

Name the project: POEPART_03.

Replace the MainWindow.xaml with the provided GUI code.

Replace the MainWindow.xaml.cs with the provided chatbot logic code.

Build the project and run.

 Usage Instructions
 Supported Commands:
Command	Example
Add Task	Add task: Enable two-factor authentication
Set Reminder	Remind me to update password tomorrow
View Tasks	Click Show Tasks button or type Show tasks
View Log	Click Show Log button or type Show activity log
Start Quiz	Click Start Quiz button or type Start quiz

 Task Example:
text
Copy
Edit
User: Add task: Review privacy settings
Chatbot: Task added: Review privacy settings. Would you like to set a reminder?
User: Yes, remind me in 3 days.
Chatbot: Got it! I’ll remind you in 3 days.
 Quiz Example:
text
Copy
Edit
User: Start quiz
Chatbot: Question 1: What should you do if you receive a suspicious email?
A) Reply B) Delete C) Report D) Ignore
User: C
Chatbot: Correct! Reporting phishing helps prevent scams.
 Activity Log Example:
text
Copy
Edit
User: Show activity log
Chatbot:
1. Task added: Enable two-factor authentication (Reminder set for 5 days)
2. Quiz started – 5 questions answered
3. Reminder set: Review privacy settings on [date]
 Notes
The chatbot automatically scans for reminders in the background and displays them when due.

Supports flexible NLP input such as:

Add reminder to check password in 2 days

Set a task to update security settings

Activity log shows the most recent 5-10 actions.
