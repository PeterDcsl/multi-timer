﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

namespace MultiTimer
{
	public partial class TimerControl : UserControl
	{
		public event EventHandler CloseRequested;

		private DateTime? LastStartTime { get; set; }
		private TimeSpan TotalTimeBeforeLastStart { get; set; } = new TimeSpan();
		private bool IsRunning
		{
			get { return UpdateTimer.Enabled; }
			set { UpdateTimer.Enabled = value; }
		}

		public TimeSpan TimerValue
		{
			get
			{
				if (LastStartTime != null && IsRunning)
					return TotalTimeBeforeLastStart + (DateTime.Now - LastStartTime.Value);
				else
					return TotalTimeBeforeLastStart;
			}
		}

		private Timer UpdateTimer { get; } = new Timer();


		public TimerControl()
		{
			InitializeComponent();

			UpdateTimer.Elapsed += TickTimer_Elapsed;
			UpdateTimer.Interval = 1000;
		}

		/// <summary>
		/// Should be called ideally every second. Updates the UI to match the time.
		/// </summary>
		public void UpdateTimeUI()
		{
			if (IsRunning)
			{
				var timerValue = TimerValue;
				HoursTextBox.Text = timerValue.Hours.ToString("D2");
				MinutesTextBox.Text = timerValue.Minutes.ToString("D2");

				TimeSeparator.Visibility = timerValue.Seconds % 2 == 0 ? Visibility.Visible : Visibility.Hidden;
			}
		}
		private void SetTimeFromUI()
		{
			if (IsLoaded)
			{
				// assuming the input validation is solid
				var hours = int.Parse(string.IsNullOrEmpty(HoursTextBox.Text) ? "0" : HoursTextBox.Text);
				var minutes = int.Parse(string.IsNullOrEmpty(MinutesTextBox.Text) ? "0" : MinutesTextBox.Text);
				TotalTimeBeforeLastStart = new TimeSpan(hours, minutes, 0);
			}
		}

		#region Events

		private void TickTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.BeginInvoke(UpdateTimeUI);
		}

		private void HourTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var validText = int.TryParse(HoursTextBox.Text + e.Text, out int hours) && 0 <= hours;

			e.Handled = !validText;
		}

		private void MinuteTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var validText = int.TryParse(MinutesTextBox.Text + e.Text, out int minutes) &&
				0 <= minutes && minutes <= 59;

			e.Handled = !validText;
		}

		private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// Prohibit space
			if (e.Key == Key.Space)
				e.Handled = true;
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!IsRunning)
				SetTimeFromUI();
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			if (!IsRunning)
			{
				IsRunning = true;
				LastStartTime = DateTime.Now;

				HoursTextBox.IsEnabled = false;
				MinutesTextBox.IsEnabled = false;
			}
		}

		private void PauseButton_Click(object sender, RoutedEventArgs e)
		{
			if (IsRunning)
			{
				IsRunning = false;
				TotalTimeBeforeLastStart = TimerValue;

				TimeSeparator.Visibility = Visibility.Visible;

				HoursTextBox.IsEnabled = true;
				MinutesTextBox.IsEnabled = true;
			}
		}

		/// <summary>
		/// No idea why the left click events are getting eaten elsewhere, but this Preview event circumvents it at least
		/// </summary>
		private void CloseLabel_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			CloseRequested?.Invoke(this, new EventArgs());
		}

		#endregion
	}
}
