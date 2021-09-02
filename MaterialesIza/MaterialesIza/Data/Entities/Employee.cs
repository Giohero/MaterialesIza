namespace MaterialesIza.Data.Entities
{
    public class Employee:IEntity
    {
        public int Id { get; set; }
        public User user { get; set; }
    }
}
