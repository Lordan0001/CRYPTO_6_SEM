using System;
using System.Drawing;
using System.Text;

namespace lab_14
{
    class LSBAlternative
    {
        private static int ReverseBits(int n)
        {
            int result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;
                n /= 2;
            }
            return result;
        }

        public static Bitmap HideText(string text, Bitmap bmp)
        {
            int charIndex = 0;
            int charValue = 0;
            int pixelIndex = 0;
            int bitsPerChar = 8; // Number of bits used for each character
            int maxChars = bmp.Width * bmp.Height * 3 / bitsPerChar; // Maximum number of characters that can be hidden in the image

            // Check if the text is too long to hide in the image
            if (text.Length > maxChars)
            {
                throw new ArgumentException("Text is too long to hide in the image.");
            }

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);

                    // Extract the RGB components of the pixel
                    int R = pixel.R;
                    int G = pixel.G;
                    int B = pixel.B;

                    for (int n = 0; n < 3; n++)
                    {
                        if (charIndex < text.Length)
                        {
                            charValue = text[charIndex++];
                        }
                        else
                        {
                            // If we have processed all characters, set charValue to 0
                            charValue = 0;
                        }

                        // Place the bits of charValue into the least significant bits of the RGB components
                        switch (pixelIndex % 3)
                        {
                            case 0: // Red component
                                {
                                    R = (R & 0xFE) | (charValue & 0x01);
                                    charValue >>= 1;
                                }
                                break;
                            case 1: // Green component
                                {
                                    G = (G & 0xFE) | (charValue & 0x01);
                                    charValue >>= 1;
                                }
                                break;
                            case 2: // Blue component
                                {
                                    B = (B & 0xFE) | (charValue & 0x01);
                                    charValue >>= 1;
                                }
                                break;
                        }
                        bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                        pixelIndex++;

                        if (charIndex >= text.Length)
                        {
                            break; // Exit the loop if we have processed all characters
                        }
                    }
                }
            }
            return bmp;
        }

        public static string ExtractText(Bitmap bmp)
        {
            int charValue = 0;
            string extractedText = "";

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    for (int n = 0; n < 3; n++)
                    {
                        // Extract the least significant bit of each RGB component
                        switch (n)
                        {
                            case 0: // Red component
                                {
                                    charValue = (charValue << 1) | (pixel.R & 0x01);
                                }
                                break;
                            case 1: // Green component
                                {
                                    charValue = (charValue << 1) | (pixel.G & 0x01);
                                }
                                break;
                            case 2: // Blue component
                                {
                                    charValue = (charValue << 1) | (pixel.B & 0x01);
                                }
                                break;
                        }

                        if (charValue % 8 == 0)
                        {
                            charValue = ReverseBits(charValue);
                            if (charValue == 0)
                            {
                                return extractedText;
                            }
                            char c = (char)charValue;
                            extractedText += c.ToString();
                        }
                    }
                }
            }
            return extractedText;
        }
    }
}
