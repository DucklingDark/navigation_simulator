using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navigation_emulator {
    class ObjectPoint {
        public PointLatLng point;
        public GMapMarker marker;

        public ObjectPoint(MainWindow window, PointLatLng nPoint) {
            point = nPoint;

            marker = new GMapMarker(point);
            marker.Shape = new markers.ObjectMarker(window, marker, point, -1);
            marker.Offset = new System.Windows.Point(-15, -15);
            marker.ZIndex = -1;
        }

        public void Change_params(MainWindow window, PointLatLng nPoint, int nCourse = -1) {
            point = nPoint;

            marker = new GMapMarker(point);
            marker.Shape = new markers.ObjectMarker(window, marker, point, nCourse);
            marker.Offset = new System.Windows.Point(-15, -15);
            marker.ZIndex = -1;
        }
    }
}
