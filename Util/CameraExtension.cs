namespace Flow.Util
{
    using Image;
    public static class CameraExtension
    {
        public static Flow<Picture, Picture, Flow<P>> Camera<P>(this Flow<P> flow)
            => Flow.WithParent(global::Flow.Util.Camera.Get(), flow);
    }
}