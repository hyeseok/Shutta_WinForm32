using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shutta
{
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();

            Board board = new Board();

            // 동적배열
            List<Player> players = new List<Player>();
            players.Add(new Player(0));
            players.Add(new Player(1));

            Scorer scorer = new SimpleScorer();

            // 둘 중 한 명이 오링날 때까지
            while (CanGoRound(players))
            {
                // 딜러가 카드를 섞음
                dealer.Shuffle();

                // 판돈 내기
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].Money -= 100;
                    board.Money += 100;
                }

                // 각 플레이어에 카드를 2장씩 준다.
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].Cards.Clear();

                    for (int j = 0; j < 2; j++)
                    {
                        Card card = dealer.GetCard();
                        players[i].TakeCard(card);
                    }
                }

                // 승자를 찾는다
                Player winner = scorer.GetWinner(players[0], players[1]);

                if (winner == null)
                    continue;

                // 판돈을 승자에게 준다.
                winner.Money += board.Money;
                board.Money = 0;

                // 플레이어가 가진 카드와 소지금, 승자를 출력한다.
                PrintRoundResult(players, winner);
            }
        }

        private static void PrintRoundResult(List<Player> players, Player winner)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"P{players[i].No} : {players[i].Money}");

                for (int j = 0; j < 2; j++)
                    Console.Write(players[i].Cards[j].ToText() + " ");
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine($"승자는 {winner.No}.");
        }

        static bool CanGoRound(List<Player> players)
        {
            if (players[0].Money > 0 && players[1].Money > 0)
                return true;

            return false;
        }
    }
}
