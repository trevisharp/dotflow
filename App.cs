using System;
using System.Windows.Forms;

namespace Flow
{
    using Util;
    using Image;
    using Image.Processing;
    public static class App
    {
        public static Flow<ImageFormBuilder> Image
            => Flow.New(ImageFormBuilder.New);

        public static Flow<ImageFormBuilder> OnKey(this Flow<ImageFormBuilder> flow, Action act)
            => Flow.From(flow, builder =>
            {
                builder.OnKey(k => act());
                return builder;
            });
        
        public static Flow<ImageFormBuilder> OnKey(this Flow<ImageFormBuilder> flow, Action<Keys> act)
            => Flow.From(flow, builder =>
            {
                builder.OnKey(act);
                return builder;
            });
        
        public static Flow<ImageFormBuilder> OnKey(this Flow<ImageFormBuilder> flow, Keys keys, Action act)
            => Flow.From(flow, builder =>
            {
                builder.OnKey(k => 
                {
                    if (k == keys)
                        act();
                });
                return builder;
            });
    
        public static Flow<ImageFormBuilder> Show(this Flow<ImageFormBuilder> flow)
            => Flow.From(flow, builder =>
            {
                builder.Show();
                return builder;
            });

        public static Flow<ImageFormBuilder> SetPicture(this Flow<ImageFormBuilder> flow, BitmapPicture picture)
            => Flow.From(flow, builder =>
            {
                builder.SetPicture(picture);
                return builder;
            });

        public static Flow<T, T, Flow<ImageFormBuilder>> OnKey<T, I>(this Flow<T, I, Flow<ImageFormBuilder>> flow, Action act)
            => Flow.From(flow, x =>
            {
                flow.Parent.State.OnKey(k => act());
                return x;
            });

        public static Flow<T, T, Flow<ImageFormBuilder>> OnKey<T, I>(this Flow<T, I, Flow<ImageFormBuilder>> flow, Action<Keys> act)
            => Flow.From(flow, x =>
            {
                flow.Parent.State.OnKey(act);
                return x;
            });

        public static Flow<T, T, Flow<ImageFormBuilder>> OnKey<T, I>(this Flow<T, I, Flow<ImageFormBuilder>> flow, Keys keys, Action act)
            => Flow.From(flow, x =>
            {
                flow.Parent.State.OnKey(k => 
                {
                    if (k == keys)
                        act();
                });
                return x;
            });
        
        public static Flow<T, T, Flow<ImageFormBuilder>> SetPicture<T, I>(this Flow<T, I, Flow<ImageFormBuilder>> flow, BitmapPicture picture)
            => Flow.From(flow, x =>
            {
                flow.Parent.State.SetPicture(picture);
                return x;
            });
        
        public static Flow<T, T, Flow<ImageFormBuilder>> SetPicture<T, I>(this Flow<T, I, Flow<ImageFormBuilder>> flow, Func<T, BitmapPicture> getpic)
            => Flow.From(flow, x =>
            {
                flow.Parent.State.SetPicture(getpic(x));
                return x;
            });
        
        public static Flow<T, T, Flow<ImageFormBuilder>> Show<T, I>(this Flow<T, I, Flow<ImageFormBuilder>> flow)
            => Flow.From(flow, x =>
            {
                flow.Parent.State.Show();
                return x;
            });
    }
}