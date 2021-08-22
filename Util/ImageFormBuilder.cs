using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Flow.Util
{
    using System.Drawing;
    using Image;
    public class ImageFormBuilder
    {
        internal ImageFormBuilder() { }
        public static ImageFormBuilder New => new ImageFormBuilder();
        static ImageFormBuilder()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private BasePicture picture = null;
        public ImageFormBuilder SetPicture(BasePicture value)
        {
            this.picture = value;
            return this;
        }

        private PictureBoxSizeMode sizemode = PictureBoxSizeMode.Zoom;
        public ImageFormBuilder SetSizeMode(PictureBoxSizeMode value)
        {
            this.sizemode = value;
            return this;
        }

        private Keys exitkey = Keys.Escape;
        public ImageFormBuilder SetExitKey(Keys value)
        {
            this.exitkey = value;
            return this;
        }

        private List<Action<Keys>> onkeyevents = new List<Action<Keys>>();
        public void OnKey(Action<Keys> key)
        {
            onkeyevents.Add(key);
        }

        private bool isshowing = false;
        public void Show()
        {
            if (isshowing)
                return;
            var form = new Form();

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.KeyPreview = true;

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = this.sizemode;
            form.Controls.Add(pb);

            System.Windows.Forms.Timer timer = new();
            timer.Interval = 25;
            
            form.KeyDown += (o, e) =>
            {
                foreach (var ev in onkeyevents)
                    ev(e.KeyCode);
                if (e.KeyCode == this.exitkey)
                {
                    form.Close();
                    Application.Exit();
                    Camera.Stop();
                }
            };

            form.Load += delegate
            {
                timer.Start();
            };

            timer.Tick += delegate
            {
                if (picture != null)
                {
                    Bitmap bmp = picture.ToBitmap();
                    if (bmp != null)
                        pb.Image = bmp;
                }
            };
            isshowing = true;
            Thread thread = null;
            thread = new Thread(() =>
            {
                Application.Run(form);
            });
            thread.Start();
        }
    }
}