using System;

namespace Flow.Image.Processing
{
    public static class PictureFlowExtension
    {
        public static Flow<Picture, Picture, Flow<Picture, Picture, P>> Copy<P>(this Flow<Picture, Picture, P> flow)
            => Flow.WithParent(
                Flow.From(flow, p => p.Copy()), 
                flow);

        public static Flow<ByteProcessingPicture, Picture> ForPixel(this Flow<Picture> flow, 
            Func<byte, byte, byte, (int, int, int)> op) => Flow.From(flow, p => p.ForPixel(op));
        
        public static Flow<ByteProcessingPicture, Picture, P> ForPixel<T, P>(this Flow<Picture, T, P> flow, 
            Func<byte, byte, byte, (int, int, int)> op) => Flow.From(flow, p => p.ForPixel(op));
    }
}