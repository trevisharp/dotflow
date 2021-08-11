using AForge.Video.DirectShow;
using System.Drawing;
using System;

namespace Flow.Util
{
    using Image;
    public static class Camera
    {
        private static int count = 0;
        public static event Action<Picture> OnFrame
        {
            add
            {
                count++;
                onframe += value;
                if (count > 0)
                    device.Start();
            }
            remove
            {
                count--;
                onframe -= value;
                if (count <= 0)
                    device.SignalToStop();
            }
        }
        public static event Action<Picture> onframe;

        private static VideoCaptureDevice device = null;
        static Camera()
        {
            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            device = new VideoCaptureDevice(devices[0].MonikerString);
            device.NewFrame += (s, e) =>
            {
                Bitmap bmp = e.Frame;
                if (onframe != null)
                    onframe(Picture.New(new Bitmap(bmp)));
            };
        }
    
        public static void Stop()
        {
            try
            {
                count = 0;
                onframe = null;
                device.SignalToStop();
                device = null;
            }
            catch { }
        }
    
        public static Flow<Picture> Get()
        {
            var flow = Flow.New(Picture.Empty);
            OnFrame += pic =>
            {
                flow.Set(pic);
            };
            return flow;
        }
    }
}