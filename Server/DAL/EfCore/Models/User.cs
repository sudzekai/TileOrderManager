namespace DAL.EfCore.Models;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? Role { get; set; }

    public int? DialogType { get; set; }

    public int? Step { get; set; }

    public int? TileId { get; set; }

    public int? LastMessageId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Tile? Tile { get; set; }
}
