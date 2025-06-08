namespace PersonalBlog.Models;

public class CreateViewModel
{
    public Article Article { get; set; } = new Article();
    public bool IdInUse { get; set; } = false;
}