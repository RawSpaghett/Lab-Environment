using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Self-playing card game that draws and discards cards until they have three matching suits or the deck runs out.
/// </summary>
public class CardGame : MonoBehaviour
{
    private string[] Ranks = { "K", "Q", "J", "A" };
    private string[] Suits = { "\u2660", "\u2665", "\u2666", "\u2663" };

    private void Start()
    {
        List<string> deck = BuildShuffleDeck();

        //Deals the opening hand
        List<string> hand = deck.Take(4).ToList();
        deck = deck.Skip(4).ToList();

        Debug.Log("I have drawn: " + FormatHand(hand));

        if (HasWon(hand))
        {
            Debug.Log("I won with my opening hand: My hand is " + FormatHand(hand));
            return;
        }

        while (deck.Count > 0)
        {
            // Discards a random card and draws a new one
            int discardIndex = Random.Range(0, hand.Count);
            string discardedCard = hand[discardIndex];

            string drawn = deck[0];
            deck.RemoveAt(0);
            hand[discardIndex] = drawn;

            string handStr = FormatHand(hand);

            if (HasWon(hand))
            {
                Debug.Log("I won! I discarded " + discardedCard + " and drew " + drawn + ". My hand is now " + handStr);
                return;
            }
            else
            {
                Debug.Log("I discarded " + discardedCard + " and drew " + drawn + ". My hand is now " + handStr);
            }
            Debug.Log("Deck has " + deck.Count + " cards left.");
        }

        Debug.Log("The deck is empty I Lost!");
    }

    private List<string> BuildShuffleDeck()
    {
        List<string> deck = new List<string>();

        foreach (string rank in Ranks)
        {
            foreach (string suit in Suits)
            {
                deck.Add(rank + suit);
            }
        }

        // LINQ shuffle: assign each card a random float and sort by it
        return deck.OrderBy(x => Random.value).ToList();
    }

    private bool HasWon(List<string> hand)
    {
        return Suits.Any(suit => hand.Count(card => card.EndsWith(suit)) >= 3);
    }

    private string FormatHand(List<string> hand)
    {
        return string.Join(", ", hand);
    }
}