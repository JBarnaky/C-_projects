using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CernelRun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard story = new Storyboard();
        DispatcherTimer cernelTimer = new System.Windows.Threading.DispatcherTimer();

        Kernel ker = null;

        private DelegateCommand fireCommand;
        private DelegateCommand speedUpCommand;
        private DelegateCommand speedDownCommand;
        private DelegateCommand upRunCommand;
        private DelegateCommand downRunCommand;      

        public DelegateCommand FireCommand
        {
            get { return fireCommand; }
        }
        public DelegateCommand UpRunCommand
        {
            get { return upRunCommand; }
        }
        public DelegateCommand DownRunCommand
        {
            get { return downRunCommand; }
        }
        public DelegateCommand SpeedUpCommand
        {
            get { return speedUpCommand; }
        }

        public DelegateCommand SpeedDownCommand
        {
            get { return speedDownCommand; }
        }

        public MainWindow()
        {
            InitializeComponent();
            SetUpCommands();
            cernelTimer.Tick += new EventHandler(cernelTimer_Tick);
            cernelTimer.Interval = new TimeSpan(0,0,0,0,500);
            BeginUsingFrames();
            
        }

        private void BeginUsingFrames()
        {

          
            int x, y;          

            Random ran = new Random();
            x = ran.Next(10, 500);
            y = ran.Next(100, 400);

            Canvas.SetTop(ell, -100);
            Canvas.SetRight(ell, x);

            DoubleAnimationUsingKeyFrames daukTop
                = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames daukRight
                = new DoubleAnimationUsingKeyFrames();

            //top animation
            daukTop.KeyFrames.Add(
                new SplineDoubleKeyFrame(
                    (double)y, // Target value (KeyValue)
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3)), // KeyTime
                    new KeySpline(0.25, 0, 0.5, 0.7) // KeySpline
                    )
                );
            daukTop.KeyFrames.Add(
                new SplineDoubleKeyFrame(
                    SystemParameters.FullPrimaryScreenHeight + 150, // Target value (KeyValue)
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6)), // KeyTime
                    new KeySpline(0.25, 0.8, 0.2, 0.4) // KeySpline
                    )
                );
 
            //right animation
            daukRight.KeyFrames.Add(
                new SplineDoubleKeyFrame(
                    400, // Target value (KeyValue)
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6)), // KeyTime
                    new KeySpline(0.3, 0.0, 0.3, 0.0) // KeySpline
                    )
                );

            Storyboard.SetTarget(daukTop, ell);
            Storyboard.SetTargetProperty(daukTop, new PropertyPath("(Canvas.Top)"));
            story.Children.Add(daukTop);
            Storyboard.SetTarget(daukRight, ell);
            Storyboard.SetTargetProperty(daukRight, new PropertyPath("(Canvas.Right)"));
            story.Children.Add(daukRight);

            story.Completed += Story_Completed;
            story.Begin(this);
        }

        private void Story_Completed(object sender, EventArgs e)
        {
           
            ell.Fill = new LinearGradientBrush(Colors.DarkKhaki, Colors.IndianRed, 1.0);
            ell.Visibility = Visibility.Visible;

            story.Begin(this);
        }

        private void SetUpCommands()
        {
            DataContext = this;
            fireCommand = new DelegateCommand(FireRunCommand_Execute, FireRunCommand_CanExecute);
            speedUpCommand = new DelegateCommand(SpeedUpRunCommand_Execute, SpeedUpRunCommand_CanExecute);
            speedDownCommand = new DelegateCommand(SpeedDownRunCommand_Execute, SpeedDownRunCommand_CanExecute);
            upRunCommand = new DelegateCommand(UpRunCommand_Executed, UpRunCommand_CanExecute);
            downRunCommand = new DelegateCommand(DownRunCommand_Executed, DownRunCommand_CanExecute);                 
            fireCommand.GestureKey = Key.F;
            upRunCommand.GestureKey = Key.Up;
            downRunCommand.GestureKey = Key.Down;
            speedUpCommand.GestureKey = Key.V;
            speedDownCommand.GestureKey = Key.C;
            lbShow.Content = "F - fire | <- left, right ->";
            
        }

        private void cernelTimer_Tick(object sender, EventArgs e)
        {
                
            if (ker != null)
            {
                
                if (Canvas.GetLeft(ker) > this.ActualWidth && Canvas.GetTop(ker) > this.ActualHeight)
                {
                    for (int i = 0; i < MainCanvas.Children.Count; i++)
                    {
                        if (MainCanvas.Children[i] is Kernel)
                        {
                            MainCanvas.Children.RemoveAt(i);
                        }
                    }
                }
               
            }
            lb.Content = ell.Fill.ToString();
            if (ell.Fill.ToString().Trim() == "#FFFF0000")
            {
                ell.Effect = null;
                ell.Visibility = Visibility.Hidden;
            }
          
        }

        private bool SpeedDownRunCommand_CanExecute()
        {
            if (Kernel.V >= 10.0) return true;
            else return false;
        }

        private void SpeedDownRunCommand_Execute()
        {          
            Kernel.V = Kernel.V - 0.1;
            pb.Value = pb.Value - 0.1;
            lb.Content = "Speed: " + pb.Value.ToString();
            lbShow.Content = "Speed kernel: " + Kernel.V.ToString();
        }

        private bool SpeedUpRunCommand_CanExecute()
        {
            if (Kernel.V <= 30.0) return true;
            else return false;
        }

        private void SpeedUpRunCommand_Execute()
        {                  
            Kernel.V = Kernel.V + 0.1;
            pb.Value = pb.Value + 0.1;
            lb.Content = "Speed: " + pb.Value.ToString();
            lbShow.Content = "Speed kernel: " + Kernel.V.ToString();
        }

        private void FireRunCommand_Execute()
        {
            for (int i = 0; i < MainCanvas.Children.Count; i++)
            {
                if (MainCanvas.Children[i] is Kernel)
                {
                    MainCanvas.Children.RemoveAt(i);
                }
            }

            cernelTimer.Start();              
            Vector offset = VisualTreeHelper.GetOffset(gun);
            double x = offset.X + 100;
            double y = offset.Y + 120;
            ker = new Kernel();
            ker.Agr = gun.AngleCannon;
            ker.X = x;
            ker.Y = y;
            ker.Intersect += Ker_Intersect;
            ker.MoveKernel += Ker_MoveKernel;
            Canvas.SetTop(ker, y);
            Canvas.SetLeft(ker, x);
            MainCanvas.Children.Add(ker);
            ker.Start();
            lb.Content = " x: " + x + " y: " + y;

        }

        private void Ker_Intersect(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bool result;
            if (ker != null && Canvas.GetLeft(ker) < this.ActualWidth && Canvas.GetTop(ker) < this.ActualHeight)
            {
                Kernel k = (Kernel)sender;
                GeneralTransform t1 = ker.TransformToVisual(this);
                GeneralTransform t2 = ell.TransformToVisual(this);
                Rect r1 = t1.TransformBounds(new Rect() { X = 0, Y = 0, Width = ker.ActualWidth, Height = ker.ActualHeight });
                Rect r2 = t2.TransformBounds(new Rect() { X = 0, Y = 0, Width = ell.ActualWidth, Height = ell.ActualHeight });
                result = r1.IntersectsWith(r2);
                if (result)
                {
                    lb.Content = "Result: " + result.ToString();
                    ell.Fill = new SolidColorBrush(Colors.Red);
                    result = false;
                }
                else { lb.Content = "Result: " + result.ToString(); }
                
            }

        }

        private void Ker_MoveKernel(object sender, PositionEventArgs e)
        {
           
            Point position = Mouse.GetPosition(MainCanvas);
            lb.Content = "X: " + position.X + "\n" + "Y: " + position.Y;
        }

        private bool FireRunCommand_CanExecute()
        {         
            return true;
        }

        private void UpRunCommand_Executed()
        {
            --gun.AngleCannon;
        }

        private bool UpRunCommand_CanExecute()
        {
            lb.Content = " up: " + gun.AngleCannon.ToString();
            if (gun.AngleCannon >= -55) return true;
            else return false;
        }

        private void DownRunCommand_Executed()
        {
            ++gun.AngleCannon;
        }

        private bool DownRunCommand_CanExecute()
        {
            lb.Content = " down: " + gun.AngleCannon.ToString();
            if (gun.AngleCannon <= 30) return true;
            else return false;
        }
       

        private void Btnkeys_Click(object sender, RoutedEventArgs e)
        {
            String FormTitle = String.Format("\nПушка - стрелки вверх-вниз.\nОгонь - F.\nСкорость снаряда - V, C.\n");
            MessageBox.Show(FormTitle, "Keys:");
        }

        private void BtnProgram_Click(object sender, RoutedEventArgs e)
        {
            String appName = Assembly.GetEntryAssembly().GetName().Name.ToString();
            String version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            String FormTitle = String.Format("Program name: {0}\nVersion: {1} ",
                                             appName,
                                             version);
            MessageBox.Show(FormTitle, "About program:");
        }

        private void gun_Loaded(object sender, RoutedEventArgs e)
        {
            ;
        }
    }

}