namespace Flow.Image.Processing
{
    public interface IProcessingPicture
    {
        IntegralImage BuildIntegralImage();
        Picture Close();
    }
}