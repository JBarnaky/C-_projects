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
    /// Interaction logic for AddDialog.xaml
    /// </summary>
    public partial class AddDialog : Window
    {
        public AddDialog()
        {
            InitializeComponent();
        }

        public string AddModel
        {
            get { return tbModel.Text; }
        }

        public string AddOwner
        {
            get { return tbOwner.Text; }
        }

        public string AddNumber
        {
            get { return tbNumber.Text; }
        }

        public string AddColor
        {
            get { return tbColor.Text; }
        }

        public string AddYear
        {
            get { return tbYear.Text; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
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
