using _2kvalikSemenova.DataBase;
using Microsoft.Win32;
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

namespace _2kvalikSemenova.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditTovarPage.xaml
    /// </summary>
    public partial class AddEditTovarPage : Page
    {
        public AddEditTovarPage()
        {
            InitializeComponent();
        }

        private SuitService suit;
        private byte[] imageBytes;

        public AddEditTovarPage(SuitService suit = null)
        {
            InitializeComponent();
            this.suit = suit;
            SuitTypeComboBox.ItemsSource = App.db.SuitType.ToList();
            SuitTypeComboBox.DisplayMemberPath = "Name";
            if (suit != null)
            {
                SuitPriceTb.Text = suit.Price.ToString();
                SuitNameTb.Text = suit.Name;
                SuitImage.Source = GetimageSources(suit.ImageBinary);

                SuitTypeComboBox.SelectedItem = suit.SuitType;
                imageBytes = suit.ImageBinary;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(SuitPriceTb.Text))
                error.Append("Введите цену услуги");
            if (string.IsNullOrWhiteSpace(SuitNameTb.Text))
                error.Append("Введите наименоване услуги");
            if (imageBytes == null || imageBytes.Length == 0)
                error.Append("Добавьте изображение для услуги");
            if (SuitTypeComboBox.SelectedItem is null)
                error.Append("Выберите тип услуги");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            if (suit != null)
            {
                suit.LastEditDate = DateTime.Now;
                suit.Price = int.Parse(SuitPriceTb.Text);
                suit.Name = SuitNameTb.Text;
                suit.ImageBinary = imageBytes;
                suit.SuitType_Id = (SuitTypeComboBox.SelectedItem as SuitType).Id;
            }
            else
            {
                SuitService newSuit = new SuitService()
                {
                    LastEditDate = DateTime.Now,
                    Price = int.Parse(SuitPriceTb.Text),
                    Name = SuitNameTb.Text,
                    ImageBinary = imageBytes,
                    SuitType_Id = (SuitTypeComboBox.SelectedItem as SuitType).Id,
                };
                App.db.SuitService.Add(newSuit);
            }

            App.db.SaveChanges();
            NavigationService.Navigate(new TovarList());

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TovarList());
        }

        private void FeedPriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
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

        private void EditImaeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                SuitImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}

      