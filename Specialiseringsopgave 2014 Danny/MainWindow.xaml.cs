using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Specialiseringsopgave_2014_Danny
{

    public partial class MainWindow : Window
    {
        // opdating tables need work

        DataTable destinationTable = null;
        DataTable vacationHouseTable = null;
        string destinationName = "";
        int IDHouse = -1;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadTables();
        }

        private void loadTables()
        {
            destinationTable = Service.Instance.LoadDestinationTable();
            dGridDestinations.DataContext = destinationTable.DefaultView;

            vacationHouseTable = Service.Instance.LoadVacationHouseTable();
            dGridVacation.DataContext = vacationHouseTable.DefaultView;
        }

        // Destination
        private void btnCreateDestination_Click(object sender, RoutedEventArgs e)
        {
            if (tBoxName.Text != "" && tBoxDay.Text != "" && tBoxPrice.Text != "")
            {
                lblDestinationError.Visibility = Visibility.Hidden;
                Destination d = new Destination(tBoxName.Text, tBoxDay.Text,Convert.ToInt32(tBoxPrice.Text));
                Service.Instance.createDestination(d);
                clearDestination();
            }
            else
            {
                lblDestinationError.Content = "Fill all boxes";
                lblDestinationError.Visibility = Visibility.Visible;
            }
            loadTables();
        }

        private void btnUpdateDestination_Click(object sender, RoutedEventArgs e)
        {
            if (destinationName != tBoxName.Text)
            {
                lblDestinationError.Content = "Not same destination";
                lblDestinationError.Visibility = Visibility.Visible;
            }
            else
            {
                lblDestinationError.Visibility = Visibility.Hidden;
                Destination destination = new Destination(tBoxName.Text,tBoxDay.Text,Convert.ToInt32(tBoxPrice.Text));
                Service.Instance.updateDestination(destination);
                loadTables();
                clearDestination();
            }
        }

        private void btnDeleteDestination_Click(object sender, RoutedEventArgs e)
        {
            if (destinationName != "")
            {
                lblDestinationError.Visibility = Visibility.Hidden;
                Service.Instance.deleteDestination(destinationName);
                clearDestination();
                destinationName = "";
            }
            else
            {
                lblDestinationError.Visibility = Visibility.Visible;
                lblDestinationError.Content = "Select a destination first";
            }
            loadTables();
        }

        private void clearDestination()
        {
            tBoxName.Text = "";
            tBoxDay.Text = "";
            tBoxPrice.Text = "";
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dGridDestinations.SelectedItem != null)
            {
                DataRowView row = dGridDestinations.SelectedItem as DataRowView;
                try
                {
                    lblDestinationError.Visibility = Visibility.Hidden;
                    destinationName = (string)row["name"];
                    string dayOfChange = (string)row["dayofchange"];
                    int price = (int)row["priceprperson"];
                    tBoxName.Text = destinationName;
                    tBoxDay.Text = dayOfChange;
                    tBoxPrice.Text = price+"";
                        
                }
                catch (NullReferenceException n)
                {
                    System.Windows.Forms.MessageBox.Show(n.Message.ToString());
                }
            }    
        }

        // Home vacation stuff
        private void btnCreateHouse_Click(object sender, RoutedEventArgs e)
        {
            if (destinationName == "")
            {
                lblVacationError.Content = "Select destination from list";
                lblVacationError.Visibility = Visibility.Visible;
            }
            else
            {
                if (tBoxPersons.Text != "" && tBoxShopping.Text != "" && tBoxBeach.Text != "" && tBoxDescription.Text != "")
                {
                    lblVacationError.Visibility = Visibility.Hidden;
                    VacationHouse v = new VacationHouse(tBoxDescription.Text, Convert.ToInt32(tBoxPersons.Text), Convert.ToInt32(tBoxShopping.Text), Convert.ToInt32(tBoxBeach.Text));
                    Service.Instance.createVacationHouse(v, destinationName);
                    IDHouse = Service.Instance.getVacationHouseId(v);
                    clearHouse();
                    VacationHomeWeeksWindow vacationHomeWeeksWindow = new VacationHomeWeeksWindow(IDHouse, 0);
                    vacationHomeWeeksWindow.Show();
                    this.Hide();
                }
                else
                {
                    lblVacationError.Content = "Fill all boxes";
                    lblVacationError.Visibility = Visibility.Visible;
                }
            }
            loadTables();
        }

        private void btnUpdateHouse_Click(object sender, RoutedEventArgs e)
        {
            if (IDHouse != -1)
            {
                lblVacationError.Visibility = Visibility.Hidden;
                VacationHouse vacationHouse = new VacationHouse(tBoxDescription.Text,Convert.ToInt32(tBoxPersons.Text),Convert.ToInt32(tBoxShopping.Text),Convert.ToInt32(tBoxBeach.Text));
                Service.Instance.updateVacationHouse(vacationHouse, IDHouse);
                clearHouse();
                IDHouse = -1;
            }
            else
            {
                lblVacationError.Content = "Please select a vacation house";
                lblVacationError.Visibility = Visibility.Visible;
            }
            loadTables();
        }

        private void btnDeleteHouse_Click(object sender, RoutedEventArgs e)
        {
            if (IDHouse != -1)
            {
                lblVacationError.Visibility = Visibility.Hidden;
                Service.Instance.deleteVacationHouse(IDHouse);
                clearHouse();
                IDHouse = -1;
            }
            else
            {
                lblVacationError.Content = "Please select a vacation house";
                lblVacationError.Visibility = Visibility.Visible;
            }
            loadTables();
        }

        private void clearHouse()
        {
            tBoxPersons.Text = "";
            tBoxShopping.Text = "";
            tBoxBeach.Text = "";
            tBoxDescription.Text = "";
        }

        private void dGridVacation_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dGridVacation.SelectedItem != null)
            {
                DataRowView row = dGridVacation.SelectedItem as DataRowView;
                try
                {
                    IDHouse = (int)row["id"];
                    string desc = (string)row["description"];
                    int maxPersons = (int)row["maxpersons"];
                    int distancetoshopping = (int)row["distancetoshopping"];
                    int distancetobeach = (int)row["distancetobeach"];
                    tBoxDescription.Text = desc;
                    tBoxPersons.Text = maxPersons+"";
                    tBoxShopping.Text = distancetoshopping+"";
                    tBoxBeach.Text = distancetobeach+"";
                }
                catch (NullReferenceException n)
                {
                    System.Windows.Forms.MessageBox.Show(n.Message.ToString());
                }
            }
        }

        private void btnUpdateWeeks_Click(object sender, RoutedEventArgs e)
        {
            if (IDHouse != -1)
            {
                lblVacationError.Visibility = Visibility.Hidden;
                VacationHomeWeeksWindow vacationHomeWeeksWindow = new VacationHomeWeeksWindow(IDHouse, 1);
                clearHouse();
                vacationHomeWeeksWindow.Show();
                this.Hide();
            }
            else
            {
                lblVacationError.Content = "Please select a vacation house";
                lblVacationError.Visibility = Visibility.Visible;
            }
        }
    }
}
