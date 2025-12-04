namespace DAL.EfCore.Models;

public partial class Review
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public long UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int Grade { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
