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
using System.Windows.Shapes;

namespace navigation_emulator {
    /// <summary>
    /// Interaction logic for AdjForm.xaml
    /// </summary>
    public partial class AdjForm : Window {
        private bool on_slider = false;
        private bool start = false;
        private Mapper mapper;
        public NavInfo nav_info;

        public AdjForm(Mapper map) {
            InitializeComponent();
            start = true;
            mapper = map;
            nav_info = new NavInfo();
            slider_kren.Value = 0;
            slider_tang.Value = 0;
            slider_course.Value = 0;
            slider_speed.Value = 0;
            slider_lat.Value = 59.9500679;
            slider_lon.Value = 30.3166866;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!on_slider) {
                base.OnMouseLeftButtonDown(e);

                this.DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Slider_value_changed(object sender, RoutedPropertyChangedEventArgs<double> e) {
            double val = Math.Round(e.NewValue, 2);

            if (start) {
                if (val > 0) {
                    ((Slider)sender).SelectionEnd = val;
                } else {
                    if (val < 0) {
                        ((Slider)sender).SelectionStart = val;
                    }
                }

                switch (((Slider)sender).Name) {
                    case "slider_kren":
                        lb_kren.Content = val;
                        nav_info.pitch = val;
                        break;
                    case "slider_tang":
                        lb_tang.Content = val;
                        nav_info.roll = val;
                        break;
                    case "slider_course":
                        lb_course.Content = (int)val;
                        nav_info.course = (int)val;
                        break;
                    case "slider_speed":
                        lb_speed.Content = val;
                        nav_info.speed = val;
                        break;
                    case "slider_lat":
                        val = Math.Round(e.NewValue, 7);
                        ((Slider)sender).SelectionEnd = val;
                        lb_lat.Content = val;
                        nav_info.lat = val;
                        break;
                    case "slider_lon":
                        val = Math.Round(e.NewValue, 7);
                        ((Slider)sender).SelectionEnd = val;
                        lb_lon.Content = val;
                        nav_info.lon = val;
                        break;
                }

                mapper.Change_object_nav(nav_info);
            }
        }
    }
}
