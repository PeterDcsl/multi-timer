using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MultiTimer
{
	public partial class CredentialsModal : Window
	{
        public string Username { get; private set; } = String.Empty;
        public static string Password { get; private set; } = String.Empty;

        public CredentialsModal()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            Username = usernameTextBox.Text;
            Password = passwordTextBox.Text;
            this.Close();
        }
    }
}
