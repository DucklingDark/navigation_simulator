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
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace navigation_emulator {
    public partial class MainWindow : Window {
        private bool mouse_on_map   = false;
        private bool full_screen    = false;
        private bool mouse_down     = false;
        private int point_to_change = -1;
        const double start_lat      = 59.9500679;
        const double start_lon      = 30.3166866;

        private PointLatLng mouse_point;
        public Mapper mapper;

        private AdjForm adjform;

        public MainWindow() {
            InitializeComponent();
            Load_map(main_map);
            mapper = new Mapper(this, new PointLatLng(start_lat, start_lon));
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
        }

        #region --- MENU ---
        private void Menu_find_ports(object sender, RoutedEventArgs e) {

        }

        private void Menu_com_connect(object sender, RoutedEventArgs e) {

        }

        private void Menu_adj_form_Click(object sender, RoutedEventArgs e) {
            if (adjform != null) {
                adjform.Close();
            }

            adjform = new AdjForm(mapper) {
                Left = this.Left + 225,
                Top = this.Top + 120
            };
            adjform.Show();
        }

        private void Menu_full_screen(object sender, RoutedEventArgs e) {
            if (full_screen == false) {
                main_window.WindowState = WindowState.Maximized;
                btn_menu_fullscreen.Header = "Оконный режим";
                full_screen = true;
            } else {
                main_window.WindowState = WindowState.Normal;
                btn_menu_fullscreen.Header = "Полноэкранный режим";
                full_screen = false;
            }
        }

        private void Left_btn_mouse(object sender, MouseButtonEventArgs e) {
            if (mouse_on_map == false) {
                base.OnMouseLeftButtonDown(e);
                this.DragMove();
            }
        }

        private void Menu_close(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
        #region --- MAP ---
        private void Load_map(GMap.NET.WindowsPresentation.GMapControl map) {
            //map.CacheLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            map.MapProvider = GMap.NET.MapProviders.GoogleChinaSatelliteMapProvider.Instance;
            map.MinZoom = 11;
            map.MaxZoom = 20;
            map.Zoom = 17;
            map.CanDragMap = true;
            map.DragButton = MouseButton.Left;
            map.Position = new PointLatLng(start_lat, start_lon);
        }

        private void Map_mouse_enter(object sender, MouseEventArgs e) {
            mouse_on_map = true;
        }

        private void Map_mouse_leave(object sender, MouseEventArgs e) {
            mouse_on_map = false;
        }

        private void Map_loaded(object sender, RoutedEventArgs e) {

        }

        private void Map_preview_mouse_wheel(object sender, MouseWheelEventArgs e) {

        }

        private void Map_mouse_move(object sender, MouseEventArgs e) {
            main_map.Focus();
            Point p = e.GetPosition(main_map);

            mouse_point = main_map.FromLocalToLatLng((int)p.X, (int)p.Y);
            lb_map_lat.Content = Convert.ToString(Math.Round(mouse_point.Lat, 7));
            lb_map_lon.Content = Convert.ToString(Math.Round(mouse_point.Lng, 7));

            mapper.Check_points(mouse_down, mouse_point, e);
            /*if (mouse_down == false) {
                point_to_change = -1;
                marker_to_change = null;
            }

            if (pointer.object_point.marker.ZIndex == 1) {
                marker_to_change = pointer.object_point.marker;
                point_to_change = 0;
            }

            lb_map_changing.Content = pointer.object_point.marker.ZIndex;

            if (e.LeftButton == MouseButtonState.Pressed && point_to_change != -1) {
                main_map.CanDragMap = false;

                mouse_point.Lat = Math.Round(mouse_point.Lat, 7);
                mouse_point.Lng = Math.Round(mouse_point.Lng, 7);

                pointer.object_point.marker.Position = mouse_point;
                pointer.object_point.Change_params(this, mouse_point);
            } else {
                main_map.CanDragMap = true;
            }*/
        }

        private void Map_mouse_down(object sender, MouseButtonEventArgs e) {
            mouse_down = true;
        }

        private void Map_mouse_up(object sender, MouseButtonEventArgs e) {
            mapper.Repaint();
            mouse_down = false;
        }

        private void Map_mouse_right_button_down(object sender, MouseButtonEventArgs e) {

        }

        private void Map_clear_points_click(object sender, RoutedEventArgs e) {

        }

        private void Map_add_waypoint_click(object sender, RoutedEventArgs e) {

        }
        #endregion
    }
}
