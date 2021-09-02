namespace MaterialesIza.Data.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }
        public User user { get; set; }
    }
}
