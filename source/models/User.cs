namespace models
{
    public class User
    {
        public string Id { get; }
        public string Name { get; }

        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public static User Anonymous = new User("anonymous", "anonymous");
        public static User FromUserId(string id) => new User(id, id);
    }
}
