using System;
using System.Reactive.Linq;
using AiChateroo.Avalonia.Cross.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AiChateroo.Avalonia.Cross.Views;


public class MessageObserver : IObserver<string>
{
    public void OnNext(string value)
    {
        // Handle new message
        Console.WriteLine($"Received message: {value}");
    }

    public void OnError(Exception error)
    {
        // Handle error
        Console.WriteLine($"Error occurred: {error.Message}");
    }

    public void OnCompleted()
    {
        // Handle completion
        Console.WriteLine("No more messages.");
    }
}



public partial class ChatView : UserControl
{
    //private readonly ChatViewModel _viewModel;
    
    //public TextBox txtMessage => this.FindControl<TextBox>("etxtMessage");
    
    public ChatView(/*ChatViewModel viewModel*/)
    {
  

        InitializeComponent();

    
    }
    
    // Somewhere in your code where you have an IObservable<string>:
    

    
    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            
            // Enter key pressed without Shift, handle sending the message
            (DataContext as ChatViewModel)?.AddMessageCommand.Execute().Subscribe();
            e.Handled = true;
        }
    }
    
    private void ScrollToEnd(object sender, SelectionChangedEventArgs e)
    {
        // Find the ListBox and scroll to the end after new message is added
        if (sender is ListBox listBox && listBox.ItemCount > 0)
        {
            listBox.ScrollIntoView(listBox.ItemCount - 1);
        }
    }
    
 

    
    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        //txtMessage.TextChanged(sender, e);
    }

    private void ScrollToEnds(object? sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}