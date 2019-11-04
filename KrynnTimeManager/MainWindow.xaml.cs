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
    private List<KrynnEvent> Events;
    private int HolidayYear;

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
      HolidayYear = currentDate.Year;
      Events = new List<KrynnEvent>();
      AddCurrentYearsHolidays();
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
          dayToAdd.MouseUp += CalendarDays_MouseUp;
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
    private void AddCurrentYearsHolidays()
    {
      List<KrynnEvent> Holidays = new List<KrynnEvent>();

      KrynnDateTime DarkDayDT = new KrynnDateTime(currentDate.Year, 1, 3);
      Holidays.Add(new KrynnEvent(DarkDayDT, currentDate, "Dark Day", "Anniversary of the First Cataclysm, the 13th day of the Yuletide celebration which begins on the 19th of Twelfthmonth.  It is also considered holy to the followers of Takhisis as a celebration of her return from the exile imposed upon her by Huma Dragonbane."));
      KrynnDateTime NightOfTheMantisDT = new KrynnDateTime(currentDate.Year, 1, 18);
      Holidays.Add(new KrynnEvent(NightOfTheMantisDT, currentDate, "Night of the Mantis", "This holy day for followers of Majere is used to renew their focus and for initiations. They will spend the day fasting, chanting, and meditating."));

      KrynnDateTime SnowDayDT = new KrynnDateTime(currentDate.Year, 2, 8);
      Holidays.Add(new KrynnEvent(SnowDayDT, currentDate, "Snow Day", "Exactly five weeks into the year, it is a celebration of the nearing end of winter.  The children typically spend the day playing in the snow."));
      KrynnDateTime HarnkeggerfestStartDT = new KrynnDateTime(currentDate.Year, 2, 9);
      Holidays.Add(new KrynnEvent(HarnkeggerfestStartDT, currentDate, "Start of Harnkeggerfest", "Originally a dwarven celebration, it is now almost universally observed by all taverns and drinkers.  It is a time of testing the first barrel of dwarf spirits and of drinking.  Some see it as a holy celebration for Reorx as well."));
      KrynnDateTime HarnkeggerfestEndDT = new KrynnDateTime(currentDate.Year, 2, 13);
      Holidays.Add(new KrynnEvent(HarnkeggerfestEndDT, currentDate, "End of Harnkeggerfest", "Originally a dwarven celebration, it is now almost universally observed by all taverns and drinkers.  It is a time of testing the first barrel of dwarf spirits and of drinking.  Some see it as a holy celebration for Reorx as well."));
      KrynnDateTime TheOathbreakingDT = new KrynnDateTime(currentDate.Year, 2, 22);
      Holidays.Add(new KrynnEvent(TheOathbreakingDT, currentDate, "The Oathbreaking", "Anniversary of the discovery of the origin of the draconians.  Metallic dragons spend the day mourning for their lost children and viciously hunting down draconians. "));
      KrynnDateTime HonorsDawnDT = new KrynnDateTime(currentDate.Year, 2, 23);
      Holidays.Add(new KrynnEvent(HonorsDawnDT, currentDate, "Honor's Dawn", "Celebrated in Solamnia, this day honors the life of Knight of the Crown Sturm Brightblade by recognizing the day of his death and sacrifice.  In the years following the War of the Lance, Solamnics began to make pilgrimages to Sturm’s tomb in the High Clerist Tower."));

      KrynnDateTime SiegedayDT = new KrynnDateTime(currentDate.Year, 3, 3);
      Holidays.Add(new KrynnEvent(SiegedayDT, currentDate, "Siegeday", "Celebrated in Solamnia (and by most wizards), this day is the anniversary of the Blue Lady Kitiara’s siege of Palanthas and the sacrifice of Archmage Raistlin Majere."));
      KrynnDateTime KiteDayDT = new KrynnDateTime(currentDate.Year, 3, 7);
      Holidays.Add(new KrynnEvent(KiteDayDT, currentDate, "Kite Day", "Last day of relaxation and fun before the real work begins for farming communities. "));
      KrynnDateTime KithKanandrasDT = new KrynnDateTime(currentDate.Year, 3, 14);
      Holidays.Add(new KrynnEvent(KithKanandrasDT, currentDate, "Kith-Kanandras", "Qualinesti elves use this day to honor the life of their founding father, Kith-Kanan; it is the anniversary of Kith-Kanan’s death.  The Qualinesti are different in that they celebrate the life of their founding father on both his day of Life-Gift and on the day of his death. "));
      KrynnDateTime SilvanosdrasDT = new KrynnDateTime(currentDate.Year, 3, 15);
      Holidays.Add(new KrynnEvent(SilvanosdrasDT, currentDate, "Silvanosdras", "Silvanesti elves use this day to honor the life of their founding father, Silvanos."));
      KrynnDateTime SpringDawningDT = new KrynnDateTime(currentDate.Year, 3, 21);
      Holidays.Add(new KrynnEvent(SpringDawningDT, currentDate, "Spring Dawning", " First day of spring and the day of the Vernal Equinox.  Celebrated by all of Ansalon, celebrations usually include some form of festival or feast.  All turnings of the season are seen as holy times for the clerics and druids of Chislev. "));
      KrynnDateTime DayOfTheHuntSpringDT = new KrynnDateTime(currentDate.Year, 3, 22);
      Holidays.Add(new KrynnEvent(DayOfTheHuntSpringDT, currentDate, "Day of the Hunt", "A holy day, on the first day of every new season, the followers of Kiri-Jolith spend the day seeking wrongs to right and helping people in need."));
      KrynnDateTime DayOfReflectionDT = new KrynnDateTime(currentDate.Year, 3, 25);
      Holidays.Add(new KrynnEvent(DayOfReflectionDT, currentDate, "Day of Reflection", "The followers of Zivilyn spend this holy day in meditation.  It usually begins with fasting and a fervent hymn."));

      KrynnDateTime ChildrensDayDT = new KrynnDateTime(currentDate.Year, 4, 1);
      Holidays.Add(new KrynnEvent(ChildrensDayDT, currentDate, "Chilren's Day", "It is a celebration of life, birth, and reproduction.  It is a day of wild abandon and reckless joy.  Any livestock born on this day are considered an omen of good luck. "));
      KrynnDateTime HarrowingDT = new KrynnDateTime(currentDate.Year, 4, 4);
      Holidays.Add(new KrynnEvent(HarrowingDT, currentDate, "Harrowing", "Planting season starts this day.  It is also seen as a holy day to the followers of Chislev.  Her clerics and druids will wander into communities blessing the crops for a good season."));
      KrynnDateTime ForgedayDT = new KrynnDateTime(currentDate.Year, 4, 19);
      Holidays.Add(new KrynnEvent(ForgedayDT, currentDate, "Forgeday", "This holy day of Reorx is celebrated mostly by dwarves and clerics of Reorx.  Blacksmiths forge particularly exquisite items and present them at a community feast."));

      KrynnDateTime BookClosingDT = new KrynnDateTime(currentDate.Year, 5, 4);
      Holidays.Add(new KrynnEvent(BookClosingDT, currentDate, "Book Closing", "Starting after the Blue Lady’s War, this was the only time Astinus would set aside his pen for a whole day.  Oddly nothing of historical significance ever seems to happen on this day.  The day is spent in remembrance of Astinus and reading one’s favorite book."));
      KrynnDateTime VisitingDayDT = new KrynnDateTime(currentDate.Year, 5, 11);
      Holidays.Add(new KrynnEvent(VisitingDayDT, currentDate, "Visiting Day", "This is a day of spring cleaning.  Most communities will clean in the morning, and then visit others’ houses in the afternoon.  The kender have their own version called “Hi, how are you?”  Many people see it as a localized version of wanderlust. "));
      KrynnDateTime FeastOfTheSeaDT = new KrynnDateTime(currentDate.Year, 5, 13);
      Holidays.Add(new KrynnEvent(FeastOfTheSeaDT, currentDate, "Feast of the Sea", "This day is considered holy to followers of Habbakuk and is also celebrated by seamen.  It is an auspicious day to launch, begin building, or christen a new sea vessel.  Most sailors toss small sacrifices to the waves for good luck."));
      KrynnDateTime VinasfestDT = new KrynnDateTime(currentDate.Year, 5, 25);
      Holidays.Add(new KrynnEvent(VinasfestDT, currentDate, "Vinasfest", "It is widely accepted in Solamnia that this is the day of Life-Gift for Vinas Solamnus and is used to honor the founding father of both Solamnia and her honorable protectors, the Knights of Solamnia. "));

      KrynnDateTime KithKananaithDT = new KrynnDateTime(currentDate.Year, 6, 1);
      Holidays.Add(new KrynnEvent(KithKananaithDT, currentDate, "Kith-Kananaith", "Qualinesti elves use this day to honor the life of their founding father, Kith-Kanan; it is the anniversary of Kith-Kanan’s Life-Gift.  The Qualinesti are different in that they celebrate the life of their founding father on both his day of Life-Gift and the day of his death."));
      KrynnDateTime DayOfStormsDT = new KrynnDateTime(currentDate.Year, 6, 3);
      Holidays.Add(new KrynnEvent(DayOfStormsDT, currentDate, "Day of Storms", "A holy day to the followers of Zeboim, it is also a day recognized by the northern sailors as the day that marks the middle of the storm season.  The day is always marked by violent thunderstorms and cyclones.  Sailors will pour red wine into the sea as a placating gift to the easily angered sea goddess."));
      KrynnDateTime MidYearDayDT = new KrynnDateTime(currentDate.Year, 6, 21);
      Holidays.Add(new KrynnEvent(MidYearDayDT, currentDate, "Mid-year Day", "This is the first day of summer and also the Summer Solstice.  All turnings of the season are seen as holy times for the clerics and druids of Chislev. "));
      KrynnDateTime DayOfTheHuntSummerDT = new KrynnDateTime(currentDate.Year, 6, 22);
      Holidays.Add(new KrynnEvent(DayOfTheHuntSummerDT, currentDate, "Day of the Hunt", "A holy day, on the first day of every new season, the followers of Kiri-Jolith spend the day seeking wrongs to right and helping people in need."));
      KrynnDateTime DayOfFireDT = new KrynnDateTime(currentDate.Year, 6, 23);
      Holidays.Add(new KrynnEvent(DayOfFireDT, currentDate, "Day of Fire", "Anniversary of the Second Cataclysm, the day Chaos was defeated."));

      KrynnDateTime DayOfSargonnasDT = new KrynnDateTime(currentDate.Year, 7, 1);
      Holidays.Add(new KrynnEvent(DayOfSargonnasDT, currentDate, "Day of Sargonnas", "This day is a day of celebration honoring the day the minotaurs revolted against the dwarves of Kal-Thax and the idea that “might makes right.”"));
      KrynnDateTime GreystoneEveDT = new KrynnDateTime(currentDate.Year, 7, 8);
      Holidays.Add(new KrynnEvent(GreystoneEveDT, currentDate, "Greystone Eve", "This day is recognized by dwarves and kender (and probably would be by gnomes as well, if they cared).  It is supposedly the day the Greystone of Gargath was released upon the world.  Dwarves view the day with superstition, while kender happily hold Stone hunts."));
      KrynnDateTime FestivalOfCandlesDT = new KrynnDateTime(currentDate.Year, 7, 15);
      Holidays.Add(new KrynnEvent(FestivalOfCandlesDT, currentDate, "Festival of Candles", "This day the dwarves of Thorbardin honor their dead, and it is known only to them. "));
      KrynnDateTime DayOfDragonsDT = new KrynnDateTime(currentDate.Year, 7, 20);
      Holidays.Add(new KrynnEvent(DayOfDragonsDT, currentDate, "Day of Dragons", "Recognized by evil chromatic dragons, it is the anniversary of their release from the Age of Exile.  They celebrate this day the best way they know how, by wanton destruction."));
      KrynnDateTime FamilyFeastDT = new KrynnDateTime(currentDate.Year, 7, 23);
      Holidays.Add(new KrynnEvent(FamilyFeastDT, currentDate, "Family Feast", "One month after the Day of Fire, it is a day of thanks and hope for the future. "));

      KrynnDateTime GoldenWeekStartDT = new KrynnDateTime(currentDate.Year, 8, 1);
      Holidays.Add(new KrynnEvent(GoldenWeekStartDT, currentDate, "Start of Golden Week", "The priests of Shinare spend the first of these holy days fasting and taking inventory.  It is always followed by a large party on the fifth day of the celebration."));
      KrynnDateTime GoldenWeekEndDT = new KrynnDateTime(currentDate.Year, 8, 5);
      Holidays.Add(new KrynnEvent(GoldenWeekEndDT, currentDate, "End of Golden Week", "The priests of Shinare spend the first of these holy days fasting and taking inventory.  It is always followed by a large party on the fifth day of the celebration."));
      KrynnDateTime FestivalOfBonesDT = new KrynnDateTime(currentDate.Year, 8, 28);
      Holidays.Add(new KrynnEvent(FestivalOfBonesDT, currentDate, "Festival of Bones", "Most people of Ansalon recognize this tradition of choosing one ancestor every year and honoring them on this day. "));

      KrynnDateTime HeroesMeetDT = new KrynnDateTime(currentDate.Year, 9, 13);
      Holidays.Add(new KrynnEvent(HeroesMeetDT, currentDate, "Heroes Meet", "Celebrated primarily in Abanasinia, this day is the anniversary of the day the Heroes of the Lance reunited before the war in Solace.  "));
      KrynnDateTime HarvestComeDT = new KrynnDateTime(currentDate.Year, 9, 21);
      Holidays.Add(new KrynnEvent(HarvestComeDT, currentDate, "Harvest Come", "Sometimes also referred to as “Summer’s End,” this holiday celebrates the last day of summer and occurs on the day of the Autumnal Equinox.  Most places celebrate this with a large local festival or feast.  All turnings of the season are seen as holy times for the clerics and druids of Chislev.  This day is also seen as holy to the followers of Chemosh symbolizing the decay of nature into autumn.  In the evening they consecrate new initiates. "));
      KrynnDateTime DayOfTheHuntAutumnDT = new KrynnDateTime(currentDate.Year, 9, 22);
      Holidays.Add(new KrynnEvent(DayOfTheHuntAutumnDT, currentDate, "Day of the Hunt", "A holy day, on the first day of every new season, the followers of Kiri-Jolith spend the day seeking wrongs to right and helping people in need. "));
      KrynnDateTime QualintsalarothDT = new KrynnDateTime(currentDate.Year, 9, 25);
      Holidays.Add(new KrynnEvent(QualintsalarothDT, currentDate, "Qualintsalaroth", "Meaning “Death of Qualinesti”, it marks the anniversary of the day the Qualinesti elves abandoned their forest to the encroaching Dragonarmies during the War of the Lance.  The Qualinesti elves use this day to honor their dead."));

      KrynnDateTime FestivalOfKnightsDT = new KrynnDateTime(currentDate.Year, 10, 12);
      Holidays.Add(new KrynnEvent(FestivalOfKnightsDT, currentDate, "Festival of Knights", "This was a day celebrated in Palanthas following the War of the Lance honoring their saviors, the Knights of Solamnia."));
      KrynnDateTime FestivalOfTheEyeDT = new KrynnDateTime(currentDate.Year, 10, 15);
      Holidays.Add(new KrynnEvent(FestivalOfTheEyeDT, currentDate, "Festival of the Eye", "This is a day of great magical power for wizards.  Although it is celebrated every year, the true power comes on a Night of the Eye.  The conjunction of the three moons to form a large eye staring down on Krynn happens over Ansalon only once every three years, and this is the night it occurs."));

      KrynnDateTime GenealogyDayDT = new KrynnDateTime(currentDate.Year, 11, 13);
      Holidays.Add(new KrynnEvent(GenealogyDayDT, currentDate, "Genealogy Day", "This day of celebration for the gnomes is sometimes called “Updateandverifyyourname Day.”  They spend the day doing just that. "));
      KrynnDateTime RifarsdagDT = new KrynnDateTime(currentDate.Year, 11, 16);
      Holidays.Add(new KrynnEvent(RifarsdagDT, currentDate, "Rifarsdag", "This is a day of celebration in Icewall, also known as “Reaver’s Day,” it is the anniversary of the death of White Dragon Highlord Feal-thas."));
      KrynnDateTime OldFolksDayDT = new KrynnDateTime(currentDate.Year, 11, 22);
      Holidays.Add(new KrynnEvent(OldFolksDayDT, currentDate, "Old Folk's Day", "Most communities use this day to honor the elders in the family.  The younger family members spend the day doing special chores for their elders. "));

      KrynnDateTime ThanksALotDayDT = new KrynnDateTime(currentDate.Year, 12, 6);
      Holidays.Add(new KrynnEvent(ThanksALotDayDT, currentDate, "Thanks-a-lot Day", "The Kender of the world spend this day in awe and thanks of all the wonderful things they have acquired over the year. "));
      KrynnDateTime KharasShameDT = new KrynnDateTime(currentDate.Year, 12, 10);
      Holidays.Add(new KrynnEvent(KharasShameDT, currentDate, "Kharas' Shame", "The Dwarves celebrate the dwarven hero Kharas.  All dwarves will tuck their beards into their shirts and belts in silent honor of Kharas who shaved his own beard in shame of the Dwarfgate Wars."));
      KrynnDateTime YuletideStartDT = new KrynnDateTime(currentDate.Year, 12, 19);
      Holidays.Add(new KrynnEvent(YuletideStartDT, currentDate, "First Day of Yuletide", "This is the first official day of the two week long Yuletide Celebration."));
      KrynnDateTime YuleDT = new KrynnDateTime(currentDate.Year, 12, 21);
      Holidays.Add(new KrynnEvent(YuleDT, currentDate, "Yule", "Celebrated around the continent, Yule signifies the first day of winter (the Winter Solstice) and is a time of family togetherness and celebration.  All turnings of the season are seen as holy times for the clerics and druids of Chislev. "));
      KrynnDateTime DayOfTheHuntWinterDT = new KrynnDateTime(currentDate.Year, 12, 22);
      Holidays.Add(new KrynnEvent(DayOfTheHuntWinterDT, currentDate, "Day of the Hunt", "A holy day, on the first day of every new season, the followers of Kiri-Jolith spend the day seeking wrongs to right and helping people in need."));
      KrynnDateTime BrothersbaneDT = new KrynnDateTime(currentDate.Year, 12, 25);
      Holidays.Add(new KrynnEvent(BrothersbaneDT, currentDate, "Brothersbane", "This day marks the catastrophic end to the Dwarfgate Wars.  Dwarves spend this day in mourning.  In the years following the War of the Lance on the morning of Brothersbane, the king of Thorbardin will meet with the thane of the Neidar on the shattered Plains of Dergoth and invite the hill dwarves into Thorbardin.  Every year, the Neidar respectfully decline."));

      foreach (KrynnEvent holiday in Holidays)
      {
        if (!holiday.UpdateTime(currentDate))
          Events.Add(holiday);
      }
    }
    private void UpdateEvents()
    {
      List<KrynnEvent> NewEventList = new List<KrynnEvent>();
      foreach (KrynnEvent Event in Events)
      {
        if (Event.UpdateTime(currentDate))
          MessageBox.Show(Event.DateTime.ToString() + "\r\n\r\n" + Event.Description, Event.Name);
        else
          NewEventList.Add(Event);
      }
      Events = NewEventList;
      if (currentDate.Year != HolidayYear || Events.Count == 0)
      {
        AddCurrentYearsHolidays();
        HolidayYear = currentDate.Year;
      }

      List<EventsListItem> items = new List<EventsListItem>();
      foreach (KrynnEvent Event in Events)
      {
        items.Add(new EventsListItem() { EventDT = Event.DateTime.ToString() + " ", Description = Event.Description + " ", Name = Event.Name });
      }
      EventsList.ItemsSource = items;
    }
    private void UpdateCalendar()
    {
      //int SelectedDayIndex = -1;
      //for(int i = 0; i < CalendarDays.Children.Count; i++)
      //{
      //  if (CalendarDays.Children[i] is CalendarDay)
      //  {
      //    CalendarDay cDay = CalendarDays.Children[i] as CalendarDay;
      //    if (cDay.Selected)
      //      SelectedDayIndex = i;
      //    break;
      //  }
      //}
      CalendarDays.Children.Clear();
      calendarMonthYear.Text = calendarDate.ToMonthYearString();
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = "Current Date: "+currentDate.ToString();
      DateInfoPanel.Visibility = Visibility.Collapsed;
      if (calendarDate.Year == 421 && calendarDate.Month == 10)
      {
        backOneMonth.IsEnabled = false;
      }
      else
      {
        backOneMonth.IsEnabled = true;
      }
      UpdateEvents();
      AddDayNames();
      AddCalendarDays();
      //if(SelectedDayIndex != -1)
      //{
      //  CalendarDay SelectedDay = CalendarDays.Children[SelectedDayIndex] as CalendarDay;
      //  SelectDay(SelectedDay);
      //}
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
      AddUpToOneDay(8 * 3600);
    }
    private void AddOneDay_Click(object sender, RoutedEventArgs e)
    {
      AddUpToOneDay(24 * 3600);
    }
    private void AddOneWeek_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.AddDays(7);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void AddOneMonth_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.AddMonths(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void AddOneYear_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.AddYears(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubEightHours_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractHours(8);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubOneHour_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractHours(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubThirtyMinutes_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMinutes(30);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubTenMinute_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMinutes(10);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubOneMinute_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMinutes(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubSixSeconds_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractSeconds(6);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubOneYear_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractYears(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubOneMonth_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractMonths(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubOneWeek_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractDays(7);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void SubOneDay_Click(object sender, RoutedEventArgs e)
    {
      currentDate = currentDate.SubtractDays(1);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }
    private void AddUpToOneDay(int seconds)
    {
      currentDate = currentDate.AddSeconds(seconds);
      currentDateTime.Text = currentDate.ToString();
      CurrentDTText.Text = currentDate.ToString();
      UpdateEvents();
    }

    //Misc. WPF Events
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
            Events.Clear();
            while(input.Position < input.Length)
            {
              KrynnEvent newEvent = (KrynnEvent)bf.Deserialize(input);
              Events.Add(newEvent);
            }
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
            foreach(KrynnEvent Event in Events)
            {
              bf.Serialize(output, Event);
            }
          }
        }
        catch (Exception ex)
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
      Events.Clear();
      UpdateCalendar();
    }


    //KrynnEvent Creation
    private void CreateNewEventButton_Click(object sender, RoutedEventArgs e)
    {
      if (String.IsNullOrEmpty(NewEventName.Text))
      {
        MessageBox.Show("Event Name cannot be empty", "Error Creating New Event");
        return;
      }
      KrynnDateTime NewEventDT = new KrynnDateTime((int)NewEventYear.Value, (int)NewEventMonth.Value, (int)NewEventDay.Value,
        (int)NewEventHour.Value, (int)NewEventMinute.Value, (int)NewEventSecond.Value);
      if(currentDate.SecondsUntil(NewEventDT) <= 0)
      {
        MessageBox.Show("Event must occur in the future", "Error Creating New Event");
        return;
      }
      Events.Add(new KrynnEvent(NewEventDT, currentDate, NewEventName.Text, NewEventDescription.Text));
      MessageBox.Show("Event Successfully Created");
      UpdateEvents();
      NewEventName.Text = "";
      NewEventDescription.Text = "";
      NewEventYear.Value = NewEventYear.Minimum;
      NewEventMonth.Value = NewEventMonth.Minimum;
      NewEventDay.Value = NewEventDay.Minimum;
      NewEventHour.Value = NewEventHour.Minimum;
      NewEventMinute.Value = NewEventMinute.Minimum;
      NewEventSecond.Value = NewEventSecond.Minimum;
    }

    private void EventToCurrentTime_Click(object sender, RoutedEventArgs e)
    {
      NewEventYear.Value = currentDate.Year;
      NewEventMonth.Value = currentDate.Month;
      NewEventDay.Value = currentDate.Day;
      NewEventHour.Value = currentDate.Hour;
      NewEventMinute.Value = currentDate.Minute;
      NewEventSecond.Value = currentDate.Second;
    }

    private void CreateNewAlarmButton_Click(object sender, RoutedEventArgs e)
    {
      if (String.IsNullOrEmpty(NewAlarmName.Text))
      {
        MessageBox.Show("Alarm Name cannot be empty", "Error Creating New Alarm");
        return;
      }
      KrynnDateTime NewAlarmDT = new KrynnDateTime(
        currentDate.Year+(int)NewAlarmYear.Value,
        currentDate.Month+(int)NewAlarmMonth.Value,
        currentDate.Day+(int)NewAlarmDay.Value,
        currentDate.Hour+(int)NewAlarmHour.Value,
        currentDate.Minute+(int)NewAlarmMinute.Value,
        currentDate.Second+(int)NewAlarmSecond.Value);
      if (currentDate.SecondsUntil(NewAlarmDT) <= 0)
      {
        MessageBox.Show("Alarm must occur in the future", "Error Creating New Alarm");
        return;
      }
      Events.Add(new KrynnEvent(NewAlarmDT, currentDate, NewAlarmName.Text, NewAlarmDescription.Text));
      MessageBox.Show("Alarm Successfully Created");
      UpdateEvents();
      NewAlarmName.Text = "";
      NewAlarmDescription.Text = "";
      NewAlarmYear.Value = NewAlarmYear.Minimum;
      NewAlarmMonth.Value = NewAlarmMonth.Minimum;
      NewAlarmDay.Value = NewAlarmDay.Minimum;
      NewAlarmHour.Value = NewAlarmHour.Minimum;
      NewAlarmMinute.Value = NewAlarmMinute.Minimum;
      NewAlarmSecond.Value = NewAlarmSecond.Minimum;
    }

    private void AlarmToZero_Click(object sender, RoutedEventArgs e)
    {
      NewAlarmYear.Value = 0;
      NewAlarmMonth.Value = 0;
      NewAlarmDay.Value = 0;
      NewAlarmHour.Value = 0;
      NewAlarmMinute.Value = 0;
      NewAlarmSecond.Value = 0;
    }


    //Day Selection
    private void CalendarDays_MouseUp(object sender, MouseButtonEventArgs e)
    {
      CalendarDay SelectedDay = sender as CalendarDay;
      SelectDay(SelectedDay);
    }

    private void SelectDay(CalendarDay SelectedDay)
    {
      if (SelectedDay.Selected)
      {
        SelectedDay.OnDeselect();
        DateInfoPanel.Visibility = Visibility.Collapsed;
      }
      else
      {
        foreach (object dayControl in CalendarDays.Children)
        {
          if (dayControl is CalendarDay)
          {
            CalendarDay cDay = dayControl as CalendarDay;
            cDay.OnDeselect();
          }
        }
        SelectedDay.OnSelect();
        DateInfoPanel.Visibility = Visibility.Visible;
      }
    }
  }
}
