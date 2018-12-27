using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace navigation_emulator.markers {
    public partial class ObjectMarker : UserControl {
        GMapMarker marker;
        MainWindow window;

        public ObjectMarker(MainWindow nWindow, GMapMarker nMarker, PointLatLng nPoint, int nCourse) {
            InitializeComponent();

            window = nWindow;
            marker = nMarker;

            lb_lat.Content = "Широта: " + Math.Round(nPoint.Lat, 8).ToString();
            lb_lon.Content = "Долгота: " + Math.Round(nPoint.Lng, 8).ToString();
            lb_course.Content = "Курс: " + 0;

            if (nCourse != -1) {
                course_marker.Angle = nCourse;
            }

            Loaded += new RoutedEventHandler(Obj_loaded);
            SizeChanged += new SizeChangedEventHandler(Obj_size_changed);
            MouseEnter += new MouseEventHandler(Obj_mouse_enter);
            MouseLeave += new MouseEventHandler(Obj_mouse_leave);
            MouseMove += new MouseEventHandler(Obj_mouse_move);
            MouseLeftButtonUp += new MouseButtonEventHandler(Obj_mouse_left_btn_up);
            MouseLeftButtonDown += new MouseButtonEventHandler(Obj_mouse_left_btn_down);

            object_popup.Placement = PlacementMode.Mouse;
        }

        void Obj_loaded(object sender, RoutedEventArgs e) {
            if (object_round_Icon.Source.CanFreeze)
                object_round_Icon.Source.Freeze();
        }

        void Obj_size_changed(object sender, SizeChangedEventArgs e) {
            marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        }

        void Obj_mouse_move(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured) {
                Point p = e.GetPosition(window.main_map);
                marker.Position = window.main_map.FromLocalToLatLng((int)p.X, (int)p.Y);
                object_popup.IsOpen = false;
            }
        }

        void Obj_mouse_left_btn_down(object sender, MouseButtonEventArgs e) {
            if (!IsMouseCaptured)
                Mouse.Capture(this);
        }

        void Obj_mouse_left_btn_up(object sender, MouseButtonEventArgs e) {
            if (IsMouseCaptured)
                Mouse.Capture(null);
        }

        void Obj_mouse_leave(object sender, MouseEventArgs e) {
            marker.ZIndex = -1;
            object_popup.IsOpen = false;
        }

        void Obj_mouse_enter(object sender, MouseEventArgs e) {
            marker.ZIndex = 1;
            object_popup.IsOpen = true;
        }

        public void Change_nav(NavInfo nav) {
            course_marker.Angle = nav.course;
            lb_lat.Content = "Широта: " + nav.lat.ToString();
            lb_lon.Content = "Долгота: " + nav.lon.ToString();
            lb_course.Content = "Курс: " + nav.course;
        }
    }
}
