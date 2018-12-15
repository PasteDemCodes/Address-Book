using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Address_Book
{
    /// <summary>
    /// Interaction logic for Birthday.xaml
    /// </summary>
    public partial class Birthday : Window
    {
        // UI's Colors are stored in here.
        ColorPallete mColorPallete = new ColorPallete();

        public Birthday()
        {
            InitializeComponent();

            // Hide ID Column in Contacts
            dataGrid_Birthday.Columns[0].Visibility = Visibility.Hidden;

            Button_exit.BorderBrush = mColorPallete.MainBodyColor;

            // Makes Window Moveable
            MouseLeftButtonDown += delegate { this.DragMove(); };
            
            string today = DateTime.UtcNow.Date.ToString("dd.MM.yyyy");

            // Clear dataGrid
            dataGrid_Birthday.ItemsSource = null;
            dataGrid_Birthday.Items.Refresh();

            string line = "";

            List<dataGriditems> items = new List<dataGriditems>();

            using (StreamReader sr = File.OpenText(@"Database/database.txt"))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] splitString = line.Split(',');

                    // splitString Indices
                    // 0: Contact ID        1: Name     2: Surname      3: Phonenumber  
                    // 4: Email             5: Birthday         6: Address


                    if (splitString[5] == today)
                    {
                        items.Add(new dataGriditems()
                        {
                            ID = splitString[0],
                            Name = splitString[1],
                            Surname = splitString[2],
                            Phonenumber = splitString[3],
                            Email = splitString[4],
                            Birthday = splitString[5],
                            Address = splitString[6]
                        });
                    }

                    line = sr.ReadLine();

                }
            }

            dataGrid_Birthday.ItemsSource = items;
        }

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            Button_exit.Foreground = mColorPallete.FocusedColor;
        }
    }
}
