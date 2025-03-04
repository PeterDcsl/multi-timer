﻿using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace MultiTimer
{
	/// <summary>
	/// Container class to hold a collection of TimerControls for the data binding to TimersListBox
	/// </summary>
	class TimerControlHolder
	{
		public ObservableCollection<TimerControl> TimerControls { get; } = new ObservableCollection<TimerControl>();
	}

	public partial class MainWindow : Window
	{
		private TimerControlHolder TimerControlHolder { get; } = new TimerControlHolder();

		private Timer UpdateTimer { get; } = new Timer();

		public MainWindow()
		{
			InitializeComponent();

			DataContext = TimerControlHolder;

			AddTimer();
			AddTimer();
			AddTimer();

			UpdateTimer.Elapsed += TickTimer_Elapsed;
			UpdateTimer.Interval = 500;
			UpdateTimer.Start();
		}

		private void AddTimer()
		{
			IEnumerable<ProjectTask> projectTasks = new List<ProjectTask>
			{
				new ProjectTask
                {
					Id = 0,
					Title = "Hello"
                },
				new ProjectTask
				{
					Id = 1,
					Title = "there"
				},
			};

			var timerControl = new TimerControl(projectTasks);
			TimerControlHolder.TimerControls.Add(timerControl);
			timerControl.CloseRequested += TimerControl_CloseRequested;
		}

		private void UpdateTimeUI()
		{
			var timerValue = TimeSpan.FromTicks(TimerControlHolder.TimerControls.Sum(x => x.TimerValue.Ticks));

			var hours = Math.Floor(timerValue.TotalHours);
			var minutes = timerValue.Minutes.ToString("D2");
			TotalTimerLabel.Content = $"{hours}:{minutes}";
		}

		private void TickTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.BeginInvoke(new Action(() => UpdateTimeUI()));
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			AddTimer();
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			CredentialsModal credentialsModal = new CredentialsModal();
			Nullable<bool> dialogResult = credentialsModal.ShowDialog();

			if (dialogResult.Value)
            {
				var username = credentialsModal.Username;
            }
		}

		private void TimerControl_CloseRequested(object sender, EventArgs e)
		{
			TimerControlHolder.TimerControls.Remove(sender as TimerControl);
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			this.MinHeight = this.ActualHeight;
			this.MaxHeight = this.ActualHeight;
		}
	}
}
