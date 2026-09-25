using CarWashManager.Core;
using System.Windows;

namespace CarWashManager.WpfApp
{
    public partial class MainWindow : Window
    {
        private CarWashOrder[] orders = new CarWashOrder[100];
        private int count = 0;

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

            int index = OrdersDataGrid.SelectedIndex;

            if (index < 0 || index >= count)
            {
                StatusTextBlock.Text =
                    Properties.Resources.SelectOrderToEdit;

                return;
            }

            OrderDialog dialog = new OrderDialog(orders[index])
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                orders[index] = dialog.Order;

                RefreshDataGrid();
                UpdateSummary();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "";

            int index = OrdersDataGrid.SelectedIndex;

            if (index < 0 || index >= count)
            {
                StatusTextBlock.Text =
                    Properties.Resources.SelectOrderToDelete;

                return;
            }

            MessageBoxResult answer = MessageBox.Show(
                Properties.Resources.DeleteQuestion,
                Properties.Resources.DeleteTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (answer != MessageBoxResult.Yes)
            {
                return;
            }

            for (int i = index; i < count - 1; i++)
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

            int resultCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (orders[i].RegistrationNumber
                    .ToLower()
                    .Contains(searchText))
                {
                    results[resultCount] = orders[i];
                    resultCount++;
                }
            }

            CarWashOrder[] visibleResults =
                new CarWashOrder[resultCount];

            Array.Copy(
                results,
                visibleResults,
                resultCount);

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

            Array.Copy(
                orders,
                visibleOrders,
                count);

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