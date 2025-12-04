namespace DAL.EfCore.Models;

public partial class Order
{
    public int Id { get; set; }

    public long UserId { get; set; }

    public int TileId { get; set; }

    public int Amount { get; set; }

    public string Address { get; set; } = null!;

    public int Status { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Tile Tile { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
