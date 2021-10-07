namespace Nut.Core.Platform
{
    public class NutColor
    {
        public NutColor()
        {
        }

        public NutColor(int argb)
        {
            ARGB = argb;
        }

        public NutColor(uint rgb, int alpha) : this((int)rgb, alpha)
        {
        }

        public NutColor(int rgb, int alpha)
        {
            ARGB = rgb;
            A = alpha;
        }

        public NutColor(int r, int g, int b) : this(r, g, b, 255)
        {
        }

        public NutColor(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public int R
        {
            get { return MaskAndShiftRight(this.ARGB, 0xFF0000, 16); }
            set { this.ARGB = ShiftOverwrite(this.ARGB, 0xFF00FFFF, value, 16); }
        }

        public int G
        {
            get { return MaskAndShiftRight(this.ARGB, 0xFF00, 8); }
            set { this.ARGB = ShiftOverwrite(this.ARGB, 0xFFFF00FF, value, 8); }
        }

        public int B
        {
            get { return MaskAndShiftRight(this.ARGB, 0xFF, 0); }
            set { this.ARGB = ShiftOverwrite(this.ARGB, 0xFFFFFF00, value, 0); }
        }

        public int A
        {
            get { return MaskAndShiftRight(this.ARGB, 0xFF000000, 24); }
            set { this.ARGB = ShiftOverwrite(this.ARGB, 0x00FFFFFF, value, 24); }
        }

        public int ARGB { get; set; }

        public override string ToString()
        {
            return $"argb: #{A:X2}{R:X2}{G:X2}{B:X2}";
        }

        private static int MaskAndShiftRight(int value, uint mask, int shift)
        {
            return (int)((value & mask) >> shift);
        }

        private static int ShiftOverwrite(int original, uint mask, int value, int shift)
        {
            var maskedOriginal = original & mask;
            var newBits = value << shift;
            return (int)(maskedOriginal | newBits);
        }
    }
}