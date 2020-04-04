using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace AppProj.Web.Helpers
{
    public class ImageSaveWithResize
    {
        int bmpW;
        int bmpH; 
        Stream imgStream; 
                
        public ImageSaveWithResize(int width, int height, Stream imgStream)
        {
            this.bmpH = height;
            this.bmpW = width;
            this.imgStream = imgStream;
        }
        public Bitmap GetBitmap()
        {
            Int32 newWidth = bmpW;
            Int32 newHeight = bmpH;

            Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgStream);
            Bitmap newBmp = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            newBmp.SetResolution(72, 72);

            Double upWidth = upBmp.Width;
            Double upHeight = upBmp.Height;


            int newX = 0;
            //Set the new top left drawing position on the image canvas
            int newY = 0;
            Double reDuce;

            //Keep the aspect ratio of image the same if not 4:3 and work out the newX and newY positions
            //to ensure the image is always in the centre of the canvas vertically and horizontally


            if (upWidth > upHeight)
            {
                //Landscape picture
                reDuce = newWidth / upWidth;
                //calculate the width percentage reduction as decimal
                newHeight = ((Int32)(upHeight * reDuce));
                //reduce the uploaded image height by the reduce amount
                newY = ((Int32)((bmpH - newHeight) / 2));
                //Position the image centrally down the canvas
                newX = 0;
                //Picture will be full width
            }
            else if (upWidth < upHeight)
            {
                //Portrait picture
                reDuce = newHeight / upHeight;
                //calculate the height percentage reduction as decimal
                newWidth = ((Int32)(upWidth * reDuce));
                //reduce the uploaded image height by the reduce amount
                newX = ((Int32)((bmpW - newWidth) / 2));
                //Position the image centrally across the canvas
                newY = 0;
                //Picture will be full hieght
            }
            else if (upWidth == upHeight)
            {
                //square picture
                reDuce = newHeight / upHeight;
                //calculate the height percentage reduction as decimal
                newWidth = ((Int32)(upWidth * reDuce));
                //reduce the uploaded image height by the reduce amount
                newX = ((Int32)((bmpW - newWidth) / 2));
                //Position the image centrally across the canvas
                newY = ((Int32)((bmpH - newHeight) / 2));
                //Position the image centrally down the canvas
            }

            //Create a new image from the uploaded picture using the Graphics class
            //Clear the graphic and set the background colour to white
            //Use Antialias and High Quality Bicubic to maintain a good quality picture
            //Save the new bitmap image using 'Png' picture format and the calculated canvas positioning
            Graphics newGraphic = Graphics.FromImage(newBmp);

            try
            {
                newGraphic.Clear(Color.White);
                newGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                newGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newGraphic.DrawImage(upBmp, newX, newY, newWidth, newHeight);
                
                return newBmp;
            }


            catch
            {  
              
            }
            finally
            {
                upBmp.Dispose();                
                newGraphic.Dispose();
            }

            return null;
        }
    }
}