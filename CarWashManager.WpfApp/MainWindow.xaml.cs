using CarWashManager.Core;
using System.Windows;

namespace CarWashManager.WpfApp
{
    public partial class MainWindow : Window
    {
        private CarWashOrder[] orders = new CarWashOrder[100];
        private int count = 0;

        private int[] visibleIndexes = new int[100];
        private int visibleCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            RefreshDataGrid();
            UpdateSummary();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "";

            if (count >= orders.Length)
            {
                StatusTextBlock.Text =
                    Properties.Resources.OrderListFull;

                return;
            }

            OrderDialog dialog = new OrderDialog
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                orders[count] = dialog.Order;
                count++;

                RefreshDataGrid();
                UpdateSummary();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "";

            int selectedIndex = OrdersDataGrid.SelectedIndex;

            if (selectedIndex < 0 || selectedIndex >= visibleCount)
            {
                StatusTextBlock.Text =
                    Properties.Resources.SelectOrderToEdit;

                return;
            }

            int realIndex = visibleIndexes[selectedIndex];

            OrderDialog dialog = new OrderDialog(orders[realIndex])
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                orders[realIndex] = dialog.Order;

                RefreshDataGrid();
                UpdateSummary();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "";

            int selectedIndex = OrdersDataGrid.SelectedIndex;

            if (selectedIndex < 0 || selectedIndex >= visibleCount)
            {
                StatusTextBlock.Text =
                    Properties.Resources.SelectOrderToDelete;

                return;
            }

            int realIndex = visibleIndexes[selectedIndex];

            MessageBoxResult answer = MessageBox.Show(
                Properties.Resources.DeleteQuestion,
                Properties.Resources.DeleteTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (answer != MessageBoxResult.Yes)
            {
                return;
            }

            for (int i = realIndex; i < count - 1; i++)
            {
                orders[i] = orders[i + 1];
            }

            count--;

            RefreshDataGrid();
            UpdateSummary();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText =
                SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                RefreshDataGrid();
                return;
            }

            CarWashOrder[] results =
                new CarWashOrder[count];

            visibleCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (orders[i].RegistrationNumber
                    .ToLower()
                    .Contains(searchText))
                {
                    results[visibleCount] = orders[i];
                    visibleIndexes[visibleCount] = i;
                    visibleCount++;
                }
            }

            CarWashOrder[] visibleResults =
                new CarWashOrder[visibleCount];

            Array.Copy(
                results,
                visibleResults,
                visibleCount);

            OrdersDataGrid.ItemsSource = visibleResults;
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            CarWashOrder[] visibleOrders =
                new CarWashOrder[count];

            visibleCount = count;

            for (int i = 0; i < count; i++)
            {
                visibleOrders[i] = orders[i];
                visibleIndexes[i] = i;
            }

            OrdersDataGrid.ItemsSource = visibleOrders;
        }

        private void UpdateSummary()
        {
            decimal totalPrice =
                CarWashLogic.GetTotalPrice(orders, count);

            int activeDuration =
                CarWashLogic.GetActiveQueueDuration(orders, count);

            DateTime finishTime =
                CarWashLogic.GetEstimatedFinishTime(orders, count);

            SummaryTextBlock.Text =
                $"Tellimusi: {count} | " +
                $"Kokku: {totalPrice:F2} € | " +
                $"Aktiivne järjekord: {activeDuration} min | " +
                $"Valmis umbes: {finishTime:HH:mm}";
        }
    }
}