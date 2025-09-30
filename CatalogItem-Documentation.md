# CatalogItem Klasse - Dokumentation

## Übersicht
Die `CatalogItem` Klasse repräsentiert ein Produktelement im eShop-Katalog und befindet sich im Namespace `eShop.Catalog.API.Model`.

## Abhängigkeiten
- `System.ComponentModel.DataAnnotations`
- `System.Text.Json.Serialization`
- `Pgvector`

## Eigenschaften (Properties)

### Grundlegende Produktinformationen
| Property | Typ | Beschreibung | Validierung |
|----------|-----|--------------|-------------|
| `Id` | `int` | Eindeutige Produkt-ID | - |
| `Name` | `string` | Produktname | `[Required]` |
| `Description` | `string` | Produktbeschreibung | - |
| `Price` | `decimal` | Produktpreis | - |
| `PictureFileName` | `string` | Dateiname des Produktbildes | - |

### Kategorisierung
| Property | Typ | Beschreibung |
|----------|-----|--------------|
| `CatalogTypeId` | `int` | ID der Produktkategorie |
| `CatalogType` | `CatalogType` | Referenz zur Produktkategorie |
| `CatalogBrandId` | `int` | ID der Produktmarke |
| `CatalogBrand` | `CatalogBrand` | Referenz zur Produktmarke |

### Lagerbestand
| Property | Typ | Beschreibung |
|----------|-----|--------------|
| `AvailableStock` | `int` | Verfügbare Lagerbestände |
| `RestockThreshold` | `int` | Mindestbestand für Nachbestellung |
| `MaxStockThreshold` | `int` | Maximum lagerfähige Menge |
| `OnReorder` | `bool` | Gibt an, ob das Produkt nachbestellt wird |

### Erweiterte Features
| Property | Typ | Beschreibung | Attribute |
|----------|-----|--------------|-----------|
| `Embedding` | `Vector` | Optionales Embedding für die Produktbeschreibung | `[JsonIgnore]` |

## Methoden

### RemoveStock(int quantityDesired)
**Zweck:** Reduziert den Lagerbestand eines Produkts.

**Parameter:**
- `quantityDesired` (int): Gewünschte Menge zum Entfernen

**Rückgabe:** 
- `int`: Tatsächlich aus dem Lager entfernte Menge

**Funktionsweise:**
- Überprüft, ob Lagerbestand vorhanden ist
- Validiert, dass die gewünschte Menge positiv ist
- Entfernt die verfügbare Menge (maximal die gewünschte Menge)
- Aktualisiert den `AvailableStock`

**Exceptions:**
- `CatalogDomainException`: Bei leerem Lager oder ungültiger Menge

**Beispiel:**
```csharp
// Versuche 5 Einheiten zu entfernen
int removed = catalogItem.RemoveStock(5);
// removed enthält die tatsächlich entfernte Menge
```

### AddStock(int quantity)
**Zweck:** Erhöht den Lagerbestand eines Produkts.

**Parameter:**
- `quantity` (int): Hinzuzufügende Menge

**Rückgabe:** 
- `int`: Tatsächlich hinzugefügte Menge

**Funktionsweise:**
- Überprüft das maximale Lagerlimit (`MaxStockThreshold`)
- Fügt die Menge hinzu, ohne das Maximum zu überschreiten
- Setzt `OnReorder` auf `false`
- Gibt die tatsächlich hinzugefügte Menge zurück

**Beispiel:**
```csharp
// Füge 10 Einheiten hinzu
int added = catalogItem.AddStock(10);
// added enthält die tatsächlich hinzugefügte Menge
```

## Konstruktor
Die Klasse hat einen parameterlosen Standardkonstruktor.

## Verwendung
Diese Klasse wird in der Catalog.API für die Verwaltung von Produktdaten verwendet und unterstützt:
- Lagerbestandsverwaltung
- Produktkategorisierung  
- Nachbestellungslogik
- Vektor-basierte Suchfunktionen (über Embeddings)

## Beziehungen
- Gehört zu einer `CatalogType` (Produktkategorie)
- Gehört zu einer `CatalogBrand` (Produktmarke)
- Unterstützt Vektor-Embeddings für erweiterte Suchfunktionen

## Geschäftslogik
- **Lagerbestandsschutz:** Verhindert negative Bestände
- **Maximale Lagerkapazität:** Begrenzt Lagerbestände auf physische Grenzen
- **Nachbestellungslogik:** Automatische Kennzeichnung für Nachbestellungen
