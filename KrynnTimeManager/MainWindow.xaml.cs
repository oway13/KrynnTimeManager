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
        private lib.KrynnDateTime testDate = new lib.KrynnDateTime(423, 1, 12);
        public MainWindow()
        {
            InitializeComponent();
            dateTimeTest.Text = testDate.ToString();

            for(int i = 1; i < 5; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    CalendarDay dayToAdd = new CalendarDay();
                    CalendarDays.Children.Add(dayToAdd);
                    Grid.SetRow(dayToAdd, i);
                    Grid.SetColumn(dayToAdd, j);
                }
            }
        }
    }
}
