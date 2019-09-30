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

namespace KrynnTimeManager.UserControls
{
  /// <summary>
  /// Interaction logic for CalendarDay.xaml
  /// </summary>
  public partial class CalendarDay : UserControl
  {
    public CalendarDay()
    {
      InitializeComponent();
      DayNumber.Text = Day.ToString();
    }

    public static readonly DependencyProperty dayProperty = 
      DependencyProperty.Register(
        "Day", typeof(int), typeof(CalendarDay), 
        new FrameworkPropertyMetadata(
          1,
          (FrameworkPropertyMetadataOptions.AffectsRender)
          
          )
        );

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

    public int Day
    {
      get { return (int)GetValue(dayProperty); }
      set { SetValue(dayProperty, value); }
    }
  }
}
