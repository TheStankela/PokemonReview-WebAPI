namespace WebApiTest1.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Owner> Owners { get; set;}

    }
}
