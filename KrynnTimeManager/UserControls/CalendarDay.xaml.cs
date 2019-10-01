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
      Name = Date.ToCDName();
      EventString.Text = Name;
      DayNumber.Text = Date.Day.ToString();
      DisplayMoons();

    }

    private void DisplayMoons()
    {
      Dictionary<MoonPhase, string> phaseDict = new Dictionary<MoonPhase, string>()
      {
        {MoonPhase.HighSanction, "High"},
        {MoonPhase.LowSanction, "Low"},
        {MoonPhase.Waning, "Wane"},
        {MoonPhase.Waxing, "Wax"}
      };
      Date.CalculateMoonPhases();

      SolPhase.Text = phaseDict[Date.SolinariPhase];
      LunPhase.Text = phaseDict[Date.LunitariPhase];
      NuiPhase.Text = phaseDict[Date.NuitariPhase];

      if (Date.SolinariApex == MoonPhaseApex.FirstQuarter)
      {
        SolinariXLeft.Offset = 0.496;
        SolinariLeft.Offset = 0.5;
        SolinariRight.Offset = 0.996;
        SolinariXRight.Offset = 1;
      }
      else if (Date.SolinariApex == MoonPhaseApex.LastQuarter)
      {
        SolinariXLeft.Offset = 0;
        SolinariLeft.Offset = 0.004;
        SolinariRight.Offset = 0.5;
        SolinariXRight.Offset = 0.504;
      }
      else if (Date.SolinariApex == MoonPhaseApex.FullMoon)
      {
        SolinariXLeft.Offset = 0;
        SolinariLeft.Offset = 0.004;
        SolinariRight.Offset = 0.996;
        SolinariXRight.Offset = 1;
      }
      else
      {
        SolinariXLeft.Offset = 1;
        SolinariLeft.Offset = 1;
        SolinariRight.Offset = 1;
        SolinariXRight.Offset = 1;
      }

      if (Date.LunitariApex == MoonPhaseApex.FirstQuarter)
      {
        LunitariXLeft.Offset = 0.496;
        LunitariLeft.Offset = 0.5;
        LunitariRight.Offset = 0.996;
        LunitariXRight.Offset = 1;
      }
      else if (Date.LunitariApex == MoonPhaseApex.LastQuarter)
      {
        LunitariXLeft.Offset = 0;
        LunitariLeft.Offset = 0.004;
        LunitariRight.Offset = 0.5;
        LunitariXRight.Offset = 0.504;
      }
      else if (Date.LunitariApex == MoonPhaseApex.FullMoon)
      {
        LunitariXLeft.Offset = 0;
        LunitariLeft.Offset = 0.004;
        LunitariRight.Offset = 0.996;
        LunitariXRight.Offset = 1;
      }
      else
      {
        LunitariXLeft.Offset = 1;
        LunitariLeft.Offset = 1;
        LunitariRight.Offset = 1;
        LunitariXRight.Offset = 1;
      }

      if (Date.NuitariApex == MoonPhaseApex.FirstQuarter)
      {
        NuitariXLeft.Offset = 0.496;
        NuitariLeft.Offset = 0.5;
        NuitariRight.Offset = 0.996;
        NuitariXRight.Offset = 1;
      }
      else if (Date.NuitariApex == MoonPhaseApex.LastQuarter)
      {
        NuitariXLeft.Offset = 0;
        NuitariLeft.Offset = 0.004;
        NuitariRight.Offset = 0.5;
        NuitariXRight.Offset = 0.504;
      }
      else if (Date.NuitariApex == MoonPhaseApex.FullMoon)
      {
        NuitariXLeft.Offset = 0;
        NuitariLeft.Offset = 0.004;
        NuitariRight.Offset = 0.996;
        NuitariXRight.Offset = 1;
      }
      else
      {
        NuitariXLeft.Offset = 1;
        NuitariLeft.Offset = 1;
        NuitariRight.Offset = 1;
        NuitariXRight.Offset = 1;
      }
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
