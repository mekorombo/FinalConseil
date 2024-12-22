namespace DashboardConseil.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime SubscribedOn { get; set; } = DateTime.Now;
    }

}
