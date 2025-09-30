namespace eShop.Basket.API.Model;

public class BasketItem : IValidatableObject
/// <summary>
/// Stellt einen Artikel im Warenkorb dar.
/// </summary>
/// <property name="Id">Die eindeutige Kennung des Warenkorb-Artikels.</property>
/// <property name="ProductId">Die Produkt-ID des Artikels.</property>
/// <property name="ProductName">Der Name des Produkts.</property>
/// <property name="UnitPrice">Der aktuelle Einzelpreis des Produkts.</property>
/// <property name="OldUnitPrice">Der vorherige Einzelpreis des Produkts.</property>
/// <property name="Quantity">Die Anzahl der Einheiten dieses Produkts im Warenkorb.</property>
/// <property name="PictureUrl">Die URL zum Produktbild.</property>
/// <summary>
/// Überprüft die Gültigkeit der Eigenschaften des Warenkorb-Artikels.
/// </summary>
/// <param name="validationContext">Der Kontext, in dem die Validierung ausgeführt wird.</param>
/// <returns>Eine Auflistung von <see cref="ValidationResult"/>-Objekten, die Validierungsfehler enthalten.</returns>
{
    public string Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (Quantity < 1)
        {
            results.Add(new ValidationResult("Invalid number of units", new[] { "Quantity" }));
        }

        return results;
    }
}
