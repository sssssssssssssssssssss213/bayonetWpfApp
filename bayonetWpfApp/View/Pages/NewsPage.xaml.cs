using bayonetWpfApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace bayonetWpfApp.View.Pages
{
    public partial class NewsPage : Page
    {
        private List<News> newsList;

        public NewsPage()
        {
            InitializeComponent();

            
            LoadNews();
        }

        private void LoadNews()
        {
    
            newsList = GetNewsFromDatabase();

            
            newsLb.ItemsSource = newsList;
        }

        
        private List<News> GetNewsFromDatabase()
        {
            

            List<News> news = new List<News>();

            
            using (var context = new KnifeDBEntities())
            {
                news = context.News.ToList();
            }

            return news;
        }
    }
}
