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

namespace lab3_2705_1
{
    using DAL_Mock;
    using Model;
    using Repository;
    /// <summary>
    /// Interaction logic for WindowAdd.xaml
    /// </summary>
    /// 


    
    public partial class WindowAdd : Window
    {
        //public partial class WindowAdd : Window
            ProgramR repository = new ProgramR();
            public  WindowAdd()
        {
            InitializeComponent();
        }
            private void button1_Click(object sender, RoutedEventArgs e)
          {
            string FamA = textBoxFAM.Text;
            string Imj  = textBoxIM.Text;
            string Otch =  textBoxOT.Text;
            int Kurs    =  textBoxKU.Text;
            double Stip =  textBoxST.Text;
            string Gor  =  textBoxG.Text;
            ProgramM infProgramM = new ProgramM (FamA, Imj, Otch, Kurs, Stip, Gor);
            repository.Add(infProgramM);
            Close();
        }

          private void button1_Click(object sender, RoutedEventArgs e)
          {

          }

          private void button2_Click(object sender, RoutedEventArgs e)
          {

          }

      
}
}