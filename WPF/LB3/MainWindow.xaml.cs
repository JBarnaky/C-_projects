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

namespace lab3_2705_1

{
    using Model;
    using Repository;
    using DAL_Mock;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        ProgramR repository = new Program1();
        private static List<ProgramM> lstProgramM = new List<ProgramM>();
        public MainWindow()
        {
            InitializeComponent();
            lwSelection.Items.Clear();
            
            foreach (Auto ap in repository.GetAll())
            {
                lwSelection.Items.Add(ap);
                
                lstProgramM.Add(ap);
            }
        }

       // private void button1_Click(object sender, RoutedEventArgs e)   // добавить
            
                private void button3_Click(object sender, RoutedEventArgs e)

        {
            WindowAdd b = new WindowAdd();
            b.ShowDialog();
            lwSelection.Items.Clear();
            foreach (ProgramM ap in repository.GetAll())
            {
                lwSelection.Items.Add(ap);
                lstProgramM.Add(ap);
            }
        }

  //      private void DelModel_Click(object sender, RoutedEventArgs e)     //удалить

                private void button2_Click(object sender, RoutedEventArgs e)
        {
            foreach (ProgramM ap in repository.GetAll())
            {
                if (ap.ProgramM == tbSearch.Text)
                {
                    //MessageBox.Show("Вы уверены?", "Удаление авто из базы",
                    //         MessageBoxButton.OKCancel, MessageBoxImage.Question);
                   // if (DialogResult.Value == MessageBoxResult.OK)
                    {
                        //listBox1.Items.Remove(ap);
                        //lstAuto.Remove(ap);
                        repository.Remove((string)ap.Model);
                    }
                }
            }

            lwSelection.Items.Clear();
            foreach (ProgramM ap in repository.GetAll())
            {
                lwSelection.Items.Add(ap);
                lstProgramM.Add(ap);
            }
        }

        //private void btnSearch_Click(object sender, RoutedEventArgs e) // как сделать поиск из DAL_Mock???     НАЙТИ
                private void button1_Click(object sender, RoutedEventArgs e)

        {
            lwSelection.Items.Clear();
            
            foreach (ProgramM ap in repository.GetAll())
            {
                if (cmbModel.IsSelected)
                {
                    if (tbSearch.Text == ap.Model)
                    {
                        lwSelection.Items.Add(ap);
                        lstProgramM.Add(ap);
                    }
                }

                if(cmbName.IsSelected)
                {
                    if (tbSearch.Text == ap.Name)
                    {
                        lwSelection.Items.Add(ap);
                        lstrogramM.Add(ap);
                    }
                }
            }
        }

        //private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void btnRefresh_Click(object sender, RoutedEventArgs e)    //  обновить
                private void button4_Click(object sender, RoutedEventArgs e)    //  обновить

        {
            lwSelection.Items.Clear();
            foreach (ProgramM ap in repository.GetAll())
            {
                lwSelection.Items.Add(ap);
                lstProgramM.Add(ap);
            }
        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)   // поиск по чём то
        //{
            
        //}

      //  private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
                private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
                   // listView1_SelectionChanged
        {
            Model.ProgramM programM = lwSelection.SelectedItem as ProgramM;
            if (programM != null)
            {
                labelFamA.Content = programM.FamA.ToString();
                labelImj.Content = programM.Imj.ToString();
                labelOtch.Content = programM.Otch.ToString();
                labelKurs.Content = programM.Kurs.ToString();
                labelStip.Content = programM.Stip.ToString();
                if (cmbModel.IsSelected)
                {
                    tbSearch.Text = programM.Model.ToString();
                }
                if(cmbName.IsSelected)
                {
                    tbSearch.Text = programM.Name.ToString();
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
