namespace DynatronChallenge.Model
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email
        {
            get { return $"{last_name}@email.com"; }
            set { }
        }
        public string created { get; set; }
        public string last_updated { get; set; }
    }
}
