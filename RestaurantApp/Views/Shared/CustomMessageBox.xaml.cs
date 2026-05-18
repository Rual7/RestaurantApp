using System.Windows;

namespace RestaurantApp.Views.Shared;

public partial class CustomMessageBox
    : Window
{
    #region Constructor

    public CustomMessageBox(
        string title,
        string message)
    {
        InitializeComponent();

        TitleText.Text =
            title;

        MessageText.Text =
            message;
    }

    #endregion

    #region Events

    private void OkButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Close();
    }

    #endregion

    #region Helpers

    public static void Show(
        string title,
        string message)
    {
        CustomMessageBox messageBox =
            new(title, message);

        messageBox.ShowDialog();
    }

    #endregion
}