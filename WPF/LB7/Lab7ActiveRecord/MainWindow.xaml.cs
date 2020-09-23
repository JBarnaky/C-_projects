using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab7ActiveRecord
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using DAL;
    public partial class MainWindow : Window
    {
        List<VehiclesActiveRecord> listVehicles = new List<VehiclesActiveRecord>();

        public MainWindow()
        {
            InitializeComponent();
            ShowList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddDialog addDialog = new AddDialog();
            if (addDialog.ShowDialog() == true)
            {
                DAL.VehiclesActiveRecord addVehicle = new VehiclesActiveRecord();
                addVehicle.Model = addDialog.AddModel;
                addVehicle.Owner = addDialog.AddOwner;
                addVehicle.Number = addDialog.AddNumber;
                addVehicle.Color = addDialog.AddColor;
                addVehicle.Year = int.Parse(addDialog.AddYear);

                addVehicle.Add();
            }

            ShowList();
        }

        private void ShowList()
        {
            listVehicles.Clear();
            DataContext = listVehicles;
            foreach (var vehicle in DAL.VehiclesActiveRecord.GetAll())
            {
                listVehicles.Add(vehicle); 
            }
            dgVehicles.Items.Refresh();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedVehicle = dgVehicles.SelectedItem as VehiclesActiveRecord;
            DAL.VehiclesActiveRecord.Remove(selectedVehicle.ID);

            ShowList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedVehicle = dgVehicles.SelectedItem as VehiclesActiveRecord;
            UpdateDialog updateDialog = new UpdateDialog();

            if (updateDialog.ShowDialog() == true)
            {
                
                selectedVehicle.Model = updateDialog.UpdateModel;
                selectedVehicle.Owner = updateDialog.UpdateOwner;
                selectedVehicle.Number = updateDialog.UpdateNumber;
                selectedVehicle.Color = updateDialog.UpdateColor;
                selectedVehicle.Year = int.Parse(updateDialog.UpdateYear);

                selectedVehicle.Update();
            }

            ShowList();
        }
    }
}
