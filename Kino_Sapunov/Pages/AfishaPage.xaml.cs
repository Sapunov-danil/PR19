using Kino_Sapunov.Classes;
using Kino_Sapunov.Models;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Sapunov.Pages
{
    public partial class AfishaPage : Page
    {
        ObservableCollection<Afisha> afishas = new ObservableCollection<Afisha>();
        ObservableCollection<Kinoteatr> kinoteatrs = new ObservableCollection<Kinoteatr>();

        public AfishaPage()
        {
            InitializeComponent();
            LoadKinoteatrs();
            LoadAfisha();
        }

        private void LoadKinoteatrs()
        {
            kinoteatrs.Clear();
            using (var conn = Connection.OpenConnection())
            {
                string sql = "SELECT * FROM Kinoteatr";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kinoteatrs.Add(new Kinoteatr(
                        reader.GetInt32("id"),
                        reader.GetString("name"),
                        reader.GetInt32("count_zal"),
                        reader.GetInt32("count")
                    ));
                }
            }
            cbKino.ItemsSource = kinoteatrs;
        }

        private void LoadAfisha()
        {
            afishas.Clear();
            using (var conn = Connection.OpenConnection())
            {
                string sql = "SELECT * FROM Afisha";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    afishas.Add(new Afisha(
                        reader.GetInt32("id"),
                        reader.GetInt32("id_kinoteatr"),
                        reader.GetString("name"),
                        reader.GetDateTime("time"),
                        reader.GetInt32("price")
                    ));
                }
            }
            dgAfisha.ItemsSource = afishas;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (cbKino.SelectedValue == null || dpTime.SelectedDate == null)
            {
                MessageBox.Show("Выберите кинотеатр и дату!");
                return;
            }

            using (var conn = Connection.OpenConnection())
            {
                string sql = "INSERT INTO Afisha (id_kinoteatr, name, time, price) VALUES (@kino, @name, @time, @price)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kino", cbKino.SelectedValue);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@time", dpTime.SelectedDate.Value);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.ExecuteNonQuery();
            }

            LoadAfisha();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgAfisha.SelectedItem is Afisha selected)
            {
                using (var conn = Connection.OpenConnection())
                {
                    string sql = "UPDATE Afisha SET id_kinoteatr=@kino, name=@name, time=@time, price=@price WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@kino", cbKino.SelectedValue);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@time", dpTime.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@id", selected.id);
                    cmd.ExecuteNonQuery();
                }
                LoadAfisha();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAfisha.SelectedItem is Afisha selected)
            {
                using (var conn = Connection.OpenConnection())
                {
                    string sql = "DELETE FROM Afisha WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", selected.id);
                    cmd.ExecuteNonQuery();
                }
                LoadAfisha();
            }
        }

        private void dgAfisha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAfisha.SelectedItem is Afisha selected)
            {
                cbKino.SelectedValue = selected.id_kinoteatr;
                txtName.Text = selected.name;
                dpTime.SelectedDate = selected.time;
                txtPrice.Text = selected.price.ToString();
            }
        }
    }
}
