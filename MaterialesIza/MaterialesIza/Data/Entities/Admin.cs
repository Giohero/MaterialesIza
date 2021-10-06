namespace MaterialesIza.Data.Entities
{
    public class Admin : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

    }
        
}
