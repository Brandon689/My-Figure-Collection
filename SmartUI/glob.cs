using System.IO;

namespace SmartUI
{
    public static class Glob
    {
        public static string Pic = @"C:\GITHUB\figureseller\MyAnimeFigureCollection\image\";
        public static string MorePic = @"C:\GITHUB\figureseller\MyAnimeFigureCollection\moreimages\";
        public static readonly string PaddedImg = @"C:\GITHUB\figureseller\MyAnimeFigureCollection\paddedimg\";

        //public static string FullPic(int x) => $"{Pic}{x}.jpg";
        public static string FullPic(int x)
        {
            var FUCKYOU = $"{Pic}{x}.jpg";
            if (File.Exists(FUCKYOU))
            {
                return FUCKYOU;
            }
            FUCKYOU = FUCKYOU.Replace(".jpg", ".png");
            return FUCKYOU;
        }
    }
}
