using Kino_Sapunov.Classes;
using Kino_Sapunov.Models;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Sapunov.Pages
{
    public partial class KinoteatrPage : Page
    {
        ObservableCollection<Kinoteatr> kinoteatrs = new ObservableCollection<Kinoteatr>();
        public KinoteatrPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
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
            dgKinoteatr.ItemsSource = kinoteatrs;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = Connection.OpenConnection())
            {
                string sql = "INSERT INTO Kinoteatr (name, count_zal, count) VALUES (@n, @z, @c)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@z", txtZal.Text);
                cmd.Parameters.AddWithValue("@c", txtCount.Text);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgKinoteatr.SelectedItem is Kinoteatr selected)
            {
                using (var conn = Connection.OpenConnection())
                {
                    string sql = "UPDATE Kinoteatr SET name=@n, count_zal=@z, count=@c WHERE id=@id";
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@n", txtName.Text);
                    cmd.Parameters.AddWithValue("@z", txtZal.Text);
                    cmd.Parameters.AddWithValue("@c", txtCount.Text);
                    cmd.Parameters.AddWithValue("@id", selected.id);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgKinoteatr.SelectedItem is Kinoteatr selected)
            {
                using (var conn = Connection.OpenConnection())
                {
                    string sql = "DELETE FROM Kinoteatr WHERE id=@id";
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", selected.id);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

    }
}
