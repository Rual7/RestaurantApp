using System.Windows;

namespace RestaurantApp.Views.Shared;

public partial class CustomMessageBox : Window
{
    public CustomMessageBox(
        string title,
        string message)
    {
        InitializeComponent();

        TitleText.Text = title;
        MessageText.Text = message;
    }

    private void OkButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Close();
    }

    public static void Show(
        string title,
        string message)
    {
        CustomMessageBox messageBox = new(
            title,
            message);

        messageBox.ShowDialog();
    }
}