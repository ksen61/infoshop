namespace Pract10
{
    public class Note
    {
        public int id;
        public string name;
        public float sum;
        public DateTime date;
        public bool prihod;

        public Note(int id, string name, float sum, DateTime date, bool prihod)
        {
            this.id = id;
            this.name = name;
            this.sum = sum;
            this.date = date;
            this.prihod = prihod;
        }
    }
}
