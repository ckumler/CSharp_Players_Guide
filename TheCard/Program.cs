//Card testCard = new Card(CardColor.Red, CardRank.Ten);
//Console.WriteLine($"The {testCard}");
//Console.WriteLine($"Card is a number card: {testCard.IsNumberCard()}");
//Console.WriteLine($"Card is a symbol card: {testCard.IsSymbolCard()}");

int CARD_COLOR_COUNT = Enum.GetNames(typeof(CardColor)).Length;
int CARD_RANK_COUNT = Enum.GetNames(typeof(CardRank)).Length;

//Console.WriteLine($"CARD_COLOR_COUNT = {CARD_COLOR_COUNT}");
//Console.WriteLine($"CARD_RANK_COUNT = {CARD_RANK_COUNT}");

Card[] cards = new Card[CARD_COLOR_COUNT*CARD_RANK_COUNT];

//Console.WriteLine(cards.Length);

for (int i = 0; i < CARD_COLOR_COUNT; i++) 
{
    for (int j = 0;  j < CARD_RANK_COUNT; j++) 
    {
        cards[CARD_RANK_COUNT * i + j] = new Card((CardColor)i, (CardRank)j);
    }
}

foreach (Card card in cards) 
{
    Console.WriteLine($"The {card}");
}





public class Card {
    public CardColor CardColor { get; set; }
    public CardRank CardRank { get; set; }

    public Card(CardColor color, CardRank rank) 
    {
        CardColor = color;
        CardRank = rank;
    }

    public bool IsNumberCard()
    {
        if (CardRank == CardRank.Dollar || CardRank == CardRank.Percent || CardRank == CardRank.Caret || CardRank == CardRank.Ampersand)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsSymbolCard() 
    {
        if (!IsNumberCard())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"{CardColor} {CardRank}";
    }
}





public enum CardColor { Red, Green, Blue, Yellow }
public enum CardRank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Dollar, Percent, Caret, Ampersand}