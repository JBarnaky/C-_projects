using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_paint
{
    public class Props: INotifyPropertyChanged
    {
        public Props()
        {

        }

        public Props(string lineColor, string fillColor, int lineThickness, double x, double y)
        {
            LineColor = lineColor;
            FillColor = fillColor;
            LineThickness = lineThickness;
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string lineColor;

        public string LineColor
        {
            get { return lineColor; }
            set
            {
                lineColor = value;
                OnPropertyChanged(nameof(LineColor));
            }
        }

        private string fillColor;

        public string FillColor
        {
            get { return fillColor; }
            set
            {
                fillColor = value;
                OnPropertyChanged(nameof(FillColor));
            }
        }

        private int lineThickness;

        public int LineThickness
        {
            get { return lineThickness; }
            set
            {
                lineThickness = value;
                OnPropertyChanged(nameof(LineThickness));

            }
        }

    }
}
