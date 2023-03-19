using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palette
{
    public partial class ImageForm : Form
    {
        private static readonly SolidBrush blackBrush = new SolidBrush(Color.Black);
        private readonly Color[] pixels;
        private Color[] palette = Array.Empty<Color>();
        public ImageForm(string filePath)
        {
            InitializeComponent();
            Image image = Image.FromFile(filePath);
            imageDisplay.Image = image;
            imageDisplay.Refresh();
            int pixelCount = image.Width * image.Height;
            Bitmap bitmap;
            if (pixelCount <= 512 * 512)
            {
                pixels = new Color[image.Width * image.Height];
                bitmap = new Bitmap(image);
            }
            else
            {
                pixels = new Color[image.Width * image.Height / 16];
                bitmap = ColorFunctions.ResizeImage(image, image.Width / 4, image.Height / 4);
            }
            using (bitmap)
            {
                for (int y = 0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++)
                        pixels[y * bitmap.Width + x] = bitmap.GetPixel(x, y);
            }
        }

        private void buttonGeneratePalette_Click(object sender, EventArgs e)
        {
            //int.TryParse(textBoxPaletteSize.Text, out int paletteSize);
            //int.TryParse(textBoxIteration.Text, out int iteration);
            int paletteSize = trackBar1.Value;

            buttonGeneratePalette.Enabled = false;
            buttonGeneratePalette.Text = "Wait..";
            panelPalette.Controls.Clear();
            GeneratePalette(paletteSize, 32);
        }

        private async void GeneratePalette(int paletteSize, int iterations)
        {
            await Task.Run(() =>
            {
                palette = ColorFunctions.BuildPaletteKMeans(pixels, paletteSize, 32, out var pixelCountPerCluster);
            });
            int panelSize = (panelPalette.Width - 16) / 4 - 6;
            for (int i = 0; i < palette.Length; i++)
            {
                Panel colorPanel = new()
                {
                    //BackColor = palette[i],
                    Size = new Size(panelSize, panelSize),
                    Tag = new SolidBrush(palette[i])
                };
                colorPanel.Paint += ColorPanel_Paint;
                panelPalette.Controls.Add(colorPanel);
            }

            buttonGeneratePalette.Text = "Get palette!";
            buttonGeneratePalette.Enabled = true;
            //textBoxPaletteSize.Enabled = true;
        }
        private void ColorPanel_Paint(object? sender, PaintEventArgs e)
        {
            if (sender == null) return;
            Panel panel = (Panel)sender;
            Rectangle rect = panel.DisplayRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            Rectangle smallerReact = rect;
            smallerReact.X += 1;
            smallerReact.Y += 1;
            smallerReact.Width -= 2;
            smallerReact.Height -= 2;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(blackBrush, rect);
            e.Graphics.FillEllipse((SolidBrush)panel.Tag, smallerReact);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }
    }
}
