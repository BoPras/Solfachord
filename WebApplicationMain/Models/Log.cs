namespace WebApplicationMain.Models
{
    public class Log
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public int StatudeCode { get; set; }
    }
}
