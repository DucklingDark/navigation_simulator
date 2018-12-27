using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace navigation_emulator {
    public class Mapper {
        private Objecter obj;
        private List<MissionPoint> mission_points;
        private MainWindow window;
        private GMapMarker marker_to_change;
        private int point_to_change = -1;
        private string mode = "object";

        public Mapper(MainWindow main_window, PointLatLng start_point) {
            obj = new Objecter();
            window = main_window;
            obj.point = new ObjectPoint(window, start_point);
            mission_points = new List<MissionPoint>();
            Repaint();
        }

        public void Repaint() {
            switch (mode) {
                case "all":
                    window.main_map.Markers.Clear();
                    break;
                case "object":
                    if (window.main_map.Markers.Count < 1) {
                        window.main_map.Markers.Add(obj.point.marker);
                    } else {
                        window.main_map.Markers[0] = obj.point.marker;
                    }
                    break;
                case "mission":
                    break;
            }
        }

        public int Check_points(bool mouse_down, PointLatLng mouse_point, MouseEventArgs e) {
            if (mouse_down == false) {
                point_to_change = -1;
                marker_to_change = null;
            }

            if (obj.point.marker.ZIndex == 1) {
                marker_to_change = obj.point.marker;
                mode = "object";
                point_to_change = 0;
            }

            if (e.LeftButton == MouseButtonState.Pressed && point_to_change != -1) {
                window.main_map.CanDragMap = false;

                mouse_point.Lat = Math.Round(mouse_point.Lat, 7);
                mouse_point.Lng = Math.Round(mouse_point.Lng, 7);

                if (mode == "object") {
                    obj.point.marker.Position = mouse_point;
                    obj.point.Change_params(window, mouse_point);
                }
            } else {
                window.main_map.CanDragMap = true;
            }

            return point_to_change;
        }

        public void Change_object_nav(NavInfo nav) {
            ((markers.ObjectMarker)obj.point.marker.Shape).Change_nav(nav);
            obj.nav_info.course = nav.course;
            Repaint();
        }
    }
}
