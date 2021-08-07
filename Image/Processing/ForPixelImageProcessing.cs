using System;
using System.Threading.Tasks;

namespace Flow.Image.Processing
{
    public static class ForPixelImageProcessing
    {
        public static Picture ForPixel(this Picture pic, Func<byte, byte, byte, (int, int, int)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    var pp = ByteProcessingPicture.FromPicture(bmp);
                    unsafe
                    {
                        Parallel.For(0, pp.height, j =>
                        {
                            byte* pixel = pp.p + j * pp.stride;
                            int width = pp.width * 3;
                            for (int i = 0; i < width; i += 3, pixel += 3)
                            {
                                var rgb = op(pixel[2], pixel[1], pixel[0]);
                                pixel[2] = (byte)rgb.Item1;
                                pixel[1] = (byte)rgb.Item2;
                                pixel[0] = (byte)rgb.Item3;
                            }
                        });
                    }
                    return pp;
                case ByteProcessingPicture bpp:
                    unsafe
                    {
                        Parallel.For(0, bpp.height, j =>
                        {
                            byte* pixel = bpp.p + j * bpp.stride;
                            int width = bpp.width * 3;
                            for (int i = 0; i < width; i += 3, pixel += 3)
                            {
                                var rgb = op(pixel[2], pixel[1], pixel[0]);
                                pixel[2] = (byte)rgb.Item1;
                                pixel[1] = (byte)rgb.Item2;
                                pixel[0] = (byte)rgb.Item3;
                            }
                        });
                    }
                    return bpp;
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, (int, int, int)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, byte, byte, int> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    var pp = ByteGrayscaleProcessingPicture.FromPicture(bmp);
                    unsafe
                    {
                        Parallel.For(0, pp.height, j =>
                        {
                            byte* pixel = pp.p + j * pp.stride;
                            int end = j * pp.stride + pp.width;
                            for (int i = j * pp.stride; i < end; i++, pixel += 3)
                                pp.data[i] = (byte)op(pixel[2], pixel[1], pixel[0]);
                        });
                    }
                    return pp;
                case ByteProcessingPicture bpp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, int> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, byte, byte, (float, float, float)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    var pp = FloatProcessingPicture.FromPicture(bmp);
                    unsafe
                    {
                        Parallel.For(0, pp.height, j =>
                        {
                            byte* pixel = pp.p + j * pp.stride;
                            int width = 3 * pp.width;
                            for (int i = 0; i < width; i += 3, pixel += 3)
                            {
                                int index = j * pp.stride + i;
                                var rgb = op(pixel[2], pixel[1], pixel[0]);
                                pp.data[index] = rgb.Item1;
                                pp.data[index + 1] = rgb.Item2;
                                pp.data[index + 2] = rgb.Item3;
                            }
                        });
                    }
                    return pp;
                case ByteProcessingPicture bpp:
                    var fpp = bpp.ToFloatProcessing();
                    unsafe
                    {
                        Parallel.For(0, fpp.height, j =>
                        {
                            byte* pixel = fpp.p + j * fpp.stride;
                            int width = 3 * fpp.width;
                            for (int i = 0; i < width; i += 3, pixel += 3)
                            {
                                int index = j * fpp.stride + i;
                                var rgb = op(pixel[2], pixel[1], pixel[0]);
                                fpp.data[index] = rgb.Item1;
                                fpp.data[index + 1] = rgb.Item2;
                                fpp.data[index + 2] = rgb.Item3;
                            }
                        });
                    }
                    bpp.Dispose();
                    return fpp;
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, (float, float, float)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, byte, byte, float> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    var pp = FloatGrayScaleProcessingPicture.FromPicture(bmp);
                    unsafe
                    {
                        Parallel.For(0, pp.height, j =>
                        {
                            int start = j * pp.stride;
                            byte* pixel = pp.p + start;
                            for (int i = start, end = start + pp.width; i < end; i++, pixel += 3)
                                pp.data[i] = op(pixel[2], pixel[1], pixel[0]);
                        });
                    }
                    return pp;
                case ByteProcessingPicture bpp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, float> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, float, float, (int, int, int)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case FloatProcessingPicture fpp:
                    var bpp = fpp.ToByteProcessing();
                    unsafe
                    {
                        Parallel.For(0, bpp.height, j =>
                        {
                            byte* pixel = bpp.p + j * bpp.stride;
                            int width = bpp.width * 3;
                            for (int i = 0; i < width; i += 3)
                            {
                                int index = i + j * bpp.stride;
                                var rgb = op(fpp.data[index], fpp.data[index + 1], fpp.data[index + 2]);
                                pixel[2] = (byte)rgb.Item1;
                                pixel[1] = (byte)rgb.Item2;
                                pixel[0] = (byte)rgb.Item3;
                            }
                        });
                    }
                    fpp.Dispose();
                    return bpp;
                default:
                    throw new InvalidOperationException($"Func<float, float, float, (int, int, int)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, float, float, int> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case FloatProcessingPicture fpp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, int> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, float, float, (float, float, float)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case FloatProcessingPicture fpp:
                    Parallel.For(0, fpp.height, j =>
                    {
                        int width = fpp.width * 3;
                        for (int i = 0; i < width; i += 3)
                        {
                            int index = i + j * fpp.stride;
                            var rgb = op(fpp.data[index], fpp.data[index + 1], fpp.data[index + 2]);
                            fpp.data[index] = rgb.Item1;
                            fpp.data[index + 1] = rgb.Item2;
                            fpp.data[index + 2] = rgb.Item3;
                        }
                    });
                    return fpp;
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, (float, float, float)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, float, float, float> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case FloatProcessingPicture fpp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, byte, byte, float> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, (int, int, int)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case ByteGrayscaleProcessingPicture bgspp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, (int, int, int)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, int> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    var pp = ByteGrayscaleProcessingPicture.FromPicture(bmp);
                    unsafe
                    {
                        Parallel.For(0, pp.height, j =>
                        {
                            byte* pixel = pp.p + j * pp.stride;
                            int end = pp.width + j * pp.stride;
                            for (int i = j * pp.stride; i < end; i++, pixel += 3)
                                pp.data[i] = (byte)op((byte)((299 * pixel[2] + 587 * pixel[1] + 114 * pixel[0]) / 1000));
                        });
                    }
                    return pp;
                case ByteGrayscaleProcessingPicture bgspp:
                    Parallel.For(0, bgspp.height, j =>
                    {
                        int end = bgspp.width + j * bgspp.stride;
                        for (int i = j * bgspp.stride; i < end; i++)
                            bgspp.data[i] = (byte)op(bgspp.data[i]);
                    });
                    return bgspp;
                default:
                    throw new InvalidOperationException($"Func<byte, int> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, (float, float, float)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case ByteGrayscaleProcessingPicture bgspp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, (float, float, float)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<byte, float> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case ByteGrayscaleProcessingPicture bgspp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<byte, float> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, (int, int, int)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case ByteGrayscaleProcessingPicture bgspp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<float, (int, int, int)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, int> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case ByteGrayscaleProcessingPicture bgspp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<float, int> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, (float, float, float)> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    return null; //TODO
                case ByteGrayscaleProcessingPicture bgspp:
                    return null; //TODO
                default:
                    throw new InvalidOperationException($"Func<float, (float, float, float)> is invalid to {pic.GetType()}");
            }
        }

        public static Picture ForPixel(this Picture pic, Func<float, float> op)
        {
            if (pic == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            switch (pic)
            {
                case BitmapPicture bmp:
                    var pp = FloatGrayScaleProcessingPicture.FromPicture(bmp);
                    unsafe
                    {
                        Parallel.For(0, pp.height, j =>
                        {
                            int start = j * pp.stride;
                            byte* pixel = pp.p + start;
                            for (int i = start, end = start + pp.width; i < end; i++, pixel += 3)
                                pp.data[i] = op((byte)((299 * pixel[2] + 587 * pixel[1] + 114 * pixel[0]) / 1000));
                        });
                    }
                    return pp;
                case FloatGrayScaleProcessingPicture fgspp:
                    Parallel.For(0, fgspp.height, j =>
                    {
                        int start = j * fgspp.stride;
                        for (int i = start, end = start + fgspp.width; i < end; i++)
                            fgspp.data[i] = op(fgspp.data[i]);
                    });
                    return fgspp;
                default:
                    throw new InvalidOperationException($"Func<float, float> is invalid to {pic.GetType()}");
            }
        }
        }
}