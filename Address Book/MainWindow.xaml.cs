using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Address_Book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // UI's Colors are stored in here.
        ColorPallete mColorPallete = new ColorPallete();

        // Each Contact has a unique ID
        int ContactUniqueID;

        // Indicates if user is editing a contact
        bool Editing_live;

        // Contact editing id, used to keep database always sorted.
        int Editing_ID;

        public object MessageBoxButtons { get; private set; }
        public object MessageBoxIcon { get; private set; }

        // Returns missing IDs in database, if a contact is deleted another has to take its place.
        // Returns highest ID + 1, if IDs are enumerated.
        int getContactUniqueID()
        {
            // try: get a list of all IDs existing in database.
            // Sort and Compare them to an enumarated list.
            // Missing IDs are returned.
            // If Missing IDs is empty, return highest int in database.
            try
            {
                string line = "";
                List<int> IDs = new List<int>();
                using (StreamReader sr = File.OpenText(@"Database/database.txt"))
                {
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        IDs.Add(Convert.ToInt32(line.Substring(0, line.IndexOf(','))));
                        line = sr.ReadLine();
                    }
                }

                IDs.Sort();
                var MissingIDs = Enumerable.Range(0, IDs[IDs.Count - 1]).Except(IDs);                

                if (MissingIDs.Any())
                {
                    return MissingIDs.First();
                }
                else
                {
                    return IDs.Max() + 1;
                }
            }

        // catch: Database is empty, return 0.
        catch (Exception)
            {
                return 0;
            }
            
        }

        void BorderColorReset()
        {
            TextBox_Name.BorderBrush = mColorPallete.MainTextboxBorderColor;
            TextBox_Surname.BorderBrush = mColorPallete.MainTextboxBorderColor;
            TextBox_Email.BorderBrush = mColorPallete.MainTextboxBorderColor;
            TextBox_Phonenumber.BorderBrush = mColorPallete.MainTextboxBorderColor;
            TextBox_Address.BorderBrush = mColorPallete.MainTextboxBorderColor;
        }

        void UpdateContactDataGrid(string query)
        {
            // Clear dataGrid
            dataGrid_Contacts.ItemsSource = null;
            dataGrid_Contacts.Items.Refresh();

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
                    
                    
                    if ((splitString[1].Contains(query) || splitString[2].Contains(query) || 
                         splitString[3].Contains(query) || splitString[4].Contains(query) || query == ""))
                    {
                        items.Add(new dataGriditems() { ID = splitString[0], Name = splitString[1],
                                                        Surname = splitString[2], Phonenumber = splitString[3],
                                                        Email = splitString[4], Birthday = splitString[5],
                                                        Address = splitString[6]});
                    }

                    line = sr.ReadLine();

                }
            }
            
            dataGrid_Contacts.ItemsSource = items;

        }

        // Saves user's selected image
        void SaveImage(string save_location)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image_Contact.Source));
            using (FileStream stream = new FileStream(save_location, FileMode.Create))
                encoder.Save(stream);            
        }


        public MainWindow()
        {
            InitializeComponent();
            
            // Makes Window Moveable
            MouseLeftButtonDown += delegate { this.DragMove(); };

            // Hide ID Column in Contacts
            //dataGrid_Contacts.Columns[0].Visibility = Visibility.Hidden;
            dataGrid_Contacts.IsReadOnly = true;

            // launch Birthday window at center of screen.
            Birthday window = new Birthday
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true
            };
            window.Show();

            // Initiating UniqueID when form launches
            ContactUniqueID = getContactUniqueID();
            
            // Assigning Colors to Assets

            Label_logo.Foreground = mColorPallete.MainTextColor;
            Label_Add_Contact.Foreground = mColorPallete.MainTextColor;
            Label_Contacts.Foreground = mColorPallete.MainTextColor;

            Label_Name.Foreground = mColorPallete.MainTextColor;
            Label_Surname.Foreground = mColorPallete.MainTextColor;
            Label_Email.Foreground = mColorPallete.MainTextColor;
            Label_Phonenumber.Foreground = mColorPallete.MainTextColor;
            Label_Email.Foreground = mColorPallete.MainTextColor;
            Label_Birthday.Foreground = mColorPallete.MainTextColor;
            Label_Address.Foreground = mColorPallete.MainTextColor;
            

            Rectangle_Image_Frame.Stroke = mColorPallete.SelectedColor;
            Rectangle_Image_Frame1.Stroke = mColorPallete.SelectedColor;

            Button_exit.BorderBrush = mColorPallete.MainBodyColor;

            Image_Contact.Source = new BitmapImage(new Uri(@"UI Icons/contact_default.png", UriKind.Relative));
            Image_Contacts.Source = new BitmapImage(new Uri(@"UI Icons/contact_default.png", UriKind.Relative));
        }        

        private void Button_Browse_Image_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "Image Files (*.png)|*.png;"
            };

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {            
                string filename = dlg.FileName;
                Image_Contact.Source = new BitmapImage(new Uri(@filename, UriKind.Absolute));
            }
        }

        // Accept only numbers as input.
        private void TextBox_Phonenumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

       
        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            // Clear dataGrid
            dataGrid_Contacts.ItemsSource = null;
            dataGrid_Contacts.Items.Refresh();

            string name = TextBox_Name.Text.ToString();
            string surname = TextBox_Surname.Text.ToString();
            string email = TextBox_Email.Text.ToString();
            string phonenumber = TextBox_Phonenumber.Text.ToString();
            string address = TextBox_Address.Text.ToString();
            DateTime? selectedDate = DatePicker_Birthday.SelectedDate;

            // Reset Border Colors
            BorderColorReset();

            // Validating Data
            Validator mValidator = new Validator(name, surname, email, phonenumber, address);

            if (mValidator.IsValid())
            {
                // Creating Object based on textboxes
                Person person = new Person(name, surname, phonenumber)
                {
                    Email = email,
                    Birthday = selectedDate,
                    Address = address
                };

                // Creating string to be inserted into database.
                // String Format: ID,NAME,SURNAME,PHONENUMBER,EMAIL,BIRTHDAY,ADDRESS
                string data = "";
                if (Editing_live)
                {
                    data += Editing_ID + ",";
                }
                else
                {
                    ContactUniqueID = getContactUniqueID();
                    data += ContactUniqueID.ToString() + ",";                    
                }

                data += person.Name + "," + person.Surname + "," 
                      + person.Phonenumber + "," + person.Email + ",";

                if (selectedDate != null)
                {
                    data += person.Birthday.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) + ",";
                }
                else
                {
                    data += "-,";
                }

                data += person.Address;

                // Get image Source path.
                // Change endfix to UniqueID + file extension
                // Glue them back together
                // Copy original image to /Contact Images
                string img_source = Image_Contact.Source.ToString();
                string save_loc = "";
                string[] database = File.ReadAllLines(@"Database/database.txt");

                if (Editing_live)
                {                    
                    database[Editing_ID] = data;
                    
                    Editing_live = false;                    
                    save_loc = @"Contact Images/" + Editing_ID + '.' + img_source.Split('.')[1];                    
                }
                else
                {
                    Array.Resize(ref database, database.Length + 1);
                    database[database.Length - 1] = data;                    
                    save_loc = @"Contact Images/" + ContactUniqueID.ToString() + '.' + img_source.Split('.')[1];
                }

                // Always sort database before saving to file.
                Array.Sort(database);
                File.WriteAllLines(@"Database/database.txt", database);

                SaveImage(save_loc);
                Button_Reset.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Label_Contact_Saved.Visibility = Visibility.Visible;
            }
            else
            {
                if (mValidator.IsEmailValid() != true)
                {
                    TextBox_Email.BorderBrush = mColorPallete.ErrorColor;
                }

                if (mValidator.IsPhonenumberValid() != true)
                {
                    TextBox_Phonenumber.BorderBrush = mColorPallete.ErrorColor;
                }
                if (mValidator.IsNameValid() != true)
                {
                    TextBox_Name.BorderBrush = mColorPallete.ErrorColor;
                }
                if (mValidator.IsSurnameValid() != true)
                {
                    TextBox_Surname.BorderBrush = mColorPallete.ErrorColor;
                }
            }
            
        }

        // Edit Existing Contact
        private void Button_Edit_Contact_Click(object sender, RoutedEventArgs e)
        {
            //   Read Selected Row as dataGridItems Object
            var data = dataGrid_Contacts.SelectedItem as dataGriditems;
            
            TextBox_Name.Text = data.Name;
            TextBox_Surname.Text = data.Surname;
            TextBox_Email.Text = data.Email;
            TextBox_Phonenumber.Text = data.Phonenumber;
            TextBox_Address.Text = data.Address; 

            if (data.Birthday != "-")
            {
                string date = data.Birthday.Replace('.', '-');
                DatePicker_Birthday.SelectedDate = DateTime.Parse(date);
            }
            else
            {
                DatePicker_Birthday.SelectedDate = null;
            }
            var path = Path.Combine(Path.DirectorySeparatorChar.ToString(),
                                    AppDomain.CurrentDomain.BaseDirectory, "Contact Images/" + data.ID) + ".png";

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = new Uri(path);
            image.EndInit();
            Image_Contact.Source = image;

            Editing_live = true;
            Editing_ID = Convert.ToInt32(data.ID);
            Grid_Contacts.Visibility = Visibility.Hidden;
            Grid_Add_Contact.Visibility = Visibility.Visible;
        }


        // Delete Selected Contact
        private void Button_Delete_Contact_Click(object sender, RoutedEventArgs e)
        {
            //   Read Selected Row as dataGridItems Object
            var data = dataGrid_Contacts.SelectedItem as dataGriditems;

            int no_line = dataGrid_Contacts.SelectedIndex;
            
            string[] database = File.ReadAllLines(@"Database/database.txt");
            try
            {
                string last_entry = database[database.Length -1 ];
                database[no_line] = last_entry;
            }
            catch (Exception)
            {
                
            }
            finally
            {
                Array.Resize(ref database, database.Length - 1);
                Array.Sort(database);
                File.WriteAllLines(@"Database/database.txt", database);
            }           

            UpdateContactDataGrid("");
        }

        // Updates Contact's DataGrid based on User's Search Query.     

        private void TextBox_Search_KeyUp(object sender, KeyEventArgs e)
        {
            string query = TextBox_Search.Text.ToString();
            UpdateContactDataGrid(query);
        }

        // Change picture when user navigates between rows

        private void dataGrid_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Editing_live)
            {
                var data = dataGrid_Contacts.SelectedItem as dataGriditems;
                try
                {
                    var path = Path.Combine(Path.DirectorySeparatorChar.ToString(),
                                        AppDomain.CurrentDomain.BaseDirectory, "Contact Images/" + data.ID) + ".png";

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource = new Uri(path);
                    image.EndInit();
                    Image_Contacts.Source = image;
                }
                catch (Exception)
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource = new Uri("Contact Images/Default.png", UriKind.Relative);
                    image.EndInit();
                    Image_Contacts.Source = image;
                }
            }
        }

        // Switching Colors when user navigates

        private void Label_Contacts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateContactDataGrid("");
            Editing_live = false;
            Grid_Contacts.Visibility = Visibility.Visible;
            Grid_Add_Contact.Visibility = Visibility.Hidden;
            Label_Contact_Saved.Visibility = Visibility.Hidden;
        }


        private void Label_Contacts_MouseEnter(object sender, MouseEventArgs e)
        {
            Label_Contacts.Foreground = mColorPallete.FocusedColor;
            Label_Contacts.Background = mColorPallete.MainTextColor;

            Label_Add_Contact.Foreground = mColorPallete.MainTextColor;
            Label_Add_Contact.Background = null;
        }

        private void Label_Contacts_MouseLeave(object sender, MouseEventArgs e)
        {
            Label_Contacts.Foreground = mColorPallete.MainTextColor;
            Label_Contacts.Background = null;
        }

        private void Label_Add_Contact_MouseEnter(object sender, MouseEventArgs e)
        {
            Label_Add_Contact.Foreground = mColorPallete.FocusedColor;
            Label_Add_Contact.Background = mColorPallete.MainTextColor;

            Label_Contacts.Foreground = mColorPallete.MainTextColor;
            Label_Contacts.Background = null;            
        }

        private void Label_Add_Contact_MouseLeave(object sender, MouseEventArgs e)
        {
            Label_Add_Contact.Foreground = mColorPallete.MainTextColor;
            Label_Add_Contact.Background = null;
        }

        private void Label_Add_Contact_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid_Add_Contact.Visibility = Visibility.Visible;
            Grid_Contacts.Visibility = Visibility.Hidden;            
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Name.Text = string.Empty;
            TextBox_Surname.Text = string.Empty;
            TextBox_Email.Text = string.Empty;
            TextBox_Phonenumber.Text = string.Empty;
            TextBox_Address.Text = string.Empty;

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = new Uri("Contact Images/Default.png", UriKind.Relative);
            image.EndInit();
            Image_Contact.Source = image;

            Label_Contact_Saved.Visibility = Visibility.Hidden;
        }

        private void Button_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            Button_exit.Foreground = mColorPallete.FocusedColor;
        }

        // Terminates Program

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        
    }

}
