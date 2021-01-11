using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Microsoft.EntityFrameworkCore;
//using System.Linq;
using System;
using System.Linq;
using System.Collections;

namespace ReceiptTrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecieptData __db;
        //dirty code for demo
        int count = 0;
        private readonly Random __random;
        String[] store_array;
        public MainWindow()
        {
            store_array =new string [] { "walmart","target","aldi"};
           __random = new Random();
        InitializeComponent();
            dataView.CanUserAddRows = false;
            dataView.CanUserDeleteRows = false;
            __db = new RecieptData();
            __db.receipts.Load();
            dataView.DataContext = __db.receipts.Local.ToObservableCollection();
            dataView.ItemsSource = __db.receipts.Local.ToObservableCollection();
            dataView.Items.Refresh();

            // dataView.ItemsSource=__db.receipts.Local.ToObservableCollection();
            dataView.Items.Refresh();
           // dataView.SourceUpdated += __db.receipts.Local.CollectionChanged;
            /*
            using (var db = new RecieptData())
            {
                //dataView.ItemsSource = db.receipts.Local.ToObservableCollection();
                //dataView.ItemsSource = db.receipts.Local.ToBindingList();
                dataView.ItemsSource = db.receipts.Local.ToObservableCollection();

            }
            */

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            label1.Text = "clicked " + count;
            count = count + 1;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var db = new RecieptData())
            {
               int store_enum = __random.Next(0, store_array.Length);
                decimal cost = Decimal.Round((decimal)(__random.NextDouble() * Math.Pow( 10 , __random.Next(0, 4))), 2);
                int year = __random.Next(2015, 2020);
                int month = __random.Next(1, 12);
                int day = __random.Next(1, DateTime.DaysInMonth(year, month));
                
                db.Add(new Receipt() { store = store_array[store_enum], price = cost, day = new DateTime(year,month,day) });
                db.SaveChanges();
         
             
            }
        }

      
        private void resetView() {
            dataView.Columns.Clear();
            var storeCol = new DataGridTextColumn() { Header = "store" };
            var priceCol = new DataGridTextColumn() { Header = "price" };
            var dateCol = new DataGridTextColumn() { Header = "date" };
            dataView.Columns.Add(storeCol);
            dataView.Columns.Add(priceCol);
            dataView.Columns.Add(dateCol);
 
        }
      

      

        private void receipt_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            __db.receipts.Load();
            dataView.Items.Refresh();
        }

 


        private void test_data_Loaded(object sender, RoutedEventArgs e)
        {
            dataBlock.Text = "hi";
            
            foreach (var reciept in __db.receipts.Local.ToObservableCollection()) {
                dataBlock.Text = "hello";
            }
        }

   
        private void dataDelete_Click(object sender, RoutedEventArgs e)
        {
            var removeQuery = __db.receipts.Where(rec => rec.id != null);
            __db.RemoveRange(removeQuery);
            __db.SaveChanges();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void run_query_but_Click(object sender, RoutedEventArgs e)
        {
            string query_price = query_box.Text;
            decimal? price = null;
            try
            {
                price = (decimal)Convert.ToDouble(query_price);

                    }
            catch (System.Exception)
            { 
            
            
            }
            if (price is not null) {
                var close_price_list =__db.receipts.Where(x => x.price < price + 5 && x.price > price - 5).OrderBy(x =>x.id).ToList();

                query_data_grid.ItemsSource = close_price_list;
                query_data_grid.Items.Refresh();
            }


        }

       
    }
}
