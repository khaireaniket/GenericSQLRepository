namespace HowToConsume.GenericRepository.SQL.Models.Request
{
    public class PersonCreateRequest
    {
        public string PersonId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
