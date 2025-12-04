namespace BLL.DTO.Types.Enums.Tools
{
    public static class EnumExtensions
    {
        public static string ToString(this OrderStatus status)
            => status switch
            {
                OrderStatus.Pending => "Ожидает принятия",
                OrderStatus.Accepted => "Принят",
                OrderStatus.InProcess => "В процессе",
                OrderStatus.Completed => "Выполнен",
                OrderStatus.Arriving => "В пути",
                OrderStatus.Arrived => "Пришёл",
                OrderStatus.Closed => "Закрыт",
                OrderStatus.Canceled => "Отменён",
                _ => "Не определён"
            };

        public static string ToString(this UserRole role)
            => role switch
            {
                UserRole.User => "Пользователь",
                UserRole.Client => "Клиент",
                UserRole.Master => "Мастер",
                UserRole.Admin => "Администратор",
                UserRole.Creator => "Создатель",
                _ => "Не определена"
            };

        public static string ToString(this DialogType type)
            => type switch
            {
                DialogType.None => "Отсутствует",
                DialogType.SimpleRegistering => "Простая регистрация",
                DialogType.Registering => "Регистрация",
                DialogType.SimpleOrdering => "Простой заказ",
                DialogType.Ordering => "Заказ",
                _ => "Не определён"
            };
    }
}