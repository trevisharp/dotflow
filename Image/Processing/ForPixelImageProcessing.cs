using System;
using System.Threading.Tasks;

namespace Sharp.Image.Processing
{
    public static class ForPixelImageProcessing
    {
        public static ByteProcessingPicture ForPixel(this Picture picture, Func<byte, byte, byte, (int, int, int)> operation)
        {
            var pp = ByteProcessingPicture.FromPicture(picture);
            unsafe
            {
                Parallel.For(0, pp.height, j =>
                {
                    byte* pixel = pp.p + j * pp.stride;
                    int width = pp.width * 3;
                    for (int i = 0; i < width; i += 3, pixel += 3)
                    {
                        var rgb = operation(pixel[2], pixel[1], pixel[0]);
                        pixel[2] = (byte)rgb.Item1;
                        pixel[1] = (byte)rgb.Item2;
                        pixel[0] = (byte)rgb.Item3;
                    }
                });
            }
            return pp;
        }

        public static ByteProcessingPicture ForPixel(this ByteProcessingPicture pp, Func<byte, byte, byte, (int, int, int)> operation)
        {
            unsafe
            {
                Parallel.For(0, pp.height, j =>
                {
                    byte* pixel = pp.p + j * pp.stride;
                    int width = pp.width * 3;
                    for (int i = 0; i < width; i += 3, pixel += 3)
                    {
                        var rgb = operation(pixel[2], pixel[1], pixel[0]);
                        pixel[2] = (byte)rgb.Item1;
                        pixel[1] = (byte)rgb.Item2;
                        pixel[0] = (byte)rgb.Item3;
                    }
                });
            }
            return pp;
        }

        public static FloatProcessingPicture ForPixel(this Picture picture, Func<byte, byte, byte, (float, float, float)> operation)
        {
            var pp = FloatProcessingPicture.FromPicture(picture);

            unsafe
            {
                Parallel.For(0, pp.height, j =>
                {
                    byte* pixel = pp.p + j * pp.stride;
                    int width = 3 * pp.width;
                    for (int i = 0; i < width; i += 3, pixel += 3)
                    {
                        int index = j * pp.stride + i;
                        var rgb = operation(pixel[2], pixel[1], pixel[0]);
                        pp.data[index] = rgb.Item1;
                        pp.data[index + 1] = rgb.Item2;
                        pp.data[index + 2] = rgb.Item3;
                    }
                });
            }

            return pp;
        }

        public static FloatProcessingPicture ForPixel(this FloatProcessingPicture pp, Func<float, float, float, (float, float, float)> operation)
        {
            Parallel.For(0, pp.height, j =>
            {
                int width = pp.width * 3;
                for (int i = 0; i < width; i += 3)
                {
                    int index = i + j * pp.stride;
                    var rgb = operation(pp.data[index], pp.data[index + 1], pp.data[index + 2]);
                    pp.data[index] = rgb.Item1;
                    pp.data[index + 1] = rgb.Item2;
                    pp.data[index + 2] = rgb.Item3;
                }
            });
            return pp;
        }

        public static FloatProcessingPicture ForPixel(this ByteProcessingPicture pp, Func<byte, byte, byte, (float, float, float)> operation)
        {
            var fpp = pp.ToFloatProcessing();

            unsafe
            {
                Parallel.For(0, fpp.height, j =>
                {
                    byte* pixel = fpp.p + j * fpp.stride;
                    int width = 3 * fpp.width;
                    for (int i = 0; i < width; i += 3, pixel += 3)
                    {
                        int index = j * fpp.stride + i;
                        var rgb = operation(pixel[2], pixel[1], pixel[0]);
                        fpp.data[index] = rgb.Item1;
                        fpp.data[index + 1] = rgb.Item2;
                        fpp.data[index + 2] = rgb.Item3;
                    }
                });
            }

            fpp.Dispose();

            return fpp;
        }

        public static ByteProcessingPicture ForPixel(this FloatProcessingPicture pp, Func<float, float, float, (byte, byte, byte)> operation)
        {
            var bpp = pp.ToByteProcessing();

            unsafe
            {
                Parallel.For(0, bpp.height, j =>
                {
                    byte* pixel = bpp.p + j * bpp.stride;
                    int width = bpp.width * 3;
                    for (int i = 0; i < width; i += 3)
                    {
                        int index = i + j * bpp.stride;
                        var rgb = operation(pp.data[index], pp.data[index + 1], pp.data[index + 2]);
                        pixel[2] = (byte)rgb.Item1;
                        pixel[1] = (byte)rgb.Item2;
                        pixel[0] = (byte)rgb.Item3;
                    }
                });
            }

            pp.Dispose();

            return bpp;
        }
    
        public static ByteGrayscaleProcessingPicture ForPixel(this Picture picture, Func<byte, byte, byte, byte> operation)
        {
            var pp = ByteGrayscaleProcessingPicture.FromPicture(picture);

            unsafe
            {
                Parallel.For(0, pp.height, j =>
                {
                    byte* pixel = pp.p + j * pp.stride;
                    int width = pp.width;
                    for (int i = 0; i < width; i++, pixel += 3)
                        pp.data[i] = operation(pixel[2], pixel[1], pixel[0]);
                });
            }

            return pp;
        }

        public static ByteGrayscaleProcessingPicture ToGrayscale(this Picture picture)
        {
            var pp = ByteGrayscaleProcessingPicture.FromPicture(picture);

            unsafe
            {
                Parallel.For(0, pp.height, j =>
                {
                    byte* pixel = pp.p + j * pp.stride;
                    int width = pp.width;
                    for (int i = 0; i < width; i++, pixel += 3)
                        pp.data[i] = (byte)((299 * pixel[2] + 587 * pixel[1] + 114 * pixel[0]) / 1000);
                });
            }

            return pp;
        }
    }
}