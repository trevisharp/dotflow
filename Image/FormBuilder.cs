using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sharp.Image
{
    public class FormBuilder
    {
        private FormBuilder() { }
        public static FormBuilder Builder => new FormBuilder();
        static FormBuilder()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private Picture picture = null;
        public FormBuilder SetPicture(Picture value)
        {
            this.picture = value;
            return this;
        }

        private PictureBoxSizeMode sizemode = PictureBoxSizeMode.Zoom;
        public FormBuilder SetSizeMode(PictureBoxSizeMode value)
        {
            this.sizemode = value;
            return this;
        }

        private Keys exitkey = Keys.Escape;
        public FormBuilder SetExitKey(Keys value)
        {
            this.exitkey = value;
            return this;
        }

        public void Show()
        {
            var form = new Form();

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.KeyPreview = true;

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = this.sizemode;
            form.Controls.Add(pb);
            
            form.KeyDown += (o, e) =>
            {
                if (e.KeyCode == this.exitkey)
                    form.Close();
            };

            form.Load += delegate
            {
                if (picture != null)
                    pb.Image = picture;
            };

            Application.Run(form);
        }
    }
}