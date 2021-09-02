namespace MaterialesIza.Data.Entities
{
    public class Provider:IEntity
    {
        public int Id { get; set; }
        public User user { get; set; }
    }
}
