using _2kvalikSemenova.DataBase;
using _2kvalikSemenova.UC;
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

namespace _2kvalikSemenova.Pages
{
    /// <summary>
    /// Логика взаимодействия для TovarList.xaml
    /// </summary>
    public partial class TovarList : Page
    {
  
            private List<SuitService> suits;
            private List<SuitService> filteredSuits;

            private int currentPage = 0;
            private int currentSuitType = 1;

            public TovarList()
            {
                InitializeComponent();
                suits = App.db.SuitService.ToList();
                DateFilterComboBox.SelectedIndex = 0;
                ApplyFilterByType();
                SuitListButton.IsEnabled = false;
                CosplaysListButton.IsEnabled = true;
            }

            private void ApplyFilterByType()
            {
                filteredSuits = suits.Where(s => s.SuitType_Id == currentSuitType).ToList();
                currentPage = 0;
                RefreshList();
            }

            private void RefreshList()
            {
                SuitListWrapPanel.Children.Clear();

                if (filteredSuits == null)
                    return;

                var suitToShow = filteredSuits
                    .Skip(currentPage * 3)
                    .Take(3)
                    .ToList();

                switch (DateFilterComboBox.SelectedIndex)
                {
                    case 1:
                        suitToShow = suitToShow.OrderBy(x => x.Name).ToList();
                        break;
                    case 2:
                        suitToShow = suitToShow.OrderByDescending(x => x.Name).ToList();
                        break;
                }

                foreach (var suit in suitToShow)
                {
                    SuitListWrapPanel.Children.Add(new TovarUserControll(suit));
                }

                UpdateNavigationButtons();
                UpdatePageText(suitToShow.Count);
            }

            private void UpdatePageText(int shownCount)
            {
                int total = filteredSuits.Count;
                int start = currentPage * 3 + 1;
                int end = start + shownCount - 1;

                if (total == 0)
                {
                    PagesText.Text = "0 из 0";
                }
                else
                {
                    PagesText.Text = $"{start}-{end} из {total}";
                }
            }

            private void UpdateNavigationButtons()
            {
                PrefPageButton.IsEnabled = currentPage > 0;
                NextPageButton.IsEnabled = (currentPage + 1) * 3 < suits.Count();
            }

            private void PrefPageButton_Click(object sender, RoutedEventArgs e)
            {
                if (currentPage > 0)
                {
                    currentPage--;
                    RefreshList();
                }
            }

            private void NextPageButton_Click(object sender, RoutedEventArgs e)
            {
                if ((currentPage + 1) * 3 < suits.Count())
                {
                    currentPage++;
                    RefreshList();
                }
            }

            private void DateFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                RefreshList();
            }

            private void AddSuitButton_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.Navigate(new AddEditTovarPage());
            }

            private void SuitListButton_Click(object sender, RoutedEventArgs e)
            {
                currentSuitType = 1;
                SuitListButton.IsEnabled = false;
                CosplaysListButton.IsEnabled = true;
                ApplyFilterByType();
            }

            private void CosplaysListButton_Click(object sender, RoutedEventArgs e)
            {
                currentSuitType = 2;
                SuitListButton.IsEnabled = true;
                CosplaysListButton.IsEnabled = false;
                ApplyFilterByType();
            }
        }
    }

