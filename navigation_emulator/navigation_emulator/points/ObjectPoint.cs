using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navigation_emulator.points {
    class ObjectPoint {
        public PointLatLng point;
        public GMapMarker marker;

        public ObjectPoint(MainWindow window, PointLatLng nPoint) {
            point = nPoint;

            marker = new GMapMarker(point);
            marker.Shape = new markers.ObjectMarker(window, marker, point);
            marker.Offset = new System.Windows.Point(-15, -15);
            marker.ZIndex = -1;
        }

        public void Change_params(MainWindow window, PointLatLng nPoint) {
            point = nPoint;

            marker = new GMapMarker(point);
            marker.Shape = new markers.ObjectMarker(window, marker, point);
            marker.Offset = new System.Windows.Point(-15, -15);
            marker.ZIndex = -1;
        }
    }
}
