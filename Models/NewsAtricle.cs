namespace BaSaltWellnesApp.Models
{
    public class NewsAtricle
    {
        public string Id { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Category {  get; set; }
        public string Language {  get; set; }
        public string Country { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
