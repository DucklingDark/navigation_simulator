using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace navigation_emulator.points {
    class Pointer {
        public ObjectPoint object_point;
        public List<MissionPoint> mission_points;
        MainWindow window;
        public GMapMarker marker_to_change;
        private int point_to_change = -1;
        private string mode = "object";

        public Pointer(MainWindow main_window, PointLatLng start_point) {
            window = main_window;
            object_point = new ObjectPoint(window, start_point);
            mission_points = new List<MissionPoint>();
            Repaint(main_window.main_map);
        }

        public void Repaint(GMapControl map) {
            switch (mode) {
                case "all":
                    map.Markers.Clear();
                    break;
                case "object":
                    if (map.Markers.Count < 1) {
                        map.Markers.Add(object_point.marker);
                    } else {
                        map.Markers[0] = object_point.marker;
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

            if (object_point.marker.ZIndex == 1) {
                marker_to_change = object_point.marker;
                mode = "object";
                point_to_change = 0;
            }

            //lb_map_changing.Content = pointer.object_point.marker.ZIndex;

            if (e.LeftButton == MouseButtonState.Pressed && point_to_change != -1) {
                window.main_map.CanDragMap = false;

                mouse_point.Lat = Math.Round(mouse_point.Lat, 7);
                mouse_point.Lng = Math.Round(mouse_point.Lng, 7);

                object_point.marker.Position = mouse_point;
                object_point.Change_params(window, mouse_point);
            } else {
                window.main_map.CanDragMap = true;
            }

            return point_to_change;
        }
    }
}
