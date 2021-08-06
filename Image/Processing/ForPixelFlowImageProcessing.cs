using System;

namespace Flow.Image.Processing
{
    public static class ForPixelFlowImageProcessing
    {
        public static IterableFlow<Picture, R> ForPixel<R>(this IterableFlow<Picture, R> flow, Func<byte, byte, byte, (int, int, int)> operation)
        {
            flow.AddOperation((Picture p) => p.ForPixel(operation));
            return flow;
        }
    }
}