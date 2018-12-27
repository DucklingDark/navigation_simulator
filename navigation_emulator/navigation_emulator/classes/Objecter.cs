using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navigation_emulator {
    public struct NavInfo {
        public double roll;
        public double pitch;
        public int course;
        public double speed;
        public double lat;
        public double lon;

        public NavInfo(int def = 0) {
            roll = 0.0;
            pitch = 0.0;
            course = 0;
            speed = 0;
            lat = 0;
            lon = 0;
        }
    }

    class Objecter {
        public ObjectPoint point;
        public NavInfo nav_info = new NavInfo();
    }
}
