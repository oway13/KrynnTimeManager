using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KrynnTimeManager.lib;
using KrynnTimeManager.UserControls;

namespace KrynnTimeManager
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private KrynnDateTime currentDate = new lib.KrynnDateTime(423, 1, 12);
    private KrynnDateTime calendarDate = new KrynnDateTime(423, 1, 1);
    public MainWindow()
    {
      InitializeComponent();
      UpdateCalendar();
    }

    private void UpdateCalendar()
    {
      currentDateTime.Text = currentDate.ToString();
      calendarMonthYear.Text = calendarDate.ToMonthYearString();
      int count = 1;
      for (int i = 1; i < 5; i++)
      {
        for (int j = 0; j < 7; j++)
        {
          CalendarDay dayToAdd = new CalendarDay();
          //dayToAdd.Day = count++;
          dayToAdd.DayNumber.Text = count.ToString();
          count++;
          CalendarDays.Children.Add(dayToAdd);
          Grid.SetRow(dayToAdd, i);
          Grid.SetColumn(dayToAdd, j);
        }
      }
    }

    private void BackOneMonth_Click(object sender, RoutedEventArgs e)
    {
      calendarDate = calendarDate.SubtractMonths(1);
      UpdateCalendar();
    }

    private void ForwardOneMonth_Click(object sender, RoutedEventArgs e)
    {
      calendarDate = calendarDate.AddMonths(1);
      UpdateCalendar();
    }
  }
}
