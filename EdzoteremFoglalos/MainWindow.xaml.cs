using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace EdzoteremFoglalos
{
    public partial class MainWindow : Window
    {
        private Gym gym;
        private string fileName = "foglalasok.txt";

        public MainWindow()
        {
            InitializeComponent();
            gym = new Gym();
            loadReservations();
            showReservations();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            int time;

            if (name == "")
            {
                MessageBox.Show("Add meg a nevet");
                return;
            }

            saveReservations();
            showReservations();
            clearFields();
            MessageBox.Show("Sikeres foglalas");
        }

        private void showReservations()
        {
            time10TextBlock.Text = getStatus(10);
            time12TextBlock.Text = getStatus(12);
            time14TextBlock.Text = getStatus(14);
            time16TextBlock.Text = getStatus(16);
            time18TextBlock.Text = getStatus(18);
        }

        private string getStatus(int time)
        {
            foreach (GymReservation reservation in gym.getReservations())
            {
                if (reservation.getReservationTime() == time)
                {
                    if (reservation is TeamReservation)
                    {
                        return "Csoportos";
                    }

                    return "Egyeni";
                }
            }

            return "Szabad";
        }

        private void clearFields()
        {
            nameTextBox.Text = "";
            timeTextBox.Text = "";
            membersTextBox.Text = "";
            teamCheckBox.IsChecked = false;
        }

        private void saveReservations()
        {
            List<string> lines = new List<string>();

            foreach (GymReservation reservation in gym.getReservations())
            {
                TeamReservation teamReservation = reservation as TeamReservation;

                if (teamReservation != null)
                {
                    string members = string.Join(",", teamReservation.getTeamMembers());

                    lines.Add("team;" +teamReservation.getContactName() + ";" +teamReservation.getReservationTime() + ";" +members);
                }
                else
                {
                    lines.Add("single;" +reservation.getContactName() + ";" +reservation.getReservationTime());
                }
            }

            File.WriteAllLines(fileName, lines);
        }

        private void loadReservations()
        {
            if (File.Exists(fileName) == false)
            {
                return;
            }

            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                if (parts.Length < 3)
                {
                    continue;
                }

                int time;

                if (int.TryParse(parts[2], out time) == false)
                {
                    continue;
                }

                if (parts[0] == "team")
                {
                    List<string> members = new List<string>();

                    if (parts.Length > 3)
                    {
                        string[] memberNames = parts[3].Split(',');

                        foreach (string member in memberNames)
                        {
                            if (member.Trim() != "")
                            {
                                members.Add(member.Trim());
                            }
                        }
                    }

                    TeamReservation teamReservation = new TeamReservation(parts[1], members, time);

                    gym.addReservation(teamReservation);
                }
                else if (parts[0] == "single")
                {
                    SingleReservation singleReservation = new SingleReservation(parts[1], time);

                    gym.addReservation(singleReservation);
                }
            }
        }
        
    }
}
