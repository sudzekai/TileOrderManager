using BLL.DTO.Interfaces.Create;

namespace BLL.DTO.Objects.User.Create
{
    public class UserCreateDto(long id, string username) : ICreateDto<DAL.EfCore.Models.User>
    {
        public long Id { get; set; } = id;

        public string Username { get; set; } = username;

        public DAL.EfCore.Models.User ToModel()
            => new()
            {
                Id = Id,
                Username = Username,
            };
    }
}
