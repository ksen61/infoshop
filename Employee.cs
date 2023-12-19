namespace Pract10
{
    public class Employee
    {
        public int id;
        public string f;
        public string i;
        public string o;
        public DateTime birthday;
        public string passport;
        public string post;
        public float salary;
        public int? user_id;

        public Employee(int id, string f, string i, DateTime birthday, string passport, string post, float salary, int? user_id = null, string o = "")
        {
            this.id = id;
            this.f = f;
            this.i = i;
            this.o = o;
            this.birthday = birthday;
            this.passport = passport;
            this.post = post;
            this.salary = salary;
            this.user_id = user_id;
        }
    }
}
