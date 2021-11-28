using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;
using System.Linq;

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
			UpdateTimer.Interval = 1000;
			UpdateTimer.Start();
		}

		private void AddTimer()
		{
			var timerControl = new TimerControl();
			TimerControlHolder.TimerControls.Add(timerControl);
			timerControl.CloseRequested += TimerControl_CloseRequested;
		}

		private void UpdateTimeUI()
		{
			var timerValue = TimeSpan.FromTicks(TimerControlHolder.TimerControls.Sum(x => x.TimerValue.Ticks));

			var hours = timerValue.Hours.ToString("D2");
			var minutes = timerValue.Minutes.ToString("D2");
			TotalTimerLabel.Content = $"{hours}:{minutes}";
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			AddTimer();
		}

		private void TickTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.BeginInvoke(new Action(() => UpdateTimeUI()));
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
