using System;
using System.Windows.Forms;

namespace Flow.Util
{
    using Image;
    public static class App
    {
        public static Flow<ImageFormBuilder> Image
            => new Flow<ImageFormBuilder>(new ImageFormBuilder());

        public static Flow<ImageFormBuilder> OnKey(this Flow<ImageFormBuilder> flow, Action act)
        {
            flow.State.OnKey(key => act());
            return flow;
        }
        
        public static Flow<ImageFormBuilder> OnKey(this Flow<ImageFormBuilder> flow, Action<Keys> act)
        {
            flow.State.OnKey(key => act(key));
            return flow;
        }
        
        public static Flow<ImageFormBuilder> OnKey(this Flow<ImageFormBuilder> flow, Keys keys, Action act)
        {
            flow.State.OnKey(key => 
            {
                if (keys == key)
                    act();
            });
            return flow;
        }
    
        public static Flow<ImageFormBuilder> Show(this Flow<ImageFormBuilder> flow)
        {
            flow.State.Show();
            return flow;
        }

        public static Flow<ImageFormBuilder> SetPicture(this Flow<ImageFormBuilder> flow, Picture picture)
        {
            flow.State.SetPicture(picture);
            return flow;
        }

        public static Flow<ImageFormBuilder> Stop(this Flow<ImageFormBuilder> flow)
        {
            Camera.Stop();
            return flow;
        }

        public static Flow<T, Flow<ImageFormBuilder>> OnKey<T>(this Flow<T, Flow<ImageFormBuilder>> flow, Action act)
        {
            flow.Return.OnKey(act);
            return flow;
        }

        public static Flow<T, Flow<ImageFormBuilder>> OnKey<T>(this Flow<T, Flow<ImageFormBuilder>> flow, Action<Keys> act)
        {
            flow.Return.OnKey(act);
            return flow;
        }

        public static Flow<T, Flow<ImageFormBuilder>> OnKey<T>(this Flow<T, Flow<ImageFormBuilder>> flow, Keys keys, Action act)
        {
            flow.Return.OnKey(keys, act);
            return flow;
        }

        public static Flow<T, Flow<ImageFormBuilder>> Show<T>(this Flow<T, Flow<ImageFormBuilder>> flow)
        {
            flow.Return.Show();
            return flow;
        }
    }
}