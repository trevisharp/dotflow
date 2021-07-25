namespace Sharp.Image.Processing
{
    public static class FiltersImageProcessing
    {
        public static ByteProcessingPicture BlurBox(this Picture picture, int size)
        {
            ByteProcessingPicture pp = ByteProcessingPicture.FromPicture(picture);
            var integral = pp.BuildIntegralImage();

            //TODO

            return pp;
        }
    }
}