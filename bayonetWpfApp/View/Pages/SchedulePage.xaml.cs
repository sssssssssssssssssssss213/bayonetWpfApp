using System.Windows;
using System.Linq;
using bayonetWpfApp.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace bayonetWpfApp.View.Pages
{
    public partial class SchedulePage : Page
    {
        private List<Schedule> allScheduleItems; 
        private List<Schedule> filteredScheduleItems; 
        public SchedulePage()
        {
            InitializeComponent();

          
            LoadScheduleItems();
        }

        private void LoadScheduleItems()
        {
            using (var context = new KnifeDBEntities())
            {
                
                allScheduleItems = context.Schedule.ToList();
                filteredScheduleItems = new List<Schedule>(allScheduleItems);


                ScheduleLv.ItemsSource = filteredScheduleItems;
            }
        }


        private void CompetitionsButton_Click(object sender, RoutedEventArgs e)
        {
   
            filteredScheduleItems = allScheduleItems.Where(item => item.EventTypeId == 1).ToList();
            ScheduleLv.ItemsSource = filteredScheduleItems;
        }

        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {
           
            filteredScheduleItems = allScheduleItems.Where(item => item.EventTypeId == 2).ToList();
            ScheduleLv.ItemsSource = filteredScheduleItems;
        }

        private void TrainingsButton_Click(object sender, RoutedEventArgs e)
        {
           
            filteredScheduleItems = allScheduleItems.Where(item => item.EventTypeId == 3).ToList();
            ScheduleLv.ItemsSource = filteredScheduleItems;
        }


    }
}
