using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfCanvas
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public double RoomArea { get; set; }
        public int RoomFloor { get; set; }
        public Color RoomColor { get; set; }
    }
}
