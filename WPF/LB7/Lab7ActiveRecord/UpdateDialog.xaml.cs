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
using System.Windows.Shapes;

namespace Lab7ActiveRecord
{
    /// <summary>
    /// Interaction logic for UpdateDialog.xaml
    /// </summary>
    public partial class UpdateDialog : Window
    {
        public UpdateDialog()
        {
            InitializeComponent();
        }

        public string UpdateModel
        {
            get { return tbModel.Text; }
        }

        public string UpdateOwner
        {
            get { return tbOwner.Text; }
        }

        public string UpdateNumber
        {
            get { return tbNumber.Text; }
        }

        public string UpdateColor
        {
            get { return tbColor.Text; }
        }

        public string UpdateYear
        {
            get { return tbYear.Text; }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
