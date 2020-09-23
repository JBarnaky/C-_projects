using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CernelRun
{
    /// <summary>
    /// Interaction logic for Cannon.xaml
    /// </summary>
    public partial class Cannon : UserControl
    {
        public Cannon()
        {
            InitializeComponent();
        }

        public int AngleCannon
        {
            get { return (int)GetValue(AngleCannonProperty); }
            set { SetValue(AngleCannonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AngleCannon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleCannonProperty =
            DependencyProperty.Register("AngleCannon", typeof(int), typeof(Cannon), new PropertyMetadata(0));



        public double CartmanRun
        {
            get { return (double)GetValue(CartmanRunProperty); }
            set { SetValue(CartmanRunProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartmanRun.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartmanRunProperty =
            DependencyProperty.Register("CartmanRun", typeof(double), typeof(Cannon), new PropertyMetadata(60.206));




    }
}
