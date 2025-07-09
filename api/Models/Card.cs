namespace api.Models;

public class Card
{
    public int CardId { get; set; }
    public string? CardName { get; set; }
    public string? ImageUrl { get; set; }
    public int BasePhysical { get; set; }
    public int BaseTolerance { get; set; }
    public int BaseIntelligence { get; set; }
    public string? Rarity { get; set; }
    public int BaseCardId { get; set; }
}