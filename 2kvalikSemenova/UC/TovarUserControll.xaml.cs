using _2kvalikSemenova.DataBase;
using _2kvalikSemenova.Pages;
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

namespace _2kvalikSemenova.UC
{
    /// <summary>
    /// Логика взаимодействия для TovarUserControll.xaml
    /// </summary>
    public partial class TovarUserControll : UserControl
    {
        private SuitService suit;
        public TovarUserControll(SuitService suit)
        {

            InitializeComponent();
            this.suit = suit;
            SuitImage.Source = GetimageSources(suit.ImageBinary);
            SuitNameText.Text = $"Наименование: {suit.Name}";
            SuitPriceText.Text = $"Цена: {suit.Price}";
            SuitTypeText.Text = $"Тип {suit.SuitType.Name}";
            SuitLastEditDateText.Text = $"Последняя дата измения {suit.LastEditDate}";
        
    }


        private BitmapImage GetimageSources(byte[] byteImage)
        {
            if (byteImage != null)
            {
                MemoryStream memoryStream = new MemoryStream(byteImage);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
            return null;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Current.Navigate(new AddEditTovarPage(suit));
        }
    }
}

