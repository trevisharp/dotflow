using System;

namespace Flow.Image.Processing
{
    using Util;
    public static class FlowImageProcessingExtension
    {
        public static R Zip<R>(this IterableFlow<Picture, R> flow, Action<Picture, R> func)
        {
            flow.AddOperation(s => 
            {
                if (s is ByteProcessingPicture pp)
                    func(pp, flow.Return);
                return s;
            });
            return flow.Return;
        }

        public static Flow<ImageFormBuilder> SetPicture(this IterableFlow<Picture, Flow<ImageFormBuilder>> flow)
        {
            flow.AddOperation(s => 
            {
                if (s is ByteProcessingPicture bpp)
                    flow.Return.SetPicture(bpp);
                else if (s is ByteGrayscaleProcessingPicture bgpp)
                    flow.Return.SetPicture(bgpp);
                else if (s is FloatProcessingPicture fpp)
                    flow.Return.SetPicture(fpp);
                else if (s is FloatGrayScaleProcessingPicture fgpp)
                    flow.Return.SetPicture(fgpp);
                return s;
            });
            return flow.Return;
        }

        public static IterableFlow<Picture, R> ForPixel<R>(this IterableFlow<Picture, R> flow, Func<byte, byte, byte, (int, int, int)> operation)
        {
            flow.AddOperation(p => 
            {
                Picture pic = p as Picture;
                return pic.ForPixel(operation);
            });
            return flow;
        }
    }
}