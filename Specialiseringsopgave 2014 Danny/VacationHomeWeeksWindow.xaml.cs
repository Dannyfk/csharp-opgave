using ClassLibrary;
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
using System.Windows.Shapes;

namespace Specialiseringsopgave_2014_Danny
{
    /// <summary>
    /// Interaction logic for VacationHomeWeeksWindow.xaml
    /// </summary>
    public partial class VacationHomeWeeksWindow : Window
    {
        int state = 0;
        int vacationHouseId = 0;
        List<VacationWeek> vacationWeeks = new List<VacationWeek>();
        public VacationHomeWeeksWindow(int id, int i)
        {
            vacationHouseId = id;
            InitializeComponent();
            if (i == 1)
            {
                state = 1;
            }
        }
        public void createGui()
        {
            for (int i = 1; i <= 52; i++)
            {
                System.Windows.Controls.TextBox newTBox = new TextBox();

                newTBox.Name = "TextBox" + i.ToString();
                newTBox.Tag = "t" + i;

                System.Windows.Controls.Label newLbl = new Label();

                newLbl.Name = "Label" + i.ToString();
                newLbl.Tag = "l" + i;
                newLbl.Content = "Week: " + i;

                if (i <= 13)
                {
                    sp1.Children.Add(newLbl);
                    sp1.Children.Add(newTBox);
                }
                else if (i > 13 && i <= 26)
                {
                    sp2.Children.Add(newLbl);
                    sp2.Children.Add(newTBox);
                }
                else if (i > 26 && i <= 39)
                {
                    sp3.Children.Add(newLbl);
                    sp3.Children.Add(newTBox);    
                }
                else
                {
                    sp4.Children.Add(newLbl);
                    sp4.Children.Add(newTBox);
                }
            }
        }

        private void updateGui()
        {
            foreach (VacationWeek vacationWeek in vacationWeeks)
            {
                if (vacationWeek.WeekNumber <= 13)
                {
                    updateWeekValues(sp1, vacationWeek);
                }
                else if (vacationWeek.WeekNumber > 13 && vacationWeek.WeekNumber <= 26)
                {
                    updateWeekValues(sp2, vacationWeek);
                }
                else if (vacationWeek.WeekNumber > 26 && vacationWeek.WeekNumber <= 39)
                {
                    updateWeekValues(sp3, vacationWeek);
                }
                else
                {
                    updateWeekValues(sp4, vacationWeek);
                }
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            vacationWeeks = Service.Instance.getVacationHousesWeeks(vacationHouseId);
            if (state != 1)
            {
                createGui();
            }
            else
            {
                createGui();
                btnCreate.Content = "Update";
                updateGui();
            }
        }

        // Add price to all
        private void addPriceAll(StackPanel s, TextBox t)
        {
            foreach (Object obj in s.Children)
            {
                if (obj is TextBox)
                {
                    (obj as TextBox).Text = t.Text;
                }
            }
        }

        private void btnColumn1_Click(object sender, RoutedEventArgs e)
        {
            addPriceAll(sp1,tBoxColumn1);
        }
        private void btnColumn2_Click(object sender, RoutedEventArgs e)
        {
            addPriceAll(sp2, tBoxColumn2);
        }
        private void btnColumn3_Click(object sender, RoutedEventArgs e)
        {
            addPriceAll(sp3, tBoxColumn3);
        }
        private void btnColumn4_Click(object sender, RoutedEventArgs e)
        {
            addPriceAll(sp4, tBoxColumn4);
        }

        // Buttons
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            VacationWeek vacationWeek = null;
            for (int i = 1; i <= 52; i++)
            {
                if (i <= 13)
                {
                    vacationWeek = createVacationWeek(sp1, i);
                }
                else if (i > 13 && i <= 26)
                {
                    vacationWeek = createVacationWeek(sp2, i);
                }
                else if (i > 26 && i <= 39)
                {
                    vacationWeek = createVacationWeek(sp3, i);
                }
                else
                {
                    vacationWeek = createVacationWeek(sp4, i);
                }
                if (state != 1)
                {
                    Service.Instance.createVacationWeek(vacationHouseId, vacationWeek);
                }
                else
                {
                    Service.Instance.updateVacationWeek(vacationHouseId, vacationWeek);
                }
            }
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private VacationWeek createVacationWeek(StackPanel s, int i)
        {
            VacationWeek vacationWeek = new VacationWeek(i);
            foreach (Object obj in s.Children)
            {
                if (obj is TextBox)
                {
                    if ((obj as TextBox).Tag.ToString() == ("t" + i))
                        {
                        int price = getPrice(s,i);
                        vacationWeek = new VacationWeek(i, price);
                        break;
                    }
                }
            }
            return vacationWeek;
        }

        private void updateWeekValues(StackPanel s, VacationWeek v)
        {
            foreach (Object obj in s.Children)
            {
                if (v.IsBooked == true)
                {
                    if (obj is TextBox && (obj as TextBox).Tag.ToString() == ("t" + v.WeekNumber))
                    {
                        (obj as TextBox).Text = v.Price + "";
                        (obj as TextBox).IsReadOnly = true;
                    }
                }
                else
                {
                    if (obj is TextBox && (obj as TextBox).Tag.ToString() == ("t" + v.WeekNumber))
                    {
                        (obj as TextBox).Text = v.Price + "";
                    }
                }
            }
        }

        private int getPrice(StackPanel s, int i)
        {
            int price = 0;
            foreach (Object obj in s.Children)
            {
                if (obj is TextBox)
                {
                    if ((obj as TextBox).Tag.ToString() == ("t" + i) && (obj as TextBox).Text!="")
                    {
                        price = Convert.ToInt32((obj as TextBox).Text);
                        break;
                    }
                }
            }
            return price;
        }
    }
}
