using System;

namespace Flow.Image.Processing
{
    using Util;
    public static class FlowImageProcessingExtension
    {
        // public static R Zip<R>(this IterableFlow<BitmapPicture, R> flow, Action<BitmapPicture, R> func)
        // {
        //     flow.AddOperation(s => 
        //     {
        //         if (s is ByteProcessingPicture pp)
        //             func(pp, flow.Return);
        //         return s;
        //     });
        //     return flow.Return;
        // }

        // public static _Flow<ImageFormBuilder> SetPicture(this IterableFlow<BitmapPicture, _Flow<ImageFormBuilder>> flow)
        // {
        //     flow.AddOperation(s => 
        //     {
        //         if (s is ByteProcessingPicture bpp)
        //             flow.Return.SetPicture(bpp);
        //         else if (s is ByteGrayscaleProcessingPicture bgpp)
        //             flow.Return.SetPicture(bgpp);
        //         else if (s is FloatProcessingPicture fpp)
        //             flow.Return.SetPicture(fpp);
        //         else if (s is FloatGrayScaleProcessingPicture fgpp)
        //             flow.Return.SetPicture(fgpp);
        //         return s;
        //     });
        //     return flow.Return;
        // }

        // public static IterableFlow<BitmapPicture, R> ForPixel<R>(this IterableFlow<BitmapPicture, R> flow, Func<byte, byte, byte, (int, int, int)> operation)
        // {
        //     flow.AddOperation(p => 
        //     {
        //         if (p is BitmapPicture pic)
        //             return pic.ForPixel(operation);
        //         else if (p is ByteProcessingPicture bpp)
        //             return bpp.ForPixel(operation);
        //         return null;
        //     });
        //     return flow;
        // }
    }
}


// if (p is Picture pic)
//     return pic.ForPixel(operation);
// else if (p is ByteProcessingPicture bpp)
//     return bpp.ForPixel(operation);
// else if (p is ByteGrayscaleProcessingPicture bgpp)
//     return bgpp.ForPixel(operation);
// else if (p is FloatProcessingPicture fpp)
//     return fpp.ForPixel(operation);
// else if (p is FloatGrayScaleProcessingPicture fgpp)
//     return fgpp.ForPixel(operation);