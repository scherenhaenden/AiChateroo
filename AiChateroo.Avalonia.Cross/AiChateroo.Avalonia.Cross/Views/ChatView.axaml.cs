using AiChateroo.Avalonia.Cross.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AiChateroo.Avalonia.Cross.Views;

public partial class ChatView : UserControl
{
    //private readonly ChatViewModel _viewModel;
    
    //public TextBox txtMessage => this.FindControl<TextBox>("etxtMessage");
    
    public ChatView(/*ChatViewModel viewModel*/)
    {
  

        InitializeComponent();

    
    }
    
    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            // Enter key pressed without Shift, handle sending the message
            //(DataContext as ChatViewModel)?.AddMessageCommand.Execute().Subscribe();
            e.Handled = true;
        }
    }
    
    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        //txtMessage.TextChanged(sender, e);
    }
}