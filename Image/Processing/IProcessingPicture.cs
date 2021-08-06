namespace Flow.Image.Processing
{
    public interface IProcessingPicture : IPicture
    {
        IntegralImage BuildIntegralImage();
        Picture Close();
    }
}