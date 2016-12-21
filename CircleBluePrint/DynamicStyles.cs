using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace simlife
{
    class DynamicStyles
    {

        //  private static string recentBrushString = "#FF808080";
        //   private static Brush recentBrush = makeBrushFill(recentBrushString);
        public static void updateButtonStyle(string sourcedEvent, Brush passedBrush, Brush existingForegroundBrush, Brush existingBackgroundBrush)
        {
            Style style = new Style { TargetType = typeof(Button) };
            //   style.Setters.Add(new Setter(Button.OverridesDefaultStyleProperty, true));
            //     Application.Current.Properties["buttonStyle"] = style;
            System.Diagnostics.Trace.WriteLine(sourcedEvent);

            char[] delimters = new char[] { ':' };
            string[] lastHalf = sourcedEvent.Split(delimters);
            System.Diagnostics.Trace.WriteLine(lastHalf[1]);


            // depending which object calls, dictates logic for property to store and property to replace
            if (lastHalf[1] == " Change Background")//get foreground and reapply
            {

                style.Setters.Add(new Setter(Button.ForegroundProperty, existingForegroundBrush));
                style.Setters.Add(new Setter(Button.BackgroundProperty, passedBrush));
            }
            if (lastHalf[1] == " Change Text")//get background and reapply
            {
                //       Setter existingBGBrushSetter = style.Setters.Cast<Setter>().FirstOrDefault(setter => setter.Property == Button.BackgroundProperty);
                //      Brush existingBGBrush = existingBGBrushSetter != null ? (Brush)existingBGBrushSetter.Value : makeBrushFill(invpassedBrush);
                style.Setters.Add(new Setter(Button.ForegroundProperty, passedBrush));
                style.Setters.Add(new Setter(Button.BackgroundProperty, existingBackgroundBrush));// existingBGBrush ));
            }
            //  style.Setters.Add(new Setter(Button.WidthProperty, (double)40));
            style.Setters.Add(new Setter(Button.HeightProperty, (double)25));
            style.Setters.Add(new Setter(Button.MarginProperty, new Thickness(10)));
            style.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(2)));
            Application.Current.Resources["buttonStyle"] = style;

        }
        //pass hex colour string to make a brush
        public static Brush makeBrushFill(string brushString)
        {
            BrushConverter converter = new BrushConverter();
            return (Brush)converter.ConvertFromString(brushString);

        }
        //pass hexadecimal rgb strings to make hex colour
        public static string makeColourHexString(string red, string green, string blue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#FF");
            sb.Append(red);
            sb.Append(green);
            sb.Append(blue);

            return sb.ToString();
        }
        //pass hexadecimal rgb strings with alpha to make hex colour
        public static string makeColourHexString(string alpha, string red, string green, string blue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#");
            sb.Append("alpha");
            sb.Append(red);
            sb.Append(green);
            sb.Append(blue);

            return sb.ToString();
        }

        //convert double to hex string
        public static string hexed(double num)
        {
            int value = Convert.ToInt32(num);
            return value.ToString("X2");
        }
        //convert string to hex string
        public static string hexed(string num)
        {
            int value = Convert.ToInt32(num);
            return value.ToString("X2");
        }

        //give a hexadecimal string and specify r g b to return
        private static string stripToIndvidualHex(string hexString, char colour)
        {
            if (!(string.IsNullOrWhiteSpace(hexString)) && hexString.Length == 9)
            {
                char[] extractedHex = hexString.ToCharArray();
                if (char.ToLower(colour) == 'r') { return extractedHex[3].ToString() + extractedHex[4].ToString(); }
                if (char.ToLower(colour) == 'g') { return extractedHex[5].ToString() + extractedHex[6].ToString(); }
                if (char.ToLower(colour) == 'b') { return extractedHex[7].ToString() + extractedHex[8].ToString(); }
            }
            return "80";
        }
        // give a start and end colour as hexidecimal rgb and specify number of steps to get there
        public static int[] itemiseForGradients(string startHex, string endHex, int generations)
        {
            int[] items = new int[10];

            items[0] = Convert.ToInt32(stripToIndvidualHex(startHex, 'r'), 16);//int red start
            items[1] = Convert.ToInt32(stripToIndvidualHex(startHex, 'g'), 16);// int green start
            items[2] = Convert.ToInt32(stripToIndvidualHex(startHex, 'b'), 16);// int blue start
            items[3] = Convert.ToInt32(stripToIndvidualHex(endHex, 'r'), 16);//int red end
            items[4] = Convert.ToInt32(stripToIndvidualHex(endHex, 'g'), 16);//int green end
            items[5] = Convert.ToInt32(stripToIndvidualHex(endHex, 'b'), 16);//int blue end

            items[6] = Math.Abs(items[0] - items[3]);//int red difference
            items[7] = Math.Abs(items[1] - items[4]);//int green difference
            items[8] = Math.Abs(items[2] - items[5]);//int blue difference
            items[9] = generations;//int number of generations to process
            return items;
        }

        public static string rgb_to_hsv(string hex)
        {
            string r, g, b;
            int maxCol=0;
              int   minCol=0;
            int chroma=0;
            r = stripToIndvidualHex(hex, 'r');
            g = stripToIndvidualHex(hex, 'g');
            b = stripToIndvidualHex(hex, 'b');
            int decR = Convert.ToInt32(r, 16);
            int decG = Convert.ToInt32(g, 16);
            int decB = Convert.ToInt32(b, 16);

            
            if (decR >= decG && decR>=decB)
            {               
                    maxCol = decR;
                    if (decB >= decG) { minCol = decG; } else { minCol = decB; }
            

            } if (decG >= decR && decG >= decB)
            {
                maxCol = decG;
                   if (decB >= decR) {minCol = decR;} else{ minCol = decB;}

            } if (decB >= decR && decB >= decG)
            {
                maxCol = decB;
                   if (decR >= decG){ minCol = decG;} else{ minCol = decR;}

            }
            chroma = maxCol - minCol;

            return null;

        }

    }
}
