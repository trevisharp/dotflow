namespace Flow.Util
{
    using Image;
    public static class CameraExtension
    {
        public static IterableFlow<Picture, Flow<R>> Camera<R>(this Flow<R> flow)
        {
            var it = new IterableFlow<Picture, Flow<R>>(flow);
            global::Flow.Util.Camera.OnFrame += p =>
            {
                it.SetState(p);
            };
            return it;
        }
    }
}