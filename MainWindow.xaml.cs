using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace POEPART_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Task> tasks = new List<Task>();
        private List<string> activityLog = new List<string>();
        private DispatcherTimer reminderTimer;
        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
        private int currentQuizIndex = 0;
        private int quizScore = 0;
        private bool quizInProgress = false;
        public MainWindow()
        {
            InitializeComponent();
            InitializeQuizQuestions();

            reminderTimer = new DispatcherTimer();
            reminderTimer.Interval = TimeSpan.FromMinutes(1);
            reminderTimer.Tick += CheckReminders;
            reminderTimer.Start();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputTextBox.Text;
            ProcessUserInput(userInput);
            UserInputTextBox.Clear();
        }

        private void ShowTasksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTasks();
        }

        private void ShowLogButton_Click(object sender, RoutedEventArgs e)
        {
            ShowActivityLog();
        }

        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void ProcessUserInput(string input)
        {
            input = input.ToLower();
            if (input.Contains("add task") || input.Contains("remind me") || input.Contains("set reminder"))
            {
                string taskTitle = input;
                string description = "Cybersecurity related task.";

                if (input.Contains("enable two-factor authentication"))
                {
                    description = "Set up two-factor authentication to secure your accounts.";
                }
                else if (input.Contains("review privacy settings"))
                {
                    description = "Review account privacy settings to ensure your data is protected.";
                }
                else if (input.Contains("update password"))
                {
                    description = "Update your password to maintain account security.";
                }

                Task newTask = new Task
                {
                    Title = taskTitle,
                    Description = description,
                    ReminderTime = DateTime.Now.AddDays(3),
                    IsCompleted = false
                };

                tasks.Add(newTask);
                activityLog.Add($"[{DateTime.Now}] Task added: '{newTask.Title}' with reminder in 3 days.");

                ChatOutputTextBlock.Text += $"Task added: '{newTask.Title}'. Would you like to set a specific reminder?\n";
            }
            else if (input.Contains("start quiz") || input.Contains("quiz"))
            {
                StartQuiz();
            }
            else if (input.Contains("show activity log") || input.Contains("what have you done for me"))
            {
                ShowActivityLog();
            }
            else if (quizInProgress)
            {
                CheckQuizAnswer(input);
            }
            else
            {
                ChatOutputTextBlock.Text += "I didn't understand that. Please try again.\n";
            }
        }

        private void ShowTasks()
        {
            ChatOutputTextBlock.Text += "Current Tasks:\n";
            foreach (var task in tasks)
            {
                ChatOutputTextBlock.Text += $"- {task.Title}: {task.Description}. Reminder: {task.ReminderTime}\n";
            }
        }

        private void ShowActivityLog()
        {
            ChatOutputTextBlock.Text += "Activity Log:\n";
            foreach (var log in activityLog.TakeLast(10))
            {
                ChatOutputTextBlock.Text += $"{log}\n";
            }
        }

        private void InitializeQuizQuestions()
        {
            quizQuestions.Add(new QuizQuestion("What should you do if you receive an email asking for your password?", new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" }, "C"));
            quizQuestions.Add(new QuizQuestion("True or False: Using 'password' as your password is safe.", new List<string> { "A) True", "B) False" }, "B"));
            quizQuestions.Add(new QuizQuestion("Which of the following is a strong password?", new List<string> { "A) 123456", "B) qwerty", "C) P@ssw0rd!2024", "D) password" }, "C"));
            quizQuestions.Add(new QuizQuestion("What is phishing?", new List<string> { "A) Legitimate company email", "B) Attempt to trick you into giving personal information", "C) Safe browser plugin", "D) None of the above" }, "B"));
            quizQuestions.Add(new QuizQuestion("True or False: Two-factor authentication adds extra security.", new List<string> { "A) True", "B) False" }, "A"));
            quizQuestions.Add(new QuizQuestion("Which link is safer to click?", new List<string> { "A) http://example.com", "B) https://example.com" }, "B"));
            quizQuestions.Add(new QuizQuestion("What is a common sign of a scam website?", new List<string> { "A) Spelling errors", "B) Missing contact information", "C) Too-good-to-be-true offers", "D) All of the above" }, "D"));
            quizQuestions.Add(new QuizQuestion("What should you regularly update?", new List<string> { "A) Social media", "B) Phone wallpaper", "C) Software and security patches", "D) None" }, "C"));
            quizQuestions.Add(new QuizQuestion("True or False: You should share your passwords with friends.", new List<string> { "A) True", "B) False" }, "B"));
            quizQuestions.Add(new QuizQuestion("What should you do when using public Wi-Fi?", new List<string> { "A) Use a VPN", "B) Access bank accounts", "C) Shop online without protection", "D) Enter personal information freely" }, "A"));
        }

        private void StartQuiz()
        {
            quizInProgress = true;
            currentQuizIndex = 0;
            quizScore = 0;
            ChatOutputTextBlock.Text += "Starting the cybersecurity quiz!\n";
            AskQuizQuestion();
        }

        private void AskQuizQuestion()
        {
            if (currentQuizIndex < quizQuestions.Count)
            {
                var question = quizQuestions[currentQuizIndex];
                ChatOutputTextBlock.Text += $"{question.Question}\n";
                foreach (var option in question.Options)
                {
                    ChatOutputTextBlock.Text += $"{option}\n";
                }
            }
            else
            {
                quizInProgress = false;
                ChatOutputTextBlock.Text += $"Quiz completed! Your score: {quizScore}/{quizQuestions.Count}\n";
                if (quizScore >= 8)
                {
                    ChatOutputTextBlock.Text += "Great job! You're a cybersecurity pro!\n";
                }
                else
                {
                    ChatOutputTextBlock.Text += "Keep learning to stay safe online!\n";
                }

                activityLog.Add($"[{DateTime.Now}] Quiz completed. Score: {quizScore}/{quizQuestions.Count}");
            }
        }

        private void CheckQuizAnswer(string input)
        {
            var correctAnswer = quizQuestions[currentQuizIndex].Answer.ToLower();
            if (input.ToLower() == correctAnswer.ToLower())
            {
                ChatOutputTextBlock.Text += "Correct!\n";
                quizScore++;
            }
            else
            {
                ChatOutputTextBlock.Text += $"Incorrect. The correct answer was {quizQuestions[currentQuizIndex].Answer}.\n";
            }

            currentQuizIndex++;
            AskQuizQuestion();
        }

        private void CheckReminders(object sender, EventArgs e)
        {
            foreach (var task in tasks)
            {
                if (!task.IsCompleted && DateTime.Now >= task.ReminderTime)
                {
                    MessageBox.Show($"Reminder: {task.Title}", "Task Reminder", MessageBoxButton.OK, MessageBoxImage.Information);
                    activityLog.Add($"[{DateTime.Now}] Reminder triggered for task: {task.Title}");
                    task.IsCompleted = true; // Mark reminder as done
                }
            }
        }
    }

    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderTime { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }

        public QuizQuestion(string question, List<string> options, string answer)
        {
            Question = question;
            Options = options;
            Answer = answer;
        }
    }
}