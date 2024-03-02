using ImageMagick;
using System;

namespace SmartUI
{
    public static class ImageMagick
    {
        public static void AddPadding(string filePath, string outFilePath)
        {
            // Read from file
            using MagickImage image = new(filePath);
            int imageSize = Math.Max(image.Width, image.Height);
            if (imageSize < 500)
                imageSize = 500;
            // Add padding
            image.Extent(imageSize, imageSize, Gravity.Center, MagickColors.White);

            // Save the result
            image.Write(outFilePath);
        }
    }
}
