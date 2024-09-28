using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
using ReactiveUI;

namespace AiChateroo.Avalonia.Cross.ViewModels
{
    public class ChatViewModel : ReactiveObject
    {
        // Observable collection to hold the messages
        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        private string messageText;

        // Property for the current text in the input field
        public string MessageText
        {
            get => messageText;
            set => this.RaiseAndSetIfChanged(ref messageText, value);
        }

        // Command to add a new message
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddMessageCommand { get; }

        public ChatViewModel()
        {
            // Initialize the AddMessageCommand
            AddMessageCommand = ReactiveCommand.Create(AddMessage);
        }

        // Method to handle adding a new message
        private void AddMessage()
        {
            // Add the current message text to the list and clear the input field
            Messages.Add(MessageText);
            MessageText = "";
        }
    }
}