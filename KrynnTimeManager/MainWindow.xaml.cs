using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;

namespace KrynnTimeManager
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private KrynnDateTime currentDate = new KrynnDateTime(421, 10, 15);
    private KrynnDateTime calendarDate;

    TextBlock firstDay = new TextBlock() { Text = "Giledai" };
    TextBlock secondDay = new TextBlock() { Text = "Luindai" };
    TextBlock thirdDay = new TextBlock() { Text = "Nuindai" };
    TextBlock fourthDay = new TextBlock() { Text = "Soldai" };
    TextBlock fifthDay = new TextBlock() { Text = "Manthus" };
    TextBlock sixthDay = new TextBlock() { Text = "Shinarai" };
    TextBlock seventhDay = new TextBlock() { Text = "Boreadai" };

    public MainWindow()
    {
      InitializeComponent();
      calendarDate = new KrynnDateTime(currentDate.Year, currentDate.Month, 1);
      UpdateCalendar();
    }

    //Calendar Updating Functions
    private void AddDayNames()
    {
      CalendarDays.Children.Add(firstDay);
      CalendarDays.Children.Add(secondDay);
      CalendarDays.Children.Add(thirdDay);
      CalendarDays.Children.Add(fourthDay);
      CalendarDays.Children.Add(fifthDay);
      CalendarDays.Children.Add(sixthDay);
      CalendarDays.Children.Add(seventhDay);
      Grid.SetRow(firstDay, 0);
      Grid.SetColumn(firstDay, 0);

      Grid.SetRow(secondDay, 0);
      Grid.SetColumn(secondDay, 1);

      Grid.SetRow(thirdDay, 0);
      Grid.SetColumn(thirdDay, 2);

      Grid.SetRow(fourthDay, 0);
      Grid.SetColumn(fourthDay, 3);

      Grid.SetRow(fifthDay, 0);
      Grid.SetColumn(fifthDay, 4);

      Grid.SetRow(sixthDay, 0);
      Grid.SetColumn(sixthDay, 5);

      Grid.SetRow(seventhDay, 0);
      Grid.SetColumn(seventhDay, 6);
    }
    private void AddCalendarDays()
    {
      int count = 1;
      for (int i = 1; i < 5; i++)
      {
        for (int j = 0; j < 7; j++)
        {
          CalendarDay dayToAdd = new CalendarDay(new KrynnDateTime(calendarDate.Year, calendarDate.Month, count));
          count++;
          CalendarDays.Children.Add(dayToAdd);
          Grid.SetRow(dayToAdd, i);
          Grid.SetColumn(dayToAdd, j);
        }
      }
      if (calendarDate.Month == currentDate.Month && calendarDate.Year == currentDate.Year)
      {
        string dateName = currentDate.ToCDName();
        nextEventText.Text = dateName;
        CalendarDay currentDay = UIHelper.FindChild<CalendarDay>(CalendarDays, dateName);
        try
        {
          currentDay.CurrentDayBorder.BorderBrush = Brushes.Orange;
          currentDay.CurrentDayBorder.BorderThickness = new Thickness(2);
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
    }
    private void UpdateCalendar()
    {
      CalendarDays.Children.Clear();
      calendarMonthYear.Text = calendarDate.ToMonthYearString();
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      if (calendarDate.Year == 421 && calendarDate.Month == 10)
      {
        backOneMonth.IsEnabled = false;
      }
      else
      {
        backOneMonth.IsEnabled = true;
      }

      AddDayNames();
      AddCalendarDays();
    }

    //Calendar Changing Buttons
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
    private void GoToCurrentDateTime_Click(object sender, RoutedEventArgs e)
    {
      calendarDate = currentDate;
      UpdateCalendar();
    }

    //Time Changing Buttons
    private void AddSixSeconds_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(6);
    }
    private void AddOneMinute_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(60);
    }
    private void AddTenMinute_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(600);
    }
    private void AddThirtyMinutes_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(1800);
    }
    private void AddOneHour_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(3600);
    }
    private void AddEightHours_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(8*3600);
    }
    private void AddOneDay_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(24*3600);
    }
    private void AddOneWeek_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.AddDays(7);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void AddOneMonth_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.AddMonths(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void AddOneYear_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.AddYears(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubEightHours_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractHours(8);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubOneHour_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractHours(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubThirtyMinutes_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMinutes(30);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubTenMinute_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMinutes(10);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubOneMinute_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMinutes(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubSixSeconds_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractSeconds(6);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubOneYear_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractYears(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubOneMonth_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMonths(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubOneWeek_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractDays(7);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void SubOneDay_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractDays(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }
    private void AddUpToOneDay(int seconds)
    {
      currentDate = currentDate.AddSeconds(seconds);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (CalendarTab.IsSelected)
      {
        UpdateCalendar();
      }
    }

    //File Buttons
    private void OpenFile_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Campaign File (*.campaign)|*.campaign";
      if (openFileDialog.ShowDialog() == true)
      {
        try
        {
          using (Stream input = File.OpenRead(openFileDialog.FileName))
          {
            BinaryFormatter bf = new BinaryFormatter();
            currentDate = (KrynnDateTime)bf.Deserialize(input);
            calendarDate = currentDate;
            //TODO: open events
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("Unable to open the campaign file\r\n" + ex.Message);
        }
      }
      UpdateCalendar();
    }

    private void SaveFile_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "Campaign File (*.campaign)|*.campaign";
      if (saveFileDialog.ShowDialog() == true)
      {
        try
        {
          using (Stream output = File.Create(saveFileDialog.FileName))
          {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(output, currentDate);
            //TODO: save events
          }
        } catch (Exception ex)
        {
          MessageBox.Show("Unable to save the campaign file\r\n" + ex.Message);
        }
      }
    }

    private void NewFile_Click(object sender, RoutedEventArgs e)
    {
      //TODO: New file dialog to set start date and maybe a campaign name?
      currentDate = new KrynnDateTime(421, 10, 15);
      calendarDate = currentDate;
      //TODO: Clear Events
      UpdateCalendar();
    }

    
  }
}
