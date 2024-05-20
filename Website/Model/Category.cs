namespace Website.Model
{
    public class Category
    {
        
        public int Id { get; set; }

        public string Name { get; set; } // Remove nullable operator '?'

        public string Slug { get; set; } // Change 'String' to 'string'

    }
}