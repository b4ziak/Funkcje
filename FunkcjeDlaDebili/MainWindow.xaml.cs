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

namespace FunkcjeDlaDebili
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        protected int itteration;
        protected double zakresStart;
        protected double zakresEnd;
        protected int round;
        public MainWindow()
        {
            InitializeComponent();
        }

        public double calcFormula(double x)
        {
            //double wynik = x * x + 2 * x + 1;
            double wynik = (900 - x) * Math.Pow(Math.E, -1 * (x / 100)) + Math.Sin(x/100);
            //double wynik = -x * x + 1000;
            //double wynik = Math.Pow(x, Math.Sin(x)) - 2;

            return wynik;
        }

        public bool setZakres(double start, double end)
        {
            if(end > start)
            {
                this.zakresEnd = end;
                this.zakresStart = start;
                return true;
            }
            else if(end < start)
            {
                this.zakresStart = end;
                this.zakresEnd = start;
                return true;
            }

            return false;
        }

        public bool setRound(int n)
        {
            this.round = n;
            return true;
        }

        public double calculate()
        {
            bool znaleziono = false;

            double start = this.zakresStart;
            double end = this.zakresEnd;
            bool normalDir = true;
            this.itteration = 0;

            if (calcFormula(start) < 0)
            {
                normalDir = true;
            }
            if(calcFormula(start) > 0)
            {
                normalDir = false;
            }

            double value = 000.0000;
            while(!znaleziono)
            {
                this.itteration++;
                if (this.itteration > 10000) { lblError.Content = "Za dużo itteracji (10000)"; break; }

                value = (start + end) / 2;
                if(Math.Round(calcFormula(value), this.round) == 0)
                {
                    znaleziono = true;
                }

                if(calcFormula(value) > 0)
                {
                    if(normalDir)
                    {
                        end = value;
                    }
                    else
                    {
                        start = value;
                    }
                }

                if (calcFormula(value) < 0)
                {
                    if (normalDir)
                    {
                        start = value;
                    }
                    else
                    {
                        end = value;
                    }
                }
            }
            return value;
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            if (!int.TryParse(txtRound.Text, out round))
            {
                lblError.Content = "Coś źle z dokładnością";
            }
            else if(round < 0 || round > 15)
            {
                lblError.Content = "Dokładność musi być pomiędzy 0 a 15";
            }
            else
            {
                lblResult.Content = "Trwa obliczanie...";
                setZakres(800, 1200);
                lblResult.Content = calculate().ToString();
                lblItteration.Content = this.itteration.ToString();
            }
        }
    }
}