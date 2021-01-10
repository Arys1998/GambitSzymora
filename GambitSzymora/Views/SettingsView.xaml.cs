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
using GambitSzymora.Models;

namespace GambitSzymora.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {

        public SettingsView()
        {
            InitializeComponent();
            cmbColors.ItemsSource = typeof(Colors).GetProperties();

        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            if (!(cmbColors.SelectedItem is null))
            {
                var color = (cmbColors.SelectedItem as System.Reflection.PropertyInfo).Name;
                SolidColorBrush backgourdColor = (SolidColorBrush)new BrushConverter().ConvertFromString(color);

                Style backgroudStyle = new Style
                {
                    TargetType = typeof(Grid)
                };

                backgroudStyle.Setters.Add(new Setter(Grid.BackgroundProperty, backgourdColor));
                Application.Current.Resources["BackgroundColor"] = backgroudStyle;
            }


            Style resolution = new Style
            {
                TargetType = typeof(Window)
            };

            Style resolutionControl = new Style
            {
                TargetType = typeof(UserControl)
            };

            if (!(Width.SelectedItem is null))
            {
                var width = (Width.SelectedItem as System.Windows.Controls.ComboBoxItem).Content;
                resolution.Setters.Add(new Setter(Window.WidthProperty, Convert.ToDouble(width)));
                resolutionControl.Setters.Add(new Setter(UserControl.WidthProperty, Convert.ToDouble(width)));

                Application.Current.MainWindow.Width = Convert.ToDouble(width);

            }

            if (!(Height.SelectedItem is null))
            {
                var height = (Height.SelectedItem as System.Windows.Controls.ComboBoxItem).Content;

                resolution.Setters.Add(new Setter(Window.WidthProperty, Convert.ToDouble(height)));

                resolutionControl.Setters.Add(new Setter(UserControl.HeightProperty, Convert.ToDouble(height)));

                Application.Current.MainWindow.Height = Convert.ToDouble(height);
            }

            Application.Current.Resources["Resolution"] = resolution;
            Application.Current.Resources["ResolutionControl"] = resolutionControl;

        }
    }
}
