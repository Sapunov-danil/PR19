namespace Kino_Sapunov.Models
{
    public class Kinoteatr
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count_zal { get; set; }
        public int count { get; set; }
        public Kinoteatr(int id, string name, int count_zal, int count) 
        {
            this.id = id;
            this.name = name;
            this.count_zal = count_zal;
            this.count = count;
        }
    }
}
