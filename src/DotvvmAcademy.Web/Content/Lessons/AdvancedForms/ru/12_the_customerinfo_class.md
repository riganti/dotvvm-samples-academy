Класс CustomerInfo
==================
В предыдущих шагах, мы разместили все свойства - `FirstName`, `LastName`, `Role` и `SelectedCountryId` в одном классе с коллекцией `Countries`.

Это не лучшая практика, потому что мы смешиваем информацию о конкретном заказчике с некоторыми другими данными, например, списком стран.
Было бы неплохо отделить свойства, которые представляют состояние некоторого объекта (заказчика, в данном случае), в отдельный класс.
Этот класс содержал бы только свойства и никаких методов.
Это называется DTO - Объект передачи данных (Data Transfer Object).

Итак, мы перемещаем свойства в классе `CustomerInfo`:

```CSHARP
public class CustomerInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public int SelectedCountryId { get; set; }
}
```

`Countries` остается в viewmodel.