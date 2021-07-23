using System.Drawing;

namespace Sharp.Image
{
    public class Picture
    {
        public Picture(int width, int height)
            => this.bmp = new Bitmap(width, height);

        public Picture(Bitmap bmp)
            => this.bmp = bmp;
        
        private Bitmap bmp;

        public void Show()
        {
            FormBuilder.Builder
                .SetPicture(this)
                .Show();
        }

        public static implicit operator Bitmap(Picture pic)
            => pic.bmp;
        public static implicit operator Picture(Bitmap bmp)
            => new Picture(bmp);
    }
}