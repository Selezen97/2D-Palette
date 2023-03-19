using Colourful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Palette
{
    internal static class ColorFunctions
    {
        private struct Vector3
        {
            public double x, y, z;
            public Vector3() { x = 0; y = 0; z = 0; }
            public Vector3(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public Vector3(in LabColor color)
            {
                x = color.L;
                y = color.a;
                z = color.b;
            }
            public void Add(in Vector3 v)
            {
                x += v.x;
                y += v.y;
                z += v.z;
            }

            public void Add(double x, double y, double z)
            {
                this.x += x;
                this.y += y;
                this.z += z;
            }

            public void Add(in LabColor color)
            {
                x += color.L;
                y += color.a;
                z += color.b;
            }
            public void Sub(in Vector3 v)
            {
                x -= v.x;
                y -= v.y;
                z -= v.z;
            }

            public double Magnitude2 => x*x+y*y+z*z;
            public static Vector3 operator +(in Vector3 v1, in Vector3 v2)
            {
                return new Vector3(v1.x+v2.x, v1.y+v2.y, v1.z+v2.z);
            }
            public static Vector3 operator -(in Vector3 v1, in Vector3 v2)
            {
                return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
            }
            public static Vector3 operator /(in Vector3 v1, int scalar)
            {
                return new Vector3(v1.x/scalar, v1.y/scalar, v1.z/scalar);
            }
        }
        //end Vector
        private static readonly Random random = new Random();
        private static readonly IColorConverter<RGBColor, LabColor> colorConverterRGBtoLab = new ConverterBuilder()
            .FromRGB(RGBWorkingSpaces.sRGB)
            .ToLab(Illuminants.D65)
            .Build();
        private static readonly IColorConverter<LabColor, RGBColor> colorConverterLabToRGB = new ConverterBuilder()
            .FromLab(Illuminants.D65)
            .ToRGB(RGBWorkingSpaces.sRGB)
            .Build();
        public static Color[] BuildPaletteKMeans(Color[] pixels, int numberOfClusters, int iterations, out int[] clustersSizes)
        {
            if (pixels.Length <= numberOfClusters)
            {
                clustersSizes = new int[pixels.Length];
                for (int i = 0; i < pixels.Length; i++)
                {
                    clustersSizes[i] = 1;
                }
                return pixels;
            }

            Vector3[] pixelsLab = new Vector3[pixels.Length];
            for (int i = 0;i < pixels.Length; i++)
            {
                pixelsLab[i] = new Vector3(colorConverterRGBtoLab.Convert(new RGBColor(pixels[i])));
            }
            int[] pixelClusterIds = new int[pixels.Length];
            Vector3[] clustersSums = new Vector3[numberOfClusters];
            clustersSizes = new int[numberOfClusters];
            for (int i=0;i<clustersSums.Length;i++)
            {
                clustersSums[i] = new Vector3();
                clustersSizes[i] = 0;
            }
            //Assign pixels to random clusters
            for (int pixelId = 0; pixelId<pixelsLab.Length;pixelId++)
            {
                int groupId = random.Next(numberOfClusters);
                pixelClusterIds[pixelId] = groupId;
                clustersSums[groupId].Add(pixelsLab[pixelId]);
                clustersSizes[groupId]++;
            }
            bool atLeastOneColorHasMovedBetweenGroups = true;
            //Iterate
            for (int iteration=0; (iteration<iterations) && atLeastOneColorHasMovedBetweenGroups; iteration++)
            {
                atLeastOneColorHasMovedBetweenGroups = false;
                for (int pixelId = 0; pixelId<pixelsLab.Length; pixelId++)
                {
                    var pixel = pixelsLab[pixelId];
                    int currentClusterId = pixelClusterIds[pixelId];
                    double minDifference = (clustersSums[currentClusterId] / clustersSizes[currentClusterId] - pixel).Magnitude2;
                    for (int clusterId = 0; clusterId < numberOfClusters; clusterId++)
                    {
                        if (clusterId == currentClusterId) continue;
                        double difference = (clustersSums[clusterId] / clustersSizes[clusterId] - pixel).Magnitude2;
                        if (difference < minDifference)
                        {
                            atLeastOneColorHasMovedBetweenGroups = true;
                            pixelClusterIds[pixelId] = clusterId;

                            clustersSizes[currentClusterId]--;
                            clustersSizes[clusterId]++;

                            clustersSums[currentClusterId].Sub(pixel);
                            clustersSums[clusterId].Add(pixel);

                            currentClusterId = clusterId;
                            minDifference = (clustersSums[currentClusterId] / clustersSizes[currentClusterId] -pixel).Magnitude2;
                        }
                    }
                }
            }

            Vector3[] clusterCentroids = new Vector3[numberOfClusters];
            for (int clusterId = 0;clusterId < numberOfClusters; clusterId++)
            {
                if (clustersSizes[clusterId]==0)
                    clusterCentroids[clusterId] = new Vector3(50,0,0);
                else
                    clusterCentroids[clusterId] = clustersSums[clusterId] / clustersSizes[clusterId];
            }
            Array.Sort(clusterCentroids, (a,b) =>
            {
                return a.x.CompareTo(b.x);
            });
            //Result
            var palette = new Color[numberOfClusters];
            for (int clusterId = 0; clusterId < palette.Length; clusterId++)
            {
                var labPaletteColor = new LabColor(clusterCentroids[clusterId].x, clusterCentroids[clusterId].y, clusterCentroids[clusterId].z);
                palette[clusterId] = colorConverterLabToRGB.Convert(labPaletteColor).ToColor();
            }
            return palette;
        }

        public static Bitmap ResizeImage(Image image,int width,int height)
        {
            var destRect = new Rectangle(0,0,width,height);
            var destImage = new Bitmap(width,height);
            destImage.SetResolution(image.HorizontalResolution,image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image,destRect,0,0,image.Width,image.Height,GraphicsUnit.Pixel,wrapMode);
                }
            }
            return destImage;
        }
    }

}
