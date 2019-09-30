﻿using System;
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

namespace KrynnTimeManager.UserControls
{
  /// <summary>
  /// Interaction logic for CalendarDay.xaml
  /// </summary>
  public partial class CalendarDay : UserControl
  {
    private KrynnDateTime Date;

    public CalendarDay(KrynnDateTime krynnDateTime)
    {
      InitializeComponent();
      this.Date = krynnDateTime;
      DayNumber.Text = Date.Day.ToString();
      Date.CalculateMoonPhases();
      if (Date.SolinariApex == MoonPhaseApex.FirstQuarter)
        SolinariRight = 0.5;
    }

    private void Grid_MouseEnter(object sender, MouseEventArgs e)
    {
      outerBorder.BorderBrush = Brushes.Blue;
      outerBorder.BorderThickness = new Thickness(2);
    }

    private void Grid_MouseLeave(object sender, MouseEventArgs e)
    {
      outerBorder.BorderBrush = Brushes.Black;
      outerBorder.BorderThickness = new Thickness(1);
    }

  }
}
