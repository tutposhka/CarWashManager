using CarWashManager.Core;
using System.Windows;

namespace CarWashManager.WpfApp
{
    public partial class OrderDialog : Window
    {
        public CarWashOrder Order { get; private set; }

        public OrderDialog()
        {
            InitializeComponent();

            VehicleTypeComboBox.ItemsSource = Enum.GetValues<VehicleType>();
            WashProgramComboBox.ItemsSource = Enum.GetValues<WashProgram>();
            StatusComboBox.ItemsSource = Enum.GetValues<OrderStatus>();

            VehicleTypeComboBox.SelectedIndex = 0;
            WashProgramComboBox.SelectedIndex = 0;
            StatusComboBox.SelectedIndex = 0;
        }

        public OrderDialog(CarWashOrder order) : this()
        {
            RegistrationTextBox.Text = order.RegistrationNumber;
            VehicleTypeComboBox.SelectedItem = order.VehicleType;
            WashProgramComboBox.SelectedItem = order.WashProgram;
            StatusComboBox.SelectedItem = order.Status;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";

            try
            {
                string registrationNumber =
                    RegistrationTextBox.Text.Trim();

                CarWashLogic.ValidateRegistrationNumber(
                    registrationNumber);

                VehicleType vehicleType =
                    (VehicleType)VehicleTypeComboBox.SelectedItem;

                WashProgram washProgram =
                    (WashProgram)WashProgramComboBox.SelectedItem;

                OrderStatus status =
                    (OrderStatus)StatusComboBox.SelectedItem;

                decimal price =
                    CarWashLogic.GetPrice(
                        vehicleType,
                        washProgram);

                int duration =
                    CarWashLogic.GetDurationMinutes(
                        vehicleType,
                        washProgram);

                Order = new CarWashOrder
                {
                    RegistrationNumber = registrationNumber,
                    VehicleType = vehicleType,
                    WashProgram = washProgram,
                    Status = status,
                    Price = price,
                    DurationMinutes = duration
                };

                DialogResult = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                ErrorTextBlock.Text =
                    Properties.Resources.RegistrationLength;
            }
            catch (ArgumentException)
            {
                ErrorTextBlock.Text =
                    Properties.Resources.RegistrationRequired;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}