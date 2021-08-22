using System;
using System.Threading.Tasks;

namespace Flow.Image.Processing
{
    public static class ForPixelImageProcessing
    {
        //ByteProcessingPicture
        public static ByteProcessingPicture ForPixel(this Picture bmp, Func<byte, byte, byte, (int, int, int)> op)
        {
            if (bmp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static ByteProcessingPicture ForPixel(this ByteProcessingPicture bpp, Func<byte, byte, byte, (int, int, int)> op)
        {
            if (bpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static ByteProcessingPicture ForPixel(this ByteGrayscaleProcessingPicture bgspp, Func<float, (int, int, int)> op)
        {
            if (bgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }
        
        public static ByteProcessingPicture ForPixel(this FloatProcessingPicture fpp, Func<float, float, float, (int, int, int)> op)
        {
            if (fpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static ByteProcessingPicture ForPixel(this FloatGrayScaleProcessingPicture fgspp, Func<float, (int, int, int)> op)
        {
            if (fgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }


        //ByteGrayscaleProcessingPicture
        public static ByteGrayscaleProcessingPicture ForPixel(this Picture bmp, Func<byte, byte, byte, int> op)
        {
            if (bmp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static ByteGrayscaleProcessingPicture ForPixel(this ByteProcessingPicture bpp, Func<byte, byte, byte, int> op)
        {
            if (bpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }

        public static ByteGrayscaleProcessingPicture ForPixel(this ByteGrayscaleProcessingPicture bgspp, Func<byte, int> op)
        {
            if (bgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            Parallel.For(0, bgspp.height, j =>
            {
                int end = bgspp.width + j * bgspp.stride;
                for (int i = j * bgspp.stride; i < end; i++)
                    bgspp.data[i] = (byte)op(bgspp.data[i]);
            });
            return bgspp;
        }

        public static ByteGrayscaleProcessingPicture ForPixel(this FloatProcessingPicture fpp, Func<float, float, float, int> op)
        {   
            if (fpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }

        public static ByteGrayscaleProcessingPicture ForPixel(this FloatGrayScaleProcessingPicture fgspp, Func<float, int> op)
        {
            if (fgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }

        public static ByteGrayscaleProcessingPicture ForPixel(this Picture bmp, Func<byte, int> op)
        {
            if (bmp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");

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
        }

        
        //FloatProcessingPicture
        public static FloatProcessingPicture ForPixel(this Picture bmp, Func<byte, byte, byte, (float, float, float)> op)
        {
            if (bmp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static FloatProcessingPicture ForPixel(this ByteProcessingPicture bpp, Func<byte, byte, byte, (float, float, float)> op)
        {
            if (bpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static FloatProcessingPicture ForPixel(this ByteGrayscaleProcessingPicture bgspp, Func<byte, (float, float, float)> op)
        {
            if (bgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }
        
        public static FloatProcessingPicture ForPixel(this FloatProcessingPicture fpp, Func<float, float, float, (float, float, float)> op)
        {
            if (fpp == null || op == null)
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
        }

        public static FloatProcessingPicture ForPixel(this FloatGrayScaleProcessingPicture fgspp, Func<float, (float, float, float)> op)
        {
            if (fgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }

        
        //FloatGrayScaleProcessingPicture
        public static FloatGrayScaleProcessingPicture ForPixel(this Picture bmp, Func<byte, byte, byte, float> op)
        {
            if (bmp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }

        public static FloatGrayScaleProcessingPicture ForPixel(this ByteProcessingPicture bpp, Func<byte, byte, byte, float> op)
        {
            if (bpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }

        public static FloatGrayScaleProcessingPicture ForPixel(this ByteGrayscaleProcessingPicture bgspp, Func<byte, float> op)
        {
            if (bgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }
        
        public static FloatGrayScaleProcessingPicture ForPixel(this FloatProcessingPicture fpp, Func<float, float, float, float> op)
        {
            if (fpp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            // TODO
            throw new NotImplementedException();
        }

        public static FloatGrayScaleProcessingPicture ForPixel(this FloatGrayScaleProcessingPicture fgspp, Func<float, float> op)
        {
            if (fgspp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
            Parallel.For(0, fgspp.height, j =>
            {
                int start = j * fgspp.stride;
                for (int i = start, end = start + fgspp.width; i < end; i++)
                    fgspp.data[i] = op(fgspp.data[i]);
            });
            return fgspp;
        }

        public static FloatGrayScaleProcessingPicture ForPixel(this Picture bmp, Func<float, float> op)
        {
            if (bmp == null || op == null)
                throw new InvalidOperationException("Picture and Operation is needed.");
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
        }
    }
}