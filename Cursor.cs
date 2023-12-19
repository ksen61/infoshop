namespace Pract10
{
    public class Cursor
    {
        public int currentpos = 0;
        private int maxpos;
        private int minpos;

        public Cursor(int minpos, int maxpos)
        {
            Console.SetCursorPosition(0, 0);
            this.minpos = minpos;
            this.maxpos = maxpos;

            currentpos = minpos;
            Show(-1);
        }

        public void Show(int prevpos)
        {
            if (minpos > maxpos)
            {
                return;
            }

            if (prevpos >= 0)
            {
                Console.SetCursorPosition(0, prevpos);
                Console.WriteLine("  ");
            }

            Console.SetCursorPosition(0, currentpos);
            Console.WriteLine("->");

            int viewPosition = Math.Max(currentpos - minpos, 0);
            Console.SetCursorPosition(0, viewPosition);
        }

        public void Next()
        {
            int prevpos = currentpos;
            currentpos += 1;
            currentpos = currentpos > maxpos ? minpos : currentpos;
            Show(prevpos);
        }
        public void Prev()
        {
            int prevpos = currentpos;
            currentpos -= 1;
            currentpos = currentpos < minpos ? maxpos : currentpos;
            Show(prevpos);
        }

        public int GetIndex()
        {
            return currentpos - minpos;
        }

        public void SetMax(int maxPos)
        {
            this.maxpos = maxPos;
            currentpos = currentpos > maxPos ? minpos : currentpos;
        }

        public void SetMin(int minpos)
        {
            this.minpos = minpos;
            currentpos = currentpos < minpos ? maxpos : currentpos;
        }
    }
}
